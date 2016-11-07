using System;
using System.Linq;
using System.Threading;
using WatiN.Core;

namespace BotvaSpider.Core
{
    class Pager
    {
        private readonly IE browser;
        private readonly string urlMatcher;
        private int currentPage = 1;

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage
        {
            get { return currentPage; }
        }

        /// <summary>
        /// Gets or sets the sleep.
        /// </summary>
        /// <value>The sleep.</value>
        public int Sleep { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pager"/> class.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="urlMatcher">The URL matcher.</param>
        public Pager(IE browser, string urlMatcher)
            : this(browser, urlMatcher, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pager"/> class.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="urlMatcher">The URL matcher.</param>
        /// <param name="firstPage">The first page.</param>
        public Pager(IE browser, string urlMatcher, int firstPage)
        {
            if (firstPage < 1) throw new ArgumentException("firstPage must be greter than 0.", "firstPage");
            this.browser = browser;
            this.urlMatcher = urlMatcher;
            this.currentPage = firstPage;

            if (currentPage != 1)
            {
                var link = GetLinkpage(currentPage);
                if (CanMove(link)) link.Click();
            }


        }



        /// <summary>
        /// Gotoes the next page.
        /// </summary>
        /// <returns></returns>
        public bool GotoNextPage()
        {
            if (!CanMove()) return false;
            var link = GetLinkpage(currentPage + 1);
            if (CanMove(link))
            {
                currentPage++;
                link.Click();
                if (Sleep > 0) Thread.Sleep(Sleep);
                return true;
            }
            return false;
        }
        protected virtual bool CanMove()
        {
            return true;
        }
        /// <summary>
        /// Determines whether this instance can move the specified link.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can move the specified link; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanMove(Link link)
        {
            return (link != null && link.Exists);
        }

        /// <summary>
        /// Gotoes the previous page.
        /// </summary>
        /// <returns></returns>
        public bool GotoPreviousPage()
        {
            if (currentPage == 1) return false;
            var link = GetLinkpage(currentPage - 1);
            if (link != null && link.Exists)
            {
                currentPage--;
                link.Click();
                return true;
            }
            return false;
        }

        private Link GetLinkpage(int numPage)
        {
            var mathcerLength = urlMatcher.Length;
            return browser.Links.Where(
                  e => e.Url.StartsWith(urlMatcher) && e.Url.LastIndexOf("&page=" + numPage) >= mathcerLength).FirstOrDefault();


        }
    }
}
