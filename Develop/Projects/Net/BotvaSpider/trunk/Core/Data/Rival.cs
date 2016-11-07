using System;
using System.ComponentModel;
using BotvaSpider.Core;
using BotvaSpider.Farming;
using Savchin.ComponentModel;
using WatiN.Core;

namespace BotvaSpider.Data
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Rival : User
    {
        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>The farm.</value>
        [Browsable(false)]
        public FarmBase Farm { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [DisplayName("От куда противник")]
        [TypeConverter(typeof(EnumTypeConverter))]
        public RivalSource Source { get; set; }

        private static string[] separator = new string[] { Environment.NewLine };

        /// <summary>
        /// Creates the rival.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static Rival Create(IE browser, RivalSource source)
        {
            var table = browser.Table(Find.ByClass("attack"));
            if (!table.Exists)
            {
                return null;
            }
            var rival = new Rival { Source = source };
            FillUser(rival, table, browser);
            return rival;
        }
        protected static void FillUser(User user, Table userData, IE browser)
        {

            var parts = userData.TableRows[0].TableCells[1].Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            user.Name = parts[0];
            if (parts.Length > 1)
                user.Clan = Clan.Create(parts[1]);
            user.FillSkils(browser);

        }
    }
}