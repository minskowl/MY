using System.Linq;
using System.Windows.Controls;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;

namespace EffectiveSoft.SilverlightDemo.Statistics
{
    public partial class HourWindow : UserControl
    {
        readonly DataSeries series = new DataSeries { RenderAs = RenderAs.Pie };

        public HourWindow()
        {
            InitializeComponent();

            UIFactory.ChartSetup(chart);
        }

        /// <summary>
        /// Shows the specified hours.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <param name="pumpNumber">The pump number.</param>
        public void Show(int hours, int pumpNumber)
        {
            FillChart(hours,  pumpNumber);

            var gridValues =
              (from p in StatisiticProcessor.Instance.Data
               where p.Hours == hours && (pumpNumber == 0 || p.PumpNumber == pumpNumber)
               select new GridRow { Time = p.Hours + ":" + p.Minutes, GasType = p.FuelType, Litres = p.Litres }).ToArray();

            grid.ItemsSource = gridValues;

        }

        private void FillChart(int hours, int pumpNumber)
        {
            var values =
                from p in StatisiticProcessor.Instance.Data
                where p.Hours == hours && (pumpNumber == 0 || p.PumpNumber == pumpNumber)
                group p by p.FuelType into g
                                          select new { Type = g.Key, Total = g.Sum(p => p.Litres) };

            foreach (var value in values)
            {
                series.DataPoints.Add(new DataPoint { YValue = value.Total, LegendText = value.Type.ToString() });
            }
            chart.Series.Add(series);
        }

        /// <summary>
        /// class GridRow
        /// </summary>
        public class GridRow
        {
            /// <summary>
            /// Gets or sets the time.
            /// </summary>
            /// <value>The time.</value>
            public string Time { get; set; }
            /// <summary>
            /// Gets or sets the type of the gas.
            /// </summary>
            /// <value>The type of the gas.</value>
            public FuelType GasType { get; set; }
            /// <summary>
            /// Gets or sets the litres.
            /// </summary>
            /// <value>The litres.</value>
            public int Litres { get; set; }

        }
    }
}
