using System;
using System.Drawing;
using System.Windows.Forms;
using Savchin.Drawing;

namespace ControlTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private void HSBCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(HSBCheckBox.Checked)
            {
                colorPicker1.Mode = ColorSheme.HSB;
                
            }
            else
            {
                colorPicker1.Mode = ColorSheme.RGB;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorPicker1.Value = Color.FromArgb(colorPicker1.Value.R + 10,
                                                colorPicker1.Value.G, 
                                                colorPicker1.Value.B);
        }
    }
}