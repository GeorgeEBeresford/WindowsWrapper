using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WindowsWrapper.Interop.Enums;
using Rectangle = WindowsWrapper.Interop.Structures.Rectangle;

namespace WindowsWrapper.Interop.Invokers
{
    /// <summary>
    /// Contains functions relating to windows that can be P/Invoked
    /// </summary>
    internal static class WindowInvoker
    {
        /// <summary>
        /// The GetWindowDC function retrieves the device context (DC) for the entire window, including title bar, menus, and scroll bars.
        /// A window device context permits painting anywhere in a window, because the origin of the device context is the upper-left corner of the window instead of the client area
        /// GetWindowDC assigns default attributes to the window device context each time it retrieves the device context. Previous attributes are lost.
        /// </summary>
        /// <param name="window">
        /// A handle to the window with a device context that is to be retrieved. If this value is NULL, GetWindowDC retrieves the device context for the entire screen.
        /// If this parameter is NULL, GetWindowDC retrieves the device context for the primary display monitor.
        /// To get the device context for other display monitors, use the EnumDisplayMonitors and CreateDC functions.
        /// </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr window);

        /// <summary>
        /// Retrieves a handle to the desktop window. The desktop window covers the entire screen.
        /// The desktop window is the area on top of which other windows are painted.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications.
        /// The effect of the ReleaseDC function depends on the type of DC. It frees only common and window DCs. It has no effect on class or private DCs.
        /// </summary>
        /// <param name="window">A handle to the window whose DC is to be released.</param>
        /// <param name="deviceContext">A handle to the DC to be released.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr window, IntPtr deviceContext);

        /// <summary>
        /// The DeleteDC function deletes the specified device context (DC).
        /// </summary>
        /// <param name="deviceContext">A handle to the device context.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr deviceContext);

        /// <summary>
        /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object.
        /// After the object is deleted, the specified handle is no longer valid.
        /// </summary>
        /// <param name="objectToDelete">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteObject(IntPtr objectToDelete);

        /// <summary>
        /// The CreateCompatibleBitmap function creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="deviceContext">A handle to a device context.</param>
        /// <param name="width">The bitmap width, in pixels.</param>
        /// <param name="height">The bitmap height, in pixels.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr deviceContext, int width, int height);

        /// <summary>
        /// The CreateCompatibleDC function creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="deviceContext">
        /// A handle to an existing DC. If this handle is NULL, the function creates a memory DC compatible with the application's current screen.
        /// </param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr deviceContext);

        /// <summary>
        /// Selects an object into the specified device context (DC). The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="deviceContext">A handle to the DC.</param>
        /// <param name="selectedObject">A handle to the object to be selected.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr deviceContext, IntPtr selectedObject);

        /// <summary>
        /// The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels from the specified source device context into a destination device context.
        /// </summary>
        /// <param name="destinationDeviceContext">
        /// A handle to the destination device context.
        /// </param>
        /// <param name="destinationXAxis">
        /// The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.
        /// </param>
        /// <param name="destinationYAxis">
        /// The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.
        /// </param>
        /// <param name="destinationWidth">
        /// The width, in logical units, of the source and destination rectangles.
        /// </param>
        /// <param name="destinationHeight">
        /// The height, in logical units, of the source and the destination rectangles.
        /// </param>
        /// <param name="sourceDeviceContext">
        /// A handle to the source device context.
        /// </param>
        /// <param name="sourceXAxis">
        /// The x-coordinate, in logical units, of the upper-left corner of the source rectangle.
        /// </param>
        /// <param name="sourceYAxis">
        /// The y-coordinate, in logical units, of the upper-left corner of the source rectangle.
        /// </param>
        /// <param name="rasterOperationCode">
        /// A raster-operation code. These codes define how the color data for the source rectangle is to be combined with the color data for the destination rectangle to achieve the final color.
        /// </param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr destinationDeviceContext, int destinationXAxis, int destinationYAxis,
            int destinationWidth, int destinationHeight, IntPtr sourceDeviceContext, int sourceXAxis, int sourceYAxis,
            CopyPixelOperation rasterOperationCode);

        /// <summary>
        /// Creates a device context (DC) for a device.
        /// </summary>
        /// <param name="driverName">
        /// Long pointer to a null-terminated string that specifies the file name of a driver.
        /// If this parameter is set to NULL, the system returns a screen DC.
        /// </param>
        /// <param name="deviceName">
        /// Long pointer to a null-terminated string that specifies the name of the specific output device being used, as shown by the Print Manager.
        /// It is not the printer model name. The lpszDevice parameter must be used.
        /// In Windows CE 2.0 and later, this parameter is ignored.
        /// </param>
        /// <param name="outputDestination">
        /// Long pointer to an output destination.
        /// </param>
        /// <param name="deviceInitialisationData">
        /// Long pointer to a DEVMODE structure containing device-specific initialization data for the device driver.
        /// The parameter must be NULL if the device driver is to use the default initialization (if any) specified by the user.
        /// </param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string driverName, string deviceName, string outputDestination,
            IntPtr deviceInitialisationData);

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working). The system
        /// assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Retrieves the current value of a specified Desktop Window Manager (DWM) attribute applied to a window.
        /// </summary>
        /// <param name="windowHandle">
        /// The handle to the window from which the attribute value is to be retrieved.
        /// </param>
        /// <param name="dwAttribute">
        /// A flag describing which value to retrieve, specified as a value of the DWMWINDOWATTRIBUTE enumeration.
        /// This parameter specifies which attribute to retrieve, and the pvAttribute parameter points to an object into which the attribute value is retrieved.
        /// </param>
        /// <param name="lpRect">
        /// A pointer to a value which, when this function returns successfully, receives the current value of the attribute.
        /// The type of the retrieved value depends on the value of the dwAttribute parameter.
        /// The DWMWINDOWATTRIBUTE enumeration topic indicates, in the row for each flag, what type of value you should pass a pointer to in the pvAttribute parameter.
        /// </param>
        /// <param name="cbAttribute">
        /// The size, in bytes, of the attribute value being received via the pvAttribute parameter.
        /// The type of the retrieved value, and therefore its size in bytes, depends on the value of the dwAttribute parameter.
        /// </param>
        /// <returns></returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr windowHandle, DwmWindowAttribute dwAttribute, out Rectangle lpRect, int cbAttribute);
    }
}
