using System.Windows.Controls;

namespace Reading.Controls
{
    public class IntegerTextBox : TextBox
    {
        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            if("0123456789".IndexOf( e.Text)==-1)
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }
    }
}
