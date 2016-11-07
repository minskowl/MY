using System;
using System.Windows.Forms;
using Savchin.Drawing;

namespace ColorPicker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetValueFromClipBoard();
        }

        #region Menu Handlers
        private void pasteHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetValueFromClipBoard();
        }

        private void copyHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ConverterColor.ToHTMLColor(colorPicker1.Value));
        }

        private void rGBToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            hSBToolStripMenuItem.Checked = !rGBToolStripMenuItem.Checked;

        }

        private void hSBToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            rGBToolStripMenuItem.Checked = !hSBToolStripMenuItem.Checked;
            SetMode();
        } 
        #endregion
        
        
        private void SetMode()
        {
            if (rGBToolStripMenuItem.Checked)
                colorPicker1.Mode = ColorSheme.RGB;
            else
                colorPicker1.Mode = ColorSheme.HSB;
        }


        private void SetValueFromClipBoard()
        {
            string text = Clipboard.GetText();
            if (string.IsNullOrEmpty(text))
                return;

            try
            {
                colorPicker1.Value = ConverterColor.ToColor(text);
            }
            catch
            {

            }
        }

    }
}