using System.Runtime.InteropServices;
using WindowsWrapper.Interop.Structures;

namespace WindowsWrapper.Interop.Invokers
{
    /// <summary>
    /// Contains functions relating to the cursoe that can be P/Invoked
    /// </summary>
    internal static class CursorInvoker
    {
        /// <summary>
        /// Retrie
        /// </summary>
        /// <param name="cursorPosition"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Point cursorPosition);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// Calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="windowHandle">
        /// A handle to the window whose window procedure will receive the message.
        /// If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system,
        /// including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not
        /// sent to child windows.
        /// Message sending is subject to UIPI.
        /// The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.
        /// </param>
        /// <param name="message">
        /// The message to be sent.
        /// </param>
        /// <param name="messageParameters">
        /// Additional message-specific information.
        /// </param>
        /// <param name="messageParameters2">
        /// Additional message-specific information.
        /// </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(int windowHandle, uint message, int messageParameters, int messageParameters2);
    }
}
