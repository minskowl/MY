#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.ComponentModel;
using System.Web.UI;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    [DefaultEvent("Click")]
    public class DeleteButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteButton"/> class.
        /// </summary>
        public DeleteButton()
        {
            Mode = ButtonType.Link;
            ImageUrl = "~/App_Themes/images/delete_16.gif";
        }

        /// <summary>
        /// Gets or sets a value indicating whether [with confirmation].
        /// </summary>
        /// <value><c>true</c> if [with confirmation]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [BindableAttribute(false)]
        [Description("Confirm to delete action")]
        [Themeable(false)]
        [DefaultValueAttribute(true)]
        public bool WithConfirmation
        {
            get
            {
                if (ViewState["WithConfirmation"] == null)
                    return true;
                return (bool)ViewState["WithConfirmation"];
            }

            set
            {
                ViewState["WithConfirmation"] = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            if(WithConfirmation)
            {
                OnClientClick = "return confirm('Are you sure you want to delete the selected item?');";
            }
            else
            {
                OnClientClick = string.Empty;
            }

            base.OnPreRender(e);

        }
    }
}
