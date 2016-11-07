using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Reprents single panel control
    /// </summary>
    public class TabPanel : Panel
    {
        #region Properties

        /// <summary>
        /// Gets or sets panel title text
        /// </summary>
        public string PanelTitle
        {
            get { return ViewState["PanelTitle"] as string;  }
            set { ViewState["PanelTitle"] = value; }
        }

        #endregion

    }
}
