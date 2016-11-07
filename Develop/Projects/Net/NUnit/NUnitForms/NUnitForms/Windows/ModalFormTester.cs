

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Savchin.WinApi;
using Savchin.WinApi.Windows;

namespace NUnit.Extensions.Forms
{
    /// <summary>
    /// Used to specify a handler for a Modal form that is displayed during testing.
    /// </summary>
    public delegate void ModalFormActivated();

    internal delegate void ModalFormActivatedHwnd(IntPtr hWnd);

    ///<summary>
    /// A class for testing Modal Forms.
    ///</summary>
    public class ModalFormTester : IDisposable
    {
        #region Properties
        private const int CbtHookType = 5;
        private const int HCBT_DESTROYWND = 4;
        private const int HCBT_ACTIVATE = 5;
        private const int HCBT_MOVESIZE = 0;
        private const int HCBT_SETFOCUS = 9;


        /// <summary>
        /// The mapping of form titles to event handlers.
        /// </summary>
        private readonly Hashtable handlers = new Hashtable();


        private HookProc callback = null;
        private int handleToHook = 0;

        /// <summary>
        /// This list is used to keep track of which windows that have been created.
        /// </summary>
        private List<IntPtr> hwndList;

        /// <summary>
        /// True if we have begun listening for CBT Activate events.
        /// </summary>
        private bool listening = false;

