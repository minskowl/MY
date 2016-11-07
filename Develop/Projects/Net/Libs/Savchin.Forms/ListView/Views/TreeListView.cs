using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A TreeListView combines an expandable tree structure with list view columns.
    /// </summary>
    /// <remarks>
    /// <para>To support tree operations, two delegates must be provided:</para>
    /// <list>
    /// <item>CanExpandGetter. This delegate must accept a model object and return a boolean indicating
    /// if that model should be expandable. </item>
    /// <item>ChildrenGetter. This delegate must accept a model object and return an IEnumerable of model
    /// objects that will be displayed as children of the parent model. This delegate will only be called
    /// for a model object if the CanExpandGetter has already returned true for that model.</item>
    /// </list>
    /// <para>
    /// The top level branches of the tree are set via the Roots property. SetObjects(), AddObjects() 
    /// and RemoveObjects() are interpreted as operations on this collection of roots.
    /// </para>
    /// <para>
    /// To add new children to an existing branch, make changes to your model objects and then
    /// call RefreshObject() on the parent.
    /// </para>
    /// <para>The tree must be a directed acyclic graph -- no cycles are allowed.</para>
    /// <para>More generally, each model object must appear only once in the tree. If the same model object appears in two
    /// places in the tree, the control will become confused.</para>
    /// </remarks>
    public class TreeListView : VirtualObjectListView
    {
        #region Delegates

        /// <summary>
        /// Delegates of this type are use to decide if the given model object can be expanded
        /// </summary>
        /// <param name="model">The model under consideration</param>
        /// <returns>Can the given model be expanded?</returns>
        public delegate bool CanExpandGetterDelegate(Object model);

        /// <summary>
        /// Delegates of this type are used to fetch the children of the given model object
        /// </summary>
        /// <param name="model">The parent whose children should be fetched</param>
        /// <returns>An enumerable over the children</returns>
        public delegate IEnumerable ChildrenGetterDelegate(Object model);

        #endregion

        /// <summary>
        /// The model that is used to manage the tree structure
        /// </summary>
        protected Tree TreeModel;

        private BaseRenderer treeRenderer;
        private bool useWaitCursorWhenExpanding = true;

        /// <summary>
        /// Make a default TreeListView
        /// </summary>
        public TreeListView()
        {
            TreeModel = new Tree(this);
            OwnerDraw = true;
            View = View.Details;

            DataSource = TreeModel;
            TreeColumnRenderer = new TreeRenderer();
        }

        #region Properties

        //------------------------------------------------------------------------------------------
        // Properties

        /// <summary>
        /// This is the delegate that will be used to decide if a model object can be expanded.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CanExpandGetterDelegate CanExpandGetter
        {
            get { return TreeModel.CanExpandGetter; }
            set { TreeModel.CanExpandGetter = value; }
        }

        /// <summary>
        /// This is the delegate that will be used to fetch the children of a model object
        /// </summary>
        /// <remarks>This delegate will only be called if the CanExpand delegate has 
        /// returned true for the model object.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ChildrenGetterDelegate ChildrenGetter
        {
            get { return TreeModel.ChildrenGetter; }
            set { TreeModel.ChildrenGetter = value; }
        }

        /// <summary>
        /// The model objects that form the top level branches of the tree.
        /// </summary>
        /// <remarks>Setting this does <b>NOT</b> reset the state of the control.
        /// In particular, it does not collapse branches.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IEnumerable Roots
        {
            get { return TreeModel.RootObjects; }
            set
            {
                // Make sure that column 0 is showing a tree
                if (GetColumn(0).Renderer == null)
                    GetColumn(0).Renderer = TreeColumnRenderer;
                if (value == null)
                    TreeModel.RootObjects = new ArrayList();
                else
                    TreeModel.RootObjects = value;
                UpdateVirtualListSize();
            }
        }

        /// <summary>
        /// The renderer that will be used to draw the tree structure
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual BaseRenderer TreeColumnRenderer
        {
            get { return treeRenderer; }
            set
            {
                treeRenderer = value;
                if (Columns.Count > 0)
                    GetColumn(0).Renderer = value;
            }
        }

        /// <summary>
        /// Should a wait cursor be shown when a branch is being expanded?
        /// </summary>
        /// <remarks>When this is true, the wait cursor will be shown whilst the children of the 
        /// branch are being fetched. If the children of the branch have already been cached, 
        /// the cursor will not change.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("Should a wait cursor be shown when a branch is being expaned?"),
         DefaultValue(true)]
        public virtual bool UseWaitCursorWhenExpanding
        {
            get { return useWaitCursorWhenExpanding; }
            set { useWaitCursorWhenExpanding = value; }
        }
        
        #endregion

        //------------------------------------------------------------------------------------------
        // Accessing

        /// <summary>
        /// Return true if the branch at the given model is expanded
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool IsExpanded(Object model)
        {
            Branch br = TreeModel.GetBranch(model);
            return (br != null && br.IsExpanded);
        }

        //------------------------------------------------------------------------------------------
        // Commands

        /// <summary>
        /// Collapse the subtree underneath the given model
        /// </summary>
        /// <param name="model"></param>
        public virtual void Collapse(Object model)
        {
            IList selection = SelectedObjects;
            int idx = TreeModel.Collapse(model);
            if (idx >= 0)
            {
                UpdateVirtualListSize();
                SelectedObjects = selection;
                RedrawItems(idx, GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Collapse all subtrees within this control
        /// </summary>
        public virtual void CollapseAll()
        {
            IList selection = SelectedObjects;
            int idx = TreeModel.CollapseAll();
            if (idx >= 0)
            {
                UpdateVirtualListSize();
                SelectedObjects = selection;
                RedrawItems(idx, GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Setup the list so it will draw selected rows using custom colours.
        /// </summary>
        /// <remarks>
        /// This method makes the list owner drawn, and ensures that all columns have at
        /// least a BaseRender installed.
        /// </remarks>
        public override void EnableCustomSelectionColors()
        {
            OwnerDraw = true;

            foreach (OLVColumn column in AllColumns)
            {
                if (column.Index > 0 && column.RendererDelegate == null)
                    column.Renderer = new BaseRenderer();
            }
        }

        /// <summary>
        /// Expand the subtree underneath the given model object
        /// </summary>
        /// <param name="model"></param>
        public virtual void Expand(Object model)
        {
            IList selection = SelectedObjects;
            int idx = TreeModel.Expand(model);
            if (idx >= 0)
            {
                UpdateVirtualListSize();
                SelectedObjects = selection;
                RedrawItems(idx, GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Expand all the branches within this tree recursively.
        /// </summary>
        /// <remarks>Be careful: this method could take a long time for large trees.</remarks>
        public virtual void ExpandAll()
        {
            IList selection = SelectedObjects;
            int idx = TreeModel.ExpandAll();
            if (idx >= 0)
            {
                UpdateVirtualListSize();
                SelectedObjects = selection;
                RedrawItems(idx, GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Update the rows that are showing the given objects
        /// </summary>
        public override void RefreshObjects(IList modelObjects)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => RefreshObjects(modelObjects)));
                return;
            }

            if (modelObjects.Count == 0)
                return;
            IList selection = SelectedObjects;

            // Refresh each object, remembering where the first update occured
            int firstChange = Int32.MaxValue;
            foreach (Object x in modelObjects)
            {
                int idx = TreeModel.RebuildChildren(x);
                if (idx >= 0)
                    firstChange = Math.Min(firstChange, idx);
            }
            UpdateVirtualListSize();
            SelectedObjects = selection;

            // Redraw everything from the first update to the end of the list
            RedrawItems(firstChange, GetItemCount() - 1, false);
        }

        /// <summary>
        /// Toggle the expanded state of the branch at the given model object
        /// </summary>
        /// <param name="model"></param>
        public virtual void ToggleExpansion(Object model)
        {
            if (IsExpanded(model))
                Collapse(model);
            else
                Expand(model);
        }

        //------------------------------------------------------------------------------------------
        // Delegates

        //------------------------------------------------------------------------------------------
        // Implementation

        /// <summary>
        /// Intercept the basic message pump to customise the mouse down and hit testing.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0201: // WM_LBUTTONDOWN
                    if (!HandleLButtonDown(ref m))
                        base.WndProc(ref m);
                    break;

                case 0x1012: // LVM_HITTEST = (LVM_FIRST + 18)
                    HandleHitTest(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// Handle a hit test to account for the indent of the branch
        /// </summary>
        /// <param name="m"></param>
        protected virtual unsafe void HandleHitTest(ref Message m)
        {
            //THINK: Do we need to do this, since we are using the build-in Level ability of
            // of ListCtrl, which should take the indent into account

            // We want to change our base behavior by taking the indentation of tree into account
            // when performing a hit test. So we figure out which row is at the test point,
            // then calculate the indentation for that row, and modify the hit test *inplace*
            // so that the normal hittest is done, but indented by the correct amount.

            DefWndProc(ref m);
            var hittest = (NativeMethods.LVHITTESTINFO*)m.LParam;
            // Find which row was hit...
            int row = hittest->iItem;
            if (row < 0)
                return;

            // ...from that decide the model object...
            Object model = TreeModel.GetNthObject(row);
            if (model == null)
                return;

            // ...and from that, the branch of the tree showing that model...
            Branch br = TreeModel.GetBranch(model);
            if (br == null)
                return;

            // ...use the indentation on that branch to modify the hittest
            hittest->pt_x += (br.Level * TreeRenderer.PIXELS_PER_LEVEL);
            DefWndProc(ref m);
        }

        /// <summary>
        /// Catch the Left Button down event.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected virtual bool HandleLButtonDown(ref Message m)
        {
            /// We have to intercept this low level message rather than the more natural
            /// overridding of OnMouseDown, since ListCtrl's internal mouse down behavior
            /// is to select (or deselect) rows when the mouse is released. We don't
            /// want the selection to change when the user expand/collapse rows, so if the
            /// mouse down event was to expand/collapse, we have to hide this mouse
            /// down event from the control. 

            int x = m.LParam.ToInt32() & 0xFFFF;
            int y = (m.LParam.ToInt32() >> 16) & 0xFFFF;

            // This horrible sequence finds what item is under the mouse position.
            // We want to find the item under the mouse, even if the mouse is not
            // actually over the icon or label. GetItemAt() will only do that
            // when FullRowSelect is true. 
            ListViewItem lvi = null;
            if (FullRowSelect)
                lvi = GetItemAt(x, y);
            else
            {
                FullRowSelect = true;
                lvi = GetItemAt(x, y);
                FullRowSelect = false;
            }

            // Are they trying to expand/collapse a row?
            return (lvi != null && HandlePossibleExpandClick((OLVListItem)lvi, x, y));
        }

        /// <summary>
        /// Handle the given mouse down event as a possible attempt to expand/collapse
        /// a row. Return true if the event was handled.
        /// </summary>
        /// <param name="olvItem">The olv item.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        protected virtual bool HandlePossibleExpandClick(OLVListItem olvItem, int x, int y)
        {
            Branch br = TreeModel.GetBranch(olvItem.RowObject);
            if (br == null || !br.CanExpand)
                return false;

            // Calculate if they clicked on the expand/collapse icon. This icon
            // appears before the icon of the item. 
            int smallImageWidth = 16;
            if (SmallImageList != null)
                smallImageWidth = SmallImageList.ImageSize.Width;

            Rectangle r = GetItemRect(olvItem.Index, ItemBoundsPortion.Icon);
            if (r.Width == 0)
            {
                // If GetItemRect() returns a  0-width rectangle, there are no images, 
                // but we still need to check if the click was before the text.
                r.X += (br.Level - 1) * (smallImageWidth + 2);
                r.Width = smallImageWidth;
            }
            else
                r.X -= (smallImageWidth + 2);

            // Take the checkboxes into account
            if (CheckBoxes)
                r.X -= (smallImageWidth + 2);

            if (!r.Contains(x, y))
                return false;

            PossibleFinishCellEditing();
            ToggleExpansion(olvItem.RowObject);
            return true;
        }

        /// <summary>
        /// Create a OLVListItem for given row index
        /// </summary>
        /// <param name="itemIndex">The index of the row that is needed</param>
        /// <returns>An OLVListItem</returns>
        /// <remarks>This differs from the base method by also setting up the IndentCount property.</remarks>
        public override OLVListItem MakeListViewItem(int itemIndex)
        {
            OLVListItem olvItem = base.MakeListViewItem(itemIndex);
            if (olvItem == null)
                return null;

            Branch br = TreeModel.GetBranch(olvItem.RowObject);
            if (br != null)
                olvItem.IndentCount = br.Level;
            return olvItem;
        }

        #region Event handlers

        /// <summary>
        /// Decide if the given key event should be handled as a normal key input to the control?
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            // We want to handle Left and Right keys within the control
            if (((keyData & Keys.KeyCode) == Keys.Left) || ((keyData & Keys.KeyCode) == Keys.Right))
            {
                return true;
            }
            else
                return base.IsInputKey(keyData);
        }

        /// <summary>
        /// Handle the keyboard input to mimic a TreeView.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            var focused = FocusedItem as OLVListItem;
            if (focused == null)
            {
                base.OnKeyDown(e);
                return;
            }

            Object modelObject = focused.RowObject;
            Branch br = TreeModel.GetBranch(modelObject);

            switch (e.KeyCode)
            {
                case Keys.Left:
                    // If the branch is expanded, collapse it. If it's collapsed,
                    // select the parent of the branch.
                    if (br.IsExpanded)
                        Collapse(modelObject);
                    else
                    {
                        if (br.ParentBranch != null && br.ParentBranch.Model != null)
                            SelectObject(br.ParentBranch.Model, true);
                    }
                    e.Handled = true;
                    break;

                case Keys.Right:
                    // If the branch is expanded, select the first child.
                    // If it isn't expanded and can be, expand it.
                    if (br.IsExpanded)
                    {
                        if (br.ChildBranches.Count > 0)
                            SelectObject(br.ChildBranches[0].Model, true);
                    }
                    else
                    {
                        if (br.CanExpand)
                            Expand(modelObject);
                    }
                    e.Handled = true;
                    break;
            }

            base.OnKeyDown(e);
        }

        #endregion

        //------------------------------------------------------------------------------------------
        // Support classes

        #region Nested type: Branch

        /// <summary>
        /// A Branch represents a sub-tree within a tree
        /// </summary>
        protected class Branch
        {
            #region BranchFlags enum

            [Flags]
            public enum BranchFlags
            {
                FirstBranch = 1,
                LastChild = 2,
                OnlyBranch = 4
            }

            #endregion

            private bool alreadyHasChildren;
            public List<Branch> ChildBranches = new List<Branch>();
            private BranchFlags flags;
            public bool IsExpanded;
            public int Level;
            public Object Model;
            public Branch ParentBranch;
            public Tree Tree;

            public Branch(Branch parent, Tree tree, Object model)
            {
                ParentBranch = parent;
                Tree = tree;
                Model = model;

                if (parent != null)
                    Level = parent.Level + 1;
            }

            //------------------------------------------------------------------------------------------
            // Properties

            /// <summary>
            /// Get the ancestor branches of this branch, with the 'oldest' ancestor first.
            /// </summary>
            public virtual IList<Branch> Ancestors
            {
                get
                {
                    var ancestors = new List<Branch>();
                    if (ParentBranch != null)
                        ParentBranch.PushAncestors(ancestors);
                    return ancestors;
                }
            }

            /// <summary>
            /// Can this branch be expanded?
            /// </summary>
            public virtual bool CanExpand
            {
                get
                {
                    if (Tree.CanExpandGetter == null || Model == null)
                        return false;
                    else
                        return Tree.CanExpandGetter(Model);
                }
            }

            /// <summary>
            /// Get/set the model objects that are beneath this branch
            /// </summary>
            public virtual IEnumerable Children
            {
                get
                {
                    var children = new ArrayList();
                    foreach (Branch x in ChildBranches)
                        children.Add(x.Model);
                    return children;
                }
                set
                {
                    ChildBranches.Clear();
                    foreach (Object x in value)
                        AddChild(x);
                    if (ChildBranches.Count > 0)
                        ChildBranches[ChildBranches.Count - 1].IsLastChild = true;
                }
            }

            /// <summary>
            /// Return the number of descendents of this branch that are currently visible
            /// </summary>
            /// <returns></returns>
            public virtual int NumberVisibleDescendents
            {
                get
                {
                    if (!IsExpanded)
                        return 0;

                    int count = ChildBranches.Count;
                    foreach (Branch br in ChildBranches)
                        count += br.NumberVisibleDescendents;
                    return count;
                }
            }

            /// <summary>
            /// Return true if this branch is the first branch of the entire tree
            /// </summary>
            public virtual bool IsFirstBranch
            {
                get { return ((flags & BranchFlags.FirstBranch) != 0); }
                set
                {
                    if (value)
                        flags |= BranchFlags.FirstBranch;
                    else
                        flags &= ~BranchFlags.FirstBranch;
                }
            }

            /// <summary>
            /// Return true if this branch is the last child of its parent
            /// </summary>
            public virtual bool IsLastChild
            {
                get { return ((flags & BranchFlags.LastChild) != 0); }
                set
                {
                    if (value)
                        flags |= BranchFlags.LastChild;
                    else
                        flags &= ~BranchFlags.LastChild;
                }
            }

            /// <summary>
            /// Return true if this branch is the only top level branch
            /// </summary>
            public virtual bool IsOnlyBranch
            {
                get { return ((flags & BranchFlags.OnlyBranch) != 0); }
                set
                {
                    if (value)
                        flags |= BranchFlags.OnlyBranch;
                    else
                        flags &= ~BranchFlags.OnlyBranch;
                }
            }

            private void PushAncestors(IList<Branch> list)
            {
                // This is designed to ignore the trunk (which has no parent)
                if (ParentBranch != null)
                {
                    ParentBranch.PushAncestors(list);
                    list.Add(this);
                }
            }

            private void AddChild(object model)
            {
                Branch br = Tree.GetBranch(model) ?? MakeBranch(model);
                ChildBranches.Add(br);
            }

            private Branch MakeBranch(object model)
            {
                var br = new Branch(this, Tree, model);
                Tree.RegisterBranch(br);
                return br;
            }

            //------------------------------------------------------------------------------------------
            // Commands

            /// <summary>
            /// Clear any cached information that this branch is holding
            /// </summary>
            public virtual void ClearCachedInfo()
            {
                Children = new ArrayList();
                alreadyHasChildren = false;
            }

            /// <summary>
            /// Collapse this branch
            /// </summary>
            public virtual void Collapse()
            {
                IsExpanded = false;
            }

            /// <summary>
            /// Expand this branch
            /// </summary>
            public virtual void Expand()
            {
                if (!CanExpand)
                    return;

                // THINK: Should we cache the children or fetch them each time? If we cache, we need a "DiscardCache" ability

                IsExpanded = true;
                if (alreadyHasChildren)
                    return;

                if (Tree.ChildrenGetter != null)
                {
                    Cursor previous = Cursor.Current;
                    try
                    {
                        if (Tree.TreeView.UseWaitCursorWhenExpanding)
                            Cursor.Current = Cursors.WaitCursor;
                        Children = Tree.ChildrenGetter(Model);
                    }
                    finally
                    {
                        if (Tree.TreeView.UseWaitCursorWhenExpanding)
                            Cursor.Current = previous;
                    }
                }

                alreadyHasChildren = true;
            }

            /// <summary>
            /// Expand this branch recursively
            /// </summary>
            public virtual void ExpandAll()
            {
                Expand();
                foreach (Branch br in ChildBranches)
                    br.ExpandAll();
            }

            /// <summary>
            /// Collapse the visible descendents of this branch into list of model objects
            /// </summary>
            /// <returns></returns>
            public virtual IList Flatten()
            {
                var flatList = new ArrayList();
                if (IsExpanded)
                    FlattenOnto(flatList);
                return flatList;
            }

            /// <summary>
            /// Flatten this branch's visible descendents onto the given list.
            /// </summary>
            /// <param name="flatList"></param>
            /// <remarks>The branch itself is <b>not</b> included in the list.</remarks>
            public virtual void FlattenOnto(IList flatList)
            {
                foreach (Branch br in ChildBranches)
                {
                    flatList.Add(br.Model);
                    if (br.IsExpanded)
                        br.FlattenOnto(flatList);
                }
            }

            /// <summary>
            /// Sort the sub-branches and their descendents so they are ordered according
            /// to the given comparer.
            /// </summary>
            /// <param name="comparer">The comparer that orders the branches</param>
            public virtual void Sort(BranchComparer comparer)
            {
                if (comparer == null || ChildBranches.Count == 0)
                    return;

                // We're about to sort the children, so clear the last child flag
                ChildBranches[ChildBranches.Count - 1].IsLastChild = false;

                ChildBranches.Sort(comparer);
                ChildBranches[ChildBranches.Count - 1].IsLastChild = true;

                foreach (Branch br in ChildBranches)
                    br.Sort(comparer);
            }

            //------------------------------------------------------------------------------------------
            // Public instance variables
        }

        #endregion

        #region Nested type: BranchComparer

        /// <summary>
        /// This class sorts branches according to how their respective model objects are sorted
        /// </summary>
        protected class BranchComparer : IComparer<Branch>
        {
            private readonly IComparer actualComparer;

            public BranchComparer(IComparer actualComparer)
            {
                this.actualComparer = actualComparer;
            }

            #region IComparer<Branch> Members

            public int Compare(Branch x, Branch y)
            {
                return actualComparer.Compare(x.Model, y.Model);
            }

            #endregion
        }

        #endregion

        #region Nested type: Tree

        /// <summary>
        /// A Tree object represents a tree structure data model that supports both 
        /// tree and flat list operations as well as fast access to branches.
        /// </summary>
        protected class Tree : IVirtualListDataSource
        {
            private readonly Dictionary<Object, Branch> mapObjectToBranch = new Dictionary<object, Branch>();
            private readonly Dictionary<Object, int> mapObjectToIndex = new Dictionary<object, int>();
            private readonly TreeListView treeView;
            private readonly Branch trunk;
            private OLVColumn lastSortColumn;
            private SortOrder lastSortOrder;
            private ArrayList objectList = new ArrayList();

            public Tree(TreeListView treeView)
            {
                this.treeView = treeView;
                trunk = new Branch(null, this, null);
                trunk.IsExpanded = true;
            }

            //------------------------------------------------------------------------------------------
            // Properties

            /// <summary>
            /// This is the delegate that will be used to decide if a model object can be expanded.
            /// </summary>
            public CanExpandGetterDelegate CanExpandGetter { get; set; }

            /// <summary>
            /// This is the delegate that will be used to fetch the children of a model object
            /// </summary>
            /// <remarks>This delegate will only be called if the CanExpand delegate has 
            /// returned true for the model object.</remarks>
            public ChildrenGetterDelegate ChildrenGetter { get; set; }

            /// <summary>
            /// Get or return the top level model objects in the tree
            /// </summary>
            public IEnumerable RootObjects
            {
                get { return trunk.Children; }
                set
                {
                    trunk.Children = value;
                    RebuildList();
                }
            }

            /// <summary>
            /// What tree view is this Tree the model for?
            /// </summary>
            public TreeListView TreeView
            {
                get { return treeView; }
            }

            #region IVirtualListDataSource Members

            public virtual object GetNthObject(int n)
            {
                return objectList[n];
            }

            public virtual int GetObjectCount()
            {
                return trunk.NumberVisibleDescendents;
            }

            public virtual int GetObjectIndex(object model)
            {
                int idx;

                return model != null && mapObjectToIndex.TryGetValue(model, out idx) ? idx : -1;
            }

            public virtual void PrepareCache(int first, int last)
            {
            }

            public virtual int SearchText(string value, int first, int last, OLVColumn column)
            {
                return AbstractVirtualListDataSource.DefaultSearchText(value, first, last, column, this);
            }

            public virtual void Sort(OLVColumn column, SortOrder order)
            {
                lastSortColumn = column;
                lastSortOrder = order;

                // Sorting is going to change the order of the branches so clear
                // the "first branch" flag
                if (trunk.ChildBranches.Count > 0)
                    trunk.ChildBranches[0].IsFirstBranch = false;

                trunk.Sort(GetBranchComparer());
                RebuildList();
            }

            public virtual void AddObjects(ICollection modelObjects)
            {
                var newRoots = new ArrayList();
                foreach (Object x in treeView.Roots)
                    newRoots.Add(x);

                foreach (Object x in modelObjects)
                    newRoots.Add(x);

                SetObjects(newRoots);
            }

            /// <summary>
            /// Adds the object.
            /// </summary>
            /// <param name="modelObject">The model object.</param>
            public void AddObject(object modelObject)
            {
                var newRoots = new ArrayList();
                foreach (Object x in treeView.Roots)
                    newRoots.Add(x);

                newRoots.Add(modelObject);

                SetObjects(newRoots);
            }

            public virtual void RemoveObjects(ICollection modelObjects)
            {
                var newRoots = new ArrayList();
                foreach (Object x in treeView.Roots)
                    newRoots.Add(x);
                foreach (Object x in modelObjects)
                    newRoots.Remove(x);
                SetObjects(newRoots);
            }

            /// </summary>
            public void Clear()
            {
                SetObjects(new ArrayList());
            }

            /// <summary>
            public virtual void SetObjects(IEnumerable collection)
            {
                // We interpret a SetObjects() call as setting the roots of the tree
                treeView.Roots = collection;
            }

            #endregion

            //------------------------------------------------------------------------------------------
            // Commands

            /// <summary>
            /// Collapse the subtree underneath the given model
            /// </summary>
            /// <param name="model">The model to be collapsed. If the model isn't in the tree,
            /// or if it is already collapsed, the command does nothing.</param>
            /// <returns>The index of the model in flat list version of the tree</returns>
            public virtual int Collapse(Object model)
            {
                Branch br = GetBranch(model);
                if (br == null || !br.IsExpanded)
                    return -1;

                int count = br.NumberVisibleDescendents;
                br.Collapse();

                // Remove the visible descendents from after the branch itself
                int idx = GetObjectIndex(model);
                objectList.RemoveRange(idx + 1, count);
                RebuildObjectMap(idx + 1);
                return idx;
            }

            /// <summary>
            /// Collapse all branches in this tree
            /// </summary>
            /// <returns>Return the index of the first root that was not collapsed</returns>
            public virtual int CollapseAll()
            {
                foreach (Branch br in trunk.ChildBranches)
                {
                    if (br.IsExpanded)
                        br.Collapse();
                }
                RebuildList();
                return 0;
            }

            /// <summary>
            /// Expand the subtree underneath the given model object
            /// </summary>
            /// <param name="model">The model to be expanded. If the model isn't in the tree,
            /// or if it cannot be expanded, the command does nothing.</param>
            /// <returns>The index of the model in flat list version of the tree</returns>
            public virtual int Expand(Object model)
            {
                Branch br = GetBranch(model);
                if (br == null || !br.CanExpand)
                    return -1;

                int idx = GetObjectIndex(model);
                InsertChildren(br, idx + 1);
                return idx;
            }

            /// <summary>
            /// Expand all branches in this tree
            /// </summary>
            /// <returns>Return the index of the first branch that was expanded</returns>
            public virtual int ExpandAll()
            {
                trunk.ExpandAll();
                Sort(lastSortColumn, lastSortOrder);
                return 0;
            }

            /// <summary>
            /// Return the Branch object that represents the given model in the tree
            /// </summary>
            /// <param name="model">The model whose branches is to be returned</param>
            /// <returns>The branch that represents the given model, or null if the model
            /// isn't in the tree.</returns>
            public virtual Branch GetBranch(object model)
            {
                Branch br;

                return mapObjectToBranch.TryGetValue(model, out br) ? br : null;
            }

            /// <summary>
            /// Rebuild the children of the given model, refreshing any cached information held about the given object
            /// </summary>
            /// <param name="model"></param>
            /// <returns>The index of the model in flat list version of the tree</returns>
            public virtual int RebuildChildren(Object model)
            {
                Branch br = GetBranch(model);
                if (br == null)
                    return -1;

                int count = br.NumberVisibleDescendents;
                br.ClearCachedInfo();

                // Remove the visible descendents from after the branch itself
                int idx = GetObjectIndex(model);
                if (count > 0)
                    objectList.RemoveRange(idx + 1, count);
                if (br.IsExpanded)
                    InsertChildren(br, idx + 1);
                return idx;
            }

            //------------------------------------------------------------------------------------------
            // Implementation

            /// <summary>
            /// Insert the children of the given branch into the given position
            /// </summary>
            /// <param name="br">The branch whose children should be inserted</param>
            /// <param name="idx">The index where the children should be inserted</param>
            protected virtual void InsertChildren(Branch br, int idx)
            {
                // Expand the branch
                br.Expand();
                br.Sort(GetBranchComparer());

                // Insert the branch's visible descendents after the branch itself
                objectList.InsertRange(idx, br.Flatten());
                RebuildObjectMap(idx);
            }

            /// <summary>
            /// Rebuild our flat internal list of objects.
            /// </summary>
            protected virtual void RebuildList()
            {
                objectList = ArrayList.Adapter(trunk.Flatten());
                if (trunk.ChildBranches.Count > 0)
                {
                    trunk.ChildBranches[0].IsFirstBranch = true;
                    trunk.ChildBranches[0].IsOnlyBranch = (trunk.ChildBranches.Count == 1);
                }
                RebuildObjectMap(0);
            }

            /// <summary>
            /// Rebuild our reverse index that maps an object to its location
            /// in the objectList array.
            /// </summary>
            /// <param name="startIndex"></param>
            protected virtual void RebuildObjectMap(int startIndex)
            {
                for (int i = startIndex; i < objectList.Count; i++)
                    mapObjectToIndex[objectList[i]] = i;
            }

            /// <summary>
            /// Remember that the given branch is part of this tree.
            /// </summary>
            /// <param name="br"></param>
            public virtual void RegisterBranch(Branch br)
            {
                mapObjectToBranch[br.Model] = br;
            }

            //------------------------------------------------------------------------------------------

            protected virtual BranchComparer GetBranchComparer()
            {
                if (lastSortColumn == null)
                    return null;
                else
                    return new BranchComparer(new ModelObjectComparer(lastSortColumn, lastSortOrder,
                                                                      treeView.GetColumn(0), lastSortOrder));
            }
        }

        #endregion

        #region Nested type: TreeRenderer

        /// <summary>
        /// This class handles drawing the tree structure of the primary column.
        /// </summary>
        public class TreeRenderer : BaseRenderer
        {
            /// <summary>
            /// How many pixels will be reserved for each level of indentation?
            /// </summary>
            public static int PIXELS_PER_LEVEL = 16 + 1;

            /// <summary>
            /// Should the renderer draw lines connecting siblings?
            /// </summary>
            public bool IsShowLines = true;

            public TreeRenderer()
            {
                LinePen = new Pen(Color.Blue, 1.0f);
                LinePen.DashStyle = DashStyle.Dot;
            }

            /// <summary>
            /// Return the branch that the renderer is currently drawing.
            /// </summary>
            private Branch Branch
            {
                get { return TreeListView.TreeModel.GetBranch(RowObject); }
            }

            /// <summary>
            /// Return the pen that will be used to draw the lines between branches
            /// </summary>
            public Pen LinePen { get; set; }

            /// <summary>
            /// Return the TreeListView for which the renderer is being used.
            /// </summary>
            public TreeListView TreeListView
            {
                get { return (TreeListView)ListView; }
            }

            /// <summary>
            /// The real work of drawing the tree is done in this method
            /// </summary>
            /// <param name="g"></param>
            /// <param name="r"></param>
            public override void Render(Graphics g, Rectangle r)
            {
                DrawBackground(g, r);

                Branch br = Branch;

                if (IsShowLines)
                    DrawLines(g, r, LinePen, br);

                if (br.CanExpand)
                {
                    Rectangle r2 = r;
                    r2.Offset(1 + (br.Level - 1) * PIXELS_PER_LEVEL, 0);
                    r2.Width = PIXELS_PER_LEVEL;

                    VisualStyleElement element = VisualStyleElement.TreeView.Glyph.Closed;
                    if (br.IsExpanded)
                        element = VisualStyleElement.TreeView.Glyph.Opened;
                    var renderer = new VisualStyleRenderer(element);
                    renderer.DrawBackground(g, r2);
                }

                int indent = br.Level * PIXELS_PER_LEVEL;
                r.Offset(indent, 0);
                r.Width -= indent;

                DrawImageAndText(g, r);
            }

            private static void DrawLines(Graphics g, Rectangle r, Pen p, Branch br)
            {
                Rectangle r2 = r;
                r2.Width = PIXELS_PER_LEVEL;

                // Vertical lines have to start on even points, otherwise the dotted line looks wrong.
                // This isn't need if pen isn't dotted.
                int top = r2.Top;
                if (p.DashStyle == DashStyle.Dot && (top & 1) == 1)
                    top += 1;

                // Draw lines for ancestors
                int midX;
                IList<Branch> ancestors = br.Ancestors;
                foreach (Branch ancestor in ancestors)
                {
                    if (!ancestor.IsLastChild)
                    {
                        midX = r2.Left + r2.Width / 2;
                        g.DrawLine(p, midX, top, midX, r2.Bottom);
                    }
                    r2.Offset(PIXELS_PER_LEVEL, 0);
                }

                // Draw lines for this branch
                midX = r2.Left + r2.Width / 2;
                int midY = r2.Top + r2.Height / 2;
                // Horizontal line first
                g.DrawLine(p, midX, midY, r2.Right, midY);
                // Vertical line second
                if (br.IsFirstBranch)
                {
                    if (!br.IsOnlyBranch)
                        g.DrawLine(p, midX, midY, midX, r2.Bottom);
                }
                else
                {
                    if (br.IsLastChild)
                        g.DrawLine(p, midX, top, midX, midY);
                    else
                        g.DrawLine(p, midX, top, midX, r2.Bottom);
                }
            }
        }

        #endregion
    }
}