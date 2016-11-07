using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.Gears;

namespace BotvaSpider.Configuration
{
    public enum TicketActionType : int
    {
        [Description("Хранить")]
        Stores = 0,
        [Description("Продавать")]
        Sale = 1,
        [Description("Использовать")]
        Use = 2,

    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TicketAction
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute]
        public TicketActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets the type of the ticket.
        /// </summary>
        /// <value>The type of the ticket.</value>
        [XmlAttribute]
        public Ticket TicketType { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        [XmlAttribute]
        public int Price { get; set; }
    }
}
