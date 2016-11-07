using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{
    class FightMessage : MessageInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FightMessage"/> is win.
        /// </summary>
        /// <value><c>true</c> if win; otherwise, <c>false</c>.</value>
        public bool Win { get; set; }
        /// <summary>
        /// Gets or sets the rival.
        /// </summary>
        /// <value>The rival.</value>
        public string Rival { get; set; }
    }
}
