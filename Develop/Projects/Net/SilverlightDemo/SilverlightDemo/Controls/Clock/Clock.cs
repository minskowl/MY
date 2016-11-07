using System;

namespace EffectiveSoft.SilverlightDemo.Controls
{
	public class Clock : ClockBase
	{
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
		public static IClockModel Create()
		{
			return new Clock();
		}

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns></returns>
		public override IClockModel Start()
		{
			if ( IsRunning )
				return this;

			timer.Start();
			isRunning = true;

			return this;
		}

        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <returns></returns>
		public override IClockModel Stop()
		{
			timer.Stop();
			isRunning = false;

			return this;
		}

        /// <summary>
        /// Continues this instance.
        /// </summary>
        /// <returns></returns>
		public override IClockModel Continue()
		{
			return this.Start();
		}

        /// <summary>
        /// Updates the clock data.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
		protected override void UpdateClockData( DateTime dateTime )
		{
			this.ClockData.Update( dateTime );
		}
	}
}
