using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Savchin.Collection.Generic
{
    [Serializable]
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        public bool IsDirty
        {
            get;
            private set;
        }

        public bool SomeChanges
        {
            get { return true; }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionEx&lt;T&gt;"/> class.
        /// </summary>
        public ObservableCollectionEx()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionEx&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public ObservableCollectionEx(List<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionEx&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public ObservableCollectionEx(IEnumerable<T> collection)
            : base(collection)
        {
        }

        #region Interface
        /// <summary>
        /// Inits the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Init(IEnumerable<T> data)
        {
            Fill(data);
            IsDirty = false;
        }

        /// <summary>
        /// Fills the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public virtual void Fill(IEnumerable<T> data)
        {
            this.CheckReentrancy();
            Items.Clear();
            if (data != null)
                foreach (var value in data)
                    Items.Add(value);


            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="data">The data.</param>
        public void AddRange(IEnumerable<T> data)
        {
            CheckReentrancy();
            if (data != null)
                foreach (var value in data)
                    Items.Add(value);


            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="toRemove">To remove.</param>
        public void RemoveRange(IEnumerable<T> toRemove)
        {
            if (toRemove == null) return;
            CheckReentrancy();

            foreach (var value in toRemove)
                Items.Remove(value);


            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, toRemove));
        }
        /// <summary>
        /// Resets the dirty.
        /// </summary>
        public void ResetDirty()
        {
            IsDirty = false;
        }

        /// <summary>
        /// Raises the collection changed.
        /// </summary>
        public void RaiseCollectionChanged()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Sets the dirty.
        /// </summary>
        public void SetDirty()
        {
            IsDirty = true;
        }

        #endregion


        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            IsDirty = true;
            OnPropertyChanged(new PropertyChangedEventArgs("IsDirty"));
        }



    }
}
