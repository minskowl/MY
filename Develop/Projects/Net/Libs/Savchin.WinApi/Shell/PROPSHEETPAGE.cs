using System;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.Shell
{

    internal delegate int PropSheetPageProc(IntPtr hWnd, x879758f4cab61ddd msg, ref PROPSHEETPAGE psp);



    /// <summary>
    /// PROPSHEETPAGE 
    /// xc2047d2fb5273fd5
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PROPSHEETPAGE
    {
        internal int dwSize;
        internal pspFlags dwFlags;
        internal IntPtr hInstance;
        internal IntPtr pResource;
        internal IntPtr hIcon;
        internal string pszTitle;
        internal DialogProc pfnDlgProc;
        internal IntPtr lParam;
        internal PropSheetPageProc pfnCallback;
        internal int x67a15b47123630c5;
        internal string x5b148009280afc91;
        internal string x651d5bc791942d0c;
    }
}