using System;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.SiteCore;

public partial class KnowledgeInfo : SitePage
{
    KnowledgeManager manager = KbContext.CurrentKb.ManagerKnowledge;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
            return;
        if (!Identifier.IsValid(RequestIntId))
        {
            RedirectorAdmin.GoToDefaultPage();
            return;
        }
        Knowledge knowledge = manager.GetByID(RequestIntId.Value);
        if (knowledge == null)
        {
            RedirectorAdmin.GoToDefaultPage();
            return;
        }

        if (!knowledge.CanView)
        {
            GoToPreviousPage();
            return;
        }
        buttonEdit.Visible = knowledge.CanEdit;
        viewKnowledge.Knowledge = knowledge;

    }

    /// <summary>
    /// Handles the OnClick event of the buttonBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonBack_OnClick(object sender, EventArgs e)
    {
        GoToPreviousPage();
    }

    /// <summary>
    /// Handles the OnClick event of the buttonEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonEdit_OnClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToKnowledgeEditPage(RequestIntId.Value);
    }
}
