
using System;

namespace Savchin.Web.HtmlProcessing
{
    /// <summary>
    /// Provides methods for files storage
    /// </summary>
    public interface IFileStorage : IDisposable
    {
        /// <summary>
        /// Saves specified file to storage
        /// </summary>
        /// <param name="file">File to save</param>
        /// <param name="isRootFile">Specifies if this is root file (requested html page)</param>
        /// <returns>New location of file</returns>
        string SaveFile(IWebFile file, bool isRootFile);
    }
}