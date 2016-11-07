using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Site.Core
{
    public static class Log
    {
        public static readonly ILog WebMoneyService = LogManager.GetLogger("WebMoney");
        public static readonly ILog Site = LogManager.GetLogger("Site");
    }
}
