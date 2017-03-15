using System.IO;
using CI.Common.Interfaces;
using Savchin.IO;

namespace CI.Common.IO
{
    public class PathManager : IPathManager
    {
        /// <summary>
        /// Gets the random name of the file.
        /// </summary>
        /// <returns></returns>
        public string GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        /// <summary>
        /// Combines the specified path1.
        /// </summary>
        /// <param name="path1">The path1.</param>
        /// <param name="path2">The path2.</param>
        /// <returns></returns>
        public string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }
        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }
        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public string GetFileName(string fileName)
        {
            return Path.GetFileName(fileName);
        }
    }
}