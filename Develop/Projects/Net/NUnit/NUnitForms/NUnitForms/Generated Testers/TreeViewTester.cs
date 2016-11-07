using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TreeViewTester : ControlTester<TreeView, TreeViewTester>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewTester"/> class.
        /// </summary>
        public TreeViewTester()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewTester"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="form">The form.</param>
        public TreeViewTester(string name, Form form) : base(name, form)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewTester"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="formName">Name of the form.</param>
        public TreeViewTester(string name, string formName) : base(name, formName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewTester"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TreeViewTester(string name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewTester"/> class.
        /// </summary>
        /// <param name="tester">The tester.</param>
        /// <param name="index">The index.</param>
        public TreeViewTester(TreeViewTester tester, int index) : base(tester, index)
        {
        }

        /// <summary>
        /// Selects a node in the tree.
        /// </summary>
        /// <param name="indices">an array of the indexes of the node to select</param>
        public void SelectNode(params int[] indices)
        {
            TreeNode currentNode = null;
            foreach (int index in indices)
            {
                if (currentNode == null)
                {
                    currentNode = Properties.Nodes[index];
                }
                else
                {
                    currentNode = currentNode.Nodes[index];
                }
            }
            Properties.SelectedNode = currentNode;
        }
    }
}