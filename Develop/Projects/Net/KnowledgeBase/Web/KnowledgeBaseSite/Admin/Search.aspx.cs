using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using System.Data;
using KnowledgeBase.SiteCore;

public partial class Search : SitePage
{
    readonly KnowledgeManager manager = KbContext.CurrentKb.ManagerKnowledge;




    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        var matcher = GetRequestString("text");

        if (string.IsNullOrEmpty(matcher))
        {
            filter.Text = matcher;
            DoSearch();
        }


    }

    /// <summary>
    /// Handles the Click event of the buttonSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    private void DoSearch()
    {
        panelItems.Visible = true;
        gridKnowledges.DataBind();
    }

    /// <summary>
    /// Gets the items data.
    /// </summary>
    /// <returns></returns>
    public DataTable GetItemsData()
    {
        DataTable result = null;
        if(panelItems.Visible)
        {
            result = manager.Search(
                filter.Text,
                filter.SelectedKnowledgeTypes,
                filter.SelectedCategories,
                filter.SelectedKeywords,
                filter.SelectedKnowledgeStatuses);

            headerItems.Text = string.Format("Items: count ({0})", result.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// Handles the Click event of the buttonViewKnowledge control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonViewKnowledge_Click(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToKnowledgeInfoPage(gridKnowledges.GetIntDataKey((Control)sender));
    }

    /// <summary>
    /// Handles the Click event of the buttonEditKnowledge control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonEditKnowledge_Click(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToKnowledgeEditPage(gridKnowledges.GetIntDataKey((Control)sender));
    }
}
