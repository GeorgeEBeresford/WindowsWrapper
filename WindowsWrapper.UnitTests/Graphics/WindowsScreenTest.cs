using System.Drawing;
using System.Windows.Forms;
using WindowsWrapper.Graphics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowsWrapper.UnitTests.Graphics
{
    [TestClass]
    public class WindowsScreenTest
    {
        private readonly WindowsScreen WindowsScreen = new WindowsScreen();

        [TestMethod]
        public void GetPixelAsImage()
        {
            Color pixel = WindowsScreen.GetPixel(0, 0);
            Assert.IsNotNull(pixel);

            pixel = WindowsScreen.GetPixel(new Point(0, 0));
            Assert.IsNotNull(pixel);
        }

        [TestMethod]
        public void GetPortionAsImage()
        {
            Bitmap portion = WindowsScreen.GetPortionAsImage(0, 0, 2, 2);
            AssertIsPortion(portion);

            portion = WindowsScreen.GetPortionAsImage(0, 0, new Size(2, 2));
            AssertIsPortion(portion);

            portion = WindowsScreen.GetPortionAsImage(new Point(0, 0), 2, 2);
            AssertIsPortion(portion);

            portion = WindowsScreen.GetPortionAsImage(new Point(0, 0), new Size(2, 2));
            AssertIsPortion(portion);
        }

        [TestMethod]
        public void GetAsImage()
        {
            Bitmap screenshot = WindowsScreen.GetAsImage();
            AssertIsScreenshot(screenshot);
        }

        private static void AssertIsScreenshot(Bitmap screenshot)
        {
            Size windowSize = Screen.PrimaryScreen.Bounds.Size;

            Assert.IsNotNull(screenshot);
            Assert.AreEqual(windowSize.Width, screenshot.Size.Width,
                $"screenshot width is not equal to {windowSize.Width}");
            Assert.AreEqual(windowSize.Height, screenshot.Size.Height,
                $"screenshot height not equal to {windowSize.Height}");
        }

        private static void AssertIsPortion(Bitmap portion)
        {
            Assert.IsNotNull(portion);
            Assert.AreEqual(2, portion.Size.Width, "portion width is not equal to 2");
            Assert.AreEqual(2, portion.Size.Height, "portion height not equal to 2");
        }
    }
}