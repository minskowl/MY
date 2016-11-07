using System;
using System.IO;

namespace Savchin.IO
{
    /// <summary>
    /// TemporaryFile
    /// </summary>
    public class TemporaryFile : IDisposable
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public TemporaryFile(string filePath)
        {
            Initialize();
            File.Copy(filePath, FilePath);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryFile"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public TemporaryFile(byte[] content)
        {
            Initialize();
            File.WriteAllBytes(FilePath, content);
        }

        private void Initialize()
        {
            FilePath = Path.GetTempFileName();
        }

        // ReSharper disable EmptyGeneralCatchClause
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                File.Delete(FilePath);
            }
            catch
            {
            }

        }
        // ReSharper restore EmptyGeneralCatchClause
    }
}
