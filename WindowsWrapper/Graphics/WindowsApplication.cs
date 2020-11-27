using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WindowsWrapper.Exceptions;
using WindowsWrapper.Interop.Enums;
using WindowsWrapper.Interop.Invokers;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace WindowsWrapper.Graphics
{
    /// <summary>
    ///     Represents an application's Window that is being displayed to the user
    /// </summary>
    public class WindowsApplication : VideoDisplay, IVideoDisplay
    {
        public WindowsApplication(IntPtr windowHandle)
        {
            Handle = windowHandle;
        }

        public IntPtr Handle { get; }

        public static WindowsApplication GetForeground()
        {
            IntPtr foregroundHandle = WindowInvoker.GetForegroundWindow();
            WindowsApplication application = new WindowsApplication(foregroundHandle);

            return application;
        }

        public Rectangle GetBounds()
        {
            int getAttributeResult = WindowInvoker.DwmGetWindowAttribute(Handle,
                DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS,
                out Interop.Structures.Rectangle applicationBounds, Marshal.SizeOf(typeof(Rectangle)));

            if (getAttributeResult != 0)
                throw new WindowAttributeNotFoundException($"Could not find application boundaries for handle {Handle}", Handle);

            Rectangle bounds = applicationBounds.AsDrawing();

            return bounds;
        }

        public Bitmap GetAsImage()
        {
            Rectangle applicationBounds = GetBounds();
            Bitmap screenshot = GetPortionAsImage(applicationBounds.X, applicationBounds.Y, applicationBounds.Width,
                applicationBounds.Height);
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