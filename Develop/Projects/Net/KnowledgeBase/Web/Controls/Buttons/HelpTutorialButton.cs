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
using Savchin.Web.Core;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class HelpTutorialButton : ButtonEx
    {

        /// <summary>
        /// Inits the control.
        /// </summary>
        public HelpTutorialButton()
        {
            ToolTip = "Tutorial";
            Mode = ButtonType.Link;
            ImageUrl = ImagePathProvider.MovieImage;
        }

        /// <summary>
        /// Gets or sets the site file ID.
        /// </summary>
        /// <value>The site file ID.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(0)]
        public virtual int SiteFileID
        {
            get
            {
                object obj1 = ViewState["SiteFileID"];
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 0;
            }
            set
            {
                ViewState["SiteFileID"] = value;
            }
        }


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (SiteFileID > 0)
                OnClientClick = "window.open('" + RedirectorBase.GetAbsoluteUrl("/TutorialView.aspx") + RedirectorBase.IdQueryString(SiteFileID) + "','_blank','directories=no,location=no,menubar=no,status=no,toolbar=no');";
            else
                Target = "_blank";

            UseSubmitBehavior = false;

        }
    }
}
