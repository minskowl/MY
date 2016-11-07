using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BotvaSpider.Data;
using Savchin.Core;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace BotvaSpider.Core
{
    public enum TopSearchType
    {
        [Description("Игрок")]
        Player = 1,
        [Description("Клан")]
        Clan = 2,
        [Description("Фракция")]
        Fraction = 3
    }
    public enum TopSearchSort
    {
        [Description("Слава")]
        Glory = 1,
        [Description("Побед")]
        Wins = 2,
        [Description("Награблено")]
        Stealing = 3,
        [Description("Утеряно")]
        Lose = 4,
        [Description("Урона")]
        Injury = 5
    }

    public enum Race
    {
        [Description("Все")]
        All = 0,
        [Description("Свинтусы")]
        Pigs = 1,
        [Description("Барантусы")]
        Sheeps = 2,
    }


    /// <summary>
    /// 
    /// </summary>
    public partial class GameController
    {
        public const int TopSearchMaxPage = 19;

        private Table GetTopObjectTable(TopSearchType type, TopSearchSort sort, Race race, int page)
        {
            if (page < 0 || page > 19)
                throw new ArgumentOutOfRangeException("page");

            OpenUrl(UrlTop + "?t=" + (int)type);

            SelectListValue("sort", (int)sort);
            SelectListValue("race", (int)race);
            SelectListValue("limit", page);
            browser.Element(Find.ByName("show")).Click();
            var table = browser.Table(Find.ByClass("default"));
            return table;
        }
        /// <summary>
        /// Gets the clans from top.
        /// </summary>
        /// <param name="race">The race.</param>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        public List<Clan> GetClansFromTop(Race race, TopSearchSort sort)
        {
            return GetClansFromTop(race, sort, 0, TopSearchMaxPage);
        }

        /// <summary>
        /// Gets the clans from top.
        /// </summary>
        /// <param name="race">The race.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="pageFrom">The page from.</param>
        /// <param name="pageTo">The page to.</param>
        /// <returns></returns>
        public List<Clan> GetClansFromTop(Race race, TopSearchSort sort, int pageFrom, int pageTo)
        {
            var result = new List<Clan>();
            for (var i = pageFrom; i <= pageTo; i++)
            {
                result.AddRange(GetClansFromTop(race, sort, i));
            }
            return result;
        }

        /// <summary>
        /// Gets the clans from top.
        /// </summary>
        /// <param name="race">The race.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public List<Clan> GetClansFromTop(Race race, TopSearchSort sort, int page)
        {
            var table = GetTopObjectTable(TopSearchType.Clan, sort, race, page);
            var result = new List<Clan>();
            var urlMather = UrlClan + "?id=";
            var clansUrls = table.Links
                .Where(e => !string.IsNullOrEmpty(e.Url) && e.Url.StartsWith(urlMather))
                .Select(e => e.Url).ToArray();


            foreach (var url in clansUrls)
            {
                OpenUrl(url);
                var clan = GetClanFromPage();
                if (clan != null)
                    result.Add(clan);
            }
            return result;
        }

        /// <summary>
        /// Gets the users from guild.
        /// </summary>
        /// <param name="guild">The guild.</param>
        /// <param name="pagesFilter">The pages filter.</param>
        /// <returns></returns>
        public List<string> GetUsersLinksFromGuild(GuildType guild, IRange<int> pagesFilter)
        {
            var url = UrlCastleTreasury + "&id=" + ((int)guild);
            if (pagesFilter == null)
            {
                Browser.GoTo(url);
            }
            else
            {
                Browser.GoTo(url + "&page=" + pagesFilter.From);
            }

            var pages = pagesFilter == null ?
                new Pager(browser, url) :
                new FilteredPager(browser, url, pagesFilter);

            pages.Sleep = 1000;

            var links = new List<string>();
            do
            {
                var table = browser.Table(Find.ByClass("default treasury_now"));
                if (table.Exists)
                {
                    links.AddRange(
                        table.Links.Where(e => !string.IsNullOrEmpty(e.Url) && e.Url.StartsWith(UrlPlayer)).Select(e => e.Url)
                        );
                }


            } while (pages.GotoNextPage());



            return links.Distinct().ToList();
        }


        /// <summary>
        /// Gets the top users links.
        /// </summary>
        /// <param name="sort">The sort.</param>
        /// <param name="race">The race.</param>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        public List<string> GetTopUsersLinks(TopSearchSort sort ,Race race, IRange<int> pages)
        {
            var links = new List<string>();
            for (var page = pages.From; page <= pages.To; page++)
            {
                var table = GetTopObjectTable(TopSearchType.Player, sort, race, page);
                links.AddRange(table.Links
                                   .Where(l => !string.IsNullOrEmpty(l.Url) && l.Url.StartsWith(UrlPlayer))
                                   .Select(l => l.Url));

            }

            return links;
        }

        /// <summary>
        /// Gets the top glory clans.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public List<string> GetTopGloryClans(int page)
        {
            GoTo(UrlTopUsers);
            Browser.SelectList(Find.ById("sort")).SelectByValue("1");//Glory
            Browser.SelectList(Find.ById("race")).SelectByValue("2");//Sheeps
            Browser.SelectList(Find.ById("type")).SelectByValue("2");//Clans
            var links = new List<string>();
            Browser.SelectList(Find.ById("limit")).SelectByValue(page.ToString());//Page
            Browser.Element(Find.ById("show")).Click();

            var urlPattern = UrlClan + "?id=";
            links.AddRange(from l in Browser.Links
                           where !string.IsNullOrEmpty(l.Url) && l.Url.StartsWith(urlPattern)
                           select l.Url);

            return links;
        }
        private Clan GetClanFromPage()
        {
            var table = browser.Table(Find.ByClass("clanInfo"));
            try
            {
                var clan = new Clan();
                clan.Name = table.TableRows[0].TableCells[1].Text;
                clan.Tag = table.TableRows[1].TableCells[1].Text;
                clan.Treasury = int.Parse(table.TableRows[6].TableCells[1].Text);

                var text = table.TableRows[5].TableCells[1].Text;
                var parts = text.Split('/');
                clan.Soldiers = int.Parse(parts[0]);
                clan.BarrackCapacity = int.Parse(parts[1]);

                return clan;
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Error("Ошибка разбора клана " + browser.Url, ex);
                return null;
            }
        }

        /// <summary>
        /// Selects the list value.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        private void SelectListValue(string id, int value)
        {
            SelectListValue(id, value.ToString());
        }

        private void SelectListValue(string id, string value)
        {
            try
            {
                browser.SelectList(Find.ById(id)).SelectByValue(value);
            }
            catch (Exception)//Muty thread FIX
            {

            }
        }
    }
}
