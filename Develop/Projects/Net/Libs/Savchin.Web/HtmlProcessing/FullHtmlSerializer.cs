using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Web.HtmlProcessing.Core;

namespace Savchin.Web.HtmlProcessing
{
    /// <summary>
    /// Full page serializer to use
    /// </summary>
    public class FullHtmlSerializer : HtmlSerializer
    {
        #region Fields

        private readonly Dictionary<string, string> _uploadedFiles = new Dictionary<string, string>();

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlSerializer"/> class.
        /// </summary>
        public FullHtmlSerializer()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlSerializer"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="html">The HTML.</param>
        /// <param name="encoding">The encoding.</param>
        public FullHtmlSerializer(Uri uri, string html, Encoding encoding)
            : base(uri, html, encoding)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlSerializer"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public FullHtmlSerializer(Uri uri)
            : base(uri)
        {
        } 
        #endregion
        #region Overriden methods

        /// <summary>
        /// Raises the <see cref="E:ResolveUrl"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
        protected override void OnResolveUrl(ResolveUrlEventArgs e)
        {
            base.OnResolveUrl(e);

            if (_uploadedFiles.ContainsKey(e.NewUrl))
            {
                e.NewUrl = _uploadedFiles[e.NewUrl];
                return;
            }

            if (!FileDownloadRequired(e.TagInfo))
                return;

            _uploadedFiles.Add(e.NewUrl, DownloadDependencyFile(e.NewUrl));
            e.NewUrl = _uploadedFiles[e.NewUrl];
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Files the download required.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>True is file download is required</returns>
        private bool FileDownloadRequired(TagInfo tag)
        {
            if (tag.TagName == "img")
                return true;
            if (tag.TagName == "script")
                return true;
            if (tag.TagName == "embed")
                return true;
            if (tag.TagName == "link")
            {
                if (tag["rel"] == null)
                    return false;
                return tag["rel"].Value.ToLower() != "alternate";
            }
            return false;
        }

        /// <summary>
        /// Gets url of file to download
        /// </summary>
        /// <param name="url">File url</param>
        private string DownloadDependencyFile(string url)
        {
            IWebFile downloadedFile = FileProvider.GetFile(new Uri(url));
            if (downloadedFile == null)
                return url;
            return FileStorage.SaveFile(downloadedFile, false);
        }

        #endregion
    }
}