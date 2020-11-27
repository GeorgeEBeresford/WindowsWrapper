using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsWrapper.Exceptions
{
    public class WindowAttributeNotFoundException : Exception
    {
        public WindowAttributeNotFoundException(string message, IntPtr handle) : base(message)
        {
            Handle = handle;
        }

        public IntPtr Handle { get; }

        public new string ToString()
        {
            return $"WindowAttributeNotFoundException: {Message} at{Environment.NewLine}{StackTrace}";
        }
    }
}
