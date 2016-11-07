using System;

namespace Savchin.TimeManagment
{
    /// <summary>
    /// TimeRange
    /// </summary>
    [Serializable]
    public struct TimeRange : IEquatable<TimeRange>
    {
        #region Properties
        private Time from;
        private Time to;

        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>From.</value>
        public Time From
        {
            get { return from; }
            set
            {
                from = value;
                Validate();
            }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public Time To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
                Validate();
            }
        }
        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        public TimePrecision Precision
        {
            get { return from.Precision; }
            set { from.Precision = to.Precision = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get
            {
                return to == Time.MinValue && from == Time.MinValue;
            }
        }
        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>The empty.</value>
        public static readonly TimeRange Empty = new TimeRange();
        #endregion

        #region Contstructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRange"/> struct.
        /// </summary>
        /// <param name="fromHour">From hour.</param>
        /// <param name="toHour">To hour.</param>
        public TimeRange(int fromHour, int toHour)
            : this(new Time(fromHour, 0), new Time(toHour, 0))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRange"/> struct.
        /// </summary>
        /// <param name="fromHour">From hour.</param>
        /// <param name="fromMintute">From mintute.</param>
        /// <param name="toHour">To hour.</param>
        /// <param name="toMintute">To mintute.</param>
        public TimeRange(int fromHour, int fromMintute, int toHour, int toMintute)
            : this(new Time(fromHour, fromMintute), new Time(toHour, toMintute))
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> struct.
        /// </summary>
        /// <param name="range">The range.</param>
        public TimeRange(TimeRange range)
        {
            from = range.From;
            to = range.To;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRange"/> struct.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="precision">The precision.</param>
        public TimeRange(TimeRange range, TimePrecision precision)
        {
            from = new Time(range.From, precision);
            to = new Time(range.To, precision);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <exception cref="ArgumentOutOfRangeException">Date from should be less than date to</exception>
        public TimeRange(Time dateFrom, Time dateTo)
        {
            from = dateFrom;
            to = dateTo;
            Validate();

        }



        #endregion

        #region Equals

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterPriority>2</filterPriority>
        public override int GetHashCode()
        {
            return From.GetHashCode() ^ To.GetHashCode();
        }

        /// <summary>
        /// Equalses the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DateRange))
                return false;

            return Equals((DateRange)obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(TimeRange other)
        {
            if (From != other.From)
                return false;

            return To == other.To;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TimeRange point1, TimeRange point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TimeRange point1, TimeRange point2)
        {
            return !point1.Equals(point2);
        }
        #endregion

        /// <summary>
        /// Sets the range.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void SetRange(Time from, Time to)
        {
            this.from = from;
            this.to = to;
            Validate();
        }

        /// <summary>
        /// Determines whether [is in range] [the specified date].
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// 	<c>true</c> if [is in range] [the specified date]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRange(DateTime date)
        {
            return IsInRange(date.ToTime());
        }

        /// <summary>
        /// Determines whether [is in range] [the specified date].
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>
        /// 	<c>true</c> if [is in range] [the specified date]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRange(Time time)
        {
            return (from < to) ?
                (time >= from && time <= to) :
                (time >= from || time <= to);
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return From + " - " + To;
        }


        /// <summary>
        /// Parses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static TimeRange Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return new TimeRange();

            var parts = s.Split(new[] { '-' });
            if (parts.Length != 2)
                throw new FormatException("Range must have format 8:59 - 23:58");
            var from = Time.Parse(parts[0].Trim());
            var to = Time.Parse(parts[1].Trim());

            return new TimeRange(from, to);
        }




        private void Validate()
        {
            if (from != Time.MinValue && from == to)
                throw new ArgumentOutOfRangeException("from", from, "Date from should be different than date to");
        }



    }
}
