using EffectiveSoft.SilverlightDemo.Controls.Windows;
using Visifire.Charts;

namespace EffectiveSoft.SilverlightDemo.Statistics
{
    public abstract class PumpSeries : SeriesSource
    {
        protected int pumpNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="PumpTotalSeries"/> class.
        /// </summary>
        /// <param name="windowsManager">The windows manager.</param>
        /// <param name="chart">The chart.</param>
        /// <param name="pumpNumber">The pump number.</param>
        protected PumpSeries(WindowsManager windowsManager, Chart chart, int pumpNumber)
            : base(windowsManager, chart)
        {
            this.pumpNumber = pumpNumber;
        }
    }
}
