using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using Savchin.Comparer;

namespace Savchin.Forms.Comparer
{
    public partial class CompareView : UserControl
    {
        private static readonly Object EventOkClick = new object();
        private static readonly Object EventCancelClick = new object();

        /// <summary>
        /// Occurs when [ok click].
        /// </summary>
        [Category("Action"), Description("OkClick")]
        public event EventHandler OkClick
        {
            add
            {
                base.Events.AddHandler(EventOkClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventOkClick, value);
            }
        }

        /// <summary>
        /// Occurs when [cancel click].
        /// </summary>
        [Category("Action"), Description("CancelClick")]
        public event EventHandler CancelClick
        {
            add
            {
                base.Events.AddHandler(EventCancelClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventCancelClick, value);
            }
        }


        private enum ObjImage : int
        {
            Dictionary = 0,
            Key = 1,
            Object = 2,

            DictionaryNew = 3,
            KeyNew = 4,
            ObjectNew = 5,

            DictionaryEqual = 6,
            KeyEqual = 7,
            ObjectEqual = 8,

            DictionaryDelete = 9,
            KeyDelete = 10,
            ObjectDelete = 11,

            ObjectNotExists = 12
        }

        private ObjectResult _result = null;
        private Filter _filter = new Filter();

        private static Dictionary<ResultType, ObjImage> sourceResultTypeToObjImage = new Dictionary<ResultType, ObjImage>();
        private static Dictionary<ResultType, ObjImage> destionationResultTypeToObjImage = new Dictionary<ResultType, ObjImage>();

        #region Ctors
        /// <summary>
        /// Initializes the <see cref="CompareView"/> class.
        /// </summary>
        static CompareView()
        {
            sourceResultTypeToObjImage.Add(ResultType.Delete, ObjImage.ObjectNotExists);
            sourceResultTypeToObjImage.Add(ResultType.New, ObjImage.ObjectNew);
            sourceResultTypeToObjImage.Add(ResultType.NotEqual, ObjImage.ObjectEqual);
            sourceResultTypeToObjImage.Add(ResultType.Equal, ObjImage.Object);

            destionationResultTypeToObjImage.Add(ResultType.Delete, ObjImage.ObjectDelete);
            destionationResultTypeToObjImage.Add(ResultType.New, ObjImage.ObjectNotExists);
            destionationResultTypeToObjImage.Add(ResultType.NotEqual, ObjImage.ObjectEqual);
            destionationResultTypeToObjImage.Add(ResultType.Equal, ObjImage.Object);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareView"/> class.
        /// </summary>
        public CompareView()
        {
            InitializeComponent();
        }
        #endregion




        /// <summary>
        /// Raises the <see cref="E:OkClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnOkClick(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventOkClick];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:CancelClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnCancelClick(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventCancelClick];
            if (handler != null)
            {
                handler(this, e);
            }
        }



        #region Show Compare Resault
        /// <summary>
        /// Shows the compare result.
        /// </summary>
        /// <param name="result">The result.</param>
        public void ShowCompareResult(ObjectResult result)
        {
            _result = result;
            treeViewSource.Nodes.Clear();
            treeViewDestination.Nodes.Clear();

            if (result.IsEquals)
            {
                AddObjectResult(ResultType.Equal, result, treeViewSource.Nodes, treeViewDestination.Nodes);
            }
            else
            {
                AddObjectResult(ResultType.NotEqual, result, treeViewSource.Nodes, treeViewDestination.Nodes);
            }

        }

        /// <summary>
        /// Adds the objects node.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <param name="nodesSource">The nodes source.</param>
        /// <param name="nodesDestionation">The nodes destionation.</param>
        private void AddObjectsNode(IEnumerable<ObjectResult> objects, TreeNodeCollection nodesSource, TreeNodeCollection nodesDestionation)
        {
            foreach (ObjectResult result in objects)
            {
                AddObjectResult(
                    result.IsEquals ? ResultType.Equal : ResultType.NotEqual,
                    result,
                    nodesSource,
                    nodesDestionation);
            }
        }

