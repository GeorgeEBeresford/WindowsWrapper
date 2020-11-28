using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsWrapper.FileSystem.Processes
{
    public class AppProcess : IProcess
    {
        public AppProcess(string appId, string arguments)
        {
            Name = appId;
            CommandPath = $@"shell:AppsFolder\{appId} {arguments}";
        }

        public string Name { get; }
        public string CommandPath { get; }
    }
}
