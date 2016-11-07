using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using Savchin.Linq.Dynamic;
using Savchin.Text;

namespace Savchin.Wpf.Controls.DataGrid.Filtering.Core.Querying
{
    public class QueryController
    {
        /// <summary>
        /// Gets or sets the column filter data.
        /// </summary>
        /// <value>The column filter data.</value>
        public FilterData ColumnFilterData { get; set; }
        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource { get; set; }

        private readonly Dictionary<string, FilterData> filtersForColumns;

        Query query;

        public Dispatcher CallingThreadDispatcher { get; set; }
        public bool UseBackgroundWorker { get; set; }
        private readonly object lockObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryController"/> class.
        /// </summary>
        public QueryController()
        {
            lockObject = new object();

            filtersForColumns = new Dictionary<string, FilterData>();
            query = new Query();
        }

        /// <summary>
        /// Does the query.
        /// </summary>
        public void DoQuery()
        {
            ColumnFilterData.IsSearchPerformed = false;

            if (!filtersForColumns.ContainsKey(ColumnFilterData.ValuePropertyBindingPath))
            {
                filtersForColumns.Add(ColumnFilterData.ValuePropertyBindingPath, ColumnFilterData);
            }
            else
            {
                filtersForColumns[ColumnFilterData.ValuePropertyBindingPath] = ColumnFilterData;
            }

            if (isRefresh)
            {
                if (filtersForColumns.ElementAt(filtersForColumns.Count - 1).Value.ValuePropertyBindingPath
                    == ColumnFilterData.ValuePropertyBindingPath)
                {
                    runFiltering();
                }
            }
            else if (filteringNeeded)
            {
                runFiltering();
            }

            ColumnFilterData.IsSearchPerformed = true;
            ColumnFilterData.IsRefresh = false;
        }

        public bool IsCurentControlFirstControl
        {
            get
            {
                return filtersForColumns.Count > 0
                    ? filtersForColumns.ElementAt(0).Value.ValuePropertyBindingPath == ColumnFilterData.ValuePropertyBindingPath : false;
            }
        }

        public void ClearFilter()
        {
            int count = filtersForColumns.Count;
            for (int i = 0; i < count; i++)
            {
                FilterData data = filtersForColumns.ElementAt(i).Value;

                data.ClearData();
            }

            DoQuery();
        }

        #region Internal

        private bool isRefresh
        {
            get { return (from f in filtersForColumns where f.Value.IsRefresh == true select f).Count() > 0; }
        }

        private bool filteringNeeded
        {
            get { return (from f in filtersForColumns where f.Value.IsSearchPerformed == false select f).Count() == 1; }
        }
        private void runFiltering()
        {
            bool filterChanged;

            createFilterExpressionsAndFilteredCollection(out filterChanged);

            if (filterChanged)
            {
                OnFilteringStarted(this, EventArgs.Empty);

                applayFilter();
            }
        }

        private void createFilterExpressionsAndFilteredCollection(out bool filterChanged)
        {
            QueryCreator queryCreator = new QueryCreator(filtersForColumns);

            queryCreator.CreateFilter(ref query);

            filterChanged = (query.IsQueryChanged || (query.FilterString != String.Empty && isRefresh));

            if (query.FilterString != String.Empty && filterChanged)
            {
                IEnumerable collection = ItemsSource as IEnumerable;

                if (ItemsSource is ListCollectionView)
                {
                    collection = (ItemsSource as ListCollectionView).SourceCollection as IEnumerable;
                }

                #region Debug
#if DEBUG
                Debug.WriteLine("QUERY STATEMENT: " + query.FilterString);
                Debug.WriteLine("QUERY PARAMETRS: " + StringUtil.Join(query.QueryParameters, ","));
#endif
                #endregion

                var result = collection.AsQueryable().Where(query.FilterString, query.QueryParameters.ToArray<object>());

                filteredCollection = result.Cast<object>().ToList();
            }
            else
            {
                filteredCollection = null;
            }

            query.StoreLastUsedValues();
        }

        #region Internal Filtering

        private IList filteredCollection;
        HashSet<object> filteredCollectionHashSet;

        void applayFilter()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);

            if (filteredCollection != null)
            {
                executeFilterAction(
                    new Action(() =>
                    {
                        filteredCollectionHashSet = initLookupDictionary(filteredCollection);

                        view.Filter = new Predicate<object>(itemPassesFilter);

                        OnFilteringFinished(this, EventArgs.Empty);
                    })
                );
            }
            else
            {
                executeFilterAction(
                    new Action(() =>
                    {
                        if (view.Filter != null)
                        {
                            view.Filter = null;
                        }

                        OnFilteringFinished(this, EventArgs.Empty);
                    })
                );
            }
        }

        private void executeFilterAction(Action action)
        {
            if (UseBackgroundWorker)
            {
                BackgroundWorker worker = new BackgroundWorker();

                worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    lock (lockObject)
                    {
                        executeActionUsingDispatcher(action);
                    }
                };

                worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (e.Error != null)
                    {
                        OnFilteringError(this, new FilteringEventArgs(e.Error));
                    }
                };

                worker.RunWorkerAsync();
            }
            else
            {
                try
                {
                    executeActionUsingDispatcher(action);
                }
                catch (Exception e)
                {
                    OnFilteringError(this, new FilteringEventArgs(e));
                }
            }
        }

        private void executeActionUsingDispatcher(Action action)
        {
            if (this.CallingThreadDispatcher != null && !this.CallingThreadDispatcher.CheckAccess())
            {
                this.CallingThreadDispatcher.Invoke
                    (
                        new Action(() =>
                        {
                            invoke(action);
                        })
                    );
            }
            else
            {
                invoke(action);
            }
        }

        private static void invoke(Action action)
        {
            System.Diagnostics.Trace.WriteLine("------------------ START APPLAY FILTER ------------------------------");
            Stopwatch sw = Stopwatch.StartNew();

            action.Invoke();

            sw.Stop();
            System.Diagnostics.Trace.WriteLine("TIME: " + sw.ElapsedMilliseconds);
            System.Diagnostics.Trace.WriteLine("------------------ STOP APPLAY FILTER ------------------------------");
        }

        private bool itemPassesFilter(object item)
        {
            return filteredCollectionHashSet.Contains(item);
        }

        #region Helpers
        private HashSet<object> initLookupDictionary(IList collection)
        {
            HashSet<object> dictionary = new HashSet<object>(collection.Cast<object>().ToList());

            return dictionary;
        }
        #endregion

        #endregion
        #endregion

        #region Progress Notification
        public event EventHandler<EventArgs> FilteringStarted;
        public event EventHandler<EventArgs> FilteringFinished;
        public event EventHandler<FilteringEventArgs> FilteringError;

        private void OnFilteringStarted(object sender, EventArgs e)
        {
            EventHandler<EventArgs> localEvent = FilteringStarted;

            if (localEvent != null) localEvent(sender, e);
        }

        private void OnFilteringFinished(object sender, EventArgs e)
        {
            EventHandler<EventArgs> localEvent = FilteringFinished;

            if (localEvent != null) localEvent(sender, e);
        }

        private void OnFilteringError(object sender, FilteringEventArgs e)
        {
            EventHandler<FilteringEventArgs> localEvent = FilteringError;

            if (localEvent != null) localEvent(sender, e);
        }
        #endregion
    }
}
