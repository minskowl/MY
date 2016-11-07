using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using IES.PerformanceTester.Core;
using IES.PerformanceTester.Gui;
using log4net;

namespace IntellexerSDK.PerformaceTests.Controls
{
    public partial class MemoryUsageControl : UserControl
    {
        private readonly BindingList<MemoryUsageDataRow> _dataRows = new BindingList<MemoryUsageDataRow>();
        private ILog logTrace = LogManager.GetLogger("MemoryUsage");
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryUsageControl"/> class.
        /// </summary>
        public MemoryUsageControl()
        {
            InitializeComponent();

            if (AppCore.TraceMemoryUsage) InitializeControl();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            this.ParentForm.Closed += ParentForm_Closed;
        }

        private void InitializeControl()
        {
            if (DesignMode) return;

            var procces = Process.GetCurrentProcess();

            AddCounter(new PerformanceCounter("Process", "Private Bytes", procces.ProcessName), DisplayMode.MBytes);
            AddCounter(new PerformanceCounter(".NET CLR Memory", "# Bytes in All Heaps", procces.ProcessName), DisplayMode.MBytes);
            AddCounter(new PerformanceCounter(".NET CLR LocksAndThreads", "# of current logical Threads", procces.ProcessName), DisplayMode.Default);

            dataGridView1.DataSource = _dataRows;

            timerRefreshState.Enabled = true;
        }

        void ParentForm_Closed(object sender, EventArgs e)
        {
            ParentForm.Closed -= ParentForm_Closed;
            foreach (var row in _dataRows)
            {
                row.Dispose();
            }
            _dataRows.Clear();
            timerRefreshState.Enabled = false;
            timerRefreshState.Dispose();
            timerRefreshState = null;
        }



        /// <summary>
        /// Adds the counter.
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <param name="mode">The mode.</param>
        private void AddCounter(PerformanceCounter counter, DisplayMode mode)
        {
            _dataRows.Add(new MemoryUsageDataRow(counter, mode));
        }

        /// <summary>
        /// Refreshes the state.
        /// </summary>
        private void RefreshState()
        {
            foreach (var row in _dataRows)
            {
                row.MakeMeasure();
            }
            RefreshGrid();
        }
        /// <summary>
        /// Handles the Click event of the buttonFreeze control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonFreeze_Click(object sender, EventArgs e)
        {
            foreach (var row in _dataRows)
            {
                row.MakeFreeze();
            }
            RefreshGrid();
        }
        /// <summary>
        /// Handles the Tick event of the timerRefreshState control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timerRefreshState_Tick(object sender, EventArgs e)
        {
            try
            {
                timerRefreshState.Enabled = false;
                RefreshState();
            }
            finally
            {
                timerRefreshState.Enabled = true;
            }
        }
        private void RefreshGrid()
        {

            _dataRows.ResetItem(0);
            _dataRows.ResetItem(1);
            _dataRows.ResetItem(2);

            logTrace.Info(string.Format("{0};{1};{2}",
                _dataRows[0].Current, _dataRows[1].Current, _dataRows[2].Current));
        }


    }
}
