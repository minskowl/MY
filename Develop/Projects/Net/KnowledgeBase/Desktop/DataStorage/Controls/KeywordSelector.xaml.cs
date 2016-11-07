using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Core;
using Savchin.Collection.Generic;
using Savchin.Wpf.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for KeywordSelector.xaml
    /// </summary>
    public partial class KeywordSelector
    {
        private ICollection<Keyword> _data;

        public ICollection<Keyword>  Keywords
        {
            get { return _data; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordSelector"/> class.
        /// </summary>
        public KeywordSelector()
        {
            InitializeComponent();

            if (this.IsDesignMode()) return;

            comboWords.ItemsSource = new ReadOnlyObservableCollection<Keyword>(AppCore.Workspace.Keywords);
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _data = e.NewValue as ICollection<Keyword>;
        }
        /// <summary>
        /// Handles the Click event of the ButtonAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(_data==null) return;

            if (comboWords.SelectedItem != null)
            {
                var keyword = (Keyword)comboWords.SelectedItem;
                if (!_data.Any(k => k.KeywordID == keyword.KeywordID))
                    _data.Add(keyword);
            }
            else if (!string.IsNullOrEmpty(comboWords.Text))
            {
                var text = comboWords.Text.Trim();
                if (!_data.Any(k => string.Compare(k.Name, text, true) == 0))
                    _data.Add(new Keyword
                                  {
                                      KeywordID = -1,
                                      Name = text
                                  });
            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the listWords control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void listWords_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listWords.SelectedItem != null && _data != null)
            {
                _data.Remove((Keyword)listWords.SelectedItem);
            }

        }

    }
}
