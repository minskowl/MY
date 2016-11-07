using System;
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
    class SearchTopUsersCommand : Command
    {
        private readonly IObjectViewer viewer;
        private readonly IControllerSource source;
        private readonly Player player;
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchGuildUsersCommand"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="viewer">The viewer.</param>
        public SearchTopUsersCommand(IControllerSource source, IObjectViewer viewer, Player player)
        {
            this.viewer = viewer;
            this.source = source;
            this.player = player;
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
            TopSearchSort sort;
            int minSkillDif;

            Race race;
            bool import;
            using (var filter = new SearchUserFilter())
            {


                if (filter.ShowDialog(SearchUserFilter.DisplayMode.TopSearch) != DialogResult.OK) return;
                levelRange = filter.LevelRange;
                pageRange = filter.PageRange;
                minSkillDif = filter.SkillDifference;
                race = filter.Race;
                sort = (TopSearchSort)filter.SecondValue;
                import = filter.Import;
            }

            var controller = source.Controller;
            new async(delegate
                          {
                              viewer.Display("Начали искать топ " + sort.GetDescription());

                              var links = controller.GetTopUsersLinks(sort,race, pageRange);

                              var counter = 0;
                              var count = links.Count;
                              viewer.Display("Всего в поиске " + count);
                              foreach (var link in links)
                              {
                                  try
                                  {
                                      var user = controller.GetUserByUrl(link);
                                      counter++;
                                      viewer.ShowStatus(counter + "/" + count);
                                      if (user == null) continue;
                                      if (user.Race != race) continue;
                                      if (levelRange != null && !levelRange.IsInRange(user.Level)) continue;
                                      if (player.GetSkillDifference(user) < minSkillDif) continue;


                                      //   user.Guild = guildType;
                                      user.UserType = UserType.Cow;

                                      viewer.Display(user);
                                      if (import) ImportUser(user);
                                  }
                                  catch (Exception ex)
                                  {
                                      AppCore.LogSystem.Warn("Ошибка чтения игрока", link);


                                  }
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