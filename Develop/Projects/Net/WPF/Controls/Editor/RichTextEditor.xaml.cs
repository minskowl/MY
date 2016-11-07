using System.Windows;
using System.Windows.Controls;

namespace Savchin.Wpf.Controls.Editor
{
    /// <summary>
    /// Interaction logic for BindableRichTextbox.xaml
    /// </summary>
    public partial class RichTextEditor : UserControl
    {
        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(RichTextEditor),
          new PropertyMetadata(string.Empty));

        /// <summary>
        /// Initializes a new instance of the <see cref="RichTextEditor"/> class.
        /// </summary>
        public RichTextEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return GetValue(TextProperty) as string; }
            set { 
                SetValue(TextProperty, value);
            }
        }
    }
}
