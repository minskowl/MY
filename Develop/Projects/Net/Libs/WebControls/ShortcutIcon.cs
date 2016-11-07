

using System.ComponentModel;
using System.Web.UI;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Adds short cut icon into application
    /// </summary>
    public class ShortcutIcon : Control
    {
        /// <summary>
        /// Gets or sets image folder path
        /// </summary>
        [Description("Path to images folder")]
        [Category("Appearance")]
        public string IconUrl
        {
            get
            {
                return ViewState["IconUrl"] as string;
            }
            set { ViewState["IconUrl"] = value; }
        }

        /// <summary>
        /// Renders control like a link tag
        /// </summary>
        /// <param name="writer">Output object</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(
                "<link  rel=\"shortcut icon\" href=\"" + IconUrl + "\"/>");

            base.Render(writer);
        }
    }
}
