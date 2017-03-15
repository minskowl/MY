namespace Savchin.IO
{
    public interface IPathManager
    {
        /// <summary>
        /// Gets the name of the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string GetDirectoryName(string path);
        /// <summary>
        /// Gets the random name of the file.
        /// </summary>
        /// <returns></returns>
        string GetRandomFileName();

        /// <summary>
        /// Combines the specified path1.
        /// </summary>
        /// <param name="path1">The path1.</param>
        /// <param name="path2">The path2.</param>
        /// <returns></returns>
        string Combine(string path1, string path2);

        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string GetExtension(string path);

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        string GetFileName(string fileName);
    }
}