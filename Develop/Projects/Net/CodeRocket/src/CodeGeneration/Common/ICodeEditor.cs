using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.CodeGeneration.Common
{
    public interface ICodeEditor
    {
        /// <summary>
        /// Selects all.
        /// </summary>
        void SelectAll();
        /// <summary>
        /// Copies this instance.
        /// </summary>
        void Copy();
        /// <summary>
        /// Pastes this instance.
        /// </summary>
        void Paste();
    }
}
