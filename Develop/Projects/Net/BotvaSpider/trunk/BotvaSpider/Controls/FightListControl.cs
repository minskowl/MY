using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Data;
using BotvaSpider.Gears;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls
{
    public partial class FightListControl : UserControl
    {
        private BindingList<CowInfo> data;

        /// <summary>
        /// Gets or sets the cows info.
        /// </summary>
        /// <value>The cows info.</value>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public BindingList<CowInfo> CowsInfo
        {
            get { return data; }
            set
            {
                data = value;
                dataGridView1.DataSource = data;

            }
        }

        public FightListControl()
        {
            InitializeComponent();

            columnCoulomb.DisplayMember = "Text";
            columnCoulomb.ValueMember = "Value";
            columnCoulomb.DataPropertyName = "Coulomb";
            columnCoulomb.DataSource = new EnumData[]
                                           {
                                               new EnumData(Coulomb.None),
                                               new EnumData(Coulomb.Undefined),
                                               new EnumData(Coulomb.CrystalThief),
                                               new EnumData(Coulomb.SmartBaby), 
                                               new EnumData(Coulomb.TrippleHoof),
                                               new EnumData(Coulomb.Attacker),
                                               new EnumData(Coulomb.Kakdams),
                                               new EnumData(Coulomb.BigPaunch),
                                               new EnumData(Coulomb.Unscrewer),
                                           };

        }

        /// <summary>
        /// Handles the Click event of the fillToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> names;
            using (var form = new FormText())
            {
                form.Text = "Вставьте список из штаба";
                if (form.ShowDialog() != DialogResult.OK) return;

                names = User.ParseUserNameList(form.Value);
            }
            foreach (var name in names)
            {
                data.Add(new CowInfo { UserName = name, Coulomb = Coulomb.Undefined });
            }
        }

        /// <summary>
        /// Handles the Click event of the clearToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data.Clear();
        }

    }
}
