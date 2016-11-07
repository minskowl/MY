using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.SiteCore;
using Savchin.Web.UI;

public partial class CategoryInfo : SitePage
{

    readonly KnowledgeManager _knowledgeManager = KbContext.CurrentKb.ManagerKnowledge;

    private Category _category;
    private int _categoryId;
    private Permission _currentPermissions;
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        int? id = RequestIntId;
        if (id.HasValue)
            _categoryId = id.Value;
        _currentPermissions = KbContext.CurrentKb.GetPermissionsForCategory(_categoryId);

        if (_currentPermissions < Permission.View)
        {
            GoToPreviousPage();
            return;
        }
        buttonAdd.Visible = _currentPermissions >= Permission.Admin;
        buttonAddItem.Visible = _currentPermissions >= Permission.Publish;

        buttonWindowUrlClose.OnClientClick = windowUrl.JScriptHide;

        if (IsPostBack)
            return;

        if (Identifier.IsValid(_categoryId))
        {
            _categoryId = id.Value;
            _category = KbContext.CurrentKb.ManagerCategory.GetByID(_categoryId);
            ShowCategory();
        }
        else
            ShowRootCategory();


    }

    private void ShowCategory()
    {
        SetTitle(_category.CategoryID, _category.Name);
        buttonDelete.Visible = true;
        buttonDelete.Text = "Delete " + _category.Name + " Category";
        buttonUp.NavigateUrl = RedirectorAdmin.GetCategoryInfoPageUrl(_category.ParentCategoryID ?? 0);
        buttonMove.NavigateUrl = RedirectorAdmin.GetCategoryMovePageUrl(_category.CategoryID);
    }

    private void ShowRootCategory()
    {
        SetTitle(0, "Root");
        buttonUp.Visible = false;
        buttonMove.Visible = false;
        panelItems.Visible = false;
        buttonDelete.Visible = false;
    }
    private void SetTitle(int id, string name)
    {
        HeaderPage1.Text = string.Format("{2} Category #{0}: {1}", id, name, _currentPermissions);
    }

    /// <summary>
    /// Handles the OnClick event of the buttonAddKnowledge control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonAddKnowledge_OnClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToKnowledgeAddPage(_categoryId);
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <returns></returns>
    public DataView GetData()
    {
        return KbContext.CurrentKb.ManagerCategory.GetShortInfoByParentCategoryID(_categoryId);
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

    #region Button Handlers

    #region Categories
    /// <summary>
    /// Handles the Click event of the ViewButton1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonViewCategory_Click(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToCategoryInfoPage(gridSubCategories.GetIntDataKey((Control)sender));

    }

    /// <summary>
    /// Handles the Click event of the buttonAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonAddClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToCategoryAddPage(_categoryId);
    }

    /// <summary>
    /// Handles the Click event of the buttonEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonEdit_Click(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToCategoryEditPage(gridSubCategories.GetIntDataKey((Control)sender));
    }
    /// <summary>
    /// Handles the Click event of the buttonDelete control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        _category = KbContext.CurrentKb.ManagerCategory.GetByID(_categoryId);
        if (_category == null)
        {
            RedirectorAdmin.GoToRootCategoryInfoPage();
            return;
        }
        KbContext.CurrentKb.ManagerCategory.Delete(_category);
        RedirectorAdmin.GoToCategoryInfoPage(_category.ParentCategoryID ?? 0);
    }
    #endregion

    #region Knowledges
    /// <summary>
    /// Handles the Click event of the buttonAddItem control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonAddItem_Click(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToKnowledgeAddPage(_categoryId);
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

    /// <summary>
    /// Handles the Click event of the buttonDeleteKnowledge control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonDeleteKnowledge_Click(object sender, EventArgs e)
    {
        Knowledge knowledge = KbContext.CurrentKb.ManagerKnowledge.GetByID(gridKnowledges.GetIntDataKey((Control)sender));
        if (knowledge != null)
            KbContext.CurrentKb.ManagerKnowledge.Delete(knowledge);

        gridKnowledges.DataBind();
    }
    #endregion

    #endregion

    /// <summary>
    /// Handles the DataBinding event of the buttonEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonEdit_DataBinding(object sender, EventArgs e)
    {
        var button = (ButtonEx)sender;
        int categoryId = gridSubCategories.GetIntDataKey(button);
        button.Visible = KbContext.CurrentKb.HasPermission(categoryId, Permission.Admin);
    }

    protected void buttonViewCategory_DataBinding(object sender, EventArgs e)
    {
        var button = (ButtonEx)sender;
        int categoryId = gridSubCategories.GetIntDataKey(button);
        button.Visible = KbContext.CurrentKb.HasPermission(categoryId, Permission.View);
    }


}
