using System.Drawing;
using WindowsWrapper.Interop.Structures;
using Point = System.Drawing.Point;

namespace WindowsWrapper.Graphics
{
    /// <summary>
    ///     Represents a device which can display video output to the user
    /// </summary>
    public interface IVideoDisplay : IHasHandle
    {
        /// <summary>
        ///     Retrieves the desktop window as an image
        /// </summary>
        /// <returns></returns>
        Bitmap GetAsImage();

        /// <summary>
        ///     Retrieves a portion of the window as an image
        /// </summary>
        /// <param name="position">The top-left position of the portion which will be retrieved</param>
        /// <param name="size">The size of the portion that will be retrieved</param>
        /// <returns></returns>
        Bitmap GetPortionAsImage(Point position, Size size);

        /// <summary>
        ///     Retrieves a portion of the window as an image
        /// </summary>
        /// <param name="position">The top-left position of the portion which will be retrieved</param>
        /// <param name="width">The size of the portion that will be retrieved</param>
        /// <param name="height">The height of the retrieved portion of the screen</param>
        /// <returns></returns>
        Bitmap GetPortionAsImage(Point position, int width, int height);

        /// <summary>
        ///     Retrieves a portion of the window as an image
        /// </summary>
        /// <param name="positionX">The x position of the portion which will be retrieved</param>
        /// <param name="positionY">The y position of the portion which will be retrieved</param>
        /// <param name="size">The size of the portion that will be retrieved</param>
        /// <returns></returns>
        Bitmap GetPortionAsImage(int positionX, int positionY, Size size);

        /// <summary>
        ///     Retrieves a portion of the window as an image
        /// </summary>
        /// <param name="positionX">The x position of the portion which will be retrieved</param>
        /// <param name="positionY">The y position of the portion which will be retrieved</param>
        /// <param name="width">The size of the portion that will be retrieved</param>
        /// <param name="height">The height of the retrieved portion of the screen</param>
        /// <returns></returns>
        Bitmap GetPortionAsImage(int positionX, int positionY, int width, int height);

        /// <summary>
        ///     Retrieves a single pixel at a position as an image
        /// </summary>
        /// <param name="position">The position of the pixel which will be retrieved</param>
        /// <returns></returns>
        Color GetPixel(Point position);

        /// <summary>
        ///     Retrieves a single pixel at a position as an image
        /// </summary>
        /// <param name="positionX">The X position of the pixel which will be retrieved</param>
        /// <param name="positionY">The Y position of the pixel which will be retrieved</param>
        /// <returns></returns>
        Color GetPixel(int positionX, int positionY);
    }
}