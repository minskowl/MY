// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Savchin.Forms.Core.Commands;

namespace Savchin.Forms.RichText.Editor
{
    /// <summary>
    /// RichTextEditor
    /// </summary>
    public partial class RichTextEditor : UserControl
    {

        #region Properties

        private string _currentFile;

        private int _checkPrint;

        /// <summary>
        /// Gets or sets a value indicating whether [accepts return].
        /// </summary>
        /// <value><c>true</c> if [accepts return]; otherwise, <c>false</c>.</value>
        public bool AcceptsReturn
        {
            get { return rtbDoc.AcceptsReturn; }
            set { rtbDoc.AcceptsReturn = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [accepts tab].
        /// </summary>
        /// <value><c>true</c> if [accepts tab]; otherwise, <c>false</c>.</value>
        public bool AcceptsTab
        {
            get { return rtbDoc.AcceptsTab; }
            set { rtbDoc.AcceptsTab = value; }
        }

        /// <summary>
        /// Gets or sets the document text.
        /// </summary>
        /// <value>The document text.</value>
        public string DocumentText
        {
            get { return rtbDoc.Text; }
            set { rtbDoc.Text=value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RichTextEditor"/> is modified.
        /// </summary>
        /// <value><c>true</c> if modified; otherwise, <c>false</c>.</value>
        public bool Modified
        {
            get { return rtbDoc.Modified; }
            set { rtbDoc.Modified = value; }
        }

        /// <summary>
        /// Gets the editor.
        /// </summary>
        /// <value>The editor.</value>
        public EditorControl Editor
        {
            get { return rtbDoc; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RichTextEditor"/> class.
        /// </summary>
        public RichTextEditor()
        {
            InitializeComponent();

            if(DesignMode)return;

            SaveAsToolStripMenuItem.BindCommand(rtbDoc.SaveAsCommand);
            SaveToolStripMenuItem.BindCommand(rtbDoc.SaveCommand);
            OpenToolStripMenuItem.BindCommand(rtbDoc.OpenFileCommand);
            NewToolStripMenuItem.BindCommand(rtbDoc.NewFileCommand);
            InsertImageToolStripMenuItem.BindCommand(rtbDoc.InsertImageCommand);

            tbrSave.BindCommand(rtbDoc.SaveCommand);
            tbrOpen.BindCommand(rtbDoc.OpenFileCommand);
            tbrNew.BindCommand(rtbDoc.NewFileCommand);

            rtbDoc.CurrentFileNameChanged += rtbDoc_CurrentFileNameChanged;

        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="fileType">Type of the file.</param>
        public void SaveFile(Stream data, RichTextBoxStreamType fileType)
        {
            rtbDoc.SaveFile(data, fileType);
        }
        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="fileType">Type of the file.</param>
        public void LoadFile(Stream data, RichTextBoxStreamType fileType)
        {
            rtbDoc.LoadFile(data, fileType);
        }
        #region "Menu Methods"

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.Modified)
                {
                    DialogResult answer = MessageBox.Show(
                        "Save this document before closing?",
                        "Unsaved Document",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes) return;

                    rtbDoc.Modified = false;
                    Application.Exit();
                }
                else
                {
                    rtbDoc.Modified = false;
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to select all document content.", "RTE - Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Copy();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to copy document content.", "RTE - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Cut();
            }
            catch
            {
                MessageBox.Show("Unable to cut document content.", "RTE - Cut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Paste();
            }
            catch
            {

                MessageBox.Show("Unable to copy clipboard content to document.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void SelectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FontDialog1.Font = rtbDoc.SelectionFont;
                FontDialog1.ShowApply = true;
                if (FontDialog1.ShowDialog() == DialogResult.OK)
                {
                    rtbDoc.SelectionFont = FontDialog1.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }



        private void FontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.ForeColor;
                if (ColorDialog1.ShowDialog() == DialogResult.OK)
                {
                    rtbDoc.SelectionColor = ColorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont == null) return;
            try
            {

                Font currentFont = rtbDoc.SelectionFont;

                FontStyle newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Bold;

                rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void ItalicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont == null) return;
            try
            {

                var currentFont = rtbDoc.SelectionFont;

                var newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void UnderlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont == null) return;
            try
            {

                Font currentFont = rtbDoc.SelectionFont;

                FontStyle newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont == null) return;
            try
            {
                var currentFont = rtbDoc.SelectionFont;
                rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Regular);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void PageColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.BackColor;
                if (ColorDialog1.ShowDialog() == DialogResult.OK)
                {
                    rtbDoc.BackColor = ColorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void mnuUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanUndo)
                {
                    rtbDoc.Undo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanRedo)
                {
                    rtbDoc.Redo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void LeftToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void CenterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void RightToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void AddBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.BulletIndent = 10;
                rtbDoc.SelectionBullet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void RemoveBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionBullet = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }





        private void mnuIndent0_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void mnuIndent5_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void mnuIndent10_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void mnuIndent15_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 15;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void mnuIndent20_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 20;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }




        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new frmFind(rtbDoc) { Owner = FindForm() };
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void FindAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new frmReplace(rtbDoc) { Owner = FindForm() };
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void PreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPreviewDialog1.Document = PrintDocument1;
                PrintPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog1.Document = PrintDocument1;
                if (PrintDialog1.ShowDialog() == DialogResult.OK)
                {
                    PrintDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void mnuPageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                PageSetupDialog1.Document = PrintDocument1;
                PageSetupDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }






        private void rtbDoc_SelectionChanged(object sender, EventArgs e)
        {
            tbrBold.Checked = rtbDoc.SelectionFont.Bold;
            tbrItalic.Checked = rtbDoc.SelectionFont.Italic;
            tbrUnderline.Checked = rtbDoc.SelectionFont.Underline;
        }




        #endregion

        #region Toolbar Methods








        private void tbrBold_Click(object sender, EventArgs e)
        {
            BoldToolStripMenuItem_Click(this, e);
        }


        private void tbrItalic_Click(object sender, EventArgs e)
        {
            ItalicToolStripMenuItem_Click(this, e);
        }


        private void tbrUnderline_Click(object sender, EventArgs e)
        {
            UnderlineToolStripMenuItem_Click(this, e);
        }


        private void tbrFont_Click(object sender, EventArgs e)
        {
            SelectFontToolStripMenuItem_Click(this, e);
        }


        private void tbrLeft_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }


        private void tbrCenter_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }


        private void tbrRight_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }


        private void tbrFind_Click(object sender, EventArgs e)
        {
            var f = new frmFind(rtbDoc);
            f.Show();
        }


        private void tspColor_Click(object sender, EventArgs e)
        {
            FontColorToolStripMenuItem_Click(this, new EventArgs());
        }




        #endregion

        void rtbDoc_CurrentFileNameChanged(object sender, EventArgs e)
        {

            Text = (rtbDoc.CurrentFileName == null) ?
                "Editor: New Document" : "Editor: " + rtbDoc.CurrentFileName;

            _currentFile = rtbDoc.CurrentFileName;
        }


        #region Printing


        private void PrintDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            _checkPrint = 0;

        }



        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            _checkPrint = rtbDoc.Print(_checkPrint, rtbDoc.TextLength, e);

            e.HasMorePages = _checkPrint < rtbDoc.TextLength;
        }

        #endregion



    }
}
// ReSharper restore LocalizableElement
// ReSharper restore InconsistentNaming