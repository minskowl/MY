using System;
using System.Xml.Serialization;
using Advertiser.Entities;
using Savchin.Development;
using Savchin.Logging;

namespace Advertiser.Core
{
    public class AdvContext : AppContext
    {

        public DataBase Data { get; set; }
        public ILogger Log { get; private set; }

        public static AdvContext Current
        {
            get { return (AdvContext)CurrentApp; }
            set { CurrentApp = value; }
        }

        private AdvContext(IApplicationObjectsProvider provider)
            : base(provider)
        {
            Log = new LoggerLog4Net("App");

        }

        public static void Init()
        {
          //  XmlSerializer.GenerateSerializer(new Type[] {typeof (DataBase)}, new XmlMapping[0]);
            CurrentApp = new AdvContext(new SingleThreadProvider());

        }
    }
}
