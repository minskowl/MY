using System.Collections.Generic;
using System.Linq;
using BotvaSpider.Data;

namespace BotvaSpider.Configuration
{
    public class PriceConditionsCollection : List<PriceCondition>
    {
        /// <summary>
        /// Determines whether this instance can invest the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can invest the specified player; otherwise, <c>false</c>.
        /// </returns>
        public bool CanInvest(Player player)
        {
            return this.Any(e => e.Enabled && e.ValidationAmmount < player.GetResourceCount(e.Price.Resource));
        }

        /// <summary>
        /// Gets the active.
        /// </summary>
        /// <returns></returns>
        public List<PriceCondition> GetActive()
        {
            return this.Where(e => e.Enabled).ToList();
        }
    }
}
