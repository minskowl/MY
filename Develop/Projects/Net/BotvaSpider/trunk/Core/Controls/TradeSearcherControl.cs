using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Configuration;

namespace BotvaSpider.Controls.Configuration.Accountant
{
    public partial class TradeSearcherControl : UserControl
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="TradeSearcherControl"/> class.
        /// </summary>
        public TradeSearcherControl()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Shows the specified conditions.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        public void Show(List<StuffSearchCondition> conditions)
        {
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(conditions.ToArray());
            for (var i = 0; i < checkedListBox1.Items.Count; i++ )
            {
                var item = (StuffSearchCondition)checkedListBox1.Items[i];
                checkedListBox1.SetItemChecked(i, item.Enabled);
            }

        }
        /// <summary>
        /// Saves the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Save(List<StuffSearchCondition> list)
        {
            list.Clear();
            list.AddRange(checkedListBox1.Items.Cast<StuffSearchCondition>());

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the checkedListBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void checkedListBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            propertyGrid1.SelectedObject = checkedListBox1.SelectedItem;
        }

        /// <summary>
        /// Handles the Click event of the addToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void addToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var str = InputText(string.Empty);
            if (string.IsNullOrEmpty(str)) return;

            checkedListBox1.Items.Add(new StuffSearchCondition { ItemName = str });
        }

        /// <summary>
        /// Handles the Click event of the editToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void editToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (checkedListBox1.SelectedIndex < 0) return;
            var item = (StuffSearchCondition)checkedListBox1.SelectedItem;
            var str = InputText(item.ItemName);
            if (string.IsNullOrEmpty(str)) return;

            item.ItemName = str;
        }

        /// <summary>
        /// Handles the Click event of the deleteToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void deleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (checkedListBox1.SelectedIndex < 0) return;
            checkedListBox1.Items.RemoveAt(checkedListBox1.SelectedIndex);
        }

        /// <summary>
        /// Inputs the text.
        /// </summary>
        /// <param name="initilaValue">The initila value.</param>
        /// <returns></returns>
        private string InputText(string initilaValue)
        {
            using (var form = new FormText())
            {
                form.Text = "Чего покупаем.";
                form.Multiline = false;
                form.Height = 100;
                form.Value = initilaValue;

                if (form.ShowDialog() != DialogResult.OK) return null;
                return form.Value.Trim();
            }

        }

        /// <summary>
        /// Handles the ItemCheck event of the checkedListBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ItemCheckEventArgs"/> instance containing the event data.</param>
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = (StuffSearchCondition)checkedListBox1.Items[e.Index];
            item.Enabled = e.NewValue == CheckState.Checked;
        }



    }
}