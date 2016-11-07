using System;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Data;
using Savchin.Text;

namespace BotvaSpider.Controls.Configuration
{
    public partial class UserListsControl : UserControl
    {
        private bool whiteListChanged = false;
        private bool bastardListChanged = false;
        private bool whiteClanListChanged = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserListsControl"/> class.
        /// </summary>
        public UserListsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Show(BotvaSettings settings)
        {
            textBoxWhite.Text = StringUtil.Join(settings.AttackSettings.WhiteList, Environment.NewLine);
            boxWhiteClans.Text = StringUtil.Join(settings.AttackSettings.ClanWhiteList, Environment.NewLine);
            textBoxBastards.Text = StringUtil.Join(settings.BastardList, Environment.NewLine);
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(BotvaSettings settings)
        {
            if (whiteListChanged)
            {
                settings.AttackSettings.WhiteList.Clear();
                settings.AttackSettings.WhiteList.AddRange(User.ParseUserNameList(textBoxWhite.Text));
            }

            if (bastardListChanged)
            {
                settings.BastardList.Clear();
                settings.BastardList.AddRange(User.ParseUserNameList(textBoxBastards.Text));
            }

            if (whiteClanListChanged)
            {
                settings.AttackSettings.ClanWhiteList.Clear();
                settings.AttackSettings.ClanWhiteList.AddRange(boxWhiteClans.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the textBoxWhite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBoxWhite_TextChanged(object sender, EventArgs e)
        {
            whiteListChanged = true;
        }

        /// <summary>
        /// Handles the TextChanged event of the textBoxBastards control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBoxBastards_TextChanged(object sender, EventArgs e)
        {
            bastardListChanged = true;
        }

        /// <summary>
        /// Handles the TextChanged event of the boxWhiteClans control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxWhiteClans_TextChanged(object sender, EventArgs e)
        {
            whiteClanListChanged = true;
        }
    }
}
