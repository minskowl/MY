using System.Windows.Forms;
using BotvaSpider.Controls;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Tools.Core;
using Savchin.Core;
using Savchin.Forms.Core.Commands;
using Savchin.Threading;

namespace BotvaSpider.Tools.Commands
{
    class SearchGuildUsersCommand : Command
    {
        private readonly IObjectViewer viewer;
        private readonly IControllerSource source;
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchGuildUsersCommand"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="viewer">The viewer.</param>
        public SearchGuildUsersCommand(IControllerSource source, IObjectViewer viewer)
        {
            this.viewer = viewer;
            this.source = source;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter, object target)
        {
            viewer.Clear();
            IRange<int> levelRange;
            IRange<int> pageRange;
            GuildType guildType;
            Race race;
            bool import;
            using (var filter = new SearchUserFilter())
            {
                if (filter.ShowDialog(SearchUserFilter.DisplayMode.Guild) != DialogResult.OK) return;
                levelRange = filter.LevelRange;
                pageRange = filter.PageRange;
                guildType = (GuildType)filter.SecondValue;
                race = filter.Race;
                import = filter.Import;
            }

            var controller = source.Controller;
            new async(delegate
                          {
                              viewer.Display(string.Format("Начали искать {0} в гильдии {1}", race.GetDescription(), guildType.GetDescription()));

                              var links = controller.GetUsersLinksFromGuild(guildType, pageRange);

                              var counter = 0;
                              var count = links.Count;
                              viewer.Display("Всего в поиске " + count);
                              foreach (var link in links)
                              {
                                  var user = controller.GetUserByUrl(link);
                                  counter++;
                                  viewer.ShowStatus(counter + "/" + count);
                                  if (user == null) continue;
                                  if (user.Race != race) continue;
                                  if (levelRange != null && !levelRange.IsInRange(user.Level)) continue;


                                  user.Guild = guildType;
                                  user.UserType = UserType.Cow;

                                  viewer.Display(user);
                                  if (import) ImportUser(user);
                              }
                              if (pageRange == null)
                                  viewer.ShowStatus("Поиск окончен");
                              else
                                  viewer.ShowStatus("Поиск окончен на странице " + pageRange.To);
                          }
                );

        }
        UserImporter importer = new UserImporter();
        private void ImportUser(BotvaSpider.Data.User user)
        {
            importer.Import(user);
            ObjectProvider.Instance.SetGuild(user.UserID, user.Guild);
        }

    }
}