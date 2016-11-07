#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.IO;
using System.Text.RegularExpressions;
using Savchin.Text;

namespace Savchin.IO
{
    /// <summary>
    /// SizeUnit
    /// </summary>
    public enum SizeUnit
    {
        /// <summary>
        /// Giga byte
        /// </summary>
        Gb,
        /// <summary>
        /// Mega Byte
        /// </summary>
        Mb,
        /// <summary>
        /// Kylo Byte
        /// </summary>
        Kb,
        /// <summary>
        /// Byte
        /// </summary>
        b
    }
    public struct StorageSize
    {
        public const int SizeKb = 1024;
        public const int SizeMb = SizeKb * SizeKb;
        public const int SizeGb = SizeKb * SizeKb * SizeKb;
        private long size;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public long Size
        {
            get { return size; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("size must be more than zero", "value");
                size = value;
            }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="StorageSize"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public StorageSize(long size)
        {
            this.size = size;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageSize"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public StorageSize(StorageSize size)
            : this(size.size)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageSize"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="unit">The unit.</param>
        public StorageSize(int size, SizeUnit unit)
        {
            this.size = GetByteSize(size, unit);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageSize"/> class.
        /// </summary>
        /// <param name="sizeString">The size string.</param>
        public StorageSize(string sizeString)
        {
            this.size = 0;
            FromString(sizeString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageSize"/> class.
        /// </summary>
        /// <param name="o">The o.</param>
        public StorageSize(object o)
        {
            this.size = 0;
            if (o is string)
                FromString((string)o);
            else
                Size = Convert.ToInt64(o);
        }
        /// <summary>
        /// Gets the unit count.
        /// </summary>
        /// <param name="byteSize">Size of the byte.</param>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public static long GetUnitCount(long byteSize, SizeUnit unit)
        {
            switch (unit)
            {
                case SizeUnit.Gb:
                    return byteSize / SizeGb;
                case SizeUnit.Mb:
                    return byteSize / SizeMb;
                case SizeUnit.Kb:
                    return byteSize / SizeKb;
                case SizeUnit.b:
                    return byteSize;
                default:
                    return byteSize;
            }
        }

        /// <summary>
        /// Gets the size of the byte.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public static long GetByteSize(int size, SizeUnit unit)
        {
            switch (unit)
            {
                case SizeUnit.Gb:
                    return size * SizeGb;
                case SizeUnit.Mb:
                    return size * SizeMb;
                case SizeUnit.Kb:
                    return size * SizeKb;
                case SizeUnit.b:
                    return size;
                default:
                    return size;
            }
        }

        /// <summary>
        /// Gets the unit count.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns></returns>
        public long GetUnitCount(SizeUnit unit)
        {
            return GetUnitCount(size, unit);
        }

        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="sizeString">The size string.</param>
        public void FromString(string sizeString)
        {
            Match match = RegularExpressions.StorageSizeRegex.Match(sizeString);
            size = long.Parse(match.Groups["digit"].Value);
            if (match.Groups["key"].Success)
            {
                string key = match.Groups["key"].Value.ToLower();
                switch (key)
                {
                    case "kb":
                        size *= SizeKb;
                        break;
                    case "mb":
                        size *= SizeMb;
                        break;
                    case "gb":
                        size *= SizeGb;
                        break;
                    default:
                        throw new NotImplementedException(key);
                }
            }
        }

        /// <summary>
        /// Froms the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        public void FromFolder(string folderName)
        {
            size = GetFolderSize(folderName);
        }



        private long GetFolderSize(string physicalPath)
        {
            long dblDirSize = 0;
            DirectoryInfo objDirInfo = new DirectoryInfo(physicalPath);
            FileInfo[] arrChildFiles = objDirInfo.GetFiles();
            DirectoryInfo[] arrSubFolders = objDirInfo.GetDirectories();
            foreach (FileInfo objChildFile in arrChildFiles)
            {
                dblDirSize += objChildFile.Length;
            }
            foreach (DirectoryInfo objChildFolder in arrSubFolders)
            {
                dblDirSize += GetFolderSize(objChildFolder.FullName);
            }
            return dblDirSize;
        }
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterPriority>2</filterPriority>
        public override string ToString()
        {
            if (size >= SizeGb)
                return string.Format("{0:#,##0.00} GB", (double)size / SizeGb);
            else if (size >= SizeMb)
                return string.Format("{0:#,##0.00} MB", (double)size / SizeMb);
            else if (size >= SizeKb)
                return string.Format("{0:#,##0.00} KB", (double)size / SizeKb);
            else
                return string.Format("{0:#,##0.00} B", size);
        }
    }
}
