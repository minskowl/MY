using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;

namespace EffectiveSoft.SilverlightDemo.Statistics
{
    public partial class TypeFuelWindow : UserControl
    {
        readonly DataSeries series = new DataSeries { RenderAs = RenderAs.Pie };

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFuelWindow"/> class.
        /// </summary>
        public TypeFuelWindow()
        {
            InitializeComponent();

            UIFactory.ChartSetup(chart);
        }

        /// <summary>
        /// Shows the specified fuel type.
        /// </summary>
        /// <param name="fuelType">Type of the fuel.</param>
        internal void Show(FuelType fuelType)
        {
            var values = from p in StatisiticProcessor.Instance.Data
                         where p.FuelType == fuelType
                         group p by p.PumpNumber into g
                         select new { PumpNumber = g.Key, Total = g.Sum(p => p.Litres) };

            List<GridRow> rows= new List<GridRow>();
            int total=0;
            foreach (var value in values)
            {
                var label = "Gas Pump " + value.PumpNumber;
                series.DataPoints.Add(new DataPoint { YValue = value.Total, LegendText = label });
                rows.Add(new GridRow { Type = label, Litres = value.Total });
                total += value.Total;
            }
            rows.Add(new GridRow { Type = "Total", Litres = total });

            chart.Series.Add(series);
            grid.ItemsSource = rows;
        }

        public class GridRow
        {
            /// <summary>
            /// Gets or sets the time.
            /// </summary>
            /// <value>The time.</value>
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the litres.
            /// </summary>
            /// <value>The litres.</value>
            public int Litres { get; set; }

        }
    }
}
