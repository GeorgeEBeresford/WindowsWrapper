using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsWrapper.FileSystem
{
    public interface IFile
    {
        string GetAssociatedFile(string fileExtension);
    }
}
