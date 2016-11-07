
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using Savchin.Core;


namespace Savchin.Forms.Browsers
{
    public partial class RegistryBrowser : UserControl
    {
        public const int ImageIndexFolder = 1;

        public event TreeViewEventHandler AfterSelect;
        public event RegistryEventHandler DeleteValue;
        public event RegistryEventHandler DeleteKey;
        public event RegistryEventHandler DeleteRegistryEntry;

        #region Properties

        private bool showRootNode;
        /// <summary>
        /// Gets or sets a value indicating whether [show root node].
        /// </summary>
        /// <value><c>true</c> if [show root node]; otherwise, <c>false</c>.</value>
        public bool ShowRootNode
        {
            get { return showRootNode; }
            set
            {
                if (showRootNode != value)
                {
                    showRootNode = value;
                    RefreshBrowser();
                }
            }
        }

        private bool conextMenuVisible = true;
        /// <summary>
        /// Gets or sets a value indicating whether [conext menu visible].
        /// </summary>
        /// <value><c>true</c> if [conext menu visible]; otherwise, <c>false</c>.</value>
        public bool ConextMenuVisible
        {
            get { return conextMenuVisible; }
            set
            {
                conextMenuVisible = value;
                if (value)
                    tvObj.ContextMenuStrip = contextMenuStrip1;
                else
                    tvObj.ContextMenuStrip = null;

            }
        }

        private RegistryKey rootKey;
        /// <summary>
        /// Gets or sets the root key.
        /// </summary>
        /// <value>The root key.</value>
        public RegistryKey RootKey
        {
            get { return rootKey; }
            set
            {
                rootKey = value;
                if (value != null)
                    RefreshBrowser();
            }
        }
        /// <summary>
        /// Gets the selected node.
        /// </summary>
        /// <value>The selected node.</value>
        public TreeNode SelectedNode
        {
            get { return tvObj.SelectedNode; }
        }

        /// <summary>
        /// Gets the selected kew.
        /// </summary>
        /// <value>The selected kew.</value>
        public RegistryKey SelectedKey
        {
            get
            {
                if (tvObj.SelectedNode == null || tvObj.SelectedNode.ImageIndex != ImageIndexFolder)
                    return null;
                return (RegistryKey)tvObj.SelectedNode.Tag;
            }
        }



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryBrowser"/> class.
        /// </summary>
        public RegistryBrowser()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Refreshes the browser.
        /// </summary>
        public void RefreshBrowser()
        {
            tvObj.Nodes.Clear();
            if (rootKey == null)
                return;

            if (showRootNode)
                CreateKeyNode(tvObj.Nodes, rootKey);
            else
                CreateNodesForKey(tvObj.Nodes, rootKey);

        }




        #region Nodes
        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="key">The key.</param>
        protected TreeNode CreateKeyNode(TreeNodeCollection nodes, RegistryKey key)
        {
            TreeNode node = CreateKeyNode(nodes, key.Name);
            node.Tag = key;
            return node;
        }

        /// <summary>
        /// Creates the key node.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private TreeNode CreateKeyNode(TreeNodeCollection nodes, string name)
        {
            string nodeName = name.Substring(name.LastIndexOf("\\") + 1);
            TreeNode node = new TreeNode(nodeName);
            node.Name = name;

            node.ImageIndex = ImageIndexFolder;
            node.SelectedImageIndex = ImageIndexFolder;
            nodes.Add(node);
            AddLoadNode(node.Nodes);

            return node;
        }

        /// <summary>
        /// Creates the nodes for key.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="key">The key.</param>
        private void CreateNodesForKey(TreeNodeCollection nodes, RegistryKey key)
        {
            foreach (string keyName in key.GetSubKeyNames())
            {
                RegistryKey subKey = key.OpenSubKey(keyName);
                CreateKeyNode(nodes, subKey);
            }

            foreach (string valueName in key.GetValueNames())
            {
                nodes.Add(valueName, valueName);
            }
        }
        /// <summary>
        /// Adds the load node.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        private void AddLoadNode(TreeNodeCollection nodes)
        {
            nodes.Clear();
            nodes.Add("#");
        }

        #endregion

        #region Tree Events

        /// <summary>
        /// Handles the BeforeExpand event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void tvObj_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.FirstNode == null)
            {
                return;
            }
            if (node.FirstNode.Text != "#")
            {
                return;
            }
            node.Nodes.Clear();
            RegistryKey key = (RegistryKey)node.Tag;
            CreateNodesForKey(node.Nodes, key);
        }


        /// <summary>
        /// Handles the DoubleClick event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tvObj_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedNode == null) return;

