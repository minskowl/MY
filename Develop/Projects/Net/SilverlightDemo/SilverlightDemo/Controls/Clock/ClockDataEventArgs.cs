using System;

namespace EffectiveSoft.SilverlightDemo.Controls
{
	public class ClockDataEventArgs : EventArgs
	{
		public ClockDataEventArgs( ClockData clockData )
		{
			this.ClockData = clockData;
		}

		public ClockData ClockData
		{
			get;
			protected set;
		}
	}
}
