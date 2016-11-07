using System.Collections.Specialized;
using System.Linq;
using Savchin.Collection.Generic;
using Savchin.Core;
using Savchin.Wpf.Input;

namespace Savchin.Wpf.Core
{

    public interface IListItem
    {
         bool IsSelected { get; set; }
    }

    public class ListItemViewModel<T> : ViewModelBase
        where T : IListItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Items.
        /// </summary>
        /// <value>The Items.</value>
        public ObservableCollectionEx<T> Items { get; private set; }

        internal static readonly string SelectedItemPropertyName = PropertyName.For<ListItemViewModel<T>>(p => p.SelectedItem);
        private T _selectedItem;
        /// <summary>
        /// Gets or sets the SelectedItem.
        /// </summary>
        /// <value>The SelectedItem.</value>
        public T SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value, OnSelectedItemChanged, SelectedItemPropertyName); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the clear command.
        /// </summary>
        /// <value>
        /// The clear command.
        /// </value>
        public DelegateCommand ClearCommand { get; private set; }

        /// <summary>
        /// Gets the delete selected command.
        /// </summary>
        /// <value>
        /// The delete selected command.
        /// </value>
        public DelegateCommand DeleteSelectedCommand { get; private set; }

        #endregion

        #region Constructions

        /// <summary>
        /// Initializes a new instance of the <see cref="ListItemViewModel{T}"/> class.
        /// </summary>
        public ListItemViewModel()
        {
            ClearCommand = new DelegateCommand(OnClearCommand, CanClearCommand);
            DeleteSelectedCommand = new DelegateCommand(OnDeleteSelectedCommand, CanDeleteSelected);

            Items = new ObservableCollectionEx<T>();
            Items.CollectionChanged += OnCollectionChanged;
        }

        #endregion

        #region Event handlers

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ClearCommand.RaiseCanExecuteChanged();
        }

        protected virtual void OnSelectedItemChanged()
        {
            DeleteSelectedCommand.RaiseCanExecuteChanged();
        }

        private void OnDeleteSelectedCommand()
        {
            Items.RemoveRange(Items.Where(e => e.IsSelected).ToArray());
        }

        private void OnClearCommand()
        {
            Items.Clear();
        }

        #endregion

        #region Helper methods

        private bool CanDeleteSelected()
        {
            return SelectedItem != null;
        }

        private bool CanClearCommand()
        {
            return Items != null && Items.Any();
        }

        #endregion

    }
}
