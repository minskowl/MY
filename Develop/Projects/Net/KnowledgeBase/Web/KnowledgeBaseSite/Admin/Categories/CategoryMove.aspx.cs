using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;

public partial class CategoryMove : SitePage
{
    readonly CategoryManager manager = KbContext.CurrentKb.ManagerCategory;
    private int categoryID;
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        int? id = RequestIntId;
        if (id.HasValue)
            categoryID = id.Value;
        else
        {
            GoToPreviousPage();
            return;
        }
        Category category = manager.GetByID(categoryID);
        if (category == null)
        {
            GoToPreviousPage();
            return;
        }

        if (IsPostBack)
            return;
        headerPage1.Text += category.Name;


    }

    protected void buttonOk_Click(object sender, EventArgs e)
    {
        Category category = manager.GetByID(categoryID);
        if(category==null)
        {
            GoToPreviousPage();
            return;
        }

        TreeNode node = listCategories.SelectedNode;
        if (node == null)
        {
            ShowAlert("Please select category to move.");
            return;
        }

        int moveCategoryId = int.Parse(node.Value);

        if (manager.CheckIsParent(categoryID, moveCategoryId))
        {
            ShowAlert("You are select invalid directory.");
            return;
        }
        category.ParentCategoryID = (moveCategoryId == 0) ? (int?)null : moveCategoryId;
        manager.Save(category);
        GoToPreviousPage();
        
    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        GoToPreviousPage();
    }

    protected void listCategories_TreeNodeCreate(object sender, TreeNodeEventArgs e)
    {
        e.Node.SelectAction = TreeNodeSelectAction.SelectExpand;
    }
}
