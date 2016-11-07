using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using Telerik.Web.UI.Widgets;

namespace KnowledgeBase.SiteCore.Providers
{
    public class KnowledgeContentProvider : FileBrowserContentProvider
    {
        private static char[] slashArray = new char[] { '/' };
        private int? knowledgeID;

        private string _itemHandlerPath;
        private string ItemHandlerPath
        {
            get
            {
                return _itemHandlerPath;
            }
        }

        private PathPermissions fullPermissions = PathPermissions.Read | PathPermissions.Delete | PathPermissions.Upload;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeContentProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="searchPatterns">The search patterns.</param>
        /// <param name="viewPaths">The view paths.</param>
        /// <param name="uploadPaths">The upload paths.</param>
        /// <param name="deletePaths">The delete paths.</param>
        /// <param name="selectedUrl">The selected URL.</param>
        /// <param name="selectedItemTag">The selected item tag.</param>
        public KnowledgeContentProvider(HttpContext context, string[] searchPatterns, string[] viewPaths, string[] uploadPaths, string[] deletePaths, string selectedUrl, string selectedItemTag)
            :
                base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
        {
            _itemHandlerPath = AppSettings.ImageProviderBaseUrl;
            if (SelectedItemTag != null && SelectedItemTag != string.Empty)
            {
                SelectedItemTag = ExtractPath(RemoveProtocolNameAndServerName(SelectedItemTag));
            }

            knowledgeID = KbContext.CurrentKb.CurrentKnowledgeID;
        }



        /// <summary>
        /// Loads a root directory with given path, where all subdirectories
        /// contained in the SelectedUrl property are loaded
        /// </summary>
        /// <remarks>
        /// The ImagesPaths, DocumentsPaths, etc properties of RadEditor
        /// allow multiple root items to be specified, separated by comma, e.g.
        /// Photos,Paintings,Diagrams. The FileBrowser class calls the
        /// ResolveRootDirectoryAsTree method for each of them.
        /// </remarks>
        /// <param name="path">the root directory path, passed by the FileBrowser</param>
        /// <returns>The root DirectoryItem or null if such does not exist</returns>
        public override DirectoryItem ResolveRootDirectoryAsTree(string path)
        {
            DirectoryItem returnValue =
                new DirectoryItem(GetName(path), GetDirectoryPath(path), string.Empty, string.Empty, PathPermissions.Read, GetChildFiles(path),
                                  GetChildDirectories(path));
            return returnValue;
        }

        /// <summary>
        /// Resolves the root directory as list.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectoryItem[] ResolveRootDirectoryAsList(string path)
        {
            //DataRow[] directoryRows = DataServer.GetAllDirectoryRows(path);
            //DirectoryItem[] directories = new DirectoryItem[directoryRows.Length];
            //for (int i = 0; i < directoryRows.Length; i++)
            //{
            //    DataRow row = directoryRows[i];
            //    string fullPath = RemoveLastSlash(DataServer.GetItemPath(row));
            //    directories[i] = new DirectoryItem(
            //        row["Name"].ToString(), 
            //        GetDirectoryPath(fullPath), 
            //        string.Empty, 
            //        string.Empty, 
            //        fullPermissions, 
            //        GetChildFiles(fullPath),
            //        new DirectoryItem[] { });
            //}
            //return directories;
            List<DirectoryItem> result = new List<DirectoryItem>();
            return result.ToArray();
        }

        /// <summary>
        /// Resolves the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectoryItem ResolveDirectory(string path)
        {
            DirectoryItem[] directories = DisplayMode == FileBrowserDisplayMode.List ? new DirectoryItem[0] : GetChildDirectories(path);
            return new DirectoryItem(GetName(path),
                                     EndWithSlash(GetDirectoryPath(path)),
                                     string.Empty,
                                     string.Empty,
                                     fullPermissions,
                                     GetChildFiles(path),
                                     directories);
        }

        public override string GetFileName(string url)
        {
            return GetName(url);
        }

