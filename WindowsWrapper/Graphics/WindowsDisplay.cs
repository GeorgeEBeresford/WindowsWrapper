using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsWrapper.Interop.Invokers;

namespace WindowsWrapper.Graphics
{
    /// <summary>
    ///     Represents a display containing all of the user's screens
    /// </summary>
    public class WindowsDisplay : VideoDisplay, IVideoDisplay
    {
        public IntPtr Handle => IntPtr.Zero;

        public Bitmap GetAsImage()
        {
            Rectangle bounds = GetAllScreensBounds();
            Bitmap screenshot = GetPortionAsImage(bounds.X, bounds.Y, bounds.Width, bounds.Height);

            return screenshot;
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

        public Bitmap GetPortionAsImage(Point position, Size size)
        {
            Bitmap screenPortionImage = GetPortionAsImage(position.X, position.Y, size.Width, size.Height);
            return screenPortionImage;
        }

        public Bitmap GetPortionAsImage(Point position, int width, int height)
        {
            Bitmap screenPortionImage = GetPortionAsImage(position.X, position.Y, width, height);
            return screenPortionImage;
        }

        public Bitmap GetPortionAsImage(int positionX, int positionY, Size size)
        {
            Bitmap screenPortionImage = GetPortionAsImage(positionX, positionY, size.Width, size.Height);
            return screenPortionImage;
        }

        public Bitmap GetPortionAsImage(int positionX, int positionY, int width, int height)
        {
            IntPtr desktopContext = WindowInvoker.CreateDC("DISPLAY", null, null, IntPtr.Zero);

            Bitmap screenPortionImage = GetScreenPortionAsImage(desktopContext, positionX, positionY, width, height);

            WindowInvoker.DeleteDC(desktopContext);

            return screenPortionImage;
        }

        private Rectangle GetAllScreensBounds()
        {
            ICollection<Screen> allScreens = Screen.AllScreens;

            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            int x = 0;
            int y = 0;

            foreach (Screen screen in allScreens)
            {
                if (screen.Bounds.X != 0) width += screen.Bounds.Width;

                if (screen.Bounds.X < 0) x += screen.Bounds.X;

                if (screen.Bounds.Y != 0) height += screen.Bounds.Height;

                if (screen.Bounds.Y < 0) y += screen.Bounds.Height;
            }

            Rectangle bounds = new Rectangle(x, y, width, height);

            return bounds;
        }
    }
}