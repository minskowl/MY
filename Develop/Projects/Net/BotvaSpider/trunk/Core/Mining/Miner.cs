using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using BotvaSpider.BookKeeping;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Logging;
using Savchin.Text;
using Savchin.Utils;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace BotvaSpider.Automation.Mining
{
    [Flags]
    public enum MinerGear : byte
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Кирка")]
        Pick = 0x1,
        [Description("Очки")]
        Glasses = 0x2,
        [Description("Каска")]
        Helmet = 0x4,

    }

    /// <summary>
    /// Miner
    /// </summary>
    public class Miner
    {

        #region Properties




        private bool initialized = false;
        private readonly IE browser;
        private readonly GameController controller;
        private readonly Player player;

        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        /// <value>The log.</value>
        public Logger Log { get; private set; }
        /// <summary>
        /// Gets or sets the pick count.
        /// </summary>
        /// <value>The pick count.</value>
        public int PickCount { get; private set; }
        /// <summary>
        /// Gets or sets the glasses count.
        /// </summary>
        /// <value>The glasses count.</value>
        public int GlassesCount { get; private set; }
        /// <summary>
        /// Gets or sets the helmet count.
        /// </summary>
        /// <value>The helmet count.</value>
        public int HelmetCount { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [need investment].
        /// </summary>
        /// <value><c>true</c> if [need investment]; otherwise, <c>false</c>.</value>
        public bool NeedInvestment
        {
            get { return initialized && (NeedBuyPick || NeedBuyGlasses || NeedBuyHelmet); }
        }
        /// <summary>
        /// Gets the gears.
        /// </summary>
        /// <value>The gears.</value>
        public MinerGear Gears
        {
            get
            {
                MinerGear gear = 0;
                if (HasPick) gear = gear | MinerGear.Pick;
                if (HasGlasses) gear = gear | MinerGear.Glasses;
                if (HasHelmet) gear = gear | MinerGear.Helmet;
                return gear;
            }
        }


        private bool HasPick
        {
            get { return PickCount > 0; }
        }
        private bool HasGlasses
        {
            get { return GlassesCount > 0; }
        }
        private bool HasHelmet
        {
            get { return HelmetCount > 0; }
        }
        private bool NeedBuyPick
        {
            get { return !HasPick; }
        }
        private bool NeedBuyGlasses
        {
            get { return AppCore.MinerSettings.UseGlasses && !HasGlasses; }
        }
        private bool NeedBuyHelmet
        {
            get { return AppCore.MinerSettings.UseHelmet && !HasHelmet; }
        }

        /// <summary>
        /// Gets the small glade attempt.
        /// </summary>
        /// <value>The small glade attempt.</value>
        private int SmallGladeAttempt
        {
            get { return player.Guild == GuildType.Miner ? 4 : 3; }
        }
        private int BigGladeAttempt
        {
            get { return player.Guild == GuildType.Miner ? 16 : 12; }
        }
        /// <summary>
        /// Gets the attempt minutes.
        /// </summary>
        /// <value>The attempt minutes.</value>
        public int AttemptMinutes
        {
            get { return player.Guild == GuildType.Miner ? 5 : 20; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Miner"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public Miner(Player player, GameController controller)
        {
            this.player = player;
            this.controller = controller;
            browser = controller.Browser;

            PickCount = 0;
            GlassesCount = 0;
            HelmetCount = 0;
            Log = AppCore.LogMine;
        }


        #region Minning in mine
        /// <summary>
        /// Starts the search cristal.
        /// </summary>
        /// <returns></returns>
        public TimeSpan? StartSearchCristal()
        {
            PrepareForSearch();
            Log.Debug("Начинаем искать кристал.");
            return controller.StartSearchCrystal();

        }

        /// <summary>
        /// Gets the crystal.
        /// </summary>
        public void GetCrystal()
        {
        getAgain:
            controller.GoTo(controller.UrlMineWork);

            var imgDobuty = browser.Image(Find.BySrc(controller.UrlImageButtonGetCrystal));

            if (!imgDobuty.Exists)
            {
                var sleepTime = controller.GetLeftTime();
                if (sleepTime.HasValue)
                {
                    Thread.Sleep(sleepTime.Value);
                    goto getAgain;
                }
            }
            var o = imgDobuty.Parent.Parent.Text;
            var parts = o.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var tmp = parts[1];
            var pos = tmp.IndexOf("-") + 1;
            var perc = tmp.Substring(pos, tmp.IndexOf("%") - pos);
            var succesPercentage = byte.Parse(perc.Trim());




            var result = new SearchCristalResult
            {
                Date = DateTime.Now,
                Level = player.Level,
                Coulomb = player.Coulomb,
                Spirit = player.Spirits,
                Percentage = succesPercentage,
                MinerGear = Gears
            };

            if (AppCore.MinerSettings.SearchCrystalLimits.Any(r => r.IsInRange(succesPercentage)))
            {
                Log.DebugFormat("{0}% успеха. Пытаемся добыть кристалл.", succesPercentage);


                result.DoAttempt = true;

                var link = (Link)imgDobuty.Parent;
                link.Click();

                var message = controller.GetMessage().Trim();
                result.ParseResult(message);

                if (result.HasBenefit)
                {
                    Log.Info(string.Format("Результат '{0}'", message), result);
                    Accountant.RegisterProfit(BalanceCategory.Mine, "Шахта", result);
                }
                else
                {
                    Log.Debug(string.Format("Результат '{0}'", message), result);
                }
                ObjectProvider.Instance.AddMineStatistics(result);
                PrepareForSearch();
                return;
            }

            Log.DebugFormat("{0}% успеха. Не исчем кристалл.", succesPercentage, result);
            ObjectProvider.Instance.AddMineStatistics(result);

            if (AppCore.MinerSettings.SearchUntilTry)
            {
                Log.Debug("Пытаемся снова найти кристалл.");


                var imgSearchAgain = controller.GetButton(controller.UrlImageButtonSearchAgain);
                imgSearchAgain.Parent.Click();
                var sleepTime = controller.GetLeftTime();
                if (sleepTime.HasValue)
                    Thread.Sleep(sleepTime.Value);
                goto getAgain;
            }
            else
            {
                var imgGoAway = controller.GetButton(controller.UrlImageButtonGoAway);
                imgGoAway.Parent.Click();
            }


        }



        private void PrepareForSearch()
        {
            UpdateStatus();
            if (NeedInvestment) BuyGears();
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        private void UpdateStatus()
        {
            AppCore.LogMine.Debug("Обновляем состояние шахтерской аммуниии");
            controller.OpenUrl(controller.UrlMineWork);

            var imgPick = controller.GetImage(controller.UrlImagePick);
            var firstRow = (TableRow)imgPick.Parent.Parent;
            var table = firstRow.ContainingTable;
            PickCount = GetGearCount(table.TableRows[1]);
            GlassesCount = GetGearCount(table.TableRows[3]);
            HelmetCount = GetGearCount(table.TableRows[5]);

            initialized = true;

        }

        /// <summary>
        /// Buys the gears.
        /// </summary>
        private void BuyGears()
        {
            AppCore.LogMine.Debug("Покупаем шахтерскую аммуниию.");
            controller.OpenUrl(controller.UrlMineShop);

            if (NeedBuyPick)
            {
                BuyMineItem("Кирка", 1);
            }

            if (NeedBuyGlasses)
            {
                BuyMineItem("Очки", 3);
            }

            if (NeedBuyHelmet)
            {
                BuyMineItem("Каска", 5);
            }
            UpdateStatus();
        }
        #endregion

        #region Visit glаde

        private int errorsCountGlade;
        /// <summary>
        /// Gets a value indicating whether this instance can visit glade.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can visit glade; otherwise, <c>false</c>.
        /// </value>
        public bool CanVisitGlade
        {
            get { return errorsCountGlade < AppCore.BotvaSettings.MaxDangerousErrors; }
        }

        private const int SmallFieldSize = 3;
        private const int BigFieldSize = 6;

        /// <summary>
        /// Visits the glade.
        /// </summary>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        /// <returns></returns>
        public bool VisitGlade(bool isSmall)
        {
            try
            {
                return VisitGladeUnsafe(isSmall);
            }
            catch (Exception ex)
            {
                Log.Warn("Ошибка посещения " + (isSmall ? "малой" : "большой") + " поляны", ex);
                errorsCountGlade++;
                if (errorsCountGlade >= AppCore.BotvaSettings.MaxDangerousErrors)
                {
                    Log.Error("Модуль посещения полян заблокирован.", "Слишком много ошибок", ex);
                }

                return false;
            }
        }

        /// <summary>
        /// Visits the glade.
        /// </summary>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        /// <returns></returns>
        private bool VisitGladeUnsafe(bool isSmall)
        {
            var size = isSmall ? SmallFieldSize : BigFieldSize;
            var buttonImageUrl = isSmall
                                     ? controller.UrlImageButtonSmallGlade
                                     : controller.UrlImageButtonBigGlade;
            var attemptSequense = isSmall ? GetSmallSequnce() : GetBigSequnce();

            controller.OpenUrl(controller.UrlMine);
            var img = browser.Image(Find.BySrc(buttonImageUrl));
            if (!(img.Parent is Link)) return false;
            img.Parent.Click();

            //Check
            var message = controller.GetMessage();
            if (!string.IsNullOrEmpty(message))
            {
                Log.Debug("Не попали на поляну.", message);
                return false;
            }
            var waitTime = controller.GetLeftTime();
            if (waitTime.HasValue) return false;



            Log.Debug(isSmall ? "Делаем поход на малую поляну. " : "Делаем поход на большую поляну. ",
                "Пробуем комбинанцию " + StringUtil.Join(attemptSequense, ","),
                attemptSequense);

            CheckGlade(attemptSequense, size);

            var result = GetGladeResult(size);
            Log.Info(isSmall ? "Результат похода на малую поляну. " : "Результат похода на большую поляну.", result);
            if (result.HasBenefit)
                Accountant.RegisterProfit(BalanceCategory.Glades, isSmall ? "Малая поляна" : "Большая поляна", result);
            ObjectProvider.Instance.SaveCrystalMap(isSmall, result.Sequence);

            return true;
        }

        private void CheckGlade(int[] attemptSequense, int size)
        {

            foreach (var i in attemptSequense)
            {
                var pos = i - 1;
                var row = pos / size;
                var column = pos % size;

                var field = browser.Table(Find.ByClass("field"));
                var cell = field.TableRows[row].TableCells[column];
                cell.Links[0].Click();
            }
        }

        private VisiteGladeResult GetGladeResult(int size)
        {
            var result = new VisiteGladeResult();
            var table = browser.Table(Find.ByClass("field"));


            for (var row = 0; row < size; row++)
                for (var column = 0; column < size; column++)
                {
                    var img = table.TableRows[row].TableCells[column].Images[0];

                    if (img.Src == controller.UrlImageEmptySpaceVisited ||
                        img.Src == controller.UrlImageEmptySpaceNotVisited) continue;

                    result.Sequence.Add(row * size + column + 1);
                    if (img.Src == controller.UrlImageCristalFound)
                    {
                        result.Cristals++;
                    }
                    else if (img.Src == controller.UrlImageSmallTicketFinded)
                    {
                        result.SmallTicket = true;
                    }
                    else if (img.Src != controller.UrlImageCristalNotFounded &&
                             img.Src != controller.UrlImageSmallTicketNotFinded &&
                             img.Src != controller.UrlImageBluePosionFounded &&
                             img.Src != controller.UrlImageBluePosionNotFounded
                        )
                    {
                        AppCore.LogSystem.Warn("Обнаружена неизвестная картинка на поляне.", img.Src);
                    }
                }
            return result;

        }

        private int[] GetSmallSequnce()
        {
            var lastAttempt = ObjectProvider.Instance.GetCrystalMapLastAttemptMap(true);


            if (lastAttempt.Count < SmallGladeAttempt)
            {
                lastAttempt.AddRange(GetRandomFromTop(lastAttempt, SmallGladeAttempt - lastAttempt.Count));
                return lastAttempt.ToArray();
            }
            if (lastAttempt.Count > SmallGladeAttempt)
            {
                lastAttempt.RemoveAt(lastAttempt.Count - 1);
                return lastAttempt.Take(SmallGladeAttempt).ToArray();
            }
            return lastAttempt.ToArray();
        }

        private int[] GetBigSequnce()
        {
            var top = ObjectProvider.Instance.GetCrystalMapPositionsTop(false);

            var limit = top.Average(e => e.Value);
            if (limit < 10) return GetRandomSequence(false);
            var topPoitions = top
                .Where(e => e.Value >= limit)
                .OrderBy(e => e.Value)
  .Select(e => e.Key).ToArray();
            if (topPoitions.Length > BigGladeAttempt)
            {
                return Randomizer.GetFromArray(topPoitions, BigGladeAttempt);
            }
            else
            {
                var tmp = Enumerable.Range(1, BigFieldSize * BigFieldSize).Except(topPoitions).ToArray();
                return topPoitions.Union(Randomizer.GetFromArray(tmp, BigGladeAttempt - topPoitions.Length)).ToArray();
            }
        }

        private int[] GetRandomSequence(bool isSmall)
        {
            var size = isSmall ? SmallFieldSize : BigFieldSize;
            var attmepts = isSmall ? SmallGladeAttempt : BigGladeAttempt;

            return Randomizer.GetFromArray(Enumerable.Range(1, size * size).ToArray(), attmepts);
        }

        private int[] GetRandomFromTop(IEnumerable<int> exept, int count)
        {
            var top = ObjectProvider.Instance.GetCrystalMapPositionsTop(true);
            var validKeys = top.Where(e => !exept.Contains(e.Key)).ToArray();
            var limit = validKeys.Average(e => e.Value);
            var topPoitions = validKeys
                .Where(e => e.Value >= limit)
                .OrderBy(e => e.Value)
                .Take(4)
                .Select(e => e.Key).ToArray();
            return Randomizer.GetFromArray(topPoitions, count);
        }
        #endregion

        private void BuyMineItem(string name, int row)
        {
            var table = browser.Table(Find.ByClass("default shop_items"));
            var cell = table.TableRows[row].TableCells[1];
            var temp = cell.Span(Find.ByClass("price")).Text.Split(new[] { ':' });
            var price = int.Parse(temp[1].Trim());
            var button = cell.Image(Find.BySrc(controller.UrlButtonBuy));
            button.Click();
            // cell.Links[0].Click();
            Accountant.RegisterPurchase(BalanceCategory.Mine, name, new Price(price));
        }
        private int GetGearCount(TableRow row)
        {
            var parts = row.TableCells[0].Text.Split(new char[] { ' ' });
            return int.Parse(parts[1]);
        }
    }
}
