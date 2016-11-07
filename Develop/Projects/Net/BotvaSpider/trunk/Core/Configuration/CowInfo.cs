using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BotvaSpider.Data;
using BotvaSpider.Gears;

namespace BotvaSpider.Configuration
{
    /// <summary>
    /// CowInfo
    /// </summary>
    public class CowInfo
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [XmlAttribute]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the coulomb.
        /// </summary>
        /// <value>The coulomb.</value>
        [XmlAttribute]
        public Coulomb Coulomb { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return UserName;
        }
    }
}
