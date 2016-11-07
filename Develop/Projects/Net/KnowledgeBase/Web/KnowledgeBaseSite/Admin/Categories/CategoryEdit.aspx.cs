using System;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.SiteCore;
using Savchin.Validation;

public partial class CategoryEdit : SitePage
{
    private int? id;
    private int? parentId;
    CategoryManager manager = KbContext.CurrentKb.ManagerCategory;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        id = RequestIntId;
        parentId = GetRequestInt(RedirectorAdmin.keyParentId);


        InitializeValidators(typeof(Category));

        if (IsPostBack)
            return;
        if (Identifier.IsValid(id))
        {
            if (!KbContext.CurrentKb.HasPermission(id.Value, Permission.Admin))
            {
                GoToPreviousPage();
                return;
            }
            ShowCategory();
        }
        else
        {
            if (!KbContext.CurrentKb.HasPermission(parentId ?? 0, Permission.Admin))
            {
                GoToPreviousPage();
                return;
            }
        }
    }
    private void ShowCategory()
    {
        Category category = manager.GetByID(id.Value);
        if (category == null)
        {
            GoToPreviousPage();
            return;
        }
        header.Text = "Category Edit";
        textBoxName.Text = category.Name;
    }

    /// <summary>
    /// Handles the Click event of the buttonSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSave_Click(object sender, EventArgs e)
    {
        Category category;
        if (Identifier.IsValid(id))
        {
            category = manager.GetByID(id.Value);
            if (category == null)
            {
                GoToPreviousPage();
                return;
            }
        }
        else
        {
            category = new Category();
            if (Identifier.IsValid(parentId))
                category.ParentCategoryID = parentId;
        }

        category.Name = textBoxName.Text.Trim();

        try
        {
            manager.Save(category);
            GoToPreviousPage();
        }
        catch (ValidationException ex)
        {
            ShowException(ex);
        }

    }

    /// <summary>
    /// Handles the Click event of the buttonCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        GoToPreviousPage();
    }
}
