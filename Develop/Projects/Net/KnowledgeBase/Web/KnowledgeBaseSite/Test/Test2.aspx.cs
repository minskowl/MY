using System;
using System.Linq;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.SiteCore.Providers;

public partial class Test_Test2 : System.Web.UI.Page
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        textBoxSummary.DocumentManager.ContentProviderTypeName = typeof(StorageContentProvider).AssemblyQualifiedName;

        textBoxSummary.DocumentManager.ViewPaths = KbContext.CurrentKb.Storages.Select(storage => storage.Path).ToArray();

    }
}
