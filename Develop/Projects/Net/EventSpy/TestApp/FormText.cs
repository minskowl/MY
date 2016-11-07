using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class FormText : Form
    {
        public FormText()
        {
            InitializeComponent();
        }

        public void ShowText(string text)
        {
            textBox1.Text = text;
            Show();
        }
    }
}
