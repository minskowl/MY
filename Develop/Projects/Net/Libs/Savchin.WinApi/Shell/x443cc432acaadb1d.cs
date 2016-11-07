using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Win32;

namespace Savchin.WinApi.Shell
{





    class x443cc432acaadb1d
    {
        internal static int x15f59d42384ec1d4;

        static x443cc432acaadb1d()
        {
            //x6d7f0aaa9dfcfc2f = IntPtr.Zero;
            //xd8304bbfb14c4991 = new Guid("000214E4-0000-0000-C000-000000000046");
            //x1e6938f6e3534b48 = new Guid("000214E6-0000-0000-C000-000000000046");
            x15f59d42384ec1d4 = -2147483640;
            //x5de94d04bcf624c3 = -2147483647;
            //x1ed73d345128a09a = -2147483641;
            //xa6dd28728feae2d6 = -2147483638;
            //x993049542aecff0e = -2147024809;
            //x6a8ed03b37428b65 = 0x4df;
            //xe5a23f0a48eee233 = -2147287035;
            //if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor > 0)) || (Environment.OSVersion.Version.Major > 5)))
            //{
            //    x40f0356efe787a49 = true;
            //}
            //x6f0e0b4ccc41d2f0 = new IntPtr(-2147483648);
            //if (Marshal.SystemDefaultCharSize == 1)
            //{
            //    xfc6e00ec171e858b = 0;
            //    x0534a8caf4c48f88 = 1;
            //}
            //else
            //{
            //    xfc6e00ec171e858b = 4;
            //    x0534a8caf4c48f88 = 5;
            //}
        }
        internal static void ApplyChangesInShell()
        {
            Shell32.SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
        }
        [DllImport("shell32.dll")]
        internal static extern int SHGetPathFromIDList(IntPtr pidl, StringBuilder path);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern int GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport("User32", EntryPoint = "InsertMenuA", CharSet = CharSet.Ansi)]
        internal static extern long InsertMenu(IntPtr hMenu, int nPosition, x3d386b25758ba442 wFlags, int wIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetSubMenu(IntPtr hMenu, int nPos);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);
 
