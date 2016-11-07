using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Savchin.Wpf.Input;

namespace Advertiser.Views
{
    public interface IItemListView
    {
        ICollectionView ItemsView { get; }
        string Title { get; }
        event PropertyChangedEventHandler PropertyChanged;
    }

    public abstract class ItemListView<T> : ViewBase, IItemListView
        where T : class
    {
        #region Properties

        public ICommand AddItemCommand { get; private set; }
        public ICommand DeleteItemCommand { get; private set; }
        public ICommand SaveItemCommand { get; private set; }

        public List<MenuItem> ContextMenu { get; private set; }



        public ICollectionView ItemsView
        {
            get;
            private set; 
        }

        public abstract Object ActiveView
        {
            get;
        }
        public ObservableCollection<T> Items
        {
            get; 
            private set;
        }

        public bool CanEdit
        {
            get { return _selectedItem != null; }
        }


        private T _selectedItem;
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;
                OnSelectedItemChanged();
            }
        }


        #endregion

        protected ItemListView(string name, List<T> source)
        {
            Title = name;
            Items = new ObservableCollection<T>(source);
            
            ItemsView = CollectionViewSource.GetDefaultView(Items );
            ContextMenu= new List<MenuItem>();

            AddItemCommand = new DelegateCommand(OnAddItem);
            DeleteItemCommand = new DelegateCommand(OnDeleteItem, CanDelete);
            SaveItemCommand = new DelegateCommand(OnSaveItem, CanSave);
        }


        protected virtual void OnSelectedItemChanged()
        {
            OnPropertyChanged("SelectedItem");
            OnPropertyChanged("CanEdit");
        }


        protected virtual bool CanSave()
        {
            return true;
        }
        protected virtual void OnSaveItem()
        {
            if (!Items.Contains(SelectedItem))
            {
                Items.Add(SelectedItem);
            }
        }

        private bool CanDelete()
        {
            return SelectedItem != null;
        }
        private void OnDeleteItem()
        {
            if (SelectedItem == null) return;

            Items.Remove(SelectedItem);
        }

        protected virtual void OnAddItem()
        {

        }
    }
}
