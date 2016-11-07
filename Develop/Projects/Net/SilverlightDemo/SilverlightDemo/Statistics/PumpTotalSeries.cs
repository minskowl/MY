using System.Collections.Generic;
using System.Linq;
using EffectiveSoft.SilverlightDemo.Controls.Windows;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;

namespace EffectiveSoft.SilverlightDemo.Statistics
{


    public class PumpTotalSeries : PumpSeries
    {
        readonly DataSeries series = new DataSeries { RenderAs = RenderAs.Line, LegendText = "Total" };
        /// <summary>
        /// Initializes a new instance of the <see cref="PumpTotalSeries"/> class.
        /// </summary>
        /// <param name="windowsManager">The windows manager.</param>
        /// <param name="chart">The chart.</param>
        /// <param name="pumpNumber">The pump number.</param>
        public PumpTotalSeries(WindowsManager windowsManager, Chart chart, int pumpNumber)
            : base(windowsManager, chart, pumpNumber)
        {
            UIFactory.ChartAddTitle(chart, string.Format("Gas Pump {0}: Total volume of the fueled gas by hour", pumpNumber));
            AddSeries(series);
        }

        /// <summary>
        /// Shows the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Show(IList<FuelingOperation> data)
        {
            var totals =
                   from p in data
                   where p.PumpNumber == pumpNumber
                   group p by p.Hours into g
                   select new { Hour = g.Key, Total = g.Sum(p => p.Litres) };

            foreach (var value in totals)
            {
                DataPoint point = series.DataPoints[value.Hour - 1];
                if (point.YValue != value.Total)
                {
                    point.YValue = value.Total;
                    string.Format(
                        "Gas pump {0} \nFueled: {2}L \nTime: {1} AM",
                        pumpNumber, value.Hour, value.Total);
                }
            }
        }
    }
}
