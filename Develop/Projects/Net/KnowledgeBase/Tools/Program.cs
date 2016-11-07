using System;
using System.Windows.Forms;
using KnowledgeBase.KbTools.Core;

namespace KnowledgeBase.KbTools
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
            Application.Run(AppCore.MainForm);
        }
    }
}
