using System.Collections.Generic;
using System.Windows.Forms;
using BotvaSpider.Farming;
using Savchin.Collection.Generic;

namespace BotvaSpider.Fighter
{
    public partial class FarmStateControlControl : UserControl
    {
        public FarmStateControlControl()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }
        /// <summary>
        /// Shows the specified cows.
        /// </summary>
        /// <param name="cows">The cows.</param>
        public void Show(IList<Cow> cows)
        {
            dataGridView1.DataSource = new SortableBindingList<Cow>(cows);
        }
    }
}