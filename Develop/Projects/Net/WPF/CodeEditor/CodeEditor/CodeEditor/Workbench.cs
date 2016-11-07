using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;
using Savchin.CodeEditor.Core;
using Savchin.CodeEditor.GUI;
using Savchin.CodeEditor.Services;

namespace Savchin.CodeEditor
{
    class Workbench : IWorkbench, IWin32Window
    {
        SdStatusBar _statusBar = new SdStatusBar();

        System.Windows.WindowState lastNonMinimizedWindowState = System.Windows.WindowState.Normal;
        private readonly Window _window;

        public Workbench(Window window)
        {
            _window = window;
            this.SynchronizingObject = new WpfSynchronizeInvoke(_window.Dispatcher);
            this.StatusBar = new SdStatusBarService(_statusBar);
        }

        IntPtr IWin32Window.Handle
        {
            get
            {
                var wnd = PresentationSource.FromVisual(_window) as System.Windows.Interop.IWin32Window;
                if (wnd != null)
                    return wnd.Handle;
                else
                    return IntPtr.Zero;
            }
        }
        public ICSharpCode.Core.Properties CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void SetMemento(ICSharpCode.Core.Properties memento)
        {
            Rect bounds = memento.Get("Bounds", new Rect(10, 10, 750, 550));
            // bounds are validated after PresentationSource is initialized (see OnSourceInitialized)
            lastNonMinimizedWindowState = memento.Get("WindowState", System.Windows.WindowState.Maximized);
            SetBounds(bounds);
        }


        public IWin32Window MainWin32Window
        {
            get { return this; }
        }

        public ISynchronizeInvoke SynchronizingObject { get; private set; }

        Window IWorkbench.MainWindow
        {
            get { return _window; }
        }

        public IStatusBarService StatusBar { get; private set; }

        public string Title
        {
            get { return _window.Title; }
            set { _window.Title=value; }
        }

        public ICollection<IViewContent> ViewContentCollection
        {
            get { throw new NotImplementedException(); }
        }

        public IViewContent ActiveViewContent
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler ActiveViewContentChanged;

        public object ActiveContent
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsActiveWindow
        {
            get { throw new NotImplementedException(); }
        }

        public bool CloseAllSolutionViews()
        {
            return true;
        }


        void SetBounds(Rect bounds)
        {
            _window.Left = bounds.Left;
            _window.Top = bounds.Top;
            _window.Width = bounds.Width;
            _window.Height = bounds.Height;
        }
    }
}
