using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Automation;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Tools.Commands;
using BotvaSpider.Tools.Core;
using Savchin.Forms;
using Savchin.Forms.Core.Commands;


namespace BotvaSpider.Consoles
{
    public partial class TopSearchConsole : ControllerConsole, IObjectViewer
    {
        readonly UserImporter importer = new UserImporter();




        /// <summary>
        /// Initializes a new instance of the <see cref="TopSearchConsole"/> class.
        /// </summary>
        public TopSearchConsole()
        {
            InitializeComponent();


            TabText = "Поиск в ТОПе";

            importGuildUsersToolStripMenuItem.BindCommand(new SearchGuildUsersCommand(this, this));

        }



        #region Buttons Handlers



        private void button1_Click(object sender, EventArgs e)
        {


            var logs = Controller.GetFightLogsUrls(GetFightsDates());
            AppCore.LogOutput.Debug("Get Logs  " + logs.Count);
            Application.DoEvents();
            var i = 1;
            foreach (var log in logs)
            {
                AppCore.LogOutput.Debug("Get Log #" + i + " " + log);
                try
                {
                    var result = Controller.GetFightResult(log);
                    AppCore.LogOutput.Debug("Результаты боя " + result);
                    Application.DoEvents();
                    importer.Import(result);
                    AppCore.LogOutput.Debug("Result imported ");
                }
                catch (Exception ex)
                {
                    AppCore.LogSystem.Error("Error import result ", ex);
                }
                i++;
                Application.DoEvents();
            }
        }

        private List<DateTime> GetFightsDates()
        {
            var exists = ObjectProvider.Instance.GetFights();
            var dates = new List<DateTime>();
            foreach (var fight in exists)
            {
                dates.Add(fight.Date);
            }
            return dates;
        }

        #endregion




        /// <summary>
        /// Handles the Click event of the importClanToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void importClanToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var users = Controller.GetUsersFromClan(textBoxUrl.Text);
            foreach (var user in users)
            {
                importer.Import(user);
            }
            MessageBox.Show("Клан " + textBoxUrl.Text + " закачан.", "Импорт клана");
        }

        /// <summary>
        /// Handles the Click event of the getRivalsFromClanToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void getRivalsFromClanToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var clans = textBoxUrl.Text.Trim().Split(new[] { ',' });

            var player = new Player(Controller);
            player.Update();
            foreach (var clan in clans)
            {
                getRivalFromClan(player, clan);
            }

            MessageBox.Show("Finish");
        }
        /// <summary>
        /// Handles the Click event of the importTopClansToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void importTopClansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var page = 0;
            if (!int.TryParse(textBoxUrl.Text, out page))
            {
                MessageBox.Show(this, "Введите номер ипортируемой страницы кланов", "Ошибка");
                return;
            }
            var clanUrls = Controller.GetTopGloryClans(page);


            for (var i = 0; i < clanUrls.Count; i++)
            {
                var users = Controller.GetUsersFromClanUrl(clanUrls[i]);
                Application.DoEvents();
                foreach (var user in users)
                {
                    importer.Import(user);
                }
                labelStatus.Text = string.Format("Кланов за импортировано {0} из {1}.", (i + 1), clanUrls.Count);
                Application.DoEvents();
            }

            MessageBox.Show(this, "Страничка " + page + " импортирована");
        }

        private void getRivalFromClan(Player player, string clanName)
        {
            var users = ObjectProvider.Instance.GetUsersByClanName(clanName);
            foreach (var user in users)
            {
                if (player.Level > user.Level && player.Level - user.Level < 3)//&& player.CompareTo(user) > 0)
                {
                    AppCore.LogOutput.Debug(user.Name + " " + user.Level);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the searchClanWithMoneyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void searchClanWithMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var clans = Controller.GetClansFromTop(Race.Sheeps, TopSearchSort.Glory, 1, 2);

            var filtered = clans.Where(c => c.BarrackCapacity >= 15 && c.Treasury > 300000)
                .OrderByDescending(c => c.Treasury).ToArray();

            var builder = new StringBuilder();

            foreach (var clan in filtered)
            {
                builder.AppendFormat("{0} [{1}] {2}({3}) {4} {5}",
                    clan.Name,
                    clan.Tag,
                    clan.Soldiers,
                    clan.BarrackCapacity,
                    clan.Treasury,
                    Environment.NewLine);
            }

            textBoxOut.Text = builder.ToString();
        }

        /// <summary>
        /// Handles the Click event of the importTopLoosersToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void importTopLoosersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = new Player(Controller);
            player.Update();

            new SearchTopUsersCommand(this, this, player).Execute(null);
        }


        void IObjectViewer.Clear()
        {
            textBoxOut.Clear();
        }

        void IObjectViewer.Display(object obj)
        {
            var text = string.Empty;

            if (obj is User)
                text = ((User)obj).Name;
            else
                text = obj.ToString();
            AppendText(text);
        }

        /// <summary>
        /// Shows the status.
        /// </summary>
        /// <param name="status">The status.</param>
        public void ShowStatus(string status)
        {
            if (labelStatus.InvokeRequired)
            {
                labelStatus.Invoke((TextDelegate)ShowStatus, new[] { status });
                return;
            }
            labelStatus.Text = status;
            Application.DoEvents();
        }

   

        /// <summary>
        /// Appends the text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void AppendText(string text)
        {
            if (textBoxOut.InvokeRequired)
            {
                textBoxOut.Invoke((TextDelegate)AppendText, new[] { text });
                return;
            }
            textBoxOut.AppendText(text + Environment.NewLine);
            Application.DoEvents();
        }


    }
}
