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
        /// <summary>
        /// Retrieves a portion of the screen as an image.
        /// From nobugz at <see cref="!:https://social.msdn.microsoft.com/Forums/en-US/474450b9-e260-4369-9efb-0d57a5b2e06d/copyfromscreen-no-alpha-window-captured"/>
        /// </summary>
        /// <param name="displayContext">A handle to the display context which we're retrieving the image from</param>
        /// <param name="positionX">The X co-ordinate of the top left corner of the portion we're retrieving</param>
        /// <param name="positionY">The Y co-ordinate of the top left corner of the portion we're retrieving</param>
        /// <param name="width">The width of the image which will be retrieves</param>
        /// <param name="height">The height of the image which will be retrieved</param>
        /// <returns></returns>
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