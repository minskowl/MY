
using System;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Recorder
{
    public class MainApp
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new AppForm());
        }
    }
}