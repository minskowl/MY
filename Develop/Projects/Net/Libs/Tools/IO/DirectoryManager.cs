using System.Collections.Generic;
using System.IO;
using System.Linq;
using CI.Common.Interfaces;
using Savchin.Collection.Generic;

namespace Savchin.IO
{
    class DirectoryManager : IDirectoryManager
    {
        private static readonly string[] EmptyStrings = new string[0];
        private static readonly IFileInfo[] EmptyInfos = new IFileInfo[0];
        /// <summary>
        /// Clears the files.
        /// </summary>
        /// <param name="path">The path.</param>
        public void ClearFiles(string path)
        {
            if (!Directory.Exists(path)) return;

            Directory.EnumerateFiles(path).Foreach(File.Delete);
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns></returns>
        public string[] GetFiles(string folderPath)
        {
            return Directory.Exists(folderPath) ? Directory.GetFiles(folderPath) : EmptyStrings;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileFilter">The file filter.</param>
        /// <returns></returns>
        public string[] GetFiles(string folderPath, params string[] fileFilter)
        {
            if (!Directory.Exists(folderPath))
                return EmptyStrings;
            return fileFilter.IsEmpty() ? Directory.GetFiles(folderPath) :
                fileFilter.SelectMany(filter => Directory.GetFiles(folderPath, filter)).ToArray();

        }

        /// <summary>
        /// Gets the file infos.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileFilter">The file filter.</param>
        /// <returns></returns>
        public IFileInfo[] GetFileInfos(string folderPath, params string[] fileFilter)
        {
            if (!Directory.Exists(folderPath))
                return EmptyInfos;

            var directory = new DirectoryInfo(folderPath);

            var infos = fileFilter.IsEmpty() ? directory.GetFiles() :
                fileFilter.SelectMany(filter => directory.GetFiles(filter, SearchOption.TopDirectoryOnly));
            return infos.Select(e => (IFileInfo)new FileInfoProvider(e)).ToArray();
        }
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileFilter">The file filter.</param>
        /// <returns></returns>
        public string[] GetFiles(string folderPath, string fileFilter)
        {
            return Directory.Exists(folderPath) ? Directory.GetFiles(folderPath, fileFilter ?? "*").ToArray() : EmptyStrings;
        }
        /// <summary>
        /// Gets the folders.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns></returns>
        public string[] GetFolders(string folderPath)
        {
            return Directory.Exists(folderPath) ? Directory.GetDirectories(folderPath) : EmptyStrings;
        }

        /// <summary>
        /// Existses the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Deletes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        public void Delete(string path, bool recursive)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }

        /// <summary>
        /// Copies the directory.
        /// </summary>
        /// <param name="pathFrom">The path from.</param>
        /// <param name="pathTo">The path to.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public void Copy(string pathFrom, string pathTo, bool overwrite = false)
        {
            if (!Directory.Exists(pathTo))
                Directory.CreateDirectory(pathTo);

            var fromFolder = new DirectoryInfo(pathFrom);

            var files = fromFolder.GetFiles();
            foreach (var file in files)
            {
                var path = Path.Combine(pathTo, file.Name);
                file.CopyTo(path, overwrite);
            }

            var directories = fromFolder.GetDirectories();
            foreach (var directory in directories)
            {
                var subDirectoryPath = Path.Combine(pathTo, directory.Name);
                Copy(directory.FullName, subDirectoryPath, overwrite);
            }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns></returns>
        public string GetParent(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return string.Empty;

            var parentFolder = Directory.GetParent(folderPath);
            return parentFolder == null ? string.Empty : parentFolder.FullName;
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }
    }
}
