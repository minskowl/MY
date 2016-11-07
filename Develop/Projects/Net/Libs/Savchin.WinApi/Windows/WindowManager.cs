using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Savchin.WinApi.Windows
{
    /// <summary>
    /// Windwows System Environment
    /// </summary>
    public static class WindowManager
    {

        private static List<Form> _forms;
        private static readonly Object FormsLock = new object();
        private static int EnumerateForms(IntPtr hwnd, IntPtr lParam)
        {
            var theForm = Form.FromHandle(hwnd) as Form;
            if (theForm != null)
            {
                _forms.Add(theForm);
            }
            return 1;
        }


        /// <summary>
        /// Gets all form.
        /// </summary>
        /// <returns></returns>
        public static List<Form> GetAllForm()
        {
            lock (FormsLock)
            {
                _forms = new List<Form>();
                User32.EnumChildWindows(User32.GetDesktopWindow(), EnumerateForms, IntPtr.Zero);
                return _forms;
            }
        }

        private static readonly Object WindowsHandlerLock= new object();
        private static List<IntPtr> _windowsHandlers;

        private static int EnumWindowHandlers(IntPtr hwnd, IntPtr lParam)
        {
            _windowsHandlers.Add(hwnd);
            return 1;
        }

        /// <summary>
        /// Gets all window handlers.
        /// </summary>
        /// <returns></returns>
        public static List<IntPtr> GetAllWindowHandlers()
        {
            lock (WindowsHandlerLock)
            {
                _windowsHandlers = new List<IntPtr>();
                User32.EnumChildWindows(User32.GetDesktopWindow(), EnumWindowHandlers, IntPtr.Zero);
                return _windowsHandlers;
            }
        }

        private static readonly Object WindowsLock= new object();
        private static List<Window> _windows;

        private static int EnumWindows(IntPtr hwnd, IntPtr lParam)
        {
            _windows.Add(new Window(hwnd));
            return 1;
        }

        /// <summary>
        /// Gets all window handlers.
        /// </summary>
        /// <returns></returns>
        public static List<Window> GetAllWindows()
        {
            lock (WindowsLock)
            {
                _windows = new List<Window>();
                User32.EnumChildWindows(User32.GetDesktopWindow(), EnumWindows, IntPtr.Zero);
                return _windows;
            }
        }
    }
}