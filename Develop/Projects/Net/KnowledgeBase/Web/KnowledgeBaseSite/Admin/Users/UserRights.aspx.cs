using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Controls;
using KnowledgeBase.DAL;

public partial class Users_UserRights : SitePage
{
    private int _userId;
    readonly UserManager _manager = KbContext.CurrentKb.ManagerUser;
    readonly UserRightManager _rightManager = KbContext.CurrentKb.ManageUserRight;

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

        int? id = RequestIntId;
        if ((id ?? -1) < 0)
        {
            GoToPreviousPage();
            return;
        }

        _userId = id.Value;

        if (IsPostBack)
            return;

        User user = _manager.GetByID(_userId);
        if (user == null)
        {
            GoToPreviousPage();
            return;
        }

        header.Text = "Assign rights  to " + user.Login;
        ShowRights(user, _rightManager.GetByUserID(_userId));
    }

    private void ShowRights(User user, IEnumerable<UserRight> rights)
    {
        var trees = new Dictionary<Permission, TreeViewCategory>
                        {
                            {Permission.View, treeView},
                            {Permission.Publish, treePublish},
                            {Permission.Moderate, treeModerate},
                            {Permission.Admin, treeAdmin}
                        };
        if (user.RootPermission.HasValue)
            trees[user.RootPermission.Value].GetNodeByValue("0").Checked = true;

        foreach (var right in rights)
        {
            trees[right.Permission].GetNodeByValue(right.CategoryID.ToString()).Checked = true;
        }
        foreach (var value in trees.Values)
        {
            value.ExpandAll();
        }
    }


    /// <summary>
    /// Handles the Click event of the buttonSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSave_Click(object sender, EventArgs e)
    {
        User user = _manager.GetByID(_userId);
        if (user == null)
        {
            GoToPreviousPage();
            return;
        }

        var temp = GetPermissions();

        if (temp.ContainsKey(0))
        {
            user.RootPermission = temp[0];
            _manager.Save(user);
            temp.Remove(0);
        }
        else
        {
            if (user.RootPermission.HasValue)
            {
                user.RootPermission = null;
                _manager.Save(user);
            }
        }

        var result = temp.Keys.Select(categoryId => new CategoryPermission(categoryId, (short) temp[categoryId])).ToArray();

        _rightManager.SaveRights(_userId, result);

        GoToPreviousPage();
    }

    /// <summary>
    /// Gets the permissions.
    /// </summary>
    /// <returns></returns>
    Dictionary<int, Permission> GetPermissions()
    {


        var result = new Dictionary<int, Permission>();

        FillRights(result, treeView, Permission.View);
        FillRights(result, treePublish, Permission.Publish);
        FillRights(result, treeModerate, Permission.Moderate);
        FillRights(result, treeAdmin, Permission.Admin);

        return result;
    }
    private void FillRights(Dictionary<int, Permission> storage, TreeViewCategory tree, Permission permission)
    {
        foreach (var categoryId in tree.GetCheckedNodes().Select(node => int.Parse(node.Value)))
        {
            storage[categoryId] = permission;
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
