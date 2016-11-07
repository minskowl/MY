using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PdPDM;
using PdWSP;
using Savchin.Forms.Browsers;
using Application = PdCommon.Application;
using BaseObject = PdCommon.BaseObject;
using NamedObject = PdCommon.NamedObject;
using View = PdPDM.View;

namespace Savchin.Controls.Browsers
{
    public partial class PDBrowser : UserControl, IObjectBrowser
    {
        public event EventHandler AfterSelect;


        #region Declaration

        private const string tTables = "Tables";
        private const string tSPs = "SPs";
        private const string tViews = "Views";



        public delegate void ChangeStatusEventHandler(string Message, MessageType Type);
        public delegate void LoadingHandler();
        public delegate void OpenProcedureHandler(Procedure sp);

        public event ChangeStatusEventHandler ChangeStatus;
        public event LoadingHandler Loading;
        public event LoadingHandler Loaded;
        public event OpenProcedureHandler OpenProcedure;



        public enum MessageType
        {
            mtInfo = 0,
            mtWarning = 1,
            mtError = 2
        }

        private bool ResourcesLoaded = false;


        #endregion

        #region Properties

        private string _modelFilePath;
        public string ModelFilePath
        {
            get { return _modelFilePath; }
            set { _modelFilePath = value; }
        }


