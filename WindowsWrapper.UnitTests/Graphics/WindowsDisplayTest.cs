using System.Drawing;
using System.Windows.Forms;
using WindowsWrapper.Graphics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowsWrapper.UnitTests.Graphics
{
    [TestClass]
    public class WindowsDisplayTest
    {
        [TestMethod]
        public void GetAsImage()
        {
            Bitmap screenshot = WindowsDisplay.GetAsImage();
            AssertIsScreenshot(screenshot);
        }


        [TestMethod]
        public void GetPixelAsImage()
        {
            Color pixel = WindowsDisplay.GetPixel(0, 0);
            Assert.IsNotNull(pixel);

            pixel = WindowsDisplay.GetPixel(new Point(0, 0));
            Assert.IsNotNull(pixel);
        }

        [TestMethod]
        public void GetPortionAsImage()
        {
            Bitmap portion = WindowsDisplay.GetPortionAsImage(0, 0, 200, 200);
            AssertIsPortion(portion);

            portion = WindowsDisplay.GetPortionAsImage(0, 0, new Size(200, 200));
            AssertIsPortion(portion);

            portion = WindowsDisplay.GetPortionAsImage(new Point(0, 0), 200, 200);
            AssertIsPortion(portion);

            portion = WindowsDisplay.GetPortionAsImage(new Point(0, 0), new Size(200, 200));
            AssertIsPortion(portion);
        }

        private static void AssertIsScreenshot(Bitmap screenshot)
        {
            Size windowSize = SystemInformation.VirtualScreen.Size;

            Assert.IsNotNull(screenshot);
            Assert.AreEqual(windowSize.Width, screenshot.Size.Width, $"screenshot width is not equal to {windowSize.Width}");
            Assert.AreEqual(windowSize.Height, screenshot.Size.Height, $"screenshot height not equal to {windowSize.Height}");
        }

        private static void AssertIsPortion(Bitmap portion)
        {
            Assert.IsNotNull(portion);
            Assert.AreEqual(200, portion.Size.Width, "portion width is not equal to 200");
            Assert.AreEqual(200, portion.Size.Height, "portion height not equal to 200");
        }

        private readonly WindowsDisplay WindowsDisplay = new WindowsDisplay();
    }
}
