using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Savchin.Forms.Core.Commands;

namespace Savchin.Forms.RichText.Editor.Commands
{
    public class SaveAsCommand : EditorCommand
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAsCommand"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public SaveAsCommand(EditorControl control)
            : base(control)
        {

        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            string fileName;
            using (var saveFileDialog1 = new SaveFileDialog
                                      {
                                          Title = "RTE - Save File",
                                          DefaultExt = "rtf",
                                          Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*",
                                          FilterIndex = 1
                                      })
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

                if (string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    return;
                }
                fileName =
                    saveFileDialog1.FileName;
            }



            string strExt = Path.GetExtension(fileName);
            strExt = strExt.ToUpper();

            if (strExt == ".RTF")
            {
                rtbDoc.SaveFile(fileName, RichTextBoxStreamType.RichText);
            }
            else
            {
                var txtWriter = new StreamWriter(fileName);
                txtWriter.Write(rtbDoc.Text);
                txtWriter.Close();

                rtbDoc.SelectionStart = 0;
                rtbDoc.SelectionLength = 0;
            }

            rtbDoc.CurrentFileName = fileName;
            rtbDoc.Modified = false;
         
            MessageBox.Show(fileName + " saved.", "File Save");

        }

    }
}
