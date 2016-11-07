using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IES.PerformanceTester.Gui;
using Savchin.Forms;

namespace IntellexerSDK.PerformaceTests
{
    static class Program
    {
        const string title = "Unhandled exception";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(AppCore.FormMain);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject == null) return;


            if (e.ExceptionObject is Exception)
            {
                HandleException((Exception)e.ExceptionObject);
            }
            else
            {
                AppCore.Log.Fatal(title + e.ExceptionObject);
            }


        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception != null)
                HandleException(e.Exception);
        }

        private static void HandleException(Exception ex)
        {

            AppCore.Log.Fatal(title, ex);
            ExceptionForm.ShowException(title, title, ex);

        }
    }
}
