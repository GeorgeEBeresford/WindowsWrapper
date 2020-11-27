namespace WindowsWrapper.Interop.Structures
{
    internal struct Rectangle
    {
        public int Left; // x position of upper-left corner
        public int Top; // y position of upper-left corner
        public int Right; // x position of lower-right corner
        public int Bottom; // y position of lower-right corner

        public System.Drawing.Rectangle AsDrawing()
        {
            return new System.Drawing.Rectangle(Left, Top, Right, Bottom);
        }
    }
}