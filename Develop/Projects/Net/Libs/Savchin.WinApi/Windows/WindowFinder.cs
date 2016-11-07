using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.WinApi.Windows
{
    public delegate bool WindowMatcher(Window window);

    /// <summary>
    /// WindowFinder
    /// </summary>
    public class WindowFinder
    {
        private string name;
        /// <summary>
        /// Finds the dialog.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<Window> FindDialog(string name)
        {
            this.name = name;
            return FindWindow(DialogNameMatcher);
        }

        /// <summary>
        /// Finds the first dialog.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Window FindFirstDialog(string name)
        {
            this.name = name;
            return FindFirstWindow(DialogNameMatcher);
        }
        private bool DialogNameMatcher(Window window)
        {
            if (!window.IsDialog)
                return false;

            if (name != null && name != window.Name)
                return false;

            return true;
        }

        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public List<Window> FindWindow(WindowMatcher match)
        {
            var result = new List<Window>();
            User32.EnumChildWindows(User32.GetDesktopWindow(), delegate(IntPtr hwnd, IntPtr lParam)
                                                                   {
                                                                       var info = new Window(hwnd);
                                                                       if (match(info))
                                                                           result.Add(info);
                                                                       return 1;
                                                                   }, IntPtr.Zero);
            return result;
        }

        /// <summary>
        /// Finds the first window.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public Window FindFirstWindow(WindowMatcher match)
        {
            Window result = null;
            User32.EnumChildWindows(User32.GetDesktopWindow(), delegate(IntPtr hwnd, IntPtr lParam)
                                                                   {
                                                                       var info = new Window(hwnd);
                                                                       if (result != null && match(info))
                                                                       {
                                                                           result = info;
                                                                       }
                                                                       return 1;
                                                                   }, IntPtr.Zero);
            return result;
        }
    }
}