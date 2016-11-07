using System;
using System.Data;
using KnowledgeBase.BussinesLayer.Core;

public partial class Controls_SearchResultsView : System.Web.UI.UserControl
{
    private Controls_SearchFilter filter;

    /// <summary>
    /// Gets or sets the filter.
    /// </summary>
    /// <value>The filter.</value>
    public Controls_SearchFilter Filter
    {
        get { return filter; }
        set { filter = value; }
    }

    /// <summary>
    /// Searches the items data.
    /// </summary>
    /// <returns></returns>
    public DataTable SearchItemsData()
    {
        return KbContext.CurrentKb.ManagerKnowledge.Search(
                filter.Text,
                filter.SelectedKnowledgeTypes,
                filter.SelectedCategories,
                filter.SelectedKeywords,
                filter.SelectedKnowledgeStatuses);
    }
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
