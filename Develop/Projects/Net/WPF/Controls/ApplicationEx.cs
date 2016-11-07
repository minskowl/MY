using System;
using System.Windows;
using Savchin.Wpf.Controls.Core;

namespace Savchin.Wpf.Controls
{
    public class ApplicationEx : Application
    {
        public ApplicationEx()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
        }

        

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorForm.Show(e);
        }
    }
}
