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
    public class ToolbarButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolbarButton"/> class.
        /// </summary>
        public ToolbarButton()
        {
            init();
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        private void init()
        {
            UseSubmitBehavior=false;
            CssClass = "toolBar";
            Width = 20;
            CausesValidation=false;
            Mode = ButtonType.ImageButton;
        }
    }
}
