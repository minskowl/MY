using System;
using System.Drawing;
using System.Text;
using BotvaSpider.Core;
using BotvaSpider.Logging;
using Savchin.Forms.Docking;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Consoles
{


    public partial class OutputWindow : DockContent
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindow"/> class.
        /// </summary>
        public OutputWindow()
        {
            InitializeComponent();
            comboBoxLoggers.Setup(typeof(LoggerType));
            comboBoxLoggers.SelectedIndex = 0;

            boxLevelFrom.Setup(typeof(LogEntryType));
            boxLevelFrom.SelectedIndex =0;
        }






        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxLoggers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void comboBoxLoggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (EnumData)comboBoxLoggers.SelectedItem;
            logViewer.Show((LoggerType)selected.Value);

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the boxLevelFrom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxLevelFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (EnumData)boxLevelFrom.SelectedItem;
            logViewer.SetLevelFilter((LogEntryType)selected.Value);
        }





    }
}