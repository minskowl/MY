#region Version & Copyright
/* 
 * $Id: DateTimeUtils.cs 34706 2008-07-07 15:05:44Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Savchin.TimeManagment
{
    /// <summary>
    /// DateTimeUtils
    /// </summary>
    public static class DateTimeUtils
    {
        public readonly static DateTime ZeroDate = new DateTime(1900, 1, 1);

        private static List<string> formats;
        private static readonly IDictionary<string, string> formatsSamples;
        private static readonly IEnumerable<int> hours;
        private static readonly IEnumerable<int> minutes;
        private static readonly NameCollection months = NameCollection.CreateMonths();
        private static readonly NameCollection weekDaysShort = NameCollection.CreateWeekDaysShort();

        /// <summary>
        /// Gets the hours.
        /// </summary>
        /// <value>The hours.</value>
        public static IEnumerable<int> Hours
        {
            get { return hours; }
        }
        /// <summary>
        /// Gets the minutes and seconds.
        /// </summary>
        /// <value>The minutes and seconds.</value>
        public static IEnumerable<int> MinutesAndSeconds
        {
            get { return minutes; }
        }
        /// <summary>
        /// Gets the months.
        /// </summary>
        /// <value>The months.</value>
        public static IList<string> Months
        {
            get { return months; }
        }
        /// <summary>
        /// Gets the formats samples.
        /// </summary>
        /// <value>The formats samples.</value>
        public static IDictionary<string, string> FormatsSamples
        {
            get { return formatsSamples; }
        }
        /// <summary>
        /// Gets the formats.
        /// </summary>
        /// <value>The formats.</value>
        public static IEnumerable<string> Formats
        {
            get { return formats; }
        }

        /// <summary>
        /// Gets the week days short.
        /// </summary>
        /// <value>The week days short.</value>
        public static IList<string> WeekDaysShort
        {
            get { return weekDaysShort; }
        }


        /// <summary>
        /// Initializes the <see cref="DateTimeUtils"/> class.
        /// </summary>
        static DateTimeUtils()
        {
            hours = Enumerable.Range(0, 24).ToArray();
            minutes = Enumerable.Range(0, 60).ToArray();

            formats = new List<string>();
            CultureInfo info = CultureInfo.CreateSpecificCulture("en-US");
            foreach (string s in info.DateTimeFormat.GetAllDateTimePatterns())
            {
                if (!formats.Contains(s))
                    formats.Add(s);
            }



            formatsSamples = new Dictionary<string, string>();
            DateTime n = DateTime.Now;

            foreach (string format in formats)
            {
                formatsSamples.Add(format, n.ToString(format));
            }
        }
        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime date)
        {
            return Microsoft.VisualBasic.DateAndTime.DatePart(
                Microsoft.VisualBasic.DateInterval.WeekOfYear,
                date,
                Microsoft.VisualBasic.FirstDayOfWeek.Monday,
                Microsoft.VisualBasic.FirstWeekOfYear.Jan1);
        }

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <returns></returns>
        public static int GetWeekOfYear()
        {
            return GetWeekOfYear(DateTime.Today);
        }

        /// <summary>
        /// Gets the first date. Return first date current year.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetFirstDate()
        {
            return new DateTime(DateTime.Today.Year, 1, 1);
        }

        /// <summary>
        /// Gets the first date.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static DateTime GetFirstDate(int year)
        {
            return new DateTime(year, 1, 1);
        }

        /// <summary>
        /// Toes the time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static Time ToTime(this DateTime date)
        {
            return new Time(date);
        }

        /// <summary>
        /// Toes the time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        public static DateTime Round(this DateTime date, TimePrecision precision)
        {
            switch (precision)
            {
                case TimePrecision.WithMilliseconds:
                    return new DateTime(date.Ticks);

                case TimePrecision.WithSeconds:
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

                case TimePrecision.WithMinutes:
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
                case TimePrecision.WithHours:
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
                default:
                    throw new ArgumentOutOfRangeException("precision");
            }
        }
        /// <summary>
        /// Toes the time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        public static Time ToTime(this DateTime date, TimePrecision precision)
        {
            return new Time(date, precision);
        }

        private class NameCollection : IList<string>
        {
            readonly List<string> names = new List<string>();

            /// <summary>
            /// Initializes a new instance of the <see cref="NameCollection"/> class.
            /// </summary>
            private NameCollection()
            {

            }
            /// <summary>
            /// Creates the week days short.
            /// </summary>
            /// <returns></returns>
            public static NameCollection CreateWeekDaysShort()
            {
                var result = new NameCollection();
                result.names.AddRange(new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" });
                return result;
            }

            /// <summary>
            /// Creates the months.
            /// </summary>
            /// <returns></returns>
            public static NameCollection CreateMonths()
            {
                var result = new NameCollection();
                var d = new DateTime(1900, 1, 1);
                while (true)
                {
                    result.names.Add(d.ToString("MMMM"));
                    if (d.Month == 12)
                        break;
                    d = d.AddMonths(1);
                }
                return result;
            }

            /// <summary>
            /// Indexes the of.
            /// </summary>
            /// <param name="item">The item.</param>
            /// <returns></returns>
            public int IndexOf(string item)
            {
                return names.IndexOf(item);
            }

            /// <summary>
            /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
            /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
            public void Insert(int index, string item)
            {
                throw new NotSupportedException("Is readonly collection");
            }

            /// <summary>
            /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index of the item to remove.</param>
            public void RemoveAt(int index)
            {
                throw new NotSupportedException("Is readonly collection");
            }

            /// <summary>
            /// Gets or sets the <see cref="System.String"/> at the specified index.
            /// </summary>
            /// <value></value>
            public string this[int index]
            {
                get { return names[index]; }
                set { throw new NotSupportedException("Is readonly collection"); }
            }

            /// <summary>
            /// Adds the specified item.
            /// </summary>
            /// <param name="item">The item.</param>
            public void Add(string item)
            {
                throw new NotSupportedException("Is readonly collection"); ;
            }

            /// <summary>
            /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </summary>
            public void Clear()
            {
                throw new NotSupportedException("Is readonly collection");
            }

            /// <summary>
            /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
            /// <returns>
            /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
            /// </returns>
            public bool Contains(string item)
            {
                return names.Contains(item);
            }

            /// <summary>
            /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
            /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(string[] array, int arrayIndex)
            {
                names.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </summary>
            /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
            /// <returns>
            /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </returns>
            public bool Remove(string item)
            {
                throw new NotSupportedException("Is readonly collection");
            }

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </summary>
            /// <value></value>
            /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</returns>
            public int Count
            {
                get { return names.Count; }
            }

            /// <summary>
            /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
            /// </summary>
            /// <value></value>
            /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.</returns>
            public bool IsReadOnly
            {
                get { return true; }
            }

            /// <summary>
            /// Gets the enumerator.
            /// </summary>
            /// <returns></returns>
            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                return names.GetEnumerator();
            }

            /// <summary>
            /// Gets the enumerator.
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return ((IEnumerable<string>)this).GetEnumerator();
            }
        }
    }
}
