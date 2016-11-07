using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Gears;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls.Configuration
{
    public partial class WardrobeSettingsControl : UserControl
    {
        private List<DataRow> data;
        public WardrobeSettingsControl()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }


        /// <summary>
        /// Shows the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Show(BotvaSettings settings)
        {
            columnAction.Setup(typeof(PlayerAction));
            columnCoulomb.Setup(typeof(Coulomb));
  
            data = settings.Wardrobe.Select(e => new DataRow { Action = e.Key, Coulomb = e.Value }).ToList();

            dataGridView1.DataSource = new BindingList<DataRow>(data);
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(BotvaSettings settings)
        {

            settings.Wardrobe.Clear();
            try
            {
                data.ForEach(e => settings.Wardrobe.Add(e.Action, e.Coulomb));
            }
            catch
            {

            }
        }

        public class DataRow
        {
            public PlayerAction Action { get; set; }
            public Coulomb Coulomb { get; set; }

            public DataRow()
            {
                Action = PlayerAction.Fight;
                Coulomb = Coulomb.Undefined;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
