using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EffectiveSoft.SilverlightDemo.Controls.Windows;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;

namespace EffectiveSoft.SilverlightDemo.Statistics
{
    public class PumpFuelTypeSeries : PumpSeries
    {
        DataSeries[] serieses = FuelTypeInfo.CreateAllSeries();

        /// <summary>
        /// Initializes a new instance of the <see cref="PumpFuelTypeSeries"/> class.
        /// </summary>
        /// <param name="windowsManager">The windows manager.</param>
        /// <param name="chart">The chart.</param>
        /// <param name="pumpNumber">The pump number.</param>
        public PumpFuelTypeSeries(WindowsManager windowsManager, Chart chart, int pumpNumber)
            : base(windowsManager, chart, pumpNumber)
        {
            foreach (var series in serieses)
            {
                for (int i = 0; i < 12; i++)
                {
                    var point = new DataPoint { YValue = 0 };
                    point.MouseLeftButtonUp += new MouseButtonEventHandler(point_MouseLeftButtonUp);
                    series.DataPoints.Add(point);
                }
                chart.Series.Add(series);
            }
            Legend legend = UIFactory.CreateLegend("legend");
            legend.MouseLeftButtonUp += new MouseButtonEventHandler(legend_MouseLeftButtonUp);
            chart.Legends.Add(legend);

        }



        /// <summary>
        /// Handles the MouseLeftButtonUp event of the legend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        void legend_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(Chart);

            FuelType fuelType;
            if(point.X>120 && point.X<169)
            {
                fuelType = FuelType.Regular;
            }
            else if(point.X>175 && point.X<230)
            {
                fuelType = FuelType.Premium;
            }
            else if (point.X > 240 && point.X < 280)
            {
                fuelType = FuelType.Premium;
            }
            else
            {
                return;
            }
           

            var content = new TypeFuelWindow();
            content.Show(fuelType);

            windowsManager.ShowWindow(
                CreateWindow(string.Format("{0} gas statistic", fuelType.ToString()), content),
                new Point(100, 100));
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the point control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        void point_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var point = (DataPoint)sender;
            var content = new HourWindow();
            content.Show((int)point.XValue, pumpNumber);


            windowsManager.ShowWindow(
                CreateWindow(string.Format("Detailed statistics at {0} AM ", point.XValue), content)
                , new Point(100, 100));
        }
        private Window CreateWindow(string caption, object content)
        {
            return new Window { Content = content, Caption = caption, Width = 600, Opacity = 0.9 };
        }

        /// <summary>
        /// Shows the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Show(IList<FuelingOperation> data)
        {
            var fuelType = FuelType.Regular;
            foreach (var series in serieses)
            {
                FillSeries(series, data, fuelType);
                fuelType++;
            }
        }

        private void FillSeries(DataSeries series, IList<FuelingOperation> data, FuelType fuelType)
        {
            var values =
                from p in data
                where p.FuelType == fuelType && (pumpNumber == 0 || p.PumpNumber == pumpNumber)
                group p by p.Hours into g
                select new { Hour = g.Key, Total = g.Sum(p => p.Litres) };

            foreach (var value in values)
            {
                var point = series.DataPoints[value.Hour - 1];
                if (point.YValue != value.Total)
                {
                    point.YValue = value.Total;
                    point.ToolTipText = GetTooltipText(fuelType, value.Hour, value.Total);
                }
            }
        }

        private string GetTooltipText(FuelType fuelType, int hour, int litres)
        {
            return pumpNumber == 0
                       ?
                           string.Format(
                               "All Pumps \nGas Type: {0}\nFueled: {2}L \nTime: {1} AM\nClick bar for the details.",
                               fuelType, hour, litres)
                       :
                           string.Format(
                               "Gas pump {0} \nGas Type: {1}\nFueled: {3}L \nTime: {2} AM\nClick bar for the details.",
                               pumpNumber, fuelType, hour, litres);
        }
    }
}
