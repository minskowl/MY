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
    public partial class ProxySettingsControl : UserControl
    {

        /// <summary>
        /// Gets or sets the proxy.
        /// </summary>
        /// <value>The proxy.</value>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public GenericProxy Proxy
        {
            get
            {
                int port = 0;
                int.TryParse(textBoxPort.Text, out port);
                return new GenericProxy
                                {
                                    UseDefaultCredentials = checkBoxUseDefaultCredentials.Checked,
                                    Address = textBoxAddress.Text,
                                    Port = port,
                                    Credential = credentialControl1.Credential
                                };
            }
            set
            {

                if (value == null)
                {
                    credentialControl1.Credential = null;
                }
                else
                {
                    checkBoxUseDefaultCredentials.Checked = value.UseDefaultCredentials;
                    textBoxAddress.Text = value.Address;
                    textBoxPort.Text = value.Port.ToString();
                    credentialControl1.Credential = value.Credential;
                }


            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProxySettingsControl"/> class.
        /// </summary>
        public ProxySettingsControl()
        {
            InitializeComponent();
        }

    }
}
