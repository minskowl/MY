using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.SiteCore;
using Savchin.Text;
using Savchin.Web.UI;

public partial class Controls_KnowledgeView : UserControl
{
    private Knowledge knowledge;

    /// <summary>
    /// Gets or sets the knowledge.
    /// </summary>
    /// <value>The knowledge.</value>
    public Knowledge Knowledge
    {
        get { return knowledge; }
        set { knowledge = value; }
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ((PageEx)Page).PageIncludes.AddJavaScript(GetType(), "highlight", AppSettings.ApplicationJavaScriptsUrl + "highlight/highlight.js");
        ((PageEx)Page).PageIncludes.AddCss(GetType(), "highlight", AppSettings.ApplicationJavaScriptsUrl + "highlight/styles/vs.css");
        Page.ClientScript.RegisterStartupScript(GetType(), "initHighlighting", "hljs.initHighlightingOnLoad.apply(null, hljs.ALL_LANGUAGES);", true);

        if (IsPostBack)
            return;

        HeaderPage1.Text = HttpUtility.HtmlEncode(Knowledge.Title);
        labelType.Text = string.Format("Type: {0} Created: {1} by {2} ",
                                       Knowledge.KnowledgeType,
                                       Knowledge.CreationDate.ToShortDateString(),
                                       Knowledge.Creator.FullName
                                       );

        labelSummary.Text = Knowledge.Summary;
        ShowKeywords();
    }

    private void ShowKeywords()
    {
        IList<Keyword> keywords = KbContext.CurrentKb.ManagerKeyword.GetByListID(knowledge.KewordsAssociations);

        var builder = new StringBuilder("Keywords:");

        foreach (Keyword keyword in keywords)
        {
            builder.AppendFormat("&nbsp;&nbsp;{0}&nbsp;&nbsp;", keyword.Name);

        }
        labelKeywords.Text = builder.ToString();

        var literal = new Literal { Text = string.Format("<meta content=\"{0}\" name=\"keywords\"/>", StringUtil.Join(keywords, ", ", "Name")) };
        Page.Header.Controls.Add(literal);

    }
}
