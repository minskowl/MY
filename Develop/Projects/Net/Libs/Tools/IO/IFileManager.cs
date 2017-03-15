using System.Collections.Generic;
using System.IO;

namespace Savchin.IO
{
    /// <summary>
    /// IFileManager
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to. </param><param name="contents">The string to write to the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.GetInvalidPathChars"/>. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null or <paramref name="contents"/> is empty.  </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        void WriteAllText(string path, string contents);

        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        void WriteAllLines(string path, IEnumerable<string> contents);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// 
        /// <returns>
        /// A string containing all lines of the file.
        /// </returns>
        /// <param name="path">The file to open for reading. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.GetInvalidPathChars "/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        string ReadAllText(string path);

        /// <summary>
        /// Reads the lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        IEnumerable<string> ReadLines(string path);

        /// <summary>
        // Deletes a file. The file specified by the designated path is deleted. 
        // If the file does not exist, Delete succeeds without throwing
        // an exception.
        /// </summary>
        /// <param name="path">The path.</param>
        void Delete(string path);

        /// <summary>
        /// Validates the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        string ValidateFileName(string fileName);
        /// <summary>
        /// Determines whether [is valid file name] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        bool IsValidFileName(string fileName);

        /// <summary>
        /// Determines whether the specified file name is exists.
        /// </summary>
        /// <param name="filePath">Name of the file.</param>
        /// <returns></returns>
        bool Exists(string filePath);

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        void Copy(string sourceFileName, string destFileName);

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        void Copy(string sourceFileName, string destFileName, bool overwrite);

        /// <summary>
        /// Moves the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        void Move(string sourceFileName, string destFileName);

        /// <summary>
        /// Opens the read.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        FileStream OpenRead(string path);

        /// <summary>
        /// Opens the write.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        FileStream OpenWrite(string path);


        /// <summary>
        /// Gets the file information.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        IFileInfo GetFileInfo(string path);

        /// <summary>
        /// Opens the text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        StreamReader OpenText(string path);

        /// <summary>
        /// Opens the stream.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        Stream OpenStream(string path);

        /// <summary>
        /// Appends all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        void AppendAllText(string path, string contents);
    }
}