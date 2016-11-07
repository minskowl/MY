using System;
using System.Windows.Forms;
using Savchin.Net;

namespace Downloader
{
    /// <summary>
    /// InternetSettingsForm
    /// </summary>
    public partial class InternetSettingsForm : Form
    {
        /// <summary>
        /// Gets or sets the internet settings.
        /// </summary>
        /// <value>The internet settings.</value>
        public InternetSettings InternetSettings
        {
            get { return internetSettingsControl1.Settings; }
            set { internetSettingsControl1.Settings = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternetSettingsForm"/> class.
        /// </summary>
        public InternetSettingsForm()
        {
            InitializeComponent();
        }


    }
}
