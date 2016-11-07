using System;
using Savchin.Validation;
using Site.Bl;
using Site.Core;
using Site.Cotrols;

public partial class Registration : SitePage
{
    private int userID;
    UserManager manager = new UserManager();

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

        userID = id.Value;


        InitializeValidators(typeof(User));

        if (IsPostBack)
            return;

        User user = manager.GetByID(userID);
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
    protected void buttonSave_Click(object sender, EventArgs e)
    {
        User user;
        if (Identifier.IsValid(userID))
        {
            user = manager.GetByID(userID);
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
            manager.Save(user);
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
    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        GoToPreviousPage();
    }
}
