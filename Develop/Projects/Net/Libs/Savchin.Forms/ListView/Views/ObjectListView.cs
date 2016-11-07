using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Savchin.WinApi;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// An object list displays 'aspects' of a collection of objects in a listview control.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The intelligence for this control is in the columns. OLVColumns are
    /// extended so they understand how to fetch an 'aspect' from each row
    /// object. They also understand how to sort by their aspect, and
    /// how to group them.
    /// </para>
    /// <para>
    /// Aspects are extracted by giving the name of a method to be called or a
    /// property to be fetched. These names can be simple names or they can be dotted
    /// to chain property access e.g. "Owner.Address.Postcode".
    /// Aspects can also be extracted by installing a delegate.
    /// </para>
    /// <para>
    /// Sorting by column clicking and grouping by column are handled automatically.
    /// </para>
    /// <para>
    /// Right clicking on the column header should present a popup menu that allows the user to
    /// choose which columns will be visible in the list. This behaviour can be disabled by
    /// setting SelectColumnsOnRightClick to false.
    /// </para>
    /// <para>
    /// This list puts sort indicators in the column headers to show the column sorting direction.
    /// On Windows XP and later, the system standard images are used.
    /// If you wish to replace the standard images with your own images, put entries in the small image list
    /// with the key values "sort-indicator-up" and "sort-indicator-down".
    /// </para>
    /// <para>
    /// For these classes to build correctly, the project must have references to these assemblies:
    /// <list>
    /// <item>System</item>
    /// <item>System.Data</item>
    /// <item>System.Design</item>
    /// <item>System.Drawing</item>
    /// <item>System.Windows.Forms (obviously)</item>
    /// </list>
    /// </para>
    /// </remarks>
    public partial class ObjectListView : System.Windows.Forms.ListView, ISupportInitialize
    {
        private bool isOwnerOfObjects; // does this ObjectListView own the Objects collection?
        private Rectangle lastUpdateRectangle; // remember the update rect from the last WM_PAINT message
        /// <summary>
        /// Sync object
        /// </summary>
        protected readonly object _syncRoot = new object();

        /// <summary>
        /// Create an ObjectListView
        /// </summary>
        public ObjectListView()
        {
            ColumnClick += HandleColumnClick;
            ItemCheck += HandleItemCheck;
            Layout += HandleLayout;
            ColumnWidthChanging += HandleColumnWidthChanging;
            ColumnWidthChanged += HandleColumnWidthChanged;

            base.View = View.Details;
            DoubleBuffered = true; // kill nasty flickers. hiss... me hates 'em
            AlternateRowBackColor = Color.Empty;
            ShowSortIndicators = true;
        }

        #region Public properties

        /// <summary>
        /// How does a user indicate that they want to edit cells?
        /// </summary>
        public enum CellEditActivateMode
        {
            /// <summary>
            /// This list cannot be edited. F2 does nothing.
            /// </summary>
            None = 0,

            /// <summary>
            /// A single click on  a <strong>subitem</strong> will edit the value. Single clicking the primary column,
            /// selects the row just like normal. The user must press F2 to edit the primary column.
            /// </summary>
            SingleClick = 1,

            /// <summary>
            /// Double clicking a subitem or the primary column will edit that cell.
            /// F2 will edit the primary column.
            /// </summary>
            DoubleClick = 2,

            /// <summary>
            /// Pressing F2 is the only way to edit the cells. Once the primary column is being edited,
            /// the other cells in the row can be edited by pressing Tab.
            /// </summary>
            F2Only = 3
        }

        private List<OLVColumn> allColumns = new List<OLVColumn>();

        private Color alternateRowBackColor = Color.Empty;
        private SortOrder alwaysGroupBySortOrder = SortOrder.None;
        private CellEditActivateMode cellEditActivation = CellEditActivateMode.None;
        private String emptyListMsg = "";
        private int freezeCount;
        private Color highlightBackgroundColor = Color.Empty;
        private Color highlightForegroundColor = Color.Empty;
        private bool isSearchOnSortColumn = true;
        private OLVColumn lastSortColumn;
        private SortOrder lastSortOrder;
        private IEnumerable objects;
        private int rowHeight = -1;
        private OLVColumn secondarySortColumn;
        private SortOrder secondarySortOrder = SortOrder.Ascending;
        private bool selectColumnsMenuStaysOpen = true;
        private bool selectColumnsOnRightClick = true;
        private ImageList shadowedImageList;
        private bool sortGroupItemsByPrimaryColumn = true;
        private bool updateSpaceFillingColumnsWhenDraggingColumnDivider = true;

        /// <summary>
        /// Get or set all the columns that this control knows about.
        /// Only those columns where IsVisible is true will be seen by the user.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you want to add new columns programmatically, add them to
        /// AllColumns and then call RebuildColumns(). Normally, you do not have to
        /// deal with this property directly. Just use the IDE.
        /// </para>
        /// <para>If you do add or remove columns from the AllColumns collection,
        /// you have to call RebuildColumns() to make those changes take effect.</para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual List<OLVColumn> AllColumns
        {
            get { return allColumns; }
            set
            {
                if (value == null)
                    allColumns = new List<OLVColumn>();
                else
                    allColumns = value;
            }
        }

        /// <summary>
        /// If every second row has a background different to the control, what color should it be?
        /// </summary>
        [Category("Appearance"),
         Description("If using alternate colors, what foregroundColor should alterate rows be?"),
         DefaultValue(typeof(Color), "")]
        public Color AlternateRowBackColor
        {
            get { return alternateRowBackColor; }
            set { alternateRowBackColor = value; }
        }

        /// <summary>
        /// Return the alternate row background color that has been set, or the default color
        /// </summary>
        [Browsable(false)]
        public virtual Color AlternateRowBackColorOrDefault
        {
            get
            {
                if (alternateRowBackColor == Color.Empty)
                    return Color.LemonChiffon;
                else
                    return alternateRowBackColor;
            }
        }

        /// <summary>
        /// This property forces the ObjectListView to always group items by the given column.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual OLVColumn AlwaysGroupByColumn { get; set; }

        /// <summary>
        /// If AlwaysGroupByColumn is not null, this property will be used to decide how
        /// those groups are sorted. If this property has the value SortOrder.None, then
        /// the sort order will toggle according to the users last header click.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortOrder AlwaysGroupBySortOrder
        {
            get { return alwaysGroupBySortOrder; }
            set { alwaysGroupBySortOrder = value; }
        }

        /// <summary>
        /// Give access to the image list that is actually being used by the control
        /// </summary>
        [Browsable(false)]
        public virtual ImageList BaseSmallImageList
        {
            get { return base.SmallImageList; }
        }

        /// <summary>
        /// How does the user indicate that they want to edit a cell?
        /// None means that the listview cannot be edited.
        /// </summary>
        /// <remarks>Columns can also be marked as editable.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("How does the user indicate that they want to edit a cell?"),
         DefaultValue(CellEditActivateMode.None)]
        public virtual CellEditActivateMode CellEditActivation
        {
            get { return cellEditActivation; }
            set { cellEditActivation = value; }
        }

        /// <summary>
        /// Should this list show checkboxes?
        /// </summary>
        public new bool CheckBoxes
        {
            get { return base.CheckBoxes; }
            set
            {
                base.CheckBoxes = value;
                // Initialize the state image list so we can display indetermined values.
                InitializeStateImageList();
            }
        }

        /// <summary>
        /// Return the model object of the row that is checked or null if no row is checked
        /// or more than one row is checked
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Object CheckedObject
        {
            get
            {
                IList checkedObjects = CheckedObjects;
                if (checkedObjects.Count == 1)
                    return checkedObjects[0];
                else
                    return null;
            }
            set { CheckedObjects = new ArrayList(new[] { value }); }
        }

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
        /// .NET's CheckedItems property is not helpful. It is just a short-hand for
        /// iterating through the list looking for items that are checked.
        /// </para>
        /// <para>
        /// The performance of the get method is O(n) and of the set method is
        /// O(n*m) where m is the number of objects being checked. Be careful on long lists.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IList CheckedObjects
        {
            get
            {
                var objects = new ArrayList();
                if (CheckBoxes)
                {
                    for (int i = 0; i < GetItemCount(); i++)
                    {
                        OLVListItem olvi = GetItem(i);
                        if (olvi.CheckState == CheckState.Checked)
                            objects.Add(olvi.RowObject);
                    }
                }
                return objects;
            }
            set
            {
                if (!CheckBoxes)
                    return;

                if (value == null)
                    value = new ArrayList();

                foreach (Object x in Objects)
                {
                    if (value.Contains(x))
                        SetObjectCheckedness(x, CheckState.Checked);
                    else
                        SetObjectCheckedness(x, CheckState.Unchecked);
                }
            }
        }

        /// <summary>
        /// Get/set the list of columns that should be used when the list switches to tile view.
        /// </summary>
        /// <remarks>If no list of columns has been installed, this value will default to the
        /// first column plus any column where IsTileViewColumn is true.</remarks>
        [Browsable(false),
         Obsolete("Use GetFilteredColumns() and OLVColumn.IsTileViewColumn instead"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<OLVColumn> ColumnsForTileView
        {
            get { return GetFilteredColumns(View.Tile); }
        }

        /// <summary>
        /// Return the visible columns in the order they are displayed to the user
        /// </summary>
        [Browsable(false)]
        public virtual List<OLVColumn> ColumnsInDisplayOrder
        {
            get
            {
                var columnsInDisplayOrder = new List<OLVColumn>(Columns.Count);
                for (int i = 0; i < Columns.Count; i++)
                    columnsInDisplayOrder.Add(null);
                for (int i = 0; i < Columns.Count; i++)
                {
                    OLVColumn col = GetColumn(i);
                    columnsInDisplayOrder[col.DisplayIndex] = col;
                }
                return columnsInDisplayOrder;
            }
        }

        /// <summary>
        /// If there are no items in this list view, what m should be drawn onto the control?
        /// </summary>
        [Category("Appearance"),
         Description("When the list has no items, show this m in the control"),
         DefaultValue("")]
        public virtual String EmptyListMsg
        {
            get { return emptyListMsg; }
            set
            {
                if (emptyListMsg != value)
                {
                    emptyListMsg = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// What font should the 'list empty' m be drawn in?
        /// </summary>
        [Category("Appearance"), Description("What font should the 'list empty' m be drawn in?"), DefaultValue(null)]
        public virtual Font EmptyListMsgFont { get; set; }

        /// <summary>
        /// Return the font for the 'list empty' m or a default
        /// </summary>
        [Browsable(false)]
        public virtual Font EmptyListMsgFontOrDefault
        {
            get
            {
                if (EmptyListMsgFont == null)
                    return new Font("Tahoma", 14);
                else
                    return EmptyListMsgFont;
            }
        }

        /// <summary>
        /// Get or set whether or not the listview is frozen. When the listview is
        /// frozen, it will not update itself.
        /// </summary>
        /// <remarks><para>The Frozen property is similar to the methods Freeze()/Unfreeze()
        /// except that changes to the Frozen property do not nest.</para></remarks>
        /// <example>objectListView1.Frozen = false; // unfreeze the control regardless of the number of Freeze() calls
        /// </example>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Frozen
        {
            get { return freezeCount > 0; }
            set
            {
                if (value)
                    Freeze();
                else if (freezeCount > 0)
                {
                    freezeCount = 1;
                    Unfreeze();
                }
            }
        }

        /// <summary>
        /// When a group title has an item count, how should the lable be formatted?
        /// </summary>
        /// <remarks>
        /// The given format string can/should have two placeholders:
        /// <list type="bullet">
        /// <item>{0} - the original group title</item>
        /// <item>{1} - the number of items in the group</item>
        /// </list>
        /// </remarks>
        /// <example>"{0} [{1} items]"</example>
        [Category("Behavior - ObjectListView"),
         Description("The format to use when suffixing item counts to group titles"), DefaultValue(null)]
        public virtual string GroupWithItemCountFormat { get; set; }

        /// <summary>
        /// Return this.GroupWithItemCountFormat or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual string GroupWithItemCountFormatOrDefault
        {
            get
            {
                if (String.IsNullOrEmpty(GroupWithItemCountFormat))
                    return "{0} [{1} items]";
                else
                    return GroupWithItemCountFormat;
            }
        }

        /// <summary>
        /// When a group title has an item count, how should the lable be formatted if
        /// there is only one item in the group?
        /// </summary>
        /// <remarks>
        /// The given format string can/should have two placeholders:
        /// <list type="bullet">
        /// <item>{0} - the original group title</item>
        /// <item>{1} - the number of items in the group (always 1)</item>
        /// </list>
        /// </remarks>
        /// <example>"{0} [{1} item]"</example>
        [Category("Behavior - ObjectListView"),
         Description("The format to use when suffixing item counts to group titles"), DefaultValue(null)]
        public virtual string GroupWithItemCountSingularFormat { get; set; }

        /// <summary>
        /// Return this.GroupWithItemCountSingularFormat or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual string GroupWithItemCountSingularFormatOrDefault
        {
            get
            {
                return String.IsNullOrEmpty(GroupWithItemCountSingularFormat) ?
                    "{0} [{1} item]" : GroupWithItemCountSingularFormat;
            }
        }

        /// <summary>
        /// Does this listview have a m that should be drawn when the list is empty?
        /// </summary>
        [Browsable(false)]
        public virtual bool HasEmptyListMsg
        {
            get { return !String.IsNullOrEmpty(EmptyListMsg); }
        }

        /// <summary>
        /// What color should be used for the background of selected rows?
        /// </summary>
        /// <remarks>Windows does not give the option of changing the selection background.
        /// So this color is only used when control is owner drawn and when columns have a
        /// renderer installed -- a basic new BaseRenderer() will suffice. The method
        /// EnableCustomSelectionColors() is a convenience method that does this.</remarks>
        [Category("Appearance"),
         Description("The background foregroundColor of selected rows when the control is owner drawn"),
         DefaultValue(typeof(Color), "")]
        public virtual Color HighlightBackgroundColor
        {
            get { return highlightBackgroundColor; }
            set { highlightBackgroundColor = value; }
        }

        /// <summary>
        /// Return the color should be used for the background of selected rows or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual Color HighlightBackgroundColorOrDefault
        {
            get
            {
                return HighlightBackgroundColor.IsEmpty ? SystemColors.Highlight : HighlightBackgroundColor;
            }
        }

        /// <summary>
        /// What color should be used for the foreground of selected rows?
        /// </summary>
        /// <remarks>Windows does not give the option of changing the selection foreground (text color).
        /// So this color is only used when control is owner drawn and when columns have a
        /// renderer installed -- a basic new BaseRenderer() will suffice. The method
        /// EnableCustomSelectionColors() is a convenience method that does this.</remarks>
        [Category("Appearance"),
         Description("The foreground foregroundColor of selected rows when the control is owner drawn"),
         DefaultValue(typeof(Color), "")]
        public virtual Color HighlightForegroundColor
        {
            get { return highlightForegroundColor; }
            set { highlightForegroundColor = value; }
        }

        /// <summary>
        /// Return the color should be used for the foreground of selected rows or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual Color HighlightForegroundColorOrDefault
        {
            get
            {
                return HighlightForegroundColor.IsEmpty ? SystemColors.HighlightText : HighlightForegroundColor;
            }
        }

        /// <summary>
        /// Return true if a cell edit operation is currently happening
        /// </summary>
        [Browsable(false)]
        public virtual bool IsCellEditing
        {
            get { return cellEditor != null; }
        }

        /// <summary>
        /// When the user types into a list, should the values in the current sort column be searched to find a match?
        /// If this is false, the primary column will always be used regardless of the sort column.
        /// </summary>
        /// <remarks>When this is true, the behavior is like that of ITunes.</remarks>
        [Category("Behavior - ObjectListView"),
         Description(
             "When the user types into a list, should the values in the current sort column be searched to find a match?"
             ),
         DefaultValue(true)]
        public virtual bool IsSearchOnSortColumn
        {
            get { return isSearchOnSortColumn; }
            set { isSearchOnSortColumn = value; }
        }

        /// <summary>
        /// Which column did we last sort by
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual OLVColumn LastSortColumn
        {
            get { return lastSortColumn; }
            set { lastSortColumn = value; }
        }

        /// <summary>
        /// Which direction did we last sort
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortOrder LastSortOrder
        {
            get { return lastSortOrder; }
            set { lastSortOrder = value; }
        }

        /// <summary>
        /// Get/set the collection of objects that this list will show
        /// </summary>
        /// <remarks>
        /// <para>
        /// The contents of the control will be updated immediately after setting this property.
        /// </para>
        /// <para>This method preserves selection, if possible. Use SetObjects() if
        /// you do not want to preserve the selection. Preserving selection is the slowest part of this
        /// code and performance is O(n) where n is the number of selected rows.</para>
        /// <para>This method is not thread safe.</para>
        /// <para>The property DOES work on virtual lists: setting is problem-free, but if you try to get it
        /// and the list has 10 million objects, it may take some time to return.</para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IEnumerable Objects
        {
            get
            {
                if (VirtualMode)
                {
                    var contents = new ArrayList(GetItemCount());
                    for (int i = 0; i < GetItemCount(); i++)
                        contents.Add(GetModelObject(i));
                    return contents;
                }
                else
                    return objects;
            }
            set
            {
                BeginUpdate();
                try
                {
                    IList previousSelection = SelectedObjects;
                    SetObjects(value);
                    SelectedObjects = previousSelection;
                }
                finally
                {
                    EndUpdate();
                }
            }
        }

        /// <summary>
        /// Specify the height of each row in the control in pixels.
        /// </summary>
        /// <remarks><para>The row height in a listview is normally determined by the font size and the small image list size.
        /// This setting allows that calculation to be overridden (within reason: you still cannot set the line height to be
        /// less than the line height of the font used in the control). </para>
        /// <para>Setting it to -1 means use the normal calculation method.</para>
        /// <para><bold>This feature is experiemental!</bold> Strange things may happen to your program,
        /// your spouse or your pet if you use it.</para>
        /// </remarks>
        [Category("Appearance"),
         Description("Specify the height of each row in pixels. -1 indicates default height"),
         DefaultValue(-1)]
        public virtual int RowHeight
        {
            get { return rowHeight; }
            set
            {
                rowHeight = value < 1 ? -1 : value;
                SetupExternalImageList();
            }
        }

        /// <summary>
        /// Get/set the column that will be used to resolve comparisons that are equal when sorting.
        /// </summary>
        /// <remarks>There is no user interface for this setting. It must be set programmatically.
        /// The default is the first column.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual OLVColumn SecondarySortColumn
        {
            get
            {
                if (secondarySortColumn == null)
                {
                    return Columns.Count > 0 ? GetColumn(0) : null;
                }
                else
                    return secondarySortColumn;
            }
            set { secondarySortColumn = value; }
        }

        /// <summary>
        /// When the SecondarySortColumn is used, in what order will it compare results?
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortOrder SecondarySortOrder
        {
            get { return secondarySortOrder; }
            set { secondarySortOrder = value; }
        }

        /// <summary>
        /// When the user right clicks on the column headers, should a menu be presented which will allow
        /// them to choose which columns will be shown in the view?
        /// </summary>
        [Category("Behavior - ObjectListView"),
         Description("When the user right clicks on the column headers, should a menu be presented which will allow them to choose which columns will be shown in the view?"),
         DefaultValue(true)]
        public virtual bool SelectColumnsOnRightClick
        {
            get { return selectColumnsOnRightClick; }
            set { selectColumnsOnRightClick = value; }
        }

        /// <summary>
        /// When the column select menu is open, should it stay open after an item is selected?
        /// Staying open allows the user to turn more than one column on or off at a time.
        /// </summary>
        [Category("Behavior - ObjectListView"),
         Description("When the column select menu is open, should it stay open after an item is selected?"),
         DefaultValue(true)]
        public virtual bool SelectColumnsMenuStaysOpen
        {
            get { return selectColumnsMenuStaysOpen; }
            set { selectColumnsMenuStaysOpen = value; }
        }

        /// <summary>
        /// Return the index of the row that is currently selected. If no row is selected,
        /// or more than one is selected, return -1.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectedIndex
        {
            get { return SelectedIndices.Count == 1 ? SelectedIndices[0] : -1; }
            set
            {
                SelectedIndices.Clear();
                if (value >= 0 && value < Items.Count)
                    SelectedIndices.Add(value);
            }
        }

        /// <summary>
        /// Get the ListViewItem that is currently selected . If no row is selected, or more than one is selected, return null.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ListViewItem SelectedItem
        {
            get { return SelectedIndices.Count == 1 ? GetItem(SelectedIndices[0]) : null; }
            set
            {
                SelectedIndices.Clear();
                if (value != null)
                    SelectedIndices.Add(value.Index);
            }
        }

        /// <summary>
        /// Get the model object from the currently selected row. If no row is selected, or more than one is selected, return null.
        /// Select the row that is displaying the given model object. All other rows are deselected.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Object SelectedObject
        {
            get { return GetSelectedObject(); }
            set { SelectObject(value); }
        }

        /// <summary>
        /// Get the model objects from the currently selected rows. If no row is selected, the returned List will be empty.
        /// When setting this value, select the rows that is displaying the given model objects. All other rows are deselected.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IList SelectedObjects
        {
            get { return GetSelectedObjects(); }
            set { SelectObjects(value); }
        }

        /// <summary>
        /// Should the list view show a bitmap in the column header to show the sort direction?
        /// </summary>
        /// <remarks>
        /// The only reason for not wanting to have sort indicators is that, on pre-XP versions of
        /// Windows, having sort indicators required the ListView to have a small image list, and
        /// as soon as you give a ListView a SmallImageList, the text of column 0 is bumped 16
        /// pixels to the right, even if you never used an image.
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("Should the list view show sort indicators in the column headers?"), DefaultValue(true)]
        public virtual bool ShowSortIndicators { get; set; }

        /// <summary>
        /// Should the list view show images on subitems?
        /// </summary>
        /// <remarks>
        /// <para>Under Windows, this works by sending messages to the underlying
        /// Windows control. To make this work under Mono, we would have to owner drawing the items :-(</para></remarks>
        [Category("Behavior - ObjectListView"), Description("Should the list view show images on subitems?"),
         DefaultValue(false)]
        public virtual bool ShowImagesOnSubItems { get; set; }

        /// <summary>
        /// This property controls whether group labels will be suffixed with a count of items.
        /// </summary>
        /// <remarks>
        /// The format of the suffix is controlled by GroupWithItemCountFormat/GroupWithItemCountSingularFormat properties
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("Will group titles be suffixed with a count of the items in the group?"), DefaultValue(false)]
        public virtual bool ShowItemCountOnGroups { get; set; }

        /// <summary>
        /// Override the SmallImageList property so we can correctly shadow its operations.
        /// </summary>
        /// <remarks><para>If you use the RowHeight property to specify the row height, the SmallImageList
        /// must be fully initialised before setting/changing the RowHeight. If you add new images to the image
        /// list after setting the RowHeight, you must assign the imagelist to the control again. Something as simple
        /// as this will work:
        /// <code>listView1.SmallImageList = listView1.SmallImageList;</code></para>
        /// </remarks>
        public new ImageList SmallImageList
        {
            get { return shadowedImageList; }
            set
            {
                shadowedImageList = value;
                SetupExternalImageList();
            }
        }

        /// <summary>
        /// When the listview is grouped, should the items be sorted by the primary column?
        /// If this is false, the items will be sorted by the same column as they are grouped.
        /// </summary>
        [Category("Behavior - ObjectListView"),
         Description(
             "When the listview is grouped, should the items be sorted by the primary column? If this is false, the items will be sorted by the same column as they are grouped."
             ),
         DefaultValue(true)]
        public virtual bool SortGroupItemsByPrimaryColumn
        {
            get { return sortGroupItemsByPrimaryColumn; }
            set { sortGroupItemsByPrimaryColumn = value; }
        }

        /// <summary>
        /// Get or set the index of the top item of this listview
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property only works when the listview is in Details view and not showing groups.
        /// </para>
        /// <para>
        /// The reason that it does not work when showing groups is that, when groups are enabled,
        /// the Windows m LVM_GETTOPINDEX always returns 0, regardless of the
        /// scroll position.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int TopItemIndex
        {
            get { return View == View.Details && TopItem != null ? TopItem.Index : -1; }
            set
            {
                int newTopIndex = Math.Min(value, GetItemCount() - 1);
                if (View == View.Details && newTopIndex >= 0)
                {
                    TopItem = Items[newTopIndex];

                    // Setting the TopItem sometimes gives off by one errors,
                    // that (bizarrely) are correct on a second attempt
                    if (TopItem != null && TopItem.Index != newTopIndex)
                        TopItem = GetItem(newTopIndex);
                }
            }
        }

        /// <summary>
        /// When resizing a column by dragging its divider, should any space filling columns be
        /// resized at each mouse move? If this is false, the filling columns will be
        /// updated when the mouse is released.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In previous versions, setting this to true produced ugly behaviour, because every
        /// column to the right of the divider being dragged was updated twice: once when
        /// the column be resized changes size (this moves all the columns slightly to the right);
        /// then again when the filling columns are updated - they are shrunk
        /// so that the combined width is not more than the control, so everything jumps slightly back to the left again.
        /// </para>
        /// <para>
        /// But, as of v2.0, the change the Windows messages in place, so there is now only one update,
        /// and everything looks nice and smooth.
        /// </para>
        /// <para>
        /// However, it still looks odd when the space filling column
        /// is in the left of the column that is being resized: the right edge of the column is dragged, but
        /// its <b>left</b> edge moves, since the space filling column is shrinking.
        /// </para>
        /// <para>Given the above behavior is probably best to turn this property off if your space filling
        /// columns aren't the right-most columns.</para>
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description(
             "When resizing a column by dragging its divider, should any space filling columns be resized at each mouse move?"
             ),
         DefaultValue(true)]
        public virtual bool UpdateSpaceFillingColumnsWhenDraggingColumnDivider
        {
            get { return updateSpaceFillingColumnsWhenDraggingColumnDivider; }
            set { updateSpaceFillingColumnsWhenDraggingColumnDivider = value; }
        }

        /// <summary>
        /// Should the list give a different background color to every second row?
        /// </summary>
        /// <remarks><para>The color of the alternate rows is given by AlternateRowBackColor.</para>
        /// <para>There is a "feature" in .NET for listviews in non-full-row-select mode, where
        /// selected rows are not drawn with their correct background color.</para></remarks>
        [Category("Appearance"), Description("Should the list view use a different backcolor to alternate rows?"),
         DefaultValue(false)]
        public virtual bool UseAlternatingBackColors { get; set; }

        /// <summary>
        /// Get/set the style of view that this listview is using
        /// </summary>
        /// <remarks>Switching to tile or details view installs the columns appropriate to that view.
        /// Confusingly, in tile view, every column is shown as a row of information.</remarks>
        public new View View
        {
            get { return base.View; }
            set
            {
                if (base.View == value)
                    return;

                if (Frozen)
                {
                    base.View = value;
                    return;
                }

                Freeze();

                // If we are switching to a Detail or Tile view, setup the columns needed for that view
                if (value == View.Details || value == View.Tile)
                {
                    ChangeToFilteredColumns(value);

                    if (value == View.Tile)
                        CalculateReasonableTileSize();
                }

                base.View = value;
                Unfreeze();
            }
        }

        #endregion

        #region Callbacks

        private Munger checkedAspectMunger;
        private string checkedAspectName;

        /// <summary>
        /// This delegate fetches the checkedness of an object as a boolean only.
        /// </summary>
        /// <remarks>Use this if you never want to worry about the
        /// Indeterminate state (which is fairly common).
        /// <para>
        /// This is a convenience wrapper around the CheckStateGetter property.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual BooleanCheckStateGetterDelegate BooleanCheckStateGetter
        {
            set
            {
                CheckStateGetter = value == null
                                       ? (CheckStateGetterDelegate)null
                                       : (x => value(x) ? CheckState.Checked : CheckState.Unchecked);
            }
        }

        /// <summary>
        /// This delegate sets the checkedness of an object as a boolean only. It must return
        /// true or false indicating if the object was checked or not.
        /// </summary>
        /// <remarks>Use this if you never want to worry about the
        /// Indeterminate state (which is fairly common).
        /// <para>
        /// This is a convenience wrapper around the CheckStatePutter property.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual BooleanCheckStatePutterDelegate BooleanCheckStatePutter
        {
            set
            {
                if (value == null)
                    CheckStatePutter = null;
                else
                    CheckStatePutter = delegate(Object x, CheckState state)
                                           {
                                               bool isChecked = (state == CheckState.Checked);
                                               return value(x, isChecked) ? CheckState.Checked : CheckState.Unchecked;
                                           };
            }
        }

        /// <summary>
        /// This delegate is called when the list wants to show a tooltip for a particular cell.
        /// The delegate should return the text to display, or null to use the default behavior
        /// (which is to show the full text of truncated cell values).
        /// </summary>
        /// <remarks>
        /// Displaying the full text of truncated cell values only work for FullRowSelect listviews.
        /// This is MS's behavior, not mine. Don't complain to me :)
        /// </remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CellToolTipGetterDelegate CellToolTipGetter { get; set; }

        /// <summary>
        /// The name of the property (or field) that holds whether or not a model is checked.
        /// </summary>
        /// <remarks>
        /// <para>The property must have a return type of bool and must be modifiable.</para>
        /// <para>Setting this property replaces any CheckStateGetter or CheckStatePutter that have been installed.
        /// Conversely, later setting the CheckStateGetter or CheckStatePutter properties will take precedence
        /// over the behavior of this property.</para>
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("The name of the property or field that holds the 'checkedness' of the model"),
         DefaultValue(null)]
        public virtual string CheckedAspectName
        {
            get { return checkedAspectName; }
            set
            {
                checkedAspectName = value;
                if (String.IsNullOrEmpty(checkedAspectName))
                {
                    CheckStateGetter = null;
                    CheckStatePutter = null;
                }
                else
                {
                    checkedAspectMunger = new Munger(checkedAspectName);
                    CheckStateGetter = delegate(Object modelObject)
                                           {
                                               var result = checkedAspectMunger.GetValue(modelObject) as bool?;
                                               if (result.HasValue && result.Value)
                                                   return CheckState.Checked;
                                               else
                                                   return CheckState.Unchecked;
                                           };
                    CheckStatePutter = delegate(Object modelObject, CheckState newValue)
                                           {
                                               checkedAspectMunger.PutValue(modelObject, newValue == CheckState.Checked);
                                               return CheckStateGetter(modelObject);
                                           };
                }
            }
        }

        /// <summary>
        /// This delegate will be called whenever the ObjectListView needs to know the check state
        /// of the row associated with a given model object.
        /// </summary>
        /// <remarks>
        /// <para>.NET has no support for indeterminate values, but as of v2.0, this class allows
        /// indeterminate values.</para>
        /// </remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CheckStateGetterDelegate CheckStateGetter { get; set; }

        /// <summary>
        /// This delegate will be called whenever the user tries to change the check state of a row.
        /// The delegate should return the state that was actually set, which may be different
        /// to the state given.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CheckStatePutterDelegate CheckStatePutter { get; set; }

        /// <summary>
        /// This delegate can be used to sort the table in a custom fasion.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The delegate must install a ListViewItemSorter on the ObjectListView.
        /// Installing the ItemSorter does the actual work of sorting the ListViewItems.
        /// See ColumnComparer in the code for an example of what an ItemSorter has to do.
        /// </para>
        /// <para>
        /// Do not install a CustomSorter on a VirtualObjectListView. Override the SortObjects()
        /// method of the IVirtualListDataSource instead.
        /// </para>
        /// </remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortDelegate CustomSorter { get; set; }

        /// <summary>
        /// This delegate is called when the list wants to show a tooltip for a particular header.
        /// The delegate should return the text to display, or null to use the default behavior
        /// (which is to not show any tooltip).
        /// </summary>
        /// <remarks>
        /// Installing a HeaderToolTipGetter takes precedence over any text in OLVColumn.ToolTipText.
        /// </remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HeaderToolTipGetterDelegate HeaderToolTipGetter { get; set; }

        /// <summary>
        /// This delegate can be used to format a OLVListItem before it is added to the control.
        /// </summary>
        /// <remarks>
        /// <para>The model object for the row can be found through the RowObject property of the OLVListItem object.</para>
        /// <para>All subitems normally have the same style as list item, so setting the forecolor on one
        /// subitem changes the forecolor of all subitems.
        /// To allow subitems to have different attributes, do this:<code>myListViewItem.UseItemStyleForSubItems = false;</code>.
        /// </para>
        /// <para>If UseAlternatingBackColors is true, the backcolor of the listitem will be calculated
        /// by the control and cannot be controlled by the RowFormatter delegate. In general, trying to use a RowFormatter
        /// when UseAlternatingBackColors is true does not work well.</para></remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual RowFormatterDelegate RowFormatter { get; set; }

        #endregion

        #region List commands

        /// <summary>
        /// Add the given model object to this control.
        /// </summary>
        /// <param name="modelObject">The model object to be displayed</param>
        /// <remarks>See AddObjects() for more details</remarks>
        public virtual void AddObject(object modelObject)
        {
            AddObjects(new[] { modelObject });
        }

        /// <summary>
        /// Add the given collection of model objects to this control.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        /// <remarks>
        /// <para>The added objects will appear in their correct sort position, if sorting
        /// is active (i.e. if LastSortColumn is not null). Otherwise, they will appear at the end of the list.</para>
        /// <para>No check is performed to see if any of the objects are already in the ListView.</para>
        /// <para>Null objects are silently ignored.</para>
        /// </remarks>
        public virtual void AddObjects(ICollection modelObjects)
        {
            if (modelObjects == null)
                return;

            BeginUpdate();
            try
            {
                // Give the world a chance to cancel or change the added objects
                var args = new ItemsAddingEventArgs(modelObjects);
                OnItemsAdding(args);
                if (args.Canceled)
                    return;
                modelObjects = args.ObjectsToAdd;

                TakeOwnershipOfObjects();
                var ourObjects = (ArrayList)Objects;
                var itemList = new List<OLVListItem>();
                foreach (object modelObject in modelObjects)
                {
                    if (modelObject != null)
                    {
                        ourObjects.Add(modelObject);
                        var lvi = new OLVListItem(modelObject);
                        FillInValues(lvi, modelObject);
                        itemList.Add(lvi);
                    }
                }
                ListViewItemSorter = null;
                Items.AddRange(itemList.ToArray());
                Sort(lastSortColumn, lastSortOrder);

                foreach (OLVListItem lvi in itemList)
                {
                    SetSubItemImages(lvi.Index, lvi);
                }

                // Tell the world that the list has changed
                OnItemsChanged(new ItemsChangedEventArgs());
            }
            finally
            {
                EndUpdate();
            }
        }

        /// <summary>
        /// Organise the view items into groups, based on the last sort column or the first column
        /// if there is no last sort column
        /// </summary>
        public virtual void BuildGroups()
        {
            BuildGroups(lastSortColumn);
        }

        /// <summary>
        /// Organise the view items into groups, based on the given column
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the AlwaysGroupByColumn property is not null,
        /// the list view items will be organisd by that column,
        /// and the 'column' parameter will be ignored.
        /// </para>
        /// <para>This method triggers sorting events: BeforeSorting and AfterSorting.</para>
        /// </remarks>
        /// <param name="column">The column whose values should be used for sorting.</param>
        public virtual void BuildGroups(OLVColumn column)
        {
            SortOrder order = lastSortOrder;
            if (order == SortOrder.None)
                order = Sorting;
            RationalizeColumnForGrouping(ref column, ref order);

            var args = new BeforeSortingEventArgs(column, order, SecondarySortColumn, SecondarySortOrder);
            OnBeforeSorting(args);
            if (args.Canceled)
                return;

            BuildGroups(args.ColumnToSort, args.SortOrder, args.SecondaryColumnToSort, args.SecondarySortOrder);

            OnAfterSorting(new AfterSortingEventArgs(args.ColumnToSort, args.SortOrder, args.SecondaryColumnToSort,
                                                     args.SecondarySortOrder));
        }

        /// <summary>
        /// Organise the view items into groups, based on the given columns
        /// </summary>
        /// <param name="column">The column whose values should be used for sorting. Cannot be null</param>
        /// <param name="order">The order in which the values from column will be sorted</param>
        /// <param name="secondaryColumn">When the values from 'column' are equal, use the values provided by this column</param>
        /// <param name="order">How will the secondary values be sorted</param>
        /// <remarks>This method does not trigger sorting events. Use BuildGroups() to do that</remarks>
        public virtual void BuildGroups(OLVColumn column, SortOrder order, OLVColumn secondaryColumn,
                                        SortOrder secondaryOrder)
        {
            // Sanity checks
            if (column == null || order == SortOrder.None)
                return;

            Groups.Clear();

            // Getting the Count forces any internal cache of the ListView to be flushed. Without
            // this, iterating over the Items will not work correctly if the ListView handle
            // has not yet been created.
            int dummy = Items.Count;

            // Separate the list view items into groups, using the group key as the descrimanent
            var map = new NullableDictionary<object, List<OLVListItem>>();
            foreach (OLVListItem olvi in Items)
            {
                object key = column.GetGroupKey(olvi.RowObject);
                //if (key == null)
                //    key = key; // null can't be used as the key for a dictionary
                if (!map.ContainsKey(key))
                    map[key] = new List<OLVListItem>();
                map[key].Add(olvi);
            }

            // Make a list of the required groups
            var groups = new List<ListViewGroup>();
            foreach (object key in map.Keys)
            {
                var lvg = new ListViewGroup(column.ConvertGroupKeyToTitle(key));
                lvg.Tag = key;
                groups.Add(lvg);
            }

            // Sort the groups
            groups.Sort(new ListViewGroupComparer(order));

            // Put each group into the list view, and give each group its member items.
            // The order of statements is important here:
            // - the header must be calculate before the group is added to the list view,
            //   otherwise changing the header causes a nasty redraw (even in the middle of a BeginUpdate...EndUpdate pair)
            // - the group must be added before it is given items, otherwise an exception is thrown (is this documented?)
            string fmt = column.GroupWithItemCountFormatOrDefault;
            string singularFmt = column.GroupWithItemCountSingularFormatOrDefault;
            var itemSorter = new ColumnComparer((SortGroupItemsByPrimaryColumn ? GetColumn(0) : column),
                                                order, secondaryColumn, secondaryOrder);
            foreach (ListViewGroup group in groups)
            {
                if (ShowItemCountOnGroups)
                {
                    int count = map[group.Tag].Count;
                    group.Header = String.Format((count == 1 ? singularFmt : fmt), group.Header, count);
                }
                Groups.Add(group);
                // If there is no sort order, don't sort since the sort isn't stable
                if (order != SortOrder.None)
                    map[group.Tag].Sort(itemSorter);
                group.Items.AddRange(map[group.Tag].ToArray());
            }
        }

        /// <summary>
        /// Build/rebuild all the list view items in the list
        /// </summary>
        public virtual void BuildList()
        {
            BuildList(true);
        }

        /// <summary>
        /// Build/rebuild all the list view items in the list
        /// </summary>
        /// <param name="shouldPreserveState">If this is true, the control will try to preserve the selection
        /// and the scroll position (see Remarks)
        /// </param>
        /// <remarks>
        /// <para>
        /// Use this method in situations were the contents of the list is basically the same
        /// as previously.
        /// </para>
        /// <para>
        /// Due to limitations in .NET's ListView, the scroll position is only preserved if
        /// the control is in Details view AND it is not showing groups.
        /// </para>
        /// </remarks>
        public virtual void BuildList(bool shouldPreserveState)
        {
            if (Frozen)
                return;

            int previousTopIndex = TopItemIndex;
            IList previousSelection = new ArrayList();
            if (shouldPreserveState && objects != null)
                previousSelection = SelectedObjects;

            BeginUpdate();
            try
            {
                Items.Clear();
                ListViewItemSorter = null;

                if (objects != null)
                {
                    // Build a list of all our items and then display them. (Building
                    // a list and then doing one AddRange is about 10-15% faster than individual adds)
                    var itemList = new List<OLVListItem>();
                    foreach (object rowObject in objects)
                    {
                        var lvi = new OLVListItem(rowObject);
                        FillInValues(lvi, rowObject);
                        itemList.Add(lvi);
                    }
                    Items.AddRange(itemList.ToArray());
                    SetAllSubItemImages();
                    Sort(lastSortColumn);

                    // If the list isn't sorted, we might need to setup the background colors here
                    if (LastSortColumn == null && UseAlternatingBackColors && View == View.Details)
                        PrepareAlternateBackColors();

                    if (shouldPreserveState)
                        SelectedObjects = previousSelection;
                }
            }
            finally
            {
                EndUpdate();
            }

            // We can only restore the scroll position after the EndUpdate() because
            // of caching that the ListView does internally during a BeginUpdate/EndUpdate pair.
            if (shouldPreserveState)
                TopItemIndex = previousTopIndex;
        }

        /// <summary>
        /// Give the listview a reasonable size of its tiles, based on the number of lines of
        /// information that each tile is going to display.
        /// </summary>
        public virtual void CalculateReasonableTileSize()
        {
            if (Columns.Count <= 0)
                return;

            int imageHeight = (LargeImageList == null ? 16 : LargeImageList.ImageSize.Height);
            int dataHeight = (Font.Height + 1) * Columns.Count;
            int tileWidth = (TileSize.Width == 0 ? 200 : TileSize.Width);
            int tileHeight = Math.Max(TileSize.Height, Math.Max(imageHeight, dataHeight));
            TileSize = new Size(tileWidth, tileHeight);
        }

        /// <summary>
        /// Rebuild this list for the given view
        /// </summary>
        /// <param name="view"></param>
        public virtual void ChangeToFilteredColumns(View view)
        {
            // Store the state
            IList previousSelection = SelectedObjects;
            int previousTopIndex = TopItemIndex;

            Freeze();
            Clear();
            List<OLVColumn> cols = GetFilteredColumns(view);
            if (view == View.Details)
            {
                // Where should each column be shown? We try to put it back where it last was,
                // but if that's not possible, it appears at the end of the columns
                foreach (OLVColumn x in cols)
                {
                    if (x.LastDisplayIndex == -1 || x.LastDisplayIndex > cols.Count - 1)
                        x.DisplayIndex = cols.Count - 1;
                    else
                        x.DisplayIndex = x.LastDisplayIndex;
                }
            }
            Columns.AddRange(cols.ToArray());
            if (view == View.Details)
                ShowSortIndicator();
            BuildList();
            Unfreeze();

            // Restore the state
            SelectedObjects = previousSelection;
            TopItemIndex = previousTopIndex;
        }

        /// <summary>
        /// Remove all items from this list
        /// </summary>
        /// <remark>This method can safely be called from background threads.</remark>
        public virtual void ClearObjects()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(ClearObjects));
            else
                SetObjects(null);
        }

        /// <summary>
        /// Copy a text and html representation of the selected rows onto the clipboard.
        /// </summary>
        /// <remarks>Be careful when using this with virtual lists. If the user has selected
        /// 10,000,000 rows, this method will faithfully try to copy all of them to the clipboard.
        /// From the user's point of view, your program will appear to have hung.</remarks>
        public virtual void CopySelectionToClipboard()
        {
            //THINK: Do we want to include something like this?
            //if (this.SelectedIndices.Count > 10000)
            //    return;

            CopyObjectsToClipboard(SelectedObjects);
        }

        /// <summary>
        /// Copy a text and html representation of the given objects onto the clipboard.
        /// </summary>
        public virtual void CopyObjectsToClipboard(IList objectsToCopy)
        {
            if (objectsToCopy.Count == 0)
                return;

            List<OLVColumn> columns = ColumnsInDisplayOrder;

            // Build text and html versions of the selection
            var sbText = new StringBuilder();
            var sbHtml = new StringBuilder("<table>");

            foreach (object modelObject in objectsToCopy)
            {
                sbHtml.Append("<tr><td>");
                foreach (OLVColumn col in columns)
                {
                    if (col != columns[0])
                    {
                        sbText.Append("\t");
                        sbHtml.Append("</td><td>");
                    }
                    string strValue = col.GetStringValue(modelObject);
                    sbText.Append(strValue);
                    sbHtml.Append(strValue); //TODO: Should encode the string value
                }
                sbText.AppendLine();
                sbHtml.AppendLine("</td></tr>");
            }
            sbHtml.AppendLine("</table>");

            // Put both the text and html versions onto the clipboard
            var dataObject = new DataObject();
            dataObject.SetText(sbText.ToString(), TextDataFormat.UnicodeText);
            dataObject.SetText(ConvertToHtmlFragment(sbHtml.ToString()), TextDataFormat.Html);
            Clipboard.SetDataObject(dataObject);
        }

        /// <summary>
        /// Convert the fragment of HTML into the Clipboards HTML format.
        /// </summary>
        /// <remarks>The HTML format is found here http://msdn2.microsoft.com/en-us/library/aa767917.aspx
        /// </remarks>
        /// <param name="fragment">The HTML to put onto the clipboard. It must be valid HTML!</param>
        /// <returns>A string that can be put onto the clipboard and will be recognized as HTML</returns>
        private string ConvertToHtmlFragment(string fragment)
        {
            // Minimal implementation of HTML clipboard format
            string source = "http://www.codeproject.com/KB/list/ObjectListView.aspx";

            const String MARKER_BLOCK =
                "Version:1.0\r\n" +
                "StartHTML:{0,8}\r\n" +
                "EndHTML:{1,8}\r\n" +
                "StartFragment:{2,8}\r\n" +
                "EndFragment:{3,8}\r\n" +
                "StartSelection:{2,8}\r\n" +
                "EndSelection:{3,8}\r\n" +
                "SourceURL:{4}\r\n" +
                "{5}";

            int prefixLength = String.Format(MARKER_BLOCK, 0, 0, 0, 0, source, "").Length;

            const String DEFAULT_HTML_BODY =
                "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">" +
                "<HTML><HEAD></HEAD><BODY><!--StartFragment-->{0}<!--EndFragment--></BODY></HTML>";

            string html = String.Format(DEFAULT_HTML_BODY, fragment);
            int startFragment = prefixLength + html.IndexOf(fragment);
            int endFragment = startFragment + fragment.Length;

            return String.Format(MARKER_BLOCK, prefixLength, prefixLength + html.Length, startFragment, endFragment,
                                 source, html);
        }

        /// <summary>
        /// Deselect all rows in the listview
        /// </summary>
        public virtual void DeselectAll()
        {
            NativeMethods.DeselectAllItems(this);
        }

        /// <summary>
        /// Setup the list so it will draw selected rows using custom colours.
        /// </summary>
        /// <remarks>
        /// This method makes the list owner drawn, and ensures that all columns have at
        /// least a BaseRender installed.
        /// </remarks>
        public virtual void EnableCustomSelectionColors()
        {
            OwnerDraw = true;

            foreach (OLVColumn column in AllColumns)
            {
                if (column.RendererDelegate == null)
                    column.Renderer = new BaseRenderer();
            }
        }

        /// <summary>
        /// Return the ListViewItem that appears immediately after the given item.
        /// If the given item is null, the first item in the list will be returned.
        /// Return null if the given item is the last item.
        /// </summary>
        /// <param name="itemToFind">The item that is before the item that is returned, or null</param>
        /// <returns>A ListViewItem</returns>
        public virtual ListViewItem GetNextItem(ListViewItem itemToFind)
        {
            if (ShowGroups)
            {
                bool isFound = (itemToFind == null);
                foreach (ListViewGroup group in Groups)
                {
                    foreach (ListViewItem lvi in group.Items)
                    {
                        if (isFound)
                            return lvi;
                        isFound = (lvi == itemToFind);
                    }
                }
                return null;
            }
            else
            {
                if (GetItemCount() == 0)
                    return null;
                if (itemToFind == null)
                    return GetItem(0);
                if (itemToFind.Index == GetItemCount() - 1)
                    return null;
                return GetItem(itemToFind.Index + 1);
            }
        }

        /// <summary>
        /// Return the n'th item (0-based) in the order they are shown to the user.
        /// If the control is not grouped, the display order is the same as the
        /// sorted list order. But if the list is grouped, the display order is different.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public virtual OLVListItem GetNthItemInDisplayOrder(int n)
        {
            if (!ShowGroups)
                return GetItem(n);

            foreach (ListViewGroup lgv in Groups)
            {
                if (n < lgv.Items.Count)
                    return (OLVListItem)lgv.Items[n];

                n -= lgv.Items.Count;
            }

            return null;
        }

        /// <summary>
        /// Return the index of the given ListViewItem as it currently shown to the user.
        /// If the control is not grouped, the display order is the same as the
        /// sorted list order. But if the list is grouped, the display order is different.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int GetItemIndexInDisplayOrder(ListViewItem value)
        {
            if (!ShowGroups)
                return value.Index;

            // TODO: This could be optimized
            int i = 0;
            foreach (ListViewGroup lvg in Groups)
            {
                foreach (ListViewItem lvi in lvg.Items)
                {
                    if (lvi == value)
                        return i;
                    i++;
                }
            }

            return -1;
        }

        /// <summary>
        /// Return the ListViewItem that appears immediately before the given item.
        /// If the given item is null, the last item in the list will be returned.
        /// Return null if the given item is the first item.
        /// </summary>
        /// <param name="itemToFind">The item that is before the item that is returned</param>
        /// <returns>A ListViewItem</returns>
        public virtual ListViewItem GetPreviousItem(ListViewItem itemToFind)
        {
            if (ShowGroups)
            {
                ListViewItem previousItem = null;
                foreach (ListViewGroup group in Groups)
                {
                    foreach (ListViewItem lvi in group.Items)
                    {
                        if (lvi == itemToFind)
                            return previousItem;
                        else
                            previousItem = lvi;
                    }
                }
                if (itemToFind == null)
                    return previousItem;
                else
                    return null;
            }
            else
            {
                if (GetItemCount() == 0)
                    return null;
                if (itemToFind == null)
                    return GetItem(GetItemCount() - 1);
                if (itemToFind.Index == 0)
                    return null;
                return GetItem(itemToFind.Index - 1);
            }
        }

        /// <summary>
        /// Update the list to reflect the contents of the given collection, without affecting
        /// the scrolling position, selection or sort order.
        /// </summary>
        /// <param name="collection">The objects to be displayed</param>
        /// <remarks>
        /// <para>This method is about twice as slow as SetObjects().</para>
        /// <para>This method is experimental -- it may disappear in later versions of the code.</para>
        /// <para>There has to be a better way to do this! JPP 15/1/2008</para>
        /// <para>In most situations, if you need this functionality, use a FastObjectListView instead. JPP 2/2/2008</para>
        /// </remarks>
        [Obsolete("Use a FastObjectListView instead of this method.", false)]
        public virtual void IncrementalUpdate(IEnumerable collection)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { IncrementalUpdate(collection); });
                return;
            }

            BeginUpdate();

            ListViewItemSorter = null;
            IList previousSelection = SelectedObjects;

            // Replace existing rows, creating new listviewitems if we get to the end of the list
            var newItems = new List<OLVListItem>();
            int rowIndex = 0;
            int itemCount = Items.Count;
            foreach (object model in collection)
            {
                if (rowIndex < itemCount)
                {
                    OLVListItem lvi = GetItem(rowIndex);
                    lvi.RowObject = model;
                    RefreshItem(lvi);
                }
                else
                {
                    var lvi = new OLVListItem(model);
                    FillInValues(lvi, model);
                    newItems.Add(lvi);
                }
                rowIndex++;
            }

            // Delete any excess rows
            int numRowsToDelete = itemCount - rowIndex;
            for (int i = 0; i < numRowsToDelete; i++)
                Items.RemoveAt(rowIndex);

            Items.AddRange(newItems.ToArray());
            Sort(lastSortColumn);

            SetAllSubItemImages();

            SelectedObjects = previousSelection;

            EndUpdate();

            objects = collection;
        }

        /// <summary>
        /// Pause (or unpause) all animations in the list
        /// </summary>
        /// <param name="isPause">true to pause, false to unpause</param>
        public virtual void PauseAnimations(bool isPause)
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                OLVColumn col = GetColumn(i);
                if (col.Renderer is ImageRenderer)
                    ((ImageRenderer)col.Renderer).Paused = isPause;
            }
        }

        /// <summary>
        /// Rebuild the columns based upon its current view and column visibility settings
        /// </summary>
        public virtual void RebuildColumns()
        {
            ChangeToFilteredColumns(View);
        }

        /// <summary>
        /// Remove the given model object from the ListView
        /// </summary>
        /// <param name="modelObject">The model to be removed</param>
        /// <remarks>See RemoveObjects() for more details</remarks>
        public virtual void RemoveObject(object modelObject)
        {
            RemoveObjects(new[] { modelObject });
        }

        /// <summary>
        /// Remove all of the given objects from the control
        /// </summary>
        /// <param name="modelObjects">Collection of objects to be removed</param>
        /// <remarks>
        /// <para>Nulls and model objects that are not in the ListView are silently ignored.</para>
        /// </remarks>
        public virtual void RemoveObjects(ICollection modelObjects)
        {
            if (modelObjects == null)
                return;

            BeginUpdate();
            try
            {
                // Give the world a chance to cancel or change the added objects
                var args = new ItemsRemovingEventArgs(modelObjects);
                OnItemsRemoving(args);
                if (args.Canceled)
                    return;
                modelObjects = args.ObjectsToRemove;

                TakeOwnershipOfObjects();
                var ourObjects = (ArrayList)Objects;
                foreach (object modelObject in modelObjects)
                {
                    if (modelObject != null)
                    {
                        ourObjects.Remove(modelObject);
                        int i = IndexOf(modelObject);
                        if (i >= 0)
                            Items.RemoveAt(i);
                    }
                }

                // Tell the world that the list has changed
                OnItemsChanged(new ItemsChangedEventArgs());
            }
            finally
            {
                EndUpdate();
            }
        }

        /// <summary>
        /// Select all rows in the listview
        /// </summary>
        public virtual void SelectAll()
        {
            NativeMethods.SelectAllItems(this);
        }

        /// <summary>
        /// Set the collection of objects that will be shown in this list view.
        /// </summary>
        /// <remark>This method can safely be called from background threads.</remark>
        /// <remarks>The list is updated immediately</remarks>
        /// <param name="collection">The objects to be displayed</param>
        public virtual void SetObjects(IEnumerable collection)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => SetObjects(collection)));
                return;
            }

            // Give the world a chance to cancel or change the assigned collection
            var args = new ItemsChangingEventArgs(objects, collection);
            OnItemsChanging(args);
            if (args.Canceled)
                return;
            collection = args.NewObjects;

            // If we own the current list and they change to another list, we don't own it anymore
            if (isOwnerOfObjects && objects != collection)
                isOwnerOfObjects = false;
            objects = collection;
            BuildList(false);

            // Tell the world that the list has changed
            OnItemsChanged(new ItemsChangedEventArgs());
        }

        /// <summary>
        /// Sort the items by the last sort column
        /// </summary>
        public new void Sort()
        {
            Sort(lastSortColumn);
        }

        #endregion

        #region Save/Restore State

        /// <summary>
        /// Return a byte array that represents the current state of the ObjectListView, such
        /// that the state can be restored by RestoreState()
        /// </summary>
        /// <remarks>
        /// <para>The state of an ObjectListView includes the attributes that the user can modify:
        /// <list>
        /// <item>current view (i.e. Details, Tile, Large Icon...)</item>
        /// <item>sort column and direction</item>
        /// <item>column order</item>
        /// <item>column widths</item>
        /// <item>column visibility</item>
        /// </list>
        /// </para>
        /// <para>
        /// It does not include selection or the scroll position.
        /// </para>
        /// </remarks>
        /// <returns>A byte array representing the state of the ObjectListView</returns>
        public virtual byte[] SaveState()
        {
            var olvState = new ObjectListViewState
                               {
                                   VersionNumber = 1,
                                   NumberOfColumns = AllColumns.Count,
                                   CurrentView = View
                               };

            // If we have a sort column, it is possible that it is not currently being shown, in which
            // case, it's Index will be -1. So we calculate its index directly. Technically, the sort
            // column does not even have to a member of AllColumns, in which case IndexOf will return -1,
            // which is works fine since we have no way of restoring such a column anyway.
            if (lastSortColumn != null)
                olvState.SortColumn = AllColumns.IndexOf(lastSortColumn);
            olvState.LastSortOrder = lastSortOrder;
            olvState.IsShowingGroups = ShowGroups;

            if (AllColumns.Count > 0 && AllColumns[0].LastDisplayIndex == -1)
                RememberDisplayIndicies();

            foreach (OLVColumn column in AllColumns)
            {
                olvState.ColumnIsVisible.Add(column.IsVisible);
                olvState.ColumnDisplayIndicies.Add(column.LastDisplayIndex);
                olvState.ColumnWidths.Add(column.Width);
            }

            // Now that we have stored our state, convert it to a byte array
            var ms = new MemoryStream();
            var serializer = new BinaryFormatter();
            serializer.Serialize(ms, olvState);

            return ms.ToArray();
        }

        /// <summary>
        /// Restore the state of the control from the given string, which must have been
        /// produced by SaveState()
        /// </summary>
        /// <param name="state">A byte array returned from SaveState()</param>
        /// <returns>Returns true if the state was restored</returns>
        public virtual bool RestoreState(byte[] state)
        {
            var ms = new MemoryStream(state);
            var deserializer = new BinaryFormatter();
            ObjectListViewState olvState;
            try
            {
                olvState = deserializer.Deserialize(ms) as ObjectListViewState;
            }
            catch (SerializationException)
            {
                return false;
            }

            // The number of columns has changed. We have no way to match old
            // columns to the new ones, so we just give up.
            if (olvState.NumberOfColumns != AllColumns.Count)
                return false;

            if (olvState.SortColumn == -1)
            {
                lastSortColumn = null;
                lastSortOrder = SortOrder.None;
            }
            else
            {
                lastSortColumn = AllColumns[olvState.SortColumn];
                lastSortOrder = olvState.LastSortOrder;
            }

            for (int i = 0; i < olvState.NumberOfColumns; i++)
            {
                OLVColumn column = AllColumns[i];
                column.Width = (int)olvState.ColumnWidths[i];
                column.IsVisible = (bool)olvState.ColumnIsVisible[i];
                column.LastDisplayIndex = (int)olvState.ColumnDisplayIndicies[i];
            }

            if (olvState.IsShowingGroups != ShowGroups)
                ShowGroups = olvState.IsShowingGroups;

            if (View == olvState.CurrentView)
                RebuildColumns();
            else
                View = olvState.CurrentView;

            return true;
        }

        /// <summary>
        /// Instances of this class are used to store the state of an ObjectListView.
        /// </summary>
        [Serializable]
        internal class ObjectListViewState
        {
            public ArrayList ColumnDisplayIndicies = new ArrayList();
            public ArrayList ColumnIsVisible = new ArrayList();
            public ArrayList ColumnWidths = new ArrayList();
            public View CurrentView;
            public bool IsShowingGroups;
            public SortOrder LastSortOrder = SortOrder.None;
            public int NumberOfColumns = 1;
            public int SortColumn = -1;
            public int VersionNumber = 1;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for the column click event
        /// </summary>
        protected virtual void HandleColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!PossibleFinishCellEditing())
                return;

            // Toggle the sorting direction on successive clicks on the same column
            if (lastSortColumn != null && e.Column == lastSortColumn.Index)
                lastSortOrder = (lastSortOrder == SortOrder.Descending ? SortOrder.Ascending : SortOrder.Descending);
            else
                lastSortOrder = SortOrder.Ascending;

            BeginUpdate();
            try
            {
                Sort(e.Column);
            }
            finally
            {
                EndUpdate();
            }
        }

        /// <summary>
        /// Handle when a user checks/unchecks a row
        /// </summary>
        protected virtual void HandleItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!Created) // || this.CheckStatePutter == null) THINK do we need this
                return;

            Object modelObject = GetModelObject(e.Index);
            OLVListItem olvi = ModelToItem(modelObject);
            if (olvi != null && olvi.CheckState != e.NewValue)
                e.NewValue = PutCheckState(modelObject, e.NewValue);
        }

        #endregion

        #region Low level Windows Message handling

        private string lastSearchString;
        private int timeLastCharEvent;

        /// <summary>
        /// Override the basic m pump for this control
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0F: // WM_PAINT
                    HandlePrePaint();
                    base.WndProc(ref m);
                    HandlePostPaint();
                    break;
                case 0x46: // WM_WINDOWPOSCHANGING
                    if (!HandleWindowPosChanging(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x4E: // WM_NOTIFY
                    if (!HandleNotify(ref m))
                        base.WndProc(ref m);
                    break;
                //case 0x100:
                //case 0x101:
                //case 0x102:
                //case 260:
                //case 0x105:

                case 0x0102: // WM_CHAR
                    if (!HandleChar(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x204E: // WM_REFLECT_NOTIFY
                    if (!HandleReflectNotify(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x114: // WM_HSCROLL:
                case 0x115: // WM_VSCROLL:
                case 0x201: // WM_LBUTTONDOWN:
                case 0x20A: // WM_MOUSEWHEEL:
                case 0x20E: // WM_MOUSEHWHEEL:
                    if (PossibleFinishCellEditing())
                        base.WndProc(ref m);
                    break;
                case 0x7B: // WM_CONTEXTMENU
                    if (!HandleContextMenu(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x1053: // LVM_FINDITEM = (LVM_FIRST + 83)
                    if (!HandleFindItem(ref m))
                        base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// The user wants to see the context menu.
        /// </summary>
        /// <param name="m">The windows m</param>
        /// <returns>A bool indicating if this m has been handled</returns>
        /// <remarks>
        /// We want to ignore context menu requests that are triggered by right clicks on the header
        /// </remarks>
        protected virtual bool HandleContextMenu(ref Message m)
        {
            // Don't try to handle context menu commands at design time.
            if (DesignMode)
                return false;

            // If the context menu command was generated by the keyboard, LParam will be -1.
            // We don't want to process these.
            if (((int)m.LParam) == -1)
                return false;

            // If the context menu came from somewhere other than the header control,
            // we also don't want to ignore it
            if (m.WParam != hdrCtrl.Handle)
                return false;

            // OK. Looks like a right click in the header
            if (!PossibleFinishCellEditing())
                return true;

            int columnIndex = hdrCtrl.ColumnIndexUnderCursor;
            return HandleHeaderRightClick(columnIndex);
        }

        /// <summary>
        /// Handle the search for item m if possible.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleChar(ref Message m)
        {
            const int MILLISECONDS_BETWEEN_KEYPRESSES = 1000;

            // What character did the user type and was it part of a longer string?
            var character = (char)m.WParam; //TODO: Will this work on 64 bit or MBCS?
            if (character == 8)
            {
                // Backspace forces the next key to be considered the start of a new search
                timeLastCharEvent = 0;
                return true;
            }

            if (Environment.TickCount < (timeLastCharEvent + MILLISECONDS_BETWEEN_KEYPRESSES))
                lastSearchString += character;
            else
                lastSearchString = character.ToString();

            // Where should the search start?
            int start = 0;
            ListViewItem focused = FocusedItem;
            if (focused != null)
            {
                start = GetItemIndexInDisplayOrder(focused);

                // If the user presses a single key, we search from after the focused item,
                // being careful not to march past the end of the list
                if (lastSearchString.Length == 1)
                {
                    start += 1;
                    if (start == GetItemCount())
                        start = 0;
                }
            }

            // Give the world a chance to fiddle with or completely avoid the searching process
            var args = new BeforeSearchingEventArgs(lastSearchString, start);
            OnBeforeSearching(args);
            if (args.Canceled)
                return true;

            // The parameters of the search may have been changed
            lastSearchString = args.StringToFind;
            start = args.StartSearchFrom;

            // Do the actual search
            int found = FindMatchingRow(lastSearchString, start, SearchDirectionHint.Down);
            if (found < 0)
                SystemSounds.Beep.Play();
            else
            {
                // Select and focus on the found item
                BeginUpdate();
                try
                {
                    SelectedIndices.Clear();
                    ListViewItem lvi = GetNthItemInDisplayOrder(found);
                    lvi.Selected = true;
                    lvi.Focused = true;
                    EnsureVisible(lvi.Index);
                }
                finally
                {
                    EndUpdate();
                }
            }

            // Tell the world that a search has occurred
            var args2 = new AfterSearchingEventArgs(lastSearchString, found);
            OnAfterSearching(args2);

            // When did this event occur?
            timeLastCharEvent = Environment.TickCount;
            return true;
        }

        /// <summary>
        /// Handle the search for item m if possible.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleFindItem(ref Message m)
        {
            // NOTE: As far as I can see, this message is never actually sent to the control, making this
            // method redundant!

            const int LVFI_STRING = 0x0002;

            var findInfo = (NativeMethods.LVFINDINFO)m.GetLParam(typeof(NativeMethods.LVFINDINFO));

            // We can only handle string searches
            if ((findInfo.flags & LVFI_STRING) != LVFI_STRING)
                return false;

            int start = m.WParam.ToInt32();
            m.Result = (IntPtr)FindMatchingRow(findInfo.psz, start, SearchDirectionHint.Down);
            return true;
        }

        /// <summary>
        /// Find the first row after the given start in which the text value in the
        /// comparison column begins with the given text. The comparison column is column 0,
        /// unless IsSearchOnSortColumn is true, in which case the current sort column is used.
        /// </summary>
        /// <param name="text">The text to be prefix matched</param>
        /// <param name="start">The index of the first row to consider</param>
        /// <param name="direction">Which direction should be searched?</param>
        /// <returns>The index of the first row that matched, or -1</returns>
        /// <remarks>The text comparison is a case-insensitive, prefix match. The search will
        /// search the every row until a match is found, wrapping at the end if needed.</remarks>
        public virtual int FindMatchingRow(string text, int start, SearchDirectionHint direction)
        {
            // We also can't do anything if we don't have data
            int rowCount = GetItemCount();
            if (rowCount == 0)
                return -1;

            // Which column are we going to use for our comparing?
            OLVColumn column = GetColumn(0);
            if (IsSearchOnSortColumn && View == View.Details && LastSortColumn != null)
                column = LastSortColumn;

            // Do two searches if necessary to find a match. The second search is the wrap-around part of searching
            int i;
            if (direction == SearchDirectionHint.Down)
            {
                i = FindMatchInRange(text, start, rowCount - 1, column);
                if (i == -1 && start > 0)
                    i = FindMatchInRange(text, 0, start - 1, column);
            }
            else
            {
                i = FindMatchInRange(text, start, 0, column);
                if (i == -1 && start != rowCount)
                    i = FindMatchInRange(text, rowCount - 1, start + 1, column);
            }

            return i;
        }

        /// <summary>
        /// Find the first row in the given range of rows that prefix matches the string value of the given column.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="column"></param>
        /// <returns>The index of the matched row, or -1</returns>
        protected virtual int FindMatchInRange(string text, int first, int last, OLVColumn column)
        {
            if (first <= last)
            {
                for (int i = first; i <= last; i++)
                {
                    string data = column.GetStringValue(GetNthItemInDisplayOrder(i).RowObject);
                    if (data.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            }
            else
            {
                for (int i = first; i >= last; i--)
                {
                    string data = column.GetStringValue(GetNthItemInDisplayOrder(i).RowObject);
                    if (data.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// In the notification messages, we handle attempts to change the width of our columns
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected unsafe bool HandleReflectNotify(ref Message m)
        {
            const int LVN_ITEMCHANGED = -101;
            const int LVN_ITEMCHANGING = -100;
            const int LVIF_STATE = 8;

            bool isMsgHandled = false;
            var nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));

            switch (nmhdr.code)
            {
                case LVN_ITEMCHANGED:
                    var nmlistviewPtr2 = (NativeMethods.NMLISTVIEW*)m.LParam;
                    if ((nmlistviewPtr2->uChanged & LVIF_STATE) != 0)
                    {
                        CheckState currentValue = CalculateState(nmlistviewPtr2->uOldState);
                        CheckState newCheckValue = CalculateState(nmlistviewPtr2->uNewState);
                        if (currentValue != newCheckValue)
                        {
                            var args4 = new ItemCheckedEventArgs(Items[nmlistviewPtr2->iItem]);
                            OnItemChecked(args4);
                            // Remove the state indicies so that we don't trigger the OnItemChecked method
                            // when we call our base method after exiting this method
                            nmlistviewPtr2->uOldState = (nmlistviewPtr2->uOldState & 0x0FFF);
                            nmlistviewPtr2->uNewState = (nmlistviewPtr2->uNewState & 0x0FFF);
                        }
                    }
                    break;

                case LVN_ITEMCHANGING:
                    // Change the CheckBox handling to cope with indeterminate state
                    var nmlistviewPtr = (NativeMethods.NMLISTVIEW*)m.LParam;
                    if ((nmlistviewPtr->uChanged & LVIF_STATE) != 0)
                    {
                        CheckState currentValue = CalculateState(nmlistviewPtr->uOldState);
                        CheckState newCheckValue = CalculateState(nmlistviewPtr->uNewState);
                        if (currentValue != newCheckValue)
                        {
                            var ice = new ItemCheckEventArgs(nmlistviewPtr->iItem, newCheckValue, currentValue);
                            OnItemCheck(ice);
                            if (ice.NewValue == currentValue)
                                m.Result = (IntPtr)1;
                            else
                            {
                                m.Result = IntPtr.Zero;
                                // Within this m, we cannot change to any other value but the expected one
                                // So if we need to switch to another value, we use BeginInvole to change to the value
                                // we want just after this m completes.
                                if (ice.NewValue != newCheckValue)
                                {
                                    OLVListItem olvItem = GetItem(nmlistviewPtr->iItem);
                                    BeginInvoke((MethodInvoker)delegate { olvItem.CheckState = ice.NewValue; });
                                }
                            }
                            //isMsgHandled = true;
                        }
                        // Prevent the base method from seeing the state change, since we've already handled it
                        nmlistviewPtr->uChanged &= ~LVIF_STATE;
                    }
                    break;

                default:
                    break;
            }

            return isMsgHandled;
        }

        private CheckState CalculateState(int state)
        {
            switch ((state & 0xf000) >> 12)
            {
                case 1:
                    return CheckState.Unchecked;
                case 2:
                    return CheckState.Checked;
                case 3:
                    return CheckState.Indeterminate;
                default:
                    return CheckState.Checked;
            }
        }

        /// <summary>
        /// In the notification messages, we handle attempts to change the width of our columns
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected unsafe bool HandleNotify(ref Message m)
        {
            bool isMsgHandled = false;

            const int HDN_FIRST = (0 - 300);
            const int HDN_ITEMCHANGINGA = (HDN_FIRST - 0);
            const int HDN_ITEMCHANGINGW = (HDN_FIRST - 20);
            const int HDN_ITEMCLICKA = (HDN_FIRST - 2);
            const int HDN_ITEMCLICKW = (HDN_FIRST - 22);
            const int HDN_DIVIDERDBLCLICKA = (HDN_FIRST - 5);
            const int HDN_DIVIDERDBLCLICKW = (HDN_FIRST - 25);
            const int HDN_BEGINTRACKA = (HDN_FIRST - 6);
            const int HDN_BEGINTRACKW = (HDN_FIRST - 26);
            //const int HDN_ENDTRACKA = (HDN_FIRST - 7);
            //const int HDN_ENDTRACKW = (HDN_FIRST - 27);
            const int HDN_TRACKA = (HDN_FIRST - 8);
            const int HDN_TRACKW = (HDN_FIRST - 28);

            const int TTN_GETDISPINFO = -530;

            // Handle the notification, remembering to handle both ANSI and Unicode versions
            var nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
            //if (nmhdr.code < HDN_FIRST)
            //    System.Diagnostics.Debug.WriteLine(nmhdr.code);

            // In KB Article #183258, MS states that when a header control has the HDS_FULLDRAG style, it will receive
            // ITEMCHANGING events rather than TRACK events. Under XP SP2 (at least) this is not always true, which may be
            // why MS has withdrawn that particular KB article. It is true that the header is always given the HDS_FULLDRAG
            // style. But even while window style set, the control doesn't always received ITEMCHANGING events.
            // The controlling setting seems to be the Explorer option "Show Window Contents While Dragging"!
            // In the category of "truly bizarre side effects", if the this option is turned on, we will receive
            // ITEMCHANGING events instead of TRACK events. But if it is turned off, we receive lots of TRACK events and
            // only one ITEMCHANGING event at the very end of the process.
            // If we receive HDN_TRACK messages, it's harder to control the resizing process. If we return a result of 1, we
            // cancel the whole drag operation, not just that particular track event, which is clearly not what we want.
            // If we are willing to compile with unsafe code enabled, we can modify the size of the column in place, using the
            // commented out code below. But without unsafe code, the best we can do is allow the user to drag the column to
            // any width, and then spring it back to within bounds once they release the mouse button. UI-wise it's very ugly.
            NativeMethods.NMHEADER nmheader;
            switch (nmhdr.code)
            {
                case HDN_ITEMCLICKA:
                case HDN_ITEMCLICKW:
                    if (!PossibleFinishCellEditing())
                    {
                        m.Result = (IntPtr)1; // prevent the change from happening
                        isMsgHandled = true;
                    }
                    break;

                case HDN_DIVIDERDBLCLICKA:
                case HDN_DIVIDERDBLCLICKW:
                case HDN_BEGINTRACKA:
                case HDN_BEGINTRACKW:
                    if (!PossibleFinishCellEditing())
                    {
                        m.Result = (IntPtr)1; // prevent the change from happening
                        isMsgHandled = true;
                        break;
                    }
                    nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
                    if (nmheader.iItem >= 0 && nmheader.iItem < Columns.Count)
                    {
                        OLVColumn column = GetColumn(nmheader.iItem);
                        // Space filling columns can't be dragged or double-click resized
                        if (column.FillsFreeSpace)
                        {
                            m.Result = (IntPtr)1; // prevent the change from happening
                            isMsgHandled = true;
                        }
                    }
                    break;

                case HDN_TRACKA:
                case HDN_TRACKW:
                    nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
                    if (nmheader.iItem >= 0 && nmheader.iItem < Columns.Count)
                    {
                        var hditem = (NativeMethods.HDITEM*)nmheader.pHDITEM;
                        OLVColumn column = GetColumn(nmheader.iItem);
                        if (hditem->cxy < column.MinimumWidth)
                            hditem->cxy = column.MinimumWidth;
                        else if (column.MaximumWidth != -1 && hditem->cxy > column.MaximumWidth)
                            hditem->cxy = column.MaximumWidth;
                    }
                    break;

                case HDN_ITEMCHANGINGA:
                case HDN_ITEMCHANGINGW:
                    nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
                    if (nmheader.iItem >= 0 && nmheader.iItem < Columns.Count)
                    {
                        var hditem =
                            (NativeMethods.HDITEM)
                            Marshal.PtrToStructure(nmheader.pHDITEM, typeof(NativeMethods.HDITEM));
                        OLVColumn column = GetColumn(nmheader.iItem);
                        // Check the mask to see if the width field is valid, and if it is, make sure it's within range
                        if ((hditem.mask & 1) == 1)
                        {
                            if (hditem.cxy < column.MinimumWidth ||
                                (column.MaximumWidth != -1 && hditem.cxy > column.MaximumWidth))
                            {
                                m.Result = (IntPtr)1; // prevent the change from happening
                                isMsgHandled = true;
                            }
                        }
                    }
                    break;

                case TTN_GETDISPINFO:
                    ListViewHitTestInfo info = HitTest(PointToClient(Cursor.Position));
                    if (info.Item != null && info.SubItem != null)
                    {
                        int columnIndex = info.Item.SubItems.IndexOf(info.SubItem);
                        String tip = GetCellToolTip(columnIndex, info.Item.Index);
                        if (!String.IsNullOrEmpty(tip))
                        {
                            // HeaderControl has almost identical code. Is there some way to unify?
                            NativeMethods.SendMessage(nmhdr.hwndFrom, 0x418, 0,
                                                      SystemInformation.MaxWindowTrackSize.Width);
                            var ttt = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
                            ttt.lpszText = tip;
                            if (RightToLeft == RightToLeft.Yes)
                                ttt.uFlags |= 4;
                            Marshal.StructureToPtr(ttt, m.LParam, false);
                            isMsgHandled = true;
                        }
                    }
                    break;

                default:
                    break;
            }

            return isMsgHandled;
        }

        /// <summary>
        /// Perform any steps needed before painting the control
        /// </summary>
        protected virtual void HandlePrePaint()
        {
            // When we get a WM_PAINT m, remember the rectangle that is being updated.
            // We can't get this information later, since the BeginPaint call wipes it out.
            lastUpdateRectangle = NativeMethods.GetUpdateRect(this);

            // When the list is empty, we want to handle the drawing of the control by ourselves.
            // Unfortunately, there is no easy way to tell our superclass that we want to do this.
            // So we resort to guile and deception. We validate the list area of the control, which
            // effectively tells our superclass that this area does not need to be painted.
            // Our superclass will then not paint the control, leaving us free to do so ourselves.
            // Without doing this trickery, the superclass will draw the
            // list as empty, and then moments later, we will draw the empty m, giving a nasty flicker
            if (GetItemCount() == 0 && HasEmptyListMsg)
                NativeMethods.ValidateRect(this, ClientRectangle);
        }

        /// <summary>
        /// Perform any steps needed after painting the control
        /// </summary>
        protected virtual void HandlePostPaint()
        {
            // If the list isn't empty or there isn't an emptyList m, do nothing
            if (GetItemCount() != 0 || !HasEmptyListMsg)
                return;

            // Draw the empty list m centered in the client area of the control
            using (
                BufferedGraphics buffered = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), ClientRectangle))
            {
                Graphics g = buffered.Graphics;
                g.Clear(BackColor);
                var sf = new StringFormat
                             {
                                 Alignment = StringAlignment.Center,
                                 LineAlignment = StringAlignment.Center,
                                 Trimming = StringTrimming.EllipsisCharacter
                             };
                g.DrawString(EmptyListMsg, EmptyListMsgFontOrDefault, SystemBrushes.ControlDark, ClientRectangle, sf);
                buffered.Render();
            }
        }

        /// <summary>
        /// Handle the window position changing.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleWindowPosChanging(ref Message m)
        {
            const int SWP_NOSIZE = 1;

            var pos = (NativeMethods.WINDOWPOS)m.GetLParam(typeof(NativeMethods.WINDOWPOS));
            if ((pos.flags & SWP_NOSIZE) == 0)
            {
                if (pos.cx < Bounds.Width) // only when shrinking
                    // pos.cx is the window width, not the client area width, so we have to subtract the border widths
                    ResizeFreeSpaceFillingColumns(pos.cx - (Bounds.Width - ClientSize.Width));
            }

            return false;
        }

        #endregion

        #region Column header clicking, column hiding and resizing

        private HeaderControl hdrCtrl;

        /// <summary>
        /// When the control is created capture the messages for the header.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BeginInvoke(new MethodInvoker(CreateHeaderControl));
        }

        protected void CreateHeaderControl()
        {
#if !MONO
            hdrCtrl = new HeaderControl(this);
#endif
        }

#if !MONO
#endif

        /// <summary>
        /// The user has right clicked on the column headers. Do whatever is required
        /// </summary>
        /// <returns>Return true if this event has been handle</returns>
        protected virtual bool HandleHeaderRightClick(int columnIndex)
        {
            var eventArgs = new ColumnClickEventArgs(columnIndex);
            OnColumnRightClick(eventArgs);

            if (SelectColumnsOnRightClick)
                ShowColumnSelectMenu(Cursor.Position);

            return SelectColumnsOnRightClick;
        }

        /// <summary>
        /// The user has right clicked on the column headers. Do whatever is required
        /// </summary>
        /// <returns>Return true if this event has been handle</returns>
        [Obsolete("Use HandleHeaderRightClick(int) instead")]
        protected virtual bool HandleHeaderRightClick()
        {
            return false;
        }

        /// <summary>
        /// Show a popup menu at the given point which will allow the user to choose which columns
        /// are visible on this listview
        /// </summary>
        /// <param name="pt">Where should the menu be placed</param>
        protected virtual void ShowColumnSelectMenu(Point pt)
        {
            ToolStripDropDown m = MakeColumnSelectMenu(new ContextMenuStrip());
            m.Show(pt);
        }

        /// <summary>
        /// Append the column selection menu items to the given menu strip.
        /// </summary>
        /// <param name="strip">The menu to which the items will be added.</param>
        /// <returns>Return the menu to which the items were added</returns>
        public virtual ToolStripDropDown MakeColumnSelectMenu(ToolStripDropDown strip)
        {
            strip.ItemClicked += ColumnSelectMenu_ItemClicked;
            strip.Closing += ColumnSelectMenu_Closing;

            var columns = new List<OLVColumn>(AllColumns);
            // Sort columns alphabetically
            //columns.Sort(delegate(OLVColumn x, OLVColumn y) { return String.Compare(x.Text, y.Text, true); });

            // Sort columns by display order
            if (AllColumns.Count > 0 && AllColumns[0].LastDisplayIndex == -1)
                RememberDisplayIndicies();
            columns.Sort((x, y) => (x.LastDisplayIndex - y.LastDisplayIndex));

            // Build menu from sorted columns
            foreach (OLVColumn col in columns)
            {
                var mi = new ToolStripMenuItem(col.Text);
                mi.Checked = col.IsVisible;
                mi.Tag = col;

                // The 'Index' property returns -1 when the column is not visible, so if the
                // column isn't visible we have to enable the item. Also the first column can't be turned off
                mi.Enabled = !col.IsVisible || (col.Index > 0);
                strip.Items.Add(mi);
            }

            return strip;
        }

        private void ColumnSelectMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var mi = (ToolStripMenuItem)e.ClickedItem;
            var col = (OLVColumn)mi.Tag;
            mi.Checked = !mi.Checked;
            col.IsVisible = mi.Checked;
            BeginInvoke(new MethodInvoker(RebuildColumns));
        }

        private void ColumnSelectMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = (SelectColumnsMenuStaysOpen &&
                        e.CloseReason == ToolStripDropDownCloseReason.ItemClicked);
        }

        /// <summary>
        /// Override the OnColumnReordered method to do what we want
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnReordered(ColumnReorderedEventArgs e)
        {
            base.OnColumnReordered(e);

            // The internal logic of the .NET code behind a ENDDRAG event means that,
            // at this point, the DisplayIndex's of the columns are not yet as they are
            // going to be. So we have to invoke a method to run later that will remember
            // what the real DisplayIndex's are.
            BeginInvoke(new MethodInvoker(RememberDisplayIndicies));
        }

        private void RememberDisplayIndicies()
        {
            // Remember the display indexes so we can put them back at a later date
            foreach (OLVColumn x in AllColumns)
                x.LastDisplayIndex = x.DisplayIndex;
        }

        /// <summary>
        /// When the column widths are changing, resize the space filling columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (UpdateSpaceFillingColumnsWhenDraggingColumnDivider && !GetColumn(e.ColumnIndex).FillsFreeSpace)
            {
                // If the width of a column is increasing, resize any space filling columns allowing the extra
                // space that the new column width is going to consume
                int oldWidth = GetColumn(e.ColumnIndex).Width;
                if (e.NewWidth > oldWidth)
                    ResizeFreeSpaceFillingColumns(ClientSize.Width - (e.NewWidth - oldWidth));
                else
                    ResizeFreeSpaceFillingColumns();
            }
        }

        /// <summary>
        /// When the column widths change, resize the space filling columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (!GetColumn(e.ColumnIndex).FillsFreeSpace)
                ResizeFreeSpaceFillingColumns();
        }

        /// <summary>
        /// When the size of the control changes, we have to resize our space filling columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleLayout(object sender, LayoutEventArgs e)
        {
            // We have to delay executing the recalculation of the columns, since virtual lists
            // get terribly confused if we resize the column widths during this event.
            if (Created)
                BeginInvoke(new MethodInvoker(ResizeFreeSpaceFillingColumns));
        }

        /// <summary>
        /// Resize our space filling columns so they fill any unoccupied width in the control
        /// </summary>
        protected virtual void ResizeFreeSpaceFillingColumns()
        {
            ResizeFreeSpaceFillingColumns(ClientSize.Width);
        }

        /// <summary>
        /// Resize our space filling columns so they fill any unoccupied width in the control
        /// </summary>
        protected virtual void ResizeFreeSpaceFillingColumns(int freeSpace)
        {
            // It's too confusing to dynamically resize columns at design time.
            if (DesignMode)
                return;

            if (Frozen)
                return;

            // Calculate the free space available
            int totalProportion = 0;
            var spaceFillingColumns = new List<OLVColumn>();
            for (int i = 0; i < Columns.Count; i++)
            {
                OLVColumn col = GetColumn(i);
                if (col.FillsFreeSpace)
                {
                    spaceFillingColumns.Add(col);
                    totalProportion += col.FreeSpaceProportion;
                }
                else
                    freeSpace -= col.Width;
            }
            freeSpace = Math.Max(0, freeSpace);

            // Any space filling column that would hit it's Minimum or Maximum
            // width must be treated as a fixed column.
            foreach (OLVColumn col in spaceFillingColumns.ToArray())
            {
                int newWidth = (freeSpace * col.FreeSpaceProportion) / totalProportion;

                if (col.MinimumWidth != -1 && newWidth < col.MinimumWidth)
                    newWidth = col.MinimumWidth;
                else if (col.MaximumWidth != -1 && newWidth > col.MaximumWidth)
                    newWidth = col.MaximumWidth;
                else
                    newWidth = 0;

                if (newWidth > 0)
                {
                    col.Width = newWidth;
                    freeSpace -= newWidth;
                    totalProportion -= col.FreeSpaceProportion;
                    spaceFillingColumns.Remove(col);
                }
            }

            // Distribute the free space between the columns
            foreach (OLVColumn col in spaceFillingColumns)
            {
                col.Width = (freeSpace * col.FreeSpaceProportion) / totalProportion;
            }
        }

        #endregion

        #region Checkboxes

        /// <summary>
        /// Mark the given object as indeterminate check state
        /// </summary>
        /// <param name="modelObject">The model object to be marked indeterminate</param>
        public virtual void CheckIndeterminateObject(object modelObject)
        {
            SetObjectCheckedness(modelObject, CheckState.Indeterminate);
        }

        /// <summary>
        /// Mark the given object as checked in the list
        /// </summary>
        /// <param name="modelObject">The model object to be checked</param>
        public virtual void CheckObject(object modelObject)
        {
            SetObjectCheckedness(modelObject, CheckState.Checked);
        }

        /// <summary>
        /// Return true of the given object is checked
        /// </summary>
        /// <param name="modelObject">The model object whose checkedness is returned</param>
        /// <returns>Is the given object checked?</returns>
        /// <remarks>If the given object is not in the list, this method returns false.</remarks>
        public virtual bool IsChecked(object modelObject)
        {
            OLVListItem olvi = ModelToItem(modelObject);
            if (olvi == null)
                return false;
            else
                return olvi.CheckState == CheckState.Checked;
        }

        /// <summary>
        /// Return true of the given object is indeterminately checked
        /// </summary>
        /// <param name="modelObject">The model object whose checkedness is returned</param>
        /// <returns>Is the given object indeterminately checked?</returns>
        /// <remarks>If the given object is not in the list, this method returns false.</remarks>
        public virtual bool IsCheckedIndeterminate(object modelObject)
        {
            OLVListItem olvi = ModelToItem(modelObject);
            return olvi != null && olvi.CheckState == CheckState.Indeterminate;
        }

        /// <summary>
        /// Get the checkedness of an object from the model. Returning null means the
        /// model does not know and the value from the control will be used.
        /// </summary>
        /// <param name="modelObject"></param>
        /// <returns></returns>
        protected virtual CheckState? GetCheckState(Object modelObject)
        {
            return CheckStateGetter == null ? (CheckState?)null : CheckStateGetter(modelObject);
        }

        /// <summary>
        /// Record the change of checkstate for the given object in the model.
        /// This does not update the UI -- only the model
        /// </summary>
        /// <param name="modelObject"></param>
        /// <param name="state"></param>
        /// <returns>The check state that was recorded and that should be used to update
        /// the control.</returns>
        protected virtual CheckState PutCheckState(Object modelObject, CheckState state)
        {
            return CheckStatePutter == null ? state : CheckStatePutter(modelObject, state);
        }

        /// <summary>
        /// Change the check state of the given object to be the given state.
        /// </summary>
        /// <param name="modelObject"></param>
        /// <param name="state"></param>
        protected virtual void SetObjectCheckedness(object modelObject, CheckState state)
        {
            OLVListItem olvi = ModelToItem(modelObject);
            if (olvi != null && olvi.CheckState != state)
            {
                olvi.CheckState = PutCheckState(modelObject, state);
                RefreshItem(olvi);
            }
        }

        /// <summary>
        /// Toggle the checkedness of the given object. A checked object becomes
        /// unchecked; an unchecked or indeterminate object becomes checked.
        /// </summary>
        /// <param name="modelObject">The model object to be checked</param>
        public virtual void ToggleCheckObject(object modelObject)
        {
            SetObjectCheckedness(modelObject,
                IsChecked(modelObject) ? CheckState.Unchecked : CheckState.Checked);
        }

        /// <summary>
        /// Mark the given object as unchecked in the list
        /// </summary>
        /// <param name="modelObject">The model object to be unchecked</param>
        public virtual void UncheckObject(object modelObject)
        {
            SetObjectCheckedness(modelObject, CheckState.Unchecked);
        }

        #endregion

        #region OLV accessing

        /// <summary>
        /// Return the column at the given index
        /// </summary>
        /// <param name="index">Index of the column to be returned</param>
        /// <returns>An OLVColumn</returns>
        public virtual OLVColumn GetColumn(int index)
        {
            return (OLVColumn)Columns[index];
        }

        /// <summary>
        /// Return the column at the given title.
        /// </summary>
        /// <param name="name">Name of the column to be returned</param>
        /// <returns>An OLVColumn</returns>
        public virtual OLVColumn GetColumn(string name)
        {
            foreach (ColumnHeader column in Columns)
            {
                if (column.Text == name)
                    return (OLVColumn)column;
            }
            return null;
        }

        /// <summary>
        /// Return a collection of columns that are appropriate to the given view.
        /// Only Tile and Details have columns; all other views have 0 columns.
        /// </summary>
        /// <param name="view">Which view are the columns being calculate for?</param>
        /// <returns>A list of columns</returns>
        public virtual List<OLVColumn> GetFilteredColumns(View view)
        {
            // For both detail and tile view, the first column must be included. Normally, we would
            // use the ColumnHeader.Index property, but if the header is not currently part of a ListView
            // that property returns -1. So, we track the index of
            // the column header, and always include the first header.

            int index = 0;
            switch (view)
            {
                case View.Details:
                    return AllColumns.FindAll(delegate(OLVColumn x) { return (index++ == 0) || x.IsVisible; });
                case View.Tile:
                    return AllColumns.FindAll(delegate(OLVColumn x) { return (index++ == 0) || x.IsTileViewColumn; });
                default:
                    return new List<OLVColumn>();
                    ;
            }
        }

        /// <summary>
        /// Return the number of items in the list
        /// </summary>
        /// <returns>the number of items in the list</returns>
        public virtual int GetItemCount()
        {
            return Items.Count;
        }

        /// <summary>
        /// Return the item at the given index
        /// </summary>
        /// <param name="index">Index of the item to be returned</param>
        /// <returns>An OLVListItem</returns>
        public virtual OLVListItem GetItem(int index)
        {
            if (index >= 0 && index < GetItemCount())
                return (OLVListItem)Items[index];
            else
                return null;
        }

        /// <summary>
        /// Return the model object at the given index
        /// </summary>
        /// <param name="index">Index of the model object to be returned</param>
        /// <returns>A model object</returns>
        public virtual object GetModelObject(int index)
        {
            return GetItem(index).RowObject;
        }

        /// <summary>
        /// Find the item and column that are under the given co-ords
        /// </summary>
        /// <param name="x">X co-ord</param>
        /// <param name="y">Y co-ord</param>
        /// <param name="selectedColumn">The column under the given point</param>
        /// <returns>The item under the given point. Can be null.</returns>
        public virtual OLVListItem GetItemAt(int x, int y, out OLVColumn selectedColumn)
        {
            selectedColumn = null;
            ListViewHitTestInfo info = HitTest(x, y);
            if (info.Item == null)
                return null;

            if (info.SubItem != null)
            {
                int subItemIndex = info.Item.SubItems.IndexOf(info.SubItem);
                selectedColumn = GetColumn(subItemIndex);
            }

            return (OLVListItem)info.Item;
        }

        #endregion

        #region Object manipulation

        /// <summary>
        /// Ensure that the given model object is visible
        /// </summary>
        /// <param name="modelObject">The model object to be revealed</param>
        public virtual void EnsureModelVisible(Object modelObject)
        {
            int idx = IndexOf(modelObject);
            if (idx >= 0)
                EnsureVisible(idx);
        }

        /// <summary>
        /// Return the model object of the row that is selected or null if there is no selection or more than one selection
        /// </summary>
        /// <returns>Model object or null</returns>
        public virtual object GetSelectedObject()
        {
            return SelectedIndices.Count == 1 ? GetModelObject(SelectedIndices[0]) : null;
        }

        /// <summary>
        /// Return the model objects of the rows that are selected or an empty collection if there is no selection
        /// </summary>
        /// <returns>ArrayList</returns>
        public virtual ArrayList GetSelectedObjects()
        {
            var objects = new ArrayList(SelectedIndices.Count);
            foreach (int index in SelectedIndices)
                objects.Add(GetModelObject(index));

            return objects;
        }

        /// <summary>
        /// Return the model object of the row that is checked or null if no row is checked
        /// or more than one row is checked
        /// </summary>
        /// <returns>Model object or null</returns>
        /// <remarks>Use CheckedObject property instead of this method</remarks>
        [Obsolete("Use CheckedObject property instead of this method")]
        public virtual object GetCheckedObject()
        {
            return CheckedObject;
        }

        /// <summary>
        /// Get the collection of model objects that are checked.
        /// </summary>
        /// <remarks>Use CheckedObjects property instead of this method</remarks>
        [Obsolete("Use CheckedObjects property instead of this method")]
        public virtual ArrayList GetCheckedObjects()
        {
            return (ArrayList)CheckedObjects;
        }

        /// <summary>
        /// Find the given model object within the listview and return its index
        /// </summary>
        /// <remarks>Technically, this method will work with virtual lists, but it will
        /// probably be very slow.</remarks>
        /// <param name="modelObject">The model object to be found</param>
        /// <returns>The index of the object. -1 means the object was not present</returns>
        public virtual int IndexOf(Object modelObject)
        {
            for (int i = 0; i < GetItemCount(); i++)
            {
                if (GetModelObject(i) == modelObject)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Update the ListViewItem with the data from its associated model.
        /// </summary>
        /// <remarks>This method does not resort or regroup the view. It simply updates
        /// the displayed data of the given item</remarks>
        public virtual void RefreshItem(OLVListItem olvi)
        {
            // For some reason, clearing the subitems also wipes out the back color,
            // so we need to store it and then put it back again later
            Color c = olvi.BackColor;
            olvi.SubItems.Clear();
            FillInValues(olvi, olvi.RowObject);
            SetSubItemImages(olvi.Index, olvi, true);
            olvi.BackColor = c;
        }

        /// <summary>
        /// Update the rows that are showing the given objects
        /// </summary>
        /// <remarks>This method does not resort or regroup the view.</remarks>
        public virtual void RefreshObject(object modelObject)
        {
            RefreshObjects(new[] { modelObject });
        }

        /// <summary>
        /// Update the rows that are showing the given objects
        /// </summary>
        /// <remarks>
        /// <para>This method does not resort or regroup the view.</para>
        /// <para>This method can safely be called from background threads.</para>
        /// </remarks>
        public virtual void RefreshObjects(IList modelObjects)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => RefreshObjects(modelObjects)));
                return;
            }
            foreach (object modelObject in modelObjects)
            {
                OLVListItem olvi = ModelToItem(modelObject);
                if (olvi != null)
                    RefreshItem(olvi);
            }
        }

        /// <summary>
        /// Update the rows that are selected
        /// </summary>
        /// <remarks>This method does not resort or regroup the view.</remarks>
        public virtual void RefreshSelectedObjects()
        {
            foreach (ListViewItem lvi in SelectedItems)
                RefreshItem((OLVListItem)lvi);
        }

        /// <summary>
        /// Scroll the listview so that the given group is at the top.
        /// </summary>
        /// <param name="lvg">The group to be revealed</param>
        /// <remarks><para>
        /// If the group is already visible, the list will still be scrolled to move
        /// the group to the top, if that is possible.
        /// </para>
        /// <para>This only works when the list is showing groups (obviously)</para>
        /// </remarks>
        public virtual void EnsureGroupVisible(ListViewGroup lvg)
        {
            if (!ShowGroups || lvg == null)
                return;

            int groupIndex = Groups.IndexOf(lvg);
            if (groupIndex <= 0)
            {
                // There is no easy way to scroll back to the beginning of the list
                int delta = 0 - User32.GetScrollPosition(Handle, false);
                NativeMethods.Scroll(this, 0, delta);
            }
            else
            {
                ListViewGroup previousGroup = Groups[groupIndex - 1];
                ListViewItem lastItemInGroup = previousGroup.Items[previousGroup.Items.Count - 1];
                Rectangle r = GetItemRect(lastItemInGroup.Index);
                int delta = r.Y + r.Height / 2;
                NativeMethods.Scroll(this, 0, delta);
            }
        }

        /// <summary>
        /// Select the row that is displaying the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="modelObject">The object to be selected or null to deselect all</param>
        public virtual void SelectObject(object modelObject)
        {
            SelectObject(modelObject, false);
        }

        /// <summary>
        /// Select the row that is displaying the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="modelObject">The object to be selected or null to deselect all</param>
        /// <param name="setFocus">Should the object be focused as well?</param>
        public virtual void SelectObject(object modelObject, bool setFocus)
        {
            // If the given model is already selected, don't do anything else (prevents an flicker)
            if (SelectedItems.Count == 1 && ((OLVListItem)SelectedItems[0]).RowObject == modelObject)
                return;

            SelectedItems.Clear();

            OLVListItem olvi = ModelToItem(modelObject);
            if (olvi != null)
            {
                olvi.Selected = true;
                if (setFocus)
                    olvi.Focused = true;
            }
        }

        /// <summary>
        /// Select the rows that is displaying any of the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        public virtual void SelectObjects(IList modelObjects)
        {
            SelectedItems.Clear();

            if (modelObjects == null)
                return;

            foreach (object modelObject in modelObjects)
            {
                OLVListItem olvi = ModelToItem(modelObject);
                if (olvi != null)
                    olvi.Selected = true;
            }
        }

        #endregion

        #region Freezing

        /// <summary>
        /// Freeze the listview so that it no longer updates itself.
        /// </summary>
        /// <remarks>Freeze()/Unfreeze() calls nest correctly</remarks>
        public virtual void Freeze()
        {
            freezeCount++;
        }

        /// <summary>
        /// Unfreeze the listview. If this call is the outermost Unfreeze(),
        /// the contents of the listview will be rebuilt.
        /// </summary>
        /// <remarks>Freeze()/Unfreeze() calls nest correctly</remarks>
        public virtual void Unfreeze()
        {
            if (freezeCount <= 0)
                return;

            freezeCount--;
            if (freezeCount == 0)
                DoUnfreeze();
        }

        /// <summary>
        /// Do the actual work required when the listview is unfrozen
        /// </summary>
        protected virtual void DoUnfreeze()
        {
            ResizeFreeSpaceFillingColumns();
            BuildList();
        }

        #endregion

        #region Column Sorting

        /// <summary>
        /// The name of the image used when a column is sorted descending
        /// </summary>
        /// <remarks>This image is only used on pre-XP systems. System images are used for XP and later</remarks>
        public const string SORT_INDICATOR_DOWN_KEY = "sort-indicator-down";

        /// <summary>
        /// The name of the image used when a column is sorted ascending
        /// </summary>
        /// <remarks>This image is only used on pre-XP systems. System images are used for XP and later</remarks>
        public const string SORT_INDICATOR_UP_KEY = "sort-indicator-up";

        /// <summary>
        /// Sort the items in the list view by the values in the given column.
        /// If ShowGroups is true, the rows will be grouped by the given column,
        /// otherwise, it will be a straight sort.
        /// </summary>
        /// <param name="columnToSortName">The name of the column whose values will be used for the sorting</param>
        public virtual void Sort(string columnToSortName)
        {
            Sort(GetColumn(columnToSortName));
        }

        /// <summary>
        /// Sort the items in the list view by the values in the given column.
        /// If ShowGroups is true, the rows will be grouped by the given column,
        /// otherwise, it will be a straight sort.
        /// </summary>
        /// <param name="columnToSortIndex">The index of the column whose values will be used for the sorting</param>
        public virtual void Sort(int columnToSortIndex)
        {
            if (columnToSortIndex >= 0 && columnToSortIndex < Columns.Count)
                Sort(GetColumn(columnToSortIndex));
        }

        /// <summary>
        /// Sort the items in the list view by the values in the given column.
        /// If ShowGroups is true, the rows will be grouped by the given column,
        /// otherwise, it will be a straight sort.
        /// </summary>
        /// <param name="columnToSort">The column whose values will be used for the sorting</param>
        public virtual void Sort(OLVColumn columnToSort)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => Sort(columnToSort)));
                return;
            }

            SortOrder order = lastSortOrder;
            if (order == SortOrder.None)
                order = Sorting;

            Sort(columnToSort, order);
        }

        /// <summary>
        /// Sort the items in the list view by the values in the given column.
        /// If ShowGroups is true, the rows will be grouped by the given column,
        /// otherwise, it will be a straight sort.
        /// </summary>
        /// <param name="columnToSort">The column whose values will be used for the sorting</param>
        /// <remarks>If ShowGroups is true and the AlwaysGroupByColumn property is not null,
        /// the list view items will be grouped by that column,
        /// and the columnToSort parameter will be ignored.</remarks>
        public virtual void Sort(OLVColumn columnToSort, SortOrder order)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { Sort(columnToSort, order); });
                return;
            }

            // If we are showing groups, there are some options that can override these settings
            if (ShowGroups)
                RationalizeColumnForGrouping(ref columnToSort, ref order);

            // Give the world a chance to fiddle with or completely avoid the sorting process
            var args = new BeforeSortingEventArgs(columnToSort, order, SecondarySortColumn, SecondarySortOrder);
            OnBeforeSorting(args);
            if (args.Canceled)
                return;

            // The event handler may have changed the sorting pattern
            columnToSort = args.ColumnToSort;
            order = args.SortOrder;
            OLVColumn secondaryColumn = args.SecondaryColumnToSort;
            SortOrder secondaryOrder = args.SecondarySortOrder;

            // Sanity checks
            if (columnToSort == null || order == SortOrder.None || Columns.Count < 1)
                return;

            // Virtual lists don't preserve selection, so we have to do it specifically
            IList selection = new ArrayList();
            if (VirtualMode)
                selection = SelectedObjects;

            // Finally, do the work of sorting
            if (ShowGroups)
                BuildGroups(columnToSort, order, secondaryColumn, secondaryOrder);
            else if (CustomSorter != null)
                CustomSorter(columnToSort, order);
            else
                ListViewItemSorter = new ColumnComparer(columnToSort, order, secondaryColumn, secondaryOrder);

            if (ShowSortIndicators)
                ShowSortIndicator(columnToSort, order);

            if (UseAlternatingBackColors && View == View.Details)
                PrepareAlternateBackColors();

            lastSortColumn = columnToSort;
            lastSortOrder = order;

            if (selection.Count > 0)
                SelectedObjects = selection;

            OnAfterSorting(new AfterSortingEventArgs(columnToSort, order, secondaryColumn, secondaryOrder));
        }

        /// <summary>
        /// When grouping items, there are some constraints that must always be observed.
        /// In particular, if AlwaysGroupByColumn property is set, it must be honoured.
        /// </summary>
        /// <param name="columnToSort"></param>
        /// <param name="order"></param>
        private void RationalizeColumnForGrouping(ref OLVColumn columnToSort, ref SortOrder order)
        {
            if (AlwaysGroupByColumn != null)
                columnToSort = AlwaysGroupByColumn;
            if (AlwaysGroupBySortOrder != SortOrder.None)
                order = AlwaysGroupBySortOrder;

            // Groups have to have a sorting column
            if (columnToSort == null && Columns.Count > 0)
                columnToSort = GetColumn(0);
            if (order == SortOrder.None)
                order = SortOrder.Ascending;
        }

        /// <summary>
        /// Put a sort indicator next to the text of the sort column
        /// </summary>
        public virtual void ShowSortIndicator()
        {
            if (ShowSortIndicators && lastSortOrder != SortOrder.None)
                ShowSortIndicator(lastSortColumn, lastSortOrder);
        }

        /// <summary>
        /// Put a sort indicator next to the text of the given given column
        /// </summary>
        /// <param name="columnToSort">The column to be marked</param>
        /// <param name="sortOrder">The sort order in effect on that column</param>
        protected virtual void ShowSortIndicator(OLVColumn columnToSort, SortOrder sortOrder)
        {
            int imageIndex = -1;

            if (!NativeMethods.HasBuiltinSortIndicators())
            {
                // If we can't use builtin image, we have to make and then locate the index of the
                // sort indicator we want to use. SortOrder.None doesn't show an image.
                if (SmallImageList == null || !SmallImageList.Images.ContainsKey(SORT_INDICATOR_UP_KEY))
                    MakeSortIndicatorImages();

                if (sortOrder == SortOrder.Ascending)
                    imageIndex = SmallImageList.Images.IndexOfKey(SORT_INDICATOR_UP_KEY);
                else if (sortOrder == SortOrder.Descending)
                    imageIndex = SmallImageList.Images.IndexOfKey(SORT_INDICATOR_DOWN_KEY);
            }

            // Set the image for each column
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i == columnToSort.Index)
                    NativeMethods.SetColumnImage(this, i, sortOrder, imageIndex);
                else
                    NativeMethods.SetColumnImage(this, i, SortOrder.None, -1);
            }
        }

        /// <summary>
        /// If the sort indicator images don't already exist, this method will make and install them
        /// </summary>
        protected virtual void MakeSortIndicatorImages()
        {
            ImageList il = SmallImageList;
            if (il == null)
            {
                il = new ImageList();
                il.ImageSize = new Size(16, 16);
            }

            // This arrangement of points works well with (16,16) images, and OK with others
            int midX = il.ImageSize.Width / 2;
            int midY = (il.ImageSize.Height / 2) - 1;
            int deltaX = midX - 2;
            int deltaY = deltaX / 2;

            if (il.Images.IndexOfKey(SORT_INDICATOR_UP_KEY) == -1)
            {
                var pt1 = new Point(midX - deltaX, midY + deltaY);
                var pt2 = new Point(midX, midY - deltaY - 1);
                var pt3 = new Point(midX + deltaX, midY + deltaY);
                il.Images.Add(SORT_INDICATOR_UP_KEY, MakeTriangleBitmap(il.ImageSize, new[] { pt1, pt2, pt3 }));
            }

            if (il.Images.IndexOfKey(SORT_INDICATOR_DOWN_KEY) == -1)
            {
                var pt1 = new Point(midX - deltaX, midY - deltaY);
                var pt2 = new Point(midX, midY + deltaY);
                var pt3 = new Point(midX + deltaX, midY - deltaY);
                il.Images.Add(SORT_INDICATOR_DOWN_KEY, MakeTriangleBitmap(il.ImageSize, new[] { pt1, pt2, pt3 }));
            }

            SmallImageList = il;
        }

        private Bitmap MakeTriangleBitmap(Size sz, Point[] pts)
        {
            var bm = new Bitmap(sz.Width, sz.Height);
            Graphics g = Graphics.FromImage(bm);
            g.FillPolygon(new SolidBrush(Color.Gray), pts);
            return bm;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// For some reason, UseItemStyleForSubItems doesn't work for the colors
        /// when owner drawing the list, so we have to specifically give each subitem
        /// the desired colors
        /// </summary>
        /// <param name="olvi">The item whose subitems are to be corrected</param>
        protected virtual void CorrectSubItemColors(ListViewItem olvi)
        {
            if (OwnerDraw && olvi.UseItemStyleForSubItems)
                foreach (ListViewItem.ListViewSubItem si in olvi.SubItems)
                {
                    si.BackColor = olvi.BackColor;
                    si.ForeColor = olvi.ForeColor;
                }
        }

        /// <summary>
        /// Fill in the given OLVListItem with values of the given row
        /// </summary>
        /// <param name="lvi">the OLVListItem that is to be stuff with values</param>
        /// <param name="rowObject">the model object from which values will be taken</param>
        protected virtual void FillInValues(OLVListItem lvi, object rowObject)
        {
            if (Columns.Count == 0)
                return;

            OLVColumn column = GetColumn(0);
            lvi.Text = column.GetStringValue(rowObject);
            lvi.ImageSelector = column.GetImage(rowObject);

            for (int i = 1; i < Columns.Count; i++)
            {
                column = GetColumn(i);
                lvi.SubItems.Add(new OLVListSubItem(column.GetStringValue(rowObject),
                                                    column.GetImage(rowObject)));
            }

            // Give the row formatter a chance to mess with the item
            if (RowFormatter != null)
                RowFormatter(lvi);

            CorrectSubItemColors(lvi);

            // Set the check state of the row, if we are showing check boxes
            if (CheckBoxes)
            {
                CheckState? state = GetCheckState(lvi.RowObject);
                if (state.HasValue)
                    lvi.CheckState = (CheckState)state;
            }
        }

        /// <summary>
        /// Make sure the ListView has the extended style that says to display subitem images.
        /// </summary>
        /// <remarks>This method must be called after any .NET call that update the extended styles
        /// since they seem to erase this setting.</remarks>
        protected virtual void ForceSubItemImagesExStyle()
        {
            NativeMethods.ForceSubItemImagesExStyle(this);
        }

        /// <summary>
        /// Convert the given image selector to an index into our image list.
        /// Return -1 if that's not possible
        /// </summary>
        /// <param name="imageSelector"></param>
        /// <returns>Index of the image in the imageList, or -1</returns>
        protected virtual int GetActualImageIndex(Object imageSelector)
        {
            if (imageSelector == null)
                return -1;

            if (imageSelector is Int32)
                return (int)imageSelector;

            var imageSelectorAsString = imageSelector as String;
            if (imageSelectorAsString != null && SmallImageList != null)
                return SmallImageList.Images.IndexOfKey(imageSelectorAsString);

            return -1;
        }

        /// <summary>
        /// Return the tooltip that should be shown when the mouse is hovered over the given column
        /// </summary>
        /// <param name="columnIndex">The column index whose tool tip is to be fetched</param>
        /// <returns>A string or null if no tool tip is to be shown</returns>
        public virtual String GetHeaderToolTip(int columnIndex)
        {
            OLVColumn column = GetColumn(columnIndex);
            if (column == null)
                return null;
            String tooltip = column.ToolTipText;
            if (HeaderToolTipGetter != null)
                tooltip = HeaderToolTipGetter(column);
            return tooltip;
        }

        /// <summary>
        /// Return the tooltip that should be shown when the mouse is hovered over the given cell
        /// </summary>
        /// <param name="columnIndex">The column index whose tool tip is to be fetched</param>
        /// <returns>A string or null if no tool tip is to be shown</returns>
        public virtual String GetCellToolTip(int columnIndex, int rowIndex)
        {
            return CellToolTipGetter == null ?
                null : CellToolTipGetter(GetColumn(columnIndex), GetModelObject(rowIndex));
            //String tooltip = String.Format("tool tip for cell ({0}, {1})\r\n=> '{2}'.", columnIndex, rowIndex, this.GetModelObject(rowIndex));
        }

        /// <summary>
        /// Return the OLVListItem that displays the given model object
        /// </summary>
        /// <param name="modelObject">The modelObject whose item is to be found</param>
        /// <returns>The OLVListItem that displays the model, or null</returns>
        /// <remarks>This method has O(n) performance.</remarks>
        public virtual OLVListItem ModelToItem(object modelObject)
        {
            // THINK: Should this method be public, protected or internal?
            if (modelObject == null)
                return null;

            OLVListItem olvi;
            foreach (ListViewItem lvi in Items)
            {
                olvi = (OLVListItem)lvi;
                if (olvi.RowObject == modelObject)
                    return olvi;
            }
            return null;
        }

        /// <summary>
        /// Prepare the listview to show alternate row backcolors
        /// </summary>
        /// <remarks>We cannot rely on lvi.Index in this method.
        /// In a straight list, lvi.Index is the display index, and can be used to determine
        /// whether the row should be colored. But when organised by groups, lvi.Index is not
        /// useable because it still refers to the position in the overall list, not the display order.
        ///</remarks>
        protected virtual void PrepareAlternateBackColors()
        {
            // If this method is called during a BeginUpdate/EndUpdate pair, changes to the
            // Items collection are cached. Getting the Count flushes that cache.
            int count = Items.Count;

            Color rowBackColor = AlternateRowBackColorOrDefault;
            int i = 0;
            if (ShowGroups)
            {
                foreach (ListViewGroup group in Groups)
                {
                    foreach (ListViewItem lvi in group.Items)
                    {
                        lvi.BackColor = i % 2 == 0 ? BackColor : rowBackColor;
                        CorrectSubItemColors(lvi);
                        i++;
                    }
                }
            }
            else
            {
                foreach (ListViewItem lvi in Items)
                {
                    lvi.BackColor = i % 2 == 0 ? BackColor : rowBackColor;
                    CorrectSubItemColors(lvi);
                    i++;
                }
            }
        }

        /// <summary>
        /// Setup all subitem images on all rows
        /// </summary>
        protected virtual void SetAllSubItemImages()
        {
            if (!ShowImagesOnSubItems)
                return;

            ForceSubItemImagesExStyle();

            for (int rowIndex = 0; rowIndex < GetItemCount(); rowIndex++)
                SetSubItemImages(rowIndex, GetItem(rowIndex));
        }

        /// <summary>
        /// For the given item and subitem, make it display the given image
        /// </summary>
        /// <param name="itemIndex">row number (0 based)</param>
        /// <param name="subItemIndex">subitem (0 is the item itself)</param>
        /// <param name="imageIndex">index into the image list</param>
        protected virtual void SetSubItemImage(int itemIndex, int subItemIndex, int imageIndex)
        {
            NativeMethods.SetSubItemImage(this, itemIndex, subItemIndex, imageIndex);
        }

        /// <summary>
        /// Tell the underlying list control which images to show against the subitems
        /// </summary>
        /// <param name="rowIndex">the index at which the item occurs</param>
        /// <param name="item">the item whose subitems are to be set</param>
        protected virtual void SetSubItemImages(int rowIndex, OLVListItem item)
        {
            SetSubItemImages(rowIndex, item, false);
        }

        /// <summary>
        /// Tell the underlying list control which images to show against the subitems
        /// </summary>
        /// <param name="rowIndex">the index at which the item occurs</param>
        /// <param name="item">the item whose subitems are to be set</param>
        /// <param name="shouldClearImages">will existing images be cleared if no new image is provided?</param>
        protected virtual void SetSubItemImages(int rowIndex, OLVListItem item, bool shouldClearImages)
        {
            if (!ShowImagesOnSubItems)
                return;

            for (int i = 1; i < item.SubItems.Count; i++)
            {
                int imageIndex = GetActualImageIndex(((OLVListSubItem)item.SubItems[i]).ImageSelector);
                if (shouldClearImages || imageIndex != -1)
                    SetSubItemImage(rowIndex, i, imageIndex);
            }
        }

        /// <summary>
        /// Take ownership of the 'objects' collection. This separats our collection from the source.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method
        /// separates the 'objects' instance variable from its source, so that any AddObject/RemoveObject
        /// calls will modify our collection and not the original colleciton.
        /// </para>
        /// <para>
        /// This method has the intentional side-effect of converting our list of objects to an ArrayList.
        /// </para>
        /// </remarks>
        protected virtual void TakeOwnershipOfObjects()
        {
            if (isOwnerOfObjects)
                return;

            isOwnerOfObjects = true;

            if (objects == null)
                objects = new ArrayList();
            else if (objects is ICollection)
                objects = new ArrayList((ICollection)objects);
            else
            {
                var newObjects = new ArrayList();
                foreach (object x in objects)
                    newObjects.Add(x);
                objects = newObjects;
            }
        }

        #endregion

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit()
        {
            Frozen = true;
        }

        void ISupportInitialize.EndInit()
        {
            Frozen = false;
        }

        #endregion

        #region Image list manipulation

        /// <summary>
        /// Update our externally visible image list so it holds the same images as our shadow list, but sized correctly
        /// </summary>
        private void SetupExternalImageList()
        {
            // If a row height hasn't been set, or an image list has been give which is the required size, just assign it
            if (rowHeight == -1 || (shadowedImageList != null && shadowedImageList.ImageSize.Height == rowHeight))
                base.SmallImageList = shadowedImageList;
            else
            {
                int width = (shadowedImageList == null ? 16 : shadowedImageList.ImageSize.Width);
                base.SmallImageList = MakeResizedImageList(width, rowHeight, shadowedImageList);
            }
        }

        /// <summary>
        /// Return a copy of the given source image list, where each image has been resized to be height x height in size.
        /// If source is null, an empty image list of the given size is returned
        /// </summary>
        /// <param name="height">Height and width of the new images</param>
        /// <param name="source">Source of the images (can be null)</param>
        /// <returns>A new image list</returns>
        private ImageList MakeResizedImageList(int width, int height, ImageList source)
        {
            var il = new ImageList();
            il.ImageSize = new Size(width, height);

            // If there's nothing to copy, just return the new list
            if (source == null)
                return il;

            il.TransparentColor = source.TransparentColor;
            il.ColorDepth = source.ColorDepth;

            // Fill the imagelist with resized copies from the source
            for (int i = 0; i < source.Images.Count; i++)
            {
                Bitmap bm = MakeResizedImage(width, height, source.Images[i], source.TransparentColor);
                il.Images.Add(bm);
            }

            // Give each image the same key it has in the original
            foreach (String key in source.Images.Keys)
            {
                il.Images.SetKeyName(source.Images.IndexOfKey(key), key);
            }

            return il;
        }

        /// <summary>
        /// Return a bitmap of the given height x height, which shows the given image, centred.
        /// </summary>
        /// <param name="height">Height and width of new bitmap</param>
        /// <param name="image">Image to be centred</param>
        /// <param name="transparent">The background color</param>
        /// <returns>A new bitmap</returns>
        private Bitmap MakeResizedImage(int width, int height, Image image, Color transparent)
        {
            var bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(transparent);
            int x = Math.Max(0, (bm.Size.Width - image.Size.Width) / 2);
            int y = Math.Max(0, (bm.Size.Height - image.Size.Height) / 2);
            g.DrawImage(image, x, y, image.Size.Width, image.Size.Height);
            return bm;
        }

        /// <summary>
        /// Initialize the state image list with the required checkbox images
        /// </summary>
        protected virtual void InitializeStateImageList()
        {
            if (StateImageList == null)
            {
                StateImageList = new ImageList();
                StateImageList.ImageSize = new Size(16, 16);
            }

            if (StateImageList.Images.Count == 0)
                AddCheckedImage(StateImageList, CheckBoxState.UncheckedNormal);
            if (StateImageList.Images.Count <= 1)
                AddCheckedImage(StateImageList, CheckBoxState.CheckedNormal);
            if (StateImageList.Images.Count <= 2)
                AddCheckedImage(StateImageList, CheckBoxState.MixedNormal);
        }

        private void AddCheckedImage(ImageList imageList, CheckBoxState checkBoxState)
        {
            var bm = new Bitmap(imageList.ImageSize.Width, imageList.ImageSize.Height);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(imageList.TransparentColor);
            CheckBoxRenderer.DrawCheckBox(g, new Point(0, 0), checkBoxState);
            imageList.Images.Add(bm);
        }

        #endregion

        #region Owner drawing

        private readonly int[] columnRightEdge = new int[256]; // will anyone ever want more than 256 columns??

        /// <summary>
        /// Owner draw the column header
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        /// <summary>
        /// Owner draw the item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            // If there is a custom renderer installed for the primary column,
            // and we're not in details view, give it a chance to draw the item.
            // So the renderer on the primary column can have two distinct tasks,
            // in details view, it draws the primary cell; in non-details view,
            // it draws the whole item.
            OLVColumn column = GetColumn(0);
            if (View != View.Details && column.RendererDelegate != null)
            {
                Object row = ((OLVListItem)e.Item).RowObject;
                e.DrawDefault = !column.RendererDelegate(e, e.Graphics, e.Bounds, row);
            }
            else
                e.DrawDefault = (View != View.Details);

            if (e.DrawDefault)
                base.OnDrawItem(e);
        }

        /// <summary>
        /// Owner draw a single subitem
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            // Don't try to do owner drawing at design time
            if (DesignMode)
            {
                e.DrawDefault = true;
                return;
            }

            // Calculate where the subitem should be drawn
            // It should be as simple as 'e.Bounds', but it isn't :-(

            // There seems to be a bug in .NET where the left edge of the bounds of subitem 0
            // is always 0. This is normally what is required, but it's wrong when
            // someone drags column 0 to somewhere else in the listview. We could
            // drop down into Windows-ville and use LVM_GETSUBITEMRECT, but just to be different
            // we keep track of the right edge of all columns, and when subitem 0
            // isn't being shown at column 0, we make its left edge to be the right
            // edge of the previous column plus a little bit.
            // NOTE: I considered replacing this with LVM_GETSUBITEMRECT, but apparently that has exactly
            // same erroneous behavior.
            Rectangle r = e.Bounds;

            if (GridLines && e.ItemState != ListViewItemStates.Selected)
            {
                r.Y += 1;
                r.Height -= 2;
            }

            if (e.ColumnIndex == 0 && e.Header.DisplayIndex != 0)
            {
                r.X = columnRightEdge[e.Header.DisplayIndex - 1] + 1;
            }
            else
                columnRightEdge[e.Header.DisplayIndex] = e.Bounds.Right;

            // Get the special renderer for this column. If there isn't one, use the default draw mechanism.
            OLVColumn column = GetColumn(e.ColumnIndex);
            if (column.RendererDelegate == null)
            {
                e.DrawDefault = true;
                return;
            }
