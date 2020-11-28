using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WindowsWrapper.FileSystem.Processes
{
    public class ExecutedProcess : IProcess
    {
        public ExecutedProcess(string executablePath, string arguments, Process process)
        {
            CommandPath = $"{executablePath} {arguments}";
            Handle = process;
            Name = executablePath;
        }

        public string CommandPath { get; }
        public Process Handle { get; }
        public string Name { get; }
    }
}
