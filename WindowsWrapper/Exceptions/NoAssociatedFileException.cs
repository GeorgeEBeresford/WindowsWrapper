using System;

namespace WindowsWrapper.Exceptions
{
    /// <summary>
    /// The file association belonging to an extension could not be found
    /// </summary>
    public class NoAssociationFoundException : Exception
    {
        public NoAssociationFoundException(string message, string fileExtension) : base(message)
        {
            FileExtension = fileExtension;
        }

        public string FileExtension { get; }

        public new string ToString()
        {
            return $"NoAssociatedFileException: {Message} at{Environment.NewLine}{StackTrace}";
        }
    }
}
