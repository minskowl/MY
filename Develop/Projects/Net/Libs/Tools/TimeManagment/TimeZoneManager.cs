#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Win32;



namespace Savchin.TimeManagment
{

    /// <summary>
    /// Provides the list of registered time zones.
    /// </summary>
    public static class TimeZoneManager
    {
        /// <summary>
        /// The registry key to use when accessing the time zone database.
        /// </summary>
        internal const string TimeZoneDatabaseKey =
            @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones";

        /// <summary>
        /// An assumed standard name of the GMT time zone.
        /// </summary>
        private const string gmtZoneName = "GMT Standard Time";

        /// <summary>
        /// The collection of registered time zones.
        /// </summary>
        private readonly static TimeZoneCollection timeZones;
        /// <summary>
        /// The Greenwich Mean Time time zone.
        /// </summary>
        private readonly static TimeZone gmtTimeZone;

        /// <summary>
        /// Gets the collection of registered time zones.
        /// </summary>
        public static TimeZoneCollection TimeZones
        {
            [DebuggerStepThrough]
            get { return timeZones; }
        }

        /// <summary>
        /// Gets the Greenwich Mean Time time zone.
        /// </summary>
        public static TimeZone GmtTimeZone
        {
            [DebuggerStepThrough]
            get { return gmtTimeZone; }
        }

        /// <summary>
        /// Initializes static members of the <see cref="TimeZoneManager"/> class.
        /// </summary>
        static TimeZoneManager()
        {
            using (RegistryKey key = Registry.
                LocalMachine.OpenSubKey(TimeZoneDatabaseKey))
            {
                timeZones = new TimeZoneCollection(key);
            }

            // Get the Greenwich time zone
            try
            {
                gmtTimeZone = timeZones[gmtZoneName];
            }
            catch (KeyNotFoundException)
            {
                gmtTimeZone = null;
            }
            if (gmtTimeZone != null) return;

            // Try to find the zone sequentially
            foreach (UITimeZone zone in timeZones)
            {
                if (!(zone.StandardName.StartsWith("GMT") ||
                    zone.ToString().Contains("Greenwich"))) continue;
                gmtTimeZone = zone; break;
            }
        }


        /// <summary>
        /// Gets the now.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public static DateTime GetNow(TimeZone destination)
        {
            return destination.ToLocalTime(DateTime.UtcNow);
        }

        #region Convert

        /// <summary>
        /// Converts the specified source time zone to destination time zone.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime Convert(TimeZone source, TimeZone destination, DateTime dateTime)
        {
            if (destination == null || source == null || source == destination)
                return dateTime;
            return destination.ToLocalTime(source.ToUniversalTime(dateTime));
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static DateRange Convert(TimeZone source, TimeZone destination, DateRange range)
        {
            if (source == null || destination == null || source == destination)
                return range;
            return new DateRange(
                destination.ToLocalTime(source.ToUniversalTime(range.From)),
                destination.ToLocalTime(source.ToUniversalTime(range.To)));
        }

        /// <summary>
        /// Converts the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static DateRange Convert(TimeZone destination, DateRange range)
        {
            if (destination == null || destination == TimeZone.CurrentTimeZone)
                return new DateRange(range);
            return new DateRange(
                destination.ToLocalTime(range.From.ToUniversalTime()),
                destination.ToLocalTime(range.To.ToUniversalTime()));
        }

        /// <summary>
        /// Converts the specified destination time zone from current time zone.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime Convert(TimeZone destination, DateTime dateTime)
        {
            if (destination == null || destination == TimeZone.CurrentTimeZone)
                return dateTime;
            return destination.ToLocalTime(dateTime.ToUniversalTime());
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="data">The data.</param>
        public static void Convert(TimeZone source, TimeZone destination, DataTable data)
        {
            if (destination == null || source == null || source == destination)
                return;

            foreach (DataColumn column in data.Columns)
            {
                if (column.DataType == typeof(DateTime))
                {
                    int ord = column.Ordinal;

                    foreach (DataRow row in data.Rows)
                    {
                        row[ord] = destination.ToLocalTime(source.ToUniversalTime((DateTime)row[ord]));
                    }
                }
            }
        }

        /// <summary>
        /// Converts the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="data">The data.</param>
        public static void Convert(TimeZone destination, DataTable data)
        {
            if (destination == null || destination == TimeZone.CurrentTimeZone)
                return;
            foreach (DataColumn column in data.Columns)
            {
                if (column.DataType == typeof(DateTime))
                {
                    int ord = column.Ordinal;

                    foreach (DataRow row in data.Rows)
                    {
                        row[ord] = destination.ToLocalTime(((DateTime)row[ord]).ToUniversalTime());
                    }
                }
            }
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="data">The data.</param>
        public static void Convert(TimeZone source, TimeZone destination, DataSet data)
        {
            if (destination == null || source == null || source == destination)
                return;

            foreach (DataTable table in data.Tables)
            {
                Convert(source, destination, table);
            }
        }

        /// <summary>
        /// Converts the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="data">The data.</param>
        public static void Convert(TimeZone destination, DataSet data)
        {
            if (destination == null || destination == TimeZone.CurrentTimeZone)
                return;
            foreach (DataTable table in data.Tables)
            {
                Convert(destination, table);
            }
        }

        #endregion
    }






}


