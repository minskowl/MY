using System;
using System.Collections.Generic;
using System.Threading;

namespace Savchin.WinApi.Windows
{
    /// <summary>
    /// WindowHandler
    /// </summary>
    public delegate bool WindowHandler(Window window);

    public class WindowWatcher : IDisposable
    {
        private readonly Thread watchThread;

        private readonly List<WindowHandler> handlers = new List<WindowHandler>();
        private bool keepRunning = true;
        private readonly IntPtr desktop;



        #region Properties

        private static readonly WindowWatcher instance = new WindowWatcher();
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static WindowWatcher Instance
        {
            get { return instance; }
        }

        private bool enabled = false;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WindowWatcher"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get { return watchThread.IsAlive; }
        }

        private int sleepDelay = 2000;
        /// <summary>
        /// Gets or sets the sleep delay.
        /// </summary>
        /// <value>The sleep delay.</value>
        public int SleepDelay
        {
            get { return sleepDelay; }
            set { sleepDelay = value; }
        }

        private int watchDelay = 200;
        /// <summary>
        /// Gets or sets the watch delay.
        /// </summary>
        /// <value>The watch delay.</value>
        public int WatchDelay
        {
            get { return watchDelay; }
            set { watchDelay = value; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowWatcher"/> class.
        /// </summary>
        private WindowWatcher()
        {
            desktop = User32.GetDesktopWindow();
            watchThread = new Thread(WatchWindows);
            watchThread.Start();
        }
        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void AddHandler(WindowHandler handler)
        {
            if (handler == null) throw new ArgumentNullException("handler");

            handlers.Add(handler);
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void RemoveHandler(WindowHandler handler)
        {
            if (handler == null) throw new ArgumentNullException("handler");
            handlers.Remove(handler);
        }
        /// <summary>
        /// Clears the handlers.
        /// </summary>
        public void ClearHandlers()
        {
            handlers.Clear();
        }

        private void WatchWindows()
        {
            do
            {
                while (enabled)
                {
                    if (handlers.Count > 0) User32.EnumChildWindows(desktop, OnEnumWindow, IntPtr.Zero);

                    Thread.Sleep(watchDelay);
                }
                Thread.Sleep(sleepDelay);
            }
            while (keepRunning);
        }

        /// <summary>
        /// Called when [enum window].
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns></returns>
        private int OnEnumWindow(IntPtr hwnd, IntPtr lParam)
        {
            var info = new Window(hwnd);
            foreach (var handler in handlers)
            {
                if (handler(info)) break;

            }

            return 1;
        }




        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("WindowWatcher.Dispose");
            lock (this)
            {
                keepRunning = false;
                if (IsRunning)
                {
                    watchThread.Join();
                }
                ClearHandlers();
            }

        }
    }
}