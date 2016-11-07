using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Savchin.Forms.RichText.Editor.Commands;

namespace Savchin.Forms.RichText.Editor
{
    /// <summary>
    /// RichTextPrinter
    /// </summary>
    public  class EditorControl : RichTextBox
    {
        #region Properties
        /// <summary>
        /// SaveAsCommand
        /// </summary>
        public readonly SaveAsCommand SaveAsCommand;
        /// <summary>
        /// SaveCommand
        /// </summary>
        public readonly SaveCommand SaveCommand;
        /// <summary>
        /// OpenFileCommand
        /// </summary>
        public readonly OpenFileCommand OpenFileCommand;
        /// <summary>
        /// NewFileCommand
        /// </summary>
        public readonly NewFileCommand NewFileCommand;

        /// <summary>
        /// InsertImageCommand
        /// </summary>
        public readonly InsertImageCommand InsertImageCommand;

        // Fields
        private const double AnInch = 14.4;
        private const int EM_FORMATRANGE = 0x439;
        private const int WM_USER = 0x400;

        /// <summary>
        /// Gets or sets a value indicating whether [accepts return].
        /// </summary>
        /// <value><c>true</c> if [accepts return]; otherwise, <c>false</c>.</value>
        public bool AcceptsReturn { get; set; }

        private string _currentFileName;

        /// <summary>
        /// Gets or sets the name of the current file.
        /// </summary>
        /// <value>The name of the current file.</value>
        public string CurrentFileName
        {
            get { return _currentFileName; }
            internal set
            {
                _currentFileName = value;
                OnCurrentFileNameChanged();
            }
        } 
        #endregion

        /// <summary>
        /// Occurs when [current file name changed].
        /// </summary>
        public event EventHandler CurrentFileNameChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorControl"/> class.
        /// </summary>
        public EditorControl()
        {
            SaveAsCommand= new SaveAsCommand(this);
            SaveCommand= new SaveCommand(this);
            OpenFileCommand= new OpenFileCommand(this);
            NewFileCommand= new NewFileCommand(this);
            InsertImageCommand= new InsertImageCommand(this);
        }

        /// <summary>
        /// Determines whether the specified key is an input key or a special key that requires preprocessing.
        /// </summary>
        /// <param name="keyData">One of the Keys value.</param>
        /// <returns>
        /// true if the specified key is an input key; otherwise, false.
        /// </returns>
        protected override bool IsInputKey(Keys keyData)
        {
            if (Multiline && ((keyData & Keys.Alt) == Keys.None))
            {
                Keys keys = keyData & Keys.KeyCode;
                if (keys == Keys.Return)
                {
                    return AcceptsReturn;
                }
            }
            return base.IsInputKey(keyData);
        }

 

 

        // Methods
        /// <summary>
        /// Prints the specified char from.
        /// </summary>
        /// <param name="charFrom">The char from.</param>
        /// <param name="charTo">The char to.</param>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public  int Print(int charFrom, int charTo, PrintPageEventArgs e)
        {
            CHARRANGE cRange;
            FORMATRANGE fmtRange;
            RECT rectPage;
            RECT rectToPrint;
            cRange.cpMin = charFrom;
            cRange.cpMax = charTo;
            rectToPrint.Top = (int)Math.Round((e.MarginBounds.Top * AnInch));
            rectToPrint.Bottom = (int)Math.Round((e.MarginBounds.Bottom * AnInch));
            rectToPrint.Left = (int)Math.Round((e.MarginBounds.Left * AnInch));
            rectToPrint.Right = (int)Math.Round((e.MarginBounds.Right * AnInch));
            rectPage.Top = (int)Math.Round((e.PageBounds.Top * AnInch));
            rectPage.Bottom = (int)Math.Round((e.PageBounds.Bottom * AnInch));
            rectPage.Left = (int)Math.Round((e.PageBounds.Left * AnInch));
            rectPage.Right = (int)Math.Round((e.PageBounds.Right * AnInch));
            IntPtr hdc = e.Graphics.GetHdc();
            fmtRange.chrg = cRange;
            fmtRange.hdc = hdc;
            fmtRange.hdcTarget = hdc;
            fmtRange.rc = rectToPrint;
            fmtRange.rcPage = rectPage;
            IntPtr res = IntPtr.Zero;
            var wparam =  new IntPtr(1);
            var lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);
            res = SendMessage(this.Handle, EM_FORMATRANGE, wparam, lparam);

            Marshal.FreeCoTaskMem(lparam);
            e.Graphics.ReleaseHdc(hdc);

            return res.ToInt32();
        }

        /// <summary>
        /// Called when [current file name changed].
        /// </summary>
        protected virtual void OnCurrentFileNameChanged()
        {
            if (CurrentFileNameChanged != null)
                CurrentFileNameChanged(this, EventArgs.Empty);
        }

        [DllImport("USER32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        // Nested Types
        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public RECT rc;
            public RECT rcPage;
            public CHARRANGE chrg;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }


}
