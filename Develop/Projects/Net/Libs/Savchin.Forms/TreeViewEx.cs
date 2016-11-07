using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Savchin.Forms
{
    /// <summary>
    /// TreeView Extender
    /// </summary>
    public class TreeViewEx : TreeView
    {
        /// <summary>
        /// Gets the first checked node.
        /// </summary>
        /// <value>The first checked node.</value>
        public TreeNode FirstCheckedNode
        {
            get
            {
                return FindFirstCheckedNode(Nodes);
            }
        }

        #region Getters

        /// <summary>
        /// Gets the last node.
        /// </summary>
        /// <returns></returns>
        public TreeNode GetLastNode()
        {
            return GetLastNode(Nodes);
        }

        /// <summary>
        /// Gets the last node.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public TreeNode GetLastNode(TreeNodeCollection nodes)
        {


            if (nodes.Count == 0)
                return null;
            TreeNode node = nodes[nodes.Count - 1];

            TreeNode searchingNode = GetLastNode(node.Nodes);

            if (searchingNode != null)
                return searchingNode;

            return node;

        }

        /// <summary>
        /// Gets all checked nodes.
        /// </summary>
        /// <returns></returns>
        public List<TreeNode> GetAllCheckedNodes()
        {
            return GetAllCheckedNodes(Nodes);
        }
        /// <summary>
        /// Gets all checked nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public List<TreeNode> GetAllCheckedNodes(TreeNodeCollection nodes)
        {
            var result = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    result.Add(node);
                result.AddRange(GetAllCheckedNodes(node.Nodes));
            }
            return result;
        }

        /// <summary>
        /// Gets the root node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public TreeNode GetRootNode(TreeNode node)
        {
            TreeNode rootNode = node;
            while (rootNode.Parent != null)
            {
                rootNode = rootNode.Parent;
            }
            return rootNode;
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        public TreeNode GetNode(string[] fullPath)
        {
            TreeNodeCollection nodes = Nodes;
            TreeNode node = null;
            foreach (string nodeName in fullPath)
            {
                node = nodes[nodeName];
                if (node == null)
                    return null;
                nodes = node.Nodes;
            }
            return node;
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public TreeNode GetNode(string fullPath, char separator)
        {
            return GetNode(fullPath.Split(new[] { separator }));
        }
        #endregion

        /// <summary>
        /// Searches the in nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="search">The search.</param>
        public void SearchInNodes(TreeNodeCollection nodes, String search)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text.ToLower() == search)
                {
                    SelectedNode = node;
                    Select();
                }
                SearchInNodes(node.Nodes, search);
            }
        }

        /// <summary>
        /// Searches the text.
        /// </summary>
        /// <param name="search">The search.</param>
        public void SearchText(String search)
        {
            SearchInNodes(Nodes, search.Trim().ToLower());
        }

        #region Find
        /// <summary>
        /// Finds the first checked node.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public TreeNode FindFirstCheckedNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
                if (node.Checked)
                    return node;


            foreach (TreeNode node in nodes)
            {
                TreeNode _find = FindFirstCheckedNode(node.Nodes);
                if (_find != null)
                    return _find;

            }
            return null;
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public List<TreeNode> FindAll(Predicate<TreeNode> matcher)
        {
            return FindAll(Nodes, matcher);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public List<TreeNode> FindAll(TreeNodeCollection nodes, Predicate<TreeNode> matcher)
        {
            var result = new List<TreeNode>();
            FindAll(result, nodes, matcher);
            return result;
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="nodes">The nodes.</param>
        /// <param name="matcher">The matcher.</param>
        public void FindAll(List<TreeNode> result, TreeNodeCollection nodes, Predicate<TreeNode> matcher)
        {
            foreach (TreeNode node in nodes)
            {
                if (matcher(node))
                    result.Add(node);
                FindAll(result, node.Nodes, matcher);
            }
        }


        /// <summary>
        /// Finds the next node by matcher.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode FindNext(Predicate<TreeNode> matcher)
        {
            if (SelectedNode != null)
                return FindNext(SelectedNode, matcher);
            if (Nodes.Count > 0)
                return FindNext(Nodes[0], matcher);

            return null;
        }

        /// <summary>
        /// Finds the next specified afterNode.
        /// </summary>
        /// <param name="afterNode">The node.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode FindNext(TreeNode afterNode, Predicate<TreeNode> matcher)
        {
            TreeNode checkNode;
            TreeNode serched = FindFirst(afterNode.Nodes, matcher);
            if (serched != null)
                return serched;

            for (checkNode = afterNode.NextNode; checkNode != null; checkNode = checkNode.NextNode)
            {
                if (matcher(checkNode))
                    return checkNode;

                serched = FindFirst(checkNode.Nodes, matcher);
                if (serched != null)
                    return serched;
            }
            for (checkNode = afterNode.Parent; checkNode != null; checkNode = checkNode.Parent)
            {
                if (checkNode.NextNode != null)
                {
                    serched = FindNext(checkNode.NextNode, matcher);
                    if (serched != null)
                        return serched;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the first.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode FindFirst(Predicate<TreeNode> matcher)
        {
            return FindFirst(Nodes, matcher);
        }

        /// <summary>
        /// Finds the specified nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode FindFirst(TreeNodeCollection nodes, Predicate<TreeNode> matcher)
        {
            foreach (TreeNode node in nodes)
            {
                if (matcher(node))
                    return node;

                TreeNode serched = FindFirst(node.Nodes, matcher);
                if (serched != null)
                    return serched;
            }
            return null;
        }

        /// <summary>
        /// Finds the reverse.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode FindReverse(Predicate<TreeNode> matcher)
        {
            if (SelectedNode != null)
                return FindReverse(SelectedNode, matcher);
            if (Nodes.Count > 0)
                return FindNext(GetLastNode(Nodes), matcher);

            return null;
        }
        /// <summary>
        /// Finds the reverse.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode FindReverse(TreeNodeCollection nodes, Predicate<TreeNode> matcher)
        {
            for (int i = nodes.Count - 1; i > 0; i--)
            {
                TreeNode checkNode = nodes[i];
                if (matcher(checkNode))
                    return checkNode;

                TreeNode serched = FindReverse(checkNode.Nodes, matcher);
                if (serched != null)
                    return serched;
            }
            return null;
        }

        /// <summary>
        /// Finds the reverse.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public TreeNode FindReverse(TreeNode node, Predicate<TreeNode> matcher)
        {
            if (node == null || matcher == null)
                return null;

            TreeNode checkNode;
            TreeNode serched;

            for (checkNode = node.PrevNode; checkNode != null; checkNode = checkNode.PrevNode)
            {
                if (matcher(checkNode))
                    return checkNode;

                serched = FindReverse(checkNode.Nodes, matcher);
                if (serched != null)
                    return serched;
            }
            checkNode = node.Parent;
            if (checkNode != null)
            {
                if (matcher(checkNode))
                    return checkNode;

                serched = FindReverse(checkNode, matcher);
                if (serched != null)
                    return serched;
            }

            return null;
        }
        #endregion

        #region Check
        /// <summary>
        /// Unchecks the nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        public void UncheckNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                UncheckNodes(node.Nodes);
            }

        }
        /// <summary>
        /// Checks the repeat on child.
        /// </summary>
        /// <param name="node">The node.</param>
        public void CheckRepeatOnChild(TreeNode node)
        {
            if (node.Checked)
                CheckNodes(node.Nodes);
            else
                UncheckNodes(node.Nodes);
        }
        /// <summary>
        /// Checks the nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        public void CheckNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true;
                CheckNodes(node.Nodes);
            }
        }
        /// <summary>
        /// Checks all nodes.
        /// </summary>
        public void CheckAllNodes()
        {
            CheckNodes(Nodes);
        }
        /// <summary>
        /// Checks the nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        public void CheckNodes(IEnumerable<TreeNode> nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true;
            }

        }
        /// <summary>
        /// Unchecks all nodes.
        /// </summary>
        public void UncheckAllNodes()
        {
            UncheckNodes(Nodes);
        }

        /// <summary>
        /// Checks the parent nodes.
        /// </summary>
        /// <param name="node">The node.</param>
        public void CheckParentNodes(TreeNode node)
        {
            if (node.Parent == null)
                return;

            if (node.Parent.Checked == false)
            {
                node.Parent.Checked = true;
            }
            CheckParentNodes(node.Parent);

        }
        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseClick"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            //Fix right click selection
            if (e.Button == MouseButtons.Right)
            {
                var node = GetNodeAt(e.X, e.Y);
                if (node != null)
                    SelectedNode = node;
            }
        }

    }
}
