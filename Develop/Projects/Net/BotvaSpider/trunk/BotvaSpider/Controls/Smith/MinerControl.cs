using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Configuration;

namespace BotvaSpider.Controls
{
    public partial class MinerControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinerControl"/> class.
        /// </summary>
        public MinerControl()
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
            if (DesignMode) return;


            LoadControl();


        }

        private void LoadControl()
        {
            saperControl.LoadMovie(0, Path.Combine(AppSettings.ApplicatioPath, "miner.swf"));
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            saperControl.LoadMovie(0, string.Empty);
            LoadControl();
          
        }
    }
}
