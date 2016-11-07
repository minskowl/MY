using System;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using Savchin.Validation;

public partial class Keywords_KeywordEdit : SitePage
{
    private int _keywordId;
    private readonly KeywordManager _manager = KbContext.CurrentKb.ManagerKeyword;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!KbContext.CurrentKb.HasUserAdminPermission)
        {
            GoToPreviousPage();
            return;
        }

        InitializeValidators(typeof(Keyword));
        int? id = RequestIntId;
        if (!Identifier.IsValid(id))
            return;
        _keywordId = id.Value;

        if (IsPostBack)
            return;
        var keyword = _manager.GetByID(_keywordId);
        if (keyword == null)
        {
            GoToPreviousPage();
            return;
        }
        ShowKeyword(keyword);
    }

    private void ShowKeyword(Keyword keyword)
    {
        header.Text = "Edit Keyword";
        Show(keyword);
        listStatus.SelectedKeywordStatus = keyword.KeywordStatus;
        listType.SelectedKeywordType = keyword.KeywordType;
    }

    /// <summary>
    /// Handles the Click event of the buttonSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSave_Click(object sender, EventArgs e)
    {
        Keyword entity;
        if (Identifier.IsValid(_keywordId))
        {
            entity = _manager.GetByID(_keywordId);
            if (entity == null)
            {
                GoToPreviousPage();
                return;
            }
        }
        else
        {
            entity = new Keyword();
        }

        try
        {
            FillKeyword(entity);
            _manager.Save(entity);
            GoToPreviousPage();
        }
        catch (ValidationException ex)
        {
            ShowException(ex);
        }
    }

    private void FillKeyword(Keyword entity)
    {
        Fill(entity);
        entity.KeywordType = listType.SelectedKeywordType;
        entity.KeywordStatus = listStatus.SelectedKeywordStatus;
    }

    /// <summary>
    /// Handles the Click event of the buttonCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonCancelClick(object sender, EventArgs e)
    {
        GoToPreviousPage();
    }
}
