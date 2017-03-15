using System.Collections.Generic;
using System.IO;
using Savchin.IO;

namespace CI.Common.Interfaces
{
    /// <summary>
    /// IDirectoryManager
    /// </summary>
    public interface IDirectoryManager
    {
        /// <summary>
        /// Clears the files.
        /// </summary>
        /// <param name="path">The path.</param>
        void ClearFiles(string path);

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns></returns>
        string[] GetFiles(string folderPath);

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileFilter">The file filter.</param>
        /// <returns></returns>
        string[] GetFiles(string folderPath, params string[] fileFilter);

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileFilter">The file filter.</param>
        /// <returns></returns>
        string[] GetFiles(string folderPath, string fileFilter);

        /// <summary>
        /// Gets the folders.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns></returns>
        string[] GetFolders(string folderPath);

        /// <summary>
        /// Existses the specified directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        bool Exists(string path);

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        void CreateDirectory(string path);

        /// <summary>
        /// Deletes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        void Delete(string path, bool recursive);

        /// <summary>
        /// Copies the directory.
        /// </summary>
        /// <param name="pathFrom">The path from.</param>
        /// <param name="pathTo">The path to.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        void Copy(string pathFrom, string pathTo, bool overwrite = false);


        /// <summary>
        /// Gets the file infos.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileFilter">The file filter.</param>
        /// <returns></returns>
        IFileInfo[] GetFileInfos(string folderPath, params string[] fileFilter);

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns></returns>
        string GetParent(string folderPath);

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption);

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption);
    }
}