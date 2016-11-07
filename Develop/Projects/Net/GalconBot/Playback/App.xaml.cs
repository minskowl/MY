using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Playback
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var cul = new CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = cul;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cul;

        }
    }
}
