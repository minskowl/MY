using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// FastObjectListDataSource
    /// </summary>
    public class FastObjectListDataSource : AbstractVirtualListDataSource
    {
        private ArrayList objectList = new ArrayList();
        private object _syncRoot = new object();

        #region Implementation

        private readonly Dictionary<Object, int> objectsToIndexMap = new Dictionary<Object, int>();

        /// <summary>
        /// Rebuild the map that remembers which model object is displayed at which line
        /// </summary>
        protected void RebuildIndexMap()
        {
            objectsToIndexMap.Clear();
            for (int i = 0; i < objectList.Count; i++)
                objectsToIndexMap[objectList[i]] = i;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FastObjectListDataSource"/> class.
        /// </summary>
        /// <param name="listView">The list view.</param>
        public FastObjectListDataSource(FastObjectListView listView)
            : base(listView)
        {
        }

        /// <summary>
        /// Gets the NTH object.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        public override object GetNthObject(int n)
        {
            object ret = null;
            lock (_syncRoot)
            {
                if (objectList.Count > n)
                    ret = objectList[n];
            }
            return ret;
        }

        /// <summary>
        /// Gets the object count.
        /// </summary>
        /// <returns></returns>
        public override int GetObjectCount()
        {
            return objectList.Count;
        }

        /// <summary>
        /// Gets the index of the object.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public override int GetObjectIndex(object model)
        {
            int index;

            if (model != null && objectsToIndexMap.TryGetValue(model, out index))
                return index;
            else
                return -1;
        }

        /// <summary>
        /// Searches the text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public override int SearchText(string value, int first, int last, OLVColumn column)
        {
            return DefaultSearchText(value, first, last, column, this);
        }

        /// <summary>
        /// Sorts the specified column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="sortOrder">The sort order.</param>
        public override void Sort(OLVColumn column, SortOrder sortOrder)
        {
            lock (_syncRoot)
            {
                if (sortOrder != SortOrder.None)
                    objectList.Sort(new ModelObjectComparer(column, sortOrder, listView.SecondarySortColumn,
                                                            listView.SecondarySortOrder));
                RebuildIndexMap();
            }
        }

        /// <summary>
        /// Adds the objects.
        /// </summary>
        /// <param name="modelObjects">The model objects.</param>
        public override void AddObjects(ICollection modelObjects)
        {
            lock (_syncRoot)
            {
                foreach (object modelObject in modelObjects)
                {
                    if (modelObject != null)
                        objectList.Add(modelObject);
                }
                RebuildIndexMap();
            }
        }
        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <returns></returns>
        public object[] GetObjects()
        {
            lock (_syncRoot)
            {
                return objectList.ToArray();
            }
        }

        /// <summary>
        /// Replaces the object.
        /// </summary>
        /// <param name="oldObject">The old object.</param>
        /// <param name="newObject">The new object.</param>
        public void ReplaceObject(object oldObject, object newObject)
        {
            lock (_syncRoot)
            {
                var index = GetObjectIndex(oldObject);
                objectList[index] = newObject;
                RebuildIndexMap();
            }
        }

        /// <summary>
        /// Remove all objects from the control
        /// </summary>
        public override void Clear()
        {
            lock (_syncRoot)
            {
                RebuildIndexMap();
                objectList.Clear();
                RebuildIndexMap();
            }
        }
        /// <summary>
        /// Removes the objects.
        /// </summary>
        /// <param name="modelObjects">The model objects.</param>
        public override void RemoveObjects(ICollection modelObjects)
        {
            lock (_syncRoot)
            {
                // MK: refactor it
                RebuildIndexMap();

                var indicesToRemove = new List<int>();
                foreach (object modelObject in modelObjects)
                {
                    int i = GetObjectIndex(modelObject);
                    if (i >= 0)
                        indicesToRemove.Add(i);
                }
                // Sort the indices from highest to lowest so that we
                // remove latter ones before earlier ones. In this way, the
                // indices of the rows doesn't change after the deletes.
                indicesToRemove.Sort();
                indicesToRemove.Reverse();

                foreach (int i in indicesToRemove)
                    listView.SelectedIndices.Remove(i);

                foreach (int i in indicesToRemove)
                {
                    if (objectList.Count > i)
                    {
                        objectList.RemoveAt(i);
                    }
                    else
                    {
                        Debug.Assert(false, "Try to remove item by wrong index value.");
                    }
                }

                RebuildIndexMap();
            }
        }

        /// <summary>
        /// Sets the objects.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public override void SetObjects(IEnumerable collection)
        {
            lock (_syncRoot)
            {
                var newObjects = new ArrayList();
                if (collection != null)
                {
                    if (collection is ICollection)
                        newObjects = new ArrayList((ICollection)collection);
                    else
                    {
                        foreach (object x in collection)
                            newObjects.Add(x);
                    }
                }

                objectList = newObjects;
                RebuildIndexMap();
            }
        }
    }
}