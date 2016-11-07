using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Savchin.WinApi.Enums;

namespace Savchin.WinApi
{
    public enum CaptureMode
    {
        Screen,
        Window
    }

    public static class ScreenCapturer
    {






        /// <summary> Capture Active Window, Desktop, Window or Control by hWnd or .NET Contro/Form and save it to a specified file.  </summary>
        /// <param name="filename">Filename.
        /// <para>* If extension is omitted, it's calculated from the type of file</para>
        /// <para>* If path is omitted, defaults to %TEMP%</para>
        /// <para>* Use %NOW% to put a timestamp in the filename</para></param>
        /// <param name="mode">Optional. The default value is CaptureMode.Window.</param>
        /// <param name="format">Optional file save mode.  Default is PNG</param>
        public static void CaptureAndSave(string filename, CaptureMode mode = CaptureMode.Window,
            ImageFormat format = null)
        {
            ImageSave(filename, format, Capture(mode));
        }

        /// <summary> Capture a specific window (or control) and save it to a specified file.  </summary>
        /// <param name="filename">Filename.
        /// <para>* If extension is omitted, it's calculated from the type of file</para>
        /// <para>* If path is omitted, defaults to %TEMP%</para>
        /// <para>* Use %NOW% to put a timestamp in the filename</para></param>
        /// <param name="handle">hWnd (handle) of the window to capture</param>
        /// <param name="format">Optional file save mode.  Default is PNG</param>
        public static void CaptureAndSave(string filename, IntPtr handle, ImageFormat format = null)
        {
            ImageSave(filename, format, Capture(handle));
        }

        /// <summary> Capture a specific window (or control) and save it to a specified file.  </summary>
        /// <param name="filename">Filename.
        /// <para>* If extension is omitted, it's calculated from the type of file</para>
        /// <para>* If path is omitted, defaults to %TEMP%</para>
        /// <para>* Use %NOW% to put a timestamp in the filename</para></param>
        /// <param name="c">Object to capture</param>
        /// <param name="format">Optional file save mode.  Default is PNG</param>
        public static void CaptureAndSave(string filename, Control c, ImageFormat format = null)
        {
            ImageSave(filename, format, Capture(c));
        }

        /// <summary> Capture the active window (default) or the desktop and return it as a bitmap </summary>
        /// <param name="mode">Optional. The default value is CaptureMode.Window.</param>
        public static Bitmap Capture(CaptureMode mode = CaptureMode.Window)
        {
            return Capture(mode == CaptureMode.Screen ? User32.GetDesktopWindow() : User32.GetForegroundWindow());
        }

        /// <summary> Capture a .NET Control, Form, UserControl, etc. </summary>
        /// <param name="c">Object to capture</param>
        /// <returns> Bitmap of control's area </returns>
        public static Bitmap Capture(Control c)
        {
            return Capture(c.Handle);
        }
        public static Bitmap MakeSnapshot(IntPtr AppWndHandle, bool IsClientWnd)
        {
            if (AppWndHandle == IntPtr.Zero || !User32.IsWindow(AppWndHandle) ||
                        !User32.IsWindowVisible(AppWndHandle))
                return null;
            if (User32.IsIconic(AppWndHandle))
                User32.ShowWindow(AppWndHandle, WindowShowStyle.Restore);//show it
            if (!User32.SetForegroundWindow(AppWndHandle))
                return null;//can't bring it to front
            System.Threading.Thread.Sleep(1000);//give it some time to redraw
            RECT appRect;
            bool res = IsClientWnd ? User32.GetClientRect(AppWndHandle, out appRect) : User32.GetWindowRect(AppWndHandle, out appRect);
            if (!res || appRect.Height == 0 || appRect.Width == 0)
            {
                return null;//some hidden window
            }
            // calculate the app rectangle
            if (IsClientWnd)
            {
                Point lt = new Point(appRect.Left, appRect.Top);
                Point rb = new Point(appRect.Right, appRect.Bottom);
                User32.ClientToScreen(AppWndHandle, ref lt);
                User32.ClientToScreen(AppWndHandle, ref rb);
                appRect.Left = lt.X;
                appRect.Top = lt.Y;
                appRect.Right = rb.X;
                appRect.Bottom = rb.Y;
            }
            //Intersect with the Desktop rectangle and get what's visible
            IntPtr DesktopHandle = User32.GetDesktopWindow();
            RECT desktopRect;
            User32.GetWindowRect(DesktopHandle, out desktopRect);
            RECT visibleRect;
            if (!User32.IntersectRect
                (out visibleRect, ref desktopRect, ref appRect))
            {
                visibleRect = appRect;
            }
            if (User32.IsRectEmpty(ref visibleRect))
                return null;

            int Width = visibleRect.Width;
            int Height = visibleRect.Height;
            IntPtr hdcTo = IntPtr.Zero;
            IntPtr hdcFrom = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;
            try
            {
                Bitmap clsRet = null;

                // get device context of the window...
                hdcFrom = IsClientWnd ? User32.GetDC(AppWndHandle) :
                        User32.GetWindowDC(AppWndHandle);

                // create dc that we can draw to...
                hdcTo = GDI32.CreateCompatibleDC(hdcFrom);
                hBitmap = GDI32.CreateCompatibleBitmap(hdcFrom, Width, Height);

                //  validate
                if (hBitmap != IntPtr.Zero)
                {
                    // adjust and copy
                    int x = appRect.Left < 0 ? -appRect.Left : 0;
                    int y = appRect.Top < 0 ? -appRect.Top : 0;
                    IntPtr hLocalBitmap = GDI32.SelectObject(hdcTo, hBitmap);
                    GDI32.BitBlt(hdcTo, 0, 0, Width, Height,
                        hdcFrom, x, y, GDI32.SRCCOPY);
                    GDI32.SelectObject(hdcTo, hLocalBitmap);
                    //  create bitmap for window image...
                    clsRet = System.Drawing.Image.FromHbitmap(hBitmap);
                }
                return clsRet;
            }
            finally
            {
                //  release the unmanaged resources
                if (hdcFrom != IntPtr.Zero)
                    User32.ReleaseDC(AppWndHandle, hdcFrom);
                if (hdcTo != IntPtr.Zero)
                    GDI32.DeleteDC(hdcTo);
                if (hBitmap != IntPtr.Zero)
                    GDI32.DeleteObject(hBitmap);
            }
        }

