using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsWrapper.Interop.Invokers;

namespace WindowsWrapper.Graphics
{
    /// <summary>
    ///     Represents a single screen
    /// </summary>
    public class WindowsScreen : VideoDisplay, IVideoDisplay
    {
        public WindowsScreen()
        {
            Handle = WindowInvoker.GetDesktopWindow();
        }

        public IntPtr Handle { get; }

        public Bitmap GetAsImage()
        {
            Size windowSize = Screen.PrimaryScreen.Bounds.Size;

            Bitmap screenshot = GetPortionAsImage(0, 0, windowSize);
            return screenshot;
        }

        public Bitmap GetPortionAsImage(Point position, Size size)
        {
            Bitmap image = GetPortionAsImage(position.X, position.Y, size.Width, size.Height);
            return image;
        }

        public Bitmap GetPortionAsImage(Point position, int width, int height)
        {
            Bitmap image = GetPortionAsImage(position.X, position.Y, width, height);
            return image;
        }

        public Bitmap GetPortionAsImage(int positionX, int positionY, Size size)
        {
            Bitmap image = GetPortionAsImage(positionX, positionY, size.Width, size.Height);
            return image;
        }

        public Bitmap GetPortionAsImage(int positionX, int positionY, int width, int height)
        {
            IntPtr desktopContext = WindowInvoker.GetWindowDC(Handle);

            Bitmap screenPortionImage = GetScreenPortionAsImage(desktopContext, positionX, positionY, width, height);

            WindowInvoker.ReleaseDC(Handle, desktopContext);

            return screenPortionImage;
        }

        public Color GetPixel(Point position)
        {
            Color pixel = GetPixel(position.X, position.Y);

            return pixel;
        }

        public Color GetPixel(int positionX, int positionY)
        {
            Bitmap pixelImage = GetPortionAsImage(positionX, positionY, 1, 1);

            Color pixel = pixelImage.GetPixel(0, 0);

            return pixel;
        }
    }
}