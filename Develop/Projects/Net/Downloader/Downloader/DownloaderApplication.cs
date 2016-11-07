using System;
using System.ComponentModel;
using System.Net;
using Savchin.Core;
using System.Windows.Forms;

namespace Downloader
{
    class DownloaderApplication : ApplicationManager<Settings>
    {
        WebClient client = new WebClient();
        private TextBox textBoxLog;
        private string fileName;

        private FormMain formMain;
        public DownloaderApplication(FormMain formMain, TextBox textBoxLog)
            : base(formMain)
        {
            this.textBoxLog = textBoxLog;

            this.formMain = formMain;

            client.UseDefaultCredentials = Settings.UseDefaultCredentials;

            if (string.IsNullOrEmpty(Settings.ProxyUser) &&
                string.IsNullOrEmpty(Settings.ProxyPassword))
                client.Proxy.Credentials =
                    new NetworkCredential(Settings.ProxyUser, Settings.ProxyPassword, Settings.ProxyDomain);

            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

        }


        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            AddLog("Finished file");
            if (e.Error != null)
                AddLog("Error: " + e.Error);

            LoadFile();


            //FileInfo info = new FileInfo(Settings.OutputPath + fileName);
            //if(info.Length==0)
            //    File.Delete(Settings.OutputPath + fileName);
        }
        public void LoadFile()
        {
            Uri url = formMain.GetNextUrl();
            if (url == null)
            {
                AddLog("End download");
                return;
            }
            fileName = url.Segments[url.Segments.Length - 1];
            AddLog("Start: " + fileName);
            client.DownloadFileAsync(url, Settings.OutputPath + fileName);

        }

        private void AddLog(string message)
        {
            textBoxLog.Text = message + Environment.NewLine + textBoxLog.Text;
        }
    }
}
