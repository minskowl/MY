using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;

namespace BotvaSpider.Controls.Configuration
{
    public partial class FormSettings : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormSettings"/> class.
        /// </summary>
        public FormSettings()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Edits the settings.
        /// </summary>
        public static DialogResult EditSettings()
        {
            using (var form = new FormSettings())
            {
                return form.ShowDialog();
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            boxAutoDisguise.Checked = AppCore.BotvaSettings.AutoDisguise;
            boxMinEmptySlots.Value = AppCore.BotvaSettings.Wardrobe.MinEmptySlots;

            fightListControl1.CowsInfo = new BindingList<CowInfo>(AppCore.AttackSettings.Cows);

            mineSettingsControl1.Show(AppCore.MinerSettings);
            userSettings.Show(AppCore.BotvaSettings);
            accountantControl1.Show(AppCore.AccountantSettings);
            userListsControl1.Show(AppCore.BotvaSettings);
            wardrobeSettingsControl1.Show(AppCore.BotvaSettings);
            attackSettingsControl1.Show(AppCore.AttackSettings);
            scheduleControl1.Show(AppCore.BotvaSettings.Schedule);
        }

        /// <summary>
        /// Handles the Click event of the buttonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!attackSettingsControl1.LevelIsValid()) return;


            Save(AppCore.GameSettings.BotvaSettings);

            mineSettingsControl1.Save(AppCore.MinerSettings);
            userListsControl1.Save(AppCore.BotvaSettings);
            wardrobeSettingsControl1.Save(AppCore.BotvaSettings);
            accountantControl1.Save(AppCore.AccountantSettings);
            userSettings.Save(AppCore.BotvaSettings);
            attackSettingsControl1.Save(AppCore.AttackSettings);
            AppCore.BotvaSettings.Schedule = scheduleControl1.GetSchedule();

            AppCore.GameSettings.Save();
            DialogResult = DialogResult.OK;

            Close();
        }



        #region Settings

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        private void Save(BotvaSettings settings)
        {
            settings.AutoDisguise = boxAutoDisguise.Checked;
            AppCore.BotvaSettings.Wardrobe.MinEmptySlots = (int)boxMinEmptySlots.Value;

        }

        #endregion


        /// <summary>
        /// Handles the CheckedChanged event of the boxAutoDisguise control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxAutoDisguise_CheckedChanged(object sender, EventArgs e)
        {
            wardrobeSettingsControl1.Enabled = boxAutoDisguise.Checked;
        }

        private void wardrobeSettingsControl1_Load(object sender, EventArgs e)
        {

        }









    }
}