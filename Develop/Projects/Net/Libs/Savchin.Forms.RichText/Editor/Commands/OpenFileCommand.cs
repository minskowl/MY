using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Forms.RichText.Editor.Commands
{
    /// <summary>
    /// OpenFileCommand
    /// </summary>
    public class OpenFileCommand : EditorCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileCommand"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public OpenFileCommand(EditorControl control)
            : base(control)
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
                if (rtbDoc.Modified)
                {
                    var answer = MessageBox.Show(
                        "Save current file before opening another document?",
                        "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.No)
                    {
                        rtbDoc.Modified = false;
                    }
                    else
                    {
                        rtbDoc.SaveCommand.Execute(target);
                    }

                }
                using (var openFileDialog1 = new OpenFileDialog())
                {
                    openFileDialog1.Title = "RTE - Open File";
                    openFileDialog1.DefaultExt = "rtf";
                    openFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.FileName = string.Empty;

                    if (openFileDialog1.ShowDialog() != DialogResult.OK) return;


                    if (string.IsNullOrEmpty(openFileDialog1.FileName))
                        return;


                    string strExt = Path.GetExtension(openFileDialog1.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        rtbDoc.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        rtbDoc.Text = File.ReadAllText(openFileDialog1.FileName);
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    rtbDoc.CurrentFileName = openFileDialog1.FileName;
                    rtbDoc.Modified = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        #endregion
    }
}
