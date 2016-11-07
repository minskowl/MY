using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using EffectiveSoft.SilverlightDemo.Core;

namespace EffectiveSoft.SilverlightDemo.Objects
{
    public partial class Car : UserControl
    {

        private const string StoryboardStartKey = "StartRoute";
        private const string StoryboardHomeKey = "HomeRoute";
        private const int missOffset = 100;

        private static readonly BitmapImage[] carSkins = new[] {
            new BitmapImage(new Uri("car1.png", UriKind.Relative)),
            new BitmapImage(new Uri("redCar.png", UriKind.Relative)),
            new BitmapImage(new Uri("car2.png", UriKind.Relative)),
            new BitmapImage(new Uri("blueCar.png", UriKind.Relative)),
            new BitmapImage(new Uri("car3.png", UriKind.Relative)),
            new BitmapImage(new Uri("greenCar.png", UriKind.Relative)),
            new BitmapImage(new Uri("car4.png", UriKind.Relative)), 
            new BitmapImage(new Uri("car5.png", UriKind.Relative)),                                
            new BitmapImage(new Uri("car6.png", UriKind.Relative)),   
                                                    };

        private readonly DispatcherTimer timerStartMovement = new DispatcherTimer();

        public enum Direction
        {
            Top,
            Bottom
        }

        #region Properties

        /// <summary>
        /// Gets or sets the check cars.
        /// </summary>
        /// <value>The check cars.</value>
        public Car[] CheckCars { get; set; }

        private bool isInroute = false;
        /// <summary>
        /// Gets a value indicating whether this instance is inroute.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is inroute; otherwise, <c>false</c>.
        /// </value>
        public bool IsInroute
        {
            get { return isInroute; }
        }

        public static readonly DependencyProperty DirectionProperty =
  DependencyProperty.Register(
  "MovementDirection", typeof(Direction),
  typeof(Car), null
);



        /// <summary>
        /// Gets or sets the movement direction.
        /// </summary>
        /// <value>The movement direction.</value>
        public Direction MovementDirection
        {
            get { return (Direction)GetValue(DirectionProperty); }
            set
            {
                ((RotateTransform)Skin.RenderTransform).Angle = value == Direction.Bottom ? 0 : 180;
                SetValue(DirectionProperty, value);

            }
        }
        private GasPump gasPump;
        /// <summary>
        /// Gets or sets the gas pump.
        /// </summary>
        /// <value>The gas pump.</value>
        public GasPump GasPump
        {
            get { return gasPump; }
            set { gasPump = value; }
        }



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        public Car()
        {
            InitializeComponent();

            var transformGroup = new TransformGroup();
            var rotateTransform = new RotateTransform();
            transformGroup.Children.Add(rotateTransform);
            RenderTransform = transformGroup;

            timerStartMovement.Tick += new EventHandler(timerStartMovement_Tick);
            timerStartMovement.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        /// <summary>
        /// Handles the Tick event of the timerStartMovement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timerStartMovement_Tick(object sender, EventArgs e)
        {
            GoHome();
        }

        /// <summary>
        /// Goes this instance.
        /// </summary>
        public void Go()
        {
            Skin.Source = carSkins[Randomizer.GetIntegerBetween(0, carSkins.Length)];
            ((Storyboard)Resources[StoryboardStartKey]).Begin();
            isInroute = true;
        }

        /// <summary>
        /// Goes the home.
        /// </summary>
        public void GoHome()
        {
            if (CanStart)
            {
                if (timerStartMovement.IsEnabled)
                    timerStartMovement.Stop();
                ((Storyboard)Resources[StoryboardHomeKey]).Begin();
            }
            else
            {
                if (!timerStartMovement.IsEnabled)
                    timerStartMovement.Start();

            }
        }
        private bool CanStart
        {
            get
            {
                double topMe = Canvas.GetTop(this);
                foreach (var car in CheckCars)
                {
                    if (car.IsInroute && !car.Equals(this))
                    {
                        var top = Canvas.GetTop(car);

                        if (
                            (MovementDirection == Direction.Bottom && top <= topMe && top >= topMe - missOffset) ||
                            (MovementDirection == Direction.Top && top <= topMe + missOffset && top >= topMe)
                            )
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Sets the start story board.
        /// </summary>
        /// <param name="storyboard">The storyboard.</param>
        internal void SetStartStoryBoard(Storyboard storyboard)
        {
            Resources.Add(StoryboardStartKey, storyboard);
        }

        /// <summary>
        /// Sets the home story board.
        /// </summary>
        /// <param name="storyboard">The storyboard.</param>
        internal void SetHomeStoryBoard(Storyboard storyboard)
        {
            Resources.Add(StoryboardHomeKey, storyboard);
        }

        /// <summary>
        /// Called when [arrive gas pump].
        /// </summary>
        public void OnArriveGasPump(object sender, EventArgs e)
        {
            gasPump.FillFuelTank(this);
        }

        public void OnRouteCompleted(object sender, EventArgs e)
        {
            isInroute = false;
        }
    }
}
