using System;
using Savchin.Web.UI;

namespace Site.Core
{
    /// <summary>
    /// Summary description for RedirectorAdmin
    /// </summary>
    public class Redirector : RedirectorBase
    {

        private const string UserArea = "UserArea/";

        /// <summary>
        /// Goes to default page.
        /// </summary>
        public static void GoToDefaultPage()
        {
            GoTo(DefaultPage);
        }


        /// <summary>
        /// Goes to user default page.
        /// </summary>
        public static void GoToUserDefaultPage()
        {
            GoTo(UserArea + DefaultPage);
        }
    }
}