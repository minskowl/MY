using log4net;

namespace CodeRocket.Common
{
    static class Log
    {
        public static readonly ILog CodeRocket = LogManager.GetLogger("CodeRocket");
    }
}
