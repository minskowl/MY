using System;
using System.Windows.Forms;
using Savchin.EventSpy.Core;
using Savchin.Forms;

namespace Savchin.EventSpy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(new ExplorerForm());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
                UnHandleException((Exception)e.ExceptionObject);
        }

        private static void UnHandleException(Exception exception)
        {
            EventSpyCore.LogApp.Fatal("UnHandleException", exception);
            ExceptionForm.ShowException("UnHandleException", "WOW", exception);

        }
    }
}
