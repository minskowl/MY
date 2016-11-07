using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Automation.Mining;
using BotvaSpider.Data;
using Savchin.Collection.Generic;
using Savchin.Forms.Helpers;
using Savchin.TimeManagment;

namespace BotvaSpider.Controls.Statistics
{
    public partial class MineCristalDistributionControl : UserControl
    {
        private List<SearchCristalResult> _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MineCristalDistributionControl"/> class.
        /// </summary>
        public MineCristalDistributionControl()
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
            comboBoxStep.Items.Add(2);
            comboBoxStep.Items.Add(5);
            comboBoxStep.Items.Add(10);
            comboBoxStep.Items.Add(20);
            comboBoxStep.Items.Add(30);
            comboBoxStep.Items.Add(60);
            comboBoxStep.SelectedIndex = 0;

            boxIntervalType.Setup(typeof(IntervalType));
            boxIntervalType.SelectedIndex = 0;

            _data = ObjectProvider.Instance.GetMineStatistics();
            dateRangeControl1.Value = new DateRange(
                _data.Min(d => d.Date).Round(TimePrecision.WithHours),
                _data.Max(d => d.Date).Round(TimePrecision.WithHours));
            ShowStatistics();
        }

        private DateTime minValue;
        /// <summary>
        /// Shows the statistics.
        /// </summary>
        private void ShowStatistics()
        {
            SearchCristalResult[] data = GetData();
            var intervalType = (IntervalType)((EnumData)boxIntervalType.SelectedItem).Value;
            var endValue = intervalType == IntervalType.Percentage ? 100 : 1440;
            var step = (int)comboBoxStep.SelectedItem;


            var result = new List<MineStatistics>();
            minValue = data.Min(e => e.Date).Round(TimePrecision.WithHours);

            for (var i = 0; i < endValue; i += step)
            {
                var d = intervalType == IntervalType.Percentage ?
                    GetPercentageInterval(data, i, (i + step)) :
                    GetTimeInterval(data, i, (i + step));

                result.Add(d);
            }
            grid.DataSource = new SortableBindingList<MineStatistics>(result);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns></returns>
        private SearchCristalResult[] GetData()
        {
            var range = dateRangeControl1.Value;
            var query = _data.Where(e => range.IsInRange(e.Date));
            if (boxBigTicket.Checked)
                query = query.Where(e => e.BigTicket);

            return query.ToArray();
        }

        private MineStatistics GetTimeInterval(IEnumerable<SearchCristalResult> data, int start, int end)
        {
            var startTime = minValue.AddMinutes(start).ToTime(TimePrecision.WithMinutes);
            var endTime = minValue.AddMinutes(end).ToTime(TimePrecision.WithMinutes);

            var range = new TimeRange(startTime, endTime);
            var result = new MineStatistics(data.Where(e => range.IsInRange(e.Date)).ToArray());
            result.Interval = range.ToString();
            return result;
        }
        private MineStatistics GetPercentageInterval(IEnumerable<SearchCristalResult> data, int startPerc, int endPerc)
        {
            var result = new MineStatistics(data.Where(e => e.Percentage >= startPerc && e.Percentage <= endPerc).ToArray());
            result.Interval = startPerc + " - " + endPerc;
            return result;
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

        public enum IntervalType
        {
            Percentage,
            Time
        }
    }
}
