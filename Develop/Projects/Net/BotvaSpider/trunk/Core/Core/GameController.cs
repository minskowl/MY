using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Data;
using BotvaSpider.Gears;
using BotvaSpider.Logging;
using Savchin.Core;

using Savchin.IO;
using Savchin.Utils;
using Savchin.Web.HtmlProcessing;
using WatiN.Core;


namespace BotvaSpider.Core
{
    public sealed partial class GameController : IDisposable
    {

        #region Properties

     
        private const string pageHeader =
            @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN""> 
<HTML xmlns=""http://www.w3.org/1999/xhtml""> 
<HEAD> 
<TITLE>Ботва Онлайн - бесплатная онлайн игра</TITLE> 
<META http-equiv=Content-Type content=""text/html; charset=UTF-8""> 

<link rel=""icon"" href=""favicon.ico"" type=""image/x-icon""> 
<link rel=""shortcut icon"" href=""favicon.ico"" type=""image/x-icon""> 

<link href=""css/sheet1.1.css"" rel=""stylesheet"" type=""text/css""> 

<script language='javascript' src='css/script.1.js'></script> 
<script language='javascript' src='css/popups.1.js'></script> 
 
		<script language='javascript' src='css/jquery.js'></script>	    <style type=""text/css""> 
<!--
.style1 {font-size: 16px}
-->
        </style> 
</HEAD> ";

