using System;
using System.Windows.Forms;

namespace FileTools.Controls
{
    public partial class FileSelectControl : UserControl
    {
        public string FileName
        {
            get { return textBox1.Text; }
        }
        public FileSelectControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var f= new OpenFileDialog())
            {
                if (f.ShowDialog() == DialogResult.OK)
                    textBox1.Text = f.FileName;
            }
        }
    }
}
