using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.CodeGeneration.Common
{
    public interface IFileTabControl
    {
        /// <summary>
        /// Clears the tabs.
        /// </summary>
        void ClearTabs();

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void AddFile(string filePath);

        /// <summary>
        /// Saves the selected.
        /// </summary>
        void SaveSelected();

        /// <summary>
        /// Acives the editor.
        /// </summary>
        /// <returns></returns>
        ICodeEditor AciveEditor { get; }
    }
}
