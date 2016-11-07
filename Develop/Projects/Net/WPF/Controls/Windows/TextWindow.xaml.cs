using System.Windows;

namespace Savchin.Wpf.Controls.Windows
{
    /// <summary>
    /// Interaction logic for TextWindow.xaml
    /// </summary>
    public partial class TextWindow
    {
        public bool CanCancel
        {
            get { return btnCancel.Visibility == Visibility.Visible; }
            set { btnCancel.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public string Value
        {
            get { return txtText.Text; }
            set { txtText.Text = value; }
        }

        public TextWindow()
        {
            InitializeComponent();
        }

    }
}
