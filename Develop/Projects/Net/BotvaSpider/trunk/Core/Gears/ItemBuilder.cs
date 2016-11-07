using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BotvaSpider.Core;
using WatiN.Core;

namespace BotvaSpider.Gears
{
    public class ItemBuilder
    {
        #region Properties
        private static readonly List<ItemPrefixes> prefixes;
        private static char[] separator = new char[] { '_' };

        private static Regex regexSpirit;

        private static readonly Dictionary<int, SpiritType> spiritsCodes;
        /// <summary>
        /// Gets the spirits codes.
        /// </summary>
        /// <value>The spirits codes.</value>
        public static Dictionary<int, SpiritType> SpiritsCodes
        {
            get { return spiritsCodes; }
        }

        private readonly GameController _controller;
        #endregion

        /// <summary>
        /// Initializes the <see cref="ItemBuilder"/> class.
        /// </summary>
        static ItemBuilder()
        {
            spiritsCodes = new Dictionary<int, SpiritType>
                               {
                                   {101, SpiritType.Ozverin},
                                   {102, SpiritType.KyshOtsuda},
                                   {103, SpiritType.Zivoder},
                                   {104, SpiritType.MegShield},
                                   {105, SpiritType.MiracleShield},
                                   {106, SpiritType.Oglushka},
                                   {107, SpiritType.Otvratka},
                                   {108, SpiritType.Antizagavorka},
                                   {109, SpiritType.MagicPoison},
                                   {110, SpiritType.Svistelka},
                                   {111, SpiritType.AntiOffender},
                                   {112, SpiritType.Devotion},
                                   {113, SpiritType.BraveScout},
                                   {114, SpiritType.IndustriousFarmer},
                                   {115, SpiritType.AssiduousMiner},
                                   {116, SpiritType.Digression},
                                   {117, SpiritType.Berserk},
                                   {118, SpiritType.GoldenPlague},
                                   {119, SpiritType.Smesec},
                                   {120, SpiritType.BigSmesec},
                                   {121, SpiritType.Nashatyry},
                                   {122, SpiritType.Antikrut},
                                   {123, SpiritType.Titan},
                                   {124, SpiritType.ClearHeart},
                                   {125, SpiritType.Revenge}
                               };

            prefixes = new List<ItemPrefixes>();
            prefixes.Add(new ItemPrefixes { Prefix = "Weap", Type = typeof(Weapon) });
            prefixes.Add(new ItemPrefixes { Prefix = "Shield", Type = typeof(Shield) });
            prefixes.Add(new ItemPrefixes { Prefix = "Helm", Type = typeof(Helmet) });
            prefixes.Add(new ItemPrefixes { Prefix = "Arm", Type = typeof(Armor) });
            prefixes.Add(new ItemPrefixes { Prefix = "Coulomb", Type = typeof(Coulomb) });
            prefixes.Add(new ItemPrefixes { Prefix = "Ticket", Type = typeof(Ticket) });
            prefixes.Add(new ItemPrefixes { Prefix = "Key", Type = typeof(Key) });


            var shorUrl = "images/items/Magic_{0}s.png";
            regexSpirit = new Regex(String.Format(shorUrl.Replace(".", "\\."), "(?'code'\\d+)"));

        }

        public ItemBuilder(GameController controller)
        {
            _controller = controller;


        }



        #region Interface
        /// <summary>
        /// Builds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="button">The button.</param>
        /// <returns></returns>
        public WardrobeItem BuildWardrobeItem(Image item, Image button)
        {
            var result = new WardrobeItem();
            BuildItem(item, result);
            result.IsPutOn = button.Src == _controller.UrlImageButtonPutOff;
            return result;
        }

        /// <summary>
        /// Builds the item info.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public ItemInfo BuildItemInfo(Image item)
        {
            var result = new ItemInfo();
            BuildItem(item, result);
            return result;
        }

        #region ItemsUrl
        /// <summary>
        /// Gets the item from small image URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static Enum GetItemFromSmallImageUrl(string url)
        {
            var smallUrl = System.IO.Path.GetFileName(url);
            smallUrl = smallUrl.Substring(0, smallUrl.Length - 5);

            var parts = smallUrl.Split(separator);
            var code = int.Parse(parts[1]);
            var type = prefixes.Where(e => e.Prefix == parts[0]).FirstOrDefault().Type;
            return (Enum)Enum.ToObject(type, code);
        }

        /// <summary>
        /// Gets the item small image URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public string GetItemSmallImageUrl(Enum item)
        {
            return GetItemImageUrl(item, true);
        }

        /// <summary>
        /// Gets the item image URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="smalImage">if set to <c>true</c> [smal image].</param>
        /// <returns></returns>
        public string GetItemImageUrl(Enum item, bool smalImage)
        {
            var result = string.Format("{0}{1}_{2}{3}.jpg",
                                       _controller.UrlItems,
                                       GetImagePrefix(item),
                                       Convert.ToInt32(item),
                                       smalImage ? "s" : String.Empty);
            return result;
        }
        #endregion
        #endregion

        private void BuildItem(Image item, ItemInfo info)
        {
            info.Type = GetItemFromSmallImageUrl(item.Src);
            var labelText = item.OuterHtml.Split(new char[] { '"' })[1];
            ParseLabel(labelText, info);
        }

        private void ParseLabel(string text, ItemInfo info)
        {
            text = text.Replace("\\'", "\"");
            var parts = text.Split(new char[] { '\'' });

            info.Level = ParseLevel(parts[5].Trim());
            info.Spirit = ParseSpirit(parts[3]);
        }

        public static SpiritType ParseSpirit(string spiritText)
        {
            if (string.IsNullOrEmpty(spiritText)) return 0;

            var match = regexSpirit.Match(spiritText);
            if (match.Success && match.Groups["code"].Success)
            {
                var code = int.Parse(match.Groups["code"].Value);

                if (SpiritsCodes.ContainsKey(code))
                {
                    return SpiritsCodes[code];
                }

                AppCore.LogSystem.Warn("!!!!!! Неизвестный заговор", spiritText);
            }
            return 0;
        }

        private static byte ParseLevel(string levelText)
        {
            if (string.IsNullOrEmpty(levelText)) return 0;

            levelText = levelText.Substring(levelText.LastIndexOf('(') + 5);
            levelText = levelText.Substring(0, levelText.LastIndexOf(')'));
            return byte.Parse(levelText);
        }
        private static string GetImagePrefix(Enum item)
        {
            var type = item.GetType();
            return prefixes.Where(e => e.Type.Equals(type)).FirstOrDefault().Prefix;
        }

        private class ItemPrefixes
        {
            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            /// <value>The type.</value>
            public Type Type { get; set; }
            public string Prefix { get; set; }
        }
    }
}
