/*
 * Created by SharpDevelop.
 * User: Alexey
 * Date: 10/31/2004
 * Time: 8:20 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Reflection;
using System.Resources;

namespace Savchin.Html
{
    /// <summary>
    /// RD
    /// </summary>
    internal sealed class RD
    {
        private static ResourceManager rm;

        internal static string GetString(string resId)
        {
            //if(null == rm)
            //    rm = new ResourceManager("strings", Assembly.GetExecutingAssembly());
            //return rm.GetString(resId);
            if (null == rm) rm = strings.ResourceManager;
            return rm.GetString(resId);
        }

        internal static string GetStringFormat(string resId, params object[] arglist)
        {
            string format = GetString(resId);
            return String.Format(format, arglist);
        }
    }
}
