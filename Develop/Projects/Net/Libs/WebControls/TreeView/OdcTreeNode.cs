using System;
using System.ComponentModel;
using System.Web.UI;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DefaultProperty("Text")]
    [ParseChildren(true, "ChildNodes")]
    public class OdcTreeNode : IStateManager, ICloneable
    {

        #region Properties
        /// <summary>
        /// Gets a collection of all child nodes.
        /// </summary>
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [MergableProperty(false)]
        public OdcTreeNodeCollection ChildNodes { get; private set; }

        /// <summary>
        /// Gets the TreeView that owns this node.
        /// </summary>
        [Browsable(false)]
        public OdcTreeView TreeView { get; internal set; }

        /// <summary>
        /// Gets  whether this node is selected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                OdcTreeView view = TreeView;
                return view != null ? view.SelectedNodekey == Key : false;
            }
        }

        /// <summary>
        /// Gets whether this node is the first in the same hierarchy.
        /// </summary>
        public bool IsFirst
        {
            get
            {
                OdcTreeNodeCollection c = Collection;
                return c != null && c.Count > 0 ? c[0] == this : false;
            }
        }

        /// <summary>
        /// Gets whether this node is the last in the same hierarchy.
        /// </summary>
        public bool IsLast
        {
            get
            {
                OdcTreeNodeCollection c = Collection;
                return c != null && c.Count > 0 ? c[c.Count - 1] == this : false;
            }
        }

        /// <summary>
        /// This value is a unique identifier.
        /// </summary>
        [Browsable(false)]
        public int Key { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is data bound.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is data bound; otherwise, <c>false</c>.
        /// </value>
        public bool IsDataBound { get; internal set; }

        private bool populateOnDemand;
        /// <summary>
        /// Gets or sets a value indicating whether [populate on demand].
        /// </summary>
        /// <value><c>true</c> if [populate on demand]; otherwise, <c>false</c>.</value>
        public bool PopulateOnDemand
        {
            get { return populateOnDemand; }
            set
            {
                if (populateOnDemand != value)
                {
                    populateOnDemand = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has child nodes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has child nodes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildNodes
        {
            get { return ChildNodes.Count > 0 || PopulateOnDemand; }
        }

        private bool? isExpanded;
        /// <summary>
        /// Gets or sets whether this node is expanded.
        /// </summary>
        public bool? IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    if (TreeView != null) TreeView.NotifyExpandedOrCollapsed(this);
                }
            }
        }


        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <value>The collection.</value>
        internal OdcTreeNodeCollection Collection
        {
            get
            {
                var node = owner as OdcTreeNode;
                if (node != null) return node.ChildNodes;
                var view = owner as OdcTreeView;
                return view != null ? view.Nodes : null;
            }
        }

        internal object owner;
        /// <summary>
        /// Gets the parent OdcTreeNode otherwise null.
        /// </summary>
        [Browsable(false)]
        public OdcTreeNode Parent
        {
            get { return owner as OdcTreeNode; }
        }


        /// <summary>
        /// Gets or sets the text for the OdcTreeNode.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets any serializable object to the node.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets the depth of this node in the hierarchy.
        /// </summary>
        public int Depth
        {
            get
            {
                if (owner == null) return -1;
                int depth = 0;
                OdcTreeNode node = Parent;
                while (node != null)
                {
                    depth++;
                    node = node.Parent;
                }
                return depth;
            }
        }

        /// <summary>
        /// Gets the data item assoicated with this node otherwise null.
        /// </summary>
        public object DataItem { get; internal set; }

        /// <summary>
        /// Gets the data path for the node if databound.
        /// </summary>
        public string DataPath { get; internal set; }

        /// <summary>
        /// Gets or sets an url for the image of this node.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets whether this node is checked.
        /// </summary>
        public bool IsChecked { get; set; }


        /// <summary>
        /// Gets or sets whether this node offers a checkbox.
        /// </summary>
        public bool? ShowCheckBox { get; set; }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public OdcTreeNodeContainer Container { get; internal set; }

        /// <summary>
        /// Gets whether this node is in edit mode.
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public bool EditMode
        {
            get
            {
                if (!CanEdit) return false;
                OdcTreeView view = TreeView;
                if (view != null)
                {
                    return view.EditNodeKey == Key;
                }
                else return false;
            }
        }


        /// <summary>
        /// Gets or sets the css class to use for this node.
        /// </summary>
        [DefaultValue("")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets whether the node can be edited.
        /// </summary>
        [DefaultValue(true)]
        public bool CanEdit { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNode"/> class.
        /// </summary>
        /// <param name="treeView">The tree view.</param>
        internal OdcTreeNode(OdcTreeView treeView)
        {
            TreeView = treeView;
            Init();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNode"/> class.
        /// </summary>
        public OdcTreeNode()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNode"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public OdcTreeNode(string text)
        {
            Init();
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNode"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        public OdcTreeNode(string text, object value)
        {
            Init();
            Text = text;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNode"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        /// <param name="isChecked">if set to <c>true</c> [is checked].</param>
        public OdcTreeNode(string text, object value, bool isChecked)
        {
            Init();
            Text = text;
            Value = value;
            IsChecked = isChecked;
            ShowCheckBox = true;
        }
        #endregion



        /// <summary>
        /// Removes this instance.
        /// </summary>
        public void Remove()
        {
            if (Parent != null)
                Parent.ChildNodes.Remove(this);
            else
                TreeView.Nodes.Remove(this);
        }

        /// <summary>
        /// Expands all.
        /// </summary>
        public void ExpandAll()
        {
            isExpanded = true;
            if (Parent != null)
                Parent.ExpandAll();
        }

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IStateManager Members

        /// <summary>
        /// When implemented by a class, gets a value indicating whether a server control is tracking its view state changes.
        /// </summary>
        /// <value></value>
        /// <returns>true if a server control is tracking its view state changes; otherwise, false.
        /// </returns>
        public bool IsTrackingViewState { get; private set; }

        /// <summary>
        /// Loads the state of the view.
        /// </summary>
        /// <param name="stateObject">The state object.</param>
        public void LoadViewState(object stateObject)
        {
            var state = (TreeNodeState)stateObject;
            state.Load(this);
        }

        /// <summary>
        /// When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> that contains the view state changes.
        /// </returns>
        public object SaveViewState()
        {
            return TreeNodeState.Create(this);
        }

        /// <summary>
        /// When implemented by a class, instructs the server control to track changes to its view state.
        /// </summary>
        public void TrackViewState()
        {
            IsTrackingViewState = true;
            ChildNodes = new OdcTreeNodeCollection(this);
        }

        #endregion

        private void Init()
        {
            ChildNodes = new OdcTreeNodeCollection(this);
            DataItem = this;
            CanEdit = true;
        }



        /// <summary>
        /// Gets whether this node is truly expanded.
        /// </summary>
        /// <returns></returns>
        internal bool IsNodeExpanded()
        {
            return IsExpanded ?? true;
        }

        /// <summary>
        /// Gets whether this node offers an expand or collapse button.
        /// </summary>
        /// <returns></returns>
        internal bool HasToggleButton()
        {
            return PopulateOnDemand || ChildNodes.Count > 0;
        }

        #region Nested type: TreeNodeState

        [Serializable]
        internal struct TreeNodeState
        {
            /// <summary>
            /// this value is most important since it is intended to identify the node for viewstate.
            /// </summary>
            public int key;

            private object childNodes;
            private string cssClass;
            private string imageUrl;
            private bool isChecked;

            private bool? isExpanded;



            private bool populate;
            private bool? showCheckBox;
            private string text;
            private object value;

            internal static TreeNodeState Create(OdcTreeNode node)
            {
                var state = new TreeNodeState
                                {
                                    text = node.Text,
                                    value = node.Value,
                                    imageUrl = node.ImageUrl,
                                    key = node.Key,
                                    isExpanded = node.IsExpanded,
                                    childNodes = node.ChildNodes.SaveViewState(),
                                    isChecked = node.IsChecked,
                                    showCheckBox = node.ShowCheckBox,
                                    populate = node.PopulateOnDemand,
                                    cssClass = node.CssClass,
                                };
                return state;
            }

            internal void Load(OdcTreeNode node)
            {
                node.ShowCheckBox = showCheckBox;
                node.IsChecked = isChecked;
                node.Text = text;
                node.Value = value;
                node.ImageUrl = imageUrl;
                node.Key = key;
                node.IsExpanded = isExpanded;
                node.PopulateOnDemand = populate;
                node.ChildNodes.LoadViewState(childNodes);
                node.CssClass = cssClass;

            }
        }

        #endregion
    }
}