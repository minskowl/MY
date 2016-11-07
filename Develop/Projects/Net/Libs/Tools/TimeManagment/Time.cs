using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Savchin.TimeManagment
{
    /// <summary>
    /// TimePrecision
    /// </summary>
    public enum TimePrecision
    {
        /// <summary>
        /// WithMilliseconds
        /// </summary>
        WithMilliseconds = 0,
        /// <summary>
        /// WithSeconds
        /// </summary>
        WithSeconds = 1,
        /// <summary>
        /// WithMinutes
        /// </summary>
        WithMinutes = 2,
        /// <summary>
        /// WithHours
        /// </summary>
        WithHours = 3,
    }

    /// <summary>
    /// Time
    /// </summary>
    [Serializable]
    public struct Time : IComparable, IComparable<Time>, IEquatable<Time>
    {
        #region Max hour, minute, second, millisecond constants

        /// <summary>
        /// Max hour value.
        /// </summary>
        public const int MaxHour = 23;
        
        /// <summary>
        /// Max minute and second value.
        /// </summary>
        public const int MaxMinuteAndSecond = 59;

        /// <summary>
        /// Max millisecond value
        /// </summary>
        private const int MaxMillisecond = 999;

        #endregion

        #region Properties
        /// <summary>
        /// MinValue
        /// </summary>
        public readonly static Time MinValue = new Time(0, 0, 0);
        /// <summary>
        /// MaxValue
        /// </summary>
        public readonly static Time MaxValue = new Time(MaxHour, MaxMinuteAndSecond, MaxMinuteAndSecond, MaxMillisecond);

        private int _hour;
        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        [XmlAttribute]
        public int Hour
        {
            get { return _hour; }
            set
            {
                HourIsValid(value);
                _hour = value;
            }
        }
        private int _minute;
        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        /// <value>The minute.</value>
        [XmlAttribute]
        public int Minute
        {
            get { return _precision <= TimePrecision.WithHours ? _minute : 0; }
            set
            {
                MinuteIsValid(value);
                _minute = value;
            }
        }
        private int _second;
        /// <summary>
        /// Gets or sets the second.
        /// </summary>
        /// <value>The second.</value>
        [XmlAttribute]
        public int Second
        {
            get { return _precision <= TimePrecision.WithSeconds ? _second : 0; }
            set
            {
                SecondIsValid(value);
                _second = value;
            }
        }
        private int _millisecond;
        /// <summary>
        /// Gets or sets the millisecond.
        /// </summary>
        /// <value>The millisecond.</value>
        [XmlAttribute]
        public int Millisecond
        {
            get { return _precision == TimePrecision.WithMilliseconds ? _millisecond : 0; }
            set
            {
                MillisecondIsValid(value);
                _millisecond = value;
            }
        }
        private TimePrecision _precision;
        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        [XmlAttribute]
        public TimePrecision Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        /// <summary>
        /// Gets the ticks.
        /// </summary>
        /// <value>The ticks.</value>
        public long Ticks
        {
            get { return TimeToTicks(Hour, Minute, Second, Millisecond); }
        }

        #endregion

        #region Constructor

  

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        public Time(DateTime dateTime)
        {
            _hour = dateTime.Hour;
            _minute = dateTime.Minute;
            _second = dateTime.Second;
            _millisecond = dateTime.Millisecond;
            _precision = TimePrecision.WithMilliseconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="precision">The precision.</param>
        public Time(DateTime dateTime, TimePrecision precision)
        {
            _precision = precision;
            switch (precision)
            {
                case TimePrecision.WithMilliseconds:
                    _hour = dateTime.Hour;
                    _minute = dateTime.Minute;
                    _second = dateTime.Second;
                    _millisecond = dateTime.Millisecond;
                   
                    break;
                case TimePrecision.WithSeconds:
                    _hour = dateTime.Hour;
                    _minute = dateTime.Minute;
                    _second = dateTime.Second;
                    _millisecond =0;

                    break;
                case TimePrecision.WithMinutes:
                    _hour = dateTime.Hour;
                    _minute = dateTime.Minute;
                    _second = 0;
                    _millisecond = 0;
                    break;
                case TimePrecision.WithHours:
                    _hour = dateTime.Hour;
                    _minute = 0;
                    _second = 0;
                    _millisecond = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("precision");
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="precision">The precision.</param>
        public Time(Time time, TimePrecision precision)
        {
            _precision = precision;
            switch (precision)
            {
                case TimePrecision.WithMilliseconds:
                    _hour = time.Hour;
                    _minute = time.Minute;
                    _second = time.Second;
                    _millisecond = time.Millisecond;

                    break;
                case TimePrecision.WithSeconds:
                    _hour = time.Hour;
                    _minute = time.Minute;
                    _second = time.Second;
                    _millisecond = 0;

                    break;
                case TimePrecision.WithMinutes:
                    _hour = time.Hour;
                    _minute = time.Minute;
                    _second = 0;
                    _millisecond = 0;
                    break;
                case TimePrecision.WithHours:
                    _hour = time.Hour;
                    _minute = 0;
                    _second = 0;
                    _millisecond = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("precision");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        public Time(int hour, int minute)
        {
            HourIsValid(hour);
            MinuteIsValid(minute);


            _hour = hour;
            _minute = minute;
            _second = 0;
            _millisecond = 0;
            _precision = TimePrecision.WithMinutes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        public Time(int hour, int minute, int second)
        {
            HourIsValid(hour);
            MinuteIsValid(minute);
            SecondIsValid(second);

            _hour = hour;
            _minute = minute;
            _second = second;
            _millisecond = 0;
            _precision = TimePrecision.WithSeconds;

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="millisecond">The millisecond.</param>
        public Time(int hour, int minute, int second, int millisecond)
            : this(hour, minute, second)
        {
            MillisecondIsValid(millisecond);
            _millisecond = millisecond;
            _precision = TimePrecision.WithMilliseconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="ticks">The ticks.</param>
        public Time(long ticks)
        {
            var span = new TimeSpan(ticks);
            _hour = span.Hours;
            _minute = span.Minutes;
            _second = span.Seconds;
            _millisecond = span.Milliseconds;
            _precision = TimePrecision.WithMilliseconds;
        }
        #endregion


        /// <summary>
        /// Toes the date time.
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return DateTime.MinValue + new TimeSpan(0, Hour, Minute, Second, Millisecond);
        }
        /// <summary>
        /// Toes the date time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public DateTime ToDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, Hour, Minute, Second, Millisecond);
        }
        #region Public Methods

        #region Validation
        /// <summary>
        /// Hours the is valid.
        /// </summary>
        /// <param name="hour">The hour.</param>
        public static void HourIsValid(int hour)
        {
            if (((hour < 0) || (hour > MaxHour)))
                throw new ArgumentOutOfRangeException("hour");
        }
        /// <summary>
        /// Minutes the is valid.
        /// </summary>
        /// <param name="minute">The minute.</param>
        public static void MinuteIsValid(int minute)
        {
            if (((minute < 0) || (minute > MaxMinuteAndSecond)))
                throw new ArgumentOutOfRangeException("minute");
        }
        /// <summary>
        /// Seconds the is valid.
        /// </summary>
        /// <param name="second">The second.</param>
        public static void SecondIsValid(int second)
        {
            if ((second < 0) || (second > MaxMinuteAndSecond))
                throw new ArgumentOutOfRangeException("second");
        }

        /// <summary>
        /// Milliseconds the is valid.
        /// </summary>
        /// <param name="millisecond">The millisecond.</param>
        public static void MillisecondIsValid(int millisecond)
        {

            if ((millisecond < 0) || (millisecond > MaxMillisecond))
                throw new ArgumentOutOfRangeException("millisecond");
        }

        #endregion

        /// <summary>
        /// Parses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static Time Parse(string s)
        {
            return new Time(DateTime.Parse(s));
        }

        #region Implementation of IComparable

        /// <summary>
        ///                     Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        ///                     A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: 
        ///                     Value 
        ///                     Meaning 
        ///                     Less than zero 
        ///                     This instance is less than <paramref name="obj" />. 
        ///                     Zero 
        ///                     This instance is equal to <paramref name="obj" />. 
        ///                     Greater than zero 
        ///                     This instance is greater than <paramref name="obj" />. 
        /// </returns>
        /// <param name="obj">
        ///                     An object to compare with this instance. 
        ///                 </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="obj" /> is not the same type as this instance. 
        ///                 </exception><filterpriority>2</filterpriority>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is Time))
            {
                throw new ArgumentException("Argument must be Time", "obj");
            }
            return CompareTo((Time)obj);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Time obj1, Time obj2)
        {
            return obj1.CompareTo(obj2) == -1;
        }
        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Time obj1, Time obj2)
        {
            return obj1.CompareTo(obj2) != 1;
        }
        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Time obj1, Time obj2)
        {
            return obj1.CompareTo(obj2) == 1;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Time obj1, Time obj2)
        {
            return obj1.CompareTo(obj2) != -1;
        }
        #endregion

        #region Implementation of IComparable<Time>

        /// <summary>
        ///                     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///                     A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: 
        ///                     Value 
        ///                     Meaning 
        ///                     Less than zero 
        ///                     This object is less than the <paramref name="other" /> parameter.
        ///                     Zero 
        ///                     This object is equal to <paramref name="other" />. 
        ///                     Greater than zero 
        ///                     This object is greater than <paramref name="other" />. 
        /// </returns>
        /// <param name="other">
        ///                     An object to compare with this object.
        ///                 </param>
        public int CompareTo(Time other)
        {
            var internalTicks = other.Ticks;
            var ticks = Ticks;
            if (ticks > internalTicks)
            {
                return 1;
            }
            if (ticks < internalTicks)
            {
                return -1;
            }
            return 0;

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
            return Ticks.GetHashCode();
        }

        /// <summary>
        /// Equalses the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Time))
                return false;

            return Equals((Time)obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Time other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Time obj1, Time obj2)
        {
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Time obj1, Time obj2)
        {
            return !obj1.Equals(obj2);
        }
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            switch (_precision)
            {
                case TimePrecision.WithMilliseconds:
                    return ToDateTime().ToString("hh:mm:ss.FFF");
                case TimePrecision.WithSeconds:
                    return ToDateTime().ToLongTimeString();
                case TimePrecision.WithMinutes:
                    return ToDateTime().ToShortTimeString();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion


        internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
        {
            long result = ((hour * 0xe10L) + (minute * 60L)) + second;
            if ((result > 0xd6bf94d5e5L) || (result < -922337203685L))
            {
                throw new ArgumentOutOfRangeException(null, "Overflow_TimeSpanTooLong");
            }
            result = (result * 0x989680L);

            result += millisecond * 0x2710L;
            if ((result < 0L) || (result > 0x2bca2875f4373fffL))
            {
                throw new ArgumentException("Arg_DateTimeRange");
            }
            return result;
        }
    }
}
