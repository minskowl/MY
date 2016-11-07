using System.Windows;
using Savchin.Validation;

namespace Savchin.Wpf.Controls.Core
{
    /// <summary>
    /// Interaction logic for ValidationMessage.xaml
    /// </summary>
    public partial class ValidationMessage : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessage"/> class.
        /// </summary>
        public ValidationMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="ex">The ex.</param>
        public static void Show(Window owner,string title, ValidationException ex )
        {
            var form = new ValidationMessage
                           {
                               Owner = owner,
                               labelTitle = {Content = title},
                               boxText = {Text = ex.GetMessage()}
                           };
            form.ShowDialog();
        }
        private void ButtonCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
