using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Gears;
using Savchin.Forms.Helpers;
using RivalSource = BotvaSpider.Core.RivalSource;

namespace BotvaSpider.Controls.Configuration
{
    public partial class RivalSourcesControl : UserControl
    {
        private BindingList<DataRow> data;
        /// <summary>
        /// Initializes a new instance of the <see cref="RivalSourcesControl"/> class.
        /// </summary>
        public RivalSourcesControl()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Shows the specified sources.
        /// </summary>
        /// <param name="sources">The sources.</param>
        public void Show(List<RivalSourceSettings> sources)
        {
            columnSource.Setup(typeof(RivalSource));
            ColumnCoulomb.Setup(typeof(Coulomb));

            data = new BindingList<DataRow>(sources.Select(e => new DataRow
                                                                    {
                                                                        Source = e.Source,
                                                                        Enabled = e.Enabled,
                                                                        Coulomb = e.Coulomb,
                                                                        MaxAttemptCount = e.MaxAttemptCount,
                                                                        LevelFrom = e.LevelFilter.LevelFrom,
                                                                        LevelTo = e.LevelFilter.LevelTo
                                                                    }).ToList());

            dataGridView1.DataSource = data;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public List<RivalSourceSettings> GetValue()
        {
            return data.Select(e => new RivalSourceSettings
                                        {
                                            Source = e.Source,
                                            Enabled = e.Enabled,
                                            MaxAttemptCount = e.MaxAttemptCount,
                                            Coulomb = e.Coulomb,
                                            LevelFilter = new LevelFilter
                                                              {
                                                                  LevelFrom = e.LevelFrom,
                                                                  LevelTo = e.LevelTo
                                                              }
                                        }).ToList();

        }

        #region Context Menu
        /// <summary>
        /// Handles the Click event of the upToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count == 0) return;


            var index = dataGridView1.SelectedRows[0].Index;
            if (index == 0) return;

            var dataItem = data[index];
            data.RemoveAt(index);
            data.Insert(index - 1, dataItem);
            dataGridView1.Rows[index - 1].Selected = true;
        }

        /// <summary>
        /// Handles the Click event of the downToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count == 0) return;


            var index = dataGridView1.SelectedRows[0].Index;
            if (index + 1 == data.Count) return;

            var dataItem = data[index];
            data.RemoveAt(index);
            data.Insert(index + 1, dataItem);
            dataGridView1.Rows[index + 1].Selected = true;
        } 

        #endregion

        private class DataRow
        {


            public RivalSource Source { get; set; }
            public bool Enabled { get; set; }
            public int MaxAttemptCount { get; set; }
            public int LevelFrom { get; set; }
            public int LevelTo { get; set; }
            public Coulomb Coulomb { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="DataRow"/> class.
            /// </summary>
            public DataRow()
            {
                Coulomb = Coulomb.Undefined;
            }
        }




    }
}
