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
	public interface IClockView
	{
		void Update( ClockData clockData );

		event EventHandler StartClock;
		event EventHandler StopClock;
		event EventHandler SplitTime;
	}
}