#if !MONO
            // Optimize drawing by only redrawing subitems that touch the area that was damaged
            if (!r.IntersectsWith(lastUpdateRectangle))
            {
                return;
            }
#endif
            // Get a graphics context for the renderer to use.
            // But we have more complications. Virtual lists have a nasty habit of drawing column 0
            // whenever there is any mouse move events over a row, and doing it in an un-double-buffered manner,
            // which results in nasty flickers! There are also some unbuffered draw when a mouse is first
            // hovered over column 0 of a normal row. So, to avoid all complications,
            // we always manually double-buffer the drawing.
            // Except with Mono, which doesn't seem to handle double buffering at all :-(
            Graphics g = e.Graphics;
            BufferedGraphics buffer = null;
#if !MONO
            bool avoidFlickerMode = true; // set this to false to see the problems with flicker
            if (avoidFlickerMode)
            {
                buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, r);
                g = buffer.Graphics;
            }
#endif
            // Finally, give the renderer a chance to draw something
            Object row = ((OLVListItem)e.Item).RowObject;
            e.DrawDefault = !column.RendererDelegate(e, g, r, row);

            if (!e.DrawDefault && buffer != null)
            {
                buffer.Render();
                buffer.Dispose();
            }
        }

        #endregion

        #region Selection Event Handling

        private bool hasIdleHandler;

        /// <summary>
        /// This method is called every time a row is selected or deselected. This can be
        /// a pain if the user shift-clicks 100 rows. We override this method so we can
        /// trigger one event for any number of select/deselects that come from one user action
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            // If we haven't already scheduled an event, schedule it to be triggered
            // By using idle time, we will wait until all select events for the same
            // user action have finished before triggering the event.
            if (!hasIdleHandler)
            {
                hasIdleHandler = true;
                Application.Idle += Application_Idle;
            }
        }

        /// <summary>
        /// The application is idle. Trigger a SelectionChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_Idle(object sender, EventArgs e)
        {
            // Remove the handler before triggering the event
            Application.Idle -= Application_Idle;
            hasIdleHandler = false;

            OnSelectionChanged(new EventArgs());
        }

        /// <summary>
        /// This event is triggered once per user action that changes the selection state
        /// of one or more rows.
        /// </summary>
        [Category("Behavior - ObjectListView"),
         Description(
             "This event is triggered once per user action that changes the selection state of one or more rows.")]
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Trigger the SelectionChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        #endregion

        #region Cell editing

        // What event should we listen for?
        // --------------------------------
        //
        // We can't use OnMouseClick, OnMouseDoubleClick, OnClick, or OnDoubleClick
        // since they are not triggered for clicks on subitems without Full Row Select.
        //
        // We could use OnMouseDown, but selecting rows is done in OnMouseUp. This means
        // that if we start the editing during OnMouseDown, the editor will automatically
        // lose focus when mouse up happens.
        //

        private CellEditEventArgs cellEditEventArgs;
        private Control cellEditor;
        private int lastMouseDownClickCount;

        /// <summary>
        /// We need the click count in the mouse up event, but that is always 1.
        /// So we have to remember the click count from the preceding mouse down event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            lastMouseDownClickCount = e.Clicks;
        }

        /// <summary>
        /// Check to see if we need to start editing a cell
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // If it was an unmodified left click, check to see if we should start editing
            if (ShouldStartCellEdit(e))
            {
                ListViewHitTestInfo info = HitTest(e.Location);
                if (info.Item != null && info.SubItem != null)
                {
                    int subItemIndex = info.Item.SubItems.IndexOf(info.SubItem);

                    // We don't edit the primary column by single clicks -- only subitems.
                    if (CellEditActivation != CellEditActivateMode.SingleClick || subItemIndex > 0)
                        EditSubItem((OLVListItem)info.Item, subItemIndex);
                }
            }
        }

        /// <summary>
        /// Should we start editing the cell?
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual bool ShouldStartCellEdit(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return false;

            if ((ModifierKeys & (Keys.Shift | Keys.Control | Keys.Alt)) != 0)
                return false;

            if (lastMouseDownClickCount == 1 && CellEditActivation == CellEditActivateMode.SingleClick)
                return true;

            return (lastMouseDownClickCount == 2 && CellEditActivation == CellEditActivateMode.DoubleClick);
        }

        /// <summary>
        /// Handle a key press on this control. We specifically look for F2 which edits the primary column,
        /// or a Tab character during an edit operation, which tries to start editing on the next (or previous) cell.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            bool isSimpleTabKey = ((keyData & Keys.KeyCode) == Keys.Tab) &&
                                  ((keyData & (Keys.Alt | Keys.Control)) == Keys.None);

            if (isSimpleTabKey && IsCellEditing)
            {
                // Tab key while editing
                // If the cell editing was cancelled, don't handle the tab
                if (!PossibleFinishCellEditing())
                    return true;

                // We can only Tab between columns when we are in Details view
                if (View != View.Details)
                    return true;

                OLVListItem olvi = cellEditEventArgs.ListViewItem;
                int subItemIndex = cellEditEventArgs.SubItemIndex;
                int displayIndex = GetColumn(subItemIndex).DisplayIndex;

                // Move to the next or previous editable subitem, depending on Shift key. Wrap at the edges
                List<OLVColumn> columnsInDisplayOrder = ColumnsInDisplayOrder;
                do
                {
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        displayIndex = (olvi.SubItems.Count + displayIndex - 1) % olvi.SubItems.Count;
                    else
                        displayIndex = (displayIndex + 1) % olvi.SubItems.Count;
                } while (!columnsInDisplayOrder[displayIndex].IsEditable);

                // If we found a different editable cell, start editing it
                subItemIndex = columnsInDisplayOrder[displayIndex].Index;
                if (cellEditEventArgs.SubItemIndex != subItemIndex)
                {
                    StartCellEdit(olvi, subItemIndex);
                    return true;
                }
            }

            // Treat F2 as a request to edit the primary column
            if (keyData == Keys.F2 && !IsCellEditing)
            {
                EditSubItem((OLVListItem)FocusedItem, 0);
                return true;
            }

            // We have to catch Return/Enter/Escape here since some types of controls
            // (e.g. ComboBox, UserControl) don't trigger key events that we can listen for.
            // Treat Return or Enter as committing the current edit operation
            if ((keyData == Keys.Return || keyData == Keys.Enter) && IsCellEditing)
            {
                PossibleFinishCellEditing();
                return true;
            }

            // Treat Escaoe as cancel the current edit operation
            if (keyData == Keys.Escape && IsCellEditing)
            {
                CancelCellEdit();
                return true;
            }

            // Treat Ctrl-C as Copy To Clipboard. We still allow the default processing
            if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.KeyCode) == Keys.C)
                CopySelectionToClipboard();

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Begin an edit operation on the given cell.
        /// </summary>
        /// <remarks>This performs various sanity checks and passes off the real work to StartCellEdit().</remarks>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        public virtual void EditSubItem(OLVListItem item, int subItemIndex)
        {
            if (item == null)
                return;

            if (subItemIndex < 0 && subItemIndex >= item.SubItems.Count)
                return;

            if (CellEditActivation == CellEditActivateMode.None)
                return;

            if (!GetColumn(subItemIndex).IsEditable)
                return;

            StartCellEdit(item, subItemIndex);
        }

        /// <summary>
        /// Really start an edit operation on a given cell. The parameters are assumed to be sane.
        /// </summary>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        protected virtual void StartCellEdit(OLVListItem item, int subItemIndex)
        {
            OLVColumn column = GetColumn(subItemIndex);
            Rectangle r = CalculateCellBounds(item, subItemIndex);
            Control c = GetCellEditor(item, subItemIndex);
            c.Bounds = r;

            // Try to align the control as the column is aligned. Not all controls support this property
            PropertyInfo pinfo = c.GetType().GetProperty("TextAlign");
            if (pinfo != null)
                pinfo.SetValue(c, column.TextAlign, null);

            // Give the control the value from the model
            SetControlValue(c, column.GetValue(item.RowObject), column.GetStringValue(item.RowObject));

            // Give the outside world the chance to munge with the process
            cellEditEventArgs = new CellEditEventArgs(column, c, r, item, subItemIndex);
            OnCellEditStarting(cellEditEventArgs);
            if (cellEditEventArgs.Cancel)
                return;

            // The event handler may have completely changed the control, so we need to remember it
            cellEditor = cellEditEventArgs.Control;

            // If the control isn't the height of the cell, centre it vertically. We don't
            // need to do this when in Tile view.
            if (View != View.Tile && cellEditor.Height != r.Height)
                cellEditor.Top += (r.Height - cellEditor.Height) / 2;

            Controls.Add(cellEditor);
            ConfigureControl();
            PauseAnimations(true);
        }

        /// <summary>
        /// Try to give the given value to the provided control. Fall back to assigning a string
        /// if the value assignment fails.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="value">The value to be given to the control</param>
        /// <param name="stringValue">The string to be given if the value doesn't work</param>
        protected virtual void SetControlValue(Control control, Object value, String stringValue)
        {
            // Look for a property called "Value". We have to look twice, the first time might get an ambiguous
            PropertyInfo pinfo = null;
            try
            {
                pinfo = control.GetType().GetProperty("Value");
            }
            catch (AmbiguousMatchException)
            {
                // The lowest level class of the control must have overridden the "Value" property.
                // We now have to specifically  look for only public instance properties declared in the lowest level class.
                pinfo = control.GetType().GetProperty("Value",
                                                      BindingFlags.DeclaredOnly | BindingFlags.Instance |
                                                      BindingFlags.Public);
            }

            // If we found it, use it to assign a value, otherwise simply set the text
            if (pinfo != null)
            {
                try
                {
                    pinfo.SetValue(control, value, null);
                    return;
                }
                catch (TargetInvocationException)
                {
                    // Not a lot we can do about this one. Something went wrong in the bowels
                    // of the method. Let's take the ostrich approach and just ignore it :-)
                }
                catch (ArgumentException)
                {
                }
            }

            // There wasn't a Value property, or we couldn't set it, so set the text instead
            try
            {
                var valueAsString = value as String;
                if (valueAsString == null)
                    control.Text = stringValue;
                else
                    control.Text = valueAsString;
            }
            catch (ArgumentOutOfRangeException)
            {
                // The value couldn't be set via the Text property.
            }
        }

        /// <summary>
        /// Setup the given control to be a cell editor
        /// </summary>
        protected virtual void ConfigureControl()
        {
            cellEditor.Validating += CellEditor_Validating;
            cellEditor.Select();
        }

        /// <summary>
        /// Return the value that the given control is showing
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        protected virtual Object GetControlValue(Control control)
        {
            try
            {
                return control.GetType().InvokeMember("Value", BindingFlags.GetProperty, null, control, null);
            }
            catch (MissingMethodException)
            {
                // Microsoft throws this
                return control.Text;
            }
            catch (MissingFieldException)
            {
                // Mono throws this
                return control.Text;
            }
        }

        /// <summary>
        /// Called when the cell editor could be about to lose focus. Time to commit the change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CellEditor_Validating(object sender, CancelEventArgs e)
        {
            cellEditEventArgs.Cancel = false;
            OnCellEditorValidating(cellEditEventArgs);

            if (cellEditEventArgs.Cancel)
            {
                cellEditEventArgs.Control.Select();
                e.Cancel = true;
            }
            else
                FinishCellEdit();
        }

        /// <summary>
        /// Return the bounds of the given cell
        /// </summary>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        /// <returns>A Rectangle</returns>
        protected virtual Rectangle CalculateCellBounds(OLVListItem item, int subItemIndex)
        {
            // Item 0 is special. Its bounds include all subitems. To get just the bounds
            // of cell for item 0, we have to use GetItemRect().
            if (subItemIndex == 0)
            {
                return GetItemRect(item.Index, ItemBoundsPortion.Label);
            }
            else
                return item.SubItems[subItemIndex].Bounds;
        }

        /// <summary>
        /// Return a control that can be used to edit the value of the given cell.
        /// </summary>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        /// <returns></returns>
        protected virtual Control GetCellEditor(OLVListItem item, int subItemIndex)
        {
            OLVColumn column = GetColumn(subItemIndex);
            Object value = column.GetValue(item.RowObject);

            //THINK: Do we want to use a Registry instead of this cascade?
            if (value is Boolean)
                return new BooleanCellEditor();

            if (value is DateTime)
            {
                var c = new DateTimePicker();
                c.Format = DateTimePickerFormat.Short;
                return c;
            }
            if (value is Int32 || value is Int16 || value is Int64)
                return new IntUpDown();

            if (value is UInt16 || value is UInt32 || value is UInt64)
                return new UintUpDown();

            if (value is Single || value is Double)
                return new FloatCellEditor();

            return MakeDefaultCellEditor(column);
        }

        /// <summary>
        /// Return a TextBox that can be used as a default cell editor.
        /// </summary>
        /// <param name="column">What column does the cell belong to?</param>
        /// <returns></returns>
        protected virtual Control MakeDefaultCellEditor(OLVColumn column)
        {
            var tb = new TextBox();
            String str;

            // Build a list of unique values, to be used as autocomplete on the editor
            var alreadySeen = new Dictionary<string, bool>();
            lock (_syncRoot)
            {
                for (int i = 0; i < Math.Min(GetItemCount(), 1000); i++)
                {
                    Object value = column.GetValue(GetModelObject(i));
                    var valueAsString = value as String;
                    if (valueAsString == null)
                        str = column.ValueToString(value);
                    else
                        str = valueAsString;
                    if (!alreadySeen.ContainsKey(str))
                    {
                        tb.AutoCompleteCustomSource.Add(str);
                        alreadySeen[str] = true;
                    }
                }
            }

            tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tb.AutoCompleteMode = AutoCompleteMode.Append;

            return tb;
        }

        /// <summary>
        /// Stop editing a cell and throw away any changes.
        /// </summary>
        public virtual void CancelCellEdit()
        {
            if (!IsCellEditing)
                return;

            // Let the world know that the user has cancelled the edit operation
            cellEditEventArgs.Cancel = true;
            OnCellEditFinishing(cellEditEventArgs);

            // Now cleanup the editing process
            CleanupCellEdit();
        }

        /// <summary>
        /// If a cell edit is in progress, finish the edit.
        /// </summary>
        /// <returns>Returns false if the finishing process was cancelled
        /// (i.e. the cell editor is still on screen)</returns>
        /// <remarks>This method does not guarantee that the editing will finish. The validation
        /// process can cause the finishing to be aborted. Developers should check the return value
        /// or use IsCellEditing property after calling this method to see if the user is still
        /// editing a cell.</remarks>
        public virtual bool PossibleFinishCellEditing()
        {
            if (IsCellEditing)
            {
                cellEditEventArgs.Cancel = false;
                OnCellEditorValidating(cellEditEventArgs);

                if (cellEditEventArgs.Cancel)
                    return false;

                FinishCellEdit();
            }

            return true;
        }

        /// <summary>
        /// Finish the cell edit operation, writing changed data back to the model object
        /// </summary>
        /// <remarks>This method does not trigger a Validating event, so it always finishes
        /// the cell edit.</remarks>
        public virtual void FinishCellEdit()
        {
            if (!IsCellEditing)
                return;

            cellEditEventArgs.Cancel = false;
            OnCellEditFinishing(cellEditEventArgs);

            // If someone doesn't cancel the editing process, write the value back into the model
            if (!cellEditEventArgs.Cancel)
            {
                Object value = GetControlValue(cellEditor);
                cellEditEventArgs.Column.PutValue(cellEditEventArgs.RowObject, value);
                RefreshItem(cellEditEventArgs.ListViewItem);
            }

            CleanupCellEdit();
        }

        /// <summary>
        /// Remove all trace of any existing cell edit operation
        /// </summary>
        protected virtual void CleanupCellEdit()
        {
            if (cellEditor == null)
                return;

            cellEditor.Validating -= CellEditor_Validating;
            Controls.Remove(cellEditor);
            cellEditor.Dispose(); //THINK: do we need to call this?
            cellEditor = null;
            Select();
            PauseAnimations(false);
        }

        #endregion

        #region Design Time

        /// <summary>
        /// Return Columns for this list. We hide the original so we can associate
        /// a specialised editor with it.
        /// </summary>
        [Editor(typeof(OLVColumnCollectionEditor), typeof(UITypeEditor))]
        public new ColumnHeaderCollection Columns
        {
            get { return base.Columns; }
        }

        /// <summary>
        /// This class works in conjunction with the OLVColumns property to allow OLVColumns
        /// to be added to the ObjectListView.
        /// </summary>
        internal class OLVColumnCollectionEditor : CollectionEditor
        {
            public OLVColumnCollectionEditor(Type t)
                : base(t)
            {
            }

            protected override Type CreateCollectionItemType()
            {
                return typeof(OLVColumn);
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                // Figure out which ObjectListView we are working on. This should be the Instance of the context.
                ObjectListView olv = null;
                if (context != null)
                    olv = context.Instance as ObjectListView;

                if (olv == null)
                {
                    //THINK: Can this ever happen?
                    Debug.WriteLine("context.Instance was NOT an ObjectListView");

                    // Hack to figure out which ObjectListView we are working on
                    var cols = (ColumnHeaderCollection)value;
                    if (cols.Count == 0)
                    {
                        cols.Add(new OLVColumn());
                        olv = (ObjectListView)cols[0].ListView;
                        cols.Clear();
                        olv.AllColumns.Clear();
                    }
                    else
                        olv = (ObjectListView)cols[0].ListView;
                }

                // Edit all the columns, not just the ones that are visible
                base.EditValue(context, provider, olv.AllColumns);

                // Calculate just the visible columns
                List<OLVColumn> newColumns = olv.GetFilteredColumns(View.Details);
                olv.Columns.Clear();
                olv.Columns.AddRange(newColumns.ToArray());

                return olv.Columns;
            }

            protected override string GetDisplayText(object value)
            {
                var col = value as OLVColumn;
                if (col == null || String.IsNullOrEmpty(col.AspectName))
                    return base.GetDisplayText(value);

                return base.GetDisplayText(value) + " (" + col.AspectName + ")";
            }
        }

        #endregion
    }

}