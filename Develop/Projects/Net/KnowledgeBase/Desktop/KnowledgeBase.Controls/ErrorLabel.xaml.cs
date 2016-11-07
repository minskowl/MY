using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Savchin.Validation;
using Savchin.Wpf.Controls.Core;
using Savchin.Wpf.Core;
using ValidationError = Savchin.Validation.ValidationError;

namespace KnowledgeBase.Controls
{
    /// <summary>
    /// Interaction logic for ErrorLabel.xaml
    /// </summary>
    public partial class ErrorLabel 
    {
        private string _propertyName;
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get
            {
                return (string.IsNullOrEmpty(_propertyName)) ? 
                    (string)Text : _propertyName;
            }
            set { _propertyName = value; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public object Text
        {
            get { return label.Content; }
            set { label.Content = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLabel"/> class.
        /// </summary>
        public ErrorLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void ShowErrors(ValidationError[] errors)
        {
            if (errors == null || errors.Length == 0)
            {
                cnlSign.Visibility = Visibility.Hidden;
            }
            else
            {
                cnlSign.Visibility = Visibility.Visible;
            }

        }
        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="title">The title.</param>
        /// <param name="ex">The ex.</param>
        public static void ShowException(DependencyObject form, string title, ValidationException ex)
        {
            ShowException(form, ex);
            ValidationMessage.Show(form as Window, title, ex);
        }

        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="ex">The ex.</param>
        public static void ShowException(DependencyObject form,ValidationException ex)
        {
            foreach (var label in form.FindChildren<ErrorLabel>())
            {
                if (label == null) continue;


                label.ShowErrors(ex.Errors.Where(e => e.PropertyName == label.PropertyName).ToArray());
            }
        }
    }
}
