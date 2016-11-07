using System.IO;
using System.Windows.Forms;
using FlatSearcher.Core;
using MyCustomWebBrowser.Core;
using Savchin.Core;
using Savchin.Forms.Core;

namespace FlatSearcher.Controls
{
    public partial class MapControl : UserControl, IMap
    {
        private string urlMap;

        public MapControl()
        {
            InitializeComponent();

            if (ControlHelper.DesignMode) return;

            urlMap = "file://127.0.0.1/" + Path.Combine(AppInfo.ApplicationPath, "Map.html").Replace(':', '$').Replace('\\', '/');
            webBrowser.Navigate(urlMap);
            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
        }
        public void Init()
        {
            Init(SystemInformation.WorkingArea.Width - 40,
                 SystemInformation.WorkingArea.Height - 100,
                 SearchContext.Current.Data.Criteria.Polygons);
        }

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (string.Compare(urlMap, e.Url.ToString(), true) == 0)
            {
                Init();
            }
        }

        public string GetPoints()
        {
            return (string)webBrowser.InvokeScript("getPoints");
        }


        private void Init(int width, int height, string points)
        {
            webBrowser.InvokeScript("Init", new object[] { width, height, points });
        }

        /// <summary>
        /// Determines whether [is in region] [the specified LNG].
        /// </summary>
        /// <param name="lng">The LNG.</param>
        /// <param name="lat">The lat.</param>
        /// <returns>
        ///   <c>true</c> if [is in region] [the specified LNG]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRegion(string lng, string lat)
        {
            var result = webBrowser.InvokeScript("IsInRegion", new object[] { lng, lat });
            return (int)result == 1;
        }
    }
}
