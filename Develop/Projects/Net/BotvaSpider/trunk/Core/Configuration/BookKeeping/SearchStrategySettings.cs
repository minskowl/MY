using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BotvaSpider.Configuration
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SearchStrategySettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InvestmentStrategySettings"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets a value indicating whether [need search].
        /// </summary>
        /// <value><c>true</c> if [need search]; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        public bool NeedSearch
        {
            get { return Enabled && StuffConditions.Count > 0; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [sound notification].
        /// </summary>
        /// <value><c>true</c> if [sound notification]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool SoundNotification { get; set; }

        [DisplayName("Критерии поиска")]
        [Description("Выбираем что и как покупать в сбытнице. Внимание в процессе тестирования !!!!")]
        public List<StuffSearchCondition> StuffConditions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchStrategySettings"/> class.
        /// </summary>
        public SearchStrategySettings()
        {
            StuffConditions= new List<StuffSearchCondition>();
        }
    }
}
