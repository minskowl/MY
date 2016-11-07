#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Savchin.Text;


namespace Savchin.Web
{
    /// <summary>
    /// JavaScriptLibrary
    /// </summary>
    public static class JavaScriptLibrary
    {
        /// <summary>
        /// Gets the close window.
        /// </summary>
        /// <value>The close window.</value>
        public static string WindowCloseScript
        {
            get { return "\n window.close(); \n"; }
        }

 
        ///// <summary>
        ///// Gets the PNG fix script.
        ///// </summary>
        ///// <value>The PNG fix script.</value>
        //public static string PngFixScript
        //{
        //    get { return "\n AlphaRender(); \n "; }
        //}

    }
}
