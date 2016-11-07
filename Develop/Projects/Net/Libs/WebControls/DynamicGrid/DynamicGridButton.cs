#region Version & Copyright
/* 
 * $Id: DynamicGridButton.cs 19679 2007-08-08 18:15:20Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.Text;

namespace Savchin.Web.UI
{
    public class DynamicGridButton
    {
        #region Properies
        private string navigateUrl = "#";
        private string text;
        private string imageUrl;
        private string alternateText;
        private string onClientClick;
        private string target;

        /// <summary>
        /// Gets or sets the navigate URL.
        /// </summary>
        /// <value>The navigate URL.</value>
        public string NavigateUrl
        {
            get { return navigateUrl; }
            set { navigateUrl = value; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        /// <summary>
        /// Gets or sets the alternate text.
        /// </summary>
        /// <value>The alternate text.</value>
        public string AlternateText
        {
            get { return alternateText; }
            set { alternateText = value; }
        }

        /// <summary>
        /// Gets or sets the on client click.
        /// </summary>
        /// <value>The on client click.</value>
        public string OnClientClick
        {
            get { return onClientClick; }
            set { onClientClick = value; }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public string Target
        {
            get { return target; }
            set { target = value; }
        }

        #endregion

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("<a href=\"{0}\" ", navigateUrl);

            if (!string.IsNullOrEmpty(alternateText))
                builder.AppendFormat("title=\"{0}\" ", alternateText);

            if (!string.IsNullOrEmpty(target))
                builder.AppendFormat("target=\"{0}\" ", target);

            if (!string.IsNullOrEmpty(onClientClick))
                builder.AppendFormat("onclick=\"{0}\" ", onClientClick);

            builder.Append(">");

            if (!string.IsNullOrEmpty(imageUrl))
            {
                builder.AppendFormat("<img border=\"0\" src=\"{0}\" ", imageUrl);
                if (!string.IsNullOrEmpty(alternateText))
                    builder.AppendFormat("title=\"{0}\" alt=\"{0}\" ", alternateText);
                builder.Append(" />");
            }
            builder.Append(text);
            builder.Append("</a>");

            return builder.ToString();
        }
    }
}
