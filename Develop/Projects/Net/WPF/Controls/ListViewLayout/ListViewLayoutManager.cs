using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Savchin.Wpf.Controls.ListViewLayout
{

    public class ListViewLayoutManager
    {
        private GridViewColumn autoSizedColumn;
        private bool loaded;
        private Cursor resizeCursor;
        private bool resizing;
        private ScrollViewer scrollViewer;

        #region Properties

        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled",
            typeof(bool),
            typeof(ListViewLayoutManager),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnLayoutManagerEnabledChanged)));

        /// <summary>
        /// Sets the enabled.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public static void SetEnabled(DependencyObject dependencyObject, bool enabled)
        {
            dependencyObject.SetValue(EnabledProperty, enabled);
        }

        private ScrollBarVisibility verticalScrollBarVisibility = ScrollBarVisibility.Auto;
        /// <summary>
        /// Gets or sets the vertical scroll bar visibility.
        /// </summary>
        /// <value>The vertical scroll bar visibility.</value>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return verticalScrollBarVisibility; }
            set { verticalScrollBarVisibility = value; }
        }

        private readonly ListView listView;
        /// <summary>
        /// Gets the list view.
        /// </summary>
        /// <value>The list view.</value>
        public ListView ListView
        {
            get { return listView; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewLayoutManager"/> class.
        /// </summary>
        /// <param name="listView">The list view.</param>
        public ListViewLayoutManager(ListView listView)
        {
            if (listView == null)
            {
                throw new ArgumentNullException("listView");
            }

            this.listView = listView;
            this.listView.Loaded += ListViewLoaded;
            this.listView.Unloaded += ListViewUnloaded;
        }


        private void ListViewLoaded(object sender, RoutedEventArgs e)
        {

            RegisterEvents();
            InitColumns();
            DoResizeColumns();
            loaded = true;
        }

        private void ListViewUnloaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                return;
            }
            UnregisterEvents(listView);
            loaded = false;
        }
        #endregion

        #region Registering/Unregistering  Events

        private bool HasSortedColumn
        {
            get
            {
                var view = listView.View as GridView;
                if (view == null) return false;

                foreach (GridViewColumn gridViewColumn in view.Columns)
                {
                    if (!string.IsNullOrEmpty(SortedColumn.GetSortExpression(gridViewColumn)))
                        return true;
                }
                return false;
            }
        }
        private void RegisterEvents()
        {
            RegisterEvents(listView);

            if (HasSortedColumn)
                listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(GridViewColumnHeaderClickedHandler));
        }

        private void RegisterEvents(DependencyObject start)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                var childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is Thumb)
                {
                    GridViewColumn gridViewColumn = FindParentColumn(childVisual);
                    if (gridViewColumn == null)
                    {
                        continue;
                    }

                    var thumb = childVisual as Thumb;
                    thumb.PreviewMouseMove += ThumbPreviewMouseMove;
                    thumb.PreviewMouseLeftButtonDown += ThumbPreviewMouseLeftButtonDown;
                    DependencyPropertyDescriptor.FromProperty(
                        GridViewColumn.WidthProperty,
                        typeof(GridViewColumn)).AddValueChanged(gridViewColumn, GridColumnWidthChanged);
                }
                else if (childVisual is GridViewColumnHeader)
                {
                    var columnHeader = childVisual as GridViewColumnHeader;
                    columnHeader.SizeChanged += GridColumnHeaderSizeChanged;
                }
                else if (scrollViewer == null && childVisual is ScrollViewer)
                {
                    scrollViewer = childVisual as ScrollViewer;
                    scrollViewer.ScrollChanged += ScrollViewerScrollChanged;
                    // assume we do the regulation of the horizontal scrollbar
                    scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    scrollViewer.VerticalScrollBarVisibility = verticalScrollBarVisibility;
                }

                RegisterEvents(childVisual); // recursive
            }
        } // RegisterEvents

        // ----------------------------------------------------------------------
        private void UnregisterEvents(DependencyObject start)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                var childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is Thumb)
                {
                    GridViewColumn gridViewColumn = FindParentColumn(childVisual);
                    if (gridViewColumn == null)
                    {
                        continue;
                    }

                    var thumb = childVisual as Thumb;
                    thumb.PreviewMouseMove -= ThumbPreviewMouseMove;
                    thumb.PreviewMouseLeftButtonDown -= ThumbPreviewMouseLeftButtonDown;
                    DependencyPropertyDescriptor.FromProperty(
                        GridViewColumn.WidthProperty,
                        typeof(GridViewColumn)).RemoveValueChanged(gridViewColumn, GridColumnWidthChanged);
                }
                else if (childVisual is GridViewColumnHeader)
                {
                    var columnHeader = childVisual as GridViewColumnHeader;
                    columnHeader.SizeChanged -= GridColumnHeaderSizeChanged;
                }
                else if (scrollViewer == null && childVisual is ScrollViewer)
                {
                    scrollViewer = childVisual as ScrollViewer;
                    scrollViewer.ScrollChanged -= ScrollViewerScrollChanged;
                }

                UnregisterEvents(childVisual); // recursive
            }
        } // UnregisterEvents 
        #endregion

        // ----------------------------------------------------------------------
        private GridViewColumn FindParentColumn(DependencyObject element)
        {
            if (element == null)
            {
                return null;
            }

            while (element != null)
            {
                if (element is GridViewColumnHeader)
                {
                    return ((GridViewColumnHeader)element).Column;
                }
                element = VisualTreeHelper.GetParent(element);
            }

            return null;
        } // FindParentColumn

        // ----------------------------------------------------------------------
        private GridViewColumnHeader FindColumnHeader(DependencyObject start, GridViewColumn gridViewColumn)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                var childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is GridViewColumnHeader)
                {
                    var gridViewHeader = childVisual as GridViewColumnHeader;
                    if (gridViewHeader.Column == gridViewColumn)
                    {
                        return gridViewHeader;
                    }
                }
                GridViewColumnHeader childGridViewHeader = FindColumnHeader(childVisual, gridViewColumn); // recursive
                if (childGridViewHeader != null)
                {
                    return childGridViewHeader;
                }
            }
            return null;
        } // FindColumnHeader

        // ----------------------------------------------------------------------
        private void InitColumns()
        {
            var view = listView.View as GridView;
            if (view == null)
            {
                return;
            }

            foreach (GridViewColumn gridViewColumn in view.Columns)
            {
                if (RangeColumn.IsRangeColumn(gridViewColumn))
                {
                    double? minWidth = RangeColumn.GetRangeMinWidth(gridViewColumn);
                    double? maxWidth = RangeColumn.GetRangeMaxWidth(gridViewColumn);
                    if (minWidth.HasValue || maxWidth.HasValue)
                    {
                        GridViewColumnHeader columnHeader = FindColumnHeader(listView, gridViewColumn);
                        if (columnHeader == null)
                        {
                            continue;
                        }
                        if (minWidth.HasValue)
                        {
                            columnHeader.MinWidth = minWidth.Value;
                        }
                        if (maxWidth.HasValue)
                        {
                            columnHeader.MaxWidth = maxWidth.Value;
                        }
                    }
                }
            }
        } // InitColumns

        #region Sorting

        GridViewColumnHeader _lastHeaderClicked = null;

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;


            if (headerClicked == null || headerClicked.Role == GridViewColumnHeaderRole.Padding)
                return;

            var sortExpression = SortedColumn.GetSortExpression(headerClicked.Column);
            var direction = SortedColumn.GetSortDirection(headerClicked.Column);

            if (string.IsNullOrEmpty(sortExpression))
                return;

            if (direction == null)
            {
                direction = ListSortDirection.Ascending;
            }
            else
            {
                direction = (direction == ListSortDirection.Ascending) ? ListSortDirection.Descending :
                ListSortDirection.Ascending;
            }
            SortedColumn.SetSortDirection(headerClicked.Column,direction);
            Sort(sortExpression, direction.Value);

            var templateKey = (direction == ListSortDirection.Ascending)
                                  ? "HeaderTemplateArrowUp"
                                  : "HeaderTemplateArrowDown";
            headerClicked.Column.HeaderTemplate = listView.Resources[templateKey] as DataTemplate;


            // Remove arrow from previously sorted header
            if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
            {
                _lastHeaderClicked.Column.HeaderTemplate = null;
            }


            _lastHeaderClicked = headerClicked;
        }


        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(listView.ItemsSource);

            dataView.SortDescriptions.Clear();

            var sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);

            dataView.Refresh();

            //listResults.ItemsSource = dataView;
        }
        #endregion

        // ----------------------------------------------------------------------
        protected virtual void ResizeColumns()
        {
            var view = listView.View as GridView;
            if (view == null || view.Columns.Count == 0)
            {
                return;
            }

            // listview width
            double actualWidth = double.PositiveInfinity;
            if (scrollViewer != null)
            {
                actualWidth = scrollViewer.ViewportWidth;
            }
            if (double.IsInfinity(actualWidth))
            {
                actualWidth = listView.ActualWidth;
            }
            if (double.IsInfinity(actualWidth) || actualWidth <= 0)
            {
                return;
            }

            double resizeableRegionCount = 0;
            double otherColumnsWidth = 0;
            // determine column sizes
            foreach (GridViewColumn gridViewColumn in view.Columns)
            {
                if (ProportionalColumn.IsProportionalColumn(gridViewColumn))
                {
                    resizeableRegionCount += ProportionalColumn.GetProportionalWidth(gridViewColumn).Value;
                }
                else
                {
                    otherColumnsWidth += gridViewColumn.ActualWidth;
                }
            }

            if (resizeableRegionCount <= 0)
            {
                // no proportional columns present: commit the regulation to the scroll viewer
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

                // search the first fill column
                GridViewColumn fillColumn = null;
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    GridViewColumn gridViewColumn = view.Columns[i];
                    if (IsFillColumn(gridViewColumn))
                    {
                        fillColumn = gridViewColumn;
                        break;
                    }
                }

                if (fillColumn != null)
                {
                    double otherColumnsWithoutFillWidth = otherColumnsWidth - fillColumn.ActualWidth;
                    double fillWidth = actualWidth - otherColumnsWithoutFillWidth;
                    if (fillWidth > 0)
                    {
                        double? minWidth = RangeColumn.GetRangeMinWidth(fillColumn);
                        double? maxWidth = RangeColumn.GetRangeMaxWidth(fillColumn);

                        bool setWidth = true;
                        if (minWidth.HasValue && fillWidth < minWidth.Value)
                        {
                            setWidth = false;
                        }
                        if (maxWidth.HasValue && fillWidth > maxWidth.Value)
                        {
                            setWidth = false;
                        }
                        if (setWidth)
                        {
                            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                            fillColumn.Width = fillWidth;
                        }
                    }
                }
                return;
            }

            double resizeableColumnsWidth = actualWidth - otherColumnsWidth;
            if (resizeableColumnsWidth <= 0)
            {
                return; // missing space
            }

            // resize columns
            double resizeableRegionWidth = resizeableColumnsWidth / resizeableRegionCount;
            foreach (GridViewColumn gridViewColumn in view.Columns)
            {
                if (ProportionalColumn.IsProportionalColumn(gridViewColumn))
                {
                    gridViewColumn.Width = ProportionalColumn.GetProportionalWidth(gridViewColumn).Value *
                                           resizeableRegionWidth;
                }
            }
        } // ResizeColumns

        // ----------------------------------------------------------------------
        // returns the delta
        private double SetRangeColumnToBounds(GridViewColumn gridViewColumn)
        {
            double startWidth = gridViewColumn.Width;

            double? minWidth = RangeColumn.GetRangeMinWidth(gridViewColumn);
            double? maxWidth = RangeColumn.GetRangeMaxWidth(gridViewColumn);

            if ((minWidth.HasValue && maxWidth.HasValue) && (minWidth > maxWidth))
            {
                return 0; // invalid case
            }

            if (minWidth.HasValue && gridViewColumn.Width < minWidth.Value)
            {
                gridViewColumn.Width = minWidth.Value;
            }
            else if (maxWidth.HasValue && gridViewColumn.Width > maxWidth.Value)
            {
                gridViewColumn.Width = maxWidth.Value;
            }

            return gridViewColumn.Width - startWidth;
        } // SetRangeColumnToBounds

        // ----------------------------------------------------------------------
        private bool IsFillColumn(GridViewColumn gridViewColumn)
        {
            if (gridViewColumn == null)
            {
                return false;
            }

            var view = listView.View as GridView;
            if (view == null || view.Columns.Count == 0)
            {
                return false;
            }

            bool? isFillCoumn = RangeColumn.GetRangeIsFillColumn(gridViewColumn);
            return isFillCoumn.HasValue && isFillCoumn.Value;
        } // IsFillColumn

        // ----------------------------------------------------------------------
        private void DoResizeColumns()
        {
            if (resizing)
            {
                return;
            }

            resizing = true;
            try
            {
                ResizeColumns();
            }
            catch
            {
                throw;
            }
            finally
            {
                resizing = false;
            }
        } // DoResizeColumns


        // ----------------------------------------------------------------------
        private void ThumbPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var thumb = sender as Thumb;
            GridViewColumn gridViewColumn = FindParentColumn(thumb);
            if (gridViewColumn == null)
            {
                return;
            }

            // suppress column resizing for proportional, fixed and range fill columns
            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                FixedColumn.IsFixedColumn(gridViewColumn) ||
                IsFillColumn(gridViewColumn))
            {
                thumb.Cursor = null;
                return;
            }

            // check range column bounds
            if (thumb.IsMouseCaptured && RangeColumn.IsRangeColumn(gridViewColumn))
            {
                double? minWidth = RangeColumn.GetRangeMinWidth(gridViewColumn);
                double? maxWidth = RangeColumn.GetRangeMaxWidth(gridViewColumn);

                if ((minWidth.HasValue && maxWidth.HasValue) && (minWidth > maxWidth))
                {
                    return; // invalid case
                }

                if (resizeCursor == null)
                {
                    resizeCursor = thumb.Cursor; // save the resize cursor
                }

                if (minWidth.HasValue && gridViewColumn.Width <= minWidth.Value)
                {
                    thumb.Cursor = Cursors.No;
                }
                else if (maxWidth.HasValue && gridViewColumn.Width >= maxWidth.Value)
                {
                    thumb.Cursor = Cursors.No;
                }
                else
                {
                    thumb.Cursor = resizeCursor; // between valid min/max
                }
            }
        } // ThumbPreviewMouseMove

        // ----------------------------------------------------------------------
        private void ThumbPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var thumb = sender as Thumb;
            GridViewColumn gridViewColumn = FindParentColumn(thumb);

            // suppress column resizing for proportional, fixed and range fill columns
            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                FixedColumn.IsFixedColumn(gridViewColumn) ||
                IsFillColumn(gridViewColumn))
            {
                e.Handled = true;
                return;
            }
        } // ThumbPreviewMouseLeftButtonDown

        // ----------------------------------------------------------------------
        private void GridColumnWidthChanged(object sender, EventArgs e)
        {
            if (!loaded)
            {
                return;
            }

            var gridViewColumn = sender as GridViewColumn;

            // suppress column resizing for proportional and fixed columns
            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) || FixedColumn.IsFixedColumn(gridViewColumn))
            {
                return;
            }

            // ensure range column within the bounds
            if (RangeColumn.IsRangeColumn(gridViewColumn))
            {
                // special case: auto column width - maybe conflicts with min/max range
                if (gridViewColumn.Width.Equals(double.NaN))
                {
                    autoSizedColumn = gridViewColumn;
                    return; // handled by the change header size event
                }

                // ensure column bounds
                if (SetRangeColumnToBounds(gridViewColumn) != 0)
                {
                    return;
                }
            }

            DoResizeColumns();
        } // GridColumnWidthChanged

        // ----------------------------------------------------------------------
        // handle autosized column
        private void GridColumnHeaderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (autoSizedColumn == null)
            {
                return;
            }

            var gridViewColumnHeader = sender as GridViewColumnHeader;
            if (gridViewColumnHeader.Column == autoSizedColumn)
            {
                if (gridViewColumnHeader.Width.Equals(double.NaN))
                {
                    // sync column with 
                    gridViewColumnHeader.Column.Width = gridViewColumnHeader.ActualWidth;
                    DoResizeColumns();
                }

                autoSizedColumn = null;
            }
        } // GridColumnHeaderSizeChanged

        // ----------------------------------------------------------------------
        private void ScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (loaded && e.ViewportWidthChange != 0)
            {
                DoResizeColumns();
            }
        } // ScrollViewerScrollChanged

        // ----------------------------------------------------------------------
        private static void OnLayoutManagerEnabledChanged(DependencyObject dependencyObject,
                                                          DependencyPropertyChangedEventArgs e)
        {
            var listView = dependencyObject as ListView;
            if (listView != null)
            {
                var enabled = (bool)e.NewValue;
                if (enabled)
                {
                    new ListViewLayoutManager(listView);
                }
            }
        } // OnLayoutManagerEnabledChanged


    }
}
