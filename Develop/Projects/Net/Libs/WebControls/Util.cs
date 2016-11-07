#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Text;
using System.Threading;
using System.Web;
using log4net;

namespace Savchin.Web.UI
{
    internal static class Util
    {
        internal static readonly ILog Log = LogManager.GetLogger("WebControls");

        /// <summary>
        /// Gets the full trace info.
        /// </summary>
        /// <returns></returns>
        public static string GetFullTraceInfo()
        {
            return string.Format("Trace info \n AppVerssion={0} \n Url={1} \n UserId={2} \n ConfigName = {3} \n Referer = {4}",
                                 "",
                                 HttpContext.Current.Request.Url,
                                 Thread.CurrentPrincipal.Identity.Name,
                                 "",
                                 HttpContext.Current.Request.UrlReferrer != null ?
                                    HttpContext.Current.Request.UrlReferrer.ToString() :
                                    string.Empty);

        }
        /// <summary>
        /// Ensures the end with semi colon.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string EnsureEndWithSemiColon(string value)
        {
            if (value != null)
            {
                int num1 = value.Length;
                if ((num1 > 0) && (value[num1 - 1] != ';'))
                {
                    return (value + ";");
                }
            }
            return value;
        }

        /// <summary>
        /// Merges the script.
        /// </summary>
        /// <param name="firstScript">The first script.</param>
        /// <param name="secondScript">The second script.</param>
        /// <returns></returns>
        internal static string MergeScript(string firstScript, string secondScript)
        {
            if (!string.IsNullOrEmpty(firstScript))
            {
                return (firstScript + secondScript);
            }
            if (secondScript.TrimStart(new char[0]).StartsWith("javascript:", StringComparison.Ordinal))
            {
                return secondScript;
            }
            return ("javascript:" + secondScript);
        }

        /// <summary>
        /// Quotes the J script string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="forUrl">if set to <c>true</c> [for URL].</param>
        /// <returns></returns>
        internal static string QuoteJScriptString(string value, bool forUrl)
        {
            StringBuilder builder1 = null;
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            int num1 = 0;
            int num2 = 0;
            for (int num3 = 0; num3 < value.Length; num3++)
            {
                switch (value[num3])
                {
                    case '%':
                        if (!forUrl)
                        {
                            break;
                        }
                        if (builder1 == null)
                        {
                            builder1 = new StringBuilder(value.Length + 6);
                        }
                        if (num2 > 0)
                        {
                            builder1.Append(value, num1, num2);
                        }
                        builder1.Append("%25");
                        num1 = num3 + 1;
                        num2 = 0;
                        goto Label_01F2;

                    case '\'':
                        if (builder1 == null)
                        {
                            builder1 = new StringBuilder(value.Length + 5);
                        }
                        if (num2 > 0)
                        {
                            builder1.Append(value, num1, num2);
                        }
                        builder1.Append(@"\'");
                        num1 = num3 + 1;
                        num2 = 0;
                        goto Label_01F2;

                    case '\\':
                        if (builder1 == null)
                        {
                            builder1 = new StringBuilder(value.Length + 5);
                        }
                        if (num2 > 0)
                        {
                            builder1.Append(value, num1, num2);
                        }
                        builder1.Append(@"\\");
                        num1 = num3 + 1;
                        num2 = 0;
                        goto Label_01F2;

                    case '\t':
                        if (builder1 == null)
                        {
                            builder1 = new StringBuilder(value.Length + 5);
                        }
                        if (num2 > 0)
                        {
                            builder1.Append(value, num1, num2);
                        }
                        builder1.Append(@"\t");
                        num1 = num3 + 1;
                        num2 = 0;
                        goto Label_01F2;

                    case '\n':
                        if (builder1 == null)
                        {
                            builder1 = new StringBuilder(value.Length + 5);
                        }
                        if (num2 > 0)
                        {
                            builder1.Append(value, num1, num2);
                        }
                        builder1.Append(@"\n");
                        num1 = num3 + 1;
                        num2 = 0;
                        goto Label_01F2;

                    case '\r':
                        if (builder1 == null)
                        {
                            builder1 = new StringBuilder(value.Length + 5);
                        }
                        if (num2 > 0)
                        {
                            builder1.Append(value, num1, num2);
                        }
                        builder1.Append(@"\r");
                        num1 = num3 + 1;
                        num2 = 0;
                        goto Label_01F2;

                    case '"':
                        if (builder1 == null)
                        {
                            builder1 = new StringBuilder(value.Length + 5);
                        }
                        if (num2 > 0)
                        {
                            builder1.Append(value, num1, num2);
                        }
                        builder1.Append("\\\"");
                        num1 = num3 + 1;
                        num2 = 0;
                        goto Label_01F2;
                }
                num2++;
            Label_01F2: ;
            }
            if (builder1 == null)
            {
                return value;
            }
            if (num2 > 0)
            {
                builder1.Append(value, num1, num2);
            }
            return builder1.ToString();
        }

 



    }
}
