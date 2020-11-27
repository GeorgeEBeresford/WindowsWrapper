using System.Runtime.InteropServices;
using System.Text;
using WindowsWrapper.Interop.Enums;

namespace WindowsWrapper.Interop.Invokers
{
    /// <summary>
    /// Contains functions relating to a file that can be P/Invoked
    /// </summary>
    internal static class FileInvoker
    {
        /// <summary>
        /// Searches for and retrieves a file or protocol association-related string from the registry.
        /// From <see cref="!:https://docs.microsoft.com/en-gb/windows/win32/api/shlwapi/nf-shlwapi-assocquerystringa"/>
        /// </summary>
        /// <param name="flags">
        /// The flags that can be used to control the search. It can be any combination of ASSOCF values, except that only one
        /// ASSOCF_INIT value can be included.
        /// </param>
        /// <param name="str">
        /// The ASSOCSTR value that specifies the type of string that is to be returned.
        /// </param>
        /// <param name="pszAssoc">
        /// A pointer to a null-terminated string that is used to determine the root key. The following four types of strings can be used.
        /// </param>
        /// <param name="pszExtra">
        /// An optional null-terminated string with additional information about the location of the string. It is typically set to a Shell verb
        /// such as open. Set this parameter to NULL if it is not used.
        /// </param>
        /// <param name="pszOut">
        /// Pointer to a null-terminated string that, when this function returns successfully, receives the requested string. Set this
        /// parameter to NULL to retrieve the required buffer size.
        /// </param>
        /// <param name="pcchOut">
        /// A pointer to a value that, when calling the function, is set to the number of characters in the pszOut buffer. When the function
        /// returns successfully, the value is set to the number of characters actually placed in the buffer.
        /// If the ASSOCF_NOTRUNCATE flag is set in flags and the buffer specified in pszOut is too small, the function returns E_POINTER
        /// and the value is set to the required size of the buffer.
        /// If pszOut is NULL, the function returns S_FALSE and pcchOut points to the required size, in characters, of the buffer.
        /// </param>
        /// <returns></returns>
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern uint AssocQueryString(
            AssocF flags,
            AssocStr str,
            string pszAssoc,
            string pszExtra,
            [Out] StringBuilder pszOut,
            ref uint pcchOut
        );

        /// <summary>
        /// Retrieves the name of and handle to the executable (.exe) file associated with a specific document file.
        /// </summary>
        /// <param name="lpFile">
        /// The address of a null-terminated string that specifies a file name. This file should be a document.
        /// </param>
        /// <param name="lpDirectory">
        /// The address of a null-terminated string that specifies the default directory. This value can be NULL.
        /// </param>
        /// <param name="lpResult">
        /// The address of a buffer that receives the file name of the associated executable file. This file name is a null-terminated string
        /// that specifies the executable file started when an "open" by association is run on the file specified in the lpFile parameter. Put
        /// simply, this is the application that is launched when the document file is directly double-clicked or when Open is chosen from
        /// the file's shortcut menu. This parameter must contain a valid non-null value and is assumed to be of length MAX_PATH.
        /// Responsibility for validating the value is left to the programmer.
        /// </param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int FindExecutable(string lpFile, string lpDirectory, [Out] StringBuilder lpResult);
    }
}
