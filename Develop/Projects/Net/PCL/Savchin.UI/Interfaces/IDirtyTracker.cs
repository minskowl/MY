using System;

namespace Savchin.UI.Interfaces
{
    public interface IDirtyTracker
    {
        /// <summary>
        /// Gets or sets the IsDirty.
        /// </summary>
        /// <value>The IsDirty.</value>
        bool IsDirty { get; }

        /// <summary>
        /// Occurs when [dirty changed].
        /// </summary>
        event EventHandler DirtyChanged;

        /// <summary>
        /// Resets the dirty.
        /// </summary>
        void ResetDirty();
    }
}