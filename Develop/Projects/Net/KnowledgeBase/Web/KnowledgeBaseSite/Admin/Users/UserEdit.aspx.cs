using System;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using Savchin.Validation;

public partial class Users_UserEdit : SitePage
{
    private int _userId;
    readonly UserManager _manager = KbContext.CurrentKb.ManagerUser;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        int? id = RequestIntId;
        if (!Identifier.IsValid(id))
            return;

        _userId = id.Value;

        if (_userId != KbContext.CurrentUserId && !KbContext.CurrentKb.HasUserAdminPermission)
        {
            GoToPreviousPage();
        }

        InitializeValidators(typeof(User));

        if (IsPostBack)
            return;

        var user = _manager.GetByID(_userId);
        if (user == null)
        {
            GoToPreviousPage();
            return;
        }
        ShowUser(user);
    }

    /// <summary>
    /// Shows the user.
    /// </summary>
    /// <param name="user">The user.</param>
    private void ShowUser(User user)
    {
        header.Text = "Edit User";
        Show(user);
    }

    /// <summary>
    /// Handles the Click event of the buttonSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonSaveClick(object sender, EventArgs e)
    {
        User user;
        if (Identifier.IsValid(_userId))
        {
            user = _manager.GetByID(_userId);
            if (user == null)
            {
                GoToPreviousPage();
                return;
            }
        }
        else
        {
            user = new User();
        }

        try
        {
            FillUser(user);
            _manager.Save(user);
            GoToPreviousPage();
        }
        catch (ValidationException ex)
        {
            ShowException(ex);
        }
    }

    private void FillUser(User user)
    {
        Fill(user);
        if(!string.IsNullOrEmpty(textBoxPassword.Text))
        {
            if(textBoxPassword.Text!=textBoxConfirmPassword.Text)
            {
                throw new ValidationException("Please confirm password correct.","Password");
            }
            user.Password = textBoxPassword.Text;
        }


       
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
