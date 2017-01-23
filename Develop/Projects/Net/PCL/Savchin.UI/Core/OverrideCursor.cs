using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Savchin.Wpf.Input
{
    public class OverrideCursor : IDisposable
    {
        static Stack<CoreCursor> stack = new Stack<CoreCursor>();
        private static CoreCursor WaitCursor = new CoreCursor(CoreCursorType.Wait, 20);
        /// <summary>
        /// Creates the wait.
        /// </summary>
        /// <returns></returns>
        public static OverrideCursor CreateWait()
        {
            return new OverrideCursor(WaitCursor);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverrideCursor"/> class.
        /// </summary>
        /// <param name="changeToCursor">The change to cursor.</param>
        public OverrideCursor(CoreCursor changeToCursor)
        {
            stack.Push(changeToCursor);

            if (Window.Current.CoreWindow.PointerCursor != changeToCursor)
                Window.Current.CoreWindow.PointerCursor = changeToCursor;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            stack.Pop();
            var cursor = stack.Count > 0 ? stack.Peek() : null;
            Window.Current.CoreWindow.PointerCursor = cursor;
        }
    }
}
