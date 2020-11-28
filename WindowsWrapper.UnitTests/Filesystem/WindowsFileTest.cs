using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindowsWrapper.FileSystem;
using WindowsWrapper.FileSystem.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowsWrapper.UnitTests.Filesystem
{
    [TestClass]
    public class WindowsFileTest
    {
        [TestMethod]
        public void CorrectAppOpenedFile()
        {
            AssertFileOpenedWithCorrectApp(".png", ExpectedPhotoViewer);
            AssertFileOpenedWithCorrectApp(".bmp", ExpectedPhotoViewer);
            AssertFileOpenedWithCorrectApp(".jpg", ExpectedPhotoViewer);
            AssertFileOpenedWithCorrectApp(".gif", ExpectedPhotoViewer);
        }

        [TestMethod]
        public void CorrectExecutableOpenedFile()
        {
            AssertFileOpenedWithCorrectExecutable(".bat", ExpectedBatchFileOpener);
            AssertFileOpenedWithCorrectExecutable(".txt", ExpectedEditor);
        }

        [TestMethod]
        public void FileIsExecuted()
        {
            WindowsFile testfile = GetTestFile("test.txt");
            AssertFileIsExecuted(testfile, ExpectedEditor);

            testfile = new WindowsFile(@"C:\WINDOWS\system32\NOTEPAD.EXE");
            AssertFileIsExecuted(testfile, testfile.ToString());

            testfile = new WindowsFile("setup.msi");
            AssertFileIsExecuted(testfile, ExpectedMsiExecutableOpener);

            testfile = GetTestFile("test.bat");
            AssertFileIsExecuted(testfile, ExpectedBatchFileOpener);

            testfile = GetTestFile("test.jpg");
            AssertFileIsExecuted(testfile, ExpectedPhotoViewer);

            testfile = GetTestFile("test.bmp");
            AssertFileIsExecuted(testfile, ExpectedPhotoViewer);

            testfile = GetTestFile("test.png");
            AssertFileIsExecuted(testfile, ExpectedPhotoViewer);

            testfile = GetTestFile("test.gif");
            AssertFileIsExecuted(testfile, ExpectedPhotoViewer);
        }

        [TestMethod]
        public void HandleIsRetrieved()
        {
            WindowsFile testfile = GetTestFile("test.txt");
            AssertHandleIsRetrieved(testfile, ExpectedEditor);

            testfile = GetTestFile("test.jpg");
            AssertHandleIsRetrievedWithTry(testfile, ExpectedPhotoViewer);

            testfile = GetTestFile("test.bmp");
            AssertHandleIsRetrievedWithTry(testfile, ExpectedPhotoViewer);

            testfile = GetTestFile("test.png");
            AssertHandleIsRetrievedWithTry(testfile, ExpectedPhotoViewer);

            testfile = GetTestFile("test.gif");
            AssertHandleIsRetrievedWithTry(testfile, ExpectedPhotoViewer);
        }

        [TestMethod]
        public void TextFilesOpenedWithEditor()
        {
            string defaultEditor = WindowsFile.GetDefaultTextEditor();
            Assert.IsNotNull(defaultEditor);
            Assert.AreNotEqual("", defaultEditor, $"Blank string returned for the default text editor");
            Assert.AreEqual(ExpectedEditor, defaultEditor, $"Default text editor was not the expected {ExpectedEditor}");
        }

        private void AssertFileIsExecuted(WindowsFile file, string expectedHandle)
        {
            bool isExecuted = file.TryExecute();

            Assert.AreEqual(true, isExecuted, "Process was not executed successfully");
            Assert.IsNotNull(file.ExecutedProcess, "Process was executed successully but testFile.ExecutedProcess is null");
            Assert.AreEqual(expectedHandle, file.ExecutedProcess.Name);
        }

        private void AssertHandleIsRetrievedWithTry(WindowsFile windowsFile, string expectedHandle)
        {
            bool isSuccess = windowsFile.TryFindExecutable(out string path, out HandleType handleType);

            Assert.IsTrue(isSuccess, $"Could not find an executable or app for {windowsFile}");
            Assert.AreEqual(expectedHandle, path, $"Path is not {expectedHandle} for file {windowsFile}");
            Assert.AreNotEqual(HandleType.NotFound, handleType, $"Handle type is not found for {windowsFile}");
        }

        private void AssertHandleIsRetrieved(WindowsFile windowsFile, string expectedHandle)
        {
            string handle = windowsFile.FindExecutable();
            Assert.AreEqual(expectedHandle, handle, $"Handle is not {expectedHandle} for file {windowsFile}");
            Assert.IsNotNull(handle, $"Handle is null for file {windowsFile}");
        }

        private WindowsFile GetTestFile(string fileName)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            Assert.IsNotNull(assemblyDirectory, "Could not find the current assembly location");

            string testFilePath = Path.Combine(assemblyDirectory, "TestFiles", fileName);
            FileInfo testFile = new FileInfo(testFilePath);

            Assert.IsTrue(testFile.Exists, $"Test file could not be found at {testFilePath}");

            WindowsFile windowsFile = new WindowsFile(testFile);
            return windowsFile;
        }

        private void AssertFileOpenedWithCorrectApp(string fileExtension, string expectedExecutable)
        {
            string associatedExecutable = WindowsFile.GetAssociatedApp(fileExtension);

            Assert.IsNotNull(associatedExecutable);
            Assert.AreNotEqual("", associatedExecutable, $"Blank string returned for the default executable for {fileExtension}");
            Assert.AreEqual(expectedExecutable, associatedExecutable, $"Default executable for {fileExtension} was not the expected {expectedExecutable}");
        }

        private void AssertFileOpenedWithCorrectExecutable(string fileExtension, string expectedExecutable)
        {
            string associatedExecutable = WindowsFile.GetAssociatedExecutable(fileExtension);

            Assert.IsNotNull(associatedExecutable);
            Assert.AreNotEqual("", associatedExecutable, $"Blank string returned for the default executable for {fileExtension}");
            Assert.AreEqual(expectedExecutable, associatedExecutable, $"Default executable for {fileExtension} was not the expected {expectedExecutable}");
        }

        private const string ExpectedPhotoViewer = @"Microsoft.Windows.Photos_8wekyb3d8bbwe!App";
        private const string ExpectedBatchFileOpener = @"C:\WINDOWS\system32\cmd.exe /c";
        private const string ExpectedMsiExecutableOpener = @"C:\WINDOWS\System32\msiexec.exe /i";
        private const string ExpectedEditor = @"C:\WINDOWS\system32\NOTEPAD.EXE";
    }
}
