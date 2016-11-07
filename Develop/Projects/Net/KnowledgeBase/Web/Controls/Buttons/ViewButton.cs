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

    /// <summary>
    /// ViewButton 
    /// </summary>
    public class ViewButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewButton"/> class.
        /// </summary>
        public ViewButton()
        {
            InitControl();
        }

        /// <summary>
        /// Inits the control.
        /// </summary>
        private void InitControl()
        {
            ToolTip = "View";
            Mode = ButtonType.Link;
            ImageUrl = ImagePathProvider.PreviewImage;
        }
    }
}
