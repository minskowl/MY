using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Savchin.Wpf.Controls.DataGrid.Filtering.Core;
using Savchin.Wpf.Controls.DataGrid.Filtering.Core.Querying;


namespace Savchin.Wpf.Controls.DataGrid.Filtering
{
    /// <summary>
    /// DataGridColumnFilter
    /// </summary>
    public class DataGridColumnFilter : Control
    {
        #region Properties
        /// <summary>
        /// Gets or sets the filter current data.
        /// </summary>
        /// <value>The filter current data.</value>
        public FilterData FilterCurrentData
        {
            get { return (FilterData)GetValue(FilterCurrentDataProperty); }
            set { SetValue(FilterCurrentDataProperty, value); }
        }

        public static readonly DependencyProperty FilterCurrentDataProperty =
            DependencyProperty.Register("FilterCurrentData", typeof(FilterData), typeof(DataGridColumnFilter));

        public DataGridColumnHeader AssignedDataGridColumnHeader
        {
            get { return (DataGridColumnHeader)GetValue(AssignedDataGridColumnHeaderProperty); }
            set { SetValue(AssignedDataGridColumnHeaderProperty, value); }
        }

        public static readonly DependencyProperty AssignedDataGridColumnHeaderProperty =
            DependencyProperty.Register("AssignedDataGridColumnHeader", typeof(DataGridColumnHeader), typeof(DataGridColumnFilter));

        /// <summary>
        /// Gets or sets the assigned data grid column.
        /// </summary>
        /// <value>The assigned data grid column.</value>
        public DataGridColumn AssignedDataGridColumn
        {
            get { return (DataGridColumn)GetValue(AssignedDataGridColumnProperty); }
            set { SetValue(AssignedDataGridColumnProperty, value); }
        }

        public static readonly DependencyProperty AssignedDataGridColumnProperty =
            DependencyProperty.Register("AssignedDataGridColumn", typeof(DataGridColumn), typeof(DataGridColumnFilter));

        /// <summary>
        /// Gets or sets the data grid.
        /// </summary>
        /// <value>The data grid.</value>
        public System.Windows.Controls.DataGrid DataGrid
        {
            get { return (System.Windows.Controls.DataGrid)GetValue(DataGridProperty); }
            set { SetValue(DataGridProperty, value); }
        }

        public static readonly DependencyProperty DataGridProperty =
            DependencyProperty.Register("DataGrid", typeof(System.Windows.Controls.DataGrid), typeof(DataGridColumnFilter));

