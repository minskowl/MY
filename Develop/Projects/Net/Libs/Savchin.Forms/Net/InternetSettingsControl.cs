using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Savchin.Net;

namespace Savchin.Forms.Net
{
    public partial class InternetSettingsControl : UserControl
    {
        InternetSettings settings = new InternetSettings();
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public InternetSettings Settings
        {
            get
            {

                return DesignMode ? null :
                    new InternetSettings
                {
                    UseProxy = checkBoxUseProxy.Checked,
                    Proxy = proxySettingsControl1.Proxy,
                    UseDefaultCredentials = checkBoxUseDefaultCredentials.Checked,
                    Credential = credentialControl1.Credential
                };
            }
            set
            {
                settings = value;
                if (value == null)
                {
                    proxySettingsControl1.Proxy = null;
                    credentialControl1.Credential = null;
                }
                else
                {
                    proxySettingsControl1.Proxy = settings.Proxy;
                    credentialControl1.Credential = settings.Credential;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternetSettingsControl"/> class.
        /// </summary>
        public InternetSettingsControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (settings == null) return;


            checkBoxUseProxy.Checked = settings.UseProxy;
            checkBoxUseDefaultCredentials.Checked = settings.UseDefaultCredentials;
            UpdateStateControls();

        }

        /// <summary>
        /// Handles the CheckedChanged event of the checkBoxUseProxy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void checkBoxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStateControls();
        }

        private void checkBoxUseDefaultCredentials_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStateControls();
        }

        /// <summary>
        /// Updates the state controls.
        /// </summary>
        private void UpdateStateControls()
        {
            proxySettingsControl1.Enabled = checkBoxUseProxy.Checked;
            credentialControl1.Enabled = !checkBoxUseDefaultCredentials.Checked;

        }
    }
}
