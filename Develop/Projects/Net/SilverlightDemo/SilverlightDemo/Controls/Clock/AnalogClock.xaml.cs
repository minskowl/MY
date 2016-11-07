using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EffectiveSoft.SilverlightDemo.Controls
{
    public partial class AnalogClock : UserControl, IClockView
    {
        private Rectangle secondsHand;
        private Rectangle minutesHand;
        private Rectangle hoursHand;
        private double clockWidth = 200;
        private double clockHeight = 200;
        private double secondsHandWidth;
        private double secondsHandHeight;
        private double minutesHandWidth;
        private double minutesHandHeight;
        private double hoursHandWidth;
        private double hoursHandHeight;

        public static readonly DependencyProperty ShowSecondHandProperty =
DependencyProperty.Register(
"ShowSecondHand", typeof(Boolean),
typeof(AnalogClock), null
);


        /// <summary>
        /// Gets or sets a value indicating whether [show second hand].
        /// </summary>
        /// <value><c>true</c> if [show second hand]; otherwise, <c>false</c>.</value>
        public Boolean ShowSecondHand
        {
            get { return (Boolean)GetValue(ShowSecondHandProperty); }
            set { SetValue(ShowSecondHandProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalogClock"/> class.
        /// </summary>
        public AnalogClock()
        {
            secondsHandWidth = 0.5;
            secondsHandHeight = clockWidth - 115;
            minutesHandWidth = 5;
            minutesHandHeight = 70;
            hoursHandWidth = 8;
            hoursHandHeight = 55;

            InitializeComponent();
            this.DrawClock();
        }

        public void OnStartClock()
        {
            if (StartClock != null)
                StartClock(this, EventArgs.Empty);
        }

        public void DrawClock()
        {
            this.DrawMarkers();
            //this.DrawDigits();
        }

        public void Update(ClockData clockData)
        {
            if (ShowSecondHand)
                this.DrawSecondsHand(clockData);
            this.DrawMinutesHand(clockData);
            this.DrawHoursHand(clockData);
        }

        public event EventHandler StartClock;

        public event EventHandler StopClock;

        public event EventHandler SplitTime;

        private void DrawMarkers()
        {
            MarkerCanvas.Children.Clear();

            for (int i = 0; i < 60; ++i)
            {
                Rectangle markerRectangle = new Rectangle();

                if (i % 5 == 0)
                {
                    markerRectangle.Width = 3;
                    markerRectangle.Height = 8;
                    markerRectangle.Fill = new SolidColorBrush(Colors.White);
                    markerRectangle.RenderTransform = this.CreateTransformGroup(i * 6,
                        -(markerRectangle.Width / 2),
                        -(markerRectangle.Height * 2 + 4.5 - ClockFaceEllipse.StrokeThickness / 2 - this.clockHeight / 2));
                }
                else
                {
                    markerRectangle.Width = 1;
                    markerRectangle.Height = 4;
                    markerRectangle.Fill = new SolidColorBrush(Colors.White);

                    markerRectangle.RenderTransform = this.CreateTransformGroup(i * 6,
                        -(markerRectangle.Width / 2),
                        -(markerRectangle.Height * 2 + 12.75 - ClockFaceEllipse.StrokeThickness / 2 - this.clockHeight / 2));
                }

                MarkerCanvas.Children.Add(markerRectangle);
            }
        }

        private void DrawSecondsHand(ClockData clockData)
        {
            if (secondsHand != null && ClockHandsCanvas.Children.Contains(secondsHand))
                ClockHandsCanvas.Children.Remove(secondsHand);

            secondsHand = this.CreateHand(this.secondsHandWidth, this.secondsHandHeight,
                new SolidColorBrush(Colors.Black), 0, 0, 0, null);
            secondsHand.RenderTransform = this.CreateTransformGroup(
                clockData.Seconds * 6,
                -this.secondsHandWidth / 2,
                -this.secondsHandHeight + 4.25);

            if (ClockHandsCanvas.Children.Contains(secondsHand) == false)
                ClockHandsCanvas.Children.Add(secondsHand);
        }

        private void DrawMinutesHand(ClockData clockData)
        {
            if (minutesHand != null && ClockHandsCanvas.Children.Contains(minutesHand))
                ClockHandsCanvas.Children.Remove(minutesHand);

            minutesHand = this.CreateHand(this.minutesHandWidth, this.minutesHandHeight,
                new SolidColorBrush(Colors.White), 0.6, 2, 2,
                new SolidColorBrush(Color.FromArgb(0x33, 0x33, 0x33, 0x33)));
            minutesHand.RenderTransform = this.CreateTransformGroup(
                6 * clockData.Minutes + clockData.Seconds / 10,
                -this.minutesHandWidth / 2,
                -this.minutesHandHeight + 4.25);

            if (ClockHandsCanvas.Children.Contains(minutesHand) == false)
                ClockHandsCanvas.Children.Add(minutesHand);
        }

        private void DrawHoursHand(ClockData clockData)
        {
            if (hoursHand != null && ClockHandsCanvas.Children.Contains(hoursHand))
                ClockHandsCanvas.Children.Remove(hoursHand);

            hoursHand = this.CreateHand(this.hoursHandWidth, this.hoursHandHeight,
                new SolidColorBrush(Colors.White), 0.6, 3, 3,
                new SolidColorBrush(Color.FromArgb(0x33, 0x33, 0x33, 0x33)));
            hoursHand.RenderTransform = this.CreateTransformGroup(
                30 * clockData.Hours + clockData.Minutes / 2 + clockData.Seconds / 120,
                -this.hoursHandWidth / 2,
                -this.hoursHandHeight + 4.25);

            if (ClockHandsCanvas.Children.Contains(hoursHand) == false)
                ClockHandsCanvas.Children.Add(hoursHand);
        }

        private Rectangle CreateHand(double width, double height, Brush fillBrush, double strokeThickness,
            double radiusX, double radiusY, Brush strokeBrush)
        {
            Rectangle hand = new Rectangle();

            hand.Width = width;
            hand.Height = height;
            hand.Fill = fillBrush;
            hand.StrokeThickness = strokeThickness;
            hand.RadiusX = radiusX;
            hand.RadiusY = radiusY;
            hand.Stroke = strokeBrush;

            return hand;
        }

        private TransformGroup CreateTransformGroup(double angle, double firstTranslateXValue,
            double firstTranslateYValue)
        {
            TransformGroup transformGroup = new TransformGroup();

            TranslateTransform firstTranslate = new TranslateTransform();
            firstTranslate.X = firstTranslateXValue;
            firstTranslate.Y = firstTranslateYValue;
            transformGroup.Children.Add(firstTranslate);

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = angle;
            transformGroup.Children.Add(rotateTransform);

            TranslateTransform secondTranslate = new TranslateTransform();
            secondTranslate.X = this.clockWidth / 2;
            secondTranslate.Y = this.clockHeight / 2;
            transformGroup.Children.Add(secondTranslate);

            return transformGroup;
        }
    }
}
