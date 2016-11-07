using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{
    public class Integer
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int Parse(string value)
        {
            return int.Parse(value.Trim().Replace(".", string.Empty));
        }
    }
}
