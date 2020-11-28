using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using WindowsWrapper.Exceptions;
using WindowsWrapper.FileSystem.Processes;
using WindowsWrapper.Interop.Enums;
using WindowsWrapper.Interop.Invokers;

namespace WindowsWrapper.FileSystem
{
    public class WindowsFile : IFile
    {
        /// <summary>
        /// Creates a representation of a windows file using an existing file information object
        /// </summary>
        /// <param name="fileInfo">Information relating to a file</param>
        public WindowsFile(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        /// <summary>
        /// Creates a representation of a windows file using the path
        /// </summary>
        /// <param name="filePath">The full path to the file</param>
        public WindowsFile(string filePath)
        {
            FileInfo = new FileInfo(filePath);
        }

        private FileInfo FileInfo { get; }

        public string FindExecutable()
        {
            if (!FileInfo.Exists)
            {
                throw new FileNotFoundException(
                    "Cannot open a file which does not exist. Please use the static methods GetAssociatedExecutable or GetAssociatedApp instead.");
            }


            // If we are finding the executable for an executable, return the current file
            if (FileInfo.Extension.Equals(".exe", StringComparison.OrdinalIgnoreCase))
            {
                return FileInfo.FullName;
            }

            // https://social.msdn.microsoft.com/Forums/vstudio/en-US/af164414-592a-42bb-bb24-47c4fe580123/how-do-i-get-the-value-of-maxpath-under-c
            const int maxPath = 260;
            StringBuilder pathBuilder = new StringBuilder(maxPath);
            int result = FileInvoker.FindExecutable(FileInfo.Name, FileInfo.DirectoryName, pathBuilder);

            if (result > 32)
            {
                return pathBuilder.ToString();
            }

            if (result == 2)
            {
                throw new FileNotFoundException($"File was not found at {FileInfo.FullName}");
            }

            if (result == 3)
            {
                throw new InvalidOperationException($"The path ({FileInfo.FullName}) was invalid");
            }

            if (result == 5)
            {
                throw new SecurityException($"The file could not be accessed at {FileInfo.FullName}");
            }

            if (result == 8)
            {
                throw new OutOfMemoryException($"The system is out of memory or resources while finding the executable for {FileInfo.FullName}");
            }

            if (result == 31)
            {
                throw new InvalidOperationException($"There is no association for the specified file type, for {FileInfo.FullName}");
            }

            throw new NoAssociationFoundException($"Could not find the associated executable for {FileInfo.FullName}. The resul {result} was returned which we were not expecting", FileInfo.Extension);
        }

        public string GetAssociatedExecutable()
        {
            string executable = GetAssociatedExecutable(FileInfo.Extension);
            return executable;
        }

        public string GetAssociatedApp()
        {
            string app = GetAssociatedApp(FileInfo.Extension);
            return app;
        }

        public bool TryFindExecutable(out string path, out HandleType handleType)
        {
            // If we are finding the executable for an executable, return the current file
            if (FileInfo.Extension.Equals(".exe", StringComparison.OrdinalIgnoreCase))
            {
                path = FileInfo.FullName;
                handleType = HandleType.Executable;
                return true;
            }

            try
            {
                string associatedExecutable = GetAssociatedExecutable(FileInfo.Extension);
                path = associatedExecutable;
                handleType = HandleType.Executable;
                return true;
            }
            // If we can't find an executable, fair enough. It may be an app.
            catch (NoAssociationFoundException)
            {
            }

            try
            {
                string associatedApp = GetAssociatedApp(FileInfo.Extension);
                path = associatedApp;
                handleType = HandleType.App;
                return true;
            }
            // If we can't find an app, we may not have an association
            catch (NoAssociationFoundException)
            {
            }

            path = null;
            handleType = HandleType.NotFound;

            return false;
        }

        public bool TryExecute()
        {
            bool isExecutableFound = TryFindExecutable(out string executablePath, out HandleType handleType);

            if (!isExecutableFound)
            {
                return false;
            }

            try
            {
                if (handleType == HandleType.Executable)
                {
                    StartExecutable(executablePath);
                }
                else
                {
                    StartApp(executablePath);
                }

                return true;
            }
            catch (Win32Exception)
            {
                return false;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        private void StartExecutable(string executablePath)
        {
            string[] executableAndFlags = executablePath.Split(' ');

            // Skip the main executable name and treat anything else as a flag
            string arguments = string.Join(" ", executableAndFlags.Skip(1));
            arguments += FileInfo.FullName;
            arguments = arguments.TrimStart();

            Process startedProcess;

            // The majority of files in a windows heirarchy are probably not executables (e.g. dlls, txt files, images, etc). Default to the majority case
            if (!executableAndFlags.First().Equals(FileInfo.FullName))
            {
                startedProcess = Process.Start(executableAndFlags.First(), arguments);
            }
            else
            {
                startedProcess = Process.Start(FileInfo.FullName);
            }

            ExecutedProcess = new ExecutedProcess(executablePath, FileInfo.FullName, startedProcess);
        }

        private void StartApp(string appPath)
        {
            Process.Start(FileInfo.FullName);
            ExecutedProcess = new AppProcess(appPath, FileInfo.FullName);
        }

        public static string GetAssociatedApp(string fileExtension)
        {
            string appId = GetFileAssociation(AssocF.None, AssocStr.AppID, fileExtension);

            return appId;
        }

        public static string GetAssociatedExecutable(string fileExtension)
        {
            string commandLinePath = GetFileAssociation(AssocF.None, AssocStr.Executable, fileExtension);

            // If the command line path only includes arguments then it must be expecting to execute it as-is with command prompt
            if (commandLinePath.StartsWith("%1"))
            {
                return @"C:\WINDOWS\system32\cmd.exe /c";
            }

            if (commandLinePath.Equals(@"C:\WINDOWS\System32\msiexec.exe", StringComparison.OrdinalIgnoreCase))
            {
                // If we are clicking on an MSI executable, default to installing it
                return @"C:\WINDOWS\System32\msiexec.exe /i";
            }

            return commandLinePath;
        }

        public static string GetDefaultTextEditor()
        {
            string editor = GetAssociatedExecutable(".txt");
            return editor;
        }

        /// <summary>
        /// Retrieves a file association for a particular extension
        /// From Ohad Schneider <see cref="!:https://stackoverflow.com/a/17773402"/>
        /// </summary>
        /// <param name="associationFlags">Any flags which need to be set in order to retrieve the association</param>
        /// <param name="associationType">The type of association which should be retrieved</param>
        /// <param name="fileExtension">The file association which we will use to determine the association</param>
        /// <returns></returns>
        private static string GetFileAssociation(AssocF associationFlags, AssocStr associationType, string fileExtension)
        {

            uint bufferSize = 0;
            uint returnedStatus = FileInvoker.AssocQueryString(associationFlags, associationType, fileExtension, null,
                null, ref bufferSize);

            const int successWithLength = 1;
            if (returnedStatus != successWithLength)
            {
                throw new NoAssociationFoundException(
                    $"Could not determine the required buffer size required for the file association {associationType} and file extension {fileExtension}",
                    fileExtension);
            }

            StringBuilder stringBuilder = new StringBuilder((int)bufferSize);
            returnedStatus = FileInvoker.AssocQueryString(associationFlags, associationType, fileExtension, null,
                stringBuilder, ref bufferSize);

            const int successWithAssociation = 0;
            if (returnedStatus != successWithAssociation)
            {
                throw new NoAssociationFoundException(
                    $"Could not determine a string for the file association {associationType} and file extension {fileExtension}",
                    fileExtension);
            }

            return stringBuilder.ToString();
        }

        public IProcess ExecutedProcess { get; private set; }

        public new string ToString()
        {
            return FileInfo.FullName;
        }
    }
}