        private bool showUnexpectedModalMessage = false;
        /// <summary>
        /// Gets or sets a value indicating whether [show unexpected modal message].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show unexpected modal message]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowUnexpectedModalMessage
        {
            get { return showUnexpectedModalMessage; }
            set { showUnexpectedModalMessage = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalFormTester"/> class.
        /// </summary>
        public ModalFormTester()
        {
            hwndList = new List<IntPtr>();
            BeginListening();
        }



        #region IDisposable Members

        ///<summary>
        /// Disposes any resources being managed.
        ///</summary>
        public void Dispose()
        {
            if (handleToHook != 0)
            {
                User32.UnhookWindowsHookEx(handleToHook);
                handleToHook = 0;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ModalFormTester"/> is reclaimed by garbage collection.
        /// </summary>
        ~ModalFormTester()
        {
            Dispose();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Clears the window list.
        /// </summary>
        public void ClearWindowList()
        {
            // Clear the list of open windows. Should be called from Setup to make sure no windows is in the list 
            // when starting a new testcase.
            hwndList.Clear();
        }
        /// <summary>
        /// A <see cref="ModalFormActivatedHwnd"/> that tries to click the OK button of the modal form.
        ///</summary>
        public void UnexpectedModal(IntPtr hWnd)
        {
            var messageBox = new MessageBoxTesterOld(hWnd);
            messageBox.ClickOk();
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="name">The name.</param>
        public void RemoveHandler(string name)
        {
            if (handlers.ContainsKey(name))
                handlers.Remove(name);
        }

        ///<summary>
        /// Registers an expected handler for the given form caption.
        ///</summary>
        ///<param name="name">The caption of the form to handle.</param>
        ///<param name="handler">The handler.</param>
        public void ExpectModal(string name, ModalFormActivated handler)
        {
            ExpectModal(name, handler, true);
        }

        ///<summary>
        /// Registers an expected or unexpected handler for the given form caption.
        ///</summary>
        ///<param name="name">The caption of the form to handle.</param>
        ///<param name="handler">The handler.</param>
        ///<param name="expected">True if this handler is expected; false if this handler is not expected.</param>
        public void ExpectModal(string name, ModalFormActivated handler, bool expected)
        {
            handlers[name] = new Handler(handler, (expected ? 1 : 0), name);
        }

        /// <summary>
        /// Verifies that all expected handlers were invoked,
        /// and that no unexpected ones were.
        /// </summary>
        public bool Verify()
        {
            foreach (Handler h in handlers.Values)
            {
                if (!h.Verify())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <returns></returns>
        public string[] GetErrors()
        {
            var errors = new ArrayList();
            foreach (string name in handlers.Keys)
            {
                var h = (Handler)handlers[name];
                if (!h.Verify())
                {
                    errors.Add(h.GetError() + string.Format(" (Form Caption = {0})", h.Name));
                }
            }
            return (string[])errors.ToArray(typeof(string));
        }
        #endregion

        #region Helpers
        ///<summary>
        /// Registers an expected or unexpected handler for the given form caption.
        ///</summary>
        ///<param name="name">The caption of the form to handle.</param>
        ///<param name="handler">The handler.</param>
        ///<param name="expectedCount">number of times this handler is expected</param>
        internal Handler Add(string name, ModalFormActivatedHwnd handler, int expectedCount)
        {
            var handlerObject = new Handler(handler, expectedCount, name);
            handlers[name] = handlerObject;
            return handlerObject;
        }




        private void BeginListening()
        {
            if (listening) return;


            listening = true;
            // Note: the callback is saved as a member to keep the CLR from shuffling off the pointer
            // before the callback is used.
            // If we try to assign the call back "inline" we get memory violation errors.
            callback = Callback_ModalListener;
            handleToHook = User32.SetWindowsHookEx(WH.WH_HCBT_ACTIVATE, callback, IntPtr.Zero, Kernel32.GetCurrentThreadId());
        }
        
        /// <summary>
        /// CBT callback called when a form is activated.
        /// If the newly activated form is modal and matches any registered names,
        /// invoke the appropriate hander.
        /// </summary>
        private int Callback_ModalListener(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == HCBT_ACTIVATE)
            {
                // Some controls sends an HCBT_ACTIVATE when changed for example tabPages. We do not 
                // want our handler to be called when a tabPage is changed. This is a problem in Modal
                // modal windows.
                if (!hwndList.Contains(wParam))
                {
                    hwndList.Add(wParam);
                    FindWindowNameAndInvokeHandler(wParam);
                }
            }
            if (code == HCBT_DESTROYWND)
            {
                // Need to remove the handle when the window is destroyed.
                if (hwndList.Contains(wParam))
                {
                    hwndList.Remove(wParam);
                }
            }

            return User32.CallNextHookEx(handleToHook, code, wParam, lParam);
        }

        private void RemoveModal(IntPtr hwnd)
        {
            string name = null;

            Form form = Form.FromHandle(hwnd) as Form;
            if (form != null && form.Modal)
            {
                name = form.Name;
            }
            else if (WindowHandle.IsDialog(hwnd))
            {
                name = WindowHandle.GetCaption(hwnd);
                if (name == null)
                {
                    name = string.Empty;
                }
            }

        }
        private void FindWindowNameAndInvokeHandler(IntPtr hwnd)
        {
            string name = null;

            var form = Control.FromHandle(hwnd) as Form;
            if (form != null && form.Modal)
            {
                name = form.Name;
            }
            else if (WindowHandle.IsDialog(hwnd))
            {
                name = WindowHandle.GetCaption(hwnd) ?? string.Empty;
            }

            Invoke(name, hwnd);

        }
        private void Invoke(string name, IntPtr hWnd)
        {
            if (name == null) return;
            if (name == string.Empty) name = "Unnamed";

            var namedHandler = handlers[name] as Handler;

            if (namedHandler == null)
            {

                if (!showUnexpectedModalMessage)return;

                var del =
                    (ModalFormActivatedHwnd)
                    Delegate.CreateDelegate(typeof(ModalFormActivatedHwnd), this, "UnexpectedModal");
                namedHandler = Add(name, del, 0);
            }
            namedHandler.Invoke(hWnd);
        }
        #endregion


        #region Nested type: Handler

        /// <summary>
        /// This class encapsulates a event handler
        /// along with information on whether it was
        /// expected to be called, and if it was actually called.
        /// </summary>
        internal class Handler
        {
            private readonly int expectedCount = 0;
            private readonly Delegate handler = null;
            private readonly string name;
            /// <summary>
            /// Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name
            {
                get { return name; }
            }
            private int invokedCount = 0;

            /// <summary>
            /// Constructs a new <see cref="Handler"/>.
            /// </summary>
            public Handler(Delegate handler, int expectedTimes, string name)
            {
                this.handler = handler;
                expectedCount = expectedTimes;
                this.name = name;
            }



            /// <summary>
            /// Verify that this handler was either expected and invoked,
            /// or not expected and not invoked.
            /// </summary>
            public bool Verify()
            {
                return expectedCount == invokedCount;
            }

            /// <summary>
            /// Invokes the wrapped event handler with the given window handle.
            /// </summary>
            /// <param name="hWnd"></param>
            public void Invoke(IntPtr hWnd)
            {
                invokedCount++;
                try
                {
                    if (handler is ModalFormActivated)
                    {
                        handler.DynamicInvoke(new object[] { });
                    }
                    else if (handler is ModalFormActivatedHwnd)
                    {
                        handler.DynamicInvoke(new object[] { hWnd });
                    }
                }
                catch (TargetInvocationException ex)
                {
                    // Unwrap any exceptions that happen in the Reflection layer.
                    if (ex.InnerException != null)
                    {
                        throw ex.InnerException;
                    }
                }
            }


            /// <summary>
            /// Gets the error.
            /// </summary>
            /// <returns></returns>
            public string GetError()
            {
                if (Verify())
                {
                    throw new InvalidOperationException("Don't call GetError when there are not errors");
                }
                return
                    string.Format("expected {0} invocations of modal, but was invoked {1} times", expectedCount,
                                  invokedCount);
            }
        }

        #endregion
    }
}