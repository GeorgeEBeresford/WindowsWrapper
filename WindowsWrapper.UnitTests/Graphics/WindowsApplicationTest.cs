using System;
using System.Drawing;
using WindowsWrapper.Graphics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowsWrapper.UnitTests.Graphics
{
    [TestClass]
    public class WindowsApplicationTest
    {
        private readonly WindowsApplication WindowsApplication = WindowsApplication.GetForeground();

        [TestMethod]
        public void WindowHandle()
        {
            IntPtr windowHandle = WindowsApplication.Handle;
            Assert.AreNotEqual(IntPtr.Zero, windowHandle);
        }

        [TestMethod]
        public void GetPixelAsImage()
        {
            Color pixel = WindowsApplication.GetPixel(0, 0);
            Assert.IsNotNull(pixel);

            pixel = WindowsApplication.GetPixel(new Point(0, 0));
            Assert.IsNotNull(pixel);
        }

        [TestMethod]
        public void GetPortionAsImage()
        {
            Bitmap portion = WindowsApplication.GetPortionAsImage(0, 0, 2, 2);
            AssertIsPortion(portion);

            portion = WindowsApplication.GetPortionAsImage(0, 0, new Size(2, 2));
            AssertIsPortion(portion);

            portion = WindowsApplication.GetPortionAsImage(new Point(0, 0), 2, 2);
            AssertIsPortion(portion);

            portion = WindowsApplication.GetPortionAsImage(new Point(0, 0), new Size(2, 2));
            AssertIsPortion(portion);
        }

        [TestMethod]
        public void GetAsImage()
        {
            Bitmap screenshot = WindowsApplication.GetAsImage();

            screenshot.Save(@"E:\test.bmp");

            AssertIsScreenshot(screenshot);
        }

        private void AssertIsScreenshot(Bitmap screenshot)
        {
            Size windowSize = WindowsApplication.GetBounds().Size;

            Assert.IsNotNull(screenshot);
            Assert.AreEqual(windowSize.Width, screenshot.Size.Width,
                $"screenshot width is not equal to {windowSize.Width}");
            Assert.AreEqual(windowSize.Height, screenshot.Size.Height,
                $"screenshot height not equal to {windowSize.Height}");
        }

        private void AssertIsPortion(Bitmap portion)
        {
            Assert.IsNotNull(portion);
            Assert.AreEqual(2, portion.Size.Width, "portion width is not equal to 2");
            Assert.AreEqual(2, portion.Size.Height, "portion height not equal to 2");
        }
    }
}