        private readonly List<Pair<GetRivalResult, string>> reasons = new List<Pair<GetRivalResult, string>>
                                                              {
  new Pair<GetRivalResult,string>(GetRivalResult.BadHealth,"У противника слишком мало здоровья.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.HaveFightInHour,"На данное животное уже нападали в течение часа.") ,                                                                 
  new Pair<GetRivalResult,string>(GetRivalResult.Holiday,"Хитрый противник ушёл в отпуск и укатил на южные пастбища. Поэтому он не может быть атакован") ,                                                                 
  new Pair<GetRivalResult,string>(GetRivalResult.ManyFights,"На одно и тоже животное можно нападать 5 раз в сутки. Вот.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.ManyFights,"На одно и то же животное можно нападать 5 раз в сутки. Вот.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.NotFinded,"Никого не нашли, ищи ещё!") ,
  new Pair<GetRivalResult,string>(GetRivalResult.NotFinded,"Оба-на, никого нэма. Ищите лучше!") ,
  new Pair<GetRivalResult,string>(GetRivalResult.NotFinded,"Противников не найдено. Вообще. Даже кнопку \"поиск\" пока жмакать больше не нужно.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.NotFinded,"Ай-яй-яй, стоит найти себе противника по уровню. Ну, хотя бы примерно.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.NotFinded,"Нельзя нападать на представителя своей расы.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.FrequentlyFight,"Нельзя так часто нападать.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.Hide,"Противник ожидал вашего нападения и ему удалось спрятаться.") ,
  new Pair<GetRivalResult,string>(GetRivalResult.ToDelete,"Противник поставлен на удаление и не может быть атакован") ,
  new Pair<GetRivalResult,string>(GetRivalResult.OnStorm,"Противник находится на штурме замка. Нападение невозможно") ,
  new Pair<GetRivalResult,string>(GetRivalResult.WasBlocked,"Противник заблокирован и не может быть атакован") ,
  new Pair<GetRivalResult,string>(GetRivalResult.InWhiteList,"Нападать на существ из белого списка? Нехорошо."),  
  new Pair<GetRivalResult,string>(GetRivalResult.NeedMoney,"У вас не хватает денег"),  

    };
        private readonly Dictionary<SkillType, string> skillImages = new Dictionary<SkillType, string>();
        /// <summary>
        /// Gets the skill images.
        /// </summary>
        /// <value>The skill images.</value>
        public Dictionary<SkillType, string> SkillImages
        {
            get { return skillImages; }
        }

        private readonly IE browser;
        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public IE Browser
        {
            get { return browser; }
        }
        /// <summary>
        /// Gets or sets the item builder.
        /// </summary>
        /// <value>The item builder.</value>
        public ItemBuilder ItemBuilder { get; private set; }

        private readonly FightResultBuilder fightResultBuilder;
        private readonly StartWithMathcer mathcer = new StartWithMathcer();
        private readonly Logger log;
        private readonly List<string> whiteList;
        private readonly List<string> whiteClanList;
        /// <summary>
        /// Gets the white clan list.
        /// </summary>
        /// <value>The white clan list.</value>
        public List<string> WhiteClanList
        {
            get { return whiteClanList; }
        }

        #endregion





        /// <summary>
        /// Initializes a new instance of the <see cref="GameController"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public GameController(Logger log)
        {
            InitUrls("http://" + AppCore.AcountSettings.Server + "/");
            browser = BrowserFactory.CreateBrowser(DomainUrl);
            Debug.Assert(browser != null, "controller without browser ");

            this.log = log;
            fightResultBuilder = new FightResultBuilder(this);

            SkillImages.Add(SkillType.Power, UrlImagePowerIcon);
            SkillImages.Add(SkillType.Protection, UrlImageProtectionIcon);
            SkillImages.Add(SkillType.Dexterity, UrlImageDexterityIcon);
            SkillImages.Add(SkillType.Mastery, UrlImageMasteryIcon);
            SkillImages.Add(SkillType.Weight, UrlImageWeightIcon);

            whiteClanList = new List<string>(AppCore.AttackSettings.ClanWhiteList);
            whiteList = ObjectProvider.Instance.GetNotCows().Union(AppCore.AttackSettings.WhiteList).ToList();

            ItemBuilder= new ItemBuilder(this);
        }

        /// <summary>
        /// Hides the browser.
        /// </summary>
        public void HideBrowser()
        {
            Savchin.WinApi.User32.ShowWindow(browser.hWnd, (short)Savchin.WinApi.SW.SW_HIDE);
        }
        /// <summary>
        /// Shows the browser.
        /// </summary>
        public void ShowBrowser()
        {
            Savchin.WinApi.User32.ShowWindow(browser.hWnd, (short)Savchin.WinApi.SW.SW_SHOW);
        }
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void HandleException(string message, Exception ex)
        {
            AppCore.LogSystem.Error(message, ex);
            CreateScreenshot("err");
            if (AppCore.BotvaSettings.AllowDebugger) Debugger.Break();
        }
        protected void CreateScreenshot(string name)
        {
            var fileName = Path.Combine(AppSettings.LogsPath,
                                        string.Format("{1}_{0:MMdd_HHmm}.html", DateTime.Now, name));

            SaveScreenshot(fileName);
        }
        /// <summary>
        /// Saves the screenshot.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveScreenshot(string fileName)
        {
            //browser.SaveAs(fileName);

            DirectoryHelper.CreateIfNotExists(Path.GetDirectoryName(fileName));

            var builder = new StringBuilder();
            builder.AppendLine(pageHeader);
            builder.AppendLine(browser.Html);
            builder.AppendLine("</HTML>");
            var html = builder.ToString();

            try
            {
                var serializer = new HtmlSerializer(browser.Uri, html, Encoding.UTF8);
                serializer.FileStorage = new FileSystemStorage(fileName);
                serializer.SaveContent();
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Log.Error("Ошибка сериализации скриншота", ex);
                SimpleSaveScreenshot(fileName, html);
            }
        }

        private void SimpleSaveScreenshot(string fileName, string html)
        {
            try
            {
                File.WriteAllText(fileName, html);
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Log.Error("Ошибка создания скриншота", ex);
            }
        }
        /// <summary>
        /// Determines whether [has cool status].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [has cool status]; otherwise, <c>false</c>.
        /// </returns>
        public bool? HasCoolStatus()
        {
            if (!OpenUrl(UrlKormushka)) return null;

            var cell = browser.Element(Find.ByClass("text_main_1")).Text;
            return cell != "Премиум не активирован";
        }

        /// <summary>
        /// Gets the has patrools. Page state in dozor
        /// </summary>
        /// <returns></returns>
        public bool? GetHasPatrools()
        {
            GoTo(UrlDozor);
            if (browser.Url != UrlDozor) return null;
            var list = Browser.SelectList(Find.ById("auto_watch"));
            return list.Exists && list.Options.Count > 0;
        }

        /// <summary>
        /// Upgrades the coulomb.
        /// </summary>
        /// <param name="couloub">The couloub.</param>
        /// <returns></returns>
        public bool UpgradeCoulomb(Coulomb couloub)
        {
            OpenUrl(UrlSmithMaster);
            var imgUrl = ItemBuilder.GetItemImageUrl(couloub, false);
            var img = browser.Image(Find.BySrc(imgUrl));
            var table = (Table)img.Parent.Parent.Parent.Parent;
            var cell = table.TableRows[table.TableRows.Count - 1].TableCells[0];
            if (cell.Links.Count == 0) return false;
            cell.Links[0].Click();

            var imgCristal = browser.Image(Find.BySrc(UrlImageCristal));
            table = (Table)imgCristal.Parent.Parent.Parent.Parent;
            cell = table.TableRows[table.TableRows.Count - 1].TableCells[0];
            if (cell.Links.Count == 0) return false;
            cell.Links[0].Click();
            var message = GetMessage();
            AppCore.LogSystem.Info("Результат апгрейда кулона " + couloub.GetDescription(), message);
            return true;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            var div = Browser.Div(Find.ByClass("message"));
            if (div.Exists)
                return div.Text;
            return null;
        }


        /// <summary>
        /// Starts the search crystal.
        /// </summary>
        /// <returns></returns>
        public TimeSpan? StartSearchCrystal()
        {
            OpenUrl(UrlMineWork);
            if (Browser.Div(Find.ByClass("message")).Exists)
            {
                return null;
            }
            var result = GetLeftTime();
            //Already search
            if (result.HasValue) return result;

            mathcer.Text = UrlMineWork + "&m=work&k=";
            Link link = Browser.Link(Find.ByUrl(mathcer));
            if (!link.Exists)
                return null;

            link.Click();


            return GetLeftTime();
        }

        /// <summary>
        /// Gets the sleep time after fight.
        /// </summary>
        /// <returns></returns>
        public TimeSpan? GetSleepTimeAfterFight()
        {
            GoTo(UrlDozor);
            return GetTimerValue("counter2");
        }

        /// <summary>
        /// Gets the left time.
        /// </summary>
        /// <returns></returns>
        public TimeSpan? GetLeftTime()
        {
            return GetTimerValue("left_time");
        }

        /// <summary>
        /// Gets the timer value.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TimeSpan? GetTimerValue(string id)
        {
            var attempt = 0;
        tryagain:

            try
            {
                var timer = Browser.Element(Find.ById(id));
                return timer.Exists ? TimeSpan.Parse(timer.Text) : (TimeSpan?)null;
            }
            catch (UnauthorizedAccessException)
            {
                attempt++;
                if (attempt > 10) throw;
                Thread.Sleep(100);
                goto tryagain;
            }
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {

            OpenUrl(UrlAccount);
            if (Browser.Image(Find.ByAlt("Персонаж")).Exists)
                return true;


            Browser.TextField(Find.ByName("email")).Value = AppCore.AcountSettings.Email;
            Browser.TextField(Find.ByName("passWord")).Value = AppCore.AcountSettings.Password;
            Browser.Image(Find.ByName("b_msg_go2")).Click();
            if ((Browser.Image(Find.ByAlt("Персонаж")).Exists))
                return true;
            var cell = Browser.TableCell(Find.ByClass("text_msg"));
            if (cell.Exists)
            {
                AppCore.LogSystem.Error("Неможем зайти в ботву.", cell.Text);
            }

            return false;
        }


        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public void Logout()
        {
            Browser.GoTo(UrlLogout);
        }


        /// <summary>
        /// Gets the attack button.
        /// </summary>
        /// <returns></returns>
        public Image GetAttackButton()
        {
            return GetButton(UrlButtonAttack);
        }

        /// <summary>
        /// Gets the button.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <returns></returns>
        public Image GetButton(string imageUrl)
        {
            return Browser.Image(Find.BySrc(imageUrl));
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <returns></returns>
        public Image GetImage(string imageUrl)
        {
            return Browser.Image(Find.BySrc(imageUrl));
        }


        /// <summary>
        /// Tries the go patrol.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public bool TryGoPatrol(int minutes)
        {
            OpenUrl(UrlDozor);

            var list = Browser.SelectList(Find.ById("auto_watch"));
            if (!list.Exists || list.Options.Count < 1) return false;


            var value = minutes / 10;
            list.SelectByValue(value.ToString());

            Browser.Element(Find.ByName("do_watch")).Click();

            var message = GetMessage();
            if (string.IsNullOrEmpty(message)) return true;

            AppCore.LogOutput.Info("В дозор не пошли по причине.", message);
            return false;
        }

        /// <summary>
        /// Gets the fight results.
        /// </summary>
        /// <returns></returns>
        public List<string> GetFightLogsUrls(List<DateTime> except)
        {

            Browser.GoTo(UrlFightMessages);

            var pages = new Pager(browser, UrlFightMessages);
            var fightLogs = new List<string>();


            do
            {
                FillLogsUrls(except, fightLogs);
            } while (pages.GotoNextPage());



            return fightLogs;
        }

        /// <summary>
        /// Gets the fight result.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public FightResult GetFightResult(string url)
        {
            Browser.GoTo(url);
            return fightResultBuilder.Build();
        }

        /// <summary>
        /// Shows the user info.
        /// </summary>
        /// <param name="username">The username.</param>
        public bool ShowUserInfo(string username)
        {
            DoSearch(username);

            foreach (Link link in SheepsLinks)
            {
                if (link.Text != username) continue;

                GoTo(link.Url);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches the users.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        public IEnumerable<User> SearchUsers(string matcher)
        {

            DoSearch(matcher);

            var links = SheepsLinks.Select(e => e.Url).ToArray();

            return GetUsersByUrl(links);
        }



        /// <summary>
        /// Searches the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public User SearchUser(string userName)
        {

            DoSearch(userName);
            var table = browser.Table(Find.ByClass("search_result"));
            var link = table.Links.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Url) &&
                e.Url.StartsWith(UrlPlayer + "?id=") &&
                string.Compare(userName, e.Text, true) == 0
                );

            if (link == null) return null;
            return GetUserByUrl(link.Url);
        }



        private IEnumerable<Link> SheepsLinks
        {
            get
            {
                foreach (Link link in browser.Links)
                {
                    if (string.IsNullOrEmpty(link.Url)) continue;
                    if (!link.Url.StartsWith(UrlPlayer + "?id=")) continue;
                    TableRow row = (TableRow)link.Parent.Parent;
                    if (!row.TableCells[0].InnerHtml.Contains("ico_b.png")) continue;
                    yield return link;
                }
            }
        }



        private void DoSearch(string username)
        {
            GoTo(UrlSearch);
            Browser.TextField(Find.ByName("word")).Value = username;
            Browser.Element(Find.BySrc(UrlImageButtonSearch)).Click();
        }

        public int GetMaxPageCount(string urlStartWith)
        {
            int maxPageNum = 0;
            var links =
                browser.Links.Where(e => !string.IsNullOrEmpty(e.Url) && e.Url.StartsWith(urlStartWith)).
                Select(e => e.Url);
            foreach (var link in links)
            {
                var parts = link.Split(new[] { "&page=" }, StringSplitOptions.RemoveEmptyEntries);
                var pageNum = int.Parse(parts[1]);
                if (pageNum > maxPageNum) maxPageNum = pageNum;
            }
            return maxPageNum;
        }

        /// <summary>
        /// Buys the blue potion.
        /// </summary>
        /// <param name="count">The count.</param>
        public void BuyBluePotion(int count)
        {
            Browser.GoTo(UrlShop);
            mathcer.Text = UrlShop + "?group=1&sub=1&buyid=2&k=";

            for (var i = 0; i < count; i++)
            {
                Browser.Link(Find.ByUrl(mathcer)).Click();
            }

        }

        private void FillLogsUrls(List<DateTime> except, ICollection<string> links)
        {
            foreach (var link in Browser.Links)
            {
                if (link.Url.StartsWith(UrlFightLog) &&
                    !IsInList(except, DateTime.Parse(link.Parent.PreviousSibling.Text)))
                    links.Add(link.Url);

            }
        }



        private bool IsInList(List<DateTime> list, DateTime date)
        {
            foreach (var time in list)
            {
                if (date.Subtract(time).TotalMinutes == 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is internet error].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is internet error]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInternetError()
        {
            if (browser.Html.Contains("<BODY>disabled</BODY>")) return false;
            try
            {
                if (browser.ContainsText("500 Internal Server Error") || browser.ContainsText("504 Gateway Time-out") ||
                    !browser.Image(Find.BySrc(UrlImageDeleloperLogo)).Exists
                    )
                {
                    AppCore.LogSystem.Warn("Ошибка сервера ботвы");
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            return false;

        }

        /// <summary>
        /// Opens the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public bool OpenUrl(string url)
        {
            return url == GetCurrentUrl() || GoTo(url);
        }
        /// <summary>
        /// Goes to.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public bool GoTo(string url)
        {
            return GoTo(url, null);
        }

        /// <summary>
        /// Goes to.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postData">The post data.</param>
        /// <returns></returns>
        public bool GoTo(string url, string postData)
        {
            var urlToGo = url;
            var sleepTime = 0;
        tryAgain:
            if (string.IsNullOrEmpty(postData))
                browser.GoTo(urlToGo);
            else
                browser.Navigate(urlToGo, 0, postData);

            if (!url.StartsWith(UrlLogin) && browser.Url.StartsWith(UrlLogin)) throw new LoginRequiredException();

            if (browser.Html.Contains("<BODY>disabled</BODY>")) return false;
            if (IsInternetError())
            {
                sleepTime += 1000;
                Thread.Sleep(sleepTime);
                urlToGo = url + (url.Contains("?") ? "&" : "?") + "r=" + Randomizer.GetIntegerBetween(100, 999);
                goto tryAgain;

            }
            return browser.Url.StartsWith(url);
        }

        #region Import Users

        /// <summary>
        /// Gets the users by URL.
        /// </summary>
        /// <param name="links">The links.</param>
        /// <returns></returns>
        public List<User> GetUsersByUrl(IEnumerable<string> links)
        {
            var result = new List<User>();
            foreach (var link in links)
            {
                var user = GetUserByUrl(link);
                if (user != null) result.Add(user);

            }
            return result;
        }

        /// <summary>
        /// Gets the user by URL.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public User GetUserByUrl(string link)
        {
            GoTo(link);
            return browser.Image(Find.BySrc(UrlImageBan)).Exists ?
                null : User.CreateFormUserPage(browser);
        }

        /// <summary>
        /// Gets the users from clan.
        /// </summary>
        /// <param name="clanName">Name of the clan.</param>
        public List<User> GetUsersFromClan(string clanName)
        {

            var result = new List<User>();
            GoTo(UrlSearch);
            //Browser.RadioButton(Find.ByName("type")).Checked = true;
            Browser.RadioButtons[1].Checked = true;
            Browser.TextField(Find.ByName("word")).Value = clanName;
            Browser.Element(Find.BySrc(UrlImageButtonSearch)).Click();

            var urls = (from l in Browser.Links
                        where !string.IsNullOrEmpty(l.Url) && l.Url.StartsWith(UrlClan) && l.ClassName == "text_main_1"
                        select l.Url).ToArray();

            foreach (var url in urls)
            {
                result.AddRange(GetUsersFromClanUrl(url));
            }
            return result;
        }

        /// <summary>
        /// Gets the users from clan URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public IEnumerable<User> GetUsersFromClanUrl(string url)
        {

            GoTo(url);
            var table = Browser.Table(Find.ByClass("text_main_4"));
            var clan = new Clan
                           {
                               Name = table.TableRows[0].TableCells[1].Text,
                               Tag = table.TableRows[1].TableCells[1].Text,
                           };

            var membersLink = (from l in Browser.Links
                               where !string.IsNullOrEmpty(l.Url) && l.Url.StartsWith(UrlClanMembers) && l.ClassName == "text_main_4"
                               select l.Url).FirstOrDefault();

            GoTo(membersLink);
            var membersLinks = (from l in Browser.Links
                                where !string.IsNullOrEmpty(l.Url) && l.Url.StartsWith(UrlPlayer) && l.ClassName == "text_main_5"
                                select l.Url).ToArray();


            var result = GetUsersByUrl(membersLinks);
            foreach (var user in result)
            {
                user.Clan = clan;
                Application.DoEvents();
            }

            return result;
        }
        #endregion



        /// <summary>
        /// Adds the current wars in white list.
        /// </summary>
        public void AddCurrentWarsInWhiteList()
        {
            foreach (var war in GetCurrentWars().Where(e => e.Started))
            {
                var enemies = war.EnemySide.Select(e => e.Tag).Except(whiteClanList).ToArray();
                whiteClanList.AddRange(enemies);
            }
        }

        /// <summary>
        /// Gets the current wars.
        /// </summary>
        /// <returns></returns>
        public List<War> GetCurrentWars()
        {
            var result = new List<War>();
            OpenUrl(UrlCurrentWars);
            foreach (var table in browser.Tables.Where(e => e.ClassName == "wars"))
            {
                var war = WarBuilder.Create(table);
                if (war != null) result.Add(war);
            }
            return result;
        }


        /// <summary>
        /// Buys the skill.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns></returns>
        public bool BuySkill(SkillType skill)
        {
            GoTo(UrlTraining);
            var matcher = UrlTraining + "?p=" + ((int)skill) + "&k=";
            var link = Browser.Links.Where(e => !string.IsNullOrEmpty(e.Url) &&
                                                   e.Url.StartsWith(matcher)).FirstOrDefault();

            if (link == null || !link.Exists) return false;

            link.Click();
            return true;
        }


        private string GetCurrentUrl()
        {
            var position = browser.Url.LastIndexOf("r=");
            return position == -1 ? browser.Url : browser.Url.Substring(0, position);
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (browser != null)
                browser.Dispose();
        }




    }
}
