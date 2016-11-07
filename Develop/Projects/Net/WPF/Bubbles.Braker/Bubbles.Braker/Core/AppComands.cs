using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Savchin.Bubbles.Core
{
    public static class AppCommands
    {
        public static readonly RoutedUICommand UndoCommand = new RoutedUICommand("Undo", "Undo", typeof(AppCommands),
            new InputGestureCollection{ new KeyGesture(Key.Z,ModifierKeys.Control) });

        public static readonly RoutedUICommand RedoCommand = new RoutedUICommand("Redo", "Redo", typeof(AppCommands),
    new InputGestureCollection { new KeyGesture(Key.Y, ModifierKeys.Control) });
    }
}
