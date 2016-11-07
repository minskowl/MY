using System;
using System.Collections.Generic;
using System.Linq;
using BotvaSpider.Automation;
using BotvaSpider.Automation.Fights;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Fighting;
using Savchin.Forms.Docking;
using RivalSource=BotvaSpider.Core.RivalSource;


namespace BotvaSpider.Consoles
{
    public partial class FightingConsole : DockContent
    {
        #region Properties
        private readonly SimpleAction RefreshFightsDelegates;
        private readonly FightMachine machine;


        private readonly List<FightResult> fights = new List<FightResult>();
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFighterConsole"/> class.
        /// </summary>
        public FightingConsole()
        {


            InitializeComponent();

            RefreshFightsDelegates = RefreshFights;

            machine = new FightMachine();
            machine.StateChanged += OnStateChanged;
            machine.FightOccured += machine_FightOccured;



            AppCore.GameSettings.SettingsChanged += GameSettings_SettingsChanged;
            SetCurrentSettings();

            userHotListControl.UseWhiteListFilter = true;
            userHotListControl.FightMachine = machine;


        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            AppCore.GameSettings.SettingsChanged -= GameSettings_SettingsChanged;
            machine.Stop();
         
            machine.Dispose();

            base.OnClosing(e);
        }
        #endregion

        #region Event Handlers

        #region Button Handlers

        /// <summary>
        /// Handles the Click event of the buttonFight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonFight_Click(object sender, EventArgs e)
        {
            buttonFight.Enabled = false;
            buttonStopFight.Enabled = true;

            machine.Start();
        }

        /// <summary>
        /// Handles the Click event of the buttonStopFight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonStopFight_Click(object sender, EventArgs e)
        {
            machine.Stop();
 
            buttonFight.Enabled = true;
            buttonStopFight.Enabled = false;

        }






        #endregion

        #region Machine Events
        private delegate void SetStateHandler(string message);
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

        void machine_FightOccured(object sender, FightResultEventArgs e)
        {
            fights.Add(e.Result);
            if (InvokeRequired)
            {
                Invoke(RefreshFightsDelegates);
            }
            else
            {
                RefreshFights();
            }

        }

        #endregion

        #region Menu Handlers





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
        #endregion

        #region Settings Handlers
        /// <summary>
        /// Handles the CheckedChanged event of the boxAttackByRandom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxAttackByRandom_CheckedChanged(object sender, EventArgs e)
        {
            AppCore.AttackSettings.GetSettings(RivalSource.FromRandom).Enabled = boxAttackByRandom.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the boxAttackByFarm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxAttackByFarm_CheckedChanged(object sender, EventArgs e)
        {
            AppCore.AttackSettings.GetSettings(RivalSource.FromFarm).Enabled = boxAttackByFarm.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the boxAttackByList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxAttackByList_CheckedChanged(object sender, EventArgs e)
        {
            AppCore.AttackSettings.GetSettings(RivalSource.FromList).Enabled = boxAttackByList.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the boxInvestmentEnabled control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxInvestmentEnabled_CheckedChanged(object sender, EventArgs e)
        {
            AppCore.AccountantSettings.InvestmentEnabled = boxInvestmentEnabled.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the checkBoxUsePatrol control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void checkBoxUsePatrol_CheckedChanged(object sender, EventArgs e)
        {
            AppCore.GameSettings.BotvaSettings.UsePatrol = checkBoxUsePatrol.Checked;
        }
        #endregion


        /// <summary>
        /// Handles the ListChanged event of the userHotListControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void userHotListControl_ListChanged(object sender, EventArgs e)
        {
            machine.TopKills.Clear();
            machine.TopKills.AddRange(userHotListControl.GetUsers());
        }

        /// <summary>
        /// Handles the SettingsChanged event of the GameSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void GameSettings_SettingsChanged(object sender, EventArgs e)
        {
            SetCurrentSettings();
        }

        #endregion




        private void SetCurrentSettings()
        {
            var settings = AppCore.AttackSettings;
            boxAttackByFarm.Checked = settings.GetSettings(RivalSource.FromFarm).Enabled;
            boxAttackByList.Checked = settings.GetSettings(RivalSource.FromList).Enabled;
            boxAttackByRandom.Checked = settings.GetSettings(RivalSource.FromRandom).Enabled;

            checkBoxUsePatrol.Checked = AppCore.BotvaSettings.UsePatrol;
            boxInvestmentEnabled.Checked = AppCore.AccountantSettings.InvestmentEnabled;

        }

        private void RefreshFights()
        {
            milkingFarmStateControl.Show(machine.Farm.Cows);

            labelFightCount.Text = GetStatistics(fights);
            labelFarmStatistics.Text = "Ферма " + GetStatistics(fights.Where(f => f.Rival.Source == RivalSource.FromFarm).ToArray());
            labelRandomStatistics.Text = "Случайных " + GetStatistics(fights.Where(f => f.Rival.Source != RivalSource.FromFarm).ToArray());
            userHotListControl.Show(machine.TopKills);

            labelBadErrorCount.Text = "Ошибок бота: " + machine.BadErrorCount;
            labelErrorCount.Text = "Ошибок сервера: " + machine.ErrorCount;
        }

        private string GetStatistics(IEnumerable<FightResult> data)
        {
            return string.Format("Битв: {0} Золота: {1}  Кристалов: {2}",
             data.Count(), data.Sum(f => f.Money), data.Sum(f => f.Crystals));
        }




        private void EnterToGame()
        {
            machine.Login();
            machine.UpdatePlayerStatus();
            labelState.Text = machine.State.ToString();
        }
        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="message">The message.</param>
        private void AddLog(string message)
        {
            AppCore.LogFights.Debug(message);
        }














    }
}
