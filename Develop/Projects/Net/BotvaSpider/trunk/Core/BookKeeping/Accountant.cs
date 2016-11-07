using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Threading;
using BotvaSpider.Automation.Mining;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Gears;
using BotvaSpider.Logging;
using Savchin.Core;
using Savchin.TimeManagment;
using WatiN.Core;

namespace BotvaSpider.BookKeeping
{
    /// <summary>
    /// Accountant
    /// </summary>
    public class Accountant
    {
        #region Properties

        private int investmentErrorCount;
        private int shopingErrorCount;

        private bool isShooping;
        private DateTime? alertBuyTime;

        private Timer timer;
        private readonly Logger Log;

        private readonly Player _player;
        private readonly TradeMaster _tradeMaster;
        private GameController _controller;



        private readonly Dictionary<Coulomb, CoulombInfo> Coulombs = CoulombInfo.Coulombs.ToDictionary(e => e.Coulomb, e => e);
        private readonly List<MessageInfo> alerts = new List<MessageInfo>();

        /// <summary>
        /// Gets a value indicating whether [need alert investment].
        /// </summary>
        /// <value><c>true</c> if [need alert investment]; otherwise, <c>false</c>.</value>
        public bool NeedAlertInvestment
        {
            get { return alertBuyTime.HasValue && DateTime.Now > alertBuyTime.Value; }
        }

