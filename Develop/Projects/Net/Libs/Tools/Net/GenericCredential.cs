using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Savchin.Net
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GenericCredential
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [XmlAttribute]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [XmlAttribute]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        [XmlAttribute]
        public string Domain { get; set; }

        /// <summary>
        /// Creates the real.
        /// </summary>
        /// <returns></returns>
        public NetworkCredential CreateReal()
        {
            return (string.IsNullOrEmpty(Domain))
                       ?
                           new NetworkCredential(UserName, Password)
                       :
                           new NetworkCredential(UserName, Password, Domain);

        }

        /// <summary>
        /// Creates the specified credential.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns></returns>
        public static GenericCredential Create(NetworkCredential credential)
        {
            return new GenericCredential
                       {
                           Domain = credential.Domain,
                           Password = credential.Password,
                           UserName = credential.UserName
                       };
        }
    }
}
