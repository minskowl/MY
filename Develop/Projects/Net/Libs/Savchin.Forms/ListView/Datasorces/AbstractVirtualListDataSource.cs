using System;
using System.Collections;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A do-nothing implementation of the VirtualListDataSource interface.
    /// </summary>
    public class AbstractVirtualListDataSource : IVirtualListDataSource
    {
        /// <summary>
        /// The list view that this data source is giving information to.
        /// </summary>
        protected VirtualObjectListView listView;

        public AbstractVirtualListDataSource(VirtualObjectListView listView)
        {
            this.listView = listView;
        }

        #region IVirtualListDataSource Members

        public virtual object GetNthObject(int n)
        {
            return null;
        }

        public virtual int GetObjectCount()
        {
            return -1;
        }

        /// <summary>
        /// Get the index of the row that is showing the given model object
        /// </summary>
        /// <param name="model">The model object sought</param>
        /// <returns>
        /// The index of the row showing the model, or -1 if the object could not be found.
        /// </returns>
        public virtual int GetObjectIndex(object model)
        {
            return -1;
        }

        /// <summary>
        /// Prepares the cache.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public virtual void PrepareCache(int from, int to)
        {
        }

        /// <summary>
        /// Find the first row that "matches" the given text in the given range.
        /// </summary>
        /// <param name="value">The text typed by the user</param>
        /// <param name="first">Start searching from this index. This may be greater than the 'to' parameter,
        /// in which case the search should descend</param>
        /// <param name="last">Do not search beyond this index. This may be less than the 'from' parameter.</param>
        /// <param name="column">The column that should be considered when looking for a match.</param>
        /// <returns>
        /// Return the index of row that was matched, or -1 if no match was found
        /// </returns>
        public virtual int SearchText(string value, int first, int last, OLVColumn column)
        {
            return -1;
        }

        /// <summary>
        /// Sort the model objects in the data source.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="order"></param>
        public virtual void Sort(OLVColumn column, SortOrder order)
        {
        }

        /// <summary>
        /// Add the given collection of model objects to this control.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        public virtual void AddObjects(ICollection modelObjects)
        {
        }

        /// <summary>
        /// Remove all of the given objects from the control
        /// </summary>
        /// <param name="modelObjects">Collection of objects to be removed</param>
        public virtual void RemoveObjects(ICollection modelObjects)
        {
        }

        /// <summary>
        /// Remove all objects from the control
        /// </summary>
        public virtual void Clear()
        {
            
        }

        /// <summary>
        public virtual void SetObjects(IEnumerable collection)
        {
        }

        #endregion

        /// <summary>
        /// This is a useful default implementation of SearchText method, intended to be called
        /// by implementors of IVirtualListDataSource.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="column"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int DefaultSearchText(string value, int first, int last, OLVColumn column,
                                            IVirtualListDataSource source)
        {
            if (first <= last)
            {
                for (int i = first; i <= last; i++)
                {
                    string data = column.GetStringValue(source.GetNthObject(i));
                    if (data.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            }
            else
            {
                for (int i = first; i >= last; i--)
                {
                    string data = column.GetStringValue(source.GetNthObject(i));
                    if (data.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            }

            return -1;
        }
    }
}