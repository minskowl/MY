using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WatiN.Core;

namespace Bashni.Core
{
    class BrowserFactory
    {
        private static List<IE> instances = new List<IE>();
        private const string urlEmpty = "about:blank";
        /// <summary>
        /// Creates the browser.
        /// </summary>
        /// <returns></returns>
        public static IE CreateBrowser(string domainUrl)
        {
            if (instances.Count == 0)
            {
                var ie = new IE(domainUrl);
                instances.Add(ie);
                return ie;
            }
            else
            {
                instances[0].Navigate(urlEmpty, BrowserNavConstants.navOpenInNewTab);
                IE ie = null;
                var attempt = 0;
                do
                {
                    ie = FindNewIE();
                    if (ie != null)
                    {
                        instances.Add(ie);
                        ie.GoTo(domainUrl);
                        return ie;
                    }
                    attempt++;
                    Thread.Sleep(100);
                } while (attempt < 10);

                throw new InvalidOperationException("Необнаржена новая вкладка IE");
            }
        }

        /// <summary>
        /// Finds the new IE.
        /// </summary>
        /// <returns></returns>
        private static IE FindNewIE()
        {
            var browsers = new IECollection(true);

            return browsers.Where(browser => browser.Url == urlEmpty && browser.hWnd == instances[0].hWnd).FirstOrDefault();


        }
    }
}