        public override string GetPath(string url)
        {
            return GetDirectoryPath(ExtractPath(RemoveProtocolNameAndServerName(url)));
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public override Stream GetFile(string url)
        {

            //byte[] content = DataServer.GetContent(ExtractPath(RemoveProtocolNameAndServerName(url)));
            //if (!Object.Equals(content, null))
            //{
            //    return new MemoryStream(content);
            //}
            return null;
        }

        public override string StoreBitmap(Bitmap bitmap, string url, ImageFormat format)
        {
            //string newItemPath = ExtractPath(RemoveProtocolNameAndServerName(url));
            //string name = GetName(newItemPath);
            //string _path = GetPath(newItemPath);
            //string tempFilePath = System.IO.Path.GetTempFileName();
            //bitmap.Save(tempFilePath);
            //byte[] content;
            //using (FileStream inputStream = File.OpenRead(tempFilePath))
            //{
            //    long size = inputStream.Length;
            //    content = new byte[size];
            //    inputStream.Read(content, 0, (int)size);
            //}

            //if (File.Exists(tempFilePath))
            //{
            //    File.Delete(tempFilePath);
            //}

            //DataServer.CreateItem(name, _path, "image/gif", false, content.Length, content);
            return string.Empty;
        }


        /// <summary>
        /// Stores the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public override string StoreFile(Telerik.Web.UI.UploadedFile file, string path, string name, params string[] arguments)
        {
            //int fileLength = (int)file.InputStream.Length;
            //byte[] content = new byte[fileLength];
            //file.InputStream.Read(content, 0, fileLength);
            //string fullPath = CombinePath(path, name);
            //if (!Object.Equals(DataServer.GetItemRow(fullPath), null))
            //{
            //    DataServer.ReplaceItemContent(fullPath, content);
            //}
            //else
            //{
            //    DataServer.CreateItem(name, path, file.ContentType, false, fileLength, content);
            //}
            int fileLength = (int)file.InputStream.Length;
            byte[] content = new byte[fileLength];
            file.InputStream.Read(content, 0, fileLength);

            switch (path.TrimEnd(slashArray))
            {
                case "/User":

                    UserFile userFile = new UserFile();
                    userFile.FileName = name;
                    userFile.Size = fileLength;
                    userFile.UserID = KbContext.CurrentUserId;

                    KbContext.CurrentKb.ManagerUserFile.Save(userFile);

                    KbContext.CurrentKb.ManagerUserFile.SetData(userFile.UserFileID, content);

                    break;

                case "/Knowledge":
                    if(!knowledgeID.HasValue)
                        return "Knowlede not saved";

                    FileInclude fileInclude = new FileInclude();
                    fileInclude.FileName = name;
                    fileInclude.Size = fileLength;
                    fileInclude.KnowledgeID = knowledgeID.Value;

                    var manager = KbContext.CurrentKb.ManagerFileInclude;
                    manager.Save(fileInclude);
                    manager.SetData(fileInclude.FileIncludeID, content);

                    break;

            }
            return string.Empty;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string DeleteFile(string path)
        {
            if (path.StartsWith("/User/"))
            {
                KbContext.CurrentKb.ManagerUserFile.DeleteByUserIDByFileName(KbContext.CurrentUserId, Path.GetFileName(path));
            }
            else if (path.StartsWith("/Knowledge/"))
            {
                KbContext.CurrentKb.ManagerFileInclude.DeleteByKnowledgeIDByFileName(knowledgeID.Value, Path.GetFileName(path));
            }
            return string.Empty;
        }

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string DeleteDirectory(string path)
        {
            //DataServer.DeleteItem(path);
            return string.Empty;
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override string CreateDirectory(string path, string name)
        {
            //DataServer.CreateItem(name, path, string.Empty, true, 0, new byte[0]);
            return string.Empty;
        }

        public override bool CanCreateDirectory
        {
            get
            {
                return false;
            }
        }

        #region Helpers

        private DirectoryItem[] GetChildDirectories(string path)
        {
            //DirectoryItem[] directories;
            //if (IsChildOf(path, ExtractPath(SelectedUrl)))
            //{
            //    DataRow[] childRows = DataServer.GetChildDirectoryRows(path);
            //    directories = new DirectoryItem[childRows.Length];
            //    int i = 0;
            //    while (i < childRows.Length)
            //    {
            //        DataRow childRow = childRows[i];
            //        string name = childRow["Name"].ToString();
            //        string itemFullPath = EndWithSlash(CombinePath(path, name));
            //        directories[i] =
            //            new DirectoryItem(name, string.Empty, itemFullPath, itemFullPath, fullPermissions, GetChildFiles(itemFullPath),
            //                              GetChildDirectories(itemFullPath));
            //        i = i + 1;
            //    }
            //    return directories;
            //}
            if (path == "/")
            {
                DirectoryItem[] directories = new DirectoryItem[2];
                directories[0] = new DirectoryItem("User", string.Empty, "/User", string.Empty, fullPermissions,
                                                   GetChildFiles("/User"), new DirectoryItem[0]);
                directories[1] = new DirectoryItem("Knowledge", string.Empty, "/Knowledge", string.Empty, fullPermissions,
                                                   GetChildFiles("/Knowledge"), new DirectoryItem[0]);
                return directories;
            }
            return new DirectoryItem[] { };
        }

        private FileItem[] GetChildFiles(string path)
        {
            if (IsChildOf(path, ExtractPath(SelectedUrl)))
            {
                //    DataRow[] childRows = DataServer.GetChildFileRows(_path);
                //    ArrayList files = new ArrayList();

                //    for (int i = 0; i < childRows.Length; i++)
                //    {
                //        DataRow childRow = childRows[i];
                //        string name = childRow["Name"].ToString();
                //        if (IsExtensionAllowed(System.IO.Path.GetExtension(name)))
                //        {
                //            string itemFullPath = CombinePath(_path, name);
                //            files.Add(
                //                new FileItem(name, Path.GetExtension(name), (int)childRow["Size"], string.Empty, GetItemUrl(itemFullPath),
                //                             itemFullPath, fullPermissions));
                //        }
                //    }
                //    return (FileItem[])files.ToArray(typeof(FileItem));

                var result = new List<FileItem>();
                switch (path.TrimEnd(slashArray))
                {
                    case "/User":
                        FillFileCollection(
                            KbContext.CurrentKb.ManagerUserFile.GetByUserID(KbContext.CurrentUserId),
                            "/User/",
                            result);
                        break;

                    case "/Knowledge":
                        if (knowledgeID.HasValue)
                            FillFileCollection(
                                KbContext.CurrentKb.ManagerFileInclude.GetByKnowledgeID(knowledgeID.Value),
                                "/Knowledge/",
                                result);
                        break;
                }
                return result.ToArray();
            }
            return new FileItem[] { };
        }
        void FillFileCollection(IEnumerable files, string path, List<FileItem> result)
        {
            foreach (IFile file in files)
            {

                if (!IsExtensionAllowed(Path.GetExtension(file.FileName)))
                    continue;
                result.Add(
                    new FileItem(
                        file.FileName,
                        Path.GetExtension(file.FileName),
                        file.Size,
                        string.Empty,
                        GetItemUrl(file.QueryString),
                        path + file.FileName,
                        fullPermissions));
            }

        }


        private string GetItemUrl(string virtualItemPath)
        {
            return string.Format("{0}?{1}", ItemHandlerPath, virtualItemPath);
        }

        private string ExtractPath(string itemUrl)
        {
            if (itemUrl == null)
            {
                return string.Empty;
            }
            if (itemUrl.StartsWith(_itemHandlerPath))
            {
                return itemUrl.Substring(GetItemUrl(string.Empty).Length);
            }
            return itemUrl;
        }

        private string GetName(string path)
        {
            if (path == null)
            {
                return string.Empty;
            }
            return path.Substring(path.LastIndexOf('/') + 1);
        }


        private string GetDirectoryPath(string path)
        {
            return path.Substring(0, path.LastIndexOf('/') + 1);
        }

        private bool IsChildOf(string parentPath, string childPath)
        {
            return childPath.StartsWith(parentPath);
        }

        private string EndWithSlash(string path)
        {
            if (!path.EndsWith("/"))
            {
                return path + "/";
            }
            return path;
        }

        private string CombinePath(string path1, string path2)
        {
            if (path1.EndsWith("/"))
            {
                return string.Format("{0}{1}", path1, path2);
            }
            if (path1.EndsWith("\\"))
            {
                path1 = path1.Substring(0, path1.Length - 1);
            }
            return string.Format("{0}/{1}", path1, path2);
        }



        private bool IsExtensionAllowed(string extension)
        {
            return Array.IndexOf(SearchPatterns, "*.*") >= 0 || Array.IndexOf(SearchPatterns, "*" + extension.ToLower()) >= 0;
        }
        private string RemoveLastSlash(string path)
        {
            if (path.EndsWith("/"))
            {
                return path.Substring(0, path.Length - 1);
            }
            return path;
        }
        #endregion
    }
}