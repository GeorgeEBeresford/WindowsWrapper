using System;
using System.Drawing;
using WindowsWrapper.Interop.Invokers;

namespace WindowsWrapper.Graphics
{
    /// <summary>
    ///     Represents a device which can display video output to the user
    /// </summary>
    public abstract class VideoDisplay
    {
        protected Bitmap GetScreenPortionAsImage(IntPtr displayContext, int positionX, int positionY, int width,
            int height)
        {
            IntPtr displayCompatibleContext = WindowInvoker.CreateCompatibleDC(displayContext);
            IntPtr displayCompatibleBitmap =
                WindowInvoker.CreateCompatibleBitmap(displayContext, width, height);

            // Select the new bitmap for future operations on our context
            IntPtr previousBitmap = WindowInvoker.SelectObject(displayCompatibleContext, displayCompatibleBitmap);

            // Copy the colour data from our desktop (and anything covering it) to our screenshot context
            bool isScreengrabSuccessful = WindowInvoker.BitBlt(displayCompatibleContext, 0, 0, width,
                height, displayContext, positionX, positionY,
                CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);

            Bitmap screenPortionImage = isScreengrabSuccessful ? Image.FromHbitmap(displayCompatibleBitmap) : null;

            // Restore our previous image selection
            WindowInvoker.SelectObject(displayCompatibleContext, previousBitmap);

            // Clean up our created objects so we're not hogging resources
            WindowInvoker.DeleteObject(displayCompatibleBitmap);
            WindowInvoker.DeleteDC(displayCompatibleContext);

            return screenPortionImage;
        }
    }
}