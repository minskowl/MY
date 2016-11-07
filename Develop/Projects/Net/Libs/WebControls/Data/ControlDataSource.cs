#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// ControlDataSource. Provide data from property of Page\UserControl
    /// </summary>
    [DisplayName("ObjectDataSource_DisplayName"), 
     Description("ObjectDataSource_Description"),
     DefaultProperty("TypeName"), 
     ToolboxBitmap(typeof (ObjectDataSource)), 
     DefaultEvent("Selecting"),
     ParseChildren(true), 
     PersistChildren(false),
     AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
     AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ControlDataSource : DataSourceControl
    {
        // Fields
        //private SqlDataSourceCache _cache;
        private ControlDataSourceView _view;
        private ICollection _viewNames;
        private const string DefaultViewName = "DefaultView";

        #region Properties

        #region Cache
        //internal SqlDataSourceCache Cache
        //{
        //    get
        //    {
        //        if (_cache == null)
        //        {
        //            _cache = new SqlDataSourceCache();
        //        }
        //        return _cache;
        //    }
        //}

        //[TypeConverter(typeof (DataSourceCacheDurationConverter)), Category("Cache"),
        // Description("DataSourceCache_Duration"), DefaultValue(0)]
        //public virtual int CacheDuration
        //{
        //    get { return Cache.Duration; }
        //    set { Cache.Duration = value; }
        //}

        //[DefaultValue(0), Description("DataSourceCache_ExpirationPolicy"), Category("Cache")]
        //public virtual DataSourceCacheExpiry CacheExpirationPolicy
        //{
        //    get { return Cache.ExpirationPolicy; }
        //    set { Cache.ExpirationPolicy = value; }
        //}

        //[Description("DataSourceCache_KeyDependency"), DefaultValue(""), Category("Cache")]
        //public virtual string CacheKeyDependency
        //{
        //    get { return Cache.KeyDependency; }
        //    set { Cache.KeyDependency = value; }
        //}
        //[Description("DataSourceCache_Enabled"), DefaultValue(false), Category("Cache")]
        //public virtual bool EnableCaching
        //{
        //    get { return Cache.Enabled; }
        //    set { Cache.Enabled = value; }
        //} 
        #endregion

        /// <summary>
        /// Gets or sets the conflict detection.
        /// </summary>
        /// <value>The conflict detection.</value>
        [DefaultValue(0), Description("ObjectDataSource_ConflictDetection"), Category("Data")]
        public ConflictOptions ConflictDetection
        {
            get { return GetView().ConflictDetection; }
            set { GetView().ConflictDetection = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [convert null to DB null].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [convert null to DB null]; otherwise, <c>false</c>.
        /// </value>
        [Description("ObjectDataSource_ConvertNullToDBNull"), DefaultValue(false), Category("Data")]
        public bool ConvertNullToDBNull
        {
            get { return GetView().ConvertNullToDBNull; }
            set { GetView().ConvertNullToDBNull = value; }
        }

        /// <summary>
        /// Gets or sets the name of the data object type.
        /// </summary>
        /// <value>The name of the data object type.</value>
        [DefaultValue(""), Description("ObjectDataSource_DataObjectTypeName"), Category("Data")]
        public string DataObjectTypeName
        {
            get { return GetView().DataObjectTypeName; }
            set { GetView().DataObjectTypeName = value; }
        }

        /// <summary>
        /// Gets or sets the delete method.
        /// </summary>
        /// <value>The delete method.</value>
        [Description("ObjectDataSource_DeleteMethod"), DefaultValue(""), Category("Data")]
        public string DeleteMethod
        {
            get { return GetView().DeleteMethod; }
            set { GetView().DeleteMethod = value; }
        }

        /// <summary>
        /// Gets the delete parameters.
        /// </summary>
        /// <value>The delete parameters.</value>
        [DefaultValue((string)null), PersistenceMode(PersistenceMode.InnerProperty),
         Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), 
         MergableProperty(false), 
         Description("ObjectDataSource_DeleteParameters"),
         Category("Data")]
        public ParameterCollection DeleteParameters
        {
            get { return GetView().DeleteParameters; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [enable paging].
        /// </summary>
        /// <value><c>true</c> if [enable paging]; otherwise, <c>false</c>.</value>
        [DefaultValue(false), Description("ObjectDataSource_EnablePaging"), Category("Paging")]
        public bool EnablePaging
        {
            get { return GetView().EnablePaging; }
            set { GetView().EnablePaging = value; }
        }

        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
        /// <value>The filter expression.</value>
        [Description("ObjectDataSource_FilterExpression"), DefaultValue(""), Category("Data")]
        public string FilterExpression
        {
            get { return GetView().FilterExpression; }
            set { GetView().FilterExpression = value; }
        }

        /// <summary>
        /// Gets the filter parameters.
        /// </summary>
        /// <value>The filter parameters.</value>
        [DefaultValue((string)null), 
         Description("ObjectDataSource_FilterParameters"),
         Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), 
         MergableProperty(false), 
         PersistenceMode(PersistenceMode.InnerProperty),
         Category("Data")]
        public ParameterCollection FilterParameters
        {
            get { return GetView().FilterParameters; }
        }

        /// <summary>
        /// Gets or sets the insert method.
        /// </summary>
        /// <value>The insert method.</value>
        [Description("ObjectDataSource_InsertMethod"), Category("Data"), DefaultValue("")]
        public string InsertMethod
        {
            get { return GetView().InsertMethod; }
            set { GetView().InsertMethod = value; }
        }

        /// <summary>
        /// Gets the insert parameters.
        /// </summary>
        /// <value>The insert parameters.</value>
        [Description("ObjectDataSource_InsertParameters"), Category("Data"), DefaultValue((string)null),
         Editor(
             "System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
             , typeof(UITypeEditor)), MergableProperty(false), PersistenceMode(PersistenceMode.InnerProperty)]
        public ParameterCollection InsertParameters
        {
            get { return GetView().InsertParameters; }
        }

        /// <summary>
        /// Gets or sets the maximum name of the rows parameter.
        /// </summary>
        /// <value>The maximum name of the rows parameter.</value>
        [DefaultValue("maximumRows"), Description("ObjectDataSource_MaximumRowsParameterName"), Category("Paging")]
        public string MaximumRowsParameterName
        {
            get { return GetView().MaximumRowsParameterName; }
            set { GetView().MaximumRowsParameterName = value; }
        }

        /// <summary>
        /// Gets or sets the old values parameter format string.
        /// </summary>
        /// <value>The old values parameter format string.</value>
        [DefaultValue("{0}"), Description("DataSource_OldValuesParameterFormatString"), Category("Data")]
        public string OldValuesParameterFormatString
        {
            get { return GetView().OldValuesParameterFormatString; }
            set { GetView().OldValuesParameterFormatString = value; }
        }

        /// <summary>
        /// Gets or sets the select count method.
        /// </summary>
        /// <value>The select count method.</value>
        [DefaultValue(""), Category("Paging"), Description("ObjectDataSource_SelectCountMethod")]
        public string SelectCountMethod
        {
            get { return GetView().SelectCountMethod; }
            set { GetView().SelectCountMethod = value; }
        }

        /// <summary>
        /// Gets or sets the select method.
        /// </summary>
        /// <value>The select method.</value>
        [Description("ObjectDataSource_SelectMethod"), DefaultValue(""), Category("Data")]
        public string SelectMethod
        {
            get { return GetView().SelectMethod; }
            set { GetView().SelectMethod = value; }
        }

        /// <summary>
        /// Gets the select parameters.
        /// </summary>
        /// <value>The select parameters.</value>
        [PersistenceMode(PersistenceMode.InnerProperty), MergableProperty(false),
         Description("ObjectDataSource_SelectParameters"),
         Editor(
             "System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
             , typeof(UITypeEditor)), Category("Data"), DefaultValue((string)null)]
        public ParameterCollection SelectParameters
        {
            get { return GetView().SelectParameters; }
        }

        /// <summary>
        /// Gets or sets the name of the sort parameter.
        /// </summary>
        /// <value>The name of the sort parameter.</value>
        [Category("Data"), DefaultValue(""), Description("ObjectDataSource_SortParameterName")]
        public string SortParameterName
        {
            get { return GetView().SortParameterName; }
            set { GetView().SortParameterName = value; }
        }

        //[DefaultValue(""), Category("Cache"), Description("SqlDataSourceCache_SqlCacheDependency")]
        //public virtual string SqlCacheDependency
        //{
        //    get
        //    {
        //        return this.Cache.SqlCacheDependency;
        //    }
        //    set
        //    {
        //        this.Cache.SqlCacheDependency = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the start name of the row index parameter.
        /// </summary>
        /// <value>The start name of the row index parameter.</value>
        [Description("ObjectDataSource_StartRowIndexParameterName"), DefaultValue("startRowIndex"), Category("Paging")]
        public string StartRowIndexParameterName
        {
            get { return GetView().StartRowIndexParameterName; }
            set { GetView().StartRowIndexParameterName = value; }
        }


        /// <summary>
        /// Gets or sets the update method.
        /// </summary>
        /// <value>The update method.</value>
        [DefaultValue(""), Description("ObjectDataSource_UpdateMethod"), Category("Data")]
        public string UpdateMethod
        {
            get { return GetView().UpdateMethod; }
            set { GetView().UpdateMethod = value; }
        }

        /// <summary>
        /// Gets the update parameters.
        /// </summary>
        /// <value>The update parameters.</value>
        [Description("ObjectDataSource_UpdateParameters"), DefaultValue((string)null),
         Editor(
             "System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
             , typeof(UITypeEditor)), Category("Data"), MergableProperty(false),
         PersistenceMode(PersistenceMode.InnerProperty)]
        public ParameterCollection UpdateParameters
        {
            get { return GetView().UpdateParameters; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [deleted].
        /// </summary>
        [Category("Data"), Description("DataSource_Deleted")]
        public event ObjectDataSourceStatusEventHandler Deleted
        {
            add { GetView().Deleted += value; }
            remove { GetView().Deleted -= value; }
        }

        /// <summary>
        /// Occurs when [deleting].
        /// </summary>
        [Description("DataSource_Deleting"), Category("Data")]
        public event ObjectDataSourceMethodEventHandler Deleting
        {
            add { GetView().Deleting += value; }
            remove { GetView().Deleting -= value; }
        }

        /// <summary>
        /// Occurs when [filtering].
        /// </summary>
        [Category("Data"), Description("DataSource_Filtering")]
        public event ObjectDataSourceFilteringEventHandler Filtering
        {
            add { GetView().Filtering += value; }
            remove { GetView().Filtering -= value; }
        }

        /// <summary>
        /// Occurs when [inserted].
        /// </summary>
        [Description("DataSource_Inserted"), Category("Data")]
        public event ObjectDataSourceStatusEventHandler Inserted
        {
            add { GetView().Inserted += value; }
            remove { GetView().Inserted -= value; }
        }

        /// <summary>
        /// Occurs when [inserting].
        /// </summary>
        [Description("DataSource_Inserting"), Category("Data")]
        public event ObjectDataSourceMethodEventHandler Inserting
        {
            add { GetView().Inserting += value; }
            remove { GetView().Inserting -= value; }
        }

        /// <summary>
        /// Occurs when [object created].
        /// </summary>
        [Category("Data"), Description("ObjectDataSource_ObjectCreated")]
        public event ObjectDataSourceObjectEventHandler ObjectCreated
        {
            add { GetView().ObjectCreated += value; }
            remove { GetView().ObjectCreated -= value; }
        }

        /// <summary>
        /// Occurs when [object creating].
        /// </summary>
        [Category("Data"), Description("ObjectDataSource_ObjectCreating")]
        public event ObjectDataSourceObjectEventHandler ObjectCreating
        {
            add { GetView().ObjectCreating += value; }
            remove { GetView().ObjectCreating -= value; }
        }

        /// <summary>
        /// Occurs when [object disposing].
        /// </summary>
        [Description("ObjectDataSource_ObjectDisposing"), Category("Data")]
        public event ObjectDataSourceDisposingEventHandler ObjectDisposing
        {
            add { GetView().ObjectDisposing += value; }
            remove { GetView().ObjectDisposing -= value; }
        }

        /// <summary>
        /// Occurs when [selected].
        /// </summary>
        [Category("Data"), Description("ObjectDataSource_Selected")]
        public event ObjectDataSourceStatusEventHandler Selected
        {
            add { GetView().Selected += value; }
            remove { GetView().Selected -= value; }
        }

        /// <summary>
        /// Occurs when [selecting].
        /// </summary>
        [Category("Data"), Description("ObjectDataSource_Selecting")]
        public event ObjectDataSourceSelectingEventHandler Selecting
        {
            add { GetView().Selecting += value; }
            remove { GetView().Selecting -= value; }
        }

        /// <summary>
        /// Occurs when [updated].
        /// </summary>
        [Description("DataSource_Updated"), Category("Data")]
        public event ObjectDataSourceStatusEventHandler Updated
        {
            add { GetView().Updated += value; }
            remove { GetView().Updated -= value; }
        }

        /// <summary>
        /// Occurs when [updating].
        /// </summary>
        [Category("Data"), Description("DataSource_Updating")]
        public event ObjectDataSourceMethodEventHandler Updating
        {
            add { GetView().Updating += value; }
            remove { GetView().Updating -= value; }
        } 
        #endregion


        #region Interface

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return GetView().Delete(null, null);
        }

        /// <summary>
        /// Inserts this instance.
        /// </summary>
        /// <returns></returns>
        public int Insert()
        {
            return GetView().Insert(null);
        }

        /// <summary>
        /// Selects this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable Select()
        {
            return GetView().Select(DataSourceSelectArguments.Empty);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            return GetView().Update(null, null, null);
        } 
        #endregion

        #region Ovverides

        /// <summary>
        /// Gets the named data source view associated with the data source control.
        /// </summary>
        /// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView"/> to retrieve. In data source controls that support only one view, such as <see cref="T:System.Web.UI.WebControls.SqlDataSource"/>, this parameter is ignored.</param>
        /// <returns>
        /// Returns the named <see cref="T:System.Web.UI.DataSourceView"/> associated with the <see cref="T:System.Web.UI.DataSourceControl"/>.
        /// </returns>
        protected override DataSourceView GetView(string viewName)
        {
            if ((viewName == null) ||
                ((viewName.Length != 0) && !string.Equals(viewName, "DefaultView", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("DataSource_InvalidViewName");
            }
            return GetView();
        }

        /// <summary>
        /// Gets a collection of names, representing the list of <see cref="T:System.Web.UI.DataSourceView"/> objects associated with the <see cref="T:System.Web.UI.DataSourceControl"/> control.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.ICollection"/> that contains the names of the <see cref="T:System.Web.UI.DataSourceView"/> objects associated with the <see cref="T:System.Web.UI.DataSourceControl"/>.
        /// </returns>
        protected override ICollection GetViewNames()
        {
            if (_viewNames == null)
            {
                _viewNames = new string[] { "DefaultView" };
            }
            return _viewNames;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Page != null)
            {
                Page.LoadComplete += new EventHandler(LoadCompleteEventHandler);
            }
        } 
        #endregion

        private void LoadCompleteEventHandler(object sender, EventArgs e)
        {
            SelectParameters.UpdateValues(Context, this);
            FilterParameters.UpdateValues(Context, this);
        }

        private ControlDataSourceView GetView()
        {
            if (_view == null)
            {
                _view = new ControlDataSourceView(this, "DefaultView", Context);
                if (base.IsTrackingViewState)
                {
                    ((IStateManager)_view).TrackViewState();
                }
            }
            return _view;
        }


        #region Caching
        //internal string CreateCacheKey(int startRowIndex, int maximumRows)
        //{
        //    StringBuilder builder = this.CreateRawCacheKey();
        //    builder.Append(':');
        //    builder.Append(startRowIndex.ToString(CultureInfo.InvariantCulture));
        //    builder.Append(':');
        //    builder.Append(maximumRows.ToString(CultureInfo.InvariantCulture));
        //    return builder.ToString();
        //}

        //internal string CreateMasterCacheKey()
        //{
        //    return this.CreateRawCacheKey().ToString();
        //}

        //private StringBuilder CreateRawCacheKey()
        //{
        //    StringBuilder builder = new StringBuilder("u", 0x400);
        //    builder.Append(base.GetType().GetHashCode().ToString(CultureInfo.InvariantCulture));
        //    builder.Append(":");
        //    builder.Append(this.CacheDuration.ToString(CultureInfo.InvariantCulture));
        //    builder.Append(':');
        //    builder.Append(((int)this.CacheExpirationPolicy).ToString(CultureInfo.InvariantCulture));
        //    builder.Append(":");
        //    builder.Append(this.SqlCacheDependency);
        //    builder.Append(":");
        //    builder.Append(this.TypeName);
        //    builder.Append(":");
        //    builder.Append(this.SelectMethod);
        //    if (this.SelectParameters.Count > 0)
        //    {
        //        builder.Append("?");
        //        foreach (DictionaryEntry entry in this.SelectParameters.GetValues(this.Context, this))
        //        {
        //            builder.Append(entry.Key.ToString());
        //            if ((entry.Value != null) && (entry.Value != DBNull.Value))
        //            {
        //                builder.Append("=");
        //                builder.Append(entry.Value.ToString());
        //            }
        //            else if (entry.Value == DBNull.Value)
        //            {
        //                builder.Append("(dbnull)");
        //            }
        //            else
        //            {
        //                builder.Append("(null)");
        //            }
        //            builder.Append("&");
        //        }
        //    }
        //    return builder;
        //}
        //internal void InvalidateCacheEntry()
        //{
        //    string key = this.CreateMasterCacheKey();
        //    Cache.Invalidate(key);
        //}



        //internal object LoadDataFromCache(int startRowIndex, int maximumRows)
        //{
        //    string key = this.CreateCacheKey(startRowIndex, maximumRows);
        //    return Cache.LoadDataFromCache(key);
        //}

        //internal int LoadTotalRowCountFromCache()
        //{
        //    string key = this.CreateMasterCacheKey();
        //    object obj2 = Cache.LoadDataFromCache(key);
        //    if (obj2 is int)
        //    {
        //        return (int) obj2;
        //    }
        //    return -1;
        //}
        //internal void SaveDataToCache(int startRowIndex, int maximumRows, object data)
        //{
        //    string key = this.CreateCacheKey(startRowIndex, maximumRows);
        //    string str2 = this.CreateMasterCacheKey();
        //    if (Cache.LoadDataFromCache(str2) == null)
        //    {
        //        Cache.SaveDataToCache(str2, -1);
        //    }
        //    CacheDependency dependency = new CacheDependency(0, new string[0], new string[] {str2});
        //    Cache.SaveDataToCache(key, data, dependency);
        //}

        //internal void SaveTotalRowCountToCache(int totalRowCount)
        //{
        //    string key = this.CreateMasterCacheKey();
        //    Cache.SaveDataToCache(key, totalRowCount);
        //} 
        #endregion

        #region ViewState
        /// <summary>
        /// Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState"/> method.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Object"/> that represents the control state to be restored.</param>
        protected override void LoadViewState(object savedState)
        {
            Pair pair = (Pair)savedState;
            if (savedState == null)
            {
                base.LoadViewState(null);
            }
            else
            {
                base.LoadViewState(pair.First);
                if (pair.Second != null)
                {
                    ((IStateManager)GetView()).LoadViewState(pair.Second);
                }
            }
        }

        /// <summary>
        /// Saves any server control view-state changes that have occurred since the time the page was posted back to the server.
        /// </summary>
        /// <returns>
        /// Returns the server control's current view state. If there is no view state associated with the control, this method returns null.
        /// </returns>
        protected override object SaveViewState()
        {
            Pair pair = new Pair();
            pair.First = base.SaveViewState();
            if (_view != null)
            {
                pair.Second = ((IStateManager)_view).SaveViewState();
            }
            if ((pair.First == null) && (pair.Second == null))
            {
                return null;
            }
            return pair;
        }



        /// <summary>
        /// Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag"/> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState"/> property.
        /// </summary>
        protected override void TrackViewState()
        {
            base.TrackViewState();
            if (_view != null)
            {
                ((IStateManager)_view).TrackViewState();
            }
        } 
        #endregion

    }
}