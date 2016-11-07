using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BotvaSpider.Editors
{

    /// <summary>
    /// MdbFileEditor
    /// </summary>
    class MdbFileEditor : FileNameEditor
    {

        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        /// <param name="ofd">The ofd.</param>
        protected override void InitializeDialog(OpenFileDialog ofd)
        {
            ofd.CheckFileExists = false;
            ofd.Filter = "Access files (*.mdb)|*.mdb|All files (*.*)|*.*";
        }
    }
}