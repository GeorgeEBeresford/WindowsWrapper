// ReSharper disable InconsistentNaming

namespace WindowsWrapper.Interop.Enums
{
    /// <summary>
    /// Provides information to the IQueryAssociations interface methods.
    /// From <see cref="!:https://web.archive.org/web/20130217094323/https://msdn.microsoft.com/en-us/library/windows/desktop/bb762471(v=vs.85).aspx"/>
    /// </summary>
    [System.Flags]
    internal enum AssocF
    {
        /// <summary>
        /// None of the following options are set.
        /// </summary>
        None = 0,
        /// <summary>
        /// Instructs IQueryAssociations interface methods not to map CLSID values to ProgID values.
        /// </summary>
        Init_NoRemapCLSID = 0x1,
        /// <summary>
        /// Identifies the value of the pwszAssoc parameter of IQueryAssociations::Init as an executable file name. If this flag
        /// is not set, the root key will be set to the ProgID associated with the .exe key instead of the executable file's ProgID.
        /// </summary>
        Init_ByExeName = 0x2,
        /// <summary>
        /// Identical to ASSOCF_INIT_BYEXENAME.
        /// </summary>
        Open_ByExeName = 0x2,
        /// <summary>
        /// Specifies that when an IQueryAssociations method does not find the requested value under the root key, it
        /// should attempt to retrieve the comparable value from the * subkey.
        /// </summary>
        Init_DefaultToStar = 0x4,
        /// <summary>
        /// Specifies that when a IQueryAssociations method does not find the requested value under the root key, it should
        /// attempt to retrieve the comparable value from the Folder subkey.
        /// </summary>
        Init_DefaultToFolder = 0x8,
        /// <summary>
        /// Specifies that only HKEY_CLASSES_ROOT should be searched, and that HKEY_CURRENT_USER should be
        /// ignored.
        /// </summary>
        NoUserSettings = 0x10,
        /// <summary>
        /// Specifies that the return string should not be truncated. Instead, return an error value and the required size for the
        /// complete string.
        /// </summary>
        NoTruncate = 0x20,
        /// <summary>
        /// Instructs IQueryAssociations methods to verify that data is accurate. This setting allows IQueryAssociations
        /// methods to read data from the user's hard disk for verification. For example, they can check the friendly name in
        /// the registry against the one stored in the .exe file. Setting this flag typically reduces the efficiency of the method.
        /// </summary>
        Verify = 0x40,
        /// <summary>
        /// Instructs IQueryAssociations methods to ignore Rundll.exe and return information about its target. Typically
        /// IQueryAssociations methods return information about the first .exe or .dll in a command string. If a command
        /// uses Rundll.exe, setting this flag tells the method to ignore Rundll.exe and return information about its target.
        /// </summary>
        RemapRunDll = 0x80,
        /// <summary>
        /// Instructs IQueryAssociations methods not to fix errors in the registry, such as the friendly name of a function not
        /// matching the one found in the .exe file.
        /// </summary>
        NoFixUps = 0x100,
        /// <summary>
        /// Specifies that the BaseClass value should be ignored.
        /// </summary>
        IgnoreBaseClass = 0x200,
        /// <summary>
        /// Introduced in Windows 7. Specifies that the "Unknown" ProgID should be ignored; instead, fail.
        /// </summary>
        Init_IgnoreUnknown = 0x400,
        /// <summary>
        /// None of the following options are set.
        /// </summary>
        Init_Fixed_ProgId = 0x800,
        /// <summary>
        /// Introduced in Windows 8. 
        /// </summary>
        Is_Protocol = 0x1000,
        /// <summary>
        /// Introduced in Windows 8. 
        /// </summary>
        Init_For_File = 0x2000
    }
}
