using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Savchin.Wpf.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationHelper
    {

        /// <summary>
        /// Gets the active window.
        /// </summary>
        /// <param name="currentApp">The current app.</param>
        /// <returns></returns>
        public static Window GetActiveWindow(this Application currentApp)
        {
            return Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);
        }

        /// <summary>
        /// Check Single Instance of appliction
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="app">The app.</param>
        /// <remarks>
        /// Using
        /// protected override void OnStartup(StartupEventArgs e)
        /// {
        /// ApplicationHelper.Make("MyWpfApplication", this);
        /// base.OnStartup(e);
        /// }
        /// </remarks>
        internal static void CheckSingleInstance(String name, Application app)
        {

            EventWaitHandle eventWaitHandle = null;
            String eventName = Environment.MachineName + "-" + AppDomain.CurrentDomain.BaseDirectory.Replace('\\', '-') + "-" + name;

            bool isFirstInstance = false;

            try
            {
                eventWaitHandle = EventWaitHandle.OpenExisting(eventName);
            }
            catch
            {
                // it's first instance
                isFirstInstance = true;
            }

            if (isFirstInstance)
            {
                eventWaitHandle = new EventWaitHandle(
                    false,
                    EventResetMode.AutoReset,
                    eventName);

                ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, waitOrTimerCallback, app, Timeout.Infinite, false);

                // not need more
                eventWaitHandle.Close();
            }
            else
            {
                eventWaitHandle.Set();

                // For that exit no interceptions
                Environment.Exit(0);
            }
        }


        private static void waitOrTimerCallback(Object state, Boolean timedOut)
        {
            Application app = (Application)state;
            app.Dispatcher.BeginInvoke(new activate(delegate() {
                Application.Current.MainWindow.Activate();
                }), null);
        }


        private delegate void activate();

    }
}
