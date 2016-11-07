using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Tools;


namespace Downloader
{
    [Serializable]
    public  class Settings  
    {
        private string baseUrl;
        private long counterStart;
        private long counterEnd;
        private string extension;
        private bool useDefaultCredentials;
        private string proxyUser;
        private string proxyPassword;
        private string proxyDomain;
        private string outputPath;

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }

        /// <summary>
        /// Gets or sets the CounterStart.
        /// </summary>
        /// <value>The CounterStart.</value>
        public long CounterStart
        {
            get { return counterStart; }
            set { counterStart = value; }
        }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>The extension.</value>
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use default credentials].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use default credentials]; otherwise, <c>false</c>.
        /// </value>
        public bool UseDefaultCredentials
        {
            get { return useDefaultCredentials; }
            set { useDefaultCredentials = value; }
        }

        /// <summary>
        /// Gets or sets the proxy user.
        /// </summary>
        /// <value>The proxy user.</value>
        public string ProxyUser
        {
            get { return proxyUser; }
            set { proxyUser = value; }
        }

        /// <summary>
        /// Gets or sets the proxy password.
        /// </summary>
        /// <value>The proxy password.</value>
        public string ProxyPassword
        {
            get { return proxyPassword; }
            set { proxyPassword = value; }
        }

        /// <summary>
        /// Gets or sets the proxy domain.
        /// </summary>
        /// <value>The proxy domain.</value>
        public string ProxyDomain
        {
            get { return proxyDomain; }
            set { proxyDomain = value; }
        }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        public string OutputPath
        {
            get { return outputPath; }
            set { outputPath = value; }
        }

        /// <summary>
        /// Gets or sets the counter end.
        /// </summary>
        /// <value>The counter end.</value>
        public long CounterEnd
        {
            get { return counterEnd; }
            set { counterEnd = value; }
        }

        public void Load()
        {
            
        }


    }
}
