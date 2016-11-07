using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Commands;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Farming;
using BotvaSpider.Gears;
using Savchin.Collection.Generic;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls
{
    public partial class FarmControl : UserControl
    {
        private ShowFightLogCommand showLogCommand= new ShowFightLogCommand();
        private List<Cow> cows;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmControl"/> class.
        /// </summary>
        public FarmControl()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            if (!DesignMode)
            {
                columnCoulomb.Setup(new Enum[]
                                              {
                                                  Coulomb.Undefined, 
                                                  Coulomb.CrystalThief,
                                                  Coulomb.Drill,
                                                  Coulomb.SmartBaby, 
                                                  Coulomb.TrippleHoof
                                              });

                columnUserType.Setup(typeof(UserType));

                comboBoxUserType.Items.Add(string.Empty);
                comboBoxUserType.Setup(typeof(UserType));


                cows = ObjectProvider.Instance.GetCows();
                SetData(cows);
            }
        }



        #region Event Handlers

        #region Grid
        /// <summary>
        /// Handles the CellEndEdit event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cow = (Cow)dataGridView1.Rows[e.RowIndex].DataBoundItem;

            var user = ObjectProvider.Instance.GetUserByID(cow.UserID);
            user.Name = cow.UserName;
            user.MilkingCoulomb = cow.MilkingCoulomb;
            ObjectProvider.Instance.UpdateUser(user);
        }
        /// <summary>
        /// Handles the UserDeletingRow event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowCancelEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var cow = (Cow)e.Row.DataBoundItem;
            ObjectProvider.Instance.DeleteUser(cow.UserID);
        }

        /// <summary>
        /// Handles the DataError event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var message= string.Format("Error view farm. Column '{0}'. \n Objetc {1}. \n Exception: {2} ",
                dataGridView1.Columns[e.ColumnIndex].HeaderText,
                dataGridView1.Rows[e.RowIndex].DataBoundItem
               , e.Exception)
            ;
            AppCore.LogSystem.Error(message);
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showLogCommand.Execute(dataGridView1.Rows[e.RowIndex].DataBoundItem);
        }
        #endregion

        #region Filter
        /// <summary>
        /// Handles the TextChanged event of the textBoxUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBoxUser_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        /// <summary>
        /// Handles the ValueChanged event of the numericUpDownLevelFrom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void numericUpDownLevelFrom_ValueChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        /// <summary>
        /// Handles the ValueChanged event of the numericUpDownLevelTo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void numericUpDownLevelTo_ValueChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxUserType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void comboBoxUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFilter();
        }
        #endregion

        #region Context Menu
        /// <summary>
        /// Handles the Click event of the showFightLogToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void showFightLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            showLogCommand.Execute(dataGridView1.SelectedRows[0].DataBoundItem);
        }
        /// <summary>
        /// Handles the Click event of the deleteToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cowsToDelete = new List<Cow>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {

                cowsToDelete.Add((Cow)row.DataBoundItem);
            }
            var bindingList = (SortableBindingList<Cow>)dataGridView1.DataSource;
            foreach (var cow in cowsToDelete)
            {
                ObjectProvider.Instance.DeleteUser(cow.UserID);
                bindingList.Remove(cow);
                cows.Remove(cow);
            }
        } 
        #endregion

        #endregion

        private void SetFilter()
        {
            IEnumerable<Cow> filtered = cows;
            if (comboBoxUserType.SelectedItem is EnumData)
            {
                var userType = (UserType)((EnumData)comboBoxUserType.SelectedItem).Value;
                filtered = filtered.Where(u => u.UserType == userType);
            }

            if (!string.IsNullOrEmpty(textBoxUser.Text))
                filtered = filtered.Where(u => u.UserName.Contains(textBoxUser.Text));


            if (numericUpDownLevelFrom.Value > 0)
            {
                var levelFrom = (int)numericUpDownLevelFrom.Value;
                filtered = filtered.Where(u => u.Level >= levelFrom);
            }

            if (numericUpDownLevelTo.Value > 0)
            {
                var levelTo = (int)numericUpDownLevelTo.Value;
                filtered = filtered.Where(u => u.Level <= levelTo);
            }

            SetData(filtered.ToList());
        }

        private void SetData(IList<Cow> data)
        {
            dataGridView1.DataSource = new SortableBindingList<Cow>(data);
        }












    }
}
