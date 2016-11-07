using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Extensions.Forms.Recorder;

namespace Savchin.EventSpy.Consoles
{
    public partial class TestGeneratorWindow : ToolWindow
    {
        private Form testedForm;
        private TestWriter writer;
        /// <summary>
        /// Gets or sets the tested form.
        /// </summary>
        /// <value>The tested form.</value>
        public Form TestedForm
        {
            get { return testedForm; }
            set
            {
                if (writer != null)
                {
                    UnBind();
                }

                if (value != null)
                {
                    testedForm = value;
                    writer = new TestWriter(value);
                    writer.TestChanged += TestChanged;
                }
            }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="TestGeneratorWindow"/> class.
        /// </summary>
        public TestGeneratorWindow()
        {
            InitializeComponent();

            this.HideOnClose = true;
            this.ShowHint = Savchin.Forms.Docking.DockState.DockBottomAutoHide;
        }
        private void UnBind()
        {
            testedForm = null;
            writer.TestChanged += TestChanged;
            writer = null;
        }

        public void TestChanged(object sender, EventArgs e)
        {
            textBox1.Text = writer.Test;
        }
    }
}
