using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Core;

namespace BotvaSpider.Controls.Configuration
{
    public partial class UserSettings : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
        {
            InitializeComponent();

            listServers.Items.AddRange(AppCore.AppSettings.Servers.ToArray());
        }

        /// <summary>
        /// Shows the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Show(BotvaSettings settings)
        {
            boxAutoThreat.Checked = settings.AutoTreat;

            boxShowAllerts.Checked = settings.ShowAllerts;
            boxDebugger.Checked = settings.AllowDebugger;
            checkBoxUsePatrol.Checked = settings.UsePatrol;

            boxCoolStatus.Checked = settings.AcountSettings.CoolStatus;
            boxEmail.Text = settings.AcountSettings.Email;
            boxPassword.Text = settings.AcountSettings.Password;
            listServers.Text = settings.AcountSettings.Server;

            boxMaxInternetErrors.Value = settings.MaxInternetErrors;
            boxMaxDangerousErrors.Value = settings.MaxDangerousErrors;
           
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(BotvaSettings settings)
        {
            settings.UsePatrol = checkBoxUsePatrol.Checked;
            settings.AutoTreat = boxAutoThreat.Checked;
    
            settings.ShowAllerts = boxShowAllerts.Checked;
            settings.AllowDebugger = boxDebugger.Checked;

            settings.AcountSettings.CoolStatus = boxCoolStatus.Checked;
            settings.AcountSettings.Email = boxEmail.Text;
            settings.AcountSettings.Password = boxPassword.Text;
            settings.AcountSettings.Server = listServers.Text;

            settings.MaxInternetErrors = (int)boxMaxInternetErrors.Value;
            settings.MaxDangerousErrors = (int)boxMaxDangerousErrors.Value;
        }
    }
}
