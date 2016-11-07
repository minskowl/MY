using System;
using BotvaSpider.Data;

namespace BotvaSpider.Core
{
    /// <summary>
    /// CowsSearcher
    /// </summary>
    public class CowsSearcher
    {
        public delegate bool StopSearchDelegate();
        /// <summary>
        /// Gets or sets the stop search.
        /// </summary>
        /// <value>The stop search.</value>
        public StopSearchDelegate StopSearch { get; set; }

        private readonly GameController controller;
        private readonly UserImporter importer;
        /// <summary>
        /// Initializes a new instance of the <see cref="CowsSearcher"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="importer">The importer.</param>
        public CowsSearcher(GameController controller, UserImporter importer)
        {
            this.controller = controller;
            this.importer = importer;
        }

        /// <summary>
        /// Starts the search.
        /// </summary>
        public void StartSearch()
        {
            try
            {

                var start = (int)'a';
                var end = (int)'z';
                bool started = true;
                for (var first = start; first <= end; first++)
                    for (var second = start; second <= end; second++)
                    {

                        if (started)
                        {
                            started = false;
                            var tmp = AppCore.GameSettings.BotvaSettings.CowSearchCounter;
                            if (!string.IsNullOrEmpty(tmp) && tmp.Length > 1)
                            {
                                first = tmp[0];
                                second = tmp[1];
                                continue;
                            }
                        }
                        var searchText = ((char)first).ToString() + ((char)second);
                        AppCore.LogOutput.Debug( "Start Import " + searchText);
                        var users = controller.SearchUsers(searchText);
                        foreach (var user in users)
                        {

                            try
                            {
                                //if (Math.Abs(user.Level - machine.Player.Level) < 3)
                                if (user.Level > 3 && user.Lose > user.Stealing)
                                {
                                    user.UserType = UserType.Cow;
                                    importer.Import(user);
                                }
                            }
                            catch (Exception ex)
                            {
                                AppCore.LogOutput.Error("Error Import user " + user.Name,ex);
                            }
                        }
                        AppCore.GameSettings.BotvaSettings.CowSearchCounter = searchText;
                        AppCore.LogOutput.Debug("End Import " + searchText);

                        if (StopSearch != null && StopSearch()) return;
                    }

            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Error("Ошибка поиска коровы.", ex);
            }
        }
    }
}
