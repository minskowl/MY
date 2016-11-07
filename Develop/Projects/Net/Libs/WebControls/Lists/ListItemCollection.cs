using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Savchin.Web.UI.Lists
{
    [Editor(
        "System.Web.UI.Design.WebControls.ListItemsCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        , typeof (UITypeEditor)),
     AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed class ListItemCollection : IList, ICollection, IEnumerable, IStateManager
    {
        // Fields
        private ArrayList listItems = new ArrayList();
        private bool marked;
        private bool saveAll;

        public int Capacity
        {
            get { return listItems.Capacity; }
            set { listItems.Capacity = value; }
        }

        public ListItem this[int index]
        {
            get { return (ListItem) listItems[index]; }
        }

        // Methods

        #region IList Members

        public void Clear()
        {
            listItems.Clear();
            if (marked)
            {
                saveAll = true;
            }
        }

        public void CopyTo(Array array, int index)
        {
            listItems.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return listItems.GetEnumerator();
        }

        public void RemoveAt(int index)
        {
            listItems.RemoveAt(index);
            if (marked)
            {
                saveAll = true;
            }
        }

        int IList.Add(object item)
        {
            var item2 = (ListItem) item;
            int num = listItems.Add(item2);
            if (marked)
            {
                item2.Dirty = true;
            }
            return num;
        }

        bool IList.Contains(object item)
        {
            return Contains((ListItem) item);
        }

        int IList.IndexOf(object item)
        {
            return IndexOf((ListItem) item);
        }

        void IList.Insert(int index, object item)
        {
            Insert(index, (ListItem) item);
        }

        void IList.Remove(object item)
        {
            Remove((ListItem) item);
        }

        public int Count
        {
            get { return listItems.Count; }
        }

        public bool IsReadOnly
        {
            get { return listItems.IsReadOnly; }
        }

        public bool IsSynchronized
        {
            get { return listItems.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return listItems[index]; }
            set { listItems[index] = value; }
        }

        #endregion

        #region IStateManager Members

        void IStateManager.LoadViewState(object state)
        {
            LoadViewState(state);
        }

        object IStateManager.SaveViewState()
        {
            return SaveViewState();
        }

        void IStateManager.TrackViewState()
        {
            TrackViewState();
        }

        bool IStateManager.IsTrackingViewState
        {
            get { return marked; }
        }

        #endregion

        #region Interface
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(string item)
        {
            Add(new ListItem(item));
        }
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        public void Add(string text, string value)
        {
            //ListItem item= new ListItem();
            //item.Text = text;
            //item.Value = value;
            Add(new ListItem(text, value));
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(ListItem item)
        {
            listItems.Add(item);
            if (marked)
            {
                item.Dirty = true;
            }
        }

        public void AddRange(ListItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            foreach (ListItem item in items)
            {
                Add(item);
            }
        }

        public bool Contains(ListItem item)
        {
            return listItems.Contains(item);
        }

        public ListItem FindByText(string text)
        {
            int num = FindByTextInternal(text, true);
            if (num != -1)
            {
                return (ListItem)listItems[num];
            }
            return null;
        } 


        internal int FindByTextInternal(string text, bool includeDisabled)
        {
            int num = 0;
            foreach (ListItem item in listItems)
            {
                if (item.Text.Equals(text) && (includeDisabled || item.Enabled))
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public ListItem FindByValue(string value)
        {
            int num = FindByValueInternal(value, true);
            if (num != -1)
            {
                return (ListItem) listItems[num];
            }
            return null;
        }

        internal int FindByValueInternal(string value, bool includeDisabled)
        {
            int num = 0;
            foreach (ListItem item in listItems)
            {
                if (item.Value.Equals(value) && (includeDisabled || item.Enabled))
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public int IndexOf(ListItem item)
        {
            return listItems.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            Insert(index, new ListItem(item));
        }

        public void Insert(int index, ListItem item)
        {
            listItems.Insert(index, item);
            if (marked)
            {
                saveAll = true;
            }
        }



        public void Remove(string item)
        {
            int index = IndexOf(new ListItem(item));
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }

        public void Remove(ListItem item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }


        #region ViewState

        /// <summary>
        /// When implemented by a class, instructs the server control to track changes to its view state.
        /// </summary>
        public void TrackViewState()
        {
            marked = true;
            for (int i = 0; i < Count; i++)
            {
                this[i].TrackViewState();
            }
        }

        /// <summary>
        /// When implemented by a class, loads the server control's previously saved view state to the control.
        /// </summary>
        /// <param name="state">An <see cref="T:System.Object"/> that contains the saved view state values for the control.</param>
        public void LoadViewState(object state)
        {
            if (state != null)
            {
                if (state is Pair)
                {
                    var pair = (Pair)state;
                    var first = (ArrayList)pair.First;
                    var second = (ArrayList)pair.Second;
                    for (int i = 0; i < first.Count; i++)
                    {
                        var num2 = (int)first[i];
                        if (num2 < Count)
                        {
                            this[num2].LoadViewState(second[i]);
                        }
                        else
                        {
                            var item = new ListItem();
                            item.LoadViewState(second[i]);
                            Add(item);
                        }
                    }
                }
                else
                {
                    var triplet = (Triplet)state;
                    listItems = new ArrayList();
                    saveAll = true;
                    var strArray = (string[])triplet.First;
                    var strArray2 = (string[])triplet.Second;
                    var third = (bool[])triplet.Third;
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        Add(new ListItem(strArray[j], strArray2[j], third[j]));
                    }
                }
            }
        }

        /// <summary>
        /// When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> that contains the view state changes.
        /// </returns>
        public object SaveViewState()
        {
            if (saveAll)
            {
                int count = Count;
                object[] objArray = new string[count];
                object[] objArray2 = new string[count];
                var z = new bool[count];
                for (int j = 0; j < count; j++)
                {
                    objArray[j] = this[j].Text;
                    objArray2[j] = this[j].Value;
                    z[j] = this[j].Enabled;
                }
                return new Triplet(objArray, objArray2, z);
            }
            var x = new ArrayList(4);
            var y = new ArrayList(4);
            for (int i = 0; i < Count; i++)
            {
                object obj2 = this[i].SaveViewState();
                if (obj2 != null)
                {
                    x.Add(i);
                    y.Add(obj2);
                }
            }
            if (x.Count > 0)
            {
                return new Pair(x, y);
            }
            return null;
        } 

        #endregion
        #endregion
    }
}