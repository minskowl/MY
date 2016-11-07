using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Sudoku
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            comboBoxSize.Items.AddRange(new object[]
            {
            Difficulty.F4, Difficulty.F6, Difficulty.F9,Difficulty.F12,Difficulty.F16


            }
        );
        }

        /// <summary>
        /// Handles the Click event of the buttonSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonSize_Click(object sender, EventArgs e)
        {
            var value = (Difficulty)comboBoxSize.SelectedItem;
            map1.Difficulty = value;
            field1.Difficulty = value;
        }

        /// <summary>
        /// Handles the Click event of the buttonCheck control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonCheck_Click(object sender, EventArgs e)
        {
            map1.ShowAvalilible(field1.Value);
            //if (!map1.HasVariants)
            //{
            //    field1.Items.Remove(field1.SelectedItem);
            //}
        }

        /// <summary>
        /// Handles the Click event of the saveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK) return;
            map1.GetSnapshot().Save(saveFileDialog1.FileName);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
            try
            {
                var game = Game.Load(openFileDialog1.FileName);
                map1.LoadFromSnapshot(game);
                field1.Difficulty = game.Difficulty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error load game");
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
        
        }

        private void clearAvailibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
    map1.ClearAvaliliblity();
        }

        private void clearValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.ClearValues();
        }

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.ValidateValues();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.AutoFind();
        }

      
    }
}