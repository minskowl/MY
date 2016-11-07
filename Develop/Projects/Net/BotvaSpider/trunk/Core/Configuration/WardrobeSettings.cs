using System.Xml.Serialization;
using BotvaSpider.Core;
using BotvaSpider.Gears;
using Savchin.Collection.Generic;

namespace BotvaSpider.Configuration
{
    public class WardrobeSettings : DictionaryEx<PlayerAction, Coulomb>
    {
        /// <summary>
        /// Gets or sets the min empty slots.
        /// </summary>
        /// <value>The min empty slots.</value>
        [XmlAttribute]
        public int MinEmptySlots { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WardrobeSettings"/> class.
        /// </summary>
        public WardrobeSettings()
        {
            MinEmptySlots = 2;
        }

        /// <summary>
        /// Gets the coulomb.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public Coulomb GetCoulomb(PlayerAction action)
        {
            if (ContainsKey(action))
                return this[action];

            return Coulomb.Undefined;
        }

    }
}
