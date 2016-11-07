using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using BotvaSpider.Configuration;

namespace BotvaSpider.Controls.Configuration.Accountant
{
    public partial class AccountantControl : UserControl
    {
        List<DataRow> data = new List<DataRow>();
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountantControl"/> class.
        /// </summary>
        public AccountantControl()
        {
            InitializeComponent();

            coulombSelector1.Text = "Прокачка кулона";
        }

        /// <summary>
        /// Shows the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Show(AccountantSettings settings)
        {
            boxInvestmentEnabled.Checked = settings.InvestmentEnabled;
            boxSearchEnabled.Checked = settings.SearchStrategy.Enabled;
            boxSoundEnabled.Checked = settings.SearchStrategy.SoundNotification;

            boxMinMoney.Value = settings.MinMoney;
            boxShopingInteval.Value = settings.ShopingInteval;
            coulombSelector1.SelectedCoulomb = settings.UpgradeCouloub;

            normalInvestmentStrategyControl.Show(settings.NormalStrategy);
            atackInvestmentStrategyControl.Show(settings.AlertStrategy);
            notificationSearcher.Show(settings.SearchStrategy.StuffConditions);
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(AccountantSettings settings)
        {
            settings.InvestmentEnabled = boxInvestmentEnabled.Checked;
            settings.SearchStrategy.Enabled = boxSearchEnabled.Checked;
            settings.SearchStrategy.SoundNotification = boxSoundEnabled.Checked;

            settings.MinMoney = (int)boxMinMoney.Value;
            settings.ShopingInteval = (int)boxShopingInteval.Value;
            settings.UpgradeCouloub = coulombSelector1.SelectedCoulomb;

            normalInvestmentStrategyControl.Save(settings.NormalStrategy);
            atackInvestmentStrategyControl.Save(settings.AlertStrategy);
            notificationSearcher.Save(settings.SearchStrategy.StuffConditions);
        }

    }
}