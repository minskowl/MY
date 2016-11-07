using BotvaSpider.Core;
using BotvaSpider.Gears;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace BotvaSpider.BookKeeping
{
    /// <summary>
    /// TradeItemControllers
    /// </summary>
    public class TradeItemController
    {
        #region Properties

        private readonly GameController _controller;
        private readonly TableRow row;


        private byte level;
        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The level.</value>
        public byte Level
        {
            get { return level; }
        }

        private byte resalesCount;
        /// <summary>
        /// Gets the resales count.
        /// </summary>
        /// <value>The resales count.</value>
        public byte ResalesCount
        {
            get { return resalesCount; }
        }

        private SpiritType spirit;
        /// <summary>
        /// Gets the spirit.
        /// </summary>
        /// <value>The spirit.</value>
        public SpiritType Spirit
        {
            get { return spirit; }
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="TradeItemController"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="row">The row.</param>
        public TradeItemController(GameController controller, TableRow row)
        {
            _controller = controller;
            this.row = row;

            var img = this.row.TableCells[0].Images[0];
            var html = img.OuterHtml;

            ParseLabel(html.Split(new[] { '"' })[1]);
        }

        #region Interface
        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <returns></returns>
        public Price GetPrice(TradeType type)
        {
            var cell = GetControllCell(type);

            int price;
            if (!int.TryParse(cell.Text, out price)) return null;


            if (cell.Images.Count == 0) return null;


            var resourceImageUrl = cell.Images[0].Src;

            if (resourceImageUrl.EndsWith(_controller.UrlImageCristal))
                return new Price { Ammount = price, Resource = Resource.Crystals };

            if (resourceImageUrl.EndsWith(_controller.UrlImageGold))
                return new Price { Ammount = price, Resource = Resource.Gold };

            return null;
        }

        /// <summary>
        /// Buys the item.
        /// </summary>
        public bool BuyItem(TradeType type)
        {
            var cell = GetControllCell(type);
            if (cell.Links.Count > 0)
            {
                cell.Links[0].Click();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return row.OuterHtml;
        }
        #endregion

        #region Helpers
        private void ParseLabel(string text)
        {
            text = text.Replace("\\'", "\"");
            var parts = text.Split(new[] { '\'' });

            ParseLevel(parts[5].Trim());
            ParseResalesCount(parts[3]);
            spirit = ItemBuilder.ParseSpirit(parts[3]);
        }

        private void ParseResalesCount(string text)
        {
            var tmp = text.Substring(text.IndexOf(':') + 2);
            tmp = tmp.Substring(0, tmp.IndexOf('<'));
            resalesCount = byte.Parse(tmp);
        }

        private void ParseLevel(string levelText)
        {
            if (string.IsNullOrEmpty(levelText)) return;

            levelText = levelText.Substring(5);
            levelText = levelText.Substring(0, levelText.IndexOf(')'));
            level = byte.Parse(levelText);
        }

        private TableCell GetControllCell(TradeType type)
        {
            return row.TableCells[(type == TradeType.Sale ? 4 : 3)];
        }
        #endregion



    }
}