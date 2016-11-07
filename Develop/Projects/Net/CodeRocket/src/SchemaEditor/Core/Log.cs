using log4net;

namespace SchemaEditor.Core
{
    internal static class Log
    {
        public static readonly ILog SchemaEditor = LogManager.GetLogger("SchemaEditor");
    }
}
