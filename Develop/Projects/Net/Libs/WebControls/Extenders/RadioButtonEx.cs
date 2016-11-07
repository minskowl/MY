#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    [ValidationProperty("Checked")]
    public class RadioButtonEx : RadioButton
    {
        /// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button"></see> control's <see cref="E:System.Web.UI.WebControls.Button.Click"></see> event is raised.</summary>
        /// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button"></see> control's <see cref="E:System.Web.UI.WebControls.Button.Click"></see> event is raised.</returns>
        [Category("Behavior")]
        [DefaultValue("")]
        [Themeable(false)]
        public virtual string OnClientClick
        {
            get
            {
                string text1 = (string)ViewState["OnClientClick"];
                if (text1 == null)
                {
                    return string.Empty;
                }
                return text1;
            }
            set
            {
                ViewState["OnClientClick"] = value;
            }
        }

        /// <summary>
        /// Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.CheckBox"></see> control to be rendered to the specified output stream.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if(! string.IsNullOrEmpty(OnClientClick) )
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Util.EnsureEndWithSemiColon(OnClientClick));
            }
        }
    }
}
