#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class EditButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditButton"/> class.
        /// </summary>
        public EditButton()
        {
            Mode = ButtonType.Link;
            ToolTip = "Edit";
            ImageUrl = "~/App_Themes/images/edit_16.gif";
        }
    }
}
