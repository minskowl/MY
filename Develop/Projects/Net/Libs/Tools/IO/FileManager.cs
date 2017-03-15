using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Savchin.IO
{
    internal class FileManager : IFileManager
    {
        private static readonly Regex NamePattern = new Regex(@"^([^\\/:*?\""<>|])+$", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        /// <summary>
        /// Writes all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        public void WriteAllText( string path, string contents)
        {
            if (path == null) throw new ArgumentNullException("path");
            CheckDirectory(path);
            File.WriteAllText(path, contents);
        }



        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            if (path == null) throw new ArgumentNullException("path");
            CheckDirectory(path);
            File.WriteAllLines(path, contents);
        }

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>
        /// A string containing all lines of the file.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Reads the lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }

        /// <summary>
        /// Deletes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// Validates the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public string ValidateFileName(string fileName)
        {
            return NamePattern.IsMatch(fileName) ? null : "File name has invalid characters \\ / : ? <> | * ";
        }

        /// <summary>
        /// Determines whether [is valid file name] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public bool IsValidFileName(string fileName)
        {
            return NamePattern.IsMatch(fileName);
        }

        /// <summary>
        /// Determines whether the specified file name is exists.
        /// </summary>
        /// <param name="filePath">Name of the file.</param>
        /// <returns></returns>
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        public void Copy(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// Moves the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        public void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        /// <summary>
        /// Opens the read.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public FileStream OpenRead(string path)
        {
            return File.OpenRead(path);
        }

        /// <summary>
        /// Opens the write.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public FileStream OpenWrite(string path)
        {
            return File.OpenWrite(path);
        }



        /// <summary>
        /// Gets the file information.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public IFileInfo GetFileInfo(string path)
        {
            return new FileInfoProvider(path);
        }

        /// <summary>
        /// Opens the text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public StreamReader OpenText(string path)
        {
            return File.OpenText(path);
        }

        /// <summary>
        /// Opens the stream.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public Stream OpenStream(string path)
        {
            return File.Open(path, FileMode.OpenOrCreate);
        }

        /// <summary>
        /// Appends all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        public void AppendAllText(string path, string contents)
        {
            File.AppendAllText(path, contents);
        }

        private static void CheckDirectory(string path)
        {
            var dirName = Path.GetDirectoryName(path);
            if (!string.IsNullOrWhiteSpace(dirName) && !Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);
        }
    }
}