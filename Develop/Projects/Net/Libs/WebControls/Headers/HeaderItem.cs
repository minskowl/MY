#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{

    public class HeaderItem : Label
    {
        /// <summary>
        /// Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.Label"/> control.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value used to render the <see cref="T:System.Web.UI.WebControls.Label"/>.</returns>
        protected override System.Web.UI.HtmlTextWriterTag TagKey
        {
            get { return System.Web.UI.HtmlTextWriterTag.H3; }
        }

    }
}
