using System;
using System.Collections.Generic;
using System.Linq;
using BotvaSpider.Core;

namespace BotvaSpider.Gears
{
    /// <summary>
    /// ItemInfo
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Enum Type { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public byte Level { get; set; }

        /// <summary>
        /// Gets or sets the spirit.
        /// </summary>
        /// <value>The spirit.</value>
        public SpiritType Spirit { get; set; }


 


    }
}