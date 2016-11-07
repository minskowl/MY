using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using EffectiveSoft.SilverlightDemo.Core;
using EffectiveSoft.SilverlightDemo.Statistics;

namespace EffectiveSoft.SilverlightDemo.Objects
{
    public partial class GasPump : UserControl
    {

        public const int LeftX = 145;
        public const int RightX = 175;

        private readonly DispatcherTimer fillTimer = new DispatcherTimer();

        private int litersCounter = 0;
        private FuelingOperation currenOperation;
        private Car car;


        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public short Number { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="GasPump"/> class.
        /// </summary>
        public GasPump()
        {
            InitializeComponent();

            LayoutRoot.Visibility = System.Windows.Visibility.Collapsed;

            fillTimer.Tick += new System.EventHandler(fillTimer_Tick);
            fillTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            fillTimer.Stop();
        }

        void fillTimer_Tick(object sender, System.EventArgs e)
        {
            litersCounter++;
            if (litersCounter > currenOperation.Litres)
            {
                fillTimer.Stop();
                StatisiticProcessor.Instance.AddOperation(currenOperation);
                currenOperation = null;
                car.GoHome();
                LayoutRoot.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                labelCounter.Text = litersCounter.ToString("0000");
            }

        }

        /// <summary>
        /// Fills the fuel tank.
        /// </summary>
        /// <param name="car">The car.</param>
        public void FillFuelTank(Car car)
        {
            litersCounter = 0;
            this.car = car;

            currenOperation = new FuelingOperation();
            currenOperation.Litres = (short)Randomizer.GetIntegerBetween(20, 50);
            currenOperation.PumpNumber = Number;
            currenOperation.FuelType = (FuelType)Randomizer.GetIntegerBetween(0, 3);

            var brush = (Brush) Resources["brush" + currenOperation.FuelType];
            labelBackground.Fill = brush;
            hobot.Fill = brush;

            labelFuelType.Text = currenOperation.FuelType.ToString();
            labelCounter.Text = litersCounter.ToString("0000");
            LayoutRoot.Visibility = System.Windows.Visibility.Visible;


            fillTimer.Start();




        }
    }
}
