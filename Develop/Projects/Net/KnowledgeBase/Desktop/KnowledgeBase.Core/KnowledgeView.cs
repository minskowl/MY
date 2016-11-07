using System;
using System.IO;
using System.Text;
using System.Web;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.IO;
using Savchin.Web.HtmlProcessing;
using Savchin.Web.HtmlProcessing.Core;

namespace KnowledgeBase.Core
{
    /// <summary>
    /// KnowledgeView
    /// </summary>
    public class KnowledgeView
    {
       // private static SyntaxHighlighter _highlighter = new SyntaxHighlighter();
        private readonly Knowledge _entity;
        private bool _filesPrepared;
        private readonly string _contentFilePath;
        private readonly string _contentServerFilePath;
        #region Properties
        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary
        {
            get { return PrepareForEdit(_entity.Summary); }
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return _entity.Title; }
        }
        /// <summary>
        /// Gets the knowledge id.
        /// </summary>
        /// <value>The knowledge id.</value>
        public int KnowledgeId
        {
            get { return _entity.KnowledgeID; }
        }

        private readonly string _filesDir;
        /// <summary>
        /// Gets the files dir.
        /// </summary>
        /// <value>The files dir.</value>
        public string FilesDir
        {
            get { return _filesDir; }
        }



        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeView"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public KnowledgeView(Knowledge entity)
        {
            _entity = entity;

            var contentDir = Path.Combine(AppCore.Settings.ContentPath, "article" + entity.KnowledgeID + "\\");

            _filesDir = Path.Combine(contentDir, "files\\");
            _contentFilePath = Path.Combine(contentDir, "index.html");
            _contentServerFilePath = Path.Combine(contentDir, "indexServer.html");
            DirectoryHelper.CreateIfNotExists(FilesDir);

        }


