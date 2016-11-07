using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Data;

namespace BotvaSpider.Controls.Mining
{
    /// <summary>
    /// ShowVariantsControl
    /// </summary>
    public partial class ShowVariantsControl : UserControl
    {
        public ShowVariantsControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the CheckedChanged event of the boxIsSmall1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxIsSmall1_CheckedChanged(object sender, EventArgs e)
        {
            crystalMapSelector1.IsSmall = boxIsSmall1.Checked;
            bigCrystalMapResult1.IsSmall = boxIsSmall1.Checked;
        }

        /// <summary>
        /// Handles the Click event of the buttonShow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonShow_Click(object sender, EventArgs e)
        {
            var pattern = crystalMapSelector1.GetResult();
            if (pattern.Count == 0)
            {
                MessageBox.Show("Please check box");
                return;
            }

            var maps = ObjectProvider.Instance.GetCrystalMaps(boxIsSmall1.Checked);

            for (var i = 0; i < pattern.Count; i++)
            {
                var state = pattern[i];
                switch (state)
                {
                    case CrystalState.Exist:
                        maps = maps.Where(map => map.Contains(i + 1)).ToList();
                        break;
                    case CrystalState.NotExists:
                        maps = maps.Where(map => !map.Contains(i + 1)).ToList();
                        break;
                    case CrystalState.Undefined:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            var statistics = new Dictionary<int, int>();
            foreach (var map in maps)
            {
                foreach (var position in map)
                {
                    if(statistics.ContainsKey(position))
                    {
                        statistics[position]++;
                    }
                    else
                    {
                        statistics.Add(position,1);
                    }
                }
            }
            //  var variants = ObjectProvider.Instance.GetCrystalMapVariant(boxIsSmall1.Checked, attemptMap);
            bigCrystalMapResult1.Show(statistics);
            // GetCrystalMaps
        }
    }
}