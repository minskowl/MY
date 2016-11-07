
using System;

namespace Savchin.Web.HtmlProcessing
{
    /// <summary>
    /// Provides web requeded web file
    /// </summary>
    public interface IWebFileProvider : IDisposable 
    {
        /// <summary>
        /// Gets the requested file
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns>File instance</returns>
        IWebFile GetFile(Uri fileUrl);
    }
}