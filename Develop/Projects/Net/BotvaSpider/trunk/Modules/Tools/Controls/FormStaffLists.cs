using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Core;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls
{
    public partial class FormStaffLists : Form
    {
        /// <summary>
        /// Gets the type of the list.
        /// </summary>
        /// <value>The type of the list.</value>
        public StaffListType ListType
        {
            get { return (StaffListType)comboBoxTypes.GetValue(); }
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>The users.</value>
        public IEnumerable<string> Users
        {
            get { return userListControl1.GetUsers(); }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FormStaffLists"/> class.
        /// </summary>
        public FormStaffLists()
        {
            InitializeComponent();

            comboBoxTypes.Setup(typeof (StaffListType));
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
