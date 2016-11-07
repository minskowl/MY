using System.Collections.Generic;
using System.Diagnostics;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.Desktop.Core;
using Savchin.IO;

namespace KnowledgeBase.Core
{
    /// <summary>
    /// IFileController
    /// </summary>
    public interface IFileController
    {
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <value>The files.</value>
        List<FileInfo> Files { get; }
        /// <summary>
        /// Gets the files dir.
        /// </summary>
        /// <value>The files dir.</value>
        string FilesDir { get; }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        ObjectType ObjectType { get; }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(FileInfo item);
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(FileInfo item);

        /// <summary>
        /// Creates the new.
        /// </summary>
        void CreateNew();
    }

    public class FileInfo
    {

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public object ID
        {
            get { return Instance.ID; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }



        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; private set; }
        public string Icon { get; private set; }
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        internal IFile Instance { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfo"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="directory">The directory.</param>
        public FileInfo(IFile instance, string directory)
        {
            Path = System.IO.Path.Combine(directory, instance.FileName);
            Icon = Mime.ImagesTypes.Contains(System.IO.Path.GetExtension(instance.FileName).Substring(1)) ? Path : "pack://application:,,,/IdeaProvider;component/Resources/Images/File.jpg";
            Name = instance.FileName;
            Instance = instance;
        }

        /// <summary>
        /// Views the file.
        /// </summary>
        public void ViewFile()
        {
            var process = new Process
                              {
                                  StartInfo = { UseShellExecute = true, FileName = Path }
                              };
            process.Start();
        }
    }
}
