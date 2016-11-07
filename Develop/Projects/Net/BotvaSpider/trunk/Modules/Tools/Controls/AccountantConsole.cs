using System;
using System.Data;
using System.Windows.Forms;
using BotvaSpider.BookKeeping;
using BotvaSpider.Core;

namespace BotvaSpider.Consoles
{
    public partial class AccountantConsole : ControllerConsole
    {
        public AccountantConsole()
        {
            InitializeComponent();
            TabText = "Áóõãàëòåðèÿ";
        }

        /// <summary>
        /// Handles the Click event of the importClanToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void importClanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var weeks=100;
            int.TryParse(textBoxWeeks.Text, out weeks);
            
            var àccountant = new Accountant(AppCore.LogOutput);
            var result = àccountant.GetClanDepositsByWeek(weeks);
            //Controller.

            var data = new DataTable();
            data.Columns.Add("User", typeof (string));
            data.Columns.Add("Week", typeof(int));
            data.Columns.Add("Money", typeof(int));
            foreach (var pair in result)
            {
                foreach (var weekMoney in pair.Value)
                {
                    data.Rows.Add(new object[] {pair.Key, weekMoney.Key, weekMoney.Value});
                }
            }
            data.DefaultView.Sort = "Week DESC, Money DESC";
            dataGridView1.DataSource = data;
            
            MessageBox.Show("Finish", "Operation");
        }
    }
}
