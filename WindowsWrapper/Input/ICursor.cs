using System.Drawing;

namespace WindowsWrapper.Input
{
    /// <summary>
    ///     Represents the user's cursor and allows it to be queried through Windows
    /// </summary>
    public interface ICursor
    {
        bool LeftClick(int windowHandle, Point position);

        bool LeftClick(int windowHandle, int positionX, int positionY);

        Point GetPosition();
    }
}