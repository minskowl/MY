using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Savchin.Web.HtmlProcessing.Core;

namespace Savchin.Web.HtmlProcessing
{
    /// <summary>
    /// WebFileProvider
    /// </summary>
    public class WebFileProvider : IWebFileProvider
    {
        private WebClient client = new WebClient();
        private List<Error> _errors= new List<Error>();

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IList<Error> Errors
        {
            get { return _errors; }
        }

        /// <summary>
        /// Gets the requested file
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns>File instance</returns>
        public virtual IWebFile GetFile(Uri fileUrl)
        {
            try
            {
                switch (fileUrl.Scheme)
                {
                    case "http":
                        return DownloadHttpFile(fileUrl);
                    case "ftp":
                        return DownloadFtpFile(fileUrl);
                    default:
                        return DownloadFile(fileUrl);
                }
            }
            catch (Exception ex)
            {
                Errors.Add(new Error { Url = fileUrl , Exception = ex});
                return null;
            }
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns></returns>
        protected virtual IWebFile DownloadFile(Uri fileUrl)
        {
            return new LoadedData(fileUrl, client.DownloadData(fileUrl));
        }

        /// <summary>
        /// Downloads the HTTP file.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns></returns>
        protected virtual IWebFile DownloadHttpFile(Uri fileUrl)
        {
            return DownloadFile(fileUrl);
        }
        /// <summary>
        /// Downloads the FTP file.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns></returns>
        protected virtual IWebFile DownloadFtpFile(Uri fileUrl)
        {
            return DownloadFile(fileUrl);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
        }
        public class Error
        {
            /// <summary>
            /// Gets or sets the URL.
            /// </summary>
            /// <value>The URL.</value>
            public Uri  Url{ get; set;}
            /// <summary>
            /// Gets or sets the exception.
            /// </summary>
            /// <value>The exception.</value>
            public Exception Exception{ get; set;}
        }
    }
}
