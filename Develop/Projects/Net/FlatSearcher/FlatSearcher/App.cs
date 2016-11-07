using System;
using System.IO;
using System.Windows.Forms;
using MyCustomWebBrowser.Core;
using Savchin.Development;
using WatiN.Core;

namespace FlatSearcher
{
    class App
    {
        [STAThread]
        static void Main()
        {
            SearchContext.Current = new SearchContext(new SingleThreadProvider());
            var fileName = Properties.Settings.Default.FileName;
            SearchContext.Current.Data = File.Exists(fileName) ? Database.Load(fileName) : Database.Load();

            Application.EnableVisualStyles();
            Application.ThreadException += Application_ThreadException;

            Settings.HighLightElement = false;

            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

        }
    }
}
