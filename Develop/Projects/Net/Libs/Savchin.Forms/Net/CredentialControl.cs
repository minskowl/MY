using System;
using System.ComponentModel;
using System.Windows.Forms;
using Savchin.Net;

namespace Savchin.Forms.Net
{
    /// <summary>
    /// CredentialControl
    /// </summary>
    public partial class CredentialControl : UserControl
    {

        /// <summary>
        /// Gets or sets the credential.
        /// </summary>
        /// <value>The credential.</value>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public GenericCredential Credential
        {
            get
            {

                return DesignMode ? null : new GenericCredential
                                       {
                                           UserName = textBoxUserName.Text,
                                           Password = textBoxPassword.Text,
                                           Domain = textBoxDomain.Text
                                       };
            }
            set
            {

                if (value != null)
                {
                    textBoxUserName.Text = value.UserName;
                    textBoxPassword.Text = value.Password;
                    textBoxDomain.Text = value.Domain;
                }


            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialControl"/> class.
        /// </summary>
        public CredentialControl()
        {
            InitializeComponent();
        }


    }
}
