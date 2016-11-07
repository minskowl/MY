using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.Core;

namespace BotvaSpider.Core
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LevelFilter : IRange<int>
    {
        /// <summary>
        /// Gets or sets the rival level from.
        /// </summary>
        /// <value>The rival level from.</value>
        [XmlAttribute]
        public int LevelFrom { get; set; }

        /// <summary>
        /// Gets or sets the rival level to.
        /// </summary>
        /// <value>The rival level to.</value>
        [XmlAttribute]
        public int LevelTo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelFilter"/> class.
        /// </summary>
        public LevelFilter()
        {
            LevelFrom = -2;
            LevelTo = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelFilter"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public LevelFilter(int from, int to)
        {
            SetValue(from, to);
        }

        /// <summary>
        /// Determines whether the specified player level is valid.
        /// </summary>
        /// <param name="playerLevel">The player level.</param>
        /// <param name="rivalLevel">The rival level.</param>
        /// <returns>
        /// 	<c>true</c> if the specified player level is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(int playerLevel, int rivalLevel)
        {
            return (rivalLevel >= LevelFrom + playerLevel && rivalLevel <= LevelTo + playerLevel);
        }


        /// <summary>
        /// Creates the full.
        /// </summary>
        /// <param name="playerLevel">The player level.</param>
        /// <returns></returns>
        public LevelFilter CreateFull(int playerLevel)
        {
            return new LevelFilter(LevelFrom + playerLevel, LevelTo + playerLevel);
        }

        [XmlIgnore]
        int IRange<int>.From
        {
            get { return LevelFrom; }
            set { LevelFrom = value; }
        }
        [XmlIgnore]
        int IRange<int>.To
        {
            get { return LevelTo; }
            set { LevelTo = value; }
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void SetValue(int from, int to)
        {
            LevelFrom = from;
            LevelTo = to;
        }

        /// <summary>
        /// Determines whether [is in range] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is in range] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRange(int value)
        {
            return value >= LevelFrom && value <= LevelTo;
        }
    }
}