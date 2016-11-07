using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Data
{
    public class Clan
    {
        /// <summary>
        /// Gets or sets the clan ID.
        /// </summary>
        /// <value>The clan ID.</value>
        public int ClanID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }
        /// <summary>
        /// Gets or sets the treasury.
        /// </summary>
        /// <value>The treasury.</value>
        public int Treasury { get; set; }
        /// <summary>
        /// Gets or sets the soldiers.
        /// </summary>
        /// <value>The soldiers.</value>
        public int Soldiers { get; set; }
        /// <summary>
        /// Gets or sets the barrack capacity.
        /// </summary>
        /// <value>The barrack capacity.</value>
        public int BarrackCapacity { get; set; }
        /// <summary>
        /// Creates the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static Clan Create(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Trim() == "Не состоит в клане") return null;

            var pos = text.IndexOf('[');
            if (pos == -1)
            {
                return new Clan { Name = text };
            }

            var name = text.Substring(0, pos).Trim();
            var tag = text.Substring(pos+1, text.IndexOf(']') - pos-1).Trim();
            return new Clan
                       {
                           Name = name,
                           Tag = tag
                       };
        }

    }
}