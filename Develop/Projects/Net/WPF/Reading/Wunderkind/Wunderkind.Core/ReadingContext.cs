using CI.Common.Logging;
using Savchin.Application;

namespace Wunderkind.Core
{
    public class ReadingContext : AppContext
    {
        public ILogger Logger
        {
            get { return (ILogger)Context["Logger"]; }
            set { Context["Logger"] = value; }
        }

        public ISpeaker Speaker
        {
            get { return (ISpeaker)Context["Speaker"]; }
            set { Context["Speaker"] = value; }
     
        }
 
        public static ReadingContext Current
        {
            get { return (ReadingContext)CurrentApp; }
        }

        public ReadingContext(IApplicationObjectsProvider provider)
            : base(provider)
        {
 
        }




    }
}
