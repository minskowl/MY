using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Data;

namespace BotvaSpider.Controls.Mining
{
    public partial class SaveMapControl : UserControl
    {
        public SaveMapControl()
        {
            InitializeComponent();

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
                ShowTop();
            }
        }

        private void ShowTop()
        {
            crystalMapResult1.Show(ObjectProvider.Instance.GetCrystalMapPositionsTop(boxIsSmall.Checked));
        }

        private void buttonStore_Click(object sender, EventArgs e)
        {
            var map = crystalMap1.GetResult();
 


            ObjectProvider.Instance.SaveCrystalMap(boxIsSmall.Checked, map);

            MessageBox.Show("Saved");
            crystalMap1.Clear();
            ShowTop();
        }

        private void boxIsSmall_CheckedChanged(object sender, EventArgs e)
        {
            crystalMap1.IsSmall = boxIsSmall.Checked;
            crystalMapResult1.IsSmall = boxIsSmall.Checked;
            ShowTop();
        }
    }
}