using System.IO;
using System.Web;
using Savchin.IO;
using Savchin.Text;



namespace Savchin.Web
{
    /// <summary>
    /// ResponseTransfer
    /// </summary>
    public static class ResponseTransfer
    {
        /// <summary>
        /// SendMode
        /// </summary>
        public enum SendMode
        {
            /// <summary>
            /// Inline
            /// </summary>
            Inline,
            /// <summary>
            /// Attachment
            /// </summary>
            Attachment
        }

        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static void SendFile(this HttpContext context, SendMode mode, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            PrepareResponse(context,
                            mode,
                            Path.GetFileName(filePath),
                            new FileInfo(filePath).Length);
            context.Response.TransmitFile(filePath);
            ResponseEnd(context.Response);
        }

        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns></returns>
        public static void SendFile(this HttpContext context, SendMode mode, string fileName, byte[] fileContent)
        {
            PrepareResponse(context,
                            mode,
                            fileName,
                            fileContent.Length);
           context.Response.OutputStream.Write(fileContent, 0, fileContent.Length);
           ResponseEnd(context.Response);
        }
        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static void SendFile(this HttpContext context, SendMode mode, string fileName, Stream content)
        {
            PrepareResponse(context, mode, fileName, content.Length);
            StreamPipe.Transfer(content, context.Response.OutputStream);
            ResponseEnd(context.Response);
        }

        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        public static void SendFile(this HttpContext context, SendMode mode, string fileName, Stream content, string mimeType)
        {
            PrepareResponse(context, mode, fileName, content.Length);
            StreamPipe.Transfer(content, context.Response.OutputStream);
            ResponseEnd(context.Response);
        }
        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static void SendFile(this HttpContext context, SendMode mode, string fileName, string content)
        {
            using (var stream = StringUtil.GetStream(content))
                SendFile(context, mode, fileName, stream);
        }
        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static void SendFile(this HttpContext context, SendMode mode, string fileName, string mimeType, string content)
        {
            using (var stream = StringUtil.GetStream(content))
                SendFile(context, mode, fileName, stream, mimeType);
        }


        #region Helpers

        /// <summary>
        /// Prepares the response.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="lengthFile">The length file.</param>
        public static void PrepareResponse(this HttpContext context, SendMode mode, string fileName, long lengthFile)
        {
            PrepareResponse(
                context,
                mode,
                fileName,
                Mime.GetMimeType(fileName),
                lengthFile);

        }
        private static void PrepareResponse(HttpContext context, SendMode mode, string fileName, string mimeType, long lengthFile)
        {
            var response = context.Response;
            response.Clear();
            response.Buffer = false;
            response.CacheControl = "no-cache";
            response.ContentType = mimeType;

            var preparedFileName = fileName;
            if (context.Request.GetBrowserType() == BrowserType.IE)
            {
                preparedFileName = HttpUtility.UrlEncode(preparedFileName);
                if (preparedFileName != null) preparedFileName = preparedFileName.Replace(@"+", @"%20");
            }
            var contentDisposition = string.Format("{0}; filename=\"{1}\";",
                (mode == SendMode.Attachment) ? "attachment" : "inline",
                preparedFileName
                );
            response.AddHeader("Content-Disposition", contentDisposition);

            response.AddHeader("Content-length", lengthFile.ToString());

        }
        /// <summary>
        /// Responses the end.
        /// </summary>
        /// <param name="response">The response.</param>
        public static void ResponseEnd(this HttpResponse response)
        {
            response.Flush();
            response.End();
            response.Close();
        } 
        #endregion
    }
}
