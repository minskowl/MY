using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Configuration;

namespace BotvaSpider.Controls.Configuration.Accountant
{
    public partial class InvestmentStrategyControl : UserControl
    {
        public InvestmentStrategyControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Shows the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Show(InvestmentStrategySettings settings)
        {
            investmentStrategiesChekedList1.Value = settings.Type;
            skillCombo1.SelectedSkill = settings.SelectedSkill;
            tradeSearcherControl1.Show(settings.StuffConditions);
            boxEnabled.Checked = settings.Enabled;
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(InvestmentStrategySettings settings)
        {
            settings.Type = investmentStrategiesChekedList1.Value;
            settings.SelectedSkill = skillCombo1.SelectedSkill;
            tradeSearcherControl1.Save(settings.StuffConditions);
            settings.Enabled = boxEnabled.Checked;
        }

    }
}