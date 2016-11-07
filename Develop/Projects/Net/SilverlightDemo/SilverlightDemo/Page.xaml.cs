using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using EffectiveSoft.SilverlightDemo.Controls;
using EffectiveSoft.SilverlightDemo.Controls.Windows;
using EffectiveSoft.SilverlightDemo.Core;
using EffectiveSoft.SilverlightDemo.Statistics;
using Visifire.Charts;
using Visifire.Commons;


namespace EffectiveSoft.SilverlightDemo
{
    public partial class Page : UserControl
    {


        private readonly Dictionary<Chart, SeriesSource> charts = new Dictionary<Chart, SeriesSource>();

        private readonly DispatcherTimer timerStatistics = new DispatcherTimer();
        private WindowsManager windowsManager = null;


        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            InitializeComponent();


            InitializeClock();

            windowsManager = new WindowsManager(LayoutRoot);

            InitializeCharts();

            Loaded += new RoutedEventHandler(Page_Loaded);

            LayoutRoot.Background = Background = new SolidColorBrush(UIFactory.BackgroundColor);

        }

        /// <summary>
        /// Handles the Loaded event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timerStatistics.Interval = new TimeSpan(0, 0, 0, 1);
            timerStatistics.Tick += new EventHandler(timerStatistics_Tick);
            timerStatistics.Start();
        }

        /// <summary>
        /// Initializes the clock.
        /// </summary>
        private void InitializeClock()
        {
            var presenter = new ClockPresenter(this.clock, VirtualClock.Instance);
            presenter.Response += new Callback(this.clock.Update);
            VirtualClock.Instance.Start();
        }

        /// <summary>
        /// Initializes the charts.
        /// </summary>
        private void InitializeCharts()
        {
            AddViewer(new TotalSeries(windowsManager, chartTotal));
            AddViewer(new PumpFuelTypeSeries(windowsManager, chartByFuelType, 0));

            AddViewer(new PumpTotalSeries(windowsManager, chartTotalPump1, 1));
            AddViewer(new PumpTotalSeries(windowsManager, chartTotalPump2, 2));
            AddViewer(new PumpTotalSeries(windowsManager, chartTotalPump3, 3));
            AddViewer(new PumpTotalSeries(windowsManager, chartTotalPump4, 4));

            AddViewer(new PumpFuelTypeSeries(windowsManager, chartByFuelTypePump1, 1));
            AddViewer(new PumpFuelTypeSeries(windowsManager, chartByFuelTypePump2, 2));
            AddViewer(new PumpFuelTypeSeries(windowsManager, chartByFuelTypePump3, 3));
            AddViewer(new PumpFuelTypeSeries(windowsManager, chartByFuelTypePump4, 4));

            foreach (Chart chart in charts.Keys)
            {
                UIFactory.ChartSetup(chart);
            }
        }

        #endregion

        private void AddViewer(SeriesSource series)
        {
            charts.Add(series.Chart, series);
        }
        /// <summary>
        /// Refreshes the specified element collection.
        /// </summary>
        /// <param name="elementCollection">The element collection.</param>
        public void RefreshCharts(UIElementCollection elementCollection)
        {
            List<FuelingOperation> data = StatisiticProcessor.Instance.Data;
            foreach (UIElement element in elementCollection)
            {
                Chart chart = element as Chart;
                if (chart != null && charts.ContainsKey(chart))
                {
                    charts[chart].Show(data);
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the timerStatistics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timerStatistics_Tick(object sender, EventArgs e)
        {
            if (tabs.SelectedContent is StackPanel)
                RefreshCharts(((StackPanel)tabs.SelectedContent).Children);
        }

        /// <summary>
        /// Handles the Click event of the buttonSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            var content = new SettingsControl();
            content.Charts = charts.Keys;


            windowsManager.ShowWindow(
                new Window { Content = content, Caption = "Demo settings ", Width = 300, Opacity = 0.9 },
                new Point(100, 100));
        }




    }
}
