using System;
using System.Windows.Forms;
using CodeRocket.Common;
using System.IO;
using Savchin.Forms.Docking;

namespace CodeRocket.Controls
{
    partial class FileWindow : DockContent
    {

        private string m_fileName = string.Empty;
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return m_fileName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    m_fileName = value;
                    return;
                }

                textEditor.LoadFile(value);


                m_fileName = value;
                this.ToolTipText = value;
            }
        }

        public FileWindow()
        {
            InitializeComponent();
        }



        // workaround of RichTextbox control's bug:
        // If load file before the control showed, all the text format will be lost
        // re-load the file after it get showed.
        private bool m_resetText = true;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (m_resetText)
            {
                m_resetText = false;
                FileName = FileName;
            }
        }

        /// <summary>
        /// Gets the persist string.
        /// </summary>
        /// <returns></returns>
        protected override string GetPersistString()
        {
            return GetType().ToString() + "," + FileName + "," + Text;
        }



        /// <summary>
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (FileName == string.Empty)
                this.textEditor.Text = Text;
        }

        #region Menu Items

        #region Edit

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(sender, e);
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.Redo();
        } 
        #endregion

        #region File
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditor.SaveFile(FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = AppCore.Current.SaveFileDialog;
            dialog.FileName = FileName;
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;
            textEditor.SaveFile(dialog.FileName);
        }

        /// <summary>
        /// Handles the Click event of the closeToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        #endregion






    }
}