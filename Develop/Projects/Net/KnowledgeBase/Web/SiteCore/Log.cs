using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace KnowledgeBase.SiteCore
{
    /// <summary>
    /// Loggers 
    /// </summary>
    public static class Log
    {
        public static readonly ILog Site = LogManager.GetLogger("Site");
        public static readonly ILog DAL = LogManager.GetLogger("DAL");

        public static readonly ILog SMTP = LogManager.GetLogger("SMTP");
        public static readonly ILog BusinessLayer = LogManager.GetLogger("BusinessLayer");



        ///// <summary>
        ///// Gets the full trace info.
        ///// </summary>
        ///// <returns></returns>
        //public static string GetFullTraceInfo()
        //{
        //    return string.Format("Trace info \n AppVerssion={0} \n Url={1} \n UserId={2} \n ConfigName = {3} \n Referer = {4}",
        //                         AppSettings.AppVersion,
        //                         HttpContext.Current.Request.Url,
        //                         Thread.CurrentPrincipal.Identity.Name,
        //                         WamSettings.Current.ConfigName,
        //                         HttpContext.Current.Request.UrlReferrer != null ?
        //                            HttpContext.Current.Request.UrlReferrer.ToString() :
        //                            string.Empty);

        //}
    }
}
