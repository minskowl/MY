using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.Core;
using BotvaSpider.Data;


namespace BotvaSpider.Configuration
{



    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class StuffSearchCondition : ICloneable
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>The name of the item.</value>
        [XmlAttribute]
        [DisplayName("Чего покупаем")]
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StuffSearchCondition"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        [DisplayName("Активно")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the sales conditions.
        /// </summary>
        /// <value>The sales conditions.</value>
        [DisplayName("Продажа")]
        public PriceConditionsCollection SalesConditions { get; set; }

        /// <summary>
        /// Gets or sets the auction conditions.
        /// </summary>
        /// <value>The auction conditions.</value>
        [DisplayName("Аукцион")]
        public PriceConditionsCollection AuctionConditions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StuffSearchCondition"/> class.
        /// </summary>
        public StuffSearchCondition()
        {
            SalesConditions = new PriceConditionsCollection();
            AuctionConditions = new PriceConditionsCollection();
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public PriceConditionsCollection GetConditions(TradeType type)
        {
            return type == TradeType.Auction ? AuctionConditions : SalesConditions;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return ItemName;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Determines whether this instance can invest the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can invest the specified player; otherwise, <c>false</c>.
        /// </returns>
        public bool CanInvest(Player player)
        {
            return Enabled && (AuctionConditions.CanInvest(player) || SalesConditions.CanInvest(player));
        }
    }


}