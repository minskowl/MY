using System;
using System.Net;
using System.Xml.Serialization;

namespace Savchin.Net
{
    /// <summary>
    /// InternetSettings
    /// </summary>
    [Serializable]
    public class InternetSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether [use default credentials].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use default credentials]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public GenericCredential Credential { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use proxy].
        /// </summary>
        /// <value><c>true</c> if [use proxy]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool UseProxy { get; set; }

        /// <summary>
        /// Gets or sets the proxy.
        /// </summary>
        /// <value>The proxy.</value>
        public GenericProxy Proxy { get; set; }


        /// <summary>
        /// Initializes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void Initialize(WebClient client)
        {
            if (Credential!=null) client.Credentials = Credential.CreateReal();
            client.UseDefaultCredentials = UseDefaultCredentials;

            if (UseProxy && Proxy != null) client.Proxy = Proxy.CreateReal();
            
        }

        /// <summary>
        /// Initializes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void Initialize(WebRequest client)
        {
            if (Credential != null) client.Credentials = Credential.CreateReal();
            client.UseDefaultCredentials = UseDefaultCredentials;

            if (UseProxy && Proxy != null) client.Proxy = Proxy.CreateReal();

        }


    }
}
