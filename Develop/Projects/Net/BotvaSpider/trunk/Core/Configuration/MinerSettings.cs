using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BotvaSpider.Gears;
using Savchin.Core;

namespace BotvaSpider.Configuration
{
    /// <summary>
    /// MinerSettings
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MinerSettings
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether [search crystals].
        /// </summary>
        /// <value><c>true</c> if [search crystals]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool VisitMine { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [search until try].
        /// </summary>
        /// <value><c>true</c> if [search until try]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool SearchUntilTry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use helmet].
        /// </summary>
        /// <value><c>true</c> if [use helmet]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool UseHelmet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use glasses].
        /// </summary>
        /// <value><c>true</c> if [use glasses]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool UseGlasses { get; set; }

        /// <summary>
        /// Gets or sets the search crystal limits.
        /// </summary>
        /// <value>The search crystal limits.</value>
        public List<Range<int>> SearchCrystalLimits { get; set; }
        /// <summary>
        /// Gets or sets the ticket actions.
        /// </summary>
        /// <value>The ticket actions.</value>
        public List<TicketAction> TicketActions { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MinerSettings"/> class.
        /// </summary>
        public MinerSettings()
        {
            SearchCrystalLimits = new List<Range<int>>();
            TicketActions = new List<TicketAction>();
        }
        private readonly static TicketAction defaultAction = new TicketAction { ActionType = TicketActionType.Stores };
        /// <summary>
        /// Gets the ticket action.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        public TicketAction GetTicketAction(Ticket ticket)
        {
            return TicketActions.FirstOrDefault(e => e.TicketType == ticket) ?? defaultAction;
        }

    }
}
