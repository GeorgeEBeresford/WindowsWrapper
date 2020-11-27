using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsWrapper.FileSystem
{
    public interface IFile
    {
        string FindExecutable();

        string GetAssociatedExecutable();

        string GetAssociatedApp();

        bool TryFindExecutable(out string path, out string handleType);
    }
}
