using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Automation.Mining;
using BotvaSpider.Data;
using Savchin.TimeManagment;

namespace BotvaSpider.Controls.Statistics
{
    public partial class MineTotalStatistics : UserControl
    {
        private List<SearchCristalResult> _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MineTotalStatistics"/> class.
        /// </summary>
        public MineTotalStatistics()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _data = ObjectProvider.Instance.GetMineStatistics();
            dateRangeControl1.Value = DateRange.GetThisDayRange();
            ShowStatistics();
        }

        /// <summary>
        /// Shows the statistics.
        /// </summary>
        private void ShowStatistics()
        {
            var range = dateRangeControl1.Value;
            var data = _data.Where(e => range.IsInRange(e.Date)).ToArray();
            var statistics = new MineStatistics(data);
            grid.Clear();
            grid.AddRow("Походов", statistics.Count);
            grid.AddRow("Попыток", statistics.CountAttempt);
            grid.AddRow("Успешных", statistics.CountSuccess);
            grid.AddRow("Ср. успехов", statistics.AvgSuccess);
            grid.AddRow("Ср. %", statistics.AvgPercentage);
            grid.AddRow("Кристалов", statistics.CountCristals);
            grid.AddRow("М. билетов", statistics.CountSmallTicket);
            grid.AddRow("Б. билетов", statistics.CountBigTicket);
            grid.ShowData();
        }

        /// <summary>
        /// Handles the Click event of the buttonShow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonShow_Click(object sender, EventArgs e)
        {
            ShowStatistics();
        }
    }
}
