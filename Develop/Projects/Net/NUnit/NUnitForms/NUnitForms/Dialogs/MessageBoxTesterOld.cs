
using System;
using System.Windows.Forms;
using Savchin.WinApi;
using Savchin.WinApi.Windows;

namespace NUnit.Extensions.Forms
{
    #region Command enum


    #endregion

    /// <summary>
    /// A ControlTester for MessageBoxes.  
    /// Allows you to handle and test MessageBoxes by pressing any of the
    /// buttons that ordinarily appear on them.
    /// </summary>
    /// <remarks>
    /// It does not extend ControlTester because MessageBoxes are not controls.</remarks>
    /// <code>
    /// public void MessageBoxHandler
    /// {
    /// 	MessageBoxTester messageBox = new MessageBoxTester( "MessageBoxName" );
    /// 	Assert.AreEqual( "MessageBoxText", messageBox.Text );
    ///   Assert.AreEqual( "MessageBoxTitle", messageBox.Title );
    /// 	messageBox.SendCommand( MessageBoxTester.Command.OK );
    /// }
    /// </code>
    public class MessageBoxTesterOld : Tester<System.Windows.Forms.MessageBox, MessageBoxTesterOld>
    {


        private int sleepDelayTime = 100;
        private int maxAttemptCount = 10;
        private WinButton firstButton;
        protected IntPtr handle;
        private Command command;
        /// <summary>
        /// Initializes a new instance of the <see cref="Savchin.WinApi.Windows.MessageBox"/> class.
        /// </summary>
        public MessageBoxTesterOld()
        {
        }

        /// <summary>
        /// Creates a MessageBoxTester with the specified handle.  NUnitForms
        /// users probably won't use this directly.  Use the other constructor.
        /// </summary>
        /// <param name="handle">The handle of the MessageBox to test.</param>
        public MessageBoxTesterOld(IntPtr handle)
            : base(null)
        {
            this.handle = handle;
        }

        /// <summary>
        /// Creates a MessageBoxTester that finds MessageBoxes with the
        /// specified name.
        /// </summary>
        /// <param name="name">The name of the MessageBox to test.</param>
        public MessageBoxTesterOld(string name)
            : base(name)
        {
        }

        #region Properties
        /// <summary>
        /// Returns the caption on the message box we are testing.
        /// </summary>
        public string Title
        {
            get
            {
                FindMessageBox();
                return WindowHandle.GetCaption(handle);
            }
        }

        /// <summary>
        /// Returns the text of the message box we are testing.
        /// </summary>
        public string Text
        {
            get
            {
                FindMessageBox();
                return WindowHandle.GetText(handle);
            }
        }

        /// <summary>
        /// Gets or sets the sleep delay time.
        /// </summary>
        /// <value>The sleep delay time.</value>
        public int SleepDelayTime
        {
            get { return sleepDelayTime; }
            set { sleepDelayTime = value; }
        }

        /// <summary>
        /// Gets or sets the max attempt count.
        /// </summary>
        /// <value>The max attempt count.</value>
        public int MaxAttemptCount
        {
            get { return maxAttemptCount; }
            set { maxAttemptCount = value; }
        }

        /// <summary>
        /// Gets the first button.
        /// </summary>
        /// <value>The first button.</value>
        public WinButton FirstButton
        {
            get
            {
                if (firstButton == null && handle != IntPtr.Zero)
                {
                    firstButton = new WinButton(2, handle);
                }
                return firstButton;
            }
        }

        #endregion

        /// <summary>
        /// Sends a command to the MessageBox.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommand(Command command)
        {
            this.command = command;
            FindMessageBox();
            SendCommnadFinal();
        }

        #region Interface
        /// <summary>
        /// Whaits the and send command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void WhaitAndSendCommand(Command command)
        {
            this.command = command;
            WindowWatcher.Instance.AddHandler(SendCommandHanler);
        }


        /// <summary>
        /// Whaits the and click first button.
        /// </summary>
        public void WhaitAndClickFirstButton()
        {
            WindowWatcher.Instance.AddHandler(ClickFirstButtonHanler);
        }


        /// <summary>
        /// Clicks the Ok button of a MessageBox.
        /// </summary>
        public void ClickOk()
        {
            SendCommand(Command.OK);
        }

        /// <summary>
        /// Clicks the cancel button of a MessageBox.
        /// </summary>
        public void ClickCancel()
        {
            SendCommand(Command.Cancel);
        }
        
        #endregion


        #region Handlers
        private bool ClickFirstButtonHanler(Window window)
        {
            if (!IsThisWindow(window))
                return false;
            FirstButton.Click();
            WindowWatcher.Instance.RemoveHandler(ClickFirstButtonHanler);
            return true;
        }

        private bool SendCommandHanler(Window window)
        {
            if (!IsThisWindow(window))
                return false;


            SendCommnadFinal();
            WindowWatcher.Instance.RemoveHandler(SendCommandHanler);
            return true;
        } 
        #endregion

        private bool IsThisWindow(Window window)
        {
            if (!window.IsDialog)
                return false;
            if (name != null && name != window.Name)
                return false;

            handle = window.Handle;
            return true;
        }


        private void SendCommnadFinal()
        {
            User32.SendMessage(handle, NativeMethods.WM_ACTIVATE, NativeMethods.MA_ACTIVATE, 0);
            User32.SendMessage(handle, NativeMethods.WM_COMMAND, (UIntPtr)((uint)command), IntPtr.Zero);
        }


        private void FindMessageBox()
        {
            FindMessageBoxEx();
            if (handle == IntPtr.Zero)
            {
                throw new ControlNotVisibleException("Message Box not visible");
            }
        }

        /// <summary>
        /// Finds the message box ex.
        /// </summary>
        /// <returns></returns>
        private void FindMessageBoxEx()
        {
            if (handle != IntPtr.Zero)
                return;

            lock (this)
            {
                User32.EnumChildWindows(User32.GetDesktopWindow(), OnEnumWindow, IntPtr.Zero);
            }
        }

        private int OnEnumWindow(IntPtr hwnd, IntPtr lParam)
        {
            if (WindowHandle.IsDialog(hwnd))
            {
                if (name == null)
                {
                    handle = hwnd;
                }
                else
                {
                    string windowName = WindowHandle.GetCaption(hwnd);
                    if (windowName == name)
                    {
                        handle = hwnd;

                    }
                }
            }
            return 1;
        }
    }


}