using System.Drawing;
using System.Threading;
using WindowsWrapper.Interop.Invokers;

namespace WindowsWrapper.Input
{
    /// <summary>
    ///     Represents the user's cursor and allows it to be queried through Windows
    /// </summary>
    public class WindowsCursor : ICursor
    {
        private const uint LeftButtonDown = 0x0201;
        private const uint LeftButtonUp = 0x0202;

        public Point GetPosition()
        {
            CursorInvoker.GetCursorPos(out Interop.Structures.Point cursorPosition);

            return cursorPosition.AsDrawing();
        }

        public bool LeftClick(int windowHandle, Point position)
        {
            bool isSuccess = LeftClick(windowHandle, position.X, position.Y);
            return isSuccess;
        }

        public bool LeftClick(int windowHandle, int positionX, int positionY)
        {
            int heightOffset = (positionY << 16) + positionX;

            if (!SendMessage(windowHandle, LeftButtonDown, 0, heightOffset)) return false;

            Thread.Sleep(27);

            return SendMessage(windowHandle, LeftButtonUp, 0, heightOffset);
        }

        private bool SendMessage(int windowHandle, uint message, int messageParameter1, int messageParameter2)
        {
            int messageResult =
                CursorInvoker.SendMessage(windowHandle, LeftButtonDown, messageParameter1, messageParameter2);

            bool isSuccess = messageResult == 0;

            return isSuccess;
        }
    }
}