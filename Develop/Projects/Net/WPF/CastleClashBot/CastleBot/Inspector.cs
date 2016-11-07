using System;
using System.Windows.Forms;
using CastleBot;

namespace CastleController
{
    public class Inspector
    {
        public static void Inject()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            var inspectorWinow = new ControllerForm {Owner = Application.OpenForms[0]};
            inspectorWinow.Show();
        }

        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception)e.ExceptionObject).Message);
        }
    }
}
