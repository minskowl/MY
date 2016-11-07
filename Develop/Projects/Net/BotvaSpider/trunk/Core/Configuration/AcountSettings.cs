using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BotvaSpider.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AcountSettings
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [XmlAttribute]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [XmlIgnore]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [XmlAttribute]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        [XmlAttribute]
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [cool status].
        /// </summary>
        /// <value><c>true</c> if [cool status]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool CoolStatus { get; set; }
    }
}
