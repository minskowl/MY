using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using log4net;
using log4net.Core;
using Savchin.Bubbles.Core;

namespace Bubbles.Braker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILog log = LogManager.GetLogger("App");



        private readonly Settings settings;
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public Settings Settings
        {
            get { return settings; }
        }
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static new App Current
        {
            get { return (App)Application.Current; }
        }

        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        public ILog Log
        {
            get { return log; }
        }
        private readonly GameStatistic statistics;
        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <value>The statistics.</value>
        public GameStatistic Statistics
        {
            get { return statistics; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">
        /// More than one instance of the <see cref="T:System.Windows.Application"/> class is created per <see cref="T:System.AppDomain"/>.
        /// </exception>
        public App()
        {
            settings = Settings.Create();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            if (!Directory.Exists(Settings.LogPath))
                Directory.CreateDirectory(Settings.LogPath);
            statistics = new GameStatistic();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject == null)
                return;
            if (e.ExceptionObject is Exception)
                UnhandledException((Exception)e.ExceptionObject);
            else
                Log.Fatal("CurrentDomain_UnhandledException \n" + e.ExceptionObject);
        }
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            UnhandledException(e.Exception);
            e.Handled = true;
        }
        private void UnhandledException(Exception ex)
        {
            Log.Fatal("UnhandledException", (Exception)ex);
        }

        /// <summary>
        /// Handles the Exit event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.ExitEventArgs"/> instance containing the event data.</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            settings.Save();
            statistics.Dispose();
        }


    }
}
