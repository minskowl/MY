using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Savchin.WinApi;
using Savchin.Wpf.Input;

namespace Savchin.WPF.SystemTools
{
    class MainWindowModel
    {
        public Process SelectedItem { get; set; }
        public Process[] Processes { get; set; }
        public Screen[] Screens { get; set; }
        public DelegateCommand<Process> SetBorderCommand { get; set; }
        public DelegateCommand<Screen> MoveToCommand { get; set; }
        public MainWindowModel()
        {
            Screens = Screen.AllScreens;
            Processes = Process.GetProcesses().Where(e => e.MainWindowHandle != IntPtr.Zero).OrderBy(e => e.ProcessName).ToArray();
            SetBorderCommand = new DelegateCommand<Process>(OnSetBorderCommand);
            MoveToCommand = new DelegateCommand<Screen>(OnMoveToCommand);
        }

        private void OnMoveToCommand(Screen obj)
        {
            if(SelectedItem==null)return;
            var b = obj.Bounds;
            User32.SetWindowPos(SelectedItem.MainWindowHandle, IntPtr.Zero, b.X, b.Y, b.Width, b.Height, SWP.SWP_SHOWWINDOW);
        }

        private void OnSetBorderCommand(Process obj)
        {
            //var style = WindowStyles.WS_POPUPWINDOW | WindowStyles.WS_TABSTOP | WindowStyles.WS_VISIBLE | WindowStyles.WS_CAPTION;
            //var res=  User32.SetWindowLongPtr(obj.MainWindowHandle, (int) SetWindowLongIndex.GWL_STYLE, new IntPtr((int)style));
            //var res1=Marshal.GetLastWin32Error();

            //var style2 = WindowStyles.WS_EX_WINDOWEDGE;
            //var res2 = User32.SetWindowLongPtr(obj.MainWindowHandle, (int)SetWindowLongIndex.GWL_EXSTYLE, new IntPtr((int)style));
            //var res3 = Marshal.GetLastWin32Error();
            User32.SetWindowPos(obj.MainWindowHandle, IntPtr.Zero, 2000, 10, 500, 500, SWP.SWP_NOSIZE);
        }
    }
}