        /// <summary>
        /// Gets a value indicating whether [need notification].
        /// </summary>
        /// <value><c>true</c> if [need notification]; otherwise, <c>false</c>.</value>
        public bool NeedNotification
        {
            get { return AppCore.AccountantSettings.InvestmentEnabled && AppCore.AccountantSettings.AlertStrategy.Enabled; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Accountant"/> class.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <param name="player">The player.</param>
        public Accountant(MailBox box, Player player)
        {
            Log = AppCore.LogAccountant;
            if (box != null)
                box.MessageComming += box_MessageComming;

            _player = player;
            _controller = new GameController(Log);
            _tradeMaster = new TradeMaster(player, _controller);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Accountant"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public Accountant(Logger log)
        {
            if (log == null) throw new ArgumentNullException("log");
            Log = log;
            _controller = new GameController(Log);
        }


        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (timer != null) return;

            timer = new Timer(OnTimerTick, null, 20000,
                (int)new TimeSpan(0, AppCore.AccountantSettings.ShopingInteval, 0).TotalMilliseconds);

        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;

            }
            if (_controller != null)
            {
                _controller.Dispose();
                _controller = null;
            }
        }



        #region Event Handlers

        /// <summary>
        /// Handles the MessageComming event of the box control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BotvaSpider.Core.MessageEventArgs"/> instance containing the event data.</param>
        void box_MessageComming(object sender, MessageEventArgs e)
        {
            if (e.Message.EventType != EventType.Attack || e.Message.IsWin) return;

            alerts.Add(e.Message);
            var settings = AppCore.GameSettings.BotvaSettings.AccountantSettings;
            if (settings.AlertStrategy.Type != InvestmentStrategy.Undefined)
            {
                CheckAttackAlert(e.Message);
            }
        }


        private void OnTimerTick(object state)
        {
            DoScheduleSleep();
            MakeShoping();
        }

        private void DoScheduleSleep()
        {
            var sleepRange = AppCore.BotvaSettings.GetSleepRange();
            if (sleepRange.IsEmpty) return;
            Thread.Sleep(sleepRange.To.ToDateTime(DateTime.Today) - DateTime.Now);
        }

        #endregion

        private void CheckAttackAlert(MessageInfo message)
        {
            var bastardName = message.Title.Substring(0, message.Title.Length - "атаковал вас".Length).Trim();

            var messages = alerts.Where(m => m.Title == message.Title).ToArray();

            if (messages.Length <= 1 && !AppCore.BotvaSettings.BastardList.Contains(bastardName)) return;

            AppCore.LogFights.Warn("Вас атакует " + bastardName, "Желательно слить ресурсы. Бот сам попытается это сделать через 40 мин.");
            alertBuyTime = DateTime.Now.AddHours(1).AddMinutes(-20);
            foreach (var info in messages)
            {
                alerts.Remove(info);
            }
        }



        /// <summary>
        /// Makes the shoping.
        /// </summary>
        public void MakeShoping()
        {
            if (isShooping) return;
            isShooping = true;

            try
            {
                Log.Debug("Делаем закупки.");


                var settings = AppCore.AccountantSettings;
                if (settings.InvestmentEnabled)
                {
                    MakeInvestments();
                    if (settings.UpgradeCouloub > 0) UpgradeCoulomb(settings.UpgradeCouloub);
                }
                if (settings.SearchStrategy.NeedSearch)
                {
                    _tradeMaster.Matchers = settings.SearchStrategy.StuffConditions;
                    _tradeMaster.DoSearch(StuffFinded);
                }

                if (AppCore.BotvaSettings.AutoTreat) BuyPotion();
            }
            catch (ThreadAbortException)
            {
                Stop();
            }
            catch (Exception ex)
            {
                shopingErrorCount++;
                if (AppCore.BotvaSettings.AllowDebugger) Debugger.Break();
                Log.Error("Ошибка закупок.", ex);
                if (shopingErrorCount >= AppCore.BotvaSettings.MaxDangerousErrors)
                {
                    Stop();
                    Log.Error("Модуль закупок отключен.", "Произошло слишком много ошибок и модуль был отключен.", ex);
                }
            }
            finally
            {
                isShooping = false;
            }
        }

        /// <summary>
        /// Stuffs the finded.
        /// </summary>
        /// <param name="tradeType">Type of the trade.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        private bool StuffFinded(TradeType tradeType, StuffSearchCondition condition, TradeItemController controller)
        {
            Log.Warn(string.Format("Найден {0} по цене {1}.", condition.ItemName, controller.GetPrice(tradeType)),
                     condition.ToString(),
                     condition.Clone());
            if (AppCore.AccountantSettings.SearchStrategy.SoundNotification)
            {
                PlaySound();
            }
            return true;
        }

        private void PlaySound()
        {
            using (var player = new SoundPlayer(Resources.tada))
            {
                player.Load();
                player.PlayLooping();
                Thread.Sleep(new TimeSpan(0, 0, 5));
                player.Stop();
                player.Dispose();
            }
        }


        /// <summary>
        /// Upgrades the coulomb.
        /// </summary>
        /// <param name="couloub">The couloub.</param>
        private void UpgradeCoulomb(Coulomb couloub)
        {
            if (!_player.Wardrobe.HasItem(couloub))
            {
                var message = string.Format("Кулон для прокачки '{0}' должен быть в одевалке.", couloub.GetDescription());
                Log.Suggestion(message, "Положите данный кулон в одевалку и бот его будет автоматически качать.");
                return;
            }

            if (!Coulombs.ContainsKey(couloub))
            {
                AppCore.LogSystem.WarnFormat("Нету информации про кулон '{0}'", couloub.GetDescription());
                return;
            }
            var info = Coulombs[couloub];
            if (_player.Cristals < info.LevelPrice) return;
            if (_controller.UpgradeCoulomb(couloub))
            {
                RegisterPurchase(BalanceCategory.CoulombUpgrade, couloub.GetDescription(), new Price(Resource.Crystals, info.LevelPrice));
            }

        }




        private void BuyPotion()
        {
            if (_player.BluePotionCount < 5 && _player.Money > AppCore.AccountantSettings.MinMoney + Wardrobe.PricePotionBlue)
            {
                var needToBuy = 5 - _player.BluePotionCount;
                int canBuy = (_player.Money - AppCore.AccountantSettings.MinMoney) / Wardrobe.PricePotionBlue;
                _controller.BuyBluePotion(canBuy < needToBuy ? canBuy : needToBuy);
            }
        }


        #region Investments

        private void MakeInvestments()
        {
            try
            {
                MakeInvestments(AppCore.AccountantSettings.NormalStrategy);

                if (NeedAlertInvestment)
                {
                    if (MakeInvestments(AppCore.AccountantSettings.AlertStrategy))
                        alertBuyTime = null;
                }
            }
            catch (Exception ex)
            {
                investmentErrorCount++;
                Log.Error("Ошибка инвестирования.", ex);
                if (investmentErrorCount >= AppCore.BotvaSettings.MaxDangerousErrors)
                    Log.Error("Инвестирование отключено.", "Произошло слишком много ошибок и данный модуль был отключен.");
            }
        }

        /// <summary>
        /// Makes the investments.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        private bool MakeInvestments(InvestmentStrategySettings settings)
        {
            if (!settings.Enabled)
            {
                AppCore.LogAccountant.Suggestion("Инвестирование выключено.", "Автоматическое инвестирование позволит вам сохранить деньги и автоматически делать покупки. \n Зайдите в Настройки > Бухгалтерия. ");
                return false;
            }

            if (settings.Type == InvestmentStrategy.Undefined) return true;
            var success = false;


            if (settings.IsSet(InvestmentStrategy.BuySpecifiedStuff))
            {
                if (!_player.WardrobeHasEmtySlots)
                {
                    AppCore.LogFights.Warn("В одевалке нет свободных мест.", "Инвестирование в шмот невозможен по причине отсутствия свободных слотов в одевалке.");
                }
                else
                {
                    _tradeMaster.Matchers = settings.StuffConditions;
                    if (_tradeMaster.InvestmentMoney())
                        success = true;
                }

            }
            return success;
        }



        #endregion

        #region Statistics

        #region Clan statistics
        /// <summary>
        /// Gets the clan deposits by week.
        /// </summary>
        /// <param name="weeks">The weeks.</param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<int, int>> GetClanDepositsByWeek(int weeks)
        {
            var result = new Dictionary<string, Dictionary<int, int>>();

            _controller.GoTo(_controller.UrlClanTreasury);
            var pagesCount = _controller.GetMaxPageCount(_controller.UrlClanTreasuryPage);

            if (GetPageDeposits(result, weeks)) return result;
            for (var i = 2; i <= pagesCount; i++)
            {
                _controller.GoTo(_controller.UrlClanTreasuryPage + "&page=" + i);

                if (GetPageDeposits(result, weeks)) return result;
            }
            return result;
        }
        /// <summary>
        /// Gets the page deposits.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="weeks">The weeks.</param>
        /// <returns>True finished</returns>
        private bool GetPageDeposits(Dictionary<string, Dictionary<int, int>> result, int weeks)
        {
            var currentWeek = DateTime.Now.GetWeekOfYear();
            var table = _controller.Browser.Table(Find.ByClass("default treasury_history"));
            for (var i = 1; i < table.TableRows.Count; i++)
            {
                var row = table.TableRows[i];
                var userName = row.TableCells[0].Text;
                var date = DateTime.Parse(row.TableCells[3].Text);
                var money = int.Parse(row.TableCells[2].Text);
                var week = date.GetWeekOfYear();
                if (currentWeek - week >= weeks) return true;
                if (!result.ContainsKey(userName))
                {
                    result.Add(userName, new Dictionary<int, int>());
                }
                var userspayments = result[userName];
                if (!userspayments.ContainsKey(week))
                {
                    userspayments.Add(week, money);
                }
                else
                {
                    userspayments[week] += money;
                }
            }
            return false;

        }
        #endregion

        #region Account statistics

        /// <summary>
        /// Registers the balance item.
        /// </summary>
        /// <param name="item">The item.</param>
        public static void RegisterBalanceItem(BalanceItem item)
        {
            if (item.IsProfit)
                AppCore.LogAccountant.Info(string.Format("Доходы {0}", item), item);
            else
                AppCore.LogAccountant.Info(string.Format("Расходы {0}", item), item);
            ObjectProvider.Instance.AddBalanceItem(item);
        }

        /// <summary>
        /// Adds the profit.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="item">The item.</param>
        /// <param name="benefit">The benefit.</param>
        public static void RegisterProfit(BalanceCategory category, string item, CristalBenefit benefit)
        {
            var result = new BalanceItem
                             {
                                 Category = category,
                                 Item = item,
                                 Gold = 0,
                                 Cristal = benefit.Cristals,
                                 SmallTicket = benefit.SmallTicket,
                                 BigTicket = benefit.BigTicket,
                                 IsProfit = true
                             };
            AppCore.LogAccountant.Info(string.Format("Получена прибыль {0}", item), result);
            ObjectProvider.Instance.AddBalanceItem(result);
        }

        /// <summary>
        /// Adds the profit.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="item">The item.</param>
        /// <param name="gold">The gold.</param>
        /// <param name="cristals">The cristals.</param>
        public static void RegisterProfit(BalanceCategory category, string item, int gold, int cristals)
        {
            var result = new BalanceItem
                             {
                                 Category = category,
                                 Item = item,
                                 Gold = gold,
                                 Cristal = cristals,
                                 IsProfit = true
                             };
            AppCore.LogAccountant.Info(string.Format("Получена прибыль {0}", item), result);
            ObjectProvider.Instance.AddBalanceItem(result);
        }

        /// <summary>
        /// Adds the purchase.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="item">The item.</param>
        /// <param name="price">The price.</param>
        public static void RegisterPurchase(BalanceCategory category, string item, Price price)
        {
            var result = new BalanceItem
                             {
                                 Category = category,
                                 Item = item,
                                 IsProfit = false
                             };
            switch (price.Resource)
            {
                case Resource.Gold:
                    result.Gold = price.Ammount;
                    break;
                case Resource.Crystals:
                    result.Cristal = price.Ammount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            AppCore.LogAccountant.Info(string.Format("Произведена покупка {0}", item), result);
            ObjectProvider.Instance.AddBalanceItem(result);
        }

        #endregion

        #endregion


    }
}