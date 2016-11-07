using System;

namespace EffectiveSoft.SilverlightDemo.Controls
{
	public class ClockData
	{
		private int milliSeconds;
		private int seconds;
		private int minutes;
		private int hours;
		private int days;
		private int months;
		private int years;

		public event EventHandler<ClockDataEventArgs> MilliSecondsChanged;
		public event EventHandler<ClockDataEventArgs> SecondsChanged;
		public event EventHandler<ClockDataEventArgs> MinutesChanged;
		public event EventHandler<ClockDataEventArgs> HoursChanged;
		public event EventHandler<ClockDataEventArgs> DaysChanged;
		public event EventHandler<ClockDataEventArgs> MonthsChanged;
		public event EventHandler<ClockDataEventArgs> YearsChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClockData"/> class.
        /// </summary>
		public ClockData()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ClockData"/> class.
        /// </summary>
        /// <param name="startData">The start data.</param>
		public ClockData( DateTime startData )
		{
			this.Update( startData );
		}



        /// <summary>
        /// Gets or sets the milli seconds.
        /// </summary>
        /// <value>The milli seconds.</value>
		public int MilliSeconds
		{
			get
			{
				return this.milliSeconds;
			}
			set
			{
				if ( this.MilliSeconds == value )
					return;

				this.milliSeconds = value;
				this.OnMilliSecondsChanged( new ClockDataEventArgs( this ) );
			}
		}

		public int Seconds
		{
			get
			{
				return this.seconds;
			}
			set
			{
				if ( this.Seconds == value )
					return;

				this.seconds = value;
				this.OnSecondsChanged( new ClockDataEventArgs( this ) );
			}
		}

		public int Minutes
		{
			get
			{
				return this.minutes;
			}
			set
			{
				if ( this.Minutes == value )
					return;

				this.minutes = value;
				this.OnMinutesChanged( new ClockDataEventArgs( this ) );
			}
		}

		public int Hours
		{
			get
			{
				return this.hours;
			}
			set
			{
				if ( this.Hours == value )
					return;

				this.hours = value;
				this.OnHoursChanged( new ClockDataEventArgs( this ) );
			}
		}

		public int Days
		{
			get
			{
				return this.days;
			}
			set
			{
				if ( this.Days == value )
					return;

				this.days = value;
				this.OnDaysChanged( new ClockDataEventArgs( this ) );
			}
		}

		public int Months
		{
			get
			{
				return this.months;
			}
			set
			{
				if ( this.Months == value )
					return;

				this.months = value;
				this.OnMonthsChanged( new ClockDataEventArgs( this ) );
			}
		}

		public int Years
		{
			get
			{
				return this.years;
			}
			set
			{
				if ( this.Years == value )
					return;

				this.years = value;
				this.OnYearsChanged( new ClockDataEventArgs( this ) );
			}
		}


	    public ClockData Update( DateTime dateTime )
		{

			this.MilliSeconds = dateTime.Millisecond;
			this.Seconds = dateTime.Second;
			this.Hours = dateTime.Hour;
			this.Days = dateTime.Day;
			this.Minutes = dateTime.Minute;
			this.Months = dateTime.Month;
			this.Years = dateTime.Year;

			return this;
		}

		public ClockData Update( TimeSpan ts )
		{
			this.MilliSeconds = ts.Milliseconds;
			this.Seconds = ts.Seconds;
			this.Hours = ts.Hours;
			this.Days = ts.Days;
			this.Minutes = ts.Minutes;

			return this;
		}

		protected void OnMilliSecondsChanged( ClockDataEventArgs e )
		{
			EventHandler<ClockDataEventArgs> temp = this.MilliSecondsChanged;
			if ( temp != null )
				temp( this, e );
		}

		protected void OnSecondsChanged( ClockDataEventArgs e )
		{
			EventHandler<ClockDataEventArgs> temp = this.SecondsChanged;
			if ( temp != null )
				temp( this, e );
		}

		protected void OnMinutesChanged( ClockDataEventArgs e )
		{
			EventHandler<ClockDataEventArgs> temp = this.MinutesChanged;
			if ( temp != null )
				temp( this, e );
		}

		protected void OnHoursChanged( ClockDataEventArgs e )
		{
			EventHandler<ClockDataEventArgs> temp = this.HoursChanged;
			if ( temp != null )
				temp( this, e );
		}

		protected void OnDaysChanged( ClockDataEventArgs e )
		{
			EventHandler<ClockDataEventArgs> temp = this.DaysChanged;
			if ( temp != null )
				temp( this, e );
		}

		protected void OnMonthsChanged( ClockDataEventArgs e )
		{
			EventHandler<ClockDataEventArgs> temp = this.MonthsChanged;
			if ( temp != null )
				temp( this, e );
		}

		protected void OnYearsChanged( ClockDataEventArgs e )
		{
			EventHandler<ClockDataEventArgs> temp = this.YearsChanged;
			if ( temp != null )
				temp( this, e );
		}
	}
}
