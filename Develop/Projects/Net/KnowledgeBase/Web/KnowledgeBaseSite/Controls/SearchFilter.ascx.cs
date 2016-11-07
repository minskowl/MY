using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;

public partial class Controls_SearchFilter : System.Web.UI.UserControl
{
    /// <summary>
    /// Gets the selected knowledge types.
    /// </summary>
    /// <value>The selected knowledge types.</value>
    public List<KnowledgeType> SelectedKnowledgeTypes
    {
        get { return listTypess.SelectedValues; }
        set { listTypess.SelectedValues = value; }
    }

    public List<KnowledgeStatus> SelectedKnowledgeStatuses
    {
        get { return listStatuses.SelectedValues; }
        set { listStatuses.SelectedValues = value; }
    }

    /// <summary>
    /// Gets the selected categories.
    /// </summary>
    /// <value>The selected categories.</value>
    public List<int> SelectedCategories
    {
        get
        {
            List<int> result = new List<int>();
            GetSelection(result, listCaregories.Nodes);
            return (result.Count == 1 && result[0] == 0) ? null : result;
        }
    }
    /// <summary>
    /// Gets the selected keywords.
    /// </summary>
    /// <value>The selected keywords.</value>
    public IList<int> SelectedKeywords
    {
        get
        {
            return listKeywords.KeywordsID;
        }
    }
    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    /// <value>The text.</value>
    public string Text
    {
        get { return textBoxText.Text.Trim(); }
        set { textBoxText.Text = value; }
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        listCaregories.Nodes[0].Checked = true;
    }

    private void GetSelection(List<int> result, TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Checked)
            {
                result.Add(int.Parse(node.Value));
            }
            else
            {
                GetSelection(result, node.ChildNodes);
            }
        }
    }
}
