using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    public class TreeViewEx : System.Web.UI.WebControls.TreeView
    {
        public delegate bool NodeMatch(TreeNode node);

        /// <summary>
        /// Gets the first checked node.
        /// </summary>
        /// <value>The first checked node.</value>
        public TreeNode FirstCheckedNode
        {
            get
            {
                return GetFirstCheckedNode(Nodes);
            }
        }
        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public TreeNode GetNode(string value)
        {
            return GetNode(Nodes, value);
        }

        #region Searchers
        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode GetNode(TreeNodeCollection nodes, NodeMatch matcher)
        {
            foreach (TreeNode node in nodes)
            {
                if (matcher(node))
                    return node;
                TreeNode findNode = GetNode(node.ChildNodes, matcher);
                if (findNode != null)
                    return findNode;
            }
            return null;
        }
        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="nodes">The nodes.</param>
        /// <param name="matcher">The matcher.</param>
        public void GetNodes(List<TreeNode> result, TreeNodeCollection nodes, NodeMatch matcher)
        {
            foreach (TreeNode node in nodes)
            {
                if (matcher(node))
                    result.Add(node);
                GetNodes(result,node.ChildNodes, matcher);
     
            }
        }
        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public List<TreeNode>  GetNodes( TreeNodeCollection nodes, NodeMatch matcher)
        {
            List<TreeNode> result= new List<TreeNode>();
            GetNodes(result, nodes, matcher);
            return result;
        }
        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public List<TreeNode> GetNodes( NodeMatch matcher)
        {
            return GetNodes(Nodes, matcher);
        }

        /// <summary>
        /// Gets the checked nodes.
        /// </summary>
        /// <returns></returns>
        public List<TreeNode> GetCheckedNodes()
        {
            return GetNodes(Nodes, CheckedMatcher);
        }

        /// <summary>
        /// Gets the first checked node.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public TreeNode GetFirstCheckedNode(TreeNodeCollection nodes)
        {
            return GetNode(nodes, CheckedMatcher);
        }
        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public TreeNode GetNode(TreeNodeCollection nodes, string value)
        {
            return GetNode(nodes, new ValueMatcher(value).Match);
        }
        /// <summary>
        /// Gets the node by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public TreeNode GetNodeByValue( string value)
        {
            return GetNode(Nodes, new ValueMatcher(value).Match);
        }
        #endregion

        /// <summary>
        /// Nodes the set checked.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="checkedValue">if set to <c>true</c> [checked value].</param>
        /// <returns></returns>
        public TreeNode NodeSetChecked(string value, bool checkedValue)
        {
            TreeNode node = GetNode(value);
            if (node == null)
                return null;
            node.Checked = checkedValue;
            return node;
        }


        #region Matchers
        private bool CheckedMatcher(TreeNode node)
        {
            return node.Checked;
        }

        private class ValueMatcher
        {
            private string searchValue;
            public ValueMatcher(string filter)
            {
                searchValue = filter;
            }
            public bool Match(TreeNode node)
            {
                return node.Value == searchValue;
            }
        }
        #endregion
    }

}
