
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI.Extenders
{
    /// <summary>
    /// PanelEx
    /// </summary>
    public class PanelEx : Panel
    {

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PanelEx"/> is hide.
        /// </summary>
        /// <value><c>true</c> if hide; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool Hide
        {
            get
            {
                object obj1 = ViewState["Hide"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return false;
            }
            set { ViewState["Hide"] = value; }
        }
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        [Category("Behavior")]
        public virtual string Tag
        {
            get { return (string)ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Hide)
            {
                Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            }
            else
            {
                Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, string.Empty);
            }
        }
    }
}