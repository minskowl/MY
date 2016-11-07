using System;
using MyCustomWebBrowser.Core;

namespace FlatSearcher.Core
{
    /// <summary>
    /// FlatEventArgs
    /// </summary>
    public class FlatEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlatEventArgs"/> class.
        /// </summary>
        /// <param name="flat">The flat.</param>
        public FlatEventArgs(Flat flat)
        {
            Flat = flat;
        }

        public Flat Flat { get; private set; }
    }


}
