using System;
using System.Windows.Forms;

namespace CastleBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       // [STAThread]
        static void Main()
        {
           // BlueStacks.hyperDroid.Frontend.Console.Main(new string[] { "Android" });
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProcessForm());
        }
    }
}
