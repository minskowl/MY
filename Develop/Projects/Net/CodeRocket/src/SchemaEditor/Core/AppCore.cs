using System.Windows.Forms;
using Savchin.Data.Schema.Controls;
using SchemaEditor.Commands;
using SchemaEditor.Controls;

namespace SchemaEditor.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AppCore
    {
        public static readonly CommandsDictionary Commands = new CommandsDictionary();
        private static IFormMain _formMain;

        /// <summary>
        /// Gets the form main.
        /// </summary>
        /// <value>The form main.</value>
        public static IFormMain FormMain
        {
            get { return _formMain; }
        }

        private static readonly OpenFileDialog _openFileDialog= new OpenFileDialog(){Filter="Schema|*.schema|All|*.*"};
        /// <summary>
        /// Gets the open file dialog.
        /// </summary>
        /// <value>The open file dialog.</value>
        public static OpenFileDialog OpenFileDialog
        {
            get { return _openFileDialog; }
        }

        private static SaveFileDialog _saveFileDialog= new SaveFileDialog();
        /// <summary>
        /// Gets the save file dialog.
        /// </summary>
        /// <value>The save file dialog.</value>
        public static SaveFileDialog SaveFileDialog
        {
            get { return _saveFileDialog; }
        }





        /// <summary>
        /// Initializes the specified main.
        /// </summary>
        /// <param name="main">The main.</param>
        internal static void Initialize(IFormMain main)
        {
            _formMain = main;
        }
    }
}
