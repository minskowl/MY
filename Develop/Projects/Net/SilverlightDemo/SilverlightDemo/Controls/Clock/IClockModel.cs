using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EffectiveSoft.SilverlightDemo.Controls
{
	public interface IClockModel
	{
		event EventHandler<ClockDataEventArgs> ClockChanging;
		event EventHandler<ClockDataEventArgs> ClockChanged;

		void SetClockInterval( TimeSpan timeSpan );
		IClockModel Start();
		IClockModel Continue();
		IClockModel Stop();
		ClockData ClockData
		{
			get;
			set;
		}
		bool IsRunning
		{
			get;
		}
	}
}
