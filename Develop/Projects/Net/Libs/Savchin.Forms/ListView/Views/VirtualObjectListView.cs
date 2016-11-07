using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A virtual object list view operates in virtual mode, that is, it only gets model objects for
    /// a row when it is needed. This gives it the ability to handle very large numbers of rows with
    /// minimal resources.
    /// </summary>
    /// <remarks><para>A listview is not a great user interface for a large number of items. But if you've
    /// ever wanted to have a list with 10 million items, go ahead, knock yourself out.</para>
    /// <para>Virtual lists can never iterate their contents. That would defeat the whole purpose.</para>
    /// <para>Given the above, grouping is not possible on virtual lists.</para>
    /// <para>For the same reason, animate GIFs should not be used in virtual lists. Animated GIFs require some state
    /// information to be stored for each animation, but virtual lists specifically do not keep any state information.
    /// In any case, you really do not want to keep state information for 10 million animations!</para>
    /// <para>
    /// Although it isn't documented, .NET virtual lists cannot have checkboxes. This class codes around this limitation,
    /// but you must use the functions provided by ObjectListView: CheckedObjects, CheckObject(), UncheckObject() and their friends. 
    /// </para>
    /// <para>
    /// If you use the normal check box properties (CheckedItems or CheckedIndicies), they will throw an exception, since the
    /// list is in virtual mode, and .NET "knows" it can't handle checkboxes in virtual mode.
    /// The "CheckBoxes" property itself can be set once, but trying to unset it later will throw an exception.
    /// </para>
    /// <para>Due to the limits of the underlying Windows control, virtual lists do not trigger ItemCheck/ItemChecked events. 
    /// Use a CheckStatePutter instead.</para>
    /// </remarks>
    public class VirtualObjectListView : ObjectListView
    {
        /// <summary>
        /// Create a VirtualObjectListView
        /// </summary>
        public VirtualObjectListView()
        {
            ShowGroups = false; // virtual lists can never show groups
            VirtualMode = true; // Virtual lists have to be virtual -- no prizes for guessing that :)

            CacheVirtualItems += HandleCacheVirtualItems;
            RetrieveVirtualItem += HandleRetrieveVirtualItem;
            SearchForVirtualItem += HandleSearchForVirtualItem;

            // At the moment, we don't need to handle this event. But we'll keep this comment to remind us about it.
            //this.VirtualItemsSelectionRangeChanged += new ListViewVirtualItemsSelectionRangeChangedEventHandler(VirtualObjectListView_VirtualItemsSelectionRangeChanged);

            DataSource = new VirtualListVersion1DataSource(this);
        }

        #region Public Properties

        private IVirtualListDataSource dataSource;

        /// <summary>
        /// Get or set the collection of model objects that are checked.
        /// When setting this property, any row whose model object isn't
        /// in the given collection will be unchecked. Setting to null is
        /// equivilent to unchecking all.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property returns a simple collection. Changes made to the returned
        /// collection do NOT affect the list. This is different to the behaviour of
        /// CheckedIndicies collection.
        /// </para>
        /// <para>
        /// When getting CheckedObjects, the performance of this method is O(n) where n is the number of checked objects.
        /// When setting CheckedObjects, the performance of this method is O(n) where n is the number of checked objects plus
        /// the number of objects to be checked.
        /// </para>
        /// <para>
        /// If the ListView is not currently showing CheckBoxes, this property does nothing. It does
        /// not remember any check box settings made.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IList CheckedObjects
        {
            get
            {
                var objects = new ArrayList();

                if (!CheckBoxes)
                    return objects;

                if (CheckStateGetter != null)
                    return base.CheckedObjects;

                foreach (var kvp in checkStateMap)
                {
                    if (kvp.Value == CheckState.Checked)
                        objects.Add(kvp.Key);
                }
                return objects;
            }
            set
            {
                if (!CheckBoxes)
                    return;

                if (value == null)
                    value = new ArrayList();

                var keys = new Object[checkStateMap.Count];
                checkStateMap.Keys.CopyTo(keys, 0);
                foreach (Object key in keys)
                {
                    if (value.Contains(key))
                        SetObjectCheckedness(key, CheckState.Checked);
                    else
                        SetObjectCheckedness(key, CheckState.Unchecked);
                }

                foreach (Object x in value)
                    SetObjectCheckedness(x, CheckState.Checked);
            }
        }

        /// <summary>
        /// Get/set the data source that is behind this virtual list
        /// </summary>
        /// <remarks>Setting this will cause the list to redraw.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IVirtualListDataSource DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                CustomSorter = delegate(OLVColumn column, SortOrder sortOrder)
                                   {
                                       ClearCachedInfo();
                                       dataSource.Sort(column, sortOrder);
                                   };
                UpdateVirtualListSize();
                Invalidate();
            }
        }

        /// <summary>
        /// This delegate is used to fetch a rowObject, given it's index within the list
        /// </summary>
        /// <remarks>Only use this property if you are not using a DataSource.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual RowGetterDelegate RowGetter
        {
            get { return ((VirtualListVersion1DataSource) dataSource).RowGetter; }
            set { ((VirtualListVersion1DataSource) dataSource).RowGetter = value; }
        }

        #endregion

        #region OLV accessing

        /// <summary>
        /// Return the number of items in the list
        /// </summary>
        /// <returns>the number of items in the list</returns>
        public override int GetItemCount()
        {
            return VirtualListSize;
        }

        /// <summary>
        /// Return the model object at the given index
        /// </summary>
        /// <param name="index">Index of the model object to be returned</param>
        /// <returns>A model object</returns>
        public override object GetModelObject(int index)
        {
            if (DataSource != null)
                return DataSource.GetNthObject(index);
            else
                return null;
        }

        /// <summary>
        /// Return the OLVListItem that displays the given model object
        /// </summary>
        /// <param name="modelObject">The modelObject whose item is to be found</param>
        /// <returns>The OLVListItem that displays the model, or null</returns>
        /// <remarks>This method has O(n) performance.</remarks>
        public override OLVListItem ModelToItem(object modelObject)
        {
            if (DataSource == null || modelObject == null)
                return null;

            int idx = DataSource.GetObjectIndex(modelObject);
            if (idx >= 0)
                return GetItem(idx);
            else
                return null;
        }

        #endregion

        #region Object manipulation

        /// <summary>
        /// Add the given collection of model objects to this control.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        /// <remarks>
        /// <para>The added objects will appear in their correct sort position, if sorting
        /// is active. Otherwise, they will appear at the end of the list.</para>
        /// <para>No check is performed to see if any of the objects are already in the ListView.</para>
        /// <para>Null objects are silently ignored.</para>
        /// </remarks>
        public override void AddObjects(ICollection modelObjects)
        {
            if (DataSource == null || modelObjects == null || modelObjects.Count == 0)
                return;

            // Give the world a chance to cancel or change the added objects
            var args = new ItemsAddingEventArgs(modelObjects);
            OnItemsAdding(args);
            if (args.Canceled)
                return;

            ClearCachedInfo();
            DataSource.AddObjects(args.ObjectsToAdd);
            Sort();
            UpdateVirtualListSize();
        }

        /// <summary>
        /// Remove all items from this list
        /// </summary>
        /// <remark>This method can safely be called from background threads.</remark>
        public override void ClearObjects()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(ClearObjects));
            else
            {
                ClearCachedInfo();
                DataSource.Clear();
                SetVirtualListSize(0);
            }
        }

        /// <summary>
        /// Refreshes the objects.
        /// </summary>
        /// <param name="minIndex">Index of the min.</param>
        /// <param name="maxIndex">Index of the max.</param>
        public void RefreshObjects(int minIndex, int maxIndex)
        {
            ClearCachedInfo();
            RedrawItems(minIndex, maxIndex, true);
        }

        /// <summary>
        /// Update the rows that are showing the given objects
        /// </summary>
        /// <remarks>This method does not resort the items.</remarks>
        public override void RefreshObjects(IList modelObjects)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate { RefreshObjects(modelObjects); });
                return;
            }

            // Without a data source, we can't do this.
            if (DataSource == null)
                return;

            ClearCachedInfo();
            foreach (object modelObject in modelObjects)
            {
                int index = DataSource.GetObjectIndex(modelObject);
                if (index >= 0)
                    RedrawItems(index, index, true);
            }
        }

        /// <summary>
        /// Remove all of the given objects from the control
        /// </summary>
        /// <param name="modelObjects">Collection of objects to be removed</param>
        /// <remarks>
        /// <para>Nulls and model objects that are not in the ListView are silently ignored.</para>
        /// <para>Due to problems in the underlying ListView, if you remove all the objects from
        /// the control using this method and the list scroll vertically when you do so,
        /// then when you subsequenially add more objects to the control,
        /// the vertical scroll bar will become confused and the control will draw one or more
        /// blank lines at the top of the list. </para>
        /// </remarks>
        public override void RemoveObjects(ICollection modelObjects)
        {
            if (DataSource == null || modelObjects == null || modelObjects.Count == 0)
                return;

            // Give the world a chance to cancel or change the removed objects
            var args = new ItemsRemovingEventArgs(modelObjects);
            OnItemsRemoving(args);
            if (args.Canceled)
                return;

            ClearCachedInfo();
            DataSource.RemoveObjects(args.ObjectsToRemove);
            UpdateVirtualListSize();
        }

        /// <summary>
        /// Select the row that is displaying the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="setFocus">Should the object be focused as well?</param>
        public override void SelectObject(object modelObject, bool setFocus)
        {
            // Without a data source, we can't do this.
            if (DataSource == null)
                return;

            // Check that the object is in the list (plus not all data sources can locate objects)
            int index = DataSource.GetObjectIndex(modelObject);
            if (index < 0 || index >= VirtualListSize)
                return;

            // If the given model is already selected, don't do anything else (prevents an flicker)
            if (SelectedIndices.Count == 1 && SelectedIndices[0] == index)
                return;

            // Finally, select the row
            SelectedIndices.Clear();
            SelectedIndices.Add(index);
            if (setFocus)
                SelectedItem.Focused = true;
        }

        /// <summary>
        /// Select the rows that is displaying any of the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        /// <remarks>This method has O(n) performance where n is the number of model objects passed.
        /// Do not use this to select all the rows in the list -- use SelectAll() for that.</remarks>
        public override void SelectObjects(IList modelObjects)
        {
            // Without a data source, we can't do this.
            if (DataSource == null)
                return;

            SelectedIndices.Clear();

            if (modelObjects == null)
                return;

            foreach (object modelObject in modelObjects)
            {
                int index = DataSource.GetObjectIndex(modelObject);
                if (index >= 0 && index < VirtualListSize)
                    SelectedIndices.Add(index);
            }
        }

        /// <summary>
        /// Set the collection of objects that this control will show.
        /// </summary>
        /// <param name="collection"></param>
        /// <remark>This method can safely be called from background threads.</remark>
        public override void SetObjects(IEnumerable collection)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate { SetObjects(collection); });
                return;
            }

            if (DataSource == null)
                return;

            BeginUpdate();
            try
            {
                // Give the world a chance to cancel or change the assigned collection
                var args = new ItemsChangingEventArgs(null, collection);
                OnItemsChanging(args);
                if (args.Canceled)
                    return;

                DataSource.SetObjects(args.NewObjects);
                UpdateVirtualListSize();
                Sort();
            }
            finally
            {
                EndUpdate();
            }
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Invalidate any cached information when we rebuild the list.
        /// </summary>
        public override void BuildList(bool shouldPreserveSelection)
        {
            ClearCachedInfo();
            Invalidate();
        }

        /// <summary>
        /// Clear any cached info this list may have been using
        /// </summary>
        public virtual void ClearCachedInfo()
        {
            lastRetrieveVirtualItemIndex = -1;
        }

        /// <summary>
        /// Get the checkedness of an object from the model. Returning null means the
        /// model does know and the value from the control will be used.
        /// </summary>
        /// <param name="modelObject"></param>
        /// <returns></returns>
        protected override CheckState? GetCheckState(object modelObject)
        {
            if (CheckStateGetter != null)
                return base.GetCheckState(modelObject);

            CheckState state = CheckState.Unchecked;
            if (modelObject != null)
                checkStateMap.TryGetValue(modelObject, out state);
            return state;
        }

        /// <summary>
        /// Create a OLVListItem for given row index
        /// </summary>
        /// <param name="itemIndex">The index of the row that is needed</param>
        /// <returns>An OLVListItem</returns>
        public virtual OLVListItem MakeListViewItem(int itemIndex)
        {
            var o = GetModelObject(itemIndex);
            if (o == null)
                return null;

            var olvi = new OLVListItem(o);
            FillInValues(olvi, olvi.RowObject);
            if (UseAlternatingBackColors)
            {
                if (View == View.Details && itemIndex % 2 == 1)
                    olvi.BackColor = AlternateRowBackColorOrDefault;
                else
                    olvi.BackColor = BackColor;

                CorrectSubItemColors(olvi);
            }

            SetSubItemImages(itemIndex, olvi);
            return olvi;
        }

        /// <summary>
        /// Record the change of checkstate for the given object in the model.
        /// This does not update the UI -- only the model
        /// </summary>
        /// <param name="modelObject"></param>
        /// <param name="state"></param>
        /// <returns>The check state that was recorded and that should be used to update
        /// the control.</returns>
        protected override CheckState PutCheckState(object modelObject, CheckState state)
        {
            state = base.PutCheckState(modelObject, state);
            checkStateMap[modelObject] = state;
            return state;
        }

        /// <summary>
        /// Prepare the listview to show alternate row backcolors
        /// </summary>
        /// <remarks>Alternate colored backrows can't be handle in the same way as our base class.
        /// With virtual lists, they are handled at RetrieveVirtualItem time.</remarks>
        protected override void PrepareAlternateBackColors()
        {
            // do nothing
        }

        /// <summary>
        /// Refresh the given item in the list
        /// </summary>
        /// <param name="olvi">The item to refresh</param>
        public override void RefreshItem(OLVListItem olvi)
        {
            ClearCachedInfo();
            RedrawItems(olvi.Index, olvi.Index, false);
        }

        /// <summary>
        /// Change the size of the list
        /// </summary>
        /// <param name="newSize"></param>
        protected virtual void SetVirtualListSize(int newSize)
        {
            if (newSize < 0 || VirtualListSize == newSize)
                return;

            int oldSize = VirtualListSize;

            ClearCachedInfo();

            // There is a bug in .NET when a virtual ListView is cleared
            // (i.e. VirtuaListSize set to 0) AND it is scrolled vertically: the scroll position 
            // is wrong when the list is next populated. To avoid this, before 
            // clearing a virtual list, we make sure the list is scrolled to the top.
            // [6 weeks later] Damn this is a pain! There are cases where this can also throw exceptions!
            try
            {
                if (newSize == 0 && TopItemIndex > 0)
                    TopItemIndex = 0;
            }
            catch (Exception)
            {
                // Ignore any failures
            }

            // In strange cases, this can throw the exceptions too. The best we can do is ignore them :(
            try
            {
                VirtualListSize = newSize;
            }
            catch (ArgumentOutOfRangeException)
            {
                // pass
            }
            catch (NullReferenceException)
            {
                // pass
            }

            // Tell the world that the size of the list has changed
            OnItemsChanged(new ItemsChangedEventArgs(oldSize, VirtualListSize));
        }

        /// <summary>
        /// Take ownership of the 'objects' collection. This separates our collection from the source.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method
        /// separates the 'objects' instance variable from its source, so that any AddObject/RemoveObject
        /// calls will modify our collection and not the original colleciton.
        /// </para>
        /// <para>
        /// VirtualObjectListViews always own their collections, so this is a no-op.
        /// </para>
        /// </remarks>
        protected override void TakeOwnershipOfObjects()
        {
        }

        /// <summary>
        /// Change the size of the virtual list so that it matches its data source
        /// </summary>
        public virtual void UpdateVirtualListSize()
        {
            if (DataSource != null)
                SetVirtualListSize(DataSource.GetObjectCount());
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Handle the CacheVirtualItems event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleCacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            if (DataSource != null)
                DataSource.PrepareCache(e.StartIndex, e.EndIndex);
        }

        /// <summary>
        /// Event handler for the column click event
        /// </summary>
        /// <remarks>
        /// <para>
        /// This differs from its base version by explicitly preserving selection.
        /// The base class (ObjectListView) stores the selection state in the ListViewItem
        /// objects, so when they are sorted, the selected-ness is automatically preserved.
        /// But a virtual list only knows which indices are selected, so the same rows are
        /// selected after sorting, even if they are showing different objects. So, we have
        /// to specifically remember which objects were selected, and then reselected them
        /// afterwards. 
        /// </para>
        /// <para>
        /// For large lists when many objects are selected, this re-selection
        /// is the slowest part of sorting the list.
        /// </para>
        /// </remarks>
        protected override void HandleColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!PossibleFinishCellEditing())
                return;

            // Toggle the sorting direction on successive clicks on the same column
            SortOrder order = SortOrder.Ascending;
            if (LastSortColumn != null && e.Column == LastSortColumn.Index)
                order = (LastSortOrder == SortOrder.Descending ? SortOrder.Ascending : SortOrder.Descending);

            BeginUpdate();
            try
            {
                Sort(GetColumn(e.Column), order);
            }
            finally
            {
                EndUpdate();
            }
        }

        /// <summary>
        /// Handle a RetrieveVirtualItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            // .NET 2.0 seems to generate a lot of these events. Before drawing *each* sub-item,
            // this event is triggered 4-8 times for the same index. So we save lots of CPU time
            // by caching the last result.
            if (lastRetrieveVirtualItemIndex != e.ItemIndex)
            {
                var o = MakeListViewItem(e.ItemIndex);
                if (o != null)
                {
                    lastRetrieveVirtualItemIndex = e.ItemIndex;
                    lastRetrieveVirtualItem = o;
                }
            }
            e.Item = lastRetrieveVirtualItem;
        }

        /// <summary>
        /// Handle the SearchForVirtualList event, which is called when the user types into a virtual list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleSearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            // The event has e.IsPrefixSearch, but as far as I can tell, this is always false (maybe that's different under Vista)
            // So we ignore IsPrefixSearch and IsTextSearch and always to a case insensitve prefix match.

            // We can't do anything if we don't have a data source
            if (DataSource == null)
                return;

            // Where should we start searching? If the last row is focused, the SearchForVirtualItemEvent starts searching
            // from the next row, which is actually an invalidate index -- so we make sure we never go past the last object.
            int start = Math.Min(e.StartIndex, DataSource.GetObjectCount() - 1);

            // Give the world a chance to fiddle with or completely avoid the searching process
            var args = new BeforeSearchingEventArgs(e.Text, start);
            OnBeforeSearching(args);
            if (args.Canceled)
                return;

            // Do the search
            int i = FindMatchingRow(args.StringToFind, args.StartSearchFrom, e.Direction);

            // Tell the world that a search has occurred
            var args2 = new AfterSearchingEventArgs(args.StringToFind, i);
            OnAfterSearching(args2);

            // If we found a match, tell the event
            if (i != -1)
                e.Index = i;
        }

        /// <summary>
        /// Find the first row in the given range of rows that prefix matches the string value of the given column.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="column"></param>
        /// <returns>The index of the matched row, or -1</returns>
        protected override int FindMatchInRange(string text, int first, int last, OLVColumn column)
        {
            return DataSource.SearchText(text, first, last, column);
        }

        /// <summary>
        /// Handle a mouse down event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!CheckBoxes)
                return;

            // Did the user click the state icon? If so, toggle the clicked row. 
            // If the given row is selected, all selected rows are given the same checkedness.
            ListViewHitTestInfo htInfo = HitTest(e.Location);
            if ((htInfo.Location & ListViewHitTestLocations.StateImage) != 0)
            {
                var clickedItem = (OLVListItem) htInfo.Item;
                ToggleCheckObject(clickedItem.RowObject);
                if (clickedItem.Selected)
                {
                    CheckState state = ModelToItem(clickedItem.RowObject).CheckState;
                    foreach (Object x in SelectedObjects)
                        SetObjectCheckedness(x, state);
                }
            }
        }

        #endregion

        #region Variable declaractions

        private readonly Dictionary<Object, CheckState> checkStateMap = new Dictionary<object, CheckState>();
        private OLVListItem lastRetrieveVirtualItem;
        private int lastRetrieveVirtualItemIndex = -1;

        #endregion
    }
}