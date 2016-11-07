using System;
using Savchin.Core;


namespace Savchin.TimeManagment
{
    /// <summary>
    /// Date Range Class
    /// </summary>
    [Serializable]
    public struct DateRange : IRange<DateTime>, IEquatable<DateRange>
    {
        #region Properties
        private DateTime from;
        private DateTime to;

        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>From.</value>
        public DateTime From
        {
            get { return from; }
            set
            {
                Validate(value, to);
                from = value;

            }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public DateTime To
        {
            get
            {
                return to;
            }
            set
            {
                Validate(from, value);
                to = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get
            {
                return to == DateTime.MinValue && from == DateTime.MinValue;
            }
        }
        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>The empty.</value>
        public static readonly DateRange Empty = new DateRange();
        #endregion

        #region Contstructors


        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> struct.
        /// </summary>
        /// <param name="range">The range.</param>
        public DateRange(DateRange range)
        {
            from = range.From;
            to = range.To;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <exception cref="ArgumentOutOfRangeException">Date from should be less than date to</exception>
        public DateRange(DateTime from, DateTime to)
        {
            Validate(from, to);
            this.from = from;
            this.to = to;
        }



        #endregion

        #region Factories

        /// <summary>
        /// Gets the year range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateRange GetYearRange(DateTime date)
        {
            var from = new DateTime(date.Year, 1, 1);
            return new DateRange(from,
                                 from.AddYears(1).AddSeconds(-1));
        }
        /// <summary>
        /// Gets the year to date range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateRange GetYearToDateRange(DateTime date)
        {
            return new DateRange(new DateTime(date.Year, 1, 1), date);
        }
        /// <summary>
        /// Gets the this month range.
        /// </summary>
        /// <returns></returns>
        public static DateRange GetThisMonthRange()
        {
            return GetMonthRange(DateTime.Today);
        }
        /// <summary>
        /// Gets the month range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateRange GetMonthRange(DateTime date)
        {
            var from = new DateTime(date.Year, date.Month, 1);
            return new DateRange(from,
                                 from.AddMonths(1).AddMinutes(-1));
        }
        /// <summary>
        /// Gets the month to date range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateRange GetMonthToDateRange(DateTime date)
        {
            return new DateRange(new DateTime(date.Year, date.Month, 1), date);
        }

        /// <summary>
        /// Gets the this week range.
        /// </summary>
        /// <returns></returns>
        public static DateRange GetThisWeekRange()
        {
            var today = DateTime.Today;
            var dayOfWeek = (int)today.DayOfWeek;
            return new DateRange(today.AddDays(1 - dayOfWeek), today.AddDays(7 - dayOfWeek));
        }
        /// <summary>
        /// Gets the this day range.
        /// </summary>
        /// <returns></returns>
        public static DateRange GetThisDayRange()
        {
            var today = DateTime.Today;
            return new DateRange(today, today.AddDays(1).AddSeconds(-1));
        }

        /// <summary>
        /// Gets the day range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateRange GetDayRange(DateTime date)
        {
            var day = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            return new DateRange(day, day.AddDays(1).AddSeconds(-1));
        }
        /// <summary>
        /// Gets the day to date range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateRange GetDayToDateRange(DateTime date)
        {
            return new DateRange(new DateTime(date.Year, date.Month, date.Day), date);
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
        public bool Equals(DateRange other)
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
        public static bool operator ==(DateRange point1, DateRange point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(DateRange point1, DateRange point2)
        {
            return !point1.Equals(point2);
        }
        #endregion

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void SetValue(DateTime from, DateTime to)
        {
            Validate(from, to);
            this.from = from;
            this.to = to;
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
            return date >= from && date <= to;
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return from + " - " + to;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public  string ToString(string format)
        {
            return string.Format(format,From,To);
        }
        /// <summary>
        /// Toes the time span.
        /// </summary>
        /// <returns></returns>
        public TimeSpan ToTimeSpan()
        {
            return to - from;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static DateRange GetValue(DateRange? range)
        {
            return (range.HasValue) ? range.Value : Empty;
        }




        /// <summary>
        /// Validates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        private static void Validate(DateTime from, DateTime to)
        {
            if (from > to)
                throw new ArgumentOutOfRangeException("from", from, "Date from should be less than date to");
        }



    }
}
