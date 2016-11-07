using System;
using BotvaSpider.Configuration;
using BotvaSpider.Core;

namespace BotvaSpider.BookKeeping
{
    /// <summary>
    /// ItemMatchedEventArgs
    /// </summary>
    public class ItemMatchedEventArgs : EventArgs
    {

        private readonly StuffSearchCondition condition;
        private readonly Price price;

        public ItemMatchedEventArgs(StuffSearchCondition condition, Price price)
        {
            this.condition = condition;
            this.price = price;
        }

        public Price Price
        {
            get { return price; }
        }

        public StuffSearchCondition Condition
        {
            get { return condition; }
        }
    }
}