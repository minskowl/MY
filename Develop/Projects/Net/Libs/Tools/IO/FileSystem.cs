using CI.Common.Interfaces;
using Savchin.IO;

namespace CI.Common.IO
{
    /// <summary>
    /// FileSystem class.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFileManager File { get; private set; }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        public IDirectoryManager Directory { get; private set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public IPathManager Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystem"/> class.
        /// </summary>
        public FileSystem()
        {
            File = new FileManager();
            Directory = new DirectoryManager();
            Path = new PathManager();
        }
    }
}
