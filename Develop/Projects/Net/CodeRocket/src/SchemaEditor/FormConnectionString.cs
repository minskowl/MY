using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Windows.Forms;
using Savchin.Data.Schema;

namespace SchemaEditor
{
    public partial class FormConnectionString : Form
    {

        #region Properties
        private DbConnectionStringBuilder builder;
        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        /// <value>The type of the provider.</value>
        public ProviderType ProviderType
        {
            get { return (ProviderType)comboBoxType.SelectedItem; }
            set { comboBoxType.SelectedItem = value; }
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get
            {
                return builder.ConnectionString;
            }
            set
            {
                builder.ConnectionString = value;
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FormConnectionString"/> class.
        /// </summary>
        public FormConnectionString()
        {
            InitializeComponent();
        

            foreach (var o in Enum.GetValues(typeof(ProviderType)))
            {
                comboBoxType.Items.Add(o);
            }
            comboBoxType.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the buttonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (ProviderType)comboBoxType.SelectedItem;
            builder = DatabaseSchema.GetFactory(type).CreateConnectionStringBuilder();
            propertyGrid1.SelectedObject = builder;
        }
    }
}