using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace KnowledgeBase.Controls
{
    public class FileSystemContentProvider : FileBrowserContentProvider
    {
        // Methods
        public FileSystemContentProvider(HttpContext context, string[] searchPatterns, string[] viewPaths, string[] uploadPaths, string[] deletePaths, string selectedUrl, string selectedItemTag)
            : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
        {
            this.ProcessPaths(base.ViewPaths);
            this.ProcessPaths(base.UploadPaths);
            this.ProcessPaths(base.DeletePaths);
            base.SelectedUrl = FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(base.SelectedUrl));
        }

        protected bool CanDelete(string path)
        {
            foreach (string str in base.DeletePaths)
            {
                if (this.IsParentOf(str, path))
                {
                    return true;
                }
            }
            return false;
        }

        protected bool CanUpload(string path)
        {
            foreach (string str in base.UploadPaths)
            {
                if (this.IsParentOf(str, path))
                {
                    return true;
                }
            }
            return false;
        }

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

        public override string DeleteDirectory(string path)
        {
            string str = base.Context.Server.MapPath(path);
            try
            {
                if (Directory.Exists(str))
                {
                    Directory.Delete(str, true);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return "NoPermissionsToDeleteFolder";
            }
            return string.Empty;
        }

        public override string DeleteFile(string path)
        {
            string str = base.Context.Server.MapPath(path);
            try
            {
                if (File.Exists(str))
                {
                    if ((File.GetAttributes(str) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        return "FileReadOnly";
                    }
                    File.Delete(str);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return "NoPermissionsToDeleteFile";
            }
            return string.Empty;
        }

        protected string GetAbsolutePath(string path)
        {
            if (path.StartsWith("~"))
            {
                path = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.AppendTrailingSlash(base.Context.Request.ApplicationPath) + path.Substring(2));
                return path.Substring(0, path.Length - 1);
            }
            return path;
        }

        public override Stream GetFile(string url)
        {
            string path = base.Context.Server.MapPath(FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(url)));
            if (!File.Exists(path))
            {
                return null;
            }
            return File.OpenRead(path);
        }

        public override string GetFileName(string url)
        {
            return Path.GetFileName(FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(url)));
        }

        public override string GetPath(string url)
        {
            return VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.GetDirectory(FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(url))).Replace(@"\", "/")));
        }

        protected PathPermissions GetPermissions(string path)
        {
            PathPermissions read = PathPermissions.Read;
            if (this.CanUpload(path))
            {
                read |= PathPermissions.Upload;
            }
            if (this.CanDelete(path))
            {
                read |= PathPermissions.Delete;
            }
            return read;
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
                paths[i] = this.GetAbsolutePath(paths[i]);
            }
        }

        public override DirectoryItem ResolveDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(base.Context.Server.MapPath(path));
            if (!dir.Exists)
            {
                return null;
            }
            DirectoryLister lister = new DirectoryLister(this, true, false);
            return lister.GetDirectory(dir, path);
        }

        public override DirectoryItem[] ResolveRootDirectoryAsList(string path)
        {
            DirectoryFlattener flattener = new DirectoryFlattener(this.ResolveRootDirectoryAsTree(path));
            return flattener.Directories;
        }

        public override DirectoryItem ResolveRootDirectoryAsTree(string path)
        {
            string str = base.Context.Server.MapPath(path);
            string virtualName = (path == "/") ? string.Empty : VirtualPathUtility.GetFileName(path);
            string location = (path == "/") ? "/" : VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.GetDirectory(path));
            DirectoryInfo dir = new DirectoryInfo(str);
            if (!dir.Exists)
            {
                return null;
            }
            DirectoryLister lister = new DirectoryLister(this, false, true);
            return lister.GetDirectory(dir, virtualName, location, path, string.Empty);
        }

        public override string StoreBitmap(Bitmap bitmap, string url, ImageFormat format)
        {
            bitmap.Save(base.Context.Server.MapPath(url), format);
            return url;
        }

        [Obsolete("Please use the other overload of StoreFile()")]
        public override string StoreFile(HttpPostedFile file, string path, string name, params string[] arguments)
        {
            //  return this.StoreFile(new PostedFile(string.Empty, file), path, name, arguments);
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

        // Properties
        public override bool CanCreateDirectory
        {
            get
            {
                return true;
            }
        }

        // Nested Types
        private class DirectoryFlattener
        {
            // Fields
            private ArrayList _directories = new ArrayList();

            // Methods
            public DirectoryFlattener(DirectoryItem item)
            {
                if (item != null)
                {
                    this.Flatten(item);
                    this._directories.Add(item);
                }
            }

            private void Flatten(DirectoryItem item)
            {
                foreach (DirectoryItem item2 in item.Directories)
                {
                    this.Flatten(item2);
                }
                this._directories.AddRange(item.Directories);
                item.ClearDirectories();
            }

            // Properties
            public DirectoryItem[] Directories
            {
                get
                {
                    return (DirectoryItem[])this._directories.ToArray(typeof(DirectoryItem));
                }
            }
        }

        private class DirectoryLister
        {
            // Fields
            private FileSystemContentProvider _contentProvider;
            private bool _includeDirectories;
            private bool _includeFiles;

            // Methods
            public DirectoryLister(FileSystemContentProvider contentProvider, bool includeFiles, bool includeDirectories)
            {
                this._contentProvider = contentProvider;
                this._includeFiles = includeFiles;
                this._includeDirectories = includeDirectories;
            }

            protected DirectoryItem[] GetDirectories(DirectoryInfo directory, string parentPath)
            {
                DirectoryInfo[] directories = directory.GetDirectories();
                ArrayList list = new ArrayList();
                for (int i = 0; i < directories.Length; i++)
                {
                    DirectoryInfo info = directories[i];
                    if (this._contentProvider.IsValid(info))
                    {
                        list.Add(this.GetDirectory(info, VirtualPathUtility.AppendTrailingSlash(parentPath) + info.Name));
                    }
                }
                return (DirectoryItem[])list.ToArray(typeof(DirectoryItem));
            }

            /// <summary>
            /// Gets the directory.
            /// </summary>
            /// <param name="dir">The dir.</param>
            /// <param name="fullPath">The full path.</param>
            /// <returns></returns>
            public DirectoryItem GetDirectory(DirectoryInfo dir, string fullPath)
            {
                string tag = this.IsListMode ? fullPath : string.Empty;
                return this.GetDirectory(dir, dir.Name, string.Empty, fullPath, tag);
            }

            /// <summary>
            /// Gets the directory.
            /// </summary>
            /// <param name="dir">The dir.</param>
            /// <param name="virtualName">Name of the virtual.</param>
            /// <param name="location">The location.</param>
            /// <param name="fullPath">The full path.</param>
            /// <param name="tag">The tag.</param>
            /// <returns></returns>
            public DirectoryItem GetDirectory(DirectoryInfo dir, string virtualName, string location, string fullPath, string tag)
            {
                PathPermissions permissions = this._contentProvider.GetPermissions(fullPath);
                bool flag = this.IsListMode ? this.IncludeDirectories : this.IsParentOf(fullPath, this.SelectedUrl);
                bool flag2 = this.IsParentOf(fullPath, this.SelectedUrl);
                DirectoryItem[] directories = flag ? this.GetDirectories(dir, fullPath) : new DirectoryItem[0];
                return new DirectoryItem(virtualName, location, fullPath, tag, permissions, flag2 ? this.GetFiles(dir, permissions, fullPath + "/") : new FileItem[0], directories);
            }

            protected FileItem[] GetFiles(DirectoryInfo directory, PathPermissions permissions, string location)
            {
                ArrayList list = new ArrayList();
                Hashtable hashtable = new Hashtable();
                foreach (string str in this._contentProvider.SearchPatterns)
                {
                    foreach (FileInfo info in directory.GetFiles(str))
                    {
                        if (!hashtable.ContainsKey(info.FullName) && this._contentProvider.IsValid(info))
                        {
                            hashtable.Add(info.FullName, string.Empty);
                            string tag = this.IsListMode ? (location + info.Name) : string.Empty;
                            list.Add(new FileItem(info.Name, info.Extension, info.Length, string.Empty, string.Empty, tag, permissions));
                        }
                    }
                }
                return (FileItem[])list.ToArray(typeof(FileItem));
            }

            protected bool IsParentOf(string virtualParent, string virtualChild)
            {
                return this._contentProvider.IsParentOf(virtualParent, virtualChild);
            }

            // Properties
            protected bool IncludeDirectories
            {
                get
                {
                    return this._includeDirectories;
                }
            }

            protected bool IncludeFiles
            {
                get
                {
                    return this._includeFiles;
                }
            }

            protected bool IsListMode
            {
                get
                {
                    return (this._contentProvider.DisplayMode == FileBrowserDisplayMode.List);
                }
            }

            protected string SelectedUrl
            {
                get
                {
                    return this._contentProvider.SelectedUrl;
                }
            }
        }
    }


}
