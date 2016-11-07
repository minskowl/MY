using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Savchin.IO;

namespace BotvaSpider.Core
{
    class DataDownloader
    {


        /// <summary>
        /// Occurs when [downloaded].
        /// </summary>
        public event DownloadRequestCompleteHandler Downloaded;


        /// <summary>
        /// Downloads the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileName">Name of the file.</param>
        public void Download(string url, string fileName)
        {
            var request = AppCore.GameSettings.ServerSettings.CreateRequest(url);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var source = response.GetResponseStream())
            using (var destination = File.OpenWrite(fileName))
            {
                StreamPipe.Transfer(source, destination);
            }

        }

        /// <summary>
        /// Starts the download.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileName">Name of the file.</param>
        public void StartDownload(string url, string fileName)
        {

            var request = AppCore.GameSettings.ServerSettings.CreateRequest(url);

            request.BeginGetResponse(AsyncCallback,
                new DownloadRequest
                    {
                        FileName = fileName,
                        Request = request
                    });

        }
        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public string ReadFile(string filename)
        {
            return File.ReadAllText(filename, Encoding.GetEncoding(1251));
        }

        private void AsyncCallback(IAsyncResult ar)
        {
            DownloadRequestCompleteEventArgs arg;
            var downloadRequest = (DownloadRequest)ar.AsyncState;
            try
            {
              
                using (var response = downloadRequest.Request.EndGetResponse(ar))
                using (var source = response.GetResponseStream())
                using (var destination = File.OpenWrite(downloadRequest.FileName))
                {
                    StreamPipe.Transfer(source, destination);
                }
                arg = new DownloadRequestCompleteEventArgs(downloadRequest.FileName,true,null);
            }
            catch (Exception ex)
            {
                arg = new DownloadRequestCompleteEventArgs(downloadRequest.FileName, false, ex);
            }
            if (Downloaded != null) Downloaded(this, arg);
        }

        private class DownloadRequest
        {
            public HttpWebRequest Request { get; set; }
            public string FileName { get; set; }
        }
    }
}
