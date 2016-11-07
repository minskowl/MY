using System;

namespace Savchin.WinApi.Shell
{
    public class ExecuteItemEventArgs : EventArgs
    {
        // Fields
        private ShellMenuItem x7bf8c4d03998048a;
        private IntPtr x96e7d32425e52ebf;
        private ExecuteMenuItemFlags xebf45bdcaa1fd1e1;

        // Methods
        internal ExecuteItemEventArgs(ShellMenuItem menuItem, ExecuteMenuItemFlags flags, IntPtr hWnd)
        {
            this.xebf45bdcaa1fd1e1 = flags;
            this.x96e7d32425e52ebf = hWnd;
            this.x7bf8c4d03998048a = menuItem;
        }

        // Properties
        public ExecuteMenuItemFlags Flags
        {
            get
            {
                return this.xebf45bdcaa1fd1e1;
            }
        }

        public IntPtr HWnd
        {
            get
            {
                return this.x96e7d32425e52ebf;
            }
        }

        public ShellMenuItem MenuItem
        {
            get
            {
                return this.x7bf8c4d03998048a;
            }
        }
    }

 

}
