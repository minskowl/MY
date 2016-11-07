using System;
using System.Collections;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A VirtualListDataSource is a complete manner to provide functionality to a virtual list.
    /// An object that implements this interface provides a VirtualObjectListView with all the
    /// information it needs to be fully functional.
    /// </summary>
    /// <remarks>Implementors must provide functioning implementations of GetObjectCount()
    /// and GetNthObject(), otherwise nothing will appear in the list.</remarks>
    public interface IVirtualListDataSource
    {
        /// <summary>
        /// Return the object that should be displayed at the n'th row.
        /// </summary>
        /// <param name="n">The index of the row whose object is to be returned.</param>
        /// <returns>The model object at the n'th row, or null if the fetching was unsuccessful.</returns>
        Object GetNthObject(int n);

        /// <summary>
        /// Return the number of rows that should be visible in the virtual list
        /// </summary>
        /// <returns>The number of rows the list view should have.</returns>
        int GetObjectCount();

        /// <summary>
        /// Get the index of the row that is showing the given model object
        /// </summary>
        /// <param name="model">The model object sought</param>
        /// <returns>The index of the row showing the model, or -1 if the object could not be found.</returns>
        int GetObjectIndex(Object model);

        /// <summary>
        /// The ListView is about to request the given range of items. Do
        /// whatever caching seems appropriate.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        void PrepareCache(int first, int last);

        /// <summary>
        /// Find the first row that "matches" the given text in the given range.
        /// </summary>
        /// <param name="value">The text typed by the user</param>
        /// <param name="first">Start searching from this index. This may be greater than the 'to' parameter, 
        /// in which case the search should descend</param>
        /// <param name="last">Do not search beyond this index. This may be less than the 'from' parameter.</param>
        /// <param name="column">The column that should be considered when looking for a match.</param>
        /// <returns>Return the index of row that was matched, or -1 if no match was found</returns>
        int SearchText(string value, int first, int last, OLVColumn column);

        /// <summary>
        /// Sort the model objects in the data source.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="order"></param>
        void Sort(OLVColumn column, SortOrder order);

        //-----------------------------------------------------------------------------------
        // Modification commands
        // THINK: Should we split these three into a separate interface?

        /// <summary>
        /// Add the given collection of model objects to this control.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        void AddObjects(ICollection modelObjects);

        /// <summary>
        /// Remove all of the given objects from the control
        /// </summary>
        /// <param name="modelObjects">Collection of objects to be removed</param>
        void RemoveObjects(ICollection modelObjects);

        /// <summary>
        /// Remove all objects from the control
        /// </summary>
        void Clear();

        /// Set the collection of objects that this control will show.
        /// </summary>
        /// <param name="collection"></param>
        void SetObjects(IEnumerable collection);
    }
}