using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Savchin.Core;
using WatiN.Core;
using BotvaSpider.Gears;

namespace BotvaSpider.Core
{
    public enum ResealesFilter
    {
        Any = 0,
        Greater3 = 3,
        Greater5 = 5,
        Greater7 = 7,
        Equal10 = 10,
        Less3 = 23,
        Less5 = 25,
        Less7 = 27
    }
    public partial class GameController
    {

        /// <summary>
        /// Does the search on market.
        /// </summary>
        /// <param name="sortType">Type of the sort.</param>
        /// <param name="matcher">The matcher.</param>
        /// <param name="resealesFilter">The level.</param>
        /// <param name="inCristals">if set to <c>true</c> [in cristals].</param>
        /// <param name="inGold">if set to <c>true</c> [in gold].</param>
        public void DoSearchOnMarket(TradeType sortType, string matcher, ResealesFilter resealesFilter, bool inCristals, bool inGold)
        {
            var url = string.Format("{0}&order={1}&dir=asc",
                UrlTradeNow,
                (sortType == TradeType.Sale ? "buy" : "bid"));

            if (inCristals != inGold)
            {
                url = url + "&money=" + (inGold ? 1 : 2);
            }
            var postData = string.Format("range={0}&filter={1}",
                (int)resealesFilter,
                HttpUtility.UrlEncode(Encoding.Default.GetBytes(matcher)).ToUpper());


            GoTo(url, postData);

        }






        public delegate bool StringMatcher(string value);
        /// <summary>
        /// Clears the wardrobe.
        /// </summary>
        public void ClearWardrobe(StringMatcher matcher)
        {
            OpenUrl(UrlTradePlace);
            var table = Browser.Table(Find.ByClass("front_items bags bag_0"));
            var previousImageCount = 0;
        doagain:
            var ticketsImages = table.Images.Where(e =>
                !string.IsNullOrEmpty(e.Src) && matcher(e.Src)).ToArray();

            if (ticketsImages.Length == 0) return;
            if (previousImageCount == ticketsImages.Length) return;
            previousImageCount = ticketsImages.Length;
            var image = ticketsImages[0];

            var cell = (TableCell)image.Parent;
            var column = cell.Index;
            var row = cell.ContainingTableRow.Index;
            var cmdCell = table.TableRows[row + 1].TableCells[column];
            if (cmdCell.Links.Count > 0)
                cmdCell.Links[0].Click();

            goto doagain;

        }
        /// <summary>
        /// Sells the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="priceSale">The price sale.</param>
        public void Sell(Enum item, Price priceSale)
        {
            Sell(item, priceSale, null, 0);
        }

        /// <summary>
        /// Sells the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="priceSale">The price sale.</param>
        /// <param name="priceAuction">The price auction.</param>
        /// <param name="auctionStep">The auction step.</param>
        public void Sell(Enum item, Price priceSale, Price priceAuction, int auctionStep)
        {
            foreach (var bag in EnumerateActiveBag())
            {
                Sell(bag, item, priceSale, priceAuction, auctionStep);
            }
        }

        private void Sell(Table bag, Enum item, Price priceSale, Price priceAuction, int auctionStep)
        {
            var attempt = 0;
            do
            {
                if (bag == null)
                {
                    log.Suggestion("Сумка ненайдена", "Чтобы продавать нужна активная сумка");
                    return;
                }
                var itemUrl = ItemBuilder.GetItemSmallImageUrl(item);
                var itemImage = bag.Images.Where(e => !string.IsNullOrEmpty(e.Src) &&
                                    string.Compare(e.Src, itemUrl, true) == 0 &&
                                    string.IsNullOrEmpty(e.Parent.ClassName)).FirstOrDefault();
                //var itemImage = bag.Image(Find.BySrc(itemUrl));
                if (itemImage == null || !itemImage.Exists) return;
                ((TableCell)itemImage.Parent.PreviousSibling).RadioButtons[0].Checked = true;
                browser.Image(Find.BySrc(UrlButtonTrade)).Click();

                if (priceSale != null)
                {
                    browser.CheckBox(Find.ByName("fast_trade")).Checked = true;
                    browser.SelectList("fast_type").SelectByValue(((int)priceSale.Resource).ToString());
                    browser.TextField("fast_price").Value = priceSale.Ammount.ToString();
                }

                if (priceAuction != null)
                {
                    browser.CheckBox(Find.ByName("trade")).Checked = true;
                    browser.SelectList("bid_type").SelectByValue(((int)priceAuction.Resource).ToString());
                    browser.TextField("bid_start").Value = priceAuction.Ammount.ToString();
                    browser.TextField("bid_step").Value = auctionStep.ToString();
                }

                browser.Image(Find.BySrc(UrlButtonTrade)).Click();

                var builder = new StringBuilder();
                if (priceSale != null)
                    builder.AppendLine("Цена продажи: " + priceSale);

                if (priceAuction != null)
                    builder.AppendLine("Цена аукцион: " + priceAuction);

                log.Info("Выставлен на продажу " + item.GetDescription(), builder.ToString());
                attempt++;

            } while (attempt < 20);
        }

        private IEnumerable<Table> EnumerateActiveBag()
        {
            OpenUrl(UrlTradePlace);
            for (var i = 3; i > 0; i--)
            {
                //Search bags
                var table = Browser.Table(Find.ByClass("front_items bags bag_" + i));
                if (!table.Exists) continue;

                //Search active bags
                var blockImage = table.Image(Find.BySrc(UrlImageBlock));
                if (blockImage.Exists) continue;

                yield return table;
            }
        }

    }
}
