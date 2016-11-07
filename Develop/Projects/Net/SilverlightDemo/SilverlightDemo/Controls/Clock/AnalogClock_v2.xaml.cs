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
	public partial class AnalogClock_v2 : UserControl, IClockView
	{
		private const string DigitalDisplayFormat = "{0} : {1} : {2}";

		public AnalogClock_v2()
		{
            InitializeComponent();
		}

		#region IClockView Members

		public void Update( ClockData clockData )
		{
			tbDigitalDisplay.Text = String.Format(
				DigitalDisplayFormat,
				clockData.Hours.ToString( "00" ),
				clockData.Minutes.ToString( "00" ),
				clockData.Seconds.ToString( "00" ) );

            if ( clockData.Seconds % 59 == 0 )
            {
                this.HoursHandStoryboard.Seek( DateTime.Now.TimeOfDay );
                this.MinutesHandStoryboard.Seek( DateTime.Now.TimeOfDay );
                this.SecondsHandStoryboard.Seek( DateTime.Now.TimeOfDay );
                this.TenthHandStoryboard.Seek( DateTime.Now.TimeOfDay );
                this.MinutesHandStoryboard.Seek( DateTime.Now.TimeOfDay );
            }
		}

		public event EventHandler StartClock;

		public event EventHandler StopClock;

		public event EventHandler SplitTime;

		#endregion

		private void SecondsHandCanvas_Loaded( object sender, RoutedEventArgs e )
		{
            this.SecondsHandStoryboard.Begin();
            this.SecondsHandStoryboard.Seek( DateTime.Now.TimeOfDay );
		}

		private void MinutesHandCanvas_Loaded( object sender, RoutedEventArgs e )
		{
            this.MinutesHandStoryboard.Begin();
            this.MinutesHandStoryboard.Seek( DateTime.Now.TimeOfDay );
		}

		private void HoursHandCanvas_Loaded( object sender, RoutedEventArgs e )
		{
            this.HoursHandStoryboard.Begin();
            this.HoursHandStoryboard.Seek( DateTime.Now.TimeOfDay );
		}

		private void MiliSecondsHandCanvas_Loaded( object sender, RoutedEventArgs e )
		{
            this.MiliSecondsHandStoryboard.Begin();
            this.MiliSecondsHandStoryboard.Seek( DateTime.Now.TimeOfDay );
		}

		private void TenthsHandCanvas_Loaded( object sender, RoutedEventArgs e )
		{
            this.TenthHandStoryboard.Begin();
            this.TenthHandStoryboard.Seek( DateTime.Now.TimeOfDay );
		}
	}
}
