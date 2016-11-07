using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// HeaderPage
    /// </summary>
    public class HeaderPage : Label
    {
        /// <summary>
        /// Gets or sets a value indicating whether [auto set title].
        /// </summary>
        /// <value><c>true</c> if [auto set title]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(true)]
        [DefaultValue(true)]
        public virtual bool AutoSetTitle
        {
            get
            {
                object obj1 = ViewState["AutoSetTitle"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }
            set { ViewState["AutoSetTitle"] = value; }
        }


        /// <summary>
        /// Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.Label"/> control.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value used to render the <see cref="T:System.Web.UI.WebControls.Label"/>.
        /// </returns>
        protected override System.Web.UI.HtmlTextWriterTag TagKey
        {
            get { return System.Web.UI.HtmlTextWriterTag.H1; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            if (AutoSetTitle)
                Page.Header.Title = Text;
        }
    }
}
