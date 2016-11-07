using System;
using Savchin.CodeGeneration.Common;

namespace Savchin.CodeGeneration.Controls
{
    public class FileTabWrapperControl : IFileTabControl
    {
        private readonly FileTabControl _instance;
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTabWrapperControl"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public FileTabWrapperControl(FileTabControl instance)
        {
            _instance = instance;
        }

        /// <summary>
        /// Clears the tabs.
        /// </summary>
        public void ClearTabs()
        {
            _instance.TabPages.Clear();
        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void AddFile(string filePath)
        {
            _instance.AddFile(filePath);
        }

        /// <summary>
        /// Saves the selected.
        /// </summary>
        public void SaveSelected()
        {
            _instance.SaveSelected();
        }

        public ICodeEditor AciveEditor
        {
            get { throw new NotImplementedException(); }
        }
    }
}
