using System.Configuration;
using log4net;

namespace IES.PerformanceTester.Gui
{
    public static class AppCore
    {
        public static readonly  ILog Log = LogManager.GetLogger("App");

        #region Properites
        private static readonly bool _traceMemoryUsage = true;
        /// <summary>
        /// Gets a value indicating whether [trace memory usage].
        /// </summary>
        /// <value><c>true</c> if [trace memory usage]; otherwise, <c>false</c>.</value>
        public static bool TraceMemoryUsage
        {
            get { return _traceMemoryUsage; }
        }

        /// <summary>
        /// FormMain
        /// </summary>
        public readonly static FormMain FormMain = new FormMain();
        #endregion



        /// <summary>
        /// Initializes the <see cref="AppCore"/> class.
        /// </summary>
        static AppCore()
        {
            _traceMemoryUsage = GetBool("TraceMemoryUsage", true);
        }

        private static bool GetBool(string key, bool def)
        {
            var tmp = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(tmp)) return def;
            return tmp.ToLower() != "false";
        }

    }
}