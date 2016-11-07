using System.Windows.Input;

namespace Savchin.CodeEditor
{
    public static class CustomCommands
    {
        public static readonly RoutedCommand CtrlSpaceCompletion = new RoutedCommand(
            "CtrlSpaceCompletion", typeof(SharpDevelopTextEditor),
            new InputGestureCollection {
				new KeyGesture(Key.Space, ModifierKeys.Control)
			});
    }
}
