using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsWrapper.FileSystem.Processes
{
    public interface IProcess
    {
        string Name { get; }
        string CommandPath { get; }
    }
}
