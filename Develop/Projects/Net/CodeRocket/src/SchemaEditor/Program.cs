using System;
using System.Threading;
using System.Windows.Forms;
using Savchin.Controls.Common;

namespace SchemaEditor
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

#if RELEASE
            Application.ThreadException += Application_OnThreadException;
#endif


            Application.Run(new FormMain());
#if RELEASE
            Application.ThreadException -= Application_OnThreadException;
#endif
        }

        /// <summary>
        /// Handles the OnThreadException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Threading.ThreadExceptionEventArgs"/> instance containing the event data.</param>
        private static void Application_OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionForm.ShowException("Error","Unhandled Exception",e.Exception);
        }
    }
}