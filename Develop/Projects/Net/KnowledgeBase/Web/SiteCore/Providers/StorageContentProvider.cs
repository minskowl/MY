using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;
using KnowledgeBase.SiteCore;

namespace KnowledgeBase.SiteCore.Providers
{
    public class StorageContentProvider : FileBrowserContentProvider
    {
        
        // Methods
        public StorageContentProvider(HttpContext context, string[] searchPatterns, string[] viewPaths,
                                      string[] uploadPaths, string[] deletePaths, string selectedUrl,
                                      string selectedItemTag)
            : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
        {
            ProcessPaths(base.UploadPaths);
            ProcessPaths(base.DeletePaths);
            base.SelectedUrl = RemoveProtocolNameAndServerName(GetAbsolutePath(base.SelectedUrl));
        }

        /// <summary>
        /// Resolves the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectoryItem ResolveDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                return null;
            }
            DirectoryLister lister = new DirectoryLister(this, true, false);
            return lister.GetDirectory(dir, path);
        }

        public override DirectoryItem[] ResolveRootDirectoryAsList(string path)
        {
            DirectoryFlattener flattener = new DirectoryFlattener(ResolveRootDirectoryAsTree(path));
            return flattener.Directories;
        }

        /// <summary>
        /// Resolves the root directory as tree.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectoryItem ResolveRootDirectoryAsTree(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                return null;
            }
            string virtualName = dir.Name;
            string location = dir.Parent.FullName.Replace('\\' ,'/');

            DirectoryLister lister = new DirectoryLister(this, false, true);
            return lister.GetDirectory(dir, virtualName, location, path, string.Empty);

        }

        #region Permissions

        /// <summary>
        /// Gets a value indicating whether the ContentProvider can create directory items or not. The visibility of the
        /// Create New Folder icon is controlled by the value of this property.
        /// </summary>
        /// <value></value>
        public override bool CanCreateDirectory
        {
            get { return false; }
        }

        protected bool CanDelete(string path)
        {
            foreach (string str in base.DeletePaths)
            {
                if (IsParentOf(str, path))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether this instance can upload the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can upload the specified path; otherwise, <c>false</c>.
        /// </returns>
        protected bool CanUpload(string path)
        {
            //foreach (string str in base.UploadPaths)
            //{
            //    if (IsParentOf(str, path))
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

        protected PathPermissions GetPermissions(string path)
        {
            //PathPermissions read = PathPermissions.Read;
            //if (CanUpload(path))
            //{
            //    read |= PathPermissions.Upload;
            //}
            //if (CanDelete(path))
            //{
            //    read |= PathPermissions.Delete;
            //}
            //return read;
            return PathPermissions.Read;
        }
        #endregion

        public override string CreateDirectory(string path, string name)
        {
            if (name.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                return "InvalidCharactersInPath";
            }
            string str = base.Context.Server.MapPath(path + name);
            try
            {
                Directory.CreateDirectory(str);
            }
            catch (UnauthorizedAccessException)
            {
                return "NoPermissionsToCreateFolder";
            }
            return string.Empty;
        }

        /// <summary>
        /// Deletes the directory item with the given virtual path.
        /// </summary>
        /// <param name="path">The virtual path of the directory item.</param>
        /// <returns>
        /// string.Empty when the operation was successful; otherwise an error message token.
        /// </returns>
        public override string DeleteDirectory(string path)
        {
            //string str = base.Context.Server.MapPath(path);
            //try
            //{
            //    if (Directory.Exists(str))
            //    {
            //        Directory.Delete(str, true);
            //    }
            //}
            //catch (UnauthorizedAccessException)
            //{
            //    return "NoPermissionsToDeleteFolder";
            //}
            return string.Empty;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string DeleteFile(string path)
        {
            //string str = base.Context.Server.MapPath(path);
            //try
            //{
            //    if (File.Exists(str))
            //    {
            //        if ((File.GetAttributes(str) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            //        {
            //            return "FileReadOnly";
            //        }
            //        File.Delete(str);
            //    }
            //}
            //catch (UnauthorizedAccessException)
            //{
            //    return "NoPermissionsToDeleteFile";
            //}
            return string.Empty;
        }

        protected string GetAbsolutePath(string path)
        {
            if (path.StartsWith("~"))
            {
                path =
                    VirtualPathUtility.AppendTrailingSlash(
                        VirtualPathUtility.AppendTrailingSlash(base.Context.Request.ApplicationPath) + path.Substring(2));
                return path.Substring(0, path.Length - 1);
            }
            return path;
        }

        public override Stream GetFile(string url)
        {
            string path = base.Context.Server.MapPath(RemoveProtocolNameAndServerName(GetAbsolutePath(url)));
            if (!File.Exists(path))
            {
                return null;
            }
            return File.OpenRead(path);
        }

        public override string GetFileName(string url)
        {
            return Path.GetFileName(RemoveProtocolNameAndServerName(GetAbsolutePath(url)));
        }

        public override string GetPath(string url)
        {
            return
                VirtualPathUtility.AppendTrailingSlash(
                    VirtualPathUtility.AppendTrailingSlash(
                        VirtualPathUtility.GetDirectory(RemoveProtocolNameAndServerName(GetAbsolutePath(url))).Replace(
                            @"\", "/")));
        }



        protected bool IsParentOf(string virtualParent, string virtualChild)
        {
            return virtualChild.ToLower().StartsWith(virtualParent.ToLower());
        }

        protected virtual bool IsValid(DirectoryInfo directory)
        {
            return true;
        }

        protected virtual bool IsValid(FileInfo file)
        {
            return true;
        }

        public void ProcessPaths(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = GetAbsolutePath(paths[i]);
            }
        }


        /// <summary>
        /// Stores an image with the given url and image format.
        /// </summary>
        /// <param name="bitmap">The Bitmap object to be stored.</param>
        /// <param name="url">The url of the bitmap.</param>
        /// <param name="format">The image format of the bitmap.</param>
        /// <returns>
        /// string.Empty when the operation was successful; otherwise an error message token.
        /// </returns>
        /// <remarks>
        /// Used when creating thumbnails in the ImageManager dialog.
        /// </remarks>
        public override string StoreBitmap(Bitmap bitmap, string url, ImageFormat format)
        {
            //bitmap.Save(base.Context.Server.MapPath(url), format);
            //return url;
            return string.Empty;
        }

        /// <summary>
        /// Creates a file item from a HttpPostedFile to the given path with the given name.
        /// </summary>
        /// <param name="file">The uploaded HttpPostedFile to store.</param>
        /// <param name="path">The virtual path where the file item should be created.</param>
        /// <param name="name">The name of the file item.</param>
        /// <param name="arguments">Additional values to be stored such as Description, DisplayName, etc.</param>
        /// <returns>
        /// String containing the full virtual path (including the file name) of the file item.
        /// </returns>
        /// <remarks>
        /// The default FileUploader control does not include the arguments parameter. If you need additional arguments
        /// you should create your own FileUploader control.
        /// </remarks>
        [Obsolete("Please use the other overload of StoreFile()")]
        public override string StoreFile(HttpPostedFile file, string path, string name, params string[] arguments)
        {
            //return StoreFile(new PostedFile(string.Empty, file), path, name, arguments);
            return string.Empty;
        }

        public override string StoreFile(UploadedFile file, string path, string name, params string[] arguments)
        {
            string str = Path.Combine(path, name);
            if (File.Exists(str))
            {
                File.Delete(str);
            }
            file.SaveAs(base.Context.Server.MapPath(str));
            return str;
        }


        #region Nested type: DirectoryFlattener

        private class DirectoryFlattener
        {
            // Fields
            private readonly ArrayList _directories = new ArrayList();

            // Methods
            public DirectoryFlattener(DirectoryItem item)
            {
                if (item != null)
                {
                    Flatten(item);
                    _directories.Add(item);
                }
            }

            // Properties
            public DirectoryItem[] Directories
            {
                get { return (DirectoryItem[])_directories.ToArray(typeof(DirectoryItem)); }
            }

            private void Flatten(DirectoryItem item)
            {
                foreach (DirectoryItem item2 in item.Directories)
                {
                    Flatten(item2);
                }
                _directories.AddRange(item.Directories);
                item.ClearDirectories();
            }
        }

        #endregion

        #region Nested type: DirectoryLister

        private class DirectoryLister
        {
            // Fields
            private readonly StorageContentProvider _contentProvider;
            private readonly bool _includeDirectories;
            private readonly bool _includeFiles;

            // Methods
            public DirectoryLister(StorageContentProvider contentProvider, bool includeFiles, bool includeDirectories)
            {
                _contentProvider = contentProvider;
                _includeFiles = includeFiles;
                _includeDirectories = includeDirectories;
            }

            protected bool IncludeDirectories
            {
                get { return _includeDirectories; }
            }

            protected bool IncludeFiles
            {
                get { return _includeFiles; }
            }

            protected bool IsListMode
            {
                get { return (_contentProvider.DisplayMode == FileBrowserDisplayMode.List); }
            }

            protected string SelectedUrl
            {
                get { return _contentProvider.SelectedUrl; }
            }

            protected DirectoryItem[] GetDirectories(DirectoryInfo directory, string parentPath)
            {
                DirectoryInfo[] directories = directory.GetDirectories();
                ArrayList list = new ArrayList();
                for (int i = 0; i < directories.Length; i++)
                {
                    DirectoryInfo info = directories[i];
                    if (_contentProvider.IsValid(info))
                    {
                        list.Add(GetDirectory(info, VirtualPathUtility.AppendTrailingSlash(parentPath) + info.Name));
                    }
                }
                return (DirectoryItem[])list.ToArray(typeof(DirectoryItem));
            }

            public DirectoryItem GetDirectory(DirectoryInfo dir, string fullPath)
            {
                string tag = IsListMode ? fullPath : string.Empty;
                return GetDirectory(dir, dir.Name, string.Empty, fullPath, tag);
            }

            public DirectoryItem GetDirectory(DirectoryInfo dir, string virtualName, string location, string fullPath,
                                              string tag)
            {
                PathPermissions permissions = _contentProvider.GetPermissions(fullPath);
                bool showDirictories = IsListMode ? IncludeDirectories : IsParentOf(fullPath, SelectedUrl);
                bool showFiles = IsParentOf(fullPath, SelectedUrl);
                DirectoryItem[] directories = showDirictories ? GetDirectories(dir, fullPath) : new DirectoryItem[0];
                return new DirectoryItem(virtualName, location, fullPath, tag, permissions,
                                         showFiles ? GetFiles(dir, permissions, fullPath + "/") : new FileItem[0], directories);
            }

            protected FileItem[] GetFiles(DirectoryInfo directory, PathPermissions permissions, string location)
            {
                ArrayList list = new ArrayList();
                Hashtable hashtable = new Hashtable();
                foreach (string str in _contentProvider.SearchPatterns)
                {
                    foreach (FileInfo info in directory.GetFiles(str))
                    {
                        if (!hashtable.ContainsKey(info.FullName) && _contentProvider.IsValid(info))
                        {
                            hashtable.Add(info.FullName, string.Empty);
                            string tag = IsListMode ? (location + info.Name) : string.Empty;
                            list.Add(new FileItem(info.Name, info.Extension, info.Length, string.Empty,
                                                  "file:///" + info.FullName, tag, permissions));
                        }
                    }
                }
                return (FileItem[])list.ToArray(typeof(FileItem));
            }

            protected bool IsParentOf(string virtualParent, string virtualChild)
            {
                return _contentProvider.IsParentOf(virtualParent, virtualChild);
            }

            // Properties
        }

        #endregion
    }
}