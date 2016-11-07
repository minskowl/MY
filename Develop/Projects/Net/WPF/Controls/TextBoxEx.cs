using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Savchin.Wpf.Controls
{

    /// <summary>
    /// Exctended TextBox.
    /// Additional functionality:
    /// *Tripple click select all text 
    /// </summary>
    public class TextBoxEx : TextBox
    {
        /// <summary>
        /// Initializes the <see cref="TextBoxEx"/> class.
        /// </summary>
        static TextBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxEx), new FrameworkPropertyMetadata(typeof(TextBoxEx)));
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.PreviewMouseDown"/> attached routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that one or more mouse buttons were pressed.</param>
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.ClickCount > 2)
            {
                SelectAll();
            }
            base.OnPreviewMouseDown(e);
        }

    }
}
