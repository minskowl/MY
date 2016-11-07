using System;
using BotvaSpider.Automation;
using BotvaSpider.Automation.Mining;
using BotvaSpider.Commands;
using BotvaSpider.Core;
using Savchin.Forms.Core.Commands;
using Savchin.Forms.Docking;

namespace BotvaSpider.Consoles
{
    public partial class MiningConsole : DockContent
    {

        private MiningMachine machine;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiningConsole"/> class.
        /// </summary>
        public MiningConsole()
        {
            InitializeComponent();

            TabText = "Шахта";

            machine = new MiningMachine();
            machine.StateChanged += OnStateChanged;

            mineCristalDistrutionToolStripMenuItem.BindCommand(new ShowMineDistributionStatisticsCommand());
           showMineTotalStatisticsToolStripMenuItem.BindCommand(new ShowMineTotalStatisticsCommand());
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //AppCore.GameSettings.SettingsChanged -= GameSettings_SettingsChanged;
            machine.Stop();
            machine.Dispose();

            base.OnClosing(e);
        }

        private delegate void SetStateHandler(string message);
        /// <summary>
        /// Called when [state changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BotvaSpider.Automation.MachineStateEventArgs"/> instance containing the event data.</param>
        private void OnStateChanged(object sender, MachineStateEventArgs e)
        {
            var message = machine.GetStatusMessage();
            if (labelState.InvokeRequired)
            {
                labelState.Invoke(new SetStateHandler(SetState), message);
            }
            else
            {
                SetState(message);
            }
        }

        private void SetState(string message)
        {
            labelState.Text = message;
            AddLog(message);
        }

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="message">The message.</param>
        private void AddLog(string message)
        {
            AppCore.LogMine.Debug(message);
        }

        private void buttonFight_Click(object sender, EventArgs e)
        {
            buttonFight.Enabled = false;
            buttonStopFight.Enabled = true;

            machine.Start();
        }

        private void buttonStopFight_Click(object sender, EventArgs e)
        {
            machine.Stop();

            buttonFight.Enabled = true;
            buttonStopFight.Enabled = false;
        }

        private void hideBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (machine == null || machine.Controller == null) return;

            if (hideBrowserToolStripMenuItem.Tag.ToString() == "Hide")
            {
                machine.Controller.HideBrowser();
                hideBrowserToolStripMenuItem.Text = "Показать браузер";
                hideBrowserToolStripMenuItem.Tag = "Show";
            }
            else
            {
                machine.Controller.ShowBrowser();
                hideBrowserToolStripMenuItem.Text = "Спрятать браузер";
                hideBrowserToolStripMenuItem.Tag = "Hide";
            }
        }


    }
}
