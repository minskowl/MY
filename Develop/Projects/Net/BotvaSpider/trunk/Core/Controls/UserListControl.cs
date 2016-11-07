using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Automation;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.Core;
using Savchin.Text;
using RivalSource=BotvaSpider.Core.RivalSource;

namespace BotvaSpider.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserListControl : UserControl
    {
        /// <summary>
        /// Gets or sets a value indicating whether [use white list filter].
        /// </summary>
        /// <value><c>true</c> if [use white list filter]; otherwise, <c>false</c>.</value>
        public bool UseWhiteListFilter { get; set; }

        /// <summary>
        /// Gets or sets the fight machine.
        /// </summary>
        /// <value>The fight machine.</value>
        public MachineBase FightMachine { get; set; }

        /// <summary>
        /// Occurs when [list changed].
        /// </summary>
        public event EventHandler ListChanged;



        /// <summary>
        /// Initializes a new instance of the <see cref="UserListControl"/> class.
        /// </summary>
        public UserListControl()
        {
            InitializeComponent();
            var guilds = EnumHelper.GetValues(typeof (GuildType)).Except(new[] {(Enum)GuildType.None});
            foreach (var guild in guilds)
            {
                var item = new ToolStripMenuItem(guild.GetDescription());
                item.Tag = guild;
                item.Click+=new EventHandler(guildItem_Click);
                guildToolStripMenuItem.DropDownItems.Add(item);
            }
 
        }

 

        /// <summary>
        /// Adds the specified users.
        /// </summary>
        /// <param name="users">The users.</param>
        public void Add(IEnumerable<string> users)
        {
            var text = GetText(users);
            if (string.IsNullOrEmpty(text)) return;
            boxInput.AppendText(Environment.NewLine + text);
        }


        /// <summary>
        /// Shows the specified list.
        /// </summary>
        /// <param name="users">The users.</param>
        public void Show(IEnumerable<string> users)
        {
            boxInput.Text = GetText(users);
        }
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetUsers()
        {
            var result = User.ParseUserNameList(boxInput.Text);
            return UseWhiteListFilter ? result.Except(AppCore.AttackSettings.WhiteList) : result;
        }

        /// <summary>
        /// Raises the <see cref="E:ListChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnListChanged(EventArgs e)
        {
            EventHandler listChangedHandler = ListChanged;
            if (listChangedHandler != null) listChangedHandler(this, e);
        }

        /// <summary>
        /// Handles the TextChanged event of the boxInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void boxInput_TextChanged(object sender, EventArgs e)
        {
            OnListChanged(e);
        }

        #region Context Menu Handlers

        /// <summary>
        /// Handles the Click event of the UndoToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boxInput.Undo();
        }

        /// <summary>
        /// Handles the Click event of the cutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boxInput.Cut();
        }

        /// <summary>
        /// Handles the Click event of the copyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boxInput.Copy();
        }

        /// <summary>
        /// Handles the Click event of the fromClipBoardToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fromClipBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boxInput.Paste();
        }

        /// <summary>
        /// Handles the Click event of the deleteToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectionLength = boxInput.SelectionLength;
            if (selectionLength == 0) return;

            boxInput.Text = boxInput.Text.Remove(boxInput.SelectionStart, selectionLength);
        }

        /// <summary>
        /// Handles the Click event of the selectAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boxInput.SelectAll();
        }
        /// <summary>
        /// Handles the Click event of the guildItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void guildItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;
            var playerLevel = FightMachine.Player.Level;
            var filter = AppCore.AttackSettings.GetLevelFilter(RivalSource.FromFarm);
            var guild = (GuildType)item.Tag;
            var cristals = ObjectProvider.Instance.GetGuildUsers(guild,filter.CreateFull(playerLevel));
            Add(cristals);
        }
        /// <summary>
        /// Handles the Click event of the insertCrystalOwnersToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertCrystalOwnersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FightMachine == null || FightMachine.State == MachineState.Logouted)
            {
                MessageBox.Show(this, "Нужно сначала войти в игру.");
                return;
            }

            var playerLevel = FightMachine.Player.Level;
            var filter = AppCore.AttackSettings.GetLevelFilter(RivalSource.FromFarm);
            var cristals = ObjectProvider.Instance.GetCristalOwners(filter.CreateFull(playerLevel));
            Add(cristals);
        }

        /// <summary>
        /// Handles the Click event of the insertFromShtabToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertFromShtabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<string> names;
            //using (var form = new FormText())
            //{
            //    form.Text = "Вставьте список из штаба";
            //    if (form.ShowDialog() != DialogResult.OK) return;

            //    names = User.ParseUserNameList(form.Value);
            //}
            var text = Clipboard.GetText();
            if (!string.IsNullOrEmpty(text))
                Add(User.ParseUserNameList(text));
        }

        /// <summary>
        /// Handles the Click event of the insertTOPCowsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertTOPCowsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private string GetText(IEnumerable<string> users)
        {
            return StringUtil.Join((UseWhiteListFilter ? users.Except(AppCore.AttackSettings.WhiteList) : users), Environment.NewLine);
        }


    }
}
