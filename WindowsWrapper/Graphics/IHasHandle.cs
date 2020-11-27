using System;

namespace WindowsWrapper.Graphics
{
    public interface IHasHandle
    {
        IntPtr Handle { get; }
    }
}