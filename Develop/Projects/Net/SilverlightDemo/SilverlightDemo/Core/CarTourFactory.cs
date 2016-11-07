using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using EffectiveSoft.SilverlightDemo.Objects;

namespace EffectiveSoft.SilverlightDemo.Core
{
    public static class CarTourFactory
    {

        private static readonly Duration directPathDuration = new Duration(TimeSpan.FromSeconds(4));

        private static readonly PropertyPath PropertyPathX = new PropertyPath("(Canvas.Left)");
        private static readonly PropertyPath PropertyPathY = new PropertyPath("(Canvas.Top)");

        private static PropertyPath PropertyPathAngle =
            new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)");
        
        #region TimeSets
        private static readonly TimeSet timeSetFrom1 = new TimeSet
        {
            Time0 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0)),
            Time1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1)),
            Time2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1, 500)),
            Time3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 3))
        };

        private static readonly TimeSet timeSetFrom2 = new TimeSet
        {
            Time0 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0)),
            Time1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1, 500)),
            Time2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2)),
            Time3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2, 500))
        };

        private static readonly TimeSet timeSetTo1 = new TimeSet
        {
            Time0 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0)),
            Time1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 500)),
            Time2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1)),
            Time3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2))
        };
        private static readonly TimeSet timeSetTo2 = new TimeSet
        {
            Time0 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0)),
            Time1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1, 500)),
            Time2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2)),
            Time3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 3))
        };
        #endregion

        #region Coordinates
        private const int LeftX0 = 40;
        private const int LeftX1 = 100;
        private const int LeftX2 = 110;

        private const int RightX0 = 270;
        private const int RightX1 = 280;
        private const int RightX2 = 340;



        private const int PumpBottomY = 275;
        private const int PumpTopY = 120;
        private const int BottomY = 460;
        private const int TopY = -35;

        private const int turnOffset = 90;
        #endregion

        #region Car Factories
        /// <summary>
        /// Creates the top car.
        /// </summary>
        /// <returns></returns>
        private static Car CreateTopCar()
        {
            return CreateCar(Car.Direction.Top);
        }

        /// <summary>
        /// Creates the bottom car.
        /// </summary>
        /// <returns></returns>
        private static Car CreateBottomCar()
        {
            return CreateCar(Car.Direction.Bottom);
        }
        /// <summary>
        /// Creates the car.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        private static Car CreateCar(Car.Direction direction)
        {
            var car = new Car { MovementDirection = direction };

            if (direction == Car.Direction.Top)
            {
                Canvas.SetLeft(car, RightX2);
                Canvas.SetTop(car, BottomY);
            }
            else
            {
                Canvas.SetLeft(car, LeftX0);
                Canvas.SetTop(car, TopY);
            }

            return car;
        }

        #endregion

        #region Left Tours
        /// <summary>
        /// Creates the left direct tour.
        /// </summary>
        /// <returns></returns>
        public static Car CreateLeftDirectTour()
        {
            return CreateDirectTour(TopY, BottomY, CreateBottomCar());
        }

        /// <summary>
        /// Creates the left pump top tour.
        /// </summary>
        /// <returns></returns>
        public static Car CreateLeftPumpTopTour()
        {
            var car = CreateBottomCar();
            var pump = new GasPump();
            Canvas.SetLeft(pump, GasPump.LeftX);
            Canvas.SetTop(pump, 95);
            car.GasPump = pump;

            CreatStoryBoardToPump(
                car, 
                timeSetTo1, 
                TopY, 
                PumpTopY - turnOffset, 
                PumpTopY, 
                LeftX0, 
                LeftX2);

            CreatStoryBoardFromPump(
                car, 
                timeSetFrom1, 
                PumpTopY, 
                PumpTopY + turnOffset,
                BottomY,
                LeftX0,
                LeftX2);

            return car;
        }

        /// <summary>
        /// Creates the left pump bottom tour.
        /// </summary>
        /// <returns></returns>
        public static Car CreateLeftPumpBottomTour()
        {
            var car = CreateBottomCar();
            var pump = new GasPump();
            Canvas.SetLeft(pump, GasPump.LeftX);
            Canvas.SetTop(pump, 240);
            car.GasPump = pump;

            CreatStoryBoardToPump(
                car, 
                timeSetTo2, 
                TopY, 
                PumpBottomY - turnOffset, 
                PumpBottomY, 
                LeftX0, 
                LeftX2);

            CreatStoryBoardFromPump(
                car, 
                timeSetFrom2, 
                PumpBottomY, 
                PumpBottomY + turnOffset,
                BottomY,
                LeftX0,
                LeftX2);

            return car;
        }



        #endregion

        #region Right Tours
        /// <summary>
        /// Creates the right direct tour.
        /// </summary>
        /// <returns></returns>
        public static Car CreateRightDirectTour()
        {
            return CreateDirectTour(BottomY, TopY, CreateTopCar());
        }

        /// <summary>
        /// Creates the right pump top tour.
        /// </summary>
        /// <returns></returns>
        public static Car CreateRightPumpTopTour()
        {
            var car = CreateTopCar();
            var pump = new GasPump();
            Canvas.SetLeft(pump, GasPump.RightX);
            Canvas.SetTop(pump, 130);
            car.GasPump = pump;

            CreatStoryBoardToPump(
                car,
                timeSetTo2,
                BottomY,
                PumpTopY + turnOffset,
                PumpTopY,
                RightX2,
                RightX0);

            CreatStoryBoardFromPump(
                car,
                timeSetFrom2,
                PumpTopY,
                PumpTopY - turnOffset,
                TopY,
                RightX2,
                RightX0);

            return car;
        }


        public static Car CreateRightPumpBottomTour()
        {
            var car = CreateTopCar();
            var pump = new GasPump();
            Canvas.SetLeft(pump, GasPump.RightX);
            Canvas.SetTop(pump, 275);
            car.GasPump = pump;

            CreatStoryBoardToPump(
                car, 
                timeSetTo1, 
                BottomY, 
                PumpBottomY + turnOffset, 
                PumpBottomY, 
                RightX2, 
                RightX0);

            CreatStoryBoardFromPump(
                car, 
                timeSetFrom1, 
                PumpBottomY, 
                PumpBottomY - turnOffset,
                TopY,
                RightX2,
                RightX0);

            return car;
        }
        #endregion

        /// <summary>
        /// Creats the story board to pump.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <param name="set">The set.</param>
        /// <param name="fromY">From Y.</param>
        /// <param name="turnY">The turn Y.</param>
        /// <param name="pumpY">The pump Y.</param>
        /// <param name="x">The x.</param>
        /// <param name="xPump">The x pump.</param>
        private static void CreatStoryBoardToPump(Car car, TimeSet set,int fromY,int turnY, int pumpY, int x, int xPump)
        {
            // Path To Gas Pump

            var sb = new Storyboard();
            //X Animation
            var keyFrameAnimationX = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(keyFrameAnimationX, PropertyPathX);
            Storyboard.SetTarget(keyFrameAnimationX, car);

            keyFrameAnimationX.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time0,
                Value = x
            });


            keyFrameAnimationX.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time1,
                Value = x
            });


            keyFrameAnimationX.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time3,
                Value = xPump 
            });
            sb.Children.Add(keyFrameAnimationX);


            //Y Animation
            var keyFrameAnimationY = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(keyFrameAnimationY, PropertyPathY);
            Storyboard.SetTarget(keyFrameAnimationY, car);


            keyFrameAnimationY.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time0,
                Value = fromY
            });


            keyFrameAnimationY.KeyFrames.Add(new SplineDoubleKeyFrame
            {
                KeyTime = set.Time1,
                Value = turnY,
                KeySpline = CreateDeaccelerateSpline()
            });

            keyFrameAnimationY.KeyFrames.Add(new SplineDoubleKeyFrame
            {
                KeyTime = set.Time3,
                Value = pumpY,
                KeySpline = CreateDeaccelerateSpline()
            });
            sb.Children.Add(keyFrameAnimationY);

            //Rotate animantion
            var keyFrameAnimationAngle = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(keyFrameAnimationAngle, PropertyPathAngle);
            Storyboard.SetTarget(keyFrameAnimationAngle, car);

            keyFrameAnimationAngle.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time0,
                Value = 0
            });

            keyFrameAnimationAngle.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time1,
                Value = 0
            });

            keyFrameAnimationAngle.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time2,
                Value = -45
            });
            keyFrameAnimationAngle.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time3,
                Value = 0
            });
            sb.Children.Add(keyFrameAnimationAngle);

            sb.Completed += car.OnArriveGasPump;

            car.SetStartStoryBoard(sb);
        }

        private static void CreatStoryBoardFromPump(Car car, TimeSet set, int pumpY, int turnY,int toY, int x, int xPump)
        {

            // Path To Gas Pump

            var sb = new Storyboard();
            //X Animation
            var keyFrameAnimationX = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(keyFrameAnimationX, PropertyPathX);
            Storyboard.SetTarget(keyFrameAnimationX, car);

            keyFrameAnimationX.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time0,
                Value = xPump
            });


            keyFrameAnimationX.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time2,
                Value = x
            });

            sb.Children.Add(keyFrameAnimationX);


            //Y Animation
            var keyFrameAnimationY = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(keyFrameAnimationY, PropertyPathY);
            Storyboard.SetTarget(keyFrameAnimationY, car);


            keyFrameAnimationY.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time0,
                Value = pumpY
            });


            keyFrameAnimationY.KeyFrames.Add(new SplineDoubleKeyFrame
            {
                KeyTime = set.Time2,
                Value = turnY,
                KeySpline = CreateAccelerateSpline()
            });

            keyFrameAnimationY.KeyFrames.Add(new SplineDoubleKeyFrame
            {
                KeyTime = set.Time3,
                Value =toY,
                KeySpline = CreateAccelerateSpline()
            });
            sb.Children.Add(keyFrameAnimationY);

            //Rotate animantion
            var keyFrameAnimationAngle = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(keyFrameAnimationAngle, PropertyPathAngle);
            Storyboard.SetTarget(keyFrameAnimationAngle, car);

            keyFrameAnimationAngle.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time0,
                Value = 0
            });

            keyFrameAnimationAngle.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time1,
                Value = 45
            });

            keyFrameAnimationAngle.KeyFrames.Add(new LinearDoubleKeyFrame
            {
                KeyTime = set.Time2,
                Value = 0
            });

            sb.Children.Add(keyFrameAnimationAngle);

            sb.Completed += car.OnRouteCompleted;

            car.SetHomeStoryBoard(sb);
        }



        private static KeySpline CreateAccelerateSpline()
        {
            return new KeySpline { ControlPoint1 = new Point(0, 0), ControlPoint2 = new Point(0.5, 0) };
        }
        private static KeySpline CreateDeaccelerateSpline()
        {
            return new KeySpline { ControlPoint1 = new Point(0, 0), ControlPoint2 = new Point(0, 0.5) };
        }
       

        private static Car CreateDirectTour(int yFrom, int yTo, Car car)
        {
            //Y movement
            var myDoubleAnimation2 = new DoubleAnimation { Duration = directPathDuration, From = yFrom, To = yTo };
            Storyboard.SetTarget(myDoubleAnimation2, car);
            Storyboard.SetTargetProperty(myDoubleAnimation2, PropertyPathY);


            var sb = new Storyboard { Duration = directPathDuration };
            sb.Children.Add(myDoubleAnimation2);
            car.SetStartStoryBoard(sb);
            sb.Completed += new EventHandler(car.OnRouteCompleted);



            return car;

        }

    }
}
