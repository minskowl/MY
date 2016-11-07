using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Savchin.Net
{
    /// <summary>
    /// GenericProxy
    /// </summary>
    public class GenericProxy
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        [XmlAttribute]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        [XmlAttribute]
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use default credentials].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use default credentials]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets or sets the credential.
        /// </summary>
        /// <value>The credential.</value>
        public GenericCredential Credential { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [bypass proxy on local].
        /// </summary>
        /// <value><c>true</c> if [bypass proxy on local]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool BypassProxyOnLocal { get; set; }

        /// <summary>
        /// Gets or sets the bypass list.
        /// </summary>
        /// <value>The bypass list.</value>
        public List<string> BypassList { get; set; }

        /// <summary>
        /// Creates the real.
        /// </summary>
        /// <returns></returns>
        public WebProxy CreateReal()
        {
            var result = new WebProxy(Address, Port);
            result.BypassProxyOnLocal = BypassProxyOnLocal;
            result.BypassList = BypassList.ToArray();

            if (Credential != null) result.Credentials = Credential.CreateReal();
            result.UseDefaultCredentials = UseDefaultCredentials;
       


            return result;

        }
    }
}
