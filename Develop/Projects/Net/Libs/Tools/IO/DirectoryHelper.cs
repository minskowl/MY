#region Version & Copyright
/* 
 * $Id: DirectoryHelper.cs 20476 2007-08-24 14:51:14Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.IO;

namespace Savchin.IO
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// Copies the directory.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void Copy(string source, string destination)
        {
            Copy(new DirectoryInfo(source), new DirectoryInfo(destination));
        }


        /// <summary>
        /// Creates if not exists.
        /// </summary>
        /// <param name="dir">The dir.</param>
        public static void CreateIfNotExists(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// Copies the directory.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void Copy(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destination.FullName,
                    file.Name));
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                Copy(dir, new DirectoryInfo(destinationDir));
            }
        }

        /// <summary>
        /// Clears the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public static void Clear(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (var directoryInfo in directory.GetDirectories())
            {
                directoryInfo.Delete(true);
            }
        }

        /// <summary>
        /// Clears the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public static void Clear(string directory)
        {
            Clear(new DirectoryInfo(directory));
        }
        /// <summary>
        /// Deletes the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public static void Delete(string directory)
        {
            if (Directory.Exists(directory))
                new DirectoryInfo(directory).Delete(true);
        }
    }
}
