using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Savchin.Collection.Generic;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// TreeNode
    /// </summary>
    public class TreeNode : INotifyPropertyChanged
    {

        #region Properties

        private TreeNode _parent;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the childrens.
        /// </summary>
        /// <value>The childrens.</value>
        public ObservableCollection<TreeNode> Childrens { get; set; }

        private bool? _isChecked = false;
        /// <summary>
        /// Gets/sets the state of the associated UI toggle (ex. CheckBox).
        /// The return value is calculated based on the check state of all
        /// child FooViewModels.  Setting this property to true or false
        /// will set all children to the same check state, and setting it 
        /// to any value will cause the parent to verify its check state.
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }

        private bool _isSelected = false;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        private bool _isExpanded;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }
        private bool _isVisible=true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The text.</param>
        /// <param name="parent">The parent.</param>
        public TreeNode(int id, string text, TreeNode parent)
        {
            _parent = parent;
            Id = id;
            Text = text;
            Childrens = new ObservableCollection<TreeNode>();
        }
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="prop">The prop.</param>
        protected void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
                Childrens.Foreach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null)
                _parent.VerifyCheckState();

            OnPropertyChanged("IsChecked");
        }
        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < Childrens.Count; ++i)
            {
                bool? current = Childrens[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            SetIsChecked(state, false, true);
        }






    }


    /// <summary>
    /// TreeNodeHelper
    /// </summary>
    public static class TreeNodeHelper
    {
        /// <summary>
        /// Gets the selected ids.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="selected">The selected.</param>
        public static void GetSelectedIds(this IEnumerable<TreeNode> items, List<int> selected)
        {
            foreach (TreeNode item in items)
            {
                if (item.IsChecked.HasValue && item.IsChecked.Value)
                {
                    selected.Add(item.Id);
                }
                else
                {
                    GetSelectedIds(item.Childrens, selected);
                }
            }
        }

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static TreeNode FindById(this IEnumerable<TreeNode> items, int id)
        {
            foreach (TreeNode item in items)
            {
                if (item.Id == id) return item;

                var finded = item.Childrens.FindById(id);
                if (finded != null) return finded;

            }
            return null;
        }
    }
}
