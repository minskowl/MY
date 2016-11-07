using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Savchin.WinApi.Shell
{
    /// <summary>
    /// Shel32.Dll wrapper
    /// </summary>
    public static class Shell32
    {
        /// <summary>
        ///Notifies the system of an event that an application has performed.
        ///  An application should use this function if it performs an action that may affect the Shell. 
        /// </summary>
        /// <param name="wEventId">The w event id.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <param name="dwItem1">The dw item1.</param>
        /// <param name="dwItem2">The dw item2.</param>
        [DllImport("Shell32.dll")]
        public static extern void SHChangeNotify(HChangeNotifyEventID wEvent, HChangeNotifyFlags uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int DragQueryFile(IntPtr hDrop, int iFile, [Out] StringBuilder lpszFile, int cch);


        [DllImport("ole32.dll")]
        internal static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);
 

 


        [DllImport("uxtheme.dll")]
        internal static extern int IsThemeActive();

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr OpenThemeData(IntPtr hwnd, string pszClassList);


        [DllImport("uxtheme.dll")]
        internal static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hDc, int iPartID, int iStateID, ref Rectangle pRect, ref Rectangle pClipRect);

        [DllImport("uxtheme.dll")]
        internal static extern IntPtr CloseThemeData(IntPtr hTheme);
 

 


 


 


    }
}
