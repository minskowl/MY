using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Savchin.Forms.Browsers;

namespace Savchin.Data.Schema.Controls
{
    /// <summary>
    /// SchemaBrowser control
    /// </summary>
    public partial class SchemaBrowser : UserControl, IObjectBrowser
    {
        private readonly List<DatabaseSchema> schemas = new List<DatabaseSchema>();


        /// <summary>
        /// Occurs when [after select].
        /// </summary>
        public event EventHandler AfterSelect;
        /// <summary>
        /// Occurs when [node mouse click].
        /// </summary>
        public event TreeNodeMouseClickEventHandler NodeMouseClick;

        private const string nodeNameTables = "Tables";
        private const string nodeNameFK = "Foreign Keys";
        private const string nodeNameColumns = "Columns";
        /// <summary>
        /// ObjImage
        /// </summary>
        public enum ObjImage : int
        {
            /// <summary>
            /// Diagram
            /// </summary>
            Diagram = 0,
            /// <summary>
            /// 
            /// </summary>
            ForeignKey = 1,
            /// <summary>
            /// 
            /// </summary>
            Folder = 2,
            /// <summary>
            /// 
            /// </summary>
            FolderOpen = 3,
            /// <summary>
            /// 
            /// </summary>
            PrimaryKey = 4,
            /// <summary>
            /// 
            /// </summary>
            StoredProcedure = 5,
            /// <summary>
            /// 
            /// </summary>
            Table = 6,
            /// <summary>
            /// 
            /// </summary>
            Loading = 7,
            /// <summary>
            /// 
            /// </summary>
            Column = 8
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaBrowser"/> class.
        /// </summary>
        public SchemaBrowser()
        {
            InitializeComponent();
        }


        #region Propperties

        /// <summary>
        /// Gets or sets a value indicating whether [check boxes].
        /// </summary>
        /// <value><c>true</c> if [check boxes]; otherwise, <c>false</c>.</value>
        public bool CheckBoxes
        {
            get { return tvObj.CheckBoxes; }
            set { tvObj.CheckBoxes = value; }
        }

        /// <summary>
        /// Gets the selected schema object.
        /// </summary>
        /// <value>The selected schema object.</value>
        public object SelectedSchemaObject
        {
            get
            {
                TreeNode node = tvObj.SelectedNode;
                if (node == null)
                    return null;
                return node.Tag;
            }
        }
        /// <summary>
        /// Gets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get
            {
                if (tvObj.SelectedNode == null)
                    return null;

                return tvObj.SelectedNode.Tag;
            }
        }
        /// <summary>
        /// Gets the checked nodes.
        /// </summary>
        /// <value>The checked nodes.</value>
        public List<TreeNode> CheckedNodes
        {
            get
            {
                return tvObj.GetAllCheckedNodes();
            }
        }

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public DatabaseSchema SelectedSchema
        {
            get
            {
                if (tvObj.SelectedNode == null)
                    return null;
                TreeNode rootSelectedNode = tvObj.GetRootNode(tvObj.SelectedNode);
                if (rootSelectedNode == null)
                    return null;


                return (DatabaseSchema)rootSelectedNode.Tag;
            }
        }
        /// <summary>
        /// Gets the schemas.
        /// </summary>
        /// <value>The schemas.</value>
        public IList<DatabaseSchema> Schemas
        {
            get
            {
                return schemas;
            }
        }
        #endregion

        /// <summary>
        /// Clears vbrowser view.
        /// </summary>
        public void Clear()
        {
            schemas.Clear();
            tvObj.Nodes.Clear();
        }