        /// <summary>
        /// Gets the content local path.
        /// </summary>
        /// <returns></returns>
        public string GetContentLocalPath()
        {
            return PrepareForView(_contentFilePath, false) ? _contentFilePath : null;
        }
        /// <summary>
        /// Gets the content server path.
        /// </summary>
        /// <returns></returns>
        public string GetContentServerPath()
        {
            return PrepareForView(_contentServerFilePath, true) ? _contentServerFilePath : null;
        }
        /// <summary>
        /// Gets the knowledge HTML.
        /// </summary>
        /// <returns></returns>
        public string GetKnowledgeHtml()
        {
            var builder = new StringBuilder();


            builder.AppendFormat(@"<html xmlns='http://www.w3.org/1999/xhtml'>
<head ><title>{0}</title>
    <meta content='text/html; charset=utf-8' http-equiv='Content-Type' />
{2}
</head>
<body><h1>#{1} {0}</h1>", HttpUtility.HtmlEncode(_entity.Title), _entity.KnowledgeID, string.Empty);//TODO: Uncomment _highlighter.GetHeaderContent()

            var link = string.Format("http://{0}:{2}/{1}", Environment.MachineName, _entity.PublicIdString, AppCore.Settings.DocServerPort);
            builder.AppendFormat("<a href='{0}' target='_blank'>{1}</a><br/>", link, HttpUtility.HtmlEncode(link));

            builder.AppendFormat("Category: {2} Type: {1} Created: {0} ", _entity.CreationDate, _entity.KnowledgeType, _entity.Category.Name);
            builder.AppendFormat("<br/><br/>{0}<br/>", _entity.Summary);

            if (_entity.KewordsAssociations.Count > 0)
            {
                var keywords = KbContext.CurrentKb.ManagerKeyword.GetByListID(_entity.KewordsAssociations);
                builder.Append("<h2>Keywords</h2>");
                foreach (var keyword in keywords)
                {
                    builder.Append(keyword.Name + ", ");
                }
                builder.Length = builder.Length - 2;
            }

            builder.AppendLine("</body></html>");
            return builder.ToString();
        }

        #region Processing
        private string PrepareForEdit(string html)
        {
            PrepareFiles();
            using (var serial = CreateProcessor(html, false))
            {
                return serial.ProcessContent();
            }
        }

        /// <summary>
        /// Processes the HTML.
        /// </summary>
        /// <param name="destinationFile">The destination file.</param>
        /// <param name="serverView">if set to <c>true</c> [server view].</param>
        /// <returns></returns>
        private bool PrepareForView(string destinationFile, bool serverView)
        {
            PrepareFiles();

            try
            {
                var html = GetKnowledgeHtml();
                using (var serial = CreateProcessor(html, serverView))
                {
                    serial.FileStorage = new FileSystemStorage(destinationFile, FilesDir);
                    serial.FileProvider = new FileProvider();
                    serial.SaveContent();
                    return true;
                }
            }
            catch (Exception ex)
            {
                AppCore.Log.Error(ex);
                return false;
            }

        }
        private HtmlProcessor CreateProcessor(string html, bool serverView)
        {
            return new HtmlProcessor(html, FilesDir, AppCore.Workspace.UserFiles.FilesDir, serverView);
        }



        #endregion

        private void PrepareFiles()
        {
            if (_filesPrepared) return;

            var artFiles = KbContext.CurrentKb.ManagerFileInclude.GetByKnowledgeID(_entity.KnowledgeID);

            foreach (var file in artFiles)
            {
                File.WriteAllBytes(Path.Combine(FilesDir, file.FileName),
                                   KbContext.CurrentKb.ManagerFileInclude.GetData(file.FileIncludeID));

            }



        }

        private class HtmlProcessor : HtmlSerializer
        {
            private readonly string _userFilesPath;
            private readonly string _articleFilesPath;
            private readonly bool _serverView;
            /// <summary>
            /// Initializes a new instance of the <see cref="HtmlProcessor"/> class.
            /// </summary>
            /// <param name="html">The HTML.</param>
            /// <param name="articleFilesPath">The article files path.</param>
            /// <param name="userFilesPath">The user files path.</param>
            /// <param name="serverView">if set to <c>true</c> [server view].</param>
            public HtmlProcessor(string html, string articleFilesPath, string userFilesPath, bool serverView)
                : base(new Uri("http://localhost/test.html"), html, Encoding.UTF8)
            {
                _articleFilesPath = articleFilesPath;
                _userFilesPath = userFilesPath;
                _serverView = serverView;
            }

            /// <summary>
            /// Raises the <see cref="E:ResolveUrl"/> event.
            /// </summary>
            /// <param name="e">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
            protected override void OnResolveUrl(ResolveUrlEventArgs e)
            {

                if (e.OldUrl.StartsWith("/KnowledgeBase/Image.ashx"))
                {
                    ResolveImagePath(e);
                    return;
                }
                //TODO: Uncomment
                //if (e.OldUrl.StartsWith(HtmlEditor.Folder + "\\"))
                //{
                //    ResolveEditorPath(e);
                //    return;
                //}
                if (e.OldUrl.StartsWith(AppCore.Settings.ContentFolder + "\\"))
                {
                    e.NewUrl = _serverView ? e.OldUrl :
                        string.Format("file:///{0}{1}", AppCore.Settings.ContentPath, e.OldUrl.Substring(AppCore.Settings.ContentFolder.Length));
                    return;
                }

            }
            //TODO: Uncomment
            //private void ResolveEditorPath(ResolveUrlEventArgs e)
            //{
            //    e.NewUrl = _serverView ? e.OldUrl :
            //        "file:///" + Path.GetFullPath(HtmlEditor.EditorPath) + e.OldUrl.Substring(HtmlEditor.Folder.Length);
            //}

            private void ResolveImagePath(ResolveUrlEventArgs e)
            {
                var query = new Uri(e.NewUrl).Query;

                if (string.IsNullOrEmpty(query)) return;
                var parts = query.Split(new char[1] { '=' });
                if (parts.Length < 2) return;


                if (query.StartsWith("?ID="))
                {
                    Guid id;
                    if (Guid.TryParse(parts[1], out id)) ResolveArticleFile(e, id);
                }
                else if (query.StartsWith("?UserFileID="))
                {
                    int id;
                    if (int.TryParse(parts[1], out id)) ResolveUserFile(e, id);
                }
            }

            private void ResolveUserFile(ResolveUrlEventArgs e, int id)
            {
                var file = KbContext.CurrentKb.ManagerUserFile.GetByID(id);
                if (file != null)
                {
                    e.NewUrl = "file:///" + Path.Combine(_userFilesPath, file.FileName);
                }
            }

            private void ResolveArticleFile(ResolveUrlEventArgs e, Guid id)
            {
                var file = KbContext.CurrentKb.ManagerFileInclude.GetByID(id);
                if (file != null)
                {
                    e.NewUrl = "file:///" + Path.Combine(_articleFilesPath, file.FileName);
                }
            }
        }




    }
}
