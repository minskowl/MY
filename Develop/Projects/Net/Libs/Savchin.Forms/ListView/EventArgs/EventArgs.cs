using System;
using System.Collections;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    //-----------------------------------------------------------------------------------
    #region Event Parameter Blocks

    public class CancellableEventArgs : EventArgs
    {
        /// <summary>
        /// Has this event been cancelled by the event handler?
        /// </summary>
        public bool Canceled;
    }

    public class BeforeSortingEventArgs : CancellableEventArgs
    {
        public BeforeSortingEventArgs(OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2)
        {
            this.ColumnToSort = column;
            this.SortOrder = order;
            this.SecondaryColumnToSort = column2;
            this.SecondarySortOrder = order2;
        }

        public OLVColumn ColumnToSort;
        public SortOrder SortOrder;
        public OLVColumn SecondaryColumnToSort;
        public SortOrder SecondarySortOrder;
    }

    /// <summary>
    /// This event is triggered after the items in the list have been changed,
    /// either through SetObjects, AddObjects or RemoveObjects.
    /// </summary>
    public class ItemsChangedEventArgs : EventArgs
    {
        public ItemsChangedEventArgs()
        {
        }

        /// <summary>
        /// Constructor for this event when used by a virtual list
        /// </summary>
        /// <param name="oldObjectCount"></param>
        /// <param name="newObjectCount"></param>
        public ItemsChangedEventArgs(int oldObjectCount, int newObjectCount)
        {
            this.oldObjectCount = oldObjectCount;
            this.newObjectCount = newObjectCount;
        }

        public int OldObjectCount
        {
            get { return oldObjectCount; }
        }
        private int oldObjectCount;

        public int NewObjectCount
        {
            get { return newObjectCount; }
        }
        private int newObjectCount;
    }

    /// <summary>
    /// This event is triggered by AddObjects before any change has been made to the list.
    /// </summary>
    public class ItemsAddingEventArgs : CancellableEventArgs
    {
        public ItemsAddingEventArgs(ICollection objectsToAdd)
        {
            this.ObjectsToAdd = objectsToAdd;
        }

        public ICollection ObjectsToAdd;
    }

    /// <summary>
    /// This event is triggered by SetObjects before any change has been made to the list.
    /// </summary>
    /// <remarks>
    /// When used with a virtual list, OldObjects will always be null.
    /// </remarks>
    public class ItemsChangingEventArgs : CancellableEventArgs
    {
        public ItemsChangingEventArgs(IEnumerable oldObjects, IEnumerable newObjects)
        {
            this.oldObjects = oldObjects;
            this.NewObjects = newObjects;
        }

        public IEnumerable OldObjects
        {
            get { return oldObjects; }
        }
        private IEnumerable oldObjects;

        public IEnumerable NewObjects;
    }

    /// <summary>
    /// This event is triggered by RemoveObjects before any change has been made to the list.
    /// </summary>
    public class ItemsRemovingEventArgs : CancellableEventArgs
    {
        public ItemsRemovingEventArgs(ICollection objectsToRemove)
        {
            this.ObjectsToRemove = objectsToRemove;
        }

        public ICollection ObjectsToRemove;
    }

    /// <summary>
    /// Triggered after the user types into a list
    /// </summary>
    public class AfterSearchingEventArgs : EventArgs
    {
        public AfterSearchingEventArgs(string stringToFind, int indexSelected)
        {
            this.stringToFind = stringToFind;
            this.indexSelected = indexSelected;
        }

        public string StringToFind
        {
            get
            {
                return this.stringToFind;
            }
        }
        private string stringToFind;

        public int IndexSelected
        {
            get
            {
                return this.indexSelected;
            }
        }
        private int indexSelected;
    }

    /// <summary>
    /// Triggered when the user types into a list
    /// </summary>
    public class BeforeSearchingEventArgs : CancellableEventArgs
    {
        public BeforeSearchingEventArgs(string stringToFind, int startSearchFrom)
        {
            this.StringToFind = stringToFind;
            this.StartSearchFrom = startSearchFrom;
        }

        public string StringToFind;
        public int StartSearchFrom;
    }

    #endregion
}
