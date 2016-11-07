using Savchin.Core;
using WatiN.Core;

namespace BotvaSpider.Core
{
    class FilteredPager : Pager
    {
        private readonly IRange<int> filter;
        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredPager"/> class.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="urlMatcher">The URL matcher.</param>
        /// <param name="filter">The filter.</param>
        public FilteredPager(IE browser, string urlMatcher, IRange<int> filter)
            : base(browser, urlMatcher, filter.From)
        {
            this.filter = filter;
        }
        /// <summary>
        /// Determines whether this instance can move.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance can move; otherwise, <c>false</c>.
        /// </returns>
        protected override bool CanMove()
        {
            return filter.IsInRange(CurrentPage + 1);
        }
    }
}
