using System;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.IO;
using Savchin.Web.HtmlProcessing;
using Savchin.Web.HtmlProcessing.Core;

namespace KnowledgeBase.Core
{
    /// <summary>
    /// FileProvider
    /// </summary>
    public class FileProvider : WebFileProvider
    {
        /// <summary>
        /// Downloads the HTTP file.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns></returns>
        protected override IWebFile DownloadHttpFile(Uri fileUrl)
        {
            if (fileUrl.Host == "localhost" && fileUrl.LocalPath.StartsWith("/KnowledgeBase/"))
            {
                return ProvideFile(fileUrl);
            }
            return base.DownloadHttpFile(fileUrl);
        }

        protected IWebFile ProvideFile(Uri fileUrl)
        {
            if (fileUrl.LocalPath == "/KnowledgeBase/Image.ashx")
            {
                var q = fileUrl.Query.Split(new char[] { '=' });
                if (q.Length < 2) return null;
                var fileID = Guid.Parse(q[1]);
                var file = KbContext.CurrentKb.ManagerFileInclude.GetByID(fileID);
                var data = KbContext.CurrentKb.ManagerFileInclude.GetData(fileID);

                return new LoadedData(fileUrl, Mime.GetMimeType(file.FileName), data);
            }
            else
            {
                AppCore.Log.Warn("Intresting URL " + fileUrl);
                return null;
            }
        }
    }
}
