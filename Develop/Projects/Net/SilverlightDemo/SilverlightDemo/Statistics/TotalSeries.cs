using System;
using System.Collections.Generic;
using System.Linq;
using EffectiveSoft.SilverlightDemo.Controls.Windows;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;

namespace EffectiveSoft.SilverlightDemo.Statistics
{
    public class TotalSeries : SeriesSource
    {
        readonly DataSeries seriesTotal = new DataSeries { RenderAs = RenderAs.Line, LegendText = "Total" };

        DataSeries[] seriesPump = new DataSeries[4] { 
            new DataSeries { RenderAs = RenderAs.Line, LegendText = "Pump 1"} ,
            new DataSeries { RenderAs = RenderAs.Line, LegendText = "Pump 2" },
            new DataSeries { RenderAs = RenderAs.Line, LegendText = "Pump 3" },
            new DataSeries { RenderAs = RenderAs.Line, LegendText = "Pump 4" }};

        /// <summary>
        /// Initializes a new instance of the <see cref="TotalSeries"/> class.
        /// </summary>
        /// <param name="windowsManager"></param>
        /// <param name="chart">The chart.</param>
        public TotalSeries(WindowsManager windowsManager, Chart chart)
            : base(windowsManager, chart)
        {
            UIFactory.ChartAddTitle(chart, "Gas Station: Total volume of the fueled gas by hour");

            AddSeries(seriesTotal);

            foreach (var series in seriesPump)
                AddSeries(series);


        }

        /// <summary>
        /// Shows the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Show(IList<FuelingOperation> data)
        {


            var values =
                   from p in data
                   group p by p.Hours into g
                   select new { Hour = g.Key, Total = g.Sum(p => p.Litres) };

            foreach (var value in values)
            {
                DataPoint point = seriesTotal.DataPoints[value.Hour - 1];
                if (point.YValue != value.Total)
                {
                    point.YValue = value.Total;
                    point.ToolTipText = string.Format(
                               "All pumps \nFueled: {0}L \nTime: {1} AM",
                                 value.Total, value.Hour);
                }
            }

            var pumpNumber = 1;
            foreach (var series in seriesPump)
            {
                var pump =
          from p in data
          where p.PumpNumber == pumpNumber
          group p by p.Hours into g
          select new { Hour = g.Key, Total = g.Sum(p => p.Litres) };

                foreach (var value in pump)
                {
                    DataPoint point = series.DataPoints[value.Hour - 1];
                    if (point.YValue != value.Total)
                    {
                        point.YValue = value.Total;
                        point.ToolTipText = string.Format(
                               "Gas pump {2}\nFueled: {0}L \nTime: {1} AM",
                                 value.Total, value.Hour, pumpNumber);
                    }

                }
                pumpNumber++;
            }


        }
    }
}
