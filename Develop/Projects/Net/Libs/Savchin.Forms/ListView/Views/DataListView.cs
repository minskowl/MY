using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A DataListView is a ListView that can be bound to a datasource (which would normally be a DataTable or DataView).
    /// </summary>
    /// <remarks>
    /// <para>This listview keeps itself in sync with its source datatable by listening for change events.</para>
    /// <para>If the listview has no columns when given a data source, it will automatically create columns to show all of the datatables columns.
    /// This will be only the simplest view of the world, and would look more interesting with a few delegates installed.</para>
    /// <para>This listview will also automatically generate missing aspect getters to fetch the values from the data view.</para>
    /// <para>Changing data sources is possible, but error prone. Before changing data sources, the programmer is responsible for modifying/resetting
    /// the column collection to be valid for the new data source.</para>
    /// <para>Internally, a CurrencyManager controls keeping the data source in-sync with other users of the data source (as per normal .NET
    /// behavior). This means that the model objects in the DataListView are DataRowView objects. If you write your own AspectGetters/Setters,
    /// they will be given DataRowView objects.</para>
    /// </remarks>
    public class DataListView : ObjectListView
    {
        #region Public Properties

        private string dataMember = "";
        private Object dataSource;

        /// <summary>
        /// Get or set the DataSource that will be displayed in this list view.
        /// </summary>
        /// <remarks>The DataSource should implement either <see cref="IList"/>, <see cref="IBindingList"/>,
        /// or <see cref="IListSource"/>. Some common examples are the following types of objects:
        /// <list type="unordered">
        /// <item><see cref="DataView"/></item>
        /// <item><see cref="DataTable"/></item>
        /// <item><see cref="DataSet"/></item>
        /// <item><see cref="DataViewManager"/></item>
        /// <item><see cref="BindingSource"/></item>
        /// </list>
        /// <para>When binding to a list container (i.e. one that implements the
        /// <see cref="IListSource"/> interface, such as <see cref="DataSet"/>)
        /// you must also set the <see cref="DataMember"/> property in order
        /// to identify which particular list you would like to display. You
        /// may also set the <see cref="DataMember"/> property even when
        /// DataSource refers to a list, since <see cref="DataMember"/> can
        /// also be used to navigate relations between lists.</para>
        /// </remarks>
        [Category("Data"),
         TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        public virtual Object DataSource
        {
            get { return dataSource; }
            set
            {
                //THINK: Should we only assign it if it is changed?
                //if (dataSource != value) {
                dataSource = value;
                RebindDataSource(true);
                //}
            }
        }

        /// <summary>
        /// Gets or sets the name of the list or table in the data source for which the DataListView is displaying data.
        /// </summary>
        /// <remarks>If the data source is not a DataSet or DataViewManager, this property has no effect</remarks>
        [Category("Data"),
         Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", typeof (UITypeEditor)),
         DefaultValue("")]
        public virtual string DataMember
        {
            get { return dataMember; }
            set
            {
                if (dataMember != value)
                {
                    dataMember = value;
                    RebindDataSource();
                }
            }
        }

        #endregion

        #region Initialization

        private CurrencyManager currencyManager;

        /// <summary>
        /// Our data source has changed. Figure out how to handle the new source
        /// </summary>
        protected virtual void RebindDataSource()
        {
            RebindDataSource(false);
        }

        /// <summary>
        /// Our data source has changed. Figure out how to handle the new source
        /// </summary>
        protected virtual void RebindDataSource(bool forceDataInitialization)
        {
            if (BindingContext == null)
                return;

            // Obtain the CurrencyManager for the current data source.
            CurrencyManager tempCurrencyManager = null;

            if (DataSource != null)
            {
                tempCurrencyManager = (CurrencyManager) BindingContext[DataSource, DataMember];
            }

            // Has our currency manager changed?
            if (currencyManager != tempCurrencyManager)
            {
                // Stop listening for events on our old currency manager
                if (currencyManager != null)
                {
                    currencyManager.MetaDataChanged -= currencyManager_MetaDataChanged;
                    currencyManager.PositionChanged -= currencyManager_PositionChanged;
                    currencyManager.ListChanged -= currencyManager_ListChanged;
                }

                currencyManager = tempCurrencyManager;

                // Start listening for events on our new currency manager
                if (currencyManager != null)
                {
                    currencyManager.MetaDataChanged += currencyManager_MetaDataChanged;
                    currencyManager.PositionChanged += currencyManager_PositionChanged;
                    currencyManager.ListChanged += currencyManager_ListChanged;
                }

                // Our currency manager has changed so we have to initialize a new data source
                forceDataInitialization = true;
            }

            if (forceDataInitialization)
                InitializeDataSource();
        }

        /// <summary>
        /// The data source for this control has changed. Reconfigure the control for the new source
        /// </summary>
        protected virtual void InitializeDataSource()
        {
            if (Frozen || currencyManager == null)
                return;

            CreateColumnsFromSource();
            CreateMissingAspectGettersAndPutters();
            SetObjects(currencyManager.List);

            // If we have some data, resize the new columns based on the data available.
            if (Items.Count > 0)
            {
                foreach (ColumnHeader column in Columns)
                {
                    if (column.Width == 0)
                        AutoResizeColumn(column.Index, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
        }

        /// <summary>
        /// Create columns for the listview based on what properties are available in the data source
        /// </summary>
        /// <remarks>
        /// <para>This method will not replace existing columns.</para>
        /// </remarks>
        protected virtual void CreateColumnsFromSource()
        {
            if (currencyManager == null || Columns.Count != 0)
                return;

            PropertyDescriptorCollection properties = currencyManager.GetItemProperties();
            if (properties.Count == 0)
                return;

            for (int i = 0; i < properties.Count; i++)
            {
                // Make a stack variable to hold the property so it can be used in the AspectGetter delegate
                PropertyDescriptor property = properties[i];

                // Relationships to other tables turn up as IBindibleLists. Don't make columns to show them.
                // CHECK: Is this always true? What other things could be here? Constraints? Triggers?
                if (property.PropertyType == typeof (IBindingList))
                    continue;

                // Create a column
                var column = new OLVColumn(property.DisplayName, property.Name);
                column.Width = 0; // zero-width since we will resize it once we have some data
                column.AspectGetter = delegate(object row) { return property.GetValue(row); };
                // If our column is a BLOB, it could be an image, so assign a renderer to draw it.
                // CONSIDER: Is this a common enough case to warrant this code?
                if (property.PropertyType == typeof (Byte[]))
                    column.Renderer = new ImageRenderer();

                // Add it to our list
                Columns.Add(column);
            }
        }

        /// <summary>
        /// Generate aspect getters and putters for any columns that are missing them (and for which we have
        /// enough information to actually generate a getter)
        /// </summary>
        protected virtual void CreateMissingAspectGettersAndPutters()
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                OLVColumn column = GetColumn(i);
                if (column.AspectGetter == null && !String.IsNullOrEmpty(column.AspectName))
                {
                    column.AspectGetter = delegate(object row)
                                              {
                                                  // In most cases, rows will be DataRowView objects
                                                  var drv = row as DataRowView;
                                                  if (drv != null)
                                                      return drv[column.AspectName];
                                                  else
                                                      return column.GetAspectByName(row);
                                              };
                }
                if (column.IsEditable && column.AspectPutter == null && !String.IsNullOrEmpty(column.AspectName))
                {
                    column.AspectPutter = delegate(object row, object newValue)
                                              {
                                                  // In most cases, rows will be DataRowView objects
                                                  var drv = row as DataRowView;
                                                  if (drv != null)
                                                      drv[column.AspectName] = newValue;
                                                  else
                                                      column.PutAspectByName(row, newValue);
                                              };
                }
            }
        }

        #endregion

        #region Object manipulations

        /// <summary>
        /// Add the given collection of model objects to this control.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        /// <remarks>This is a no-op for data lists, since the data
        /// is controlled by the DataSource. Manipulate the data source
        /// rather than this view of the data source.</remarks>
        public override void AddObjects(ICollection modelObjects)
        {
        }

        /// <summary>
        /// Remove the given collection of model objects from this control.
        /// </summary>
        /// <remarks>This is a no-op for data lists, since the data
        /// is controlled by the DataSource. Manipulate the data source
        /// rather than this view of the data source.</remarks>
        public override void RemoveObjects(ICollection modelObjects)
        {
        }

        #endregion

        #region Event Handlers

        private bool isChangingIndex;

        /// <summary>
        /// What should we do when the list is unfrozen
        /// </summary>
        protected override void DoUnfreeze()
        {
            RebindDataSource(true);
        }

        /// <summary>
        /// Handles binding context changes
        /// </summary>
        /// <param name="e">The EventArgs that will be passed to any handlers
        /// of the BindingContextChanged event.</param>
        protected override void OnBindingContextChanged(EventArgs e)
        {
            base.OnBindingContextChanged(e);

            // If our binding context changes, we must rebind, since we will
            // have a new currency managers, even if we are still bound to the
            // same data source.
            RebindDataSource(false);
        }

        /// <summary>
        /// Handles parent binding context changes
        /// </summary>
        /// <param name="e">Unused EventArgs.</param>
        protected override void OnParentBindingContextChanged(EventArgs e)
        {
            base.OnParentBindingContextChanged(e);

            // BindingContext is an ambient property - by default it simply picks
            // up the parent control's context (unless something has explicitly
            // given us our own). So we must respond to changes in our parent's
            // binding context in the same way we would changes to our own
            // binding context.
            RebindDataSource(false);
        }

        // CurrencyManager ListChanged event handler.
        // Deals with fine-grained changes to list items.
        // It's actually difficult to deal with these changes in a fine-grained manner.
        // If our listview is grouped, then any change may make a new group appear or
        // an old group disappear. It is rarely enough to simply update the affected row.
        protected virtual void currencyManager_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                    // Well, usually fine-grained... The whole list has changed utterly, so reload it.
                case ListChangedType.Reset:
                    InitializeDataSource();
                    break;

                    // A single item has changed, so just refresh that.
                    // TODO: Even in this simple case, we should probably rebuild the list.
                case ListChangedType.ItemChanged:
                    Object changedRow = currencyManager.List[e.NewIndex];
                    RefreshObject(changedRow);
                    break;

                    // A new item has appeared, so add that.
                    // We get this event twice if certain grid controls are used to add a new row to a
                    // datatable: once when the editing of a new row begins, and once again when that
                    // editing commits. (If the user cancels the creation of the new row, we never see
                    // the second creation.) We detect this by seeing if this is a view on a row in a
                    // DataTable, and if it is, testing to see if it's a new row under creation.
                case ListChangedType.ItemAdded:
                    Object newRow = currencyManager.List[e.NewIndex];
                    var drv = newRow as DataRowView;
                    if (drv == null || !drv.IsNew)
                    {
                        // Either we're not dealing with a view on a data table, or this is the commit
                        // notification. Either way, this is the final notification, so we want to
                        // handle the new row now!
                        InitializeDataSource();
                    }
                    break;

                    // An item has gone away.
                case ListChangedType.ItemDeleted:
                    InitializeDataSource();
                    break;

                    // An item has changed its index.
                case ListChangedType.ItemMoved:
                    InitializeDataSource();
                    break;

                    // Something has changed in the metadata.
                    // CHECK: When are these events actually fired?
                case ListChangedType.PropertyDescriptorAdded:
                case ListChangedType.PropertyDescriptorChanged:
                case ListChangedType.PropertyDescriptorDeleted:
                    InitializeDataSource();
                    break;
            }
        }

        // The CurrencyManager calls this if the data source looks
        // different. We just reload everything.
        // CHECK: Do we need this if we are handle ListChanged metadata events?
        protected virtual void currencyManager_MetaDataChanged(object sender, EventArgs e)
        {
            InitializeDataSource();
        }

        // Called by the CurrencyManager when the currently selected item
        // changes. We update the ListView selection so that we stay in sync
        // with any other controls bound to the same source.
        protected virtual void currencyManager_PositionChanged(object sender, EventArgs e)
        {
            int index = currencyManager.Position;

            // Make sure the index is sane (-1 pops up from time to time)
            if (index < 0 || index >= Items.Count)
                return;

            // Avoid recursion. If we are currently changing the index, don't
            // start the process again.
            if (isChangingIndex)
                return;

            try
            {
                isChangingIndex = true;

                // We can't use the index directly, since our listview may be sorted
                SelectedObject = currencyManager.List[index];

                // THINK: Do we always want to bring it into view?
                if (SelectedItems.Count > 0)
                    SelectedItems[0].EnsureVisible();
            }
            finally
            {
                isChangingIndex = false;
            }
        }

        /// <summary>
        /// Handle a SelectedIndexChanged event
        /// </summary>
        /// <param name="e">The event</param>
        /// <remarks>
        /// Called by Windows Forms when the currently selected index of the
        /// control changes. This usually happens because the user clicked on
        /// the control. In this case we want to notify the CurrencyManager so
        /// that any other bound controls will remain in sync. This method will
        /// also be called when we changed our index as a result of a
        /// notification that originated from the CurrencyManager, and in that
        /// case we avoid notifying the CurrencyManager back!
        /// </remarks>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            // Prevent recursion
            if (isChangingIndex)
                return;

            // If we are bound to a datasource, and only one item is selected,
            // tell the currency manager which item is selected.
            if (SelectedIndices.Count == 1 && currencyManager != null)
            {
                try
                {
                    isChangingIndex = true;

                    // We can't use the selectedIndex directly, since our listview may be sorted.
                    // So we have to find the index of the selected object within the original list.
                    currencyManager.Position = currencyManager.List.IndexOf(SelectedObject);
                }
                finally
                {
                    isChangingIndex = false;
                }
            }
        }

        #endregion
    }
}