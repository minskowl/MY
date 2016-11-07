using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.Core;
using BotvaSpider.Gears;

namespace BotvaSpider.Configuration
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RivalSourceSettings
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [XmlAttribute]
        public RivalSource Source { get; set; }
        /// <summary>
        /// Gets or sets the coulomb.
        /// </summary>
        /// <value>The coulomb.</value>
        [XmlAttribute]
        public Coulomb Coulomb { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RivalSourceSettings"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the max attempt count.
        /// </summary>
        /// <value>The max attempt count.</value>
        [XmlAttribute]
        public int MaxAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the level filter.
        /// </summary>
        /// <value>The level filter.</value>
        public LevelFilter LevelFilter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RivalSourceSettings"/> class.
        /// </summary>
        public RivalSourceSettings()
        {
            LevelFilter= new LevelFilter();
            Coulomb = Coulomb.Undefined;
        }

    }
}
