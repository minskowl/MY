using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.SiteCore;
using Savchin.Web.UI;

public partial class Public_KnowledgeInfo : PageEx
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Guid? id = RequestGuidId;
        if (!id.HasValue)
        {
            Redirector.GoToPublicDefaultPage();
            return;
        }

        Knowledge knowledge = KbContext.CurrentKb.ManagerKnowledge.GetByPublicID(id.Value);
        if (knowledge == null)
        {
            Redirector.GoToPublicDefaultPage();
            return;
        }

        viewKnowledge.Knowledge = knowledge;
    }
}
