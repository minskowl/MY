using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.Drawing;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class TreeViewCategory : TreeViewEx
    {
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(true)]
        public virtual bool LazyLoad
        {
            get
            {
                object obj1 = ViewState["LazyLoad"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }
            set { ViewState["LazyLoad"] = value; }
        }

        public event TreeNodeEventHandler TreeNodeCreate;

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (DesignMode)
                return;


            NodeIndent = 15;
            EnableClientScript = true;
            ShowLines = true;

            NodeStyle.Font.Size = FontUnit.Point(8);
            NodeStyle.ForeColor = Color.Black;
            NodeStyle.HorizontalPadding = Unit.Pixel(2);
            NodeStyle.NodeSpacing = Unit.Pixel(0);
            NodeStyle.VerticalPadding = Unit.Pixel(2);

            HoverNodeStyle.Font.Underline = true;
            HoverNodeStyle.ForeColor = ConverterColor.ToColor("#6666AA");

            Nodes.Add(CreateNode("Root", 0, true));


        }


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodePopulate"/> event of the <see cref="T:System.Web.UI.WebControls.TreeView"/> control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs"/> that contains event data.</param>
        protected override void OnTreeNodePopulate(TreeNodeEventArgs e)
        {

            base.OnTreeNodePopulate(e);

            int parentId = int.Parse(e.Node.Value);
            CreateChildNodes(e.Node.ChildNodes, parentId);
        }

        /// <summary>
        /// Raises the <see cref="E:CreateNode"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.TreeNodeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCreateNode(TreeNodeEventArgs e)
        {
            if (TreeNodeCreate != null)
                TreeNodeCreate(this, e);
        }


        /// <summary>
        /// Creates the node.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The id.</param>
        /// <param name="hasChild">if set to <c>true</c> [has child].</param>
        /// <returns></returns>
        private TreeNode CreateNode(string name, int id, bool hasChild)
        {
            TreeNode node = new TreeNode(name, id.ToString());
            node.Expanded = false;
            node.PopulateOnDemand = (LazyLoad && hasChild);
            node.ImageUrl = ImagePathProvider.SmallFolderImage;
            OnCreateNode(new TreeNodeEventArgs(node));

            if (!node.PopulateOnDemand && hasChild)
            {
                CreateChildNodes(node.ChildNodes, id);
            }
            return node;
        }
        /// <summary>
        /// Creates the child nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="parentId">The parent id.</param>
        private void CreateChildNodes(TreeNodeCollection nodes, int parentId)
        {
            var data = KbContext.CurrentKb.ManagerCategory.GetShortInfoByParentCategoryID(parentId);
            if (data != null)
                foreach (DataRowView row in data)
                {
                    nodes.Add(CreateNode((string)row["Name"],
                                         (int)row["CategoryID"],
                                         ((int)row["cntSubCategories"]) > 0));
                }
        }

    }
}
