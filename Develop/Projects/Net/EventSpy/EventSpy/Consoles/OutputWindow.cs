using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Savchin.EventSpy.Core;


namespace Savchin.EventSpy.Consoles
{


    public partial class OutputWindow : ToolWindow, ILog
    {
        private StringBuilder output = new StringBuilder();
        private string test;
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindow"/> class.
        /// </summary>
        public OutputWindow()
        {
            InitializeComponent();

            comboBoxLoggers.Items.Add(Logger.Output);
            comboBoxLoggers.Items.Add(Logger.Test);
            comboBoxLoggers.SelectedItem = Logger.Output;

        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AddMessage(string message)
        {
            textBox1.AppendText(message + Environment.NewLine);
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public void AddMessage(Logger logger, string message)
        {
            if (logger == Logger.Output)
                output.Append(message + Environment.NewLine);
            else
                test = message;
            ChangeText();
        }

        private void ChangeText()
        {
            if ((Logger)comboBoxLoggers.SelectedItem == Logger.Output)
                textBox1.Text = output.ToString();
            else
                textBox1.Text = test;
        }
    }
}