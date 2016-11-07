

using System;
using System.Text;
using System.Reflection;

namespace Savchin.Web.HtmlProcessing.Core
{
    public class MhtBuilder
    {
        #region Constants

        private const string MimeBoundaryTag = "----=_NextPart_000_00";

        #endregion

        #region Fields

        private StringBuilder _mhtBuilder = new StringBuilder();

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the document text.
        /// </summary>
        /// <returns>Generated content</returns>
        public string GetDocumentText()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(GetHeader("File data"));
            builder.AppendLine(_mhtBuilder.ToString());
            builder.AppendLine("--" + MimeBoundaryTag);

            return builder.ToString();
        }

        /// <summary>
        /// Prepends the text data.
        /// </summary>
        /// <param name="fileText">The file text.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="url">The URL.</param>
        /// <param name="textEncoding">The text encoding.</param>
        public void PrependTextData(string fileText, string contentType, string url, Encoding textEncoding)
        {
            _mhtBuilder.Insert(0, QuotedPrintableEncode(fileText, textEncoding));
            _mhtBuilder.Insert(0, GetFileHeader(contentType, "quoted-printable", url, textEncoding));
        }

        /// <summary>
        /// Appends the text data.
        /// </summary>
        /// <param name="fileText">The file text.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="url">The URL.</param>
        /// <param name="textEncoding">The text encoding.</param>
        public void AppendTextData(string fileText, string contentType, string url, Encoding textEncoding)
        {
            _mhtBuilder.Append(GetFileHeader(contentType, "quoted-printable", url, textEncoding));
            _mhtBuilder.AppendLine(QuotedPrintableEncode(fileText, textEncoding));
        }

        /// <summary>
        /// Appends the MHT binary file from memory storage.
        /// </summary>
        /// <param name="webFile">The web file.</param>
        /// <param name="ChunkSize">Size of the chunk.</param>
        public void AppendBinaryData(byte[] fileBytes, string url, string contentType)
        {
            const int chunkSize = 57;
            _mhtBuilder.Append(GetFileHeader(contentType, "base64", url));

            int len = fileBytes.Length;
            if (len <= chunkSize)
            {
                AppendBytes(fileBytes);
                return;
            }

            for (int bufferPosition = 0; bufferPosition < len; bufferPosition += chunkSize)
            {
                AppendBytes(
                    fileBytes,
                    bufferPosition,
                    bufferPosition + chunkSize < len ? chunkSize : len - bufferPosition);
            }
        }

        #endregion

        #region Append file header

        /// <summary>
        /// appends the Mht header, which includes the root HTML
        /// </summary>
        private string GetHeader(string subject)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("From: <Saved by " + Environment.UserName + " on " + Environment.MachineName + ">");
            builder.AppendLine("Subject: " + subject);
            builder.AppendLine("Date: " + DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz"));
            builder.AppendLine("MIME-Version: 1.0");
            builder.AppendLine("Content-Type: multipart/related;");
            builder.AppendLine(Convert.ToChar(9) + "type=\"text/html\";");
            builder.AppendLine(Convert.ToChar(9) + "boundary=\"" + MimeBoundaryTag + "\"");
            builder.AppendLine("X-MimeOLE: Produced by " + this.GetType().ToString() + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            builder.AppendLine();
            builder.AppendLine("This is a multi-part message in MIME format.");

            return builder.ToString();
        }

        /// <summary>
        /// Appends the MHT file header.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="url">The URL.</param>
        private string GetFileHeader(string contentType, string contentEncoding, string url)
        {
            return GetFileHeader(contentType, contentEncoding, url, null);
        }

        /// <summary>
        /// Appends the MHT file header.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="url">The URL.</param>
        /// <param name="textEncoding">The text encoding.</param>
        private string GetFileHeader(string contentType, string contentEncoding, string url, Encoding textEncoding)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine();
            builder.AppendLine("--" + MimeBoundaryTag);
            builder.AppendLine("Content-Type: " + contentType + ";");
            if (textEncoding != null)
                builder.AppendLine(Convert.ToChar(9) + "charset=\"" + textEncoding.WebName + "\"");
            builder.AppendLine("Content-Transfer-Encoding: " + contentEncoding);
            builder.AppendLine("Content-Location: " + url);
            builder.AppendLine();

            return builder.ToString();
        }

        #endregion

        #region Append MHT bytes

        /// <summary>
        /// Appends the MHT bytes.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        private void AppendBytes(byte[] buffer)
        {
            AppendBytes(buffer, buffer.Length);
        }

        /// <summary>
        /// Appends the MHT bytes.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="length">The length.</param>
        private void AppendBytes(byte[] buffer, int length)
        {
            AppendBytes(buffer, 0, length);
        }

        /// <summary>
        /// Appends the MHT bytes.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offeset">The offeset.</param>
        /// <param name="length">The length.</param>
        private void AppendBytes(byte[] buffer, int offeset, int length)
        {
            _mhtBuilder.AppendLine(Convert.ToBase64String(buffer, offeset, length));
        }

        #endregion

        #region Quoted-Printable encoding

        /// <summary>
        /// converts a string into Quoted-Printable encoding
        ///   http://www.freesoft.org/CIE/RFC/1521/6.htm
        /// </summary>
        private static string QuotedPrintableEncode(string stringToEncode, Encoding encoding)
        {
            int ascii = 0;
            int lastSpace = 0;
            int lineLength = 0;
            int lineBreaks = 0;
            StringBuilder builder = new StringBuilder();

            if (string.IsNullOrEmpty(stringToEncode))
                return string.Empty;

            foreach (char charToEncode in stringToEncode)
            {

                ascii = Convert.ToInt32(charToEncode);

                if (ascii == 61 | ascii > 126)
                {
                    if (ascii <= 255)
                    {
                        builder.Append("=");
                        builder.Append(Convert.ToString(ascii, 16).ToUpper());
                        lineLength += 3;
                    }
                    else
                    {
                        //-- double-byte land..
                        foreach (byte stringByte in encoding.GetBytes(new char[] { charToEncode }))
                        {
                            builder.Append("=");
                            builder.Append(Convert.ToString(stringByte, 16).ToUpper());
                            lineLength += 3;
                        }
                    }
                }
                else
                {
                    builder.Append(charToEncode);
                    lineLength += 1;
                    if (ascii == 32) lastSpace = builder.Length;
                }

                if (lineLength >= 73)
                {
                    if (lastSpace == 0)
                    {
                        builder.Insert(builder.Length, "=" + Environment.NewLine);
                        lineLength = 0;
                    }
                    else
                    {
                        builder.Insert(lastSpace, "=" + Environment.NewLine);
                        lineLength = builder.Length - lastSpace - 1;
                    }
                    lineBreaks += 1;
                    lastSpace = 0;
                }

            }

            //-- if we split the line, have to indicate trailing spaces
            if (lineBreaks > 0)
            {
                if (builder[builder.Length - 1] == ' ')
                {
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append("=20");
                }
            }

            return builder.ToString();
        }

        #endregion
    }
}