        /// <summary> Capture a specific window and return it as a bitmap </summary>
        /// <param name="handle">hWnd (handle) of the window to capture</param>
        public static Bitmap Capture(IntPtr handle)
        {
            Rectangle bounds;
            RECT rect ;
            User32.GetWindowRect(handle, out rect);

            bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            if (bounds.IsEmpty) return null;

            CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);

            var result = new Bitmap(bounds.Width, bounds.Height);
            using (var g = Graphics.FromImage(result))
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

            return result;
        }
        public static Bitmap DrawToBitmap(IntPtr handle, Rectangle targetBounds)
        {

            if (targetBounds.Width <= 0 || targetBounds.Height <= 0 || (targetBounds.X < 0 || targetBounds.Y < 0))
                throw new ArgumentException("targetBounds");

            var bitmap = new Bitmap(targetBounds.Width, targetBounds.Height);
            //if (!this.IsHandleCreated)
            //    this.CreateHandle();
            int num1 = targetBounds.Width;
            int num2 = targetBounds.Height;
            using (Graphics graphics1 = Graphics.FromImage((Image)new Bitmap(num1, num2, bitmap.PixelFormat)))
            {
                IntPtr hdc1 = graphics1.GetHdc();
                User32.SendMessage(handle, 791, hdc1, (IntPtr)30);
                using (Graphics graphics2 = Graphics.FromImage((Image)bitmap))
                {
                    IntPtr hdc2 = graphics2.GetHdc();
                    GDI32.BitBlt(hdc2, targetBounds.X, targetBounds.Y, num1, num2, hdc1, 0, 0, 13369376);
                    graphics2.ReleaseHdcInternal(hdc2);
                }
                graphics1.ReleaseHdcInternal(hdc1);
            }
            return bitmap;
        }
        /// <summary> Position of the cursor relative to the start of the capture </summary>
        public static Point CursorPosition;


        /// <summary> Save an image to a specific file </summary>
        /// <param name="filename">Filename.
        /// <para>* If extension is omitted, it's calculated from the type of file</para>
        /// <para>* If path is omitted, defaults to %TEMP%</para>
        /// <para>* Use %NOW% to put a timestamp in the filename</para></param>
        /// <param name="format">Optional file save mode.  Default is PNG</param>
        /// <param name="image">Image to save.  Usually a BitMap, but can be any
        /// Image.</param>
        private static void ImageSave(string filename, ImageFormat format, Image image)
        {
            format = format ?? ImageFormat.Png;
            if (!filename.Contains("."))
                filename = filename.Trim() + "." + format.ToString().ToLower();

            if (!filename.Contains(@"\"))
                filename = Path.Combine(Environment.GetEnvironmentVariable("TEMP") ?? @"C:\Temp", filename);

            filename = filename.Replace("%NOW%", DateTime.Now.ToString("yyyy-MM-dd@hh.mm.ss"));
            image.Save(filename, format);
        }
    }
}