
using System.Collections.Generic;

namespace NUnit.Extensions.Forms.Recorder
{
    /// <summary>
    /// This class can be used to prevent <see cref="Recorder"/>-derived
    /// classes from automatically being added to a <see cref="SupportedEventsRegistry"/>.
    /// </summary>
    public static class Censor
    {
        //can suppress the effect of specific recorder assemblies during testing.
        //this is easier (and more hackish) than loading / unloading app domains.

        private static IList<string> CensoredRecorders = new List<string>();

        ///<summary>
        /// Adds the named class to the list of censored <see cref="Recorder"/>s.
        ///</summary>
        public static void Add(string recorderName)
        {
            CensoredRecorders.Add(recorderName);
        }

        /// <summary>
        /// Removes the named class from the list of censored <see cref="Recorder"/>s.
        /// </summary>
        public static void Remove(string recorderName)
        {
            CensoredRecorders.Remove(recorderName);
        }

        ///<summary>
        /// Returns true if the <see cref="Censor"/> is blocking
        /// the given <see cref="Recorder"/> name.
        ///</summary>
        public static bool Contains(string recorderName)
        {
            return CensoredRecorders.Contains(recorderName);
        }
    }
}