        public IEnumerable DataGridItemsSource
        {
            get { return (IEnumerable)GetValue(DataGridItemsSourceProperty); }
            set { SetValue(DataGridItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty DataGridItemsSourceProperty =
            DependencyProperty.Register("DataGridItemsSource", typeof(IEnumerable), typeof(DataGridColumnFilter));

        //public bool IsFilteringInProgress
        //{
        //    get { return (bool)GetValue(IsFilteringInProgressProperty); }
        //    set { SetValue(IsFilteringInProgressProperty, value); }
        //}

        //public static readonly DependencyProperty IsFilteringInProgressProperty =
        //    DependencyProperty.Register("IsFilteringInProgress", typeof(bool), typeof(DataGridColumnFilter));

        public FilterType FilterType { get { return FilterCurrentData != null ? FilterCurrentData.Type : FilterType.Text; } }

        public bool IsTextFilterControl
        {
            get { return (bool)GetValue(IsTextFilterControlProperty); }
            set { SetValue(IsTextFilterControlProperty, value); }
        }

        public static readonly DependencyProperty IsTextFilterControlProperty =
            DependencyProperty.Register("IsTextFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsNumericFilterControl
        {
            get { return (bool)GetValue(IsNumericFilterControlProperty); }
            set { SetValue(IsNumericFilterControlProperty, value); }
        }
        public static readonly DependencyProperty IsNumericFilterControlProperty =
            DependencyProperty.Register("IsNumericFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsNumericBetweenFilterControl
        {
            get { return (bool)GetValue(IsNumericBetweenFilterControlProperty); }
            set { SetValue(IsNumericBetweenFilterControlProperty, value); }
        }
        public static readonly DependencyProperty IsNumericBetweenFilterControlProperty =
            DependencyProperty.Register("IsNumericBetweenFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsBooleanFilterControl
        {
            get { return (bool)GetValue(IsBooleanFilterControlProperty); }
            set { SetValue(IsBooleanFilterControlProperty, value); }
        }
        public static readonly DependencyProperty IsBooleanFilterControlProperty =
            DependencyProperty.Register("IsBooleanFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsListFilterControl
        {
            get { return (bool)GetValue(IsListFilterControlProperty); }
            set { SetValue(IsListFilterControlProperty, value); }
        }
        public static readonly DependencyProperty IsListFilterControlProperty =
            DependencyProperty.Register("IsListFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsDateTimeFilterControl
        {
            get { return (bool)GetValue(IsDateTimeFilterControlProperty); }
            set { SetValue(IsDateTimeFilterControlProperty, value); }
        }
        public static readonly DependencyProperty IsDateTimeFilterControlProperty =
            DependencyProperty.Register("IsDateTimeFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsDateTimeBetweenFilterControl
        {
            get { return (bool)GetValue(IsDateTimeBetweenFilterControlProperty); }
            set { SetValue(IsDateTimeBetweenFilterControlProperty, value); }
        }
        public static readonly DependencyProperty IsDateTimeBetweenFilterControlProperty =
            DependencyProperty.Register("IsDateTimeBetweenFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsFirstFilterControl
        {
            get { return (bool)GetValue(IsFirstFilterControlProperty); }
            set { SetValue(IsFirstFilterControlProperty, value); }
        }
        public static readonly DependencyProperty IsFirstFilterControlProperty =
            DependencyProperty.Register("IsFirstFilterControl", typeof(bool), typeof(DataGridColumnFilter));

        public bool IsControlInitialized
        {
            get { return (bool)GetValue(IsControlInitializedProperty); }
            set { SetValue(IsControlInitializedProperty, value); }
        }
        public static readonly DependencyProperty IsControlInitializedProperty =
            DependencyProperty.Register("IsControlInitialized", typeof(bool), typeof(DataGridColumnFilter));
        #endregion

        /// <summary>
        /// Initializes the <see cref="DataGridColumnFilter"/> class.
        /// </summary>
        static DataGridColumnFilter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGridColumnFilter), new FrameworkPropertyMetadata(typeof(DataGridColumnFilter)));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridColumnFilter"/> class.
        /// </summary>
        public  DataGridColumnFilter()
        {
            
        }
        #region Overrides
        /// <summary>
        /// Invoked whenever the effective value of any dependency property on this <see cref="T:System.Windows.FrameworkElement"/> has been updated. The specific dependency property that changed is reported in the arguments parameter. Overrides <see cref="M:System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)"/>.
        /// </summary>
        /// <param name="e">The event data that describes the property that changed, as well as old and new values.</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == DataGridItemsSourceProperty
                && e.OldValue != e.NewValue
                && AssignedDataGridColumn != null
                && DataGrid != null
                && AssignedDataGridColumn is DataGridColumn)
            {
                Initialize();

                FilterCurrentData.IsRefresh = true;//query optimization filed

                FilterCurrentDataFilterChangedEvent(this, EventArgs.Empty);//init query

                FilterCurrentData.FilterChangedEvent -= FilterCurrentDataFilterChangedEvent;
                FilterCurrentData.FilterChangedEvent += FilterCurrentDataFilterChangedEvent;
            }

            base.OnPropertyChanged(e);
        }
        #endregion



        #region Initialization
        private void Initialize()
        {
            if (DataGridItemsSource != null && AssignedDataGridColumn != null && DataGrid != null)
            {
                InitFilterData();

                InitControlType();

                HandleListFilterType();

                HookUpCommands();

                IsControlInitialized = true;
            }
        }

        private void InitFilterData()
        {
            if (FilterCurrentData != null && FilterCurrentData.IsTypeInitialized) return;

            var valuePropertyBindingPath = GetValuePropertyBindingPath(AssignedDataGridColumn);

            bool typeInitialized;

            var valuePropertyType = getValuePropertyType(
                valuePropertyBindingPath, GetItemSourceElementType(out typeInitialized));

            var filterType = FilterTypeHelper.GetFilterType(
                valuePropertyType,
                AssignedDataGridColumn is DataGridComboBoxColumn,
                DataGridColumnExtensions.GetIsBetweenFilterControl(AssignedDataGridColumn));


            string queryString = String.Empty;
            string queryStringTo = String.Empty;

            FilterCurrentData = new FilterData(
                FilterOperator.Undefined,
                filterType,
                valuePropertyBindingPath,
                valuePropertyType,
                queryString,
                queryStringTo,
                typeInitialized,
                DataGridColumnExtensions.GetIsCaseSensitiveSearch(AssignedDataGridColumn));
        }

        private void InitControlType()
        {
            IsFirstFilterControl = false;

            IsTextFilterControl = false;
            IsNumericFilterControl = false;
            IsBooleanFilterControl = false;
            IsListFilterControl = false;
            IsDateTimeFilterControl = false;

            IsNumericBetweenFilterControl = false;
            IsDateTimeBetweenFilterControl = false;

            if (FilterType == FilterType.Text)
            {
                IsTextFilterControl = true;
            }
            else if (FilterType == FilterType.Numeric)
            {
                IsNumericFilterControl = true;
            }
            else if (FilterType == FilterType.Boolean)
            {
                IsBooleanFilterControl = true;
            }
            else if (FilterType == FilterType.List)
            {
                IsListFilterControl = true;
            }
            else if (FilterType == FilterType.DateTime)
            {
                IsDateTimeFilterControl = true;
            }
            else if (FilterType == FilterType.NumericBetween)
            {
                IsNumericBetweenFilterControl = true;
            }
            else if (FilterType == FilterType.DateTimeBetween)
            {
                IsDateTimeBetweenFilterControl = true;
            }
        }

        private void HandleListFilterType()
        {
            if (FilterCurrentData.Type != FilterType.List) return;

            var comboBox = this.Template.FindName("PART_ComboBoxFilter", this) as ComboBox;
            var column = AssignedDataGridColumn as DataGridComboBoxColumn;
            if (comboBox == null || column == null) return;

            if (DataGridComboBoxExtensions.GetIsTextFilter(column))
            {
                FilterCurrentData.Type = FilterType.Text;
                InitControlType();
            }
            else //list filter type
            {
                BindingOperations.SetBinding(comboBox, ComboBox.ItemsSourceProperty,
                                             BindingOperations.GetBinding(column, ComboBox.ItemsSourceProperty));

                comboBox.RequestBringIntoView += SetComboBindingAndHanldeUnsetValue;
            }
        }

        private void SetComboBindingAndHanldeUnsetValue(object sender, RequestBringIntoViewEventArgs e)
        {
            var combo = sender as ComboBox;

            IList list = combo.ItemsSource.Cast<object>().ToList();

            if (list.Count > 0 && list[0] != DependencyProperty.UnsetValue)
            {
                combo.RequestBringIntoView -= SetComboBindingAndHanldeUnsetValue;

                list.Insert(0, DependencyProperty.UnsetValue);

                var column = AssignedDataGridColumn as DataGridComboBoxColumn;

                combo.DisplayMemberPath = column.DisplayMemberPath;
                combo.SelectedValuePath = column.SelectedValuePath;

                combo.ItemsSource = list;
            }
        }

        private static string GetValuePropertyBindingPath(DataGridColumn column)
        {
            string path = String.Empty;

            if (column is DataGridBoundColumn)
            {
                var bc = column as DataGridBoundColumn;
                path = (bc.Binding as Binding).Path.Path;
            }
            else if (column is DataGridTemplateColumn)
            {
                var tc = column as DataGridTemplateColumn;

                object templateContent = tc.CellTemplate.LoadContent();

                if (templateContent != null && templateContent is TextBlock)
                {
                    var block = templateContent as TextBlock;

                    BindingExpression binding = block.GetBindingExpression(TextBlock.TextProperty);

                    path = binding.ParentBinding.Path.Path;
                }
            }
            else if (column is DataGridComboBoxColumn)
            {
                var comboColumn = column as DataGridComboBoxColumn;

                path = null;

                var binding = ((comboColumn.SelectedValueBinding) as Binding);

                if (binding == null)
                {
                    binding = ((comboColumn.SelectedItemBinding) as Binding);
                }

                if (binding == null)
                {
                    binding = comboColumn.SelectedValueBinding as Binding;
                }

                if (binding != null)
                {
                    path = binding.Path.Path;
                }

                if (comboColumn.SelectedItemBinding != null && comboColumn.SelectedValueBinding == null)
                {
                    if (path != null && path.Trim().Length > 0)
                    {
                        if (DataGridComboBoxExtensions.GetIsTextFilter(comboColumn))
                        {
                            path += "." + comboColumn.DisplayMemberPath;
                        }
                        else
                        {
                            path += "." + comboColumn.SelectedValuePath;
                        }
                    }
                }
            }

            return path;
        }

        private Type getValuePropertyType(string path, Type elementType)
        {
            if (elementType == null) return typeof(object);

            var properties = path.Split(".".ToCharArray()[0]);

            PropertyInfo pi;

            if (properties.Length == 1)
            {
                pi = elementType.GetProperty(path);
            }
            else
            {
                pi = elementType.GetProperty(properties[0]);

                for (int i = 1; i < properties.Length; i++)
                {
                    if (pi != null)
                    {
                        pi = pi.PropertyType.GetProperty(properties[i]);
                    }
                }
            }


            return pi != null ? pi.PropertyType : typeof(object);
        }

        private Type GetItemSourceElementType(out bool typeInitialized)
        {
            typeInitialized = false;

            Type elementType = null;

            var l = (DataGridItemsSource as IList);

            if (l != null && l.Count > 0)
            {
                object obj = l[0];

                if (obj != null)
                {
                    elementType = l[0].GetType();
                    typeInitialized = true;
                }
                else
                {
                    elementType = typeof(object);
                }
            }
            if (l == null)
            {
                var lw = (DataGridItemsSource as ListCollectionView);

                if (lw != null && lw.Count > 0)
                {
                    object obj = lw.CurrentItem;

                    if (obj != null)
                    {
                        elementType = lw.CurrentItem.GetType();
                        typeInitialized = true;
                    }
                    else
                    {
                        elementType = typeof(object);
                    }
                }
            }

            return elementType;
        }


        private void HookUpCommands()
        {
            if (DataGridExtensions.GetClearFilterCommand(DataGrid) == null)
            {
                DataGridExtensions.SetClearFilterCommand(
                    DataGrid, new DataGridFilterCommand(ClearQuery));
            }
        }
        #endregion

        #region Querying
        void FilterCurrentDataFilterChangedEvent(object sender, EventArgs e)
        {
            if (DataGrid == null) return;

            var query = QueryControllerFactory.GetQueryController(DataGrid, FilterCurrentData, DataGridItemsSource);

            //AddFilterStateHandlers(query);

            query.DoQuery();

            IsFirstFilterControl = query.IsCurentControlFirstControl;
        }

        private void ClearQuery(object parameter)
        {
            if (DataGrid == null) return;

            var query = QueryControllerFactory.GetQueryController(
                DataGrid, FilterCurrentData, DataGridItemsSource);

            query.ClearFilter();
        }

        //private void AddFilterStateHandlers(QueryController query)
        //{
        //    query.FilteringStarted -= QueryFilteringStarted;
        //    query.FilteringFinished -= QueryFilteringFinished;

        //    query.FilteringStarted += QueryFilteringStarted;
        //    query.FilteringFinished += QueryFilteringFinished;
        //}

        //void QueryFilteringFinished(object sender, EventArgs e)
        //{
        //    var senderData = ((QueryController)sender).ColumnFilterData;
        //    if (Equals(FilterCurrentData, senderData))
        //    {
        //        IsFilteringInProgress = false;
        //    }
        //}

        //void QueryFilteringStarted(object sender, EventArgs e)
        //{
        //    var senderData=((QueryController) sender).ColumnFilterData;
        //    if (Equals(FilterCurrentData, senderData))
        //    {
        //        IsFilteringInProgress = true;
        //    }
        //}
        #endregion
    }
}