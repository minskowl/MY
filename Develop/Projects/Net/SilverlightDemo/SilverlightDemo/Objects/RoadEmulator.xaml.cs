using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using EffectiveSoft.SilverlightDemo.Core;

namespace EffectiveSoft.SilverlightDemo.Objects
{
    public partial class RoadEmulator : UserControl
    {

        private readonly DispatcherTimer timerStartMovement = new DispatcherTimer();
        private readonly List<Car> cars = new List<Car>();


        #region Properties


        public static readonly DependencyProperty MovementStartProperty =
      DependencyProperty.Register(
      "MovementStart", typeof(Boolean),
      typeof(RoadEmulator), null
  );


        /// <summary>
        /// Gets or sets a value indicating whether [movement start].
        /// </summary>
        /// <value><c>true</c> if [movement start]; otherwise, <c>false</c>.</value>
        public Boolean MovementStart
        {
            get { return (Boolean)GetValue(MovementStartProperty); }
            set
            {

                SetValue(MovementStartProperty, value);
                if (value)
                {
                    if (!timerStartMovement.IsEnabled)
                        timerStartMovement.Start();
                }
                else
                {
                    if (timerStartMovement.IsEnabled)
                        timerStartMovement.Stop();
                }
            }
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="RoadEmulator"/> class.
        /// </summary>
        public RoadEmulator()
        {
            InitializeComponent();

            CreateTours();

            timerStartMovement.Interval = GetStartInterval();
            timerStartMovement.Tick += new EventHandler(timerStartMovement_Tick);

            Loaded += new RoutedEventHandler(RoadEmulator_Loaded);
         

        }

        /// <summary>
        /// Handles the Loaded event of the RoadEmulator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void RoadEmulator_Loaded(object sender, RoutedEventArgs e)
        {
            timerStartMovement.Start();
        }

        private void CreateTours()
        {


            cars.Add(CarTourFactory.CreateLeftDirectTour());
            cars.Add(CarTourFactory.CreateLeftPumpTopTour());
            cars.Add(CarTourFactory.CreateLeftPumpBottomTour());
            cars.Add(CarTourFactory.CreateLeftDirectTour());

            cars.Add(CarTourFactory.CreateRightDirectTour());
            cars.Add(CarTourFactory.CreateRightPumpBottomTour());
            cars.Add(CarTourFactory.CreateRightPumpTopTour());
            cars.Add(CarTourFactory.CreateRightDirectTour());

            var leftCheck = new[] { cars[0], cars[1], cars[2], cars[3] };
            var rightCheck = new[] { cars[4], cars[5], cars[6], cars[7] };

            cars[0].CheckCars = leftCheck;
            cars[1].CheckCars = leftCheck;
            cars[2].CheckCars = leftCheck;
            cars[3].CheckCars = leftCheck;

            
            cars[4].CheckCars = rightCheck;
            cars[5].CheckCars = rightCheck;
            cars[6].CheckCars = rightCheck;
            cars[7].CheckCars = rightCheck;

            short pumpNumber = 1;
            for (int i = 0; i < cars.Count; i++)
            {
                
                string carName = "Car" + (i + 1);
                var car = cars[i];
                
                car.Name = carName;
                LayoutRoot.Children.Add(car);
                if (car.GasPump!=null)
                {
                    car.GasPump.Number = pumpNumber;
                    pumpNumber++;
                    LayoutRoot.Children.Add(car.GasPump);
                }

                comboPathes.Items.Add(carName);
            }


        }


        void timerStartMovement_Tick(object sender, EventArgs e)
        {
            StartMovement();
        }

        private TimeSpan GetStartInterval()
        {
            return new TimeSpan(0, 0, 0, Randomizer.GetIntegerBetween(2, 6));
        }

        /// <summary>
        /// Starts the movement.
        /// </summary>
        public void StartMovement()
        {
            timerStartMovement.Stop();
            Car car = cars[Randomizer.GetIntegerBetween(0, cars.Count)];
            if (!car.IsInroute)
                car.Go();

            timerStartMovement.Interval = GetStartInterval();
            timerStartMovement.Start();
        }

        /// <summary>
        /// Handles the OnClick event of the ButtonStartAnimation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonStartAnimation_OnClick(object sender, RoutedEventArgs e)
        {
            MovementStart = !MovementStart;
        }

        /// <summary>
        /// Handles the OnClick event of the ButtonGo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonGo_OnClick(object sender, RoutedEventArgs e)
        {
            string carName = (string) comboPathes.SelectedItem;
            //Storyboard storyboard = (Storyboard)Resources[comboPathes.SelectedItem];
            //storyboard.Begin();
            foreach (var car in cars)
            {
                if(car.Name==carName)
                    car.Go();

            }
            
        }
    }
}
