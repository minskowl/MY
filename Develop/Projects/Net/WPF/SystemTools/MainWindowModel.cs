using System;
using System.Diagnostics;
using System.Linq;
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

        public DelegateCommand<Process> SendCdJwfUtilsDebugCommand { get; set; }
        static MainWindowModel()
        {
            // Process.EnterDebugMode();
        }

        public MainWindowModel()
        {
            Screens = Screen.AllScreens;
            Processes = GetProcess();
            SetBorderCommand = new DelegateCommand<Process>(OnSetBorderCommand);
            SendCdJwfUtilsDebugCommand = new DelegateCommand<Process>(OnSendCdJwfUtilsDebugCommand);
            MoveToCommand = new DelegateCommand<Screen>(OnMoveToCommand);
        }

        private void OnSendCdJwfUtilsDebugCommand(Process obj)
        {
//            SendText(obj, @"cd JWFUtils_Debug
//");
        }

        private Process[] GetProcess()
        {
            /*
            var result = new List<Process>();
            foreach (var process in Process.GetProcesses())
            {
               
                try
                {
                    foreach (ProcessModule module in process.Modules)
                    {
                        if (module.FileName.StartsWith(@"C:\Program Files (x86)\Trend Micro"))
                        {
                            result.Add(process);
                            break;
                        }
                    }
                }
                catch (Win32Exception e)
                {
                    Console.WriteLine($"{process.ProcessName} {process.Id} {e.Message}");
                }
      
            }
            return result.OrderBy(e => e.ProcessName).ToArray();*/
            return Process.GetProcesses().OrderBy(e => e.ProcessName).ToArray();
        }

        private void OnMoveToCommand(Screen obj)
        {
            if (SelectedItem == null || SelectedItem.MainWindowHandle != IntPtr.Zero) return;
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

        //private InputSimulator _inputSimulator = new InputSimulator();
        //private void SendText(Process obj, string text)
        //{
        //    User32.SetForegroundWindow(obj.MainWindowHandle);
        //    //User32.SendMessage(obj.MainWindowHandle, (uint)WM.WM_SETTEXT, 0, text);
        //    User32.SendMessage(obj.MainWindowHandle, (uint)WM.WM_KEYDOWN, User32.VkKeyScan('c'), 0);

        //}
    }
}