        private string _resourcePath;
        /// <summary>
        /// Gets or sets the resource path.
        /// </summary>
        /// <value>The resource path.</value>
        public string ResourcePath
        {
            get
            {
                return _resourcePath;
            }
            set
            {
                if (value == null)
                    return;

                if (!(Directory.Exists(value)))
                {
                    throw new DirectoryNotFoundException("Direcory not found: " + value);
                }
                if (value.Substring(value.Length - 1) == "\\")
                {
                    _resourcePath = value;
                }
                else
                {
                    _resourcePath = value + "\\";
                }
                LoadResources();
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
        /// Gets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public BaseObject SelectedBaseObject
        {
            get
            {
                if (tvObj.SelectedNode == null)
                    return null;

                return (BaseObject)tvObj.SelectedNode.Tag;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show search].
        /// </summary>
        /// <value><c>true</c> if [show search]; otherwise, <c>false</c>.</value>
        public bool ShowSearch
        {
            get { return panelSearch.Visible; }
            set { panelSearch.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check boxes].
        /// </summary>
        /// <value><c>true</c> if [check boxes]; otherwise, <c>false</c>.</value>
        public bool CheckBoxes
        {
            get { return tvObj.CheckBoxes; }
            set { tvObj.CheckBoxes = value; }
        }

        #endregion

        public PDBrowser()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Clears vbrowser view.
        /// </summary>
        public void Clear()
        {
            tvObj.Nodes.Clear();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void OpenFile(string fileName)
        {
            ModelFilePath = fileName;
        }

        /// <summary>
        /// Closes the PD.
        /// </summary>
        [Obsolete("Use Clear method.")]
        public void ClosePD()
        {
            Clear();
        }

        #region Events
        private void PDBrowser_Resize(object sender, EventArgs e)
        {
            buttonSearch.Left = Width - buttonSearch.Width;
            cmbObjects.Width = buttonSearch.Left - cmbObjects.Left - 10;
            tvObj.Top = cmbObjects.Bottom + 10;
            tvObj.Height = Height - tvObj.Top;
            tvObj.Width = Width;
        }

        private void PDBrowser_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                LoadModel();
        }

        /// <summary>
        /// Handles the AfterExpand event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvObj_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ExpandNode(e.Node);
        }
        /// <summary>
        /// Handles the DoubleClick event of the tvObj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tvObj_DoubleClick(object sender, EventArgs e)
        {
            //OpenSelectedObject();

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

        #region Menu
        private void miLoadModel_Click_1(object sender, EventArgs e)
        {
            LoadModel();
        }

        private void miSaveModel_Click_1(object sender, EventArgs e)
        {
            Model m = null;
            try
            {
                if (tvObj.SelectedNode == null)
                {
                    return;
                }
                if (tvObj.SelectedNode.ImageIndex != (int)ind.imitModel)
                {
                    if (ChangeStatus != null)
                    {
                        ChangeStatus("Please select model", MessageType.mtWarning);
                    }
                    return;
                }
                BaseObject itemObject = (BaseObject)tvObj.SelectedNode.Tag;
                if (itemObject.ClassKind == (int)PdPDM_Classes.cls_Model)
                {
                    m = ((Model)(tvObj.SelectedNode.Tag));
                    if (m.Modified)
                    {
                        throw new NotImplementedException();
                        //m.Save( );
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ChangeStatus != null)
                {
                    ChangeStatus(ex.Message, MessageType.mtError);
                }
                return;
            }
            if (ChangeStatus != null)
            {
                ChangeStatus("PD Model " + m.Name + " saved sucessfuly", MessageType.mtInfo);
            }
        }


        private void miOpenSP_Click_1(object sender, EventArgs e)
        {
            // OpenSelectedObject();
        }
        private void cmMain_Opening(object sender, CancelEventArgs e)
        {
            if (tvObj.SelectedNode == null)
            {
                miLoadModel.Visible = true;
                miSaveModel.Visible = false;
                return;
            }
            if (tvObj.SelectedNode.ImageIndex == (int)ind.imitModel)
            {
                miLoadModel.Visible = true;
                miSaveModel.Visible = true;
            }
            else if (tvObj.SelectedNode.ImageIndex == (int)ind.imitSP)
            {
                miLoadModel.Visible = false;
                miSaveModel.Visible = false;
            }
            else
            {
                miLoadModel.Visible = false;
                miSaveModel.Visible = false;
            }
        }

        #endregion

        #endregion

        #region Images
        private enum ind
        {
            //indTables = 0,
            //indProcedures = 1,
            //imitServDis = 0,
            //imitServEn = 1,
            imitFolder = 2,
            imitPackage = 15,
            imitModel = 16,
            imitTable = 3,
            imitView = 4,
            imitSP = 5,
            //imitDB = 6,
            imitLoading = 7,

            imitColumn = 8,
            //imitParametr = 9,
            //imitDBCur = 10,
            //imitFileSQL = 11,
            //imitFile = 12,
            //imitSave = 17,
            // imitSaveCur = 19,
            // imitOpen = 18,
            icError = 13,
            //icExlamation = 14
        }
        private void LoadResources()
        {

            if (ResourcesLoaded)
            {
                return;
            }
            try
            {
                ImageList il = new ImageList();
                il.Images.Add(new Icon(_resourcePath + "empty.ico"));

                tvObj.ImageList = il;
            }
            catch 
            {
            }
            ResourcesLoaded = true;
        }
        #endregion

        #region Create Nodes
        private TreeNode CreateLoadingNode()
        {
            TreeNode nLoading = new TreeNode();
            nLoading.Text = "Loading...";
            nLoading.ImageIndex = (int)ind.imitLoading;
            nLoading.SelectedImageIndex = (int)ind.imitLoading;
            return nLoading;
        }

        private TreeNode CreateObjNode(NamedObject o, bool l)
        {
            TreeNode n = new TreeNode();
            n.Tag = o;
            if (o.ClassName == "Package")
            {
                n.ImageIndex = (int)ind.imitPackage;
                n.SelectedImageIndex = (int)ind.imitPackage;
                n.Text = o.Code;
            }
            else if (o.ClassName == "Procedure")
            {
                n.ImageIndex = (int)ind.imitSP;
                n.SelectedImageIndex = (int)ind.imitSP;
                n.Text = o.Code;
            }
            else if (o.ClassName == "View")
            {
                n.ImageIndex = (int)ind.imitView;
                n.SelectedImageIndex = (int)ind.imitView;
            }
            else if (o.ClassName == "Table")
            {
                n.ImageIndex = (int)ind.imitTable;
                n.SelectedImageIndex = (int)ind.imitTable;
                n.Text = o.Code;
            }
            else if (o.ClassName == "Column")
            {
                n.ImageIndex = (int)ind.imitColumn;
                n.SelectedImageIndex = (int)ind.imitColumn;
                var c = ((Column)(o));
                n.Text = o.Code + " " + c.DataType;
            }
            else
            {
                n.ImageIndex = (int)ind.icError;
                n.SelectedImageIndex = (int)ind.icError;
                n.Text = o.Code;
            }
            if (l)
            {
                n.Nodes.Add(CreateLoadingNode());
            }
            return n;
        }

        private TreeNode CreateFolderNode(object o, string name, bool l)
        {
            TreeNode n = new TreeNode();
            n.ImageIndex = (int)ind.imitFolder;
            n.SelectedImageIndex = (int)ind.imitFolder;
            n.Text = name;
            n.Tag = o;
            if (l)
            {
                n.Nodes.Add(CreateLoadingNode());
            }
            return n;
        }

        #endregion

        #region Prints Nodes

        private void PrintWorkspace(TreeNodeCollection nodes, Workspace ws)
        {
            if (nodes.Count > 0)
            {
                nodes.Clear();
            }
            WorkspaceModel wm;
            try
            {
                foreach (WorkspaceElement we in ws.Children)
                {
                    if (we.ClassName == "Workspace Model")
                    {

                        wm = (WorkspaceModel)we;
                        if (wm.ModelObject == null && wm.filename == ModelFilePath)
                            wm.Open();


                        if (wm.ModelObject != null && wm.ModelObject.ClassName == "Physical Data Model")
                        {
                            TreeNode n = new TreeNode();
                            n.Tag = wm;
                            n.ImageIndex = (int)ind.imitModel;
                            n.SelectedImageIndex = (int)ind.imitModel;
                            n.Text = wm.Name;
                            n.Nodes.Add(CreateLoadingNode());
                            n.Tag = wm.ModelObject;

                            nodes.Add(n);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnChangeStatus(ex.Message, MessageType.mtError);

            }
        }

        private void PrintModel(TreeNodeCollection nodes, Model pack)
        {
            if (nodes.Count > 0)
            {
                nodes.Clear();
            }
            var countNodes = pack.Packages.Count;

            if (pack.Tables.Count > 0)
                countNodes++;

            if (pack.Views.Count > 0)
                countNodes++;

            if (pack.Procedures.Count > 0)
                countNodes++;

            var n = new TreeNode[countNodes];

            countNodes = 0;
            foreach (NamedObject o in pack.Packages)
            {
                n[countNodes] = CreateObjNode(o, true);
                countNodes++;
            }
            if (pack.Tables.Count > 0)
            {
                n[countNodes] = CreateFolderNode(pack, tTables, true);
                countNodes++;
                foreach (Table table in pack.Tables)
                {
                    cmbObjects.Items.Add(table.Name);
                }

            }
            if (pack.Views.Count > 0)
            {
                n[countNodes] = CreateFolderNode(pack, tViews, true);
                countNodes++;
            }
            if (pack.Procedures.Count > 0)
            {
                n[countNodes] = CreateFolderNode(pack, tSPs, true);
            }
            nodes.AddRange(n);
        }

        private void PrintPackage(TreeNodeCollection nodes, Package pack)
        {
            if (nodes.Count > 0)
            {
                nodes.Clear();
            }
            var countNodes = pack.Packages.Count;
            if (pack.Tables.Count > 0)
                countNodes++;
            if (pack.Views.Count > 0)
                countNodes++;
            if (pack.Procedures.Count > 0)
                countNodes++;
            var n = new TreeNode[countNodes];

            countNodes = 0;
            foreach (NamedObject o in pack.Packages)
            {
                n[countNodes] = CreateObjNode(o, true);
                countNodes++;
            }
            if (pack.Tables.Count > 0)
            {
                n[countNodes] = CreateFolderNode(pack, tTables, true);
                countNodes++;
            }
            if (pack.Views.Count > 0)
            {
                n[countNodes] = CreateFolderNode(pack, tViews, true);
                countNodes++;
            }
            if (pack.Procedures.Count > 0)
            {
                n[countNodes] = CreateFolderNode(pack, tSPs, true);
            }
            nodes.AddRange(n);
        }

        private void PrintObjectCol(TreeNodeCollection nodes, ObjectCol col, bool l)
        {
            if (nodes.Count > 0)
            {
                nodes.Clear();
            }

            int c;
            if (col.Count < 100)
            {
                c = col.Count;
            }
            else
            {
                c = 100;
            }
            var arNodes = new TreeNode[c];
            int i = 0;
            int j = 0;
            nodes.Clear();
            foreach (NamedObject o in col)
            {
                arNodes[j] = CreateObjNode(o, l);
                if ((j == 99))
                {
                    nodes.AddRange(arNodes);
                    j = -1;
                    if ((col.Count - i < 100))
                    {
                        // TODO: NotImplemented statement: ICSharpCode.SharpRefactory.Parser.AST.VB.ReDimStatement 
                    }
                    else if (col.Count - 1 == i)
                    {
                        return;
                    }
                }
                j += 1;
                i += 1;
                System.Windows.Forms.Application.DoEvents();
            }
            nodes.AddRange(arNodes);
        }


        #endregion




        bool NodeLoaded = false;
        private void ExpandNode(TreeNode node)
        {

            if (node.FirstNode.ImageIndex != (int)ind.imitLoading)
            {
                return;
            }

            if (NodeLoaded)
            {
                return;
            }
            NodeLoaded = true;
            OnLoading();

            try
            {
                if (node.ImageIndex == (int)ind.imitPackage)
                {
                    PrintPackage(node.Nodes, (Package)node.Tag);
                }
                else if (node.ImageIndex == (int)ind.imitFolder)
                {
                    Model p = ((Model)(node.Tag));
                    if (node.Text == tTables)
                    {
                        PrintObjectCol(node.Nodes, p.Tables, true);
                    }
                    else if (node.Text == tTables)
                    {
                        PrintObjectCol(node.Nodes, p.Views, true);
                    }
                    else if (node.Text == tSPs)
                    {
                        PrintObjectCol(node.Nodes, p.Procedures, true);
                    }
                }
                else if (node.ImageIndex == (int)ind.imitTable)
                {
                    Table t;
                    t = ((Table)(node.Tag));
                    PrintObjectCol(node.Nodes, t.Columns, false);
                }
                else if (node.ImageIndex == (int)ind.imitView)
                {
                    View t;
                    t = ((View)(node.Tag));
                    PrintObjectCol(node.Nodes, t.Columns, false);
                }
                else if (node.ImageIndex == (int)ind.imitSP)
                {
                    //Procedure t = ((Procedure)(node.Tag));
                    throw new NotImplementedException("PrintSPParams(.Nodes, t)");
                }
                else if (node.ImageIndex == (int)ind.imitModel)
                {
                    try
                    {
                        if (node.Tag == null)
                        {
                            if (ChangeStatus != null)
                            {
                                ChangeStatus("imitModel :)", MessageType.mtWarning);
                            }
                            return;
                        }
                        BaseObject o = (BaseObject)node.Tag;
                        if (o.ClassName == "Workspace Model")
                        {
                            WorkspaceModel wm = (WorkspaceModel)node.Tag;
                            if (wm.ModelObject == null)
                            {
                                wm.Open();
                            }
                            node.Tag = wm.ModelObject;
                            PrintModel(node.Nodes, (Model)wm.ModelObject);
                        }
                        else if (o.ClassName == "Physical Data Model")
                        {
                            PrintModel(node.Nodes, (Model)o);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnChangeStatus(ex.Message, MessageType.mtError);
                    }
                }
            }
            catch (Exception ex)
            {
                OnChangeStatus(ex.Message, MessageType.mtError);

            }
            OnLoaded();

            NodeLoaded = false;
        }

        private void LoadModel()
        {
            try
            {

                //Guid guid = new Guid("{A299BAEB-C0EF-4794-A8E5-02D9FEFC4F21}");

                //Type powerDesignerApplictionType = Type.GetTypeFromCLSID(guid);


                //Type powerDesignerApplictionType = Type.GetTypeFromProgID("PowerDesigner.Application");
                //appl = (Application)Activator.CreateInstance(powerDesignerApplictionType);

                //PrintWorkspace(tvObj.Nodes, (Workspace)appl.ActiveWorkspace);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Cannot create ActiveX component.")
                    OnChangeStatus("Please Load PowerDesigner", MessageType.mtWarning);
                else
                    OnChangeStatus(ex.Message, MessageType.mtError);

            }
        }


        //public void OpenSelectedObject()
        //{
        //    if (tvObj.SelectedNode == null)
        //    {
        //        return;
        //    }
        //    if (tvObj.SelectedNode.ImageIndex == (int)ind.imitSP)
        //    {
        //        if (OpenProcedure != null)
        //        {
        //            OpenProcedure(((Procedure)(tvObj.SelectedNode.Tag)));
        //        }
        //    }
        //    else
        //    {
        //        if (ChangeStatus != null)
        //        {
        //            ChangeStatus("Please select procedure", MessageType.mtWarning);
        //        }
        //    }
        //}



        #region Raising Events
        /// <summary>
        /// Called when [change status].
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Type">The type.</param>
        protected virtual void OnChangeStatus(string Message, MessageType Type)
        {
            if (ChangeStatus != null)
                ChangeStatus(Message, Type);

        }
        /// <summary>
        /// Called when [loading].
        /// </summary>
        protected virtual void OnLoading()
        {
            if (Loading != null)
                Loading();
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        protected virtual void OnLoaded()
        {
            if (Loaded != null)
                Loaded();
        }

        /// <summary>
        /// Called when [open procedure].
        /// </summary>
        /// <param name="sp">The sp.</param>
        protected virtual void OnOpenProcedure(Procedure sp)
        {
            if (OpenProcedure != null)
                OpenProcedure(sp);
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
        #endregion

        private void openDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvObj.SelectedNode == null)
                return;
            if (tvObj.SelectedNode.ImageIndex == (int)ind.imitTable)
            {
                PdPDM.BaseObject baseObject = (PdPDM.BaseObject)tvObj.SelectedNode.Tag;
                Model model = (Model)tvObj.SelectedNode.Parent.Parent.Tag;
                foreach (PhysicalDiagram diagram in model.PhysicalDiagrams)
                {
                    PdPDM.BaseObject symbol = diagram.FindSymbol(baseObject, true);
                    if (symbol != null)
                    {

                        diagram.OpenView();
                        // diagram.AdjustSymbolToText((PdPDM.BaseObject)symbol, true, false);
                        // ARect view = diagram.GetUsedRectangle();
                        // view.Left = 1000;
                        // int views = diagram.GetViewCount();
                        // PdPDM.TableSymbol tableSymbol = (PdPDM.TableSymbol)symbol;
                        //tableSymbol.ShowPropertySheet();
                        return;
                    }

                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            tvObj.SearchText(cmbObjects.Text);
        }




    }
}
