using System;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.Shell
{
    /// <summary>
    /// Comctl32
    /// </summary>
    public static class Comctl32
    {
        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern IntPtr CreatePropertySheetPage(ref PROPSHEETPAGE psp);

        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern int DestroyPropertySheetPage(IntPtr hPage);
    }
}