        /// <summary>
        /// Adds the object result.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="result">The result.</param>
        /// <param name="nodesSource">The nodes source.</param>
        /// <param name="nodesDestionation">The nodes destionation.</param>
        private void AddObjectResult(ResultType type, CompareResultBase result, TreeNodeCollection nodesSource, TreeNodeCollection nodesDestionation)
        {
            //TODO::Uncomment
            //string nodeName = (result.Source != null) ? GetObjectName(result.Source) :
            //                                            GetObjectName(result.Destination);



            ////TreeNode sourceNode = AddObjectNode(
            ////                        nodeName,
            ////                        (int)sourceResultTypeToObjImage[type],
            ////                        result,
            ////                        nodesSource);

            ////TreeNode destionationNode = AddObjectNode(
            ////                        nodeName,
            ////                        (int)destionationResultTypeToObjImage[type],
            ////                        result,
            ////                        nodesDestionation);

            //////AddObjectsNode(result.Results.Values.OfType<ObjectResult>(), sourceNode.Nodes, destionationNode.Nodes);
            //////AddDictionariesNodes(result.Results.OfType<KeyValuePair<string, DictionaryResult>>(), sourceNode.Nodes, destionationNode.Nodes);

        }

        /// <summary>
        /// Adds the object node.
        /// </summary>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="ImageIndex">Index of the image.</param>
        /// <param name="result">The result.</param>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        private TreeNode AddObjectNode(string nodeName, int ImageIndex, CompareResultBase result, TreeNodeCollection nodes)
        {
            TreeNode node = nodes.Add(nodeName, nodeName, ImageIndex, ImageIndex);
            node.Tag = result;
            if (IsNotEqual(node))
                node.Checked = true;
            return node;
        }

        /// <summary>
        /// Adds the objects node.
        /// </summary>
        /// <param name="dictionaries">The dictionaries.</param>
        /// <param name="nodesSource">The nodes source.</param>
        /// <param name="nodesDestionation">The nodes destionation.</param>
        private void AddDictionariesNodes(IEnumerable<KeyValuePair<string, DictionaryResult>> dictionaries, TreeNodeCollection nodesSource, TreeNodeCollection nodesDestionation)
        {
            foreach (KeyValuePair<string, DictionaryResult> keyValuePair in dictionaries)
            {
                AddDictionaryNode(keyValuePair, nodesSource, nodesDestionation);
            }
        }

        /// <summary>
        /// Adds the dictionary node.
        /// </summary>
        /// <param name="keyValuePair">The key value pair.</param>
        /// <param name="nodesSource">The nodes source.</param>
        /// <param name="nodesDestionation">The nodes destionation.</param>
        private void AddDictionaryNode(KeyValuePair<string, DictionaryResult> keyValuePair, TreeNodeCollection nodesSource, TreeNodeCollection nodesDestionation)
        {
            DictionaryResult dictionaryResult = keyValuePair.Value;

            TreeNode destionationNode = AddDictionaryNode(keyValuePair.Key, dictionaryResult.IsEquals, nodesDestionation);
            TreeNode sourceNode = AddDictionaryNode(keyValuePair.Key, dictionaryResult.IsEquals, nodesSource);



            foreach (string key in dictionaryResult.Keys)
            {
                DictionaryEntryResult dictionaryEntryResult = dictionaryResult[key];

                AddObjectResult(dictionaryEntryResult.ResultType, dictionaryEntryResult.ObjectResult, sourceNode.Nodes, destionationNode.Nodes);
            }
        }

        /// <summary>
        /// Adds the dictionary node.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isEquals">if set to <c>true</c> [is equals].</param>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        private TreeNode AddDictionaryNode(string name, bool isEquals, TreeNodeCollection nodes)
        {
            int imageIndex;
            if (isEquals)
                imageIndex = (int)ObjImage.Dictionary;
            else
                imageIndex = (int)ObjImage.DictionaryEqual;

            TreeNode node = nodes.Add(name, name, imageIndex, imageIndex);
            return node;
        }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        private static string GetObjectName(object o)
        {
            string result = null;
            Type objectType = o.GetType();
            PropertyInfo propertyInfo = objectType.GetProperty("Name");

            if (propertyInfo != null && propertyInfo.CanRead)
            {
                object namevalue = propertyInfo.GetValue(o, null);
                if (namevalue != null)
                {
                    result = namevalue.ToString();
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                result = objectType.Name;
            }
            return result;
        }

        #endregion

        #region Sync Tree
        private bool blockCollapse = false;
        /// <summary>
        /// Handles the BeforeCollapse event of the treeViewSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void treeViewSource_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (blockCollapse)
            {
                blockCollapse = false;
                return;
            }
            blockCollapse = true;

            string FullPath = e.Node.FullPath;


            TreeNode node = treeViewDestination.GetNode(FullPath, treeViewSource.PathSeparator[0]);
            if (node != null)
                node.Collapse();
        }

        /// <summary>
        /// Handles the BeforeCollapse event of the treeViewDestination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void treeViewDestination_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (blockCollapse)
            {
                blockCollapse = false;
                return;
            }
            blockCollapse = true;

