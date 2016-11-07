using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.BookKeeping;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.ComponentModel;
using Savchin.Core;

namespace BotvaSpider.Configuration
{
    public enum MatchResult
    {
        Ok = 0,
        InvalidType = 1,
        BadPriceAmmount = 2,
        BadPriceResource = 3,
        BadLevel = 4,
        BadResalesCount = 5,
        BadSpirit=6
    }

    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PriceCondition
    {
        /// <summary>
        /// Gets or sets the strategy.
        /// </summary>
        /// <value>The strategy.</value>
        [XmlAttribute]
        [DisplayName("Стратегия")]
        [Description("Две стратегии: 1 цена меньше определенной суммы 2. Цена меньше имеющихся денег")]
        [TypeConverter(typeof(EnumTypeConverter))]
        public TradeStrategy Strategy { get; set; }

        /// <summary>
        /// Gets or sets the min resales count.
        /// </summary>
        /// <value>The min resales count.</value>
        [XmlAttribute]
        [DisplayName("Мин. кол-во перепродаж")]
        public byte MinResalesCount { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        [XmlAttribute]
        [DisplayName("Уровень")]
        public byte Level { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        [XmlAttribute]
        [DisplayName("Заговор")]
        public SpiritType Spirit { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        [DisplayName("Цена")]
        public Price Price { get; set; }

        [XmlAttribute]
        [DisplayName("Активно")]
        public bool Enabled { get; set; }
        
        [XmlIgnore]
        public int ValidationAmmount
        {
            get
            {

                return (Strategy == TradeStrategy.BelowOrEqualExists) ? AppCore.AccountantSettings.GetMinResource(Price.Resource) :
                                                                                                                                      AppCore.AccountantSettings.GetMinResource(Price.Resource) + Price.Ammount;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PriceCondition"/> class.
        /// </summary>
        public PriceCondition()
        {
            Strategy = TradeStrategy.BelowOrEqualAmmount;
            Price = new Price();
        }
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Ур{0} Пер{1} {2} {3} ",
                Level, MinResalesCount, Strategy.GetDescription(), Price);
        }
        /// <summary>
        /// Matches the specified price.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public MatchResult Match(Player player, TradeItemController controller, TradeType type)
        {

            var result = MatchPrice(player, controller.GetPrice(type));
            if (result != MatchResult.Ok) return result;


            if (controller.Level != Level) return MatchResult.BadLevel;
            if (controller.ResalesCount < MinResalesCount) return MatchResult.BadResalesCount;
            if (Spirit > SpiritType.None && controller.Spirit != Spirit) return MatchResult.BadSpirit;
            return MatchResult.Ok;
        }

        private MatchResult MatchPrice(Player player, Price price)
        {

            if (price == null) return MatchResult.InvalidType;
            if (price.Resource != Price.Resource) return MatchResult.BadPriceResource;

            if (price.Resource == Resource.Gold && price.Ammount + AppCore.AccountantSettings.MinMoney > player.Money)
            {
                return MatchResult.BadPriceAmmount;
            }
            switch (Strategy)
            {
                case TradeStrategy.BelowOrEqualExists:
                    if (price.Ammount > player.GetResourceCount(price.Resource))
                        return MatchResult.BadPriceAmmount;
                    break;
                case TradeStrategy.BelowOrEqualAmmount:

                    if (price.CompareTo(Price) == 1) return MatchResult.BadPriceAmmount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return MatchResult.Ok;
        }
    }
}