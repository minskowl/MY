using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BotvaSpider.Data;
using WatiN.Core;

namespace BotvaSpider.Core
{

    public enum RivalSource
    {
        [Description("Ферма")]
        FromFarm,
        [Description("Списка")]
        FromList,
        [Description("Горячего списка")]
        FromHotList,
        [Description("Случайный")]
        FromRandom,
        [Description("Список для грабежа")]
        StaffRobberyList,
        [Description("Список для славы")]
        StaffGloryList,
        [Description("Список для мести")]
        StaffRevengeList,
    }
    public enum RandomSearchMode
    {
        [Description("Поиск равных")]
        Equal = 1,
        [Description("Поиск сильных")]
        Strong = 3,
        [Description("Поиск слабых")]
        Weak = 2,
        [Description("Список для грабежа")]
        RobberyList = 10,
        [Description("Список для славы")]
        GloryList = 11,
        [Description("Список для мести")]
        RevengeList = 12,
    }
    public enum StaffListType
    {
        [Description("Список для грабежа")]
        RobberyList = 1,
        [Description("Список для славы")]
        GloryList = 2,
        [Description("Список для мести")]
        RevengeList = 3,
        [Description("Белый список")]
        WhiteList = 4,
    }
    public partial class GameController
    {
        /// <summary>
        /// Clears the staff list.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="progress">The progress.</param>
        public void ClearStaffList(StaffListType type, ShowState progress)
        {
            OpenUrl(UrlShtabNotes);
            var counter = 0;
            var rows = 0;
            do
            {
                var div = browser.Div("b_notes_" + (int)type);
                var table = div.Tables[0];
                if (table.TableRows.Count == 1)
                {
                    if (progress != null) progress(string.Format("Конец очистки"), 100);
                    return;
                }
                if (rows==0) rows = table.TableRows.Count;
                table.TableRows[1].TableCells[0].Links[0].Click();
                counter++;
                if (progress != null) progress(string.Format("Очистка штабного списка {0}\\{1}", counter, rows), 0);
            } while (true);

        }

        /// <summary>
        /// Adds the staff list.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="users">The users.</param>
        public void AddStaffList(StaffListType type, IEnumerable<string> users)
        {
            OpenUrl(UrlShtabNotes);
            var notInList = new List<string>(users);
            var div = browser.Div("b_notes_" + (int)type);
            var table = div.Tables[0];
            for (var i = 1; i < table.TableRows.Count; i++)
            {
                var text = table.TableRows[i].TableCells[1].Text;
                var userFromList = text.Substring(0, text.LastIndexOf('[')).Trim();
                notInList.Remove(userFromList);
            }

            foreach (var user in notInList)
            {
                try
                {
                    browser.TextField(Find.ByName("username")).Value = user;
                    SelectListValue("group", (int)type);
                    browser.Image(Find.ByName("do_add")).Click();
                }
                catch (Exception ex)
                {
                    GoTo(UrlShtabNotes);
                }
            }
        }
        /// <summary>
        /// Determines whether this instance [can kill rival] the specified rival.
        /// </summary>
        /// <param name="rival">The rival.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can kill rival] the specified rival; otherwise, <c>false</c>.
        /// </returns>
        public bool CanKillRival(Rival rival)
        {
            if (whiteList.Contains(rival.Name))
            {
                AppCore.LogFights.Debug(
          string.Format("Противник {0} негодиться для атаки.", rival.Name),
          "Он у вас в белом списке", rival);
                return false;
            }

            if (rival.Clan != null && (WhiteClanList.Contains(rival.Clan.Tag) || WhiteClanList.Contains(rival.Clan.Name)))
            {
                AppCore.LogFights.Debug(
                string.Format("Противник {0} негодиться для атаки.", rival.Name),
                "Он находиться в клане который у вас в белом списке.", rival);
                return false;
            }
            if (AppCore.AttackSettings.AllowLostGlory)
                return true;
            var msg = Browser.Span(Find.ByClass("text_main_5"));


            if (msg.Exists &&
                msg.Spans.Count > 0 &&
                msg.Spans[0].ClassName == "num_minus")
            {
                AppCore.LogFights.Debug(
          string.Format("Противник {0} негодиться для атаки  ", rival.Name),
          "Требуется слив славы. Для разреншения слива славы зайдите в Настройки > Атака", rival);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Kills the rival.
        /// </summary>
        /// <returns></returns>
        public FightResult KillRival()
        {

            GetAttackButton().Click();
            if (!Browser.Url.StartsWith(UrlFightLogPage))
            {
                var logUrl = Browser.Link(Find.ByUrl(new Regex(UrlFightLogPage + @"\?log_id=\d+")));
                if (logUrl.Exists)
                {
                    logUrl.Click();
                }
                else
                {
                    return null;
                }
            }
            return fightResultBuilder.Build();
        }

        #region Search Rival To  Fight
        /// <summary>
        /// Gets the rival to fight.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public GetRivalResult GetRivalToFight(string userName)
        {
            OpenUrl(UrlDozor);

            Browser.TextField(Find.ById("name")).Value = userName;
            Browser.Element(Find.ByName("name_search")).Click();

            return GetSearchResult();
        }

        /// <summary>
        /// Searches the rival.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public GetRivalResult SearchRival(LevelFilter filter)
        {
            OpenUrl(UrlDozor);

            Browser.TextField(Find.ById("min")).Value = filter.LevelFrom.ToString();
            var boxMaxLevel = Browser.TextField(Find.ById("max"));
            boxMaxLevel.Value = filter.LevelTo.ToString();
            var button = boxMaxLevel.NextSibling.NextSibling.NextSibling;
            button.Click();

            return GetSearchResult();
        }

        /// <summary>
        /// Searches the rival.
        /// </summary>
        /// <param name="type">The type.</param>
        public GetRivalResult SearchRandomRival(RandomSearchMode type)
        {
            OpenUrl(UrlDozor);

            SelectListValue("type", (int)type);//Find equal
            GetAttackButton().Click();

            return GetSearchResult();
        }
        private GetRivalResult GetSearchResult()
        {
            var table = Browser.Table(Find.ByClass("attack"));
            if (table.Exists)
            {
                return GetRivalResult.OK;
            }

            var reason = GetMessage();
            log.Debug("Бой не состоялся.", string.Format("По причине {0}", reason));

            foreach (var pair in reasons)
            {
                if (reason.Contains(pair.Second))
                {
                    if (pair.First == GetRivalResult.NeedMoney)
                        throw new NoMoneyException();
                    return pair.First;
                }
            }
            log.WarnFormat("!!!!!!! Обнаружена неизвестная причина '{0}' ", reason);
            return GetRivalResult.System;
        }

        #endregion

    }
}
