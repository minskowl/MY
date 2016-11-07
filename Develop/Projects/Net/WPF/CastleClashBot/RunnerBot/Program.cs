using System;
using Console = BlueStacks.hyperDroid.Frontend.Console;
using System.Windows.Forms;
namespace RunnerBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Console.Main(new string[] { "Android" });
            //var f = new Form1();
            //f.ShowDialog();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
