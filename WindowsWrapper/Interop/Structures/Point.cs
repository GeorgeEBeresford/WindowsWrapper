using System.Runtime.InteropServices;

namespace WindowsWrapper.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public System.Drawing.Point AsDrawing()
        {
            return new System.Drawing.Point(X, Y);
        }
    }
}