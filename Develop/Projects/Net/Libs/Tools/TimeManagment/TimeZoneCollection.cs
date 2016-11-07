#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Win32;

namespace Savchin.TimeManagment
{

    /// <summary>
    /// Represents a collection of named time zones.
    /// </summary>
    public class TimeZoneCollection : ReadOnlyCollection<TimeZone>
    {
        /// <summary>
        /// The dictionary mapping time zones with their names.
        /// </summary>
        private readonly Dictionary<string, TimeZone> nameMap;

        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="TimeZoneCollection"/> class.
        /// </summary>
        /// <param name="data">The registry data to use
        /// when populating the collection.</param>
        internal TimeZoneCollection(RegistryKey data)
            : base(new List<TimeZone>())
        {
            string[] subKeyNames = data.GetSubKeyNames();
            nameMap = new Dictionary<string, TimeZone>(subKeyNames.Length);
            foreach (string name in subKeyNames)
            {
                using (RegistryKey key = data.OpenSubKey(name))
                {
                    UITimeZone item = new UITimeZone(name, key);
                    Items.Add(item);
                    nameMap.Add(item.StandardName, item);
                }
            }
            
            ((List<TimeZone>)Items).Sort(Comparer<TimeZone>.Default);
        }

        /// <summary>
        /// Gets the time zone associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the time zone to return.</param>
        public TimeZone this[string name]
        {
            [DebuggerStepThrough]
            get { return nameMap[name]; }
        }

        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <value>The names.</value>
        public IEnumerable<string> Names
        {
            get
            {
                return nameMap.Keys;
            }
        }
    }
}
