using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using WatiN.Core;

namespace BotvaSpider.BookKeeping
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TradeMaster
    {
        #region Properties

        private readonly GameController controller;
        private readonly IE browser;


        /// <summary>
        /// Gets or sets the matchers.
        /// </summary>
        /// <value>The matchers.</value>
        public List<StuffSearchCondition> Matchers { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance can invest.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can invest; otherwise, <c>false</c>.
        /// </value>
        private bool CanInvest
        {
            get
            {
                return Matchers.Any(e => e.CanInvest(player));
            }
        }
        private readonly Player player;
        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Player Player
        {
            get { return player; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeMaster"/> class.
        /// </summary>
        public TradeMaster(Player player, GameController controller)
        {
            this.controller = controller;
            this.browser = controller.Browser;

            this.player = player;

            Matchers = new List<StuffSearchCondition>();
        }

        public event EventHandler<ItemMatchedEventArgs> ItemMatched;
        /// <summary>
        /// MatchDelegate
        /// </summary>
        /// <returns>true- continue</returns>
        public delegate bool MatchDelegate(TradeType tradeType, StuffSearchCondition condition, TradeItemController controller);

        /// <summary>
        /// Starts the search.
        /// </summary>
        public void StartSearch()
        {
            DoSearch(FindHandler);
        }

        /// <summary>
        /// Starts the search.
        /// </summary>
        /// <param name="matchDelegate">The search delegate.</param>
        public void DoSearch(MatchDelegate matchDelegate)
        {
            foreach (var itemMatcher in Matchers)
            {
                if (string.IsNullOrEmpty(itemMatcher.ItemName) && !itemMatcher.Enabled) continue;

                if (itemMatcher.AuctionConditions.Count > 0)
                {
                    Search(TradeType.Auction, itemMatcher, matchDelegate);
                }
                if (itemMatcher.SalesConditions.Count > 0)
                {
                    Search(TradeType.Sale, itemMatcher, matchDelegate);
                }
            }


        }
        private void Search(TradeType tradeType, StuffSearchCondition itemMatcher, MatchDelegate matchDelegate)
        {
            AppCore.LogAccountant.DebugFormat("Ищем {0} {1}.",
                itemMatcher.ItemName, tradeType == TradeType.Auction ? "на аукционе" : "в продаже");

            var matchers = itemMatcher.GetConditions(tradeType).GetActive();

            var hasGold = matchers.Any(e => e.Price.Resource == Resource.Gold);
            var hasCristall = matchers.Any(e => e.Price.Resource == Resource.Crystals);
            var minLevel = matchers.Min(e => e.MinResalesCount);
            controller.DoSearchOnMarket(tradeType, itemMatcher.ItemName, GetResealesFilter(minLevel), hasCristall, hasGold);

            foreach (var itemController in EnumerateTradeItems())
            {
                foreach (var matcher in matchers.ToArray())
                {
                    var matchResult = matcher.Match(Player, itemController, tradeType);
                    switch (matchResult)
                    {
                        case MatchResult.Ok:
                            if (!matchDelegate(tradeType, itemMatcher, itemController))
                                return;
                            break;

                        case MatchResult.BadPriceAmmount:
                            matchers.Remove(matcher);
                            break;
                        default:
                            break;
                    }
                }
                if (matchers.Count == 0) return;
            }
        }

        private ResealesFilter GetResealesFilter(int level)
        {
            if (level == 10) return ResealesFilter.Equal10;
            if (level > 6) return ResealesFilter.Greater7;
            if (level > 4) return ResealesFilter.Greater5;
            if (level > 2) return ResealesFilter.Greater3;
            return ResealesFilter.Any;

        }

        /// <summary>
        /// Investments the money.
        /// </summary>
        public bool InvestmentMoney()
        {
            var success = false;
            if (!CanInvest) return false;
        investAgain:
            try
            {
                DoSearch(delegate(TradeType tradeType, StuffSearchCondition condition, TradeItemController item)
                                       {
                                           var price = item.GetPrice(tradeType);
                                           if (!item.BuyItem(tradeType)) return false;
                                           var message = controller.GetMessage();
                                           if (!string.IsNullOrEmpty(message))
                                           {
                                               AppCore.LogAccountant.Warn("Покупка не сделана.", "Покупка не сделана по причине " + message);
                                               throw new InvalidOperationException(message);
                                           }
                                           AppCore.LogAccountant.DebugFormat("{0} {1} по цене {2}.",
                                               tradeType == TradeType.Sale ? "Куплено" : "Сделана ставка", condition, price);

                                           success = true;
                                           Player.UpdateResources();

                                           return false;
                                       });
            }
            catch (InvalidOperationException ex)
            {
                AppCore.LogSystem.Debug("Несмогли инвестировать ресурсы.", ex);
                goto investAgain;
            }
            return success;
        }



        private IEnumerable<TradeItemController> EnumerateTradeItems()
        {
            var pages = new Pager(browser, controller.UrlTradeNow);
            do
            {
                var results = browser.Table(Find.ByClass("trade center"));
                for (var rowIndex = 1; rowIndex < results.TableRows.Count; rowIndex++)
                {
                    var row = results.TableRows[rowIndex];
                    if (row.TableCells.Count == 1) break;

                    yield return new TradeItemController(controller, row);
                }
            } while (pages.GotoNextPage());
        }

        /// <summary>
        /// Finds the handler.
        /// </summary>
        /// <param name="tradeType">Type of the trade.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        private bool FindHandler(TradeType tradeType, StuffSearchCondition condition, TradeItemController controller)
        {
            OnItemMatched(new ItemMatchedEventArgs(condition, controller.GetPrice(tradeType)));
            return true;
        }



        /// <summary>
        /// Raises the <see cref="E:ItemMatched"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BotvaSpider.BookKeeping.ItemMatchedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemMatched(ItemMatchedEventArgs e)
        {
            EventHandler<ItemMatchedEventArgs> itemMatchedHandler = ItemMatched;
            if (itemMatchedHandler != null) itemMatchedHandler(this, e);
        }
    }
}