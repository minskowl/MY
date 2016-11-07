using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;

public partial class Controls_CategoryInfoView : System.Web.UI.UserControl
{
    private int _categoryId;
    private readonly CategoryManager _categoryManager = KbContext.CurrentKb.ManagerCategory;
    private readonly KnowledgeManager _knowledgeManager = KbContext.CurrentKb.ManagerKnowledge;

    /// <summary>
    /// Gets or sets the category ID.
    /// </summary>
    /// <value>The category ID.</value>
    public int CategoryID
    {
        get { return _categoryId; }
        set { _categoryId = value; }
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <returns></returns>
    public DataView GetData()
    {
        return _categoryManager.GetShortInfoByParentCategoryID(_categoryId);
    }

    /// <summary>
    /// Gets the items data.
    /// </summary>
    /// <returns></returns>
    public DataView GetItemsData()
    {
        if (!Identifier.IsValid(_categoryId)) return null;

        var data = _knowledgeManager.GetShortInfoByCategoryID(_categoryId);
        _knowledgeManager.RemoveNotViewable(data);
        return data.DefaultView;
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (_categoryId > 0)
        {
            Category category = KbContext.CurrentKb.ManagerCategory.GetByID(_categoryId);
            if (category == null)
                _categoryId = 0;
            else
            {
                header.Text = "Category: " + category.Name;
            }
        }
    }
}
