using System;
using Savchin.Web.Core;

namespace KnowledgeBase.SiteCore
{
    /// <summary>
    /// Summary description for Redirector
    /// </summary>
    public class Redirector : RedirectorBase
    {
        #region Public


        /// <summary>
        /// Gets the knowledge info public page.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string GetKnowledgeInfoPublicPage(Guid id)
        {
            return GetAbsoluteUrl("KnowledgeInfo.aspx" + IdQueryString(id));
        }

        /// <summary>
        /// Goes to public default page.
        /// </summary>
        public static void GoToPublicDefaultPage()
        {
            GoTo(DefaultPage);
        }
        #endregion
    }
}
