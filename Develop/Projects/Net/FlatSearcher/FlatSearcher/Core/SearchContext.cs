using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Development;
using Savchin.Logging;

namespace MyCustomWebBrowser.Core
{
    public class SearchContext : AppContext
    {
        public Database Data { get; set; }
        public ILogger Log { get; private set; }

        public static SearchContext Current
        {
            get { return (SearchContext)CurrentApp; }
            set { CurrentApp = value; }
        }

        public SearchContext(IApplicationObjectsProvider provider)
            : base(provider)
        {
            Log = new LoggerLog4Net("App");

        }
    }
}
