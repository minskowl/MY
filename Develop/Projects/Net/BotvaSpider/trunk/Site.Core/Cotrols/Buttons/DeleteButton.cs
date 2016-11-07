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

namespace Site.Cotrols
{
    /// <summary>
    /// DeleteButton
    /// </summary>
    [DefaultEvent("Click")]
    public class DeleteButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteButton"/> class.
        /// </summary>
        public DeleteButton()
        {
            Mode = ButtonType.Link;
            ToolTip = "Удалить";
            ImageUrl = ImagePathProvider.DeleteImage;
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
                OnClientClick = "return confirm('Вы уверены, что ходите удалить выбраный объект?');";
            }
           

            base.OnPreRender(e);

        }
    }
}