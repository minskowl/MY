using System.Windows.Input;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// Commands
    /// </summary>
    public static class AppCommands
    {
        public static readonly RoutedUICommand Add;
        /// <summary>
        /// View
        /// </summary>
        public static readonly RoutedUICommand View;
        /// <summary>
        /// Edit
        /// </summary>
        public static readonly RoutedUICommand Edit;
        /// <summary>
        /// Edit
        /// </summary>
        public static readonly RoutedUICommand Delete;
        /// <summary>
        /// Forward
        /// </summary>
        public static readonly RoutedUICommand Forward;
        /// <summary>
        /// Backward
        /// </summary>
        public static readonly RoutedUICommand Backward;

        /// <summary>
        /// Export
        /// </summary>
        public static readonly RoutedUICommand Export;

        /// <summary>
        /// HideToTray
        /// </summary>
        public static readonly RoutedUICommand HideToTray;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCommands"/> class.
        /// </summary>
        static AppCommands()
        {
            var type = typeof (AppCommands);
            View = new RoutedUICommand("View", "View", type);
            View.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));
            View.InputGestures.Add(new KeyGesture(Key.Enter));
            View.InputGestures.Add(new MouseGesture(MouseAction.LeftDoubleClick));


            Edit = new RoutedUICommand("Edit", "Edit", type);
            Edit.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));

            Delete = new RoutedUICommand("Delete", "Delete", type);
            Delete.InputGestures.Add(new KeyGesture(Key.Delete));
            Delete.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));



            Add = new RoutedUICommand("Add", "Add", type);
            Add.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
            Add.InputGestures.Add(new KeyGesture(Key.Insert));

            Forward = new RoutedUICommand("Forward", "Forward", type);
            Forward.InputGestures.Add(new KeyGesture(Key.Y, ModifierKeys.Alt));

            Backward = new RoutedUICommand("Backward", "Backward", type);
            Backward.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Alt));

            Export = new RoutedUICommand("Export", "Export", type);
            Export.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));

            HideToTray = new RoutedUICommand("Hide to Tray", "HideToTray", type);
            HideToTray.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Alt));
            
        }

        /// <summary>
        /// Adds the new knowledge.
        /// </summary>
        public static void AddNewKnowledge()
        {
            Add.Execute(new ItemParams(ObjectType.Knowledge, 0), App.Current.MainWindow);
        }

    }
}
