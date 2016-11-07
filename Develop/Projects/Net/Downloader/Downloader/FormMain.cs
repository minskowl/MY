using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Savchin.Controls.Common;
using Savchin.Net;
using Savchin.Utils;

namespace Downloader
{
    public partial class FormMain : Form
    {
        DownloaderApplication application;
        private InternetSettings settings;
        private static readonly string settingsFileName = AppDomain.CurrentDomain.BaseDirectory + @"\InetSettings.dll";

        public FormMain()
        {

            InitializeComponent();
            application = new DownloaderApplication(this, textBoxLog);

            if (File.Exists(settingsFileName))
                settings = TypeSerializer<InternetSettings>.FromXmlFile(settingsFileName);
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            TypeSerializer<InternetSettings>.ToXmlFile(settingsFileName, settings);
        }
        public Uri GetNextUrl()
        {
            long counter = long.Parse(textBoxStart.Text);
            if (counter > long.Parse(textBoxEnd.Text))
            {
                return null;
            }

            Uri url = new Uri(textBoxBaseUrl.Text + textBoxStart.Text + textBoxExtenshion.Text);
            textBoxStart.Text = (++counter).ToString();
            return url;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            textBoxBaseUrl.Text = application.Settings.BaseUrl;
            textBoxExtenshion.Text = application.Settings.Extension;
            textBoxStart.Text = application.Settings.CounterStart.ToString();
            textBoxEnd.Text = application.Settings.CounterEnd.ToString();
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            application.LoadFile();
        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new InternetSettingsForm())
            {
                form.InternetSettings = settings;
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                settings = form.InternetSettings;
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new WebClient();
                if (settings != null) settings.Initialize(client);
                client.DownloadFile(new Uri("http://www.google.com.by/"), @"c:\test.html");
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowException("Error dowload file", "Fuck", ex);
            }
        }




    }
}