            if (SelectedKey == null)
            {
                TreeNode node = SelectedNode;
                RegistryKey key = (RegistryKey)node.Parent.Tag;

                switch (key.GetValueKind(node.Name))
                {
                    case RegistryValueKind.String:
                        RegistryStringValueForm.EditStringValue(key, node.Name);
                        break;
                    case RegistryValueKind.ExpandString:
                        break;
                    case RegistryValueKind.Binary:
                        break;
                    case RegistryValueKind.DWord:
                        RegistryStringValueForm.EditDWORDValue(key, node.Name);
                        break;
                    case RegistryValueKind.MultiString:
                        break;
                    case RegistryValueKind.QWord:
                        break;
                    case RegistryValueKind.Unknown:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


            }
        }


        /// <summary>
        /// Handles the AfterLabelEdit event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.NodeLabelEditEventArgs"/> instance containing the event data.</param>
        private void tvObj_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            RegistryKey key;
            TreeNode node = e.Node;
            if (node.ImageIndex == ImageIndexFolder)
            {

                if (node.Tag == null)//Create new key
                {
                    string name = node.Parent.Name + "\\" + node.Text;

                    RegistryKey newKey = RegistryHelper.CreateKey(name);
                    newKey.Flush();


                    node.Tag = newKey;
                }
                else
                {
                    if (string.IsNullOrEmpty(e.Label))
                        return;
                    key = (RegistryKey)node.Tag;
                    string newKeyName = key.Name.Substring(0, key.Name.LastIndexOf('\\') + 1) + e.Label;
                    node.Tag = RegistryHelper.RenameKey(key,newKeyName);
                    node.Name = e.Label;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(e.Label))
                    return;

                key = (RegistryKey)node.Parent.Tag;
                RegistryHelper.RenameValue(key, node.Text, e.Label);
                node.Name = e.Label;
            }
        }
        /// <summary>
        /// Handles the AfterSelect event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvObj_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnAfterSelect(e);
        }
        #endregion

        #region Menu
        /// <summary>
        /// Handles the Click event of the stringValueToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void stringValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedKey == null || SelectedNode == null)
                return;
            TreeNode node = SelectedNode;
            RegistryKey key = SelectedKey;
            string name = RegistryHelper.GetNewValueName(key);

            if (!node.IsExpanded) node.Expand();

            Registry.SetValue(key.Name, name, String.Empty, RegistryValueKind.String);

            TreeNode nodeNew = node.Nodes.Add(name);
            nodeNew.Name = name;

            nodeNew.BeginEdit();

        }



        /// <summary>
        /// Handles the Click event of the deleteToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey key = SelectedKey;
            if (MessageBox.Show("Are you sure you want to delete this key and all of its subkeys?",
                                "Delete confirmation",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return;

            if (key != null)
            {
                RegistryHelper.DeleteKey(key);
                SelectedNode.Remove();

                OnDeleteKey(new RegistryEventArgs(key.Name, null, null));
                OnDeleteRegistryEntry(new RegistryEventArgs(key.Name, null, null));
            }
        }
        /// <summary>
        /// Handles the Click event of the renameToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                SelectedNode.BeginEdit();
        }

        /// <summary>
        /// Handles the Click event of the kewToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void kewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey key = SelectedKey ?? rootKey;
            TreeNode node = SelectedNode;
            if (key == null && node == null)
                return;

            string newName = RegistryHelper.GetNewKeyName(key);

            TreeNode nodeNew = CreateKeyNode((node == null) ? tvObj.Nodes : node.Nodes,
                                             RegistryHelper.GetShortName(key) + "\\" + newName);
            if (node != null) node.Expand();
            nodeNew.BeginEdit();


        }
        #endregion

        #region Events
        /// <summary>
        /// Raises the <see cref="E:DeleteRegistryEntry"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Savchin.Forms.Browsers.RegistryEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDeleteRegistryEntry(RegistryEventArgs e)
        {
            if (DeleteRegistryEntry != null)
                DeleteRegistryEntry(this, e);
        }
        /// <summary>
        /// Raises the <see cref="E:DeleteKey"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Savchin.Forms.Browsers.RegistryEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDeleteKey(RegistryEventArgs e)
        {
            if (DeleteKey != null)
                DeleteKey(this, e);
        }
        /// <summary>
        /// Raises the <see cref="E:DeleteValue"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Savchin.Forms.Browsers.RegistryEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDeleteValue(RegistryEventArgs e)
        {
            if (DeleteValue != null)
                DeleteValue(this, e);
        }
        /// <summary>
        /// Raises the <see cref="E:AfterSelect"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterSelect(TreeViewEventArgs e)
        {
            if (AfterSelect != null)
                AfterSelect(this, e);
        }

        #endregion




    }
}