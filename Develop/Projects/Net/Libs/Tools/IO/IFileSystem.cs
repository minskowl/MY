using CI.Common.Interfaces;

namespace Savchin.IO
{
    /// <summary>
    /// Class provide to access to FileSystem.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        IFileManager File { get; }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        IDirectoryManager Directory { get; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        IPathManager Path { get; }
    }
}