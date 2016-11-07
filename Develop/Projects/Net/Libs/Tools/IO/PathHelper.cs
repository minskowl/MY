using System;
using System.Linq;
using System.IO;
using Savchin.Core;
using Savchin.Text;

namespace Savchin.IO
{
    public class PathHelper
    {
        private static char[] separator = new char[2] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };

        public static char[] IvalidFileNameChars = new char[] { '\\', '/', '|', '*', '?', '"', '<', '>' };

        private static Pair<string, string>[] _replacements;
        /// <summary>
        /// Escapes the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string EscapePath(string path)
        {
            if(_replacements==null)
                _replacements = IvalidFileNameChars.Select(e => new Pair<string, string>(new string(e, 1), String.Format("%{0:X}", (int) e))).ToArray();

            foreach (var replacement in _replacements)
            {
                path = path.Replace(replacement.First, replacement.Second);
            }
            return path;
        }

        /// <summary>
        /// Makes the file name valid.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string MakeFileNameValid(string fileName)
        {
            return StringUtil.Remove(fileName, IvalidFileNameChars);
        }


        /// <summary>
        /// Creates the name of the unique.
        /// </summary>
        /// <param name="sourceName">Name of the source.</param>
        /// <returns></returns>
        public static string CreateUniqueName(string sourceName)
        {
            string result = Path.IsPathRooted(sourceName) ? sourceName : Path.GetFullPath(sourceName);


            if (!File.Exists(result))
                return result;

            string path = Path.GetDirectoryName(result);
            string fileName = Path.GetFileNameWithoutExtension(result);
            string extension = Path.GetExtension(result);
            if (!path.EndsWith("\\"))
                path = path + "\\";
            int i = 0;

            do
            {
                i++;
                result = path + fileName + i + extension;
            } while (File.Exists(result));
            return result;

        }

        /// <summary>
        /// Gets the realitive.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="sourcePath">The source path.</param>
        /// <returns></returns>
        public static string GetRealitive(string basePath, string sourcePath)
        {
            string[] baseParts = basePath.TrimEnd(separator).Split(separator);
            string[] sourceParts = sourcePath.TrimEnd(separator).Split(separator);

            int index = 0;
            try
            {
                while (baseParts[index] == sourceParts[index])
                {
                    index++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            if (baseParts.Length == sourceParts.Length && baseParts.Length == index)
                return ".";

            if (baseParts.Length == index)
            {
                string res = ".";
                for (int i = index; i < sourceParts.Length; i++)
                    res += @"\" + sourceParts[i];
                return res;
            }
            else if (sourceParts.Length == index)
            {
                string res = string.Empty;
                for (int i = index; i < baseParts.Length; i++)
                    res += @"..\";
                return res.TrimEnd(separator);
            }
            else
            {
                string res = string.Empty;
                for (int i = index; i < baseParts.Length; i++)
                    res += @"..\";
                res = res.TrimEnd(separator);
                for (int i = index; i < sourceParts.Length; i++)
                    res += @"\" + sourceParts[i];
                return res;
            }


        }
    }
}
