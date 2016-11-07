using System;
using System.Text;
using System.Text.RegularExpressions;
using Savchin.Web.HtmlProcessing.Core;

namespace Savchin.Web.HtmlProcessing
{
    /// <summary>
    /// Represents external file
    /// </summary>
    public class HtmlSerializer : IDisposable
    {
        #region Fields

        private readonly HtmlParser htmlParser = new HtmlParser();

        private Uri _baseFolderUrl = null;

        #endregion

        #region Events

        public event EventHandler<ResolveUrlEventArgs> ResolveUrl = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the HTML file.
        /// </summary>
        /// <value>The HTML file.</value>
        public IWebFile HtmlFile { get; set; }

        /// <summary>
        /// Gets or sets the file provider.
        /// </summary>
        /// <value>The file provider.</value>
        public IWebFileProvider FileProvider { get; set; }

        /// <summary>
        /// Gets or sets the file storage.
        /// </summary>
        /// <value>The file storage.</value>
        public IFileStorage FileStorage { get; set; }

        /// <summary>
        /// Gets or sets the HTML file URI.
        /// </summary>
        /// <value>The HTML file URI.</value>
        public Uri HtmlFileUri { get; set; }

        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlSerializer"/> class.
        /// </summary>
        public HtmlSerializer()
        {
            HtmlFile = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlSerializer"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="html">The HTML.</param>
        /// <param name="encoding">The encoding.</param>
        public HtmlSerializer(Uri uri, string html, Encoding encoding)
        {
            HtmlFile = new LoadedHtml(uri, html, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlSerializer"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public HtmlSerializer(Uri uri)
        {
            HtmlFile = null;
            this.HtmlFileUri = uri;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Processes the dependencies.
        /// </summary>
        public void SaveContent()
        {
            var newFile = new GenericWebFile(ProcessContent(), HtmlFile);
            SaveFile(newFile, true);
        }

        /// <summary>
        /// Processes the content.
        /// </summary>
        /// <returns>processed conntent</returns>
        public string ProcessContent()
        {
            if (HtmlFile == null) HtmlFile = FileProvider.GetFile(HtmlFileUri);
            _baseFolderUrl = new Uri(htmlParser.GetBaseUrlFolder(HtmlFile));

            return ResolveDependencies();
        }

        #endregion

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="isRootFile">if set to <c>true</c> [is root file].</param>
        protected virtual void SaveFile(IWebFile file, bool isRootFile)
        {
            FileStorage.SaveFile(file, isRootFile);
        }

        #region Html processing methods

        /// <summary>
        /// Converts the URL to relative.
        /// </summary>
        private string ResolveDependencies()
        {
            return htmlParser.RegExpTag.Replace(HtmlFile.Text, TagMatchEvaluator);
        }

        /// <summary>
        /// Tags the match evaluator.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>Replacement string</returns>
        private string TagMatchEvaluator(Match match)
        {
            var tag = htmlParser.BuildTag(match);
            ProcessTag(tag);
            return tag.ToString();
        }



        #endregion

        #region Virtual methods

        /// <summary>
        /// Processes the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        protected virtual void ProcessTag(TagInfo tag)
        {
            if (!tag.HasFileReference)
                return;

            var args = new ResolveUrlEventArgs
                                           {
                                               TagInfo = tag,
                                               OldUrl = tag.FileRerefenceAttribute.FileReferenceUrl
                                           };

            args.NewUrl = ResolveUrlInternal(args.OldUrl);

            OnResolveUrl(args);

            tag.FileRerefenceAttribute.FileReferenceUrl = args.NewUrl;
        }

        /// <summary>
        /// Raises the <see cref="E:ResolveUrl"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
        protected virtual void OnResolveUrl(ResolveUrlEventArgs e)
        {
            if (ResolveUrl != null)
                ResolveUrl(this, e);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Resolves the URL internal.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>full url</returns>
        private string ResolveUrlInternal(string url)
        {
            if (IsFullUrl(url))
                return url;
            if (url == "/")
                return _baseFolderUrl.GetLeftPart(UriPartial.Authority);
            if (url.StartsWith("#"))
                return url;
            if (url.StartsWith("/"))
                return _baseFolderUrl.GetLeftPart(UriPartial.Authority) + url;
            return _baseFolderUrl + url;
        }

        /// <summary>
        /// Determines whether [is full URL] [the specified URL].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// 	<c>true</c> if [is full URL] [the specified URL]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsFullUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;
            try
            {
                new Uri(url);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (FileStorage != null)
            {
                FileStorage.Dispose();
                FileStorage = null;
            }
            if (FileProvider != null)
            {
                FileProvider.Dispose();
                FileProvider = null;
            }
        }


    }
}