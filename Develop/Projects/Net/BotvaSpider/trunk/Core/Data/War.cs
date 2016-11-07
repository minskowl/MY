using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Data
{
    public class War
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets our side.
        /// </summary>
        /// <value>Our side.</value>
        public List<Clan> OurSide { get; set; }

        /// <summary>
        /// Gets or sets the enemy side.
        /// </summary>
        /// <value>The enemy side.</value>
        public List<Clan> EnemySide { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="War"/> is started.
        /// </summary>
        /// <value><c>true</c> if started; otherwise, <c>false</c>.</value>
        public bool Started { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="War"/> class.
        /// </summary>
        public War()
        {
            OurSide= new List<Clan>();
            EnemySide = new List<Clan>();
        }
    }
}
