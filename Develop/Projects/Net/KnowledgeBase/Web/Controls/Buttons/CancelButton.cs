#region Version & Copyright
/* 
 * $Id: SocketListControl.ascx.cs 18228 2007-06-25 11:53:02Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System.ComponentModel;
using System.Web.UI.WebControls;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    /// <summary>
    /// Cancel Button
    /// </summary>
    public class CancelButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelButton"/> class.
        /// </summary>
        public CancelButton()
        {
            CausesValidation = false;
            Text = "Cancel";
            Mode = ButtonType.Link;
            ImageUrl = ImagePathProvider.CancelImage;
        }
    }
}
