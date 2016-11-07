using System;
using System.Collections.Generic;
using System.Linq;
using BotvaSpider.Core;
using WatiN.Core;

namespace BotvaSpider.Gears
{
    /// <summary>
    /// Wardrobe
    /// </summary>
    public class Wardrobe
    {
        #region Properties
        public const int PricePotionBlue = 200;

        private readonly IE _browser;
        private readonly GameController controller;

        readonly StartWithMathcer matherStartWith = new StartWithMathcer();
        private readonly List<Coulomb> coulombs;

        private readonly List<WardrobeItem> items = new List<WardrobeItem>();
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<WardrobeItem> Items
        {
            get { return items; }
        }
        private int emptySlotsCount;
        /// <summary>
        /// Gets a value indicating whether [empty slots count].
        /// </summary>
        /// <value><c>true</c> if [empty slots count]; otherwise, <c>false</c>.</value>
        public int EmptySlotsCount
        {
            get { return emptySlotsCount; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Wardrobe"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public Wardrobe(GameController controller)
        {
            this.controller = controller;
            _browser = controller.Browser;
          

            coulombs = Enum.GetValues(typeof(Coulomb)).Cast<Coulomb>().ToList();
            coulombs.Remove(Coulomb.None);
            coulombs.Remove(Coulomb.Undefined);
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        public void UpdateStatus()
        {
            emptySlotsCount = 0;
            Items.Clear();

            var table = GetWardrobeTable();
            foreach (TableRow row in table.TableRows)
                foreach (TableCell cell in row.TableCells)
                {
                    var imgItem = cell.Images[0];
                    if (imgItem.Src == controller.UrlImageEmptySlot)
                    {
                        emptySlotsCount++;
                    }
                    else if (imgItem.Src == controller.UrlImageStake)//Stavka v sbytince
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            var item = controller.ItemBuilder.BuildWardrobeItem(imgItem, cell.Images[1]);
                            Items.Add(item);
                        }
                        catch (Exception ex)
                        {
                            AppCore.LogSystem.Warn("Ошибка обновления гардероба.", imgItem.OuterHtml, ex);
                        }

                    }

                }

        }

        /// <summary>
        /// Determines whether [is put on] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if [is put on] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPutOn(Coulomb type)
        {
            var link = GetItemLink(type);
            if (link == null || !link.Exists) return false;
            return !IsPutOnLink(link);
        }

        /// <summary>
        /// Puts the on.
        /// </summary>
        /// <param name="type">The type.</param>
        public bool PutOn(Coulomb type)
        {
            Image image = GetItemImage(type);
            if (image == null || !image.Exists) return false;

            var link = (Link)image.NextSibling.NextSibling;
            if (!link.Exists) return false;
            if (IsPutOnLink(link)) link.Click();
            return true;

        }
        /// <summary>
        /// Determines whether [has small tickets].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [has small tickets]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasSmallTickets()
        {
            return HasItem(Ticket.Small);
        }



        /// <summary>
        /// Determines whether the specified item type has item.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>
        /// 	<c>true</c> if the specified item type has item; otherwise, <c>false</c>.
        /// </returns>
        public bool HasItem(Enum itemType)
        {
            var type = itemType.GetType();
            return items.Any(e => e.Type.GetType().Equals(type) && e.Type.CompareTo(itemType) == 0);
        }

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        public int GetItemCount(Enum itemType)
        {
            var type = itemType.GetType();
            return items.Where(e => e.Type.GetType().Equals(type) && e.Type.CompareTo(itemType) == 0).Count();
        }

        /// <summary>
        /// Gets the clothed pendent.
        /// </summary>
        /// <returns></returns>
        public Coulomb GetClothedPendent()
        {
            var item = items.Where(e => e.IsPutOn && e.Type is Coulomb).FirstOrDefault();
            return item == null ? Coulomb.None : (Coulomb)item.Type;
        }

        /// <summary>
        /// Tries the treat.
        /// </summary>
        public void TryTreat()
        {
            var link = getHealthButtonLink();
            if (link.Exists)
                link.Click();
        }

        /// <summary>
        /// Gets the blue potion count.
        /// </summary>
        /// <returns></returns>
        public int GetBluePotionCount()
        {
            matherStartWith.Text = controller.UrlPotionBlueStart;
            var img = _browser.Image(Find.BySrc(matherStartWith));
            if (!img.Exists) return 0;
            var tmp = img.Src;
            var startPos = tmp.LastIndexOf('_');
            var endPos = tmp.LastIndexOf('.');
            tmp = tmp.Substring(startPos + 1, endPos - startPos - 1);
            return int.Parse(tmp);
        }


        private Image GetItemImage(Enum itemType)
        {
            controller.OpenUrl(controller.UrlAccount);
            var table = GetWardrobeTable();
            var imageUrl = controller.ItemBuilder.GetItemSmallImageUrl(itemType);
            return table.Image(Find.BySrc(imageUrl));
        }



        private Table GetWardrobeTable()
        {
            try
            {
                controller.OpenUrl(controller.UrlAccount);
                var div = _browser.Div("weapons");
                return div.Tables[0];
            }
            catch (Exception ex)
            {
                controller.HandleException("Ошибка нахождения раздевалки", ex);
                return null;
            }
        }

        private Link GetItemLink(Enum item)
        {
            var image = GetItemImage(item);
            if (image == null || !image.Exists) return null;
            return (Link)image.NextSibling.NextSibling;
        }

        private bool IsPutOnLink(Link link)
        {
            return link.InnerHtml.Contains("b_on.png");
        }

        private Link getHealthButtonLink()
        {
            controller.OpenUrl(controller.UrlAccount);
            matherStartWith.Text = controller.UrlAccount + @"?activate=1144185&group=1&k=";
            return _browser.Link(Find.ByUrl(matherStartWith));
        }
    }
}