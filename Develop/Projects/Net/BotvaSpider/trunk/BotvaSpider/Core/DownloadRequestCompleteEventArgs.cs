using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{
    internal delegate void DownloadRequestCompleteHandler(object sender, DownloadRequestCompleteEventArgs e);
    internal class DownloadRequestCompleteEventArgs : EventArgs
    {
        private readonly string fileName;
        private readonly bool successfuly;
        private readonly Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadRequestCompleteEventArgs"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="successfuly">if set to <c>true</c> [successfuly].</param>
        /// <param name="exception">The exception.</param>
        public DownloadRequestCompleteEventArgs(string fileName, bool successfuly, Exception exception)
        {
            this.fileName = fileName;
            this.exception = exception;
            this.successfuly = successfuly;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DownloadRequestCompleteEventArgs"/> is successfuly.
        /// </summary>
        /// <value><c>true</c> if successfuly; otherwise, <c>false</c>.</value>
        public bool Successfuly
        {
            get { return successfuly; }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get { return exception; }
        }
    }
}
