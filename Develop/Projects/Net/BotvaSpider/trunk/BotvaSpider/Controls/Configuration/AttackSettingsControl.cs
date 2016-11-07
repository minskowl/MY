
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;

namespace BotvaSpider.Controls.Configuration
{
    public partial class AttackSettingsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttackSettingsControl"/> class.
        /// </summary>
        public AttackSettingsControl()
        {
            InitializeComponent();
            boxAttackTimeShift.Maximum = 1000;
        }

        /// <summary>
        /// Levels the is valid.
        /// </summary>
        /// <returns></returns>
        public bool LevelIsValid()
        {
            //if (!levelFilterFarm.ValidateFilter() ||
            //    !levelFilterList.ValidateFilter() ||
            //    !levelFilterRandom.ValidateFilter())
            //    return false;
            var confirmMessage = new StringBuilder();

            if (checkBoxAllowLostGlory.Checked)
            {
                confirmMessage.AppendLine("Включен слив славы.");
            }

            //if (levelFilterList.LevelFilter.LevelTo > -1 ||
            //    levelFilterFarm.LevelFilter.LevelTo > -1 ||
            //    levelFilterRandom.LevelFilter.LevelTo > -1)
            //{
            //    confirmMessage.AppendLine("Включен набор опыта.");
            //}
            if (confirmMessage.Length > 0)
            {
                confirmMessage.AppendLine("Вы уверены, что хотите этого??");

                return MessageBox.Show(this,
                                       confirmMessage.ToString(),
                                       "Внимание !!!!!!!!!!!!",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Exclamation) == DialogResult.Yes;

            }
            return true;
        }


        /// <summary>
        /// Shows the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Show(AttackSettings settings)
        {

            boxAttackTimeShift.Value = settings.AttackTimeShift;
            boxMinSkillDifference.Value = settings.MinSkillDifference;
            boxMinBenefit.Value= settings.MinBenefit;

            boxIgnoreWarsClan.Checked = settings.IgnoreWarsClan;
            checkBoxAllowLostGlory.Checked = settings.AllowLostGlory;

            rivalSourcesControl1.Show(settings.RivalSources);
            
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(AttackSettings settings)
        {
            settings.IgnoreWarsClan = boxIgnoreWarsClan.Checked;
            settings.AllowLostGlory = checkBoxAllowLostGlory.Checked;
            settings.AttackTimeShift = (int)boxAttackTimeShift.Value;
            settings.MinSkillDifference = (int)boxMinSkillDifference.Value;
            settings.MinBenefit = (int)boxMinBenefit.Value;

            settings.RivalSources=rivalSourcesControl1.GetValue();
        }

    }
}
