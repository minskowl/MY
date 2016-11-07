#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class AddButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddButton"/> class.
        /// </summary>
        public AddButton()
        {
            Mode = ButtonType.Link;
            ToolTip = "Add";
            ImageUrl = ImagePathProvider.AddImage;
        }
    }

}
