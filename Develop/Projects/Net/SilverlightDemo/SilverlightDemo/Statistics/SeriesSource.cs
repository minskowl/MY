using System.Collections.Generic;
using System.Windows;
using EffectiveSoft.SilverlightDemo.Controls.Windows;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;
using Visifire.Commons;

namespace EffectiveSoft.SilverlightDemo.Statistics
{
    public abstract class SeriesSource 
    {
        protected readonly WindowsManager windowsManager;
        private readonly Chart chart;
        /// <summary>
        /// Gets the chart.
        /// </summary>
        /// <value>The chart.</value>
        public Chart Chart
        {
            get { return chart; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesSource"/> class.
        /// </summary>
        /// <param name="windowsManager">The windows manager.</param>
        /// <param name="chart">The chart.</param>
        protected SeriesSource(WindowsManager windowsManager, Chart chart)
        {
            this.chart = chart;
            this.windowsManager = windowsManager;

            UIFactory.ChartSetup(chart);
        }



        /// <summary>
        /// Adds the series.
        /// </summary>
        /// <param name="series">The series.</param>
        protected void AddSeries(DataSeries series)
        {
            for (int i = 0; i < 12; i++)
            {
                series.DataPoints.Add(new DataPoint { YValue = 0 });
            }
            chart.Series.Add(series);
        }

        public abstract void Show(IList<FuelingOperation> data);
    }
}
