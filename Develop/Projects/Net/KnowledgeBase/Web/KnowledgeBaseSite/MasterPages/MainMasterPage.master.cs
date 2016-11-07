#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.SiteCore;
using Savchin.Web;
using Savchin.Web.UI;


public partial class MainMasterPage : MasterPage
{


    /// <summary>
    /// 
    /// </summary>
    private bool withFrame = true;

    private ScrollBars pageScrollBars = ScrollBars.Auto;
    /// <summary>
    /// Gets or sets the page scroll bars.
    /// </summary>
    /// <value>The page scroll bars.</value>
    public ScrollBars PageScrollBars
    {
        get { return pageScrollBars; }
        set { pageScrollBars = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool WithFrame
    {
        get { return withFrame; }
        set { withFrame = value; }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (withFrame)
            frame.Attributes.Add("class", "frame");

        switch (PageScrollBars)
        {
            case ScrollBars.None:
                pageScrollArea.Attributes.CssStyle.Add(HtmlTextWriterStyle.Overflow, "hidden");
                break;
            case ScrollBars.Horizontal:
                break;
            case ScrollBars.Vertical:
                break;
            case ScrollBars.Both:
                break;
            case ScrollBars.Auto:
                pageScrollArea.Attributes.CssStyle.Add(HtmlTextWriterStyle.Overflow, "auto");
                break;
            default:
                pageScrollArea.Attributes.CssStyle.Add(HtmlTextWriterStyle.Overflow, "auto");
                break;
        }
    }
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        SetPageTitle();
        if (IsPostBack)
            return;
        {
            ControlHelper.SetBackgroundImager(headerCell, Page.Theme, "images/title_bg.gif");

            if (ControlHelper.BrowserType == BrowserType.FireFox)
            {
                tableMain.Attributes.CssStyle.Remove("height");
            }
        }
    }
    private void SetPageTitle()
    {
        //TODO: Uncomment when site map
        //SiteMapNode currentNode = SiteMap.CurrentNode;
        //if (currentNode != null)
        //{
        //    Page.Header.Title = currentNode.Title;
        //}
        //else
        //{
        //    Page.Header.Title = string.Empty;
        //}
        JavaScriptBuilder loadScript = ((PageEx)Page).JavaScriptOnLoad;
        loadScript.AppendLine("\n window.parent.document.title='KB :: " +
                                                    JavaScriptBuilder.ApplyEscapeSequences(Page.Header.Title) + "'; \n");

        loadScript.AppendLine(SiteJavaScriptLibrary.RestorFrameScript);
    }

}
