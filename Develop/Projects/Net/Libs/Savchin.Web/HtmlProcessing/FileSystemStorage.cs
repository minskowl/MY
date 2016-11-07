using System;
using System.IO;

namespace Savchin.Web.HtmlProcessing
{
    /// <summary>
    /// Saves files to specific file system location
    /// </summary>
    public class FileSystemStorage : IFileStorage
    {
        #region Fields

        /// <summary>
        /// Gets or sets the root file path.
        /// </summary>
        /// <value>The root file path.</value>
        public string RootFilePath { get; set; }

        /// <summary>
        /// Gets or sets the files folder path.
        /// </summary>
        /// <value>The files folder path.</value>
        public string FilesFolderPath { get; set; }

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStorage"/> class.
        /// </summary>
        public FileSystemStorage()
            : this(string.Empty, string.Empty)
        {
        
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStorage"/> class.
        /// </summary>
        /// <param name="rootFilePath">The root file path.</param>
        public FileSystemStorage(string rootFilePath)
            : this(rootFilePath, Path.GetDirectoryName(rootFilePath))
        {
    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStorage"/> class.
        /// </summary>
        /// <param name="rootFilePath">The root file path.</param>
        /// <param name="filesFolderPath">The files folder path.</param>
        public FileSystemStorage(string rootFilePath, string filesFolderPath)
        {
            FilesFolderPath = filesFolderPath;
            RootFilePath = rootFilePath;
        }

        #endregion

        #region IFileStorage Members

        /// <summary>
        /// Saves specified file to storage
        /// </summary>
        /// <param name="file">File to save</param>
        /// <param name="isRootFile">Specifies if this is root file (requested html page)</param>
        /// <returns>New location of file</returns>
        public string SaveFile(IWebFile file, bool isRootFile)
        {
            if (!Directory.Exists(FilesFolderPath))
                Directory.CreateDirectory(FilesFolderPath);

            var filePath = isRootFile ? RootFilePath : 
                        Path.Combine(FilesFolderPath,GetUniqueFile(GetFileNameFromUrl(file.Url), FilesFolderPath));
            File.WriteAllBytes(filePath, file.Bytes);
            return "file:///" + filePath.Replace(" ", "%20");
        }

        #endregion

        /// <summary>
        /// Gets the unique file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileFolder">The file folder.</param>
        /// <returns></returns>
        private string GetUniqueFile(string fileName, string fileFolder)
        {
            var currentFileName = fileName;
            var attempt = 0;

            do
            {
                if (!File.Exists(Path.Combine(fileFolder, currentFileName)))
                    return currentFileName;
                currentFileName = "(" + (++attempt) + ") " + fileName;
            }
            while (true);
        }


        /// <summary>
        /// Gets the file name from URL.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns>Path from url</returns>
        private string GetFileNameFromUrl(Uri fileUrl)
        {
            var fileName = Path.GetFileName(fileUrl.GetLeftPart(UriPartial.Path));
            return string.IsNullOrEmpty(fileName) ? "file" : fileName;
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion
    }
}