            string FullPath = e.Node.FullPath;


            TreeNode node = treeViewSource.GetNode(FullPath, treeViewDestination.PathSeparator[0]);
            if (node != null)
                node.Collapse();
        }

        private bool blockExpand = false;
        /// <summary>
        /// Handles the BeforeExpand event of the treeViewSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void treeViewSource_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (blockExpand)
            {
                blockExpand = false;
                return;
            }
            blockExpand = true;

            string FullPath = e.Node.FullPath;


            TreeNode node = treeViewDestination.GetNode(FullPath, treeViewSource.PathSeparator[0]);
            if (node != null)
                node.Expand();
        }

        /// <summary>
        /// Handles the BeforeExpand event of the treeViewDestination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void treeViewDestination_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (blockExpand)
            {
                blockExpand = false;
                return;
            }
            blockExpand = true;
            string FullPath = e.Node.FullPath;

            TreeNode node = treeViewSource.GetNode(FullPath, treeViewDestination.PathSeparator[0]);
            if (node != null)
                node.Expand();


        }

        private bool blockWheel = false;
        /// <summary>
        /// Handles the WndProcMouseWheel event of the treeViewSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Savchin.Forms.MessageEventArgs"/> instance containing the event data.</param>
        private void treeViewSource_WndProcMouseWheel(object sender, MessageEventArgs e)
        {
            if (blockWheel)
            {
                blockWheel = false;
                return;
            }

            blockWheel = true;

            Message msg = Message.Create(treeViewDestination.Handle, e.Message.Msg, e.Message.WParam, e.Message.LParam);
            treeViewDestination.SendMessage(ref msg);
        }

        /// <summary>
        /// Handles the WndProcMouseWheel event of the treeViewDestination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Savchin.Forms.MessageEventArgs"/> instance containing the event data.</param>
        private void treeViewDestination_WndProcMouseWheel(object sender, MessageEventArgs e)
        {
            if (blockWheel)
            {
                blockWheel = false;
                return;
            }
            blockWheel = true;

            Message msg = Message.Create(treeViewSource.Handle, e.Message.Msg, e.Message.WParam, e.Message.LParam);
            treeViewSource.SendMessage(ref msg);
        }

        private void treeViewSource_Scroll(object sender, MessageEventArgs e)
        {
            Message msg = Message.Create(treeViewDestination.Handle, e.Message.Msg, e.Message.WParam, new IntPtr(1));
            treeViewDestination.SendMessage(ref msg);
        }

        private void treeViewDestination_Scroll(object sender, MessageEventArgs e)
        {
            Message msg = Message.Create(treeViewSource.Handle, e.Message.Msg, e.Message.WParam, new IntPtr(1));
            treeViewSource.SendMessage(ref msg);

        }
        /// <summary>
        /// Handles the ScrollHorizontal event of the treeViewSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Savchin.Forms.MessageEventArgs"/> instance containing the event data.</param>
        private void treeViewSource_ScrollHorizontal(object sender, MessageEventArgs e)
        {
            Message msg = Message.Create(treeViewDestination.Handle, e.Message.Msg, e.Message.WParam, new IntPtr(1));
            treeViewDestination.SendMessage(ref msg);
        }

        /// <summary>
        /// Handles the ScrollHorizontal event of the treeViewDestination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Savchin.Forms.MessageEventArgs"/> instance containing the event data.</param>
        private void treeViewDestination_ScrollHorizontal(object sender, MessageEventArgs e)
        {
            Message msg = Message.Create(treeViewSource.Handle, e.Message.Msg, e.Message.WParam, new IntPtr(1));
            treeViewSource.SendMessage(ref msg);
        }



        #endregion

        #region Selection
        /// <summary>
        /// Handles the BeforeSelect event of the treeViewSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void treeViewSource_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            ObjImage objImage = (ObjImage)e.Node.ImageIndex;

            if (IsObjectNode(objImage))
                SetPrimtives(e.Node);

        }

        /// <summary>
        /// Handles the BeforeSelect event of the treeViewDestination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void treeViewDestination_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            ObjImage objImage = (ObjImage)e.Node.ImageIndex;

            if (IsObjectNode(objImage))
                SetPrimtives(e.Node);
        }


        private void SetPrimtives(TreeNode node)
        {
            ObjectResult result = (ObjectResult)node.Tag;
            bindingSource1.DataSource = result.Results.Values.OfType<PrimitiveResult>().ToArray();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.IsFirstDisplayedRow)
                return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (!(bool)row.Cells[0].Value)
            {
                row.DefaultCellStyle.BackColor = Color.Red;
                e.InheritedRowStyle.BackColor = Color.Red;
            }
        }
        #endregion

        #region ToolBar


        /// <summary>
        /// Handles the Click event of the toolStripButtonFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonFilter_Click(object sender, EventArgs e)
        {

            Enabled = false;
            FormFilter.EditFilter(_filter);
            Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonFirst control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonFirst_Click(object sender, EventArgs e)
        {
            if (treeViewSource.Nodes.Count == 0)
                return;

            TreeNode node = treeViewSource.FindFirst(IsNotEqual);
            if (node == null)
                return;

            treeViewSource.SelectedNode = node;
            node.Collapse(true);
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonPrevious control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonPrevious_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewSource.FindReverse(IsNotEqual);
            if (node == null)
                return;

            treeViewSource.SelectedNode = node;
            node.Collapse(true);
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewSource.FindNext(IsNotEqual);
            if (node == null)
                return;

            treeViewSource.SelectedNode = node;
            node.Collapse(true);
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonLast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonLast_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewSource.FindReverse(treeViewSource.GetLastNode(), IsNotEqual);
            if (node == null)
                return;

            treeViewSource.SelectedNode = node;
            node.Collapse(true);
        }


        /// <summary>
        /// Handles the Click event of the selectAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewDestination.CheckNodes(treeViewDestination.FindAll(IsNotEqual));
        }

        private void selectCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewDestination.CheckNodes(treeViewDestination.FindAll(IsCreate));
        }

        private void selectModificationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectDeletionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewDestination.UncheckAllNodes();
        }
        /// <summary>
        /// Handles the Click event of the toolStripButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            OnCancelClick(e);
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonOK_Click(object sender, EventArgs e)
        {
            if (_result != null)
                _result.Sync();

            OnOkClick(e);
        }


        #endregion

        #region Matchers

        private bool IsCreate(TreeNode node)
        {
            return IsCreate((ObjImage)node.ImageIndex);
        }

        private bool IsCreate(ObjImage ImageIndex)
        {
            return
                ImageIndex == ObjImage.ObjectNotExists;
        }

        /// <summary>
        /// Determines whether [is not equal] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// 	<c>true</c> if [is not equal] [the specified node]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsNotEqual(TreeNode node)
        {
            return IsNotEqual((ObjImage)node.ImageIndex);
        }

        private bool IsNotEqual(ObjImage ImageIndex)
        {
            return ImageIndex == ObjImage.ObjectDelete ||
                   ImageIndex == ObjImage.ObjectNew ||
                   ImageIndex == ObjImage.ObjectNotExists ||
                   ImageIndex == ObjImage.ObjectEqual ||
                   ImageIndex == ObjImage.DictionaryDelete ||
                   ImageIndex == ObjImage.DictionaryEqual ||
                   ImageIndex == ObjImage.DictionaryNew;
        }

        private bool IsObjectNode(ObjImage ImageIndex)
        {
            return ImageIndex == ObjImage.Object ||
                   ImageIndex == ObjImage.ObjectDelete ||
                   ImageIndex == ObjImage.ObjectNew ||
                   ImageIndex == ObjImage.ObjectNotExists ||
                   ImageIndex == ObjImage.ObjectEqual;
        }
        #endregion

        private void treeViewDestination_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Tag == null)
                return;

            CompareResultBase compareResult = (CompareResultBase)node.Tag;
            compareResult.IsSync = node.Checked;

            if (node.Checked == false)
            {
                treeViewDestination.UncheckNodes(node.Nodes);
            }

            if (compareResult is ObjectResult)
            {
                ObjectResult objectResult = (ObjectResult)compareResult;
                throw new NotImplementedException("Need uncomment code");
                //if (node.Checked)
                //{

                //    if (node.Parent != null && node.Parent.Parent != null)
                //        node.Parent.Parent.Checked = true;

                //    if (_filter.PrimitivesKeys.Count == 0)
                //        objectResult.Primitives.SyncAll();
                //    else
                //        objectResult.Primitives.Sync(_filter.PrimitivesKeys);
                //}
                //else
                //{
                //    if (_filter.PrimitivesKeys.Count == 0)
                //        objectResult.Primitives.UnSyncAll();
                //    else
                //        objectResult.Primitives.UnSync(_filter.PrimitivesKeys);
                //}
            }

        }

        private void treeViewDestination_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!e.Node.Checked)
                return;

            e.Cancel = !IsNotEqual(e.Node);
        }

        private void treeViewDestination_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }






    }
}
