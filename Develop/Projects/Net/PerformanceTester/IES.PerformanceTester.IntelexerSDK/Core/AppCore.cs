using System.Configuration;

namespace IES.PerformanceTester.IntelexerSDK.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AppCore
    {
        #region Properites

        private static readonly bool _useWordCache = true;
        /// <summary>
        /// Gets a value indicating whether [use word cache].
        /// </summary>
        /// <value><c>true</c> if [use word cache]; otherwise, <c>false</c>.</value>
        public static bool UseWordCache
        {
            get { return _useWordCache; }
        }

        #endregion



        /// <summary>
        /// Initializes the <see cref="AppCore"/> class.
        /// </summary>
        static AppCore()
        {
            _useWordCache = GetBool("UseWordCache", true);
        }

        private static bool GetBool(string key, bool def)
        {
            var tmp = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(tmp)) return def;
            return tmp.ToLower() != "false";
        }

    }
}