using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Savchin.Wpf.Input
{
    public class OverrideCursor : IDisposable 
    { 
        static Stack<Cursor> stack = new Stack<Cursor>();

        /// <summary>
        /// Creates the wait.
        /// </summary>
        /// <returns></returns>
        public static OverrideCursor CreateWait()
        {
            return new OverrideCursor(Cursors.Wait);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverrideCursor"/> class.
        /// </summary>
        /// <param name="changeToCursor">The change to cursor.</param>
        public OverrideCursor(Cursor changeToCursor) 
        { 
            stack.Push(changeToCursor); 
            if (Mouse.OverrideCursor != changeToCursor)       
                Mouse.OverrideCursor = changeToCursor; 
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() 
        { 
            stack.Pop(); 
            Cursor cursor = stack.Count > 0 ? stack.Peek() : null; 
            if (cursor != Mouse.OverrideCursor)       
                Mouse.OverrideCursor = cursor; 
        } 
    }
}
