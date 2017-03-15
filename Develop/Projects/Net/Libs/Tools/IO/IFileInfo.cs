namespace Savchin.IO
{
    public interface IFileInfo
    {
        /// <summary>
        /// Gets the name of the directory.
        /// </summary>
        /// <value>
        /// The name of the directory.
        /// </value>
        string DirectoryName { get; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        string FullName { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        StorageSize Size { get; }
    }
}