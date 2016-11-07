using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.SiteCore;

public partial class Test_Default : SitePage
{  
    private int categoryID = 0;

    public enum ViewMode
    {
        Browse,
        Search
    }
    ViewMode Mode
    {
        get
        {
            object o = ViewState["Mode"];
            return o == null ? ViewMode.Browse : (ViewMode)o;
        }
        set
        {
            ViewState["Mode"] = value;
            switch (value)
            {
                case ViewMode.Browse:
                    
                    categoryInfoView.Visible = true;
                    searchResultsView.Visible = false;
                    categoryInfoView.CategoryID = categoryID;

                    break;
                case ViewMode.Search:
                    categoryInfoView.Visible = false;
                    searchResultsView.Visible = true;

                    break;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        buttonLogin.NavigateUrl = RedirectorAdmin.AdminDefaultPage;
        searchResultsView.Filter = filter;

        Int32.TryParse(categoryIDField.Value, out categoryID);

        if (categoryID > 0)
        {
            Mode = ViewMode.Browse;

        }



    }


    /// <summary>
    /// Handles the TreeNodeCreate event of the listCategories control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.TreeNodeEventArgs"/> instance containing the event data.</param>
    protected void listCategories_TreeNodeCreate(object sender, TreeNodeEventArgs e)
    {
        e.Node.NavigateUrl = "javascript: showCategory('" + e.Node.Value + "');";
    }





    /// <summary>
    /// Handles the Click event of the buttonSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        Mode = ViewMode.Search;
    }
}
