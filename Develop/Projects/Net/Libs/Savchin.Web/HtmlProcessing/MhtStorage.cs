using System.IO;
using Savchin.Web.HtmlProcessing.Core;

namespace Savchin.Web.HtmlProcessing
{
    public class MhtStorage : IFileStorage
    {
        #region Fields

        private MhtBuilder _builder = new MhtBuilder();

        #endregion

        #region Properties

        private string _filePath = string.Empty;

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MhtStorage"/> class.
        /// </summary>
        public MhtStorage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MhtStorage"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public MhtStorage(string filePath) :
            this()
        {
            _filePath = filePath;
        }

        #endregion

        #region IFileStorage Members

        /// <summary>
        /// Saves specified file to storage
        /// </summary>
        /// <param name="file">File to save</param>
        /// <param name="isRootFile">Specifies if this is root file (requested html page)</param>
        /// <returns>New location of file</returns>
        public string SaveFile(IWebFile file, bool isRootFile)
        {
            var fileUrl = file.Url.ToString();
            if (file.ContentType.Contains("text"))
            {
                if (IsValidTextData(file.Text))
                {
                    if (isRootFile)
                        _builder.PrependTextData(file.Text, file.ContentType, fileUrl, file.TextEncoding);
                    else
                        _builder.AppendTextData(file.Text, file.ContentType, fileUrl, file.TextEncoding);
                }
            }
            else
            {
                _builder.AppendBinaryData(file.Bytes, fileUrl, file.ContentType);
            }
            return fileUrl;
        }

        public void Flush()
        {
            using (TextWriter output = new StreamWriter(FilePath))
            {
                output.Write(_builder.GetDocumentText());
            }
        }

        #endregion

        private bool IsValidTextData(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\0')
                    return false;
            }
            return true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Flush();
        }

        #endregion
    }
}