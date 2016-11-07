﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileTools.Controls
{
    public partial class FormText : Form
    {
        /// <summary>
        /// Gets or sets the edit text.
        /// </summary>
        /// <value>The edit text.</value>
        public string EditText
        {
            get { return textBox1.Text; }
            set {  textBox1.Text=value; }
        }

        public FormText()
        {
            InitializeComponent();
        }
    }
}
