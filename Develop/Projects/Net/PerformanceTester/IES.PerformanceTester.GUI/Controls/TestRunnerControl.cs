using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using IES.PerformanceTester.Tests;


namespace IES.PerformanceTester.Gui.Controls
{
    /// <summary>
    /// TestRunner
    /// </summary>
    public partial class TestRunnerControl : UserControl
    {
        private ITestRunner _testRunner;

        /// <summary>
        /// Gets or sets the test runner.
        /// </summary>
        /// <value>The test runner.</value>
        public ITestRunner TestRunner
        {
            get { return _testRunner; }
            set
            {
                if (_testRunner == value) return;

                _testRunner = value;

                if (_testRunner == null) return;
                _testRunner.Enabled = TestEnabled;
                _testRunner.ThreadCount = ThreadCount;
            }
        }


        /// <summary>
        /// Gets or sets the thread count.
        /// </summary>
        /// <value>The thread count.</value>
        public int ThreadCount
        {
            get { return (int)boxThreadCount.Value; }
            set { boxThreadCount.Value = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [test enabled].
        /// </summary>
        /// <value><c>true</c> if [test enabled]; otherwise, <c>false</c>.</value>
        public bool TestEnabled
        {
            get { return boxEnabled.Checked; }
            set { boxEnabled.Checked = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunnerControl"/> class.
        /// </summary>
        public TestRunnerControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Refreshes the state.
        /// </summary>
        public void RefreshState()
        {
            ComputeStatistics(TestRunner.Tests);
        }

        /// <summary>
        /// Handles the ValueChanged event of the boxThreadCount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxThreadCount_ValueChanged(object sender, EventArgs e)
        {
            if (TestRunner != null) TestRunner.ThreadCount = ThreadCount;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the boxEnabled control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (TestRunner != null) TestRunner.Enabled = TestEnabled;
        }



        private void ComputeStatistics(IEnumerable<TestBase> tests)
        {
            labelAttempts.Text = tests.Sum(e => e.CountersAttempts).ToString();
            labelErrors.Text = tests.Sum(e => e.CounterErrors).ToString();
            labelThreads.Text = tests.Count().ToString();
            labelRunned.Text = tests.Where(e => e.Thread.ThreadState == ThreadState.Running).Count().ToString();
        }
    }
}