        [DllImport("user32.dll")]
        internal static extern int IsMenu(IntPtr hMenu);
 
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern int SetMenuItemBitmaps(IntPtr hMenu, uint uPosition, uint uFlags, IntPtr hBitmapUnchecked, IntPtr hBitmapChecked);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("comctl32.dll")]
        internal static extern IntPtr CreatePropertySheetPage(ref PROPSHEETPAGE psp);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetParent(IntPtr hWndChild);
        [DllImport("user32.dll")]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndParent);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("kernel32.dll")]
        internal static extern int MulDiv(int n1, int n2, int n3);


        [DllImport("uxtheme.dll")]
        internal static extern int IsThemeActive();

        [DllImport("uxtheme.dll")]
        internal static extern IntPtr CloseThemeData(IntPtr hTheme);





        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr OpenThemeData(IntPtr hwnd, string pszClassList);

        [DllImport("uxtheme.dll")]
        internal static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hDc, int iPartID, int iStateID, ref Rectangle pRect, ref Rectangle pClipRect);

          [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int DragQueryFile(IntPtr hDrop, int iFile, [Out] StringBuilder lpszFile, int cch);



    

        [DllImport("ole32.dll")]
        internal static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int GetMenuItemCount(IntPtr hMenu);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetMenuString(IntPtr hMenu, int idItem, IntPtr buff, int nMaxCount, int flag);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int DestroyMenu(IntPtr hMenu);

        [DllImport("User32")]
        internal static extern IntPtr CreatePopupMenu();
 

 






        internal static int xefc704ff04352756(IntPtr x4b902d71c53f7c0b)
        {
            return (short)((((long)x4b902d71c53f7c0b) >> 0x10) & 0xffffL);
        }
        internal static uint xefc704ff04352756(uint x4b902d71c53f7c0b)
        {
            return (x4b902d71c53f7c0b >> 0x10);
        }


        internal static string x12ebed3b7b748553(string xaf71327495ea8953, string xf9228eaf857193bc)
        {
            return Path.Combine(Path.GetDirectoryName(xaf71327495ea8953), xf9228eaf857193bc);
        }

        internal static void x43908261d1e1c129(Type x3201d6d15a947682)
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Shell Extensions\Approved");
            key.DeleteValue(x3201d6d15a947682.GUID.ToString("B"), false);
            key.Close();
        }
        internal static void xa346e04a75e6b993(Type x3201d6d15a947682)
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Shell Extensions\Approved");
            key.SetValue(x3201d6d15a947682.GUID.ToString("B"), x3201d6d15a947682.Name);
            key.Close();
        }


        internal static void x2994e7e7a2efe4d9(string xf6987a1745781d6f, IntPtr x2fc199ef4dd366cc, int x0794c2e13bd18666)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(xf6987a1745781d6f);
            int length = bytes.Length;
            if ((length + 2) > x0794c2e13bd18666)
            {
                length = x0794c2e13bd18666 - 2;
            }
            Marshal.Copy(bytes, 0, x2fc199ef4dd366cc, length);
            Marshal.WriteByte(x2fc199ef4dd366cc, length, 0);
            Marshal.WriteByte(x2fc199ef4dd366cc, length + 1, 0);
        }

        internal static void xdf8e4b332b4a57af(string xf6987a1745781d6f, IntPtr x2fc199ef4dd366cc, int x0794c2e13bd18666)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(xf6987a1745781d6f);
            int length = bytes.Length;
            if ((length + 1) > x0794c2e13bd18666)
            {
                length = x0794c2e13bd18666 - 1;
            }
            Marshal.Copy(bytes, 0, x2fc199ef4dd366cc, length);
            Marshal.WriteByte(x2fc199ef4dd366cc, length, 0);
        }

        /// <summary>
        /// X0810e12d53bb2dfbs the specified xd4b53aa4023f2377.
        /// </summary>
        /// <param name="xd4b53aa4023f2377">The xd4b53aa4023f2377.</param>
        /// <returns></returns>
        internal static string[] x0810e12d53bb2dfb(IOleDataObject xd4b53aa4023f2377)
        {
            return GetFileList(xd4b53aa4023f2377);
        }

        /// <summary>
        /// Gets the file list.
        /// </summary>
        /// <param name="xd4b53aa4023f2377">The xd4b53aa4023f2377.</param>
        /// <returns></returns>
        internal static string[] GetFileList(IOleDataObject xd4b53aa4023f2377)
        {
            string[] strArray = null;
            FORMATETC pformatetc = new FORMATETC();
            STGMEDIUM pmedium = new STGMEDIUM();
            pformatetc.cfFormat = 15;
            pformatetc.tymed = TYMED.TYMED_HGLOBAL;
            pformatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
            if (xd4b53aa4023f2377.GetData(ref pformatetc, ref pmedium) != 0)
            {
                return null;
            }
            int num = DragQueryFile(pmedium.unionmember, -1, null, 0);
            StringBuilder lpszFile = new StringBuilder(260);
            if (num > 0)
            {
                strArray = new string[num];
                for (int i = 0; i < num; i++)
                {
                    DragQueryFile(pmedium.unionmember, i, lpszFile, lpszFile.Capacity);
                    strArray[i] = lpszFile.ToString();
                }
            }
            ReleaseStgMedium(ref pmedium);
            return strArray;
        }

        //internal static int x2c48e34280fa641c(object x6ed4ed9ed59eb694, IntPtr xfc7ac5f41078c161, IntPtr x68624aa203540fdc, IntPtr x130fbcecf32fe781)
        //{
        //    return GetDialogUnitsH(x6ed4ed9ed59eb694);
        //}

        //internal static int GetDialogUnitsH(object x6ed4ed9ed59eb694)
        //{
        //    if (IntPtr.Size == 4)
        //    {
        //        LoadLibrary(x12ebed3b7b748553(x6ed4ed9ed59eb694.GetType().Assembly.Location, "LogicNP.PropSheetExtensionHelper.dll"));
        //        return GetDialogUnits();
        //    }
        //    LoadLibrary(x12ebed3b7b748553(x6ed4ed9ed59eb694.GetType().Assembly.Location, "LogicNP.PropSheetExtensionHelper_x64.dll"));
        //    return GetDialogUnits_x64();
        //}









        internal static IntPtr x1240f363affcf768(IntPtr x96e7d32425e52ebf, int x62d7c038e79af605, IntPtr xdb79253d30e51ebc)
        {
            if (IntPtr.Size == 4)
            {
                return (IntPtr)SetWindowLong(x96e7d32425e52ebf, x62d7c038e79af605, (int)xdb79253d30e51ebc);
            }
            return SetWindowLongPtr(x96e7d32425e52ebf, x62d7c038e79af605, xdb79253d30e51ebc);
        }

        //internal static int xed34dd38c3f5b36e(object x6ed4ed9ed59eb694, IntPtr xfc7ac5f41078c161, IntPtr x68624aa203540fdc, IntPtr x130fbcecf32fe781)
        //{
        //    if (IntPtr.Size == 4)
        //    {
        //        LoadLibrary(x12ebed3b7b748553(x6ed4ed9ed59eb694.GetType().Assembly.Location, "LogicNP.PropSheetExtensionHelper.dll"));
        //        CallCallback(xfc7ac5f41078c161, x68624aa203540fdc, x130fbcecf32fe781);
        //    }
        //    else
        //    {
        //        LoadLibrary(x12ebed3b7b748553(x6ed4ed9ed59eb694.GetType().Assembly.Location, "LogicNP.PropSheetExtensionHelper_x64.dll"));
        //        CallCallback_x64(xfc7ac5f41078c161, x68624aa203540fdc, x130fbcecf32fe781);
        //    }
        //    return 1;
        //}











    }
}
