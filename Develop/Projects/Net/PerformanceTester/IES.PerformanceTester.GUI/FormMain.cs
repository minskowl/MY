using System;
using System.ComponentModel;
using System.Windows.Forms;
using IES.PerformanceTester.Gui.Commands;
using IES.PerformanceTester.Gui.Controls;
using IES.PerformanceTester.Tests;

namespace IES.PerformanceTester.Gui
{
    public partial class FormMain : Form
    {
        private TestRunnersCollection runners = new TestRunnersCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            tabPageMemoryUsage.Visible = AppCore.TraceMemoryUsage;

            loadToolStripMenuItem.BindCommand(new LoadAssemblyCommand());
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            timerRefreshState.Enabled = false;
            runners.Dispose();

            base.OnClosing(e);
        }

        /// <summary>
        /// Handles the Click event of the buttonStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (runners.Count == 0) return;

            buttonStart.Enabled = false;
            if (buttonStart.Text == "Start")
            {

                runners.Start();
                buttonStart.Text = "Stop";
                timerRefreshState.Enabled = true;
            }
            else
            {
                runners.Stop();
                buttonStart.Text = "Start";
                timerRefreshState.Enabled = false;
                RefreshState();
            }

            buttonStart.Enabled = true;
        }




        /// <summary>
        /// Handles the Tick event of the timerRefreshState control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timerRefreshState_Tick(object sender, EventArgs e)
        {
            timerRefreshState.Enabled = false;

            RefreshState();

            timerRefreshState.Enabled = true;
        }

        private void RefreshState()
        {
            foreach (TabPage page in tabControl1.TabPages)
            {
                if (page.Controls[0] is TestRunnerControl)
                {
                    ((TestRunnerControl)page.Controls[0]).RefreshState();
                }
            }
        }

        /// <summary>
        /// Clears the test.
        /// </summary>
        public void ClearTest()
        {
            runners.Stop();
            runners.Clear();
            var index = 0;
            while (tabControl1.TabPages.Count > 1)
            {
                if(tabControl1.TabPages[index].Controls[0] is TestRunnerControl)
                {
                    tabControl1.TabPages.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }

        /// <summary>
        /// Adds the test.
        /// </summary>
        /// <param name="testType">Type of the test.</param>
        public void AddTest(Type testType)
        {
            var searchRunner = new TestRunner(testType, testType.Name);
            runners.Add(searchRunner);

            var settingsControl = CreateSettingTab(testType);
            settingsControl.TestRunner = searchRunner;
        }

        private TestRunnerControl CreateSettingTab(Type testType)
        {
            var settingsControl = new TestRunnerControl
                                      {
                                          Dock = DockStyle.Fill,
                                          Location = new System.Drawing.Point(3, 3),
                                          Name = testType.Name,
                                          Size = new System.Drawing.Size(916, 204),
                                          TabIndex = 0
                                      };

            var page = new TabPage
                           {
                               Location = new System.Drawing.Point(4, 22),
                               Name = testType.Name,
                               Padding = new Padding(3),
                               Size = new System.Drawing.Size(922, 210),
                               TabIndex = 0,
                               Text = testType.Name,
                               UseVisualStyleBackColor = true
                           };

            page.Controls.Add(settingsControl);

            tabControl1.Controls.Add(page);
            return settingsControl;
        }
    }
}