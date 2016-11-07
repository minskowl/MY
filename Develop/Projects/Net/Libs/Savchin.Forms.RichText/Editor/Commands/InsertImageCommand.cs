using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Forms.RichText.Editor.Commands
{
    /// <summary>
    /// InsertImageCommand
    /// </summary>
    public class InsertImageCommand : EditorCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertImageCommand"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public InsertImageCommand(EditorControl control)
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
            using (var openFileDialog1 = new OpenFileDialog
                                      {
                                          Title = "RTE - Insert Image File",
                                          DefaultExt = "rtf",
                                          Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif",
                                          FilterIndex = 1
                                      })
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    InsertImage(openFileDialog1.FileName);
            }

        }

        private void InsertImage(string fileName)
        {
            try
            {

                var img = Image.FromFile(fileName);
                Clipboard.SetDataObject(img);
                DataFormats.Format df = DataFormats.GetFormat(DataFormats.Bitmap);
                if (rtbDoc.CanPaste(df))
                {
                    rtbDoc.Paste(df);
                }
            }
            catch
            {
                MessageBox.Show("Unable to insert image format selected.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
