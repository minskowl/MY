using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Forms.RichText.Editor.Commands
{
    /// <summary>
    /// SaveCommand
    /// </summary>
    public class SaveCommand : EditorCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCommand"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public SaveCommand(EditorControl control) : base(control)
        {
        }

        #region Overrides of Command

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            try
            {
                if (string.IsNullOrEmpty( rtbDoc.CurrentFileName))
                {
                    rtbDoc.SaveAsCommand.Execute(target);
                    return;
                }


                string strExt = Path.GetExtension(rtbDoc.CurrentFileName);
                strExt = strExt.ToUpper();
                if (strExt == ".RTF")
                {
                    rtbDoc.SaveFile(rtbDoc.CurrentFileName);
                }
                else
                {

                    File.WriteAllText(rtbDoc.CurrentFileName, rtbDoc.Text);
                    rtbDoc.SelectionStart = 0;
                    rtbDoc.SelectionLength = 0;
                }

                rtbDoc.Modified = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Save Error");
            }

        }

        #endregion
    }
}
