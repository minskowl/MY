using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BotvaSpider.Data;
using WatiN.Core;

namespace BotvaSpider.Core
{
    static class WarBuilder
    {
        private static readonly Regex regexClan = new Regex(@"(?'Name'.*?\s)(?'Tag'\[.*?\])$", RegexOptions.Singleline | RegexOptions.Compiled);
        /// <summary>
        /// Creates the specified table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static War Create(Table table)
        {
            var warName = table.Parent.PreviousSibling.Text;
            try
            {
                var war = new War();
                war.Name = warName;
                var sidesTables = table.TableRows[1].Tables;

                war.OurSide = GetWarClans(sidesTables[0]);
                war.EnemySide = GetWarClans(sidesTables[1]);
                war.Started = !table.TableRows[table.TableRows.Count-1].TableCells[0].Span(Find.ById("timer_0")).Exists;
                return war;
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Warn("Ошибка парсинга войны", warName, ex);
                return null;
            }
        }
        /// <summary>
        /// Gets the war clans.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static List<Clan> GetWarClans(Table table)
        {
            var result = new List<Clan>();
            foreach (var row in table.TableRows)
            {
                if (string.IsNullOrEmpty(row.TableCells[0].Text)) continue;
                var clanName = row.TableCells[0].Text.Trim();
                if (string.IsNullOrEmpty(clanName)) break;
                var clan = CreateClan(clanName);
                if (clan != null) result.Add(clan);
            }
            return result;
        }
        /// <summary>
        /// Creates the clan.
        /// </summary>
        /// <param name="clanName">Name of the clan.</param>
        /// <returns></returns>
        private static Clan CreateClan(string clanName)
        {
            var match = regexClan.Match(clanName);
            if (!match.Success)
            {
                AppCore.LogSystem.Warn("Несмогли распарсить клан.", clanName);
                return null;
            }
            var tag = match.Groups["Tag"].Value;
            return new Clan
                       {
                           Tag = tag.Substring(1, tag.Length-2),
                           Name = match.Groups["Name"].Value
                       };
        }
    }
}
