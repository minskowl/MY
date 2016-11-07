using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;

namespace Savchin.TimeManagment
{
    /// <summary>
    /// Represents a time zone used for the user interface.
    /// </summary>
    [Serializable]
    public class UITimeZone : TimeZone, IComparable<TimeZone>,IComparable, IDeserializationCallback
    {
        //TODO: Implement your own logic of retrieving and updating the current UI time zone for a specific user.
        #region Current UI time zone

        /// <summary>
        /// The collection of current UI time zones.
        /// </summary>
        private readonly static Dictionary<string, TimeZone> userTimeZones =
            new Dictionary<string, TimeZone>();

        /// <summary>
        /// Gets or sets the time zone of the current user.
        /// </summary>
        public static TimeZone CurrentUITimeZone
        {
            get
            {
                // Check whether the user is authenticated
                string userName = Thread.CurrentPrincipal.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                    return TimeZoneManager.GmtTimeZone;

                // Get the time zone of the current user
                TimeZone result = null;
                lock (userTimeZones)
                {
                    if (userTimeZones.TryGetValue(userName, out result))
                        return result;
                }
                return TimeZoneManager.GmtTimeZone;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                // Check whether the user is authenticated
                string userName = Thread.CurrentPrincipal.Identity.Name;
                if (string.IsNullOrEmpty(userName)) return;

                // Update the user's settings
                UITimeZone newZone = GetUITimeZone(value);
                if (newZone == null)
                {
                    throw new ArgumentException("The specified time zone " +
                        "was not recognized as a registered time zone.", "value");
                }
                lock (userTimeZones)
                {
                    if (userTimeZones.ContainsKey(userName))
                        userTimeZones[userName] = newZone;
                    else
                        userTimeZones.Add(userName, newZone);
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="UITimeZone"/> instance representing
        /// an equivalent to the specified time zone.
        /// </summary>
        /// <param name="timeZone">The time zone to convert.</param>
        private static UITimeZone GetUITimeZone(TimeZone timeZone)
        {
            if (timeZone is UITimeZone)
                return (UITimeZone)timeZone;
            try
            {
                return (UITimeZone)TimeZoneManager.
                    TimeZones[timeZone.StandardName];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// The regular expression to use when parsing
        /// the display name of a time zone.
        /// </summary>
        private readonly static Regex displayNameRegex =new Regex(@"\(GMT(?:[\+\-]\d\d\:\d\d)?\)\s(?<Dscr>.*)",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// The system index of the time zone.
        /// </summary>
        private readonly uint index;

        /// <summary>
        /// The <see cref="String"/> used when
        /// displaying the time zone to the user.
        /// </summary>
        private readonly string displayName;
        /// <summary>
        /// Representative cities belonging to the time zone
        /// or other characteristic description of the zone.
        /// </summary>
        private string description;

        /// <summary>
        /// The standard time zone name.
        /// </summary>
        private string standardName;
        /// <summary>
        /// The daylight saving time zone name.
        /// </summary>
        private string daylightName;
        /// <summary>
        /// The actual UTC bias specification.
        /// </summary>
        private BiasSettings actualSettings;
        /// <summary>
        /// Determines whether the time zone uses different dates
        /// for the daylight saving period in different years.
        /// </summary>
        private bool isDaylightDynamic;
        /// <summary>
        /// The first year in the dynamic DST list
        /// </summary>
        private int firstDaylightEntry;
        /// <summary>
        /// The last year in the dynamic DST list
        /// </summary>
        private int lastDaylightEntry;

        /// <summary>
        /// The collection of previously calculated daylight changes.
        /// </summary>
        [NonSerialized]
        private Dictionary<int, DaylightTime> cachedDaylightChanges;

        /// <summary>
        /// Initializes the non-serialized members.
        /// </summary>
        void IDeserializationCallback.OnDeserialization(object sender)
        {
            cachedDaylightChanges = new Dictionary<int, DaylightTime>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UITimeZone"/> class.
        /// </summary>
        /// <param name="name">The name of the time zone
        /// as recorded in the Windows registry.</param>
        /// <param name="data">The time zone's registry data.</param>
        internal UITimeZone(string name, RegistryKey data)
        {
            cachedDaylightChanges = new Dictionary<int, DaylightTime>();

            // Convert the signed number obtained from the registry
            // to the unsigned value of the system index.
            int? indexValue = (int?)data.GetValue("Index");
            if (indexValue.HasValue)
            {
                index = (((uint)(indexValue.Value >> 16)) << 16) |
                    ((uint)(indexValue.Value & 0xFFFF));
            }

            // Get the display name of the time zone.
            displayName = (string)data.GetValue("Display");
            Match m = displayNameRegex.Match(displayName);
            description = m.Groups["Dscr"].Value;

            // Get other descriptive data of the time zone
            standardName = (string)data.GetValue("Std");
            daylightName = (string)data.GetValue("Dlt");
            actualSettings = new BiasSettings(
                (byte[])data.GetValue("TZI"));

            // Check whether the time zone uses dynamic DST
            string[] subKeyNames = data.GetSubKeyNames();
            foreach (string subKeyName in subKeyNames)
            {
                if (subKeyName != "Dynamic DST") continue;
                isDaylightDynamic = true;
                using (RegistryKey subKey = data.OpenSubKey(subKeyName))
                {
                    firstDaylightEntry = (int)subKey.GetValue("FirstEntry");
                    lastDaylightEntry = (int)subKey.GetValue("LastEntry");
                }
                if (standardName != name) // it should not be
                    standardName = name; // is used when accessing the registry
                return;
            }
            isDaylightDynamic = false;
            firstDaylightEntry = int.MinValue;
            lastDaylightEntry = int.MaxValue;
        }


        /// <summary>
        /// Gets the system index of the time zone.
        /// </summary>
        internal uint Index
        {
            [DebuggerStepThrough]
            get { return index; }
        }

        /// <summary>
        /// Gets the standard time zone name.
        /// </summary>
        public override string StandardName
        {
            [DebuggerStepThrough]
            get { return standardName; }
        }

        /// <summary>
        /// Gets the daylight saving time zone name.
        /// </summary>
        public override string DaylightName
        {
            get
            {
                if (string.IsNullOrEmpty(daylightName))
                    return standardName;
                return daylightName;
            }
        }

        /// <summary>
        /// Returns the daylight saving time period for a particular year.
        /// </summary>
        /// <param name="year">The year to which
        /// the daylight saving time period applies.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year"/> is less than 1 or greater than 9999.</exception>
        public override DaylightTime GetDaylightChanges(int year)
        {
            if ((year < 1) || (year > 9999))
                throw new ArgumentOutOfRangeException("year");

            lock (cachedDaylightChanges)
            {
                DaylightTime result = null;
                if (cachedDaylightChanges.TryGetValue(year, out result))
                    return result;
                BiasSettings settings = GetBiasSettings(year);
                result = settings.GetDaylightChanges(year);
                cachedDaylightChanges.Add(year, result);
                return result;
            }
        }

        /// <summary>
        /// Returns the coordinated universal time (UTC) offset
        /// for the specified local time.
        /// </summary>
        /// <param name="time">The local date and time.</param>
        public override TimeSpan GetUtcOffset(DateTime time)
        {
            if (time.Kind == DateTimeKind.Utc)
                return TimeSpan.Zero;

            int year = time.Year;
            DaylightTime daylightTimes = GetDaylightChanges(year);
            BiasSettings settings = GetBiasSettings(year);

            if (!IsDaylightSavingTime(time, daylightTimes))
                return settings.ZoneBias;
            return settings.ZoneBias + daylightTimes.Delta;
        }

        /// <summary>
        /// Returns the coordinated universal time (UTC) offset
        /// for the specified universal time.
        /// </summary>
        /// <param name="time">The UTC date and time.</param>
        private TimeSpan GetUtcOffsetFromUniversalTime(DateTime time)
        {
            int year = time.Year;
            DaylightTime daylightTimes = GetDaylightChanges(year);
            BiasSettings settings = GetBiasSettings(year);

            TimeSpan result = settings.ZoneBias;
            if ((daylightTimes == null) || (daylightTimes.Delta.Ticks == 0))
                return result;

            DateTime time3 = daylightTimes.Start - result;
            DateTime time4 = (daylightTimes.End - result) - daylightTimes.Delta;
            DateTime time5, time6;
            if (daylightTimes.Delta.Ticks > 0)
            {
                time5 = time4 - daylightTimes.Delta;
                time6 = time4;
            }
            else
            {
                time5 = time3;
                time6 = time3 - daylightTimes.Delta;
            }

            bool flag = false;
            if (time3 > time4)
                flag = (time < time4) || (time >= time3);
            else
                flag = (time >= time3) && (time < time4);
            return flag ? result + daylightTimes.Delta : result;
        }

        /// <summary>
        /// Returns the local time that corresponds to
        /// a specified coordinated universal time (UTC).
        /// </summary>
        /// <param name="time">A UTC time.</param>
        public override DateTime ToLocalTime(DateTime time)
        {
            if (time.Kind == DateTimeKind.Local) return time;
            // Note that this implementation ignores
            // possible Daylight Saving Time ambiguity
            long ticks = time.Ticks +
                GetUtcOffsetFromUniversalTime(time).Ticks;
            long max = DateTime.MaxValue.Ticks;
            if (ticks > max) return new DateTime(max, DateTimeKind.Local);
            if (ticks < 0) return new DateTime(0, DateTimeKind.Local);
            return new DateTime(ticks, DateTimeKind.Local);
        }

        /// <summary>
        /// Returns the time bias specification for the specified year.
        /// </summary>
        /// <param name="year">The year for which the bias applies.</param>
        private BiasSettings GetBiasSettings(int year)
        {
            // Check whether the time zone uses dynamic DST
            if (!isDaylightDynamic) return actualSettings;

            if (year < firstDaylightEntry)
                year = firstDaylightEntry;
            else if (year > lastDaylightEntry)
                year = lastDaylightEntry;

            // Get the collection of dynamic DST data
            Dictionary<int, BiasSettings> years = null;
            lock (standardName)
            {
                string key = standardName + " Dynamic DST data";
                //TODO: Use caching to increase performance
                //years = (Dictionary<int, BiasSettings>)Application.Cache[key];
                if (years == null)
                {
                    years = new Dictionary<int, BiasSettings>();
                    //Application.Cache.Add(key, years);
                }
            }

            // Get the DST data for the specified year
            BiasSettings result = null;
            lock (years)
            {
                if (years.TryGetValue(year, out result))
                    return result;

                string keyName = TimeZoneManager.TimeZoneDatabaseKey;
                keyName += string.Format(@"\{0}\Dynamic DST", standardName);
                using (RegistryKey key = Registry.
                    LocalMachine.OpenSubKey(keyName))
                {
                    byte[] rawData = (byte[])key.GetValue(year.ToString());
                    result = (rawData == null) ? actualSettings :
                        new BiasSettings(rawData);
                }
                years.Add(year, result);
            }
            return result;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representing the time zone.
        /// </summary>
        public override string ToString()
        {
            return displayName;
        }

        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A 32-bit integer that indicates the relative order
        /// of the entities being compared.</returns>
        /// <exception cref="ArgumentException"><paramref name="other"/>
        /// is not the same type as this instance.</exception>
        int IComparable.CompareTo(object other)
        {
            if (other == null) return 1;
            if (!this.GetType().IsInstanceOfType(other))
                throw new ArgumentException(null, "other");
            UITimeZone otherZone = (UITimeZone)other;
            TimeSpan thisBias = actualSettings.ZoneBias;
            TimeSpan otherBias = otherZone.actualSettings.ZoneBias;
            if (thisBias == otherBias)
                return StringComparer.InvariantCultureIgnoreCase.
                    Compare(description, otherZone.description);
            else
                return thisBias.CompareTo(otherBias);
        }

        int IComparable<TimeZone>.CompareTo(TimeZone other)
        {
            return ((IComparable)this).CompareTo(other);
        }

        #endregion


        /// <summary>
        /// Provides the UTC offset specification.
        /// </summary>
        [Serializable]
        private class BiasSettings
        {
            /// <summary>
            /// The time zone specification data.
            /// </summary>
            private TimeZoneInfo data;

            /// <summary>
            /// Initializes a new instance of the <see cref="BiasSettings"/> class.
            /// </summary>
            /// <param name="rawData">The data to use for initialization.</param>
            internal BiasSettings(byte[] rawData)
            {
                data = GetTimeZoneInfo(rawData);
            }

            /// <summary>
            /// Gets the basic bias of the time zone.
            /// </summary>
            public TimeSpan ZoneBias
            {
                [DebuggerStepThrough]
                get { return new TimeSpan(0, -data.Bias, 0); }
            }

            /// <summary>
            /// Gets the Daylight Saving Time bias.
            /// </summary>
            public TimeSpan DaylightBias
            {
                [DebuggerStepThrough]
                get { return new TimeSpan(0, -data.DaylightBias, 0); }
            }

            /// <summary>
            /// Returns the daylight saving time period for a particular year.
            /// </summary>
            /// <param name="year">The year to which
            /// the daylight saving time period applies.</param>
            public DaylightTime GetDaylightChanges(int year)
            {
                DateTime start = GetParticularDay(year, data.DaylightDate);
                DateTime end = GetParticularDay(year, data.StandardDate);
                if ((start == DateTime.MinValue) || (end == DateTime.MinValue))
                {
                    return new DaylightTime(DateTime.MinValue,
                        DateTime.MinValue, TimeSpan.Zero);
                }
                return new DaylightTime(start, end, this.DaylightBias);
            }

            #region Conversion Routines

            /// <summary>
            /// Returns the particular date in the specified year
            /// on which the daylight saving time period applies.
            /// </summary>
            private DateTime GetParticularDay(int year, DaylightLimit rule)
            {
                if (!rule.Enabled) return DateTime.MinValue;

                // Get the first occurence of the requested weekday
                DateTime result = new DateTime(year, rule.Month, 1,
                    rule.Hour, rule.Minute, rule.Second,
                    rule.Milliseconds, DateTimeKind.Local);
                int diff = rule.DayOfWeek - (int)result.DayOfWeek;
                if (diff < 0) diff += 7;
                result = result.AddDays(diff);

                // Get the day when the daylight time change applies
                result = result.AddDays((rule.Day - 1) * 7);
                if (result.Month == rule.Month) return result;
                return result.AddDays(-7.0);
            }

            /// <summary>
            /// Interprets raw byte data as a .NET structure.
            /// </summary>
            /// <param name="rawData">The data to interpret.</param>
            private static TimeZoneInfo GetTimeZoneInfo(byte[] rawData)
            {
                TimeZoneInfo result = new TimeZoneInfo();
                if (rawData.Length != Marshal.SizeOf(result))
                    throw new ArgumentException(null, "rawData");

                GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
                try
                {
                    result = (TimeZoneInfo)Marshal.PtrToStructure(
                        handle.AddrOfPinnedObject(), typeof(TimeZoneInfo));
                    return result;
                }
                finally
                {
                    handle.Free();
                }
            }

            /// <summary>
            /// Represents the standard Windows SYSTEMTIME structure which
            /// is used when defining limits of daylight saving time periods.
            /// </summary>
            [Serializable, StructLayout(LayoutKind.Sequential)]
            private struct DaylightLimit
            {
                public UInt16 Year;
                public UInt16 Month;
                public UInt16 DayOfWeek;
                public UInt16 Day;
                public UInt16 Hour;
                public UInt16 Minute;
                public UInt16 Second;
                public UInt16 Milliseconds;

                /// <summary>
                /// Indicates whether the daylight time limit
                /// represents an applicable DST change.
                /// </summary>
                public bool Enabled
                {
                    [DebuggerStepThrough]
                    get { return Month > 0; }
                }

            }//end DaylightLimit


            /// <summary>
            /// The layout of the TZI value in the Windows registry.
            /// </summary>
            [Serializable, StructLayout(LayoutKind.Sequential)]
            private struct TimeZoneInfo
            {
                public int Bias;
                public int StandardBias;
                public int DaylightBias;
                public DaylightLimit StandardDate;
                public DaylightLimit DaylightDate;

            }//end TimeZoneInfo

            #endregion

        }//end BiasSettings

    }
}
