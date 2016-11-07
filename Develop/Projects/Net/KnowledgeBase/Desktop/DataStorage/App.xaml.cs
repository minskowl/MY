using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using KnowledgeBase.Core;
using Savchin.Wpf.Controls.Core;

namespace KnowledgeBase.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#if DEBUG
        private DispatcherTimer _timer;
#endif

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppCore.Init();

            // KbContext.Current.SetCurrentUserID(6);

#if DEBUG
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 10), DispatcherPriority.ApplicationIdle, OnTimer,
                                         Dispatcher.CurrentDispatcher);
            _timer.IsEnabled = true;
#endif

        }

        private void OnTimer(object sender, EventArgs e)
        {

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AppCore.Dispose();
#if DEBUG
            if (_timer != null)
            {
                _timer.Tick -= OnTimer;
                _timer.IsEnabled = false;
                _timer = null;
            }
#endif
        }
        /// <summary>
        /// Handles the DispatcherUnhandledException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                AppCore.Log.Error("Unhandled Exception", e.Exception);
                ErrorForm.Show(e);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }


    }
}
