using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Controls;
using BotvaSpider.Core;

namespace BotvaSpider
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

            if (!AppCore.Initilaize(new MainForm())) return;

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            Application.Run(AppCore.FormMain);

        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            FormErrorReport.ShowErrorReport(e.Exception, "Фатальная ошибка", AppCore.Version);
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            AppCore.Unload();
        }
    }
}
