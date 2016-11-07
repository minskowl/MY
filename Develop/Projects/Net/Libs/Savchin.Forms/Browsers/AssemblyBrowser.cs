using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Savchin.Forms.Browsers
{
    public partial class AssemblyBrowser : UserControl, IObjectBrowser
    {
        public event EventHandler AfterSelect;



        private Assembly _browsedAssembly;

        /// <summary>
        /// 
        /// </summary>
        public enum images : int
        {
            /// <summary>
            /// 
            /// </summary>
            IndEmpty = 0,
            /// <summary>
            /// 
            /// </summary>
            IndAssembly = 1,
            /// <summary>
            /// 
            /// </summary>
            IndNamespace = 2,
            /// <summary>
            /// 
            /// </summary>
            IndClass = 3,
            /// <summary>
            /// 
            /// </summary>
            IndMethod = 4,
            /// <summary>
            /// 
            /// </summary>
            IndProperty = 5
        }
        #region Properties
        /// <summary>
        /// Gets the browsed assembly.
        /// </summary>
        /// <value>The browsed assembly.</value>
        public Assembly BrowsedAssembly
        {
            get { return _browsedAssembly; }
        }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        public TreeNodeCollection Nodes
        {
            get { return tvObjects.Nodes; }
        }
        /// <summary>
        /// Gets the checked nodes.
        /// </summary>
        /// <value>The checked nodes.</value>
        public List<TreeNode> CheckedNodes
        {
            get { return tvObjects.GetAllCheckedNodes(); }
        }
        /// <summary>
        /// Gets the first checked node.
        /// </summary>
        /// <value>The first checked node.</value>
        public TreeNode FirstCheckedNode
        {
            get { return tvObjects.FirstCheckedNode; }
        }

        /// <summary>
        /// Gets the selected node.
        /// </summary>
        /// <value>The selected node.</value>
        public TreeNode SelectedNode
        {
            get { return tvObjects.SelectedNode; }
        }

        /// <summary>
        /// Gets a value indicating whether [check boxes].
        /// </summary>
        /// <value><c>true</c> if [check boxes]; otherwise, <c>false</c>.</value>
        public bool CheckBoxes
        {
            get { return tvObjects.CheckBoxes; }
            set { tvObjects.CheckBoxes = value; }
        }
        /// <summary>
        /// Gets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get
            {
                if (tvObjects.SelectedNode == null)
                    return null;
                return tvObjects.SelectedNode.Tag;
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyBrowser"/> class.
        /// </summary>
        public AssemblyBrowser()
        {
            InitializeComponent();
        }

        #region Interface

        /// <summary>
        /// Opens assembly file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void OpenFile(string fileName)
        {
            if (!(File.Exists(fileName)))
            {
                return;
            }
            tvObjects.Nodes.Clear();
            _browsedAssembly = Assembly.LoadFrom(fileName);
            AddAssembly(_browsedAssembly);
        }
        /// <summary>
        /// Adds the curent domain.
        /// </summary>
        public void AddCurentDomain()
        {
            AddDomain(AppDomain.CurrentDomain);
        }

        /// <summary>
        /// Adds the domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        public void AddDomain(AppDomain domain)
        {
            foreach (var assembly in domain.GetAssemblies())
            {
                AddAssembly( assembly);
            }
        }

        /// <summary>
        /// Adds the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void AddAssembly(Assembly assembly)
        {
            TreeNode node = new TreeNode
                                {
                                    ImageIndex = ((int) images.IndAssembly),
                                    SelectedImageIndex = ((int) images.IndAssembly),
                                    Text = assembly.GetName().Name,
                                    Tag = assembly
                                };
            foreach (Type typ in assembly.GetTypes())
            {
                node.Nodes.Add(CreateNodeType(typ));
            }
            tvObjects.Nodes.Add(node);
        }
        /// <summary>
        /// Clears vbrowser view.
        /// </summary>
        public void Clear()
        {
            _browsedAssembly = null;
            tvObjects.Nodes.Clear();
        }


        /// <summary>
        /// Closes the assembly.
        /// </summary>
        [Obsolete]
        public void CloseAssembly()
        {
            Clear();
        }
        /// <summary>
        /// Selects the node by full path.
        /// </summary>
        /// <param name="FullPath">The full path.</param>
        public void SelectNodeByFullPath(string FullPath)
        {

            TreeNode node = tvObjects.GetNode(FullPath, tvObjects.PathSeparator[0]); ;
            if (node == null)
            {
                return;
            }
            tvObjects.SelectedNode = node;
            tvObjects.Select();
        }
        #endregion

        #region tvObjects Events
        /// <summary>
        /// Handles the AfterSelect event of the tvObjects control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void TvObjectsAfterSelect(object sender, TreeViewEventArgs e)
        {
            OnAfterSelect(new EventArgs());
        }

        /// <summary>
        /// Handles the BeforeExpand event of the tvObjects control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void TvObjectsBeforeExpand(object sender, TreeViewCancelEventArgs e)
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
            if (((images)(node.ImageIndex)) == images.IndClass)
            {
                expandType(node);
            }
            else if (((images)(node.ImageIndex)) == images.IndProperty)
            {
                expandProperty(node);
            }
        }

        /// <summary>
        /// Handles the AfterCheck event of the tvObjects control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void TvObjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            tvObjects.CheckParentNodes(e.Node);
        }
        #endregion

        #region Load Assembly


        private TreeNode CreateNodeType(Type typ)
        {
            TreeNode node;
            node = new TreeNode();
            node.Text = typ.Name;
            cmbType.Items.Add(typ.Name);
            node.Tag = typ;
            node.ImageIndex = (int)images.IndClass;
            node.SelectedImageIndex = (int)images.IndClass;
            AddLoadNode(node.Nodes);
            return node;
        }

        private void AddField(TreeNodeCollection nodes, FieldInfo obj)
        {
            TreeNode node;
            node = new TreeNode();
            node.Text = obj.Name;
            node.Tag = obj;
            node.ImageIndex = (int)images.IndProperty;
            node.SelectedImageIndex = (int)images.IndProperty;
            nodes.Add(node);
        }

        private void AddProperty(TreeNodeCollection nodes, PropertyInfo obj)
        {
            TreeNode node;
            node = new TreeNode();
            node.Text = obj.Name;
            node.Tag = obj;
            node.ImageIndex = (int)images.IndProperty;
            node.SelectedImageIndex = (int)images.IndProperty;
            nodes.Add(node);
            if (!(obj.ReflectedType == null))
            {
                AddLoadNode(node.Nodes);
            }
        }

        private void AddLoadNode(TreeNodeCollection nodes)
        {
            nodes.Clear();
            nodes.Add("#");
        }

        private void AddMethod(TreeNodeCollection nodes, MethodInfo obj)
        {
            TreeNode node;
            node = new TreeNode();
            node.Text = obj.Name;
            node.Tag = obj;
            node.ImageIndex = (int)images.IndMethod;
            node.SelectedImageIndex = (int)images.IndMethod;
            nodes.Add(node);
        }

        private void expandType(TreeNode node)
        {
            Type typ = ((Type)(node.Tag));
            node.Nodes.Clear();
            foreach (FieldInfo inf in typ.GetFields())
            {
                AddField(node.Nodes, inf);
            }
            foreach (PropertyInfo inf in typ.GetProperties())
            {
                AddProperty(node.Nodes, inf);
            }
            foreach (MethodInfo inf in typ.GetMethods())
            {
                AddMethod(node.Nodes, inf);
            }
            foreach (Type inf in typ.GetInterfaces())
            {
                node.Nodes.Add(CreateNodeType(inf));
            }
        }

        private void expandProperty(TreeNode node)
        {
            PropertyInfo propery = ((PropertyInfo)(node.Tag));
            node.Nodes.Clear();
            node.Nodes.Add(CreateNodeType(propery.PropertyType));
        }
        #endregion

        #region Context Menu Events

        /// <summary>
        /// Handles the Click event of the findInToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FindInToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (tvObjects.SelectedNode == null)
            {
                return;
            }
            if (tvObjects.SelectedNode.ImageIndex != (int)images.IndClass)
            {
                MessageBoxEx.ShowWaring(null,"Search","Search only in types");
                return;
            }
            var frm = new FormText();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var typ = (Type)(tvObjects.SelectedNode.Tag);
            var arrayMemberInfo = typ.FindMembers(MemberTypes.All, 0, DelegateToSearchCriteria, frm.Value);

            for (int index = 0; index <= arrayMemberInfo.Length - 1; index++)
            {
                Console.WriteLine("Result of FindMembers -\t" + arrayMemberInfo[index] + "\n");
            }
        }

        /// <summary>
        /// Handles the Click event of the addBookMarkToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddBookMarkToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the uncheckAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UncheckAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            tvObjects.UncheckAllNodes();
        }

        /// <summary>
        /// Handles the Click event of the allToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AllToolStripMenuItemClick(object sender, EventArgs e)
        {
            tvObjects.CheckAllNodes();
        }


        /// <summary>
        /// Delegates to search criteria.
        /// </summary>
        /// <param name="objMemberInfo">The obj member info.</param>
        /// <param name="objSearch">The obj search.</param>
        /// <returns></returns>
        public static bool DelegateToSearchCriteria(MemberInfo objMemberInfo, object objSearch)
        {
            return objMemberInfo.Name.ToString() == objSearch.ToString();
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the cmdSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdSearchClick(object sender, EventArgs e)
        {
            tvObjects.SearchText(cmbType.Text);
        }
        /// <summary>
        /// Raises the <see cref="E:AfterSelect"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterSelect(EventArgs e)
        {
            if (AfterSelect != null)
                AfterSelect(this, e);
        }





    }
}