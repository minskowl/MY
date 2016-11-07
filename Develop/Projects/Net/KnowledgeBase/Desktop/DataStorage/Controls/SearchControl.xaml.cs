using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.Desktop.Controls.Docking;
using KnowledgeBase.Desktop.Core;

namespace KnowledgeBase.Desktop.Controls
{

    /// <summary>
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl 
    {
        #region Properties
        private readonly List<EnumSelection<KnowledgeStatus>> _statuses;
        private readonly List<EnumSelection<KnowledgeType>> _types;

        public SearchFilter Filter
        {
            get
            {
                return new SearchFilter
                {
                    Text = boxtText.Text.Trim(),
                    Categories = listCategories.GetSelectedCategories(),
                    Keywords = listKeywords.Keywords.Select(e => e.KeywordID).ToList(),
                    Statuses = _statuses.Where(e => e.IsChecked).Select(e => e.Value).ToList(),
                    Types = _types.Where(e => e.IsChecked).Select(e => e.Value).ToList()
                };
            }
            set
            {
                SetValue(FilterProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Filter.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(SearchFilter), typeof(SearchFilterControl), new UIPropertyMetadata(new SearchFilter()));

        #endregion


        public SearchControl()
        {
            InitializeComponent();


            _statuses = Enum.GetValues(typeof(KnowledgeStatus)).OfType<KnowledgeStatus>()
           .Select(e => new EnumSelection<KnowledgeStatus>(e)).ToList();
            listStatuses.ItemsSource = _statuses;

            _types = Enum.GetValues(typeof(KnowledgeType)).OfType<KnowledgeType>()
                .Select(e => new EnumSelection<KnowledgeType>(e)).ToList();
            listTypes.ItemsSource = _types;

            listKeywords.DataContext = new ObservableCollection<Keyword>();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Find.Execute(Filter, App.Current.MainWindow);
        }

        public class EnumSelection<T>
        {
            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            public T Value { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is checked.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
            /// </value>
            public bool IsChecked { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="EnumSelection"/> class.
            /// </summary>
            public EnumSelection()
            {

            }
            /// <summary>
            /// Initializes a new instance of the <see cref="EnumSelection"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public EnumSelection(T value)
            {
                Value = value;
            }
        }
    }
}
