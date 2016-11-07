using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Savchin.Collection;
using Savchin.Collection.Sorting;
using Savchin.Text;


namespace Savchin.Web.UI
{



    /// <summary>
    /// ControlDataSourceView
    /// </summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
     AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ControlDataSourceView : DataSourceView, IStateManager
    {
        #region Fields


        private readonly ControlDataSource _owner;
        private readonly HttpContext _context;



        private ConflictOptions _conflictDetection;
        private bool _convertNullToDBNull;
        private string _dataObjectTypeName;
        private string _deleteMethod;
        private ParameterCollection _deleteParameters;
        private bool _enablePaging;
        private string _filterExpression;
        private ParameterCollection _filterParameters;
        private string _insertMethod;
        private ParameterCollection _insertParameters;
        private string _maximumRowsParameterName;
        private string _oldValuesParameterFormatString;
        private string _selectCountMethod;
        private string _selectMethod;
        private ParameterCollection _selectParameters;
        private string _sortParameterName;
        private string _startRowIndexParameterName;
        private bool _tracking;
        private string _updateMethod;
        private ParameterCollection _updateParameters;
        private static readonly object EventDeleted = new object();
        private static readonly object EventDeleting = new object();
        private static readonly object EventFiltering = new object();
        private static readonly object EventInserted = new object();
        private static readonly object EventInserting = new object();
        private static readonly object EventObjectCreated = new object();
        private static readonly object EventObjectCreating = new object();
        private static readonly object EventObjectDisposing = new object();
        private static readonly object EventSelected = new object();
        private static readonly object EventSelecting = new object();
        private static readonly object EventUpdated = new object();
        private static readonly object EventUpdating = new object();
        #endregion

        #region Properties
        // Properties
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView"/> object associated with the current <see cref="T:System.Web.UI.DataSourceControl"/> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)"/> operation.
        /// </summary>
        /// <value></value>
        /// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
        public override bool CanDelete
        {
            get { return (DeleteMethod.Length != 0); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView"/> object associated with the current <see cref="T:System.Web.UI.DataSourceControl"/> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)"/> operation.
        /// </summary>
        /// <value></value>
        /// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
        public override bool CanInsert
        {
            get { return (InsertMethod.Length != 0); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView"/> object associated with the current <see cref="T:System.Web.UI.DataSourceControl"/> object supports paging through the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)"/> method.
        /// </summary>
        /// <value></value>
        /// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
        public override bool CanPage
        {
            get { return EnablePaging; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView"/> object associated with the current <see cref="T:System.Web.UI.DataSourceControl"/> object supports retrieving the total number of data rows, instead of the data.
        /// </summary>
        /// <value></value>
        /// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
        public override bool CanRetrieveTotalRowCount
        {
            get
            {
                if (SelectCountMethod.Length <= 0)
                {
                    return !EnablePaging;
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView"/> object associated with the current <see cref="T:System.Web.UI.DataSourceControl"/> object supports a sorted view on the underlying data source.
        /// </summary>
        /// <value></value>
        /// <returns>true if the operation is supported; otherwise, false. The default implementation returns false.</returns>
        public override bool CanSort
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView"/> object associated with the current <see cref="T:System.Web.UI.DataSourceControl"/> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)"/> operation.
        /// </summary>
        /// <value></value>
        /// <returns>true if the operation is supported; otherwise, false. The default implementation returns false.</returns>
        public override bool CanUpdate
        {
            get { return (UpdateMethod.Length != 0); }
        }

        /// <summary>
        /// Gets or sets the conflict detection.
        /// </summary>
        /// <value>The conflict detection.</value>
        public ConflictOptions ConflictDetection
        {
            get { return _conflictDetection; }
            set
            {
                if ((value < ConflictOptions.OverwriteChanges) || (value > ConflictOptions.CompareAllValues))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _conflictDetection = value;
                OnDataSourceViewChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [convert null to DB null].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [convert null to DB null]; otherwise, <c>false</c>.
        /// </value>
        public bool ConvertNullToDBNull
        {
            get { return _convertNullToDBNull; }
            set { _convertNullToDBNull = value; }
        }

        /// <summary>
        /// Gets or sets the name of the data object type.
        /// </summary>
        /// <value>The name of the data object type.</value>
        public string DataObjectTypeName
        {
            get
            {
                if (_dataObjectTypeName == null)
                {
                    return string.Empty;
                }
                return _dataObjectTypeName;
            }
            set
            {
                if (DataObjectTypeName != value)
                {
                    _dataObjectTypeName = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the delete method.
        /// </summary>
        /// <value>The delete method.</value>
        public string DeleteMethod
        {
            get
            {
                if (_deleteMethod == null)
                {
                    return string.Empty;
                }
                return _deleteMethod;
            }
            set { _deleteMethod = value; }
        }

        /// <summary>
        /// Gets the delete parameters.
        /// </summary>
        /// <value>The delete parameters.</value>
        public ParameterCollection DeleteParameters
        {
            get
            {
                if (_deleteParameters == null)
                {
                    _deleteParameters = new ParameterCollection();
                }
                return _deleteParameters;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable paging].
        /// </summary>
        /// <value><c>true</c> if [enable paging]; otherwise, <c>false</c>.</value>
        public bool EnablePaging
        {
            get { return _enablePaging; }
            set
            {
                if (EnablePaging != value)
                {
                    _enablePaging = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
        /// <value>The filter expression.</value>
        public string FilterExpression
        {
            get
            {
                if (_filterExpression == null)
                {
                    return string.Empty;
                }
                return _filterExpression;
            }
            set
            {
                if (FilterExpression != value)
                {
                    _filterExpression = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets the filter parameters.
        /// </summary>
        /// <value>The filter parameters.</value>
        public ParameterCollection FilterParameters
        {
            get
            {
                if (_filterParameters == null)
                {
                    _filterParameters = new ParameterCollection();
                    _filterParameters.ParametersChanged += new EventHandler(SelectParametersChangedEventHandler);
                    if (_tracking)
                    {
                        ((IStateManager)_filterParameters).TrackViewState();
                    }
                }
                return _filterParameters;
            }
        }

        /// <summary>
        /// Gets or sets the insert method.
        /// </summary>
        /// <value>The insert method.</value>
        public string InsertMethod
        {
            get
            {
                if (_insertMethod == null)
                {
                    return string.Empty;
                }
                return _insertMethod;
            }
            set { _insertMethod = value; }
        }



        /// <summary>
        /// Gets the insert parameters.
        /// </summary>
        /// <value>The insert parameters.</value>
        public ParameterCollection InsertParameters
        {
            get
            {
                if (_insertParameters == null)
                {
                    _insertParameters = new ParameterCollection();
                }
                return _insertParameters;
            }
        }

        /// <summary>
        /// When implemented by a class, gets a value indicating whether a server control is tracking its view state changes.
        /// </summary>
        /// <value></value>
        /// <returns>true if a server control is tracking its view state changes; otherwise, false.
        /// </returns>
        protected bool IsTrackingViewState
        {
            get { return _tracking; }
        }

        /// <summary>
        /// Gets or sets the maximum name of the rows parameter.
        /// </summary>
        /// <value>The maximum name of the rows parameter.</value>
        public string MaximumRowsParameterName
        {
            get
            {
                if (_maximumRowsParameterName == null)
                {
                    return "maximumRows";
                }
                return _maximumRowsParameterName;
            }
            set
            {
                if (MaximumRowsParameterName != value)
                {
                    _maximumRowsParameterName = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the old values parameter format string.
        /// </summary>
        /// <value>The old values parameter format string.</value>
        [Category("Data"), Description("DataSource_OldValuesParameterFormatString"), DefaultValue("{0}")]
        public string OldValuesParameterFormatString
        {
            get
            {
                if (_oldValuesParameterFormatString == null)
                {
                    return "{0}";
                }
                return _oldValuesParameterFormatString;
            }
            set
            {
                _oldValuesParameterFormatString = value;
                OnDataSourceViewChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the select count method.
        /// </summary>
        /// <value>The select count method.</value>
        public string SelectCountMethod
        {
            get
            {
                if (_selectCountMethod == null)
                {
                    return string.Empty;
                }
                return _selectCountMethod;
            }
            set
            {
                if (SelectCountMethod != value)
                {
                    _selectCountMethod = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the select method.
        /// </summary>
        /// <value>The select method.</value>
        public string SelectMethod
        {
            get
            {
                if (_selectMethod == null)
                {
                    return string.Empty;
                }
                return _selectMethod;
            }
            set
            {
                if (SelectMethod != value)
                {
                    _selectMethod = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets the select parameters.
        /// </summary>
        /// <value>The select parameters.</value>
        public ParameterCollection SelectParameters
        {
            get
            {
                if (_selectParameters == null)
                {
                    _selectParameters = new ParameterCollection();
                    _selectParameters.ParametersChanged += new EventHandler(SelectParametersChangedEventHandler);
                    if (_tracking)
                    {
                        ((IStateManager)_selectParameters).TrackViewState();
                    }
                }
                return _selectParameters;
            }
        }

        /// <summary>
        /// Gets or sets the name of the sort parameter.
        /// </summary>
        /// <value>The name of the sort parameter.</value>
        public string SortParameterName
        {
            get
            {
                if (_sortParameterName == null)
                {
                    return string.Empty;
                }
                return _sortParameterName;
            }
            set
            {
                if (SortParameterName != value)
                {
                    _sortParameterName = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the start name of the row index parameter.
        /// </summary>
        /// <value>The start name of the row index parameter.</value>
        public string StartRowIndexParameterName
        {
            get
            {
                if (_startRowIndexParameterName == null)
                {
                    return "startRowIndex";
                }
                return _startRowIndexParameterName;
            }
            set
            {
                if (StartRowIndexParameterName != value)
                {
                    _startRowIndexParameterName = value;
                    OnDataSourceViewChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// When implemented by a class, gets a value indicating whether a server control is tracking its view state changes.
        /// </summary>
        /// <value></value>
        /// <returns>true if a server control is tracking its view state changes; otherwise, false.
        /// </returns>
        bool IStateManager.IsTrackingViewState
        {
            get { return IsTrackingViewState; }
        }



        /// <summary>
        /// Gets or sets the update method.
        /// </summary>
        /// <value>The update method.</value>
        public string UpdateMethod
        {
            get
            {
                if (_updateMethod == null)
                {
                    return string.Empty;
                }
                return _updateMethod;
            }
            set { _updateMethod = value; }
        }

        /// <summary>
        /// Gets the update parameters.
        /// </summary>
        /// <value>The update parameters.</value>
        public ParameterCollection UpdateParameters
        {
            get
            {
                if (_updateParameters == null)
                {
                    _updateParameters = new ParameterCollection();
                }
                return _updateParameters;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when [deleted].
        /// </summary>
        public event ObjectDataSourceStatusEventHandler Deleted
        {
            add { Events.AddHandler(EventDeleted, value); }
            remove { Events.RemoveHandler(EventDeleted, value); }
        }

        /// <summary>
        /// Occurs when [deleting].
        /// </summary>
        public event ObjectDataSourceMethodEventHandler Deleting
        {
            add { Events.AddHandler(EventDeleting, value); }
            remove { Events.RemoveHandler(EventDeleting, value); }
        }

        /// <summary>
        /// Occurs when [filtering].
        /// </summary>
        public event ObjectDataSourceFilteringEventHandler Filtering
        {
            add { Events.AddHandler(EventFiltering, value); }
            remove { Events.RemoveHandler(EventFiltering, value); }
        }

        /// <summary>
        /// Occurs when [inserted].
        /// </summary>
        public event ObjectDataSourceStatusEventHandler Inserted
        {
            add { Events.AddHandler(EventInserted, value); }
            remove { Events.RemoveHandler(EventInserted, value); }
        }

        /// <summary>
        /// Occurs when [inserting].
        /// </summary>
        public event ObjectDataSourceMethodEventHandler Inserting
        {
            add { Events.AddHandler(EventInserting, value); }
            remove { Events.RemoveHandler(EventInserting, value); }
        }

        /// <summary>
        /// Occurs when [object created].
        /// </summary>
        public event ObjectDataSourceObjectEventHandler ObjectCreated
        {
            add { Events.AddHandler(EventObjectCreated, value); }
            remove { Events.RemoveHandler(EventObjectCreated, value); }
        }

        /// <summary>
        /// Occurs when [object creating].
        /// </summary>
        public event ObjectDataSourceObjectEventHandler ObjectCreating
        {
            add { Events.AddHandler(EventObjectCreating, value); }
            remove { Events.RemoveHandler(EventObjectCreating, value); }
        }

        /// <summary>
        /// Occurs when [object disposing].
        /// </summary>
        public event ObjectDataSourceDisposingEventHandler ObjectDisposing
        {
            add { Events.AddHandler(EventObjectDisposing, value); }
            remove { Events.RemoveHandler(EventObjectDisposing, value); }
        }

        /// <summary>
        /// Occurs when [selected].
        /// </summary>
        public event ObjectDataSourceStatusEventHandler Selected
        {
            add { Events.AddHandler(EventSelected, value); }
            remove { Events.RemoveHandler(EventSelected, value); }
        }

        /// <summary>
        /// Occurs when [selecting].
        /// </summary>
        public event ObjectDataSourceSelectingEventHandler Selecting
        {
            add { Events.AddHandler(EventSelecting, value); }
            remove { Events.RemoveHandler(EventSelecting, value); }
        }

        /// <summary>
        /// Occurs when [updated].
        /// </summary>
        public event ObjectDataSourceStatusEventHandler Updated
        {
            add { Events.AddHandler(EventUpdated, value); }
            remove { Events.RemoveHandler(EventUpdated, value); }
        }

        /// <summary>
        /// Occurs when [updating].
        /// </summary>
        public event ObjectDataSourceMethodEventHandler Updating
        {
            add { Events.AddHandler(EventUpdating, value); }
            remove { Events.RemoveHandler(EventUpdating, value); }
        }
        #endregion

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlDataSourceView"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="name">The name.</param>
        /// <param name="context">The context.</param>
        public ControlDataSourceView(ControlDataSource owner, string name, HttpContext context)
            : base(owner, name)
        {
            _owner = owner;
            _context = context;
        }


        #region Interface

        /// <summary>
        /// Deletes the specified keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="oldValues">The old values.</param>
        /// <returns></returns>
        public int Delete(IDictionary keys, IDictionary oldValues)
        {
            return ExecuteDelete(keys, oldValues);
        }

        /// <summary>
        /// Inserts the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public int Insert(IDictionary values)
        {
            return ExecuteInsert(values);
        }
        /// <summary>
        /// Selects the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public IEnumerable Select(DataSourceSelectArguments arguments)
        {
            return ExecuteSelect(arguments);
        }

        /// <summary>
        /// Updates the specified keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="values">The values.</param>
        /// <param name="oldValues">The old values.</param>
        /// <returns></returns>
        public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
        {
            return ExecuteUpdate(keys, values, oldValues);
        }
        #endregion

        #region Execute Implementation

        /// <summary>
        /// Performs a delete operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView"/> object represents.
        /// </summary>
        /// <param name="keys">An <see cref="T:System.Collections.IDictionary"/> of object or row keys to be deleted by the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)"/> operation.</param>
        /// <param name="oldValues">An <see cref="T:System.Collections.IDictionary"/> of name/value pairs that represent data elements and their original values.</param>
        /// <returns>
        /// The number of items that were deleted from the underlying data storage.
        /// </returns>
        protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
        {
            ObjectDataSourceMethod method;
            if (!CanDelete)
            {
                throw new NotSupportedException("ObjectDataSourceView_DeleteNotSupported");
            }

            Type dataObjectType = TryGetDataObjectType();
            if (dataObjectType != null)
            {
                IDictionary destination = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                MergeDictionaries(DeleteParameters, keys, destination);
                if (ConflictDetection == ConflictOptions.CompareAllValues)
                {
                    if (oldValues == null)
                    {
                        throw new InvalidOperationException("ObjectDataSourceView_Pessimistic");
                    }
                    MergeDictionaries(DeleteParameters, oldValues, destination);
                }
                object oldDataObject = BuildDataObject(dataObjectType, destination);
                method = GetResolvedMethodData(DeleteMethod, dataObjectType, oldDataObject, null,
                                          DataSourceOperation.Delete);
                ObjectDataSourceMethodEventArgs e = new ObjectDataSourceMethodEventArgs(method.Parameters);
                OnDeleting(e);
                if (e.Cancel)
                {
                    return 0;
                }
            }
            else
            {
                IOrderedDictionary dictionary2 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                string oldValuesParameterFormatString = OldValuesParameterFormatString;
                MergeDictionaries(DeleteParameters, DeleteParameters.GetValues(_context, _owner), dictionary2);
                MergeDictionaries(DeleteParameters, keys, dictionary2, oldValuesParameterFormatString);
                if (ConflictDetection == ConflictOptions.CompareAllValues)
                {
                    if (oldValues == null)
                    {
                        throw new InvalidOperationException("ObjectDataSourceView_Pessimistic");
                    }
                    MergeDictionaries(DeleteParameters, oldValues, dictionary2, oldValuesParameterFormatString);
                }
                ObjectDataSourceMethodEventArgs args2 = new ObjectDataSourceMethodEventArgs(dictionary2);
                OnDeleting(args2);
                if (args2.Cancel)
                {
                    return 0;
                }
                method = GetResolvedMethodData(DeleteMethod, dictionary2, DataSourceOperation.Delete);
            }
            ObjectDataSourceResult result = InvokeMethod(method);
            //if (_owner.Cache.Enabled)
            //{
            //    _owner.InvalidateCacheEntry();
            //}
            OnDataSourceViewChanged(EventArgs.Empty);
            return result.AffectedRows;
        }

        /// <summary>
        /// Performs an insert operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView"/> object represents.
        /// </summary>
        /// <param name="values">An <see cref="T:System.Collections.IDictionary"/> of name/value pairs used during an insert operation.</param>
        /// <returns>
        /// The number of items that were inserted into the underlying data storage.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)"/> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView"/>.
        /// </exception>
        protected override int ExecuteInsert(IDictionary values)
        {
            ObjectDataSourceMethod method;
            if (!CanInsert)
            {
                throw new NotSupportedException("ObjectDataSourceView_InsertNotSupported");
            }

            Type dataObjectType = TryGetDataObjectType();
            if (dataObjectType != null)
            {
                if ((values == null) || (values.Count == 0))
                {
                    throw new InvalidOperationException("ObjectDataSourceView_InsertRequiresValues");
                }
                IDictionary destination = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                MergeDictionaries(InsertParameters, values, destination);
                object newDataObject = BuildDataObject(dataObjectType, destination);
                method = GetResolvedMethodData(InsertMethod, dataObjectType, null, newDataObject,
                                          DataSourceOperation.Insert);
                ObjectDataSourceMethodEventArgs e = new ObjectDataSourceMethodEventArgs(method.Parameters);
                OnInserting(e);
                if (e.Cancel)
                {
                    return 0;
                }
            }
            else
            {
                IOrderedDictionary dictionary2 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                MergeDictionaries(InsertParameters, InsertParameters.GetValues(_context, _owner), dictionary2);
                MergeDictionaries(InsertParameters, values, dictionary2);
                ObjectDataSourceMethodEventArgs args2 = new ObjectDataSourceMethodEventArgs(dictionary2);
                OnInserting(args2);
                if (args2.Cancel)
                {
                    return 0;
                }
                method = GetResolvedMethodData(InsertMethod, dictionary2, DataSourceOperation.Insert);
            }
            ObjectDataSourceResult result = InvokeMethod(method);
            //if (_owner.Cache.Enabled)
            //{
            //    _owner.InvalidateCacheEntry();
            //}
            OnDataSourceViewChanged(EventArgs.Empty);
            return result.AffectedRows;
        }

        /// <summary>
        /// Gets a list of data from the underlying data storage.
        /// </summary>
        /// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments"/> that is used to request operations on the data beyond basic data retrieval.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> list of data from the underlying data storage.
        /// </returns>
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
        {
            if (SelectMethod.Length == 0)
            {
                throw new InvalidOperationException("ObjectDataSourceView_SelectNotSupported");
            }
            if (CanSort)
            {
                arguments.AddSupportedCapabilities(DataSourceCapabilities.Sort);
            }
            if (CanPage)
            {
                arguments.AddSupportedCapabilities(DataSourceCapabilities.Page);
            }
            if (CanRetrieveTotalRowCount)
            {
                arguments.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
            }
            arguments.RaiseUnsupportedCapabilitiesError(this);
            IOrderedDictionary parameters = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
            foreach (DictionaryEntry entry in SelectParameters.GetValues(_context, _owner))
            {
                parameters[entry.Key] = entry.Value;
            }
            //bool enabled = _owner.Cache.Enabled;
            //if (enabled)
            //{
            //    object dataObject = _owner.LoadDataFromCache(arguments.StartRowIndex, arguments.MaximumRows);
            //    if (dataObject != null)
            //    {
            //        DataView view = dataObject as DataView;
            //        if (view != null)
            //        {
            //            if (arguments.RetrieveTotalRowCount && (SelectCountMethod.Length == 0))
            //            {
            //                arguments.TotalRowCount = view.Count;
            //            }
            //            if (FilterExpression.Length > 0)
            //            {
            //                throw new NotSupportedException("ObjectDataSourceView_FilterNotSupported");
            //            }
            //            if (string.IsNullOrEmpty(arguments.SortExpression))
            //            {
            //                return view;
            //            }
            //        }
            //        else
            //        {
            //            DataTable table = FilteredDataSetHelper.GetDataTable(_owner, dataObject);
            //            if (table != null)
            //            {
            //                ProcessPagingData(arguments, parameters);
            //                return CreateFilteredDataView(table, arguments.SortExpression, FilterExpression);
            //            }
            //            IEnumerable enumerable = CreateEnumerableData(dataObject, arguments);
            //            ProcessPagingData(arguments, parameters);
            //            return enumerable;
            //        }
            //    }
            //}
            ObjectDataSourceSelectingEventArgs e = new ObjectDataSourceSelectingEventArgs(parameters, arguments, false);
            OnSelecting(e);
            if (e.Cancel)
            {
                return null;
            }
            OrderedDictionary mergedParameters = new OrderedDictionary(parameters.Count);
            foreach (DictionaryEntry entry2 in parameters)
            {
                mergedParameters.Add(entry2.Key, entry2.Value);
            }
            string sortParameterName = SortParameterName;
            if (sortParameterName.Length > 0)
            {
                parameters[sortParameterName] = arguments.SortExpression;
                arguments.SortExpression = string.Empty;
            }
            if (EnablePaging)
            {
                string maximumRowsParameterName = MaximumRowsParameterName;
                string startRowIndexParameterName = StartRowIndexParameterName;
                if (string.IsNullOrEmpty(maximumRowsParameterName) || string.IsNullOrEmpty(startRowIndexParameterName))
                {
                    throw new InvalidOperationException("ObjectDataSourceView_MissingPagingSettings");
                }
                IDictionary source = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                source[maximumRowsParameterName] = arguments.MaximumRows;
                source[startRowIndexParameterName] = arguments.StartRowIndex;
                MergeDictionaries(SelectParameters, source, parameters);
            }


            ObjectDataSourceMethod method = GetResolvedMethodData(SelectMethod, parameters, DataSourceOperation.Select);
            ObjectDataSourceResult result = InvokeMethod(method);
            if (result.ReturnValue == null)
            {
                return null;
            }
            if (arguments.RetrieveTotalRowCount && (SelectCountMethod.Length > 0))
            {
                int totalRowCount = -1;
                //if (enabled)
                //{
                //    totalRowCount = _owner.LoadTotalRowCountFromCache();
                //    if (totalRowCount >= 0)
                //    {
                //        arguments.TotalRowCount = totalRowCount;
                //    }
                //}
                if (totalRowCount < 0)
                {
                    totalRowCount = QueryTotalRowCount(mergedParameters, arguments);
                    arguments.TotalRowCount = totalRowCount;
                    //if (enabled)
                    //{
                    //    _owner.SaveTotalRowCountToCache(totalRowCount);
                    //}
                }
            }

            DataView view = result.ReturnValue as DataView;
            if (view != null)
            {


                if (arguments.RetrieveTotalRowCount && (SelectCountMethod.Length == 0))
                {
                    arguments.TotalRowCount = view.Count;
                }
                if (FilterExpression.Length > 0)
                {
                    throw new NotSupportedException("ObjectDataSourceView_FilterNotSupported");
                }
                if (!string.IsNullOrEmpty(arguments.SortExpression))
                {
                    //if (enabled)
                    //{
                    //    throw new NotSupportedException("ObjectDataSourceView_CacheNotSupportedOnSortedDataView");
                    //}
                    view.Sort = arguments.SortExpression;
                }
                //if (enabled)
                //{
                //    SaveDataAndRowCountToCache(arguments, result.ReturnValue);
                //}
                return view;
            }
            DataTable dataTable = GetDataTable(_owner, result.ReturnValue);
            if (dataTable != null)
            {
                if (arguments.RetrieveTotalRowCount && (SelectCountMethod.Length == 0))
                {
                    arguments.TotalRowCount = dataTable.Rows.Count;
                }
                //if (enabled)
                //{
                //    SaveDataAndRowCountToCache(arguments, result.ReturnValue);
                //}


                return CreateFilteredDataView(dataTable, arguments.SortExpression, FilterExpression);
            }
            IEnumerable data = CreateEnumerableData(result.ReturnValue, arguments);
            //if (enabled)
            //{
            //    if (data is IDataReader)
            //    {
            //        throw new NotSupportedException(
            //            SR.GetString("ObjectDataSourceView_CacheNotSupportedOnIDataReader", new object[] { _owner.ID }));
            //    }
            //    SaveDataAndRowCountToCache(arguments, data);
            //}
            return data;
        }

        /// <summary>
        /// Performs an update operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView"/> object represents.
        /// </summary>
        /// <param name="keys">An <see cref="T:System.Collections.IDictionary"/> of object or row keys to be updated by the update operation.</param>
        /// <param name="values">An <see cref="T:System.Collections.IDictionary"/> of name/value pairs that represent data elements and their new values.</param>
        /// <param name="oldValues">An <see cref="T:System.Collections.IDictionary"/> of name/value pairs that represent data elements and their original values.</param>
        /// <returns>
        /// The number of items that were updated in the underlying data storage.
        /// </returns>
        protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
        {
            ObjectDataSourceMethod method;
            if (!CanUpdate)
            {
                throw new NotSupportedException("ObjectDataSourceView_UpdateNotSupported");
            }
            Type type = GetType();
            Type dataObjectType = TryGetDataObjectType();
            if (dataObjectType != null)
            {
                if (ConflictDetection == ConflictOptions.CompareAllValues)
                {
                    if (oldValues == null)
                    {
                        throw new InvalidOperationException("ObjectDataSourceView_Pessimistic");
                    }
                    IDictionary destination = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                    IDictionary dictionary2 = null;
                    MergeDictionaries(UpdateParameters, oldValues, destination);
                    MergeDictionaries(UpdateParameters, keys, destination);
                    MergeDictionaries(UpdateParameters, values, destination);

                    dictionary2 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                    MergeDictionaries(UpdateParameters, oldValues, dictionary2);
                    MergeDictionaries(UpdateParameters, keys, dictionary2);
                    object newDataObject = BuildDataObject(dataObjectType, destination);
                    object oldDataObject = BuildDataObject(dataObjectType, dictionary2);
                    method = GetResolvedMethodData(UpdateMethod, dataObjectType, oldDataObject, newDataObject,
                                              DataSourceOperation.Update);
                }
                else
                {
                    IDictionary dictionary3 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                    MergeDictionaries(UpdateParameters, oldValues, dictionary3);
                    MergeDictionaries(UpdateParameters, keys, dictionary3);
                    MergeDictionaries(UpdateParameters, values, dictionary3);
                    object obj4 = BuildDataObject(dataObjectType, dictionary3);
                    method =
                        GetResolvedMethodData(UpdateMethod, dataObjectType, null, obj4, DataSourceOperation.Update);
                }
                ObjectDataSourceMethodEventArgs e = new ObjectDataSourceMethodEventArgs(method.Parameters);
                OnUpdating(e);
                if (e.Cancel)
                {
                    return 0;
                }
            }
            else
            {
                IOrderedDictionary dictionary4 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
                string oldValuesParameterFormatString = OldValuesParameterFormatString;
                IDictionary source = UpdateParameters.GetValues(_context, _owner);
                if (keys != null)
                {
                    foreach (DictionaryEntry entry in keys)
                    {
                        if (source.Contains(entry.Key))
                        {
                            source.Remove(entry.Key);
                        }
                    }
                }
                MergeDictionaries(UpdateParameters, source, dictionary4);
                MergeDictionaries(UpdateParameters, values, dictionary4);
                if (ConflictDetection == ConflictOptions.CompareAllValues)
                {
                    MergeDictionaries(UpdateParameters, oldValues, dictionary4, oldValuesParameterFormatString);
                }
                MergeDictionaries(UpdateParameters, keys, dictionary4, oldValuesParameterFormatString);
                ObjectDataSourceMethodEventArgs args2 = new ObjectDataSourceMethodEventArgs(dictionary4);
                OnUpdating(args2);
                if (args2.Cancel)
                {
                    return 0;
                }
                method = GetResolvedMethodData(UpdateMethod, dictionary4, DataSourceOperation.Update);
            }
            ObjectDataSourceResult result = InvokeMethod(method);
            //if (_owner.Cache.Enabled)
            //{
            //    _owner.InvalidateCacheEntry();
            //}
            OnDataSourceViewChanged(EventArgs.Empty);
            return result.AffectedRows;
        }

        #endregion

        #region Events

        /// <summary>
        /// Raises the <see cref="Deleted"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDeleted(ObjectDataSourceStatusEventArgs e)
        {
            ObjectDataSourceStatusEventHandler handler =Events[EventDeleted] as ObjectDataSourceStatusEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Deleting"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDeleting(ObjectDataSourceMethodEventArgs e)
        {
            ObjectDataSourceMethodEventHandler handler =
               Events[EventDeleting] as ObjectDataSourceMethodEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Filtering"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs"/> instance containing the event data.</param>
        protected virtual void OnFiltering(ObjectDataSourceFilteringEventArgs e)
        {
            ObjectDataSourceFilteringEventHandler handler =
               Events[EventFiltering] as ObjectDataSourceFilteringEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Inserted"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs"/> instance containing the event data.</param>
        protected virtual void OnInserted(ObjectDataSourceStatusEventArgs e)
        {
            ObjectDataSourceStatusEventHandler handler =
               Events[EventInserted] as ObjectDataSourceStatusEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Inserting"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs"/> instance containing the event data.</param>
        protected virtual void OnInserting(ObjectDataSourceMethodEventArgs e)
        {
            ObjectDataSourceMethodEventHandler handler =
                Events[EventInserting] as ObjectDataSourceMethodEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="ObjectCreated"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceEventArgs"/> instance containing the event data.</param>
        protected virtual void OnObjectCreated(ObjectDataSourceEventArgs e)
        {
            ObjectDataSourceObjectEventHandler handler =
               Events[EventObjectCreated] as ObjectDataSourceObjectEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="ObjectCreating"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceEventArgs"/> instance containing the event data.</param>
        protected virtual void OnObjectCreating(ObjectDataSourceEventArgs e)
        {
            ObjectDataSourceObjectEventHandler handler =
               Events[EventObjectCreating] as ObjectDataSourceObjectEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="ObjectDisposing"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnObjectDisposing(ObjectDataSourceDisposingEventArgs e)
        {
            ObjectDataSourceDisposingEventHandler handler =
               Events[EventObjectDisposing] as ObjectDataSourceDisposingEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Selected"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSelected(ObjectDataSourceStatusEventArgs e)
        {
            ObjectDataSourceStatusEventHandler handler =
               Events[EventSelected] as ObjectDataSourceStatusEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Selecting"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSelecting(ObjectDataSourceSelectingEventArgs e)
        {
            ObjectDataSourceSelectingEventHandler handler =
               Events[EventSelecting] as ObjectDataSourceSelectingEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Updated"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs"/> instance containing the event data.</param>
        protected virtual void OnUpdated(ObjectDataSourceStatusEventArgs e)
        {
            ObjectDataSourceStatusEventHandler handler =Events[EventUpdated] as ObjectDataSourceStatusEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="Updating"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs"/> instance containing the event data.</param>
        protected virtual void OnUpdating(ObjectDataSourceMethodEventArgs e)
        {
            ObjectDataSourceMethodEventHandler handler =
               Events[EventUpdating] as ObjectDataSourceMethodEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region ViewsState

        /// <summary>
        /// When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> that contains the view state changes.
        /// </returns>
        protected virtual object SaveViewState()
        {
            Pair pair = new Pair();
            pair.First = (_selectParameters != null) ? ((IStateManager)_selectParameters).SaveViewState() : null;
            pair.Second = (_filterParameters != null) ? ((IStateManager)_filterParameters).SaveViewState() : null;
            if ((pair.First == null) && (pair.Second == null))
            {
                return null;
            }
            return pair;
        }

        /// <summary>
        /// Loads the state of the view.
        /// </summary>
        /// <param name="savedState">State of the saved.</param>
        protected virtual void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                Pair pair = (Pair)savedState;
                if (pair.First != null)
                {
                    ((IStateManager)SelectParameters).LoadViewState(pair.First);
                }
                if (pair.Second != null)
                {
                    ((IStateManager)FilterParameters).LoadViewState(pair.Second);
                }
            }
        }
        /// <summary>
        /// Loads the state of the view.
        /// </summary>
        /// <param name="savedState">State of the saved.</param>
        void IStateManager.LoadViewState(object savedState)
        {
            LoadViewState(savedState);
        }

        /// <summary>
        /// When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> that contains the view state changes.
        /// </returns>
        object IStateManager.SaveViewState()
        {
            return SaveViewState();
        }

        /// <summary>
        /// When implemented by a class, instructs the server control to track changes to its view state.
        /// </summary>
        void IStateManager.TrackViewState()
        {
            TrackViewState();
        }

        /// <summary>
        /// When implemented by a class, instructs the server control to track changes to its view state.
        /// </summary>
        protected virtual void TrackViewState()
        {
            _tracking = true;
            if (_selectParameters != null)
            {
                ((IStateManager)_selectParameters).TrackViewState();
            }
            if (_filterParameters != null)
            {
                ((IStateManager)_filterParameters).TrackViewState();
            }
        }
        #endregion

        #region Helpers
        private void SelectParametersChangedEventHandler(object o, EventArgs e)
        {
            OnDataSourceViewChanged(EventArgs.Empty);
        }

        private void MergeDictionaries(ParameterCollection reference, IDictionary source, IDictionary destination)
        {
            MergeDictionaries(reference, source, destination, null);
        }

        private void MergeDictionaries(ParameterCollection reference, IDictionary source, IDictionary destination,
                                              string parameterNameFormatString)
        {
            if (source != null)
            {
                foreach (DictionaryEntry entry in source)
                {
                    object obj2 = entry.Value;
                    Parameter parameter = null;
                    string key = (string)entry.Key;
                    if (parameterNameFormatString != null)
                    {
                        key = string.Format(CultureInfo.InvariantCulture, parameterNameFormatString, new object[] { key });
                    }
                    foreach (Parameter parameter2 in reference)
                    {
                        if (string.Equals(parameter2.Name, key, StringComparison.OrdinalIgnoreCase))
                        {
                            parameter = parameter2;
                            break;
                        }
                    }
                    if (parameter != null)
                    {
                        obj2 = GetValue(obj2, parameter.DefaultValue, parameter.Type,
                                               parameter.ConvertEmptyStringToNull, true);
                    }
                    destination[key] = obj2;
                }
            }
        }
        internal object GetValue(object value, string defaultValue, TypeCode type, bool convertEmptyStringToNull, bool ignoreNullableTypeChanges)
        {
            if (type == TypeCode.DBNull)
            {
                return DBNull.Value;
            }
            if (convertEmptyStringToNull)
            {
                string str = value as string;
                if ((str != null) && (str.Length == 0))
                {
                    value = null;
                }
            }
            if (value == null)
            {
                if (convertEmptyStringToNull && string.IsNullOrEmpty(defaultValue))
                {
                    defaultValue = null;
                }
                if (defaultValue == null)
                {
                    return null;
                }
                value = defaultValue;
            }
            if ((type == TypeCode.Object) || (type == TypeCode.Empty))
            {
                return value;
            }
            if (ignoreNullableTypeChanges)
            {
                Type type2 = value.GetType();
                if (type2.IsGenericType && (type2.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    return value;
                }
            }
            return (value = Convert.ChangeType(value, type, CultureInfo.CurrentCulture));
        }


        private DataObjectMethodType GetMethodTypeFromOperation(DataSourceOperation operation)
        {
            switch (operation)
            {
                case DataSourceOperation.Delete:
                    return DataObjectMethodType.Delete;

                case DataSourceOperation.Insert:
                    return DataObjectMethodType.Insert;

                case DataSourceOperation.Select:
                    return DataObjectMethodType.Select;

                case DataSourceOperation.Update:
                    return DataObjectMethodType.Update;
            }
            throw new ArgumentOutOfRangeException("operation");
        }

        private IDictionary GetOutputParameters(ParameterInfo[] parameters, object[] values)
        {
            IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo info = parameters[i];
                if (info.ParameterType.IsByRef)
                {
                    dictionary[info.Name] = values[i];
                }
            }
            return dictionary;
        }
        private object GetSourceObject()
        {
            return _owner.TemplateControl;
        }

        private Type GeType()
        {
            return GetSourceObject().GetType();
        }

        private ObjectDataSourceMethod GetResolvedMethodData(string methodName, IDictionary allParameters,
                                                             DataSourceOperation operation)
        {
            bool isCountOperation = operation == DataSourceOperation.SelectCount;
            DataObjectMethodType select = DataObjectMethodType.Select;
            if (!isCountOperation)
            {
                select = GetMethodTypeFromOperation(operation);
            }
            Type type = GeType();

            MethodInfo[] methods =
                type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            MethodInfo method = null;
            ParameterInfo[] parameters = null;
            int num = -1;
            bool flag2 = false;
            int count = allParameters.Count;
            foreach (MethodInfo searchedMethod in methods)
            {
                if (!string.Equals(methodName, searchedMethod.Name, StringComparison.OrdinalIgnoreCase) ||
                    searchedMethod.IsGenericMethodDefinition)
                    continue;

                ParameterInfo[] methodParameters = searchedMethod.GetParameters();
                if (methodParameters.Length != count)
                    continue;

                bool isInvalidMethod = false;
                foreach (ParameterInfo parameterInfo in methodParameters)
                {
                    if (!allParameters.Contains(parameterInfo.Name))
                    {
                        isInvalidMethod = true;
                        break;
                    }
                }
                if (isInvalidMethod)
                    continue;

                int num4 = 0;
                if (!isCountOperation)
                {
                    DataObjectMethodAttribute attribute =
                        Attribute.GetCustomAttribute(searchedMethod, typeof(DataObjectMethodAttribute), true) as
                        DataObjectMethodAttribute;
                    if ((attribute != null) && (attribute.MethodType == select))
                    {
                        if (attribute.IsDefault)
                        {
                            num4 = 2;
                        }
                        else
                        {
                            num4 = 1;
                        }
                    }
                }
                if (num4 == num)
                {
                    flag2 = true;
                }
                else if (num4 > num)
                {
                    num = num4;
                    flag2 = false;
                    method = searchedMethod;
                    parameters = methodParameters;
                }
            }

            if (flag2)
            {
                throw new InvalidOperationException("ObjectDataSourceView_MultipleOverloads");
            }
            if (method == null)
            {
                if (count == 0)
                {
                    throw new InvalidOperationException(string.Format("Method {0} without params not found.", methodName));
                }
                string[] array = new string[count];
                allParameters.Keys.CopyTo(array, 0);
                throw new InvalidOperationException(string.Format("Method {0} with params {1} not found  ",methodName,StringUtil.Join(array,",")));
            }

            return new ObjectDataSourceMethod(
                operation,
                type,
                method,
                GetParameters(allParameters, parameters));
        }

        private OrderedDictionary GetParameters(IDictionary allParameters, ParameterInfo[] infoArray2)
        {
            int length = infoArray2.Length;
            if (length == 0)
                return null;


            OrderedDictionary result = new OrderedDictionary(length, StringComparer.OrdinalIgnoreCase);
            bool convertNullToDBNull = ConvertNullToDBNull;
            for (int i = 0; i < infoArray2.Length; i++)
            {
                ParameterInfo info4 = infoArray2[i];
                string name = info4.Name;
                object obj2 = allParameters[name];
                if (convertNullToDBNull && (obj2 == null))
                {
                    obj2 = DBNull.Value;
                }
                else
                {
                    obj2 = BuildObjectValue(obj2, info4.ParameterType, name);
                }
                result.Add(name, obj2);
            }

            return result;
        }

        private ObjectDataSourceMethod GetResolvedMethodData(string methodName, Type dataObjectType,
                                                             object oldDataObject, object newDataObject,
                                                             DataSourceOperation operation)
        {
            int num;
            Type type = GeType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static |
                                BindingFlags.Instance);
            MethodInfo methodInfo = null;
            ParameterInfo[] infoArray2 = null;
            if (oldDataObject == null)
            {
                num = 1;
            }
            else if (newDataObject == null)
            {
                num = 1;
            }
            else
            {
                num = 2;
            }
            foreach (MethodInfo info2 in methods)
            {
                if (string.Equals(methodName, info2.Name, StringComparison.OrdinalIgnoreCase) &&
                    !info2.IsGenericMethodDefinition)
                {
                    ParameterInfo[] parameters = info2.GetParameters();
                    if (parameters.Length == num)
                    {
                        if ((num == 1) && (parameters[0].ParameterType == dataObjectType))
                        {
                            methodInfo = info2;
                            infoArray2 = parameters;
                            break;
                        }
                        if (((num == 2) && (parameters[0].ParameterType == dataObjectType)) &&
                            (parameters[1].ParameterType == dataObjectType))
                        {
                            methodInfo = info2;
                            infoArray2 = parameters;
                            break;
                        }
                    }
                }
            }
            if (methodInfo == null)
            {
                throw new InvalidOperationException("ObjectDataSourceView_MethodNotFoundForDataObject");
            }
            OrderedDictionary dictionary = new OrderedDictionary(2, StringComparer.OrdinalIgnoreCase);
            if (oldDataObject == null)
            {
                dictionary.Add(infoArray2[0].Name, newDataObject);
            }
            else if (newDataObject == null)
            {
                dictionary.Add(infoArray2[0].Name, oldDataObject);
            }
            else
            {
                string name = infoArray2[0].Name;
                string a = infoArray2[1].Name;
                string b =
                    string.Format(CultureInfo.InvariantCulture, OldValuesParameterFormatString, new object[] { name });
                if (string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
                {
                    dictionary.Add(name, newDataObject);
                    dictionary.Add(a, oldDataObject);
                }
                else
                {
                    b = string.Format(CultureInfo.InvariantCulture, OldValuesParameterFormatString, new object[] { a });
                    if (!string.Equals(name, b, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new InvalidOperationException("ObjectDataSourceView_NoOldValuesParams");
                    }
                    dictionary.Add(name, oldDataObject);
                    dictionary.Add(a, newDataObject);
                }
            }
            return new ObjectDataSourceMethod(operation, type, methodInfo, dictionary.AsReadOnly());
        }


        private Type TryGetDataObjectType()
        {
            string dataObjectTypeName = DataObjectTypeName;
            if (dataObjectTypeName.Length == 0)
            {
                return null;
            }
            Type type = BuildManager.GetType(dataObjectTypeName, false, true);
            if (type == null)
            {
                throw new InvalidOperationException("ObjectDataSourceView_DataObjectTypeNotFound");
            }
            return type;
        }


        private ObjectDataSourceResult InvokeMethod(ObjectDataSourceMethod method)
        {

            ObjectDataSourceEventArgs e = new ObjectDataSourceEventArgs(null);
            OnObjectCreating(e);
            if (e.ObjectInstance == null)
            {
                e.ObjectInstance = GetSourceObject();
                OnObjectCreated(e);
            }

            object returnValue = null;
            int affectedRows = -1;
            bool flag = false;
            object[] parameters = null;
            if ((method.Parameters != null) && (method.Parameters.Count > 0))
            {
                parameters = new object[method.Parameters.Count];
                for (int i = 0; i < method.Parameters.Count; i++)
                {
                    parameters[i] = method.Parameters[i];
                }
            }
            try
            {
                returnValue = method.MethodInfo.Invoke(e.ObjectInstance, parameters);
            }
            catch (Exception exception)
            {
                IDictionary outputParameters = GetOutputParameters(method.MethodInfo.GetParameters(), parameters);
                ObjectDataSourceStatusEventArgs args2 =
                    new ObjectDataSourceStatusEventArgs(returnValue, outputParameters, exception);
                flag = true;
                switch (method.Operation)
                {
                    case DataSourceOperation.Delete:
                        OnDeleted(args2);
                        break;

                    case DataSourceOperation.Insert:
                        OnInserted(args2);
                        break;

                    case DataSourceOperation.Select:
                        OnSelected(args2);
                        break;

                    case DataSourceOperation.Update:
                        OnUpdated(args2);
                        break;

                    case DataSourceOperation.SelectCount:
                        OnSelected(args2);
                        break;
                }
                affectedRows = args2.AffectedRows;
                if (!args2.ExceptionHandled)
                {
                    throw;
                }
            }
            finally
            {

                if (!flag)
                {
                    IDictionary dictionary2 = GetOutputParameters(method.MethodInfo.GetParameters(), parameters);
                    ObjectDataSourceStatusEventArgs args3 =
                        new ObjectDataSourceStatusEventArgs(returnValue, dictionary2);
                    switch (method.Operation)
                    {
                        case DataSourceOperation.Delete:
                            OnDeleted(args3);
                            break;

                        case DataSourceOperation.Insert:
                            OnInserted(args3);
                            break;

                        case DataSourceOperation.Select:
                            OnSelected(args3);
                            break;

                        case DataSourceOperation.Update:
                            OnUpdated(args3);
                            break;

                        case DataSourceOperation.SelectCount:
                            OnSelected(args3);
                            break;
                    }
                    affectedRows = args3.AffectedRows;
                }

            }
            return new ObjectDataSourceResult(returnValue, affectedRows);
        }

        private object BuildDataObject(Type dataObjectType, IDictionary inputParameters)
        {
            object component = Activator.CreateInstance(dataObjectType);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
            foreach (DictionaryEntry entry in inputParameters)
            {
                string name = (entry.Key == null) ? string.Empty : entry.Key.ToString();
                PropertyDescriptor descriptor = properties.Find(name, true);
                if (descriptor == null)
                {
                    throw new InvalidOperationException("ObjectDataSourceView_DataObjectPropertyNotFound");
                }
                if (descriptor.IsReadOnly)
                {
                    throw new InvalidOperationException("ObjectDataSourceView_DataObjectPropertyReadOnly");
                }
                object obj3 = BuildObjectValue(entry.Value, descriptor.PropertyType, name);
                descriptor.SetValue(component, obj3);
            }
            return component;
        }

        private object BuildObjectValue(object value, Type destinationType, string paramName)
        {
            if ((value != null) && !destinationType.IsInstanceOfType(value))
            {
                Type elementType = destinationType;
                bool flag = false;
                if (destinationType.IsGenericType && (destinationType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    elementType = destinationType.GetGenericArguments()[0];
                    flag = true;
                }
                else if (destinationType.IsByRef)
                {
                    elementType = destinationType.GetElementType();
                }
                value = ConvertType(value, elementType, paramName);
                if (flag)
                {
                    Type type = value.GetType();
                    if (elementType != type)
                    {
                        throw new InvalidOperationException("ObjectDataSourceView_CannotConvertType");
                    }
                }
            }
            return value;
        }

        private object ConvertType(object value, Type type, string paramName)
        {
            string text = value as string;
            if (text != null)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                if (converter == null)
                {
                    return value;
                }
                try
                {
                    value = converter.ConvertFromInvariantString(text);
                }
                catch (NotSupportedException)
                {
                    throw new InvalidOperationException("ObjectDataSourceView_CannotConvertType");
                }
                catch (FormatException)
                {
                    throw new InvalidOperationException("ObjectDataSourceView_CannotConvertType");
                }
            }
            return value;
        }

        private IEnumerable CreateEnumerableData(object dataObject, DataSourceSelectArguments arguments)
        {
            if (FilterExpression.Length > 0)
            {
                throw new NotSupportedException("ObjectDataSourceView_FilterNotSupported");
            }

            var enumerable = dataObject as IEnumerable;
            if (enumerable != null)
            {
                if (!string.IsNullOrEmpty(arguments.SortExpression))
                {
                    enumerable = enumerable.OrderBy(arguments.SortExpression);
                }
                if ((!EnablePaging && arguments.RetrieveTotalRowCount) && (SelectCountMethod.Length == 0))
                {
                    arguments.TotalRowCount = enumerable.Count();
                }
                return enumerable;
            }
            if (!string.IsNullOrEmpty(arguments.SortExpression))
            {
                throw new NotSupportedException("ControlDataSourceView SortNotSupported");
            }
            if (arguments.RetrieveTotalRowCount && (SelectCountMethod.Length == 0))
            {
                arguments.TotalRowCount = 1;
            }
            return new object[] { dataObject };
        }

        private IEnumerable CreateFilteredDataView(DataTable dataTable, string sortExpression, string filterExpression)
        {
            IOrderedDictionary values = FilterParameters.GetValues(_context, _owner);
            if (filterExpression.Length > 0)
            {
                ObjectDataSourceFilteringEventArgs e = new ObjectDataSourceFilteringEventArgs(values);
                OnFiltering(e);
                if (e.Cancel)
                {
                    return null;
                }
            }
            return CreateFilteredDataView(dataTable, sortExpression, filterExpression, values);
        }

        private DataView CreateFilteredDataView(DataTable table, string sortExpression, string filterExpression, IDictionary filterParameters)
        {
            DataView view = new DataView(table);
            if (!string.IsNullOrEmpty(sortExpression))
            {
                view.Sort = sortExpression;
            }
            if (!string.IsNullOrEmpty(filterExpression))
            {
                bool flag = false;
                object[] args = new object[filterParameters.Count];
                int index = 0;
                foreach (DictionaryEntry entry in filterParameters)
                {
                    if (entry.Value == null)
                    {
                        flag = true;
                        break;
                    }
                    args[index] = entry.Value;
                    index++;
                }
                filterExpression = string.Format(CultureInfo.InvariantCulture, filterExpression, args);
                if (!flag)
                {
                    view.RowFilter = filterExpression;
                }
            }
            return view;
        }

        private DataTable GetDataTable(Control owner, object dataObject)
        {
            DataSet set = dataObject as DataSet;
            if (set == null)
            {
                return (dataObject as DataTable);
            }
            if (set.Tables.Count == 0)
            {
                throw new InvalidOperationException("FilteredDataSetHelper_DataSetHasNoTables");
            }
            return set.Tables[0];
        }

        private int QueryTotalRowCount(IOrderedDictionary mergedParameters, DataSourceSelectArguments arguments)
        {
            if (SelectCountMethod.Length > 0)
            {
                ObjectDataSourceSelectingEventArgs e =
                    new ObjectDataSourceSelectingEventArgs(mergedParameters, arguments, true);
                OnSelecting(e);
                if (e.Cancel)
                {
                    return -1;
                }

                ObjectDataSourceMethod method = GetResolvedMethodData(SelectCountMethod, mergedParameters, DataSourceOperation.SelectCount);
                ObjectDataSourceResult result = InvokeMethod(method);
                if ((result.ReturnValue != null) && (result.ReturnValue is int))
                {
                    return (int)result.ReturnValue;
                }
            }
            return -1;
        }

        #endregion

        #region Cache Implementation

        //private void ProcessPagingData(DataSourceSelectArguments arguments, IOrderedDictionary parameters)
        //{
        //    if (arguments.RetrieveTotalRowCount)
        //    {
        //        int totalRowCount = _owner.LoadTotalRowCountFromCache();
        //        if (totalRowCount >= 0)
        //        {
        //            arguments.TotalRowCount = totalRowCount;
        //        }
        //        else
        //        {
        //            object instance = null;
        //            totalRowCount = QueryTotalRowCount(parameters, arguments, true, ref instance);
        //            arguments.TotalRowCount = totalRowCount;
        //            _owner.SaveTotalRowCountToCache(totalRowCount);
        //        }
        //    }
        //}



        //private void ReleaseInstance(object instance)
        //{
        //    ObjectDataSourceDisposingEventArgs e = new ObjectDataSourceDisposingEventArgs(instance);
        //    OnObjectDisposing(e);
        //    if (!e.Cancel)
        //    {
        //        IDisposable disposable = instance as IDisposable;
        //        if (disposable != null)
        //        {
        //            disposable.Dispose();
        //        }
        //    }
        //}

        //private void SaveDataAndRowCountToCache(DataSourceSelectArguments arguments, object data)
        //{
        //    if (arguments.RetrieveTotalRowCount && (_owner.LoadTotalRowCountFromCache() != arguments.TotalRowCount))
        //    {
        //        _owner.SaveTotalRowCountToCache(arguments.TotalRowCount);
        //    }
        //    _owner.SaveDataToCache(arguments.StartRowIndex, arguments.MaximumRows, data);
        //}

        #endregion

        #region Nested Types

        [StructLayout(LayoutKind.Sequential)]
        private struct ObjectDataSourceMethod
        {
            internal DataSourceOperation Operation;
            internal Type Type;
            internal OrderedDictionary Parameters;
            internal MethodInfo MethodInfo;

            internal ObjectDataSourceMethod(DataSourceOperation operation, Type type, MethodInfo methodInfo,
                                            OrderedDictionary parameters)
            {
                Operation = operation;
                Type = type;
                Parameters = parameters;
                MethodInfo = methodInfo;
            }
        }

        private class ObjectDataSourceResult
        {
            // Fields
            internal readonly int AffectedRows;
            internal readonly object ReturnValue;

            // Methods
            /// <summary>
            /// Initializes a new instance of the <see cref="ObjectDataSourceResult"/> class.
            /// </summary>
            /// <param name="returnValue">The return value.</param>
            /// <param name="affectedRows">The affected rows.</param>
            internal ObjectDataSourceResult(object returnValue, int affectedRows)
            {
                ReturnValue = returnValue;
                AffectedRows = affectedRows;
            }
        }
        #endregion
    }
}