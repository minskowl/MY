using System;
using System.IO;

namespace Savchin.IO
{
    public class FileInfoProvider : IFileInfo
    {
        private readonly FileInfo _fileInfo;

        /// <summary>
        /// Gets the name of the directory.
        /// </summary>
        /// <value>
        /// The name of the directory.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public string DirectoryName => _fileInfo.DirectoryName;

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName => _fileInfo.FullName;

        private readonly Lazy<StorageSize> _size;
        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public StorageSize Size => _size.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoProvider"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FileInfoProvider(string path)
            : this(new FileInfo(path))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoProvider"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        internal FileInfoProvider(FileInfo info)
        {
            _fileInfo = info;
            _size = new Lazy<StorageSize>(() => new StorageSize(_fileInfo.Length));

        }

    }
}