        /// <summary>
        /// Closes all schemas.
        /// </summary>
        [Obsolete("Use Clear method")]
        public void CloseAllSchemas()
        {
            Clear();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void OpenFile(string fileName)
        {
            ShowSchema(DatabaseSchema.Load(fileName));
        }

        /// <summary>
        /// Shows the schema.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        [Obsolete("Use OpenFile method")]
        public void ShowSchema(string filePath)
        {
            OpenFile(filePath);
        }

        /// <summary>
        /// Shows the schema.
        /// </summary>
        /// <param name="databaseSchema">The database schema.</param>
        public void ShowSchema(DatabaseSchema databaseSchema)
        {
            if (databaseSchema == null)
                return;

            schemas.Add(databaseSchema);

            TreeNode diagramNode = tvObj.Nodes.Add(databaseSchema.Name, databaseSchema.Name, (int)ObjImage.Diagram);
            diagramNode.Tag = databaseSchema;

            if (databaseSchema.Tables.Count > 0)
            {
                AddFolderNode(diagramNode.Nodes, nodeNameTables);
            }

            if (databaseSchema.ForeignKeys.Count > 0)
            {
                AddFolderNode(diagramNode.Nodes, nodeNameFK);
            }
        }
        /// <summary>
        /// Expands all.
        /// </summary>
        public void ExpandAll()
        {
            tvObj.ExpandAll();
        }

        #region Helpers

        private TreeNode AddFolderNode(TreeNodeCollection nodes, string name)
        {
            TreeNode result = nodes.Add(name, name, (int)ObjImage.Folder, (int)ObjImage.FolderOpen);
            AddLoadingNode(result.Nodes);
            return result;
        }

        private TreeNode AddLoadingNode(TreeNodeCollection nodes)
        {
            return nodes.Add("Loading ...", "Loading ...", (int)ObjImage.Loading);
        }

        private void tvObj_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count != 1)
                return;

            ObjImage image = (ObjImage)e.Node.Nodes[0].ImageIndex;
            if (image != ObjImage.Loading)
                return;

            switch (e.Node.Name)
            {
                case nodeNameTables:
                    ShowTables(e.Node);
                    break;

                case nodeNameFK:
                    ShowForeignKeys(e.Node);
                    break;
                case nodeNameColumns:
                    ShowColumns(e.Node);
                    break;
                default:
                    break;
            }
        }



        private void ShowTables(TreeNode tableNode)
        {
            DatabaseSchema schema = (DatabaseSchema)tableNode.Parent.Tag;
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (TableSchema table in schema.Tables)
            {
                TreeNode node = new TreeNode(table.Name, (int)ObjImage.Table, (int)ObjImage.Table);
                node.Tag = table;
                AddFolderNode(node.Nodes, nodeNameColumns);
                nodes.Add(node);
            }
            tableNode.Nodes.Clear();
            tableNode.Nodes.AddRange(nodes.ToArray());
        }
        private void ShowColumns(TreeNode nodeFolder)
        {
            TableSchema table = (TableSchema)nodeFolder.Parent.Tag;
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (ColumnSchema column in table.Columns)
            {
                TreeNode node = new TreeNode(column.Name, (int)ObjImage.Column, (int)ObjImage.Column);
                node.Tag = column;
                nodes.Add(node);
            }
            nodeFolder.Nodes.Clear();
            nodeFolder.Nodes.AddRange(nodes.ToArray());
        }

        private void ShowForeignKeys(TreeNode tableNode)
        {
            DatabaseSchema schema = (DatabaseSchema)tableNode.Parent.Tag;
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (ForeignKeySchema table in schema.ForeignKeys)
            {
                TreeNode node = new TreeNode(table.Name, (int)ObjImage.ForeignKey, (int)ObjImage.ForeignKey);
                node.Tag = table;
                nodes.Add(node);
            }
            tableNode.Nodes.Clear();
            tableNode.Nodes.AddRange(nodes.ToArray());
        }
        #endregion



        /// <summary>
        /// Handles the DoubleClick event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tvObj_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
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
        /// <summary>
        /// Raises the <see cref="E:NodeMouseClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (NodeMouseClick != null)
                NodeMouseClick(this, e);
        }
        /// <summary>
        /// Handles the AfterSelect event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvObj_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnAfterSelect(new EventArgs());
        }

        /// <summary>
        /// Handles the NodeMouseClick event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        private void tvObj_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OnNodeMouseClick(e);
        }



        ///// <summary>
        ///// Handles the NodeMouseClick event of the tvObj control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.Forms.TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        //private void tvObj_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    OnNodeMouseClick(e);
        //}

        ///// <summary>
        ///// Raises the <see cref="E:NodeMouseClick"/> event.
        ///// </summary>
        ///// <param name="e">The <see cref="System.Windows.Forms.TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        //protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        //{
        //    if (NodeMouseClick != null)
        //    {
        //        NodeMouseClick(this, e);
        //    }
        //}

    }
}
