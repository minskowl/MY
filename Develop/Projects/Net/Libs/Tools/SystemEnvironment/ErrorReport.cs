using System;
using System.IO;
using Savchin.Core;

namespace Savchin.SystemEnvironment
{
    /// <summary>
    /// Error Report class stores all information about Error, state of application and computer environment. Use for sending report to developers about unhadled exception
    /// </summary>
    [Serializable]
    public class ErrorReport
    {

        #region Properties

        /// <summary>
        /// Gets or sets the title of report.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the exeption.
        /// </summary>
        /// <value>The exeption.</value>
        public string Exeption { get; set; }

        /// <summary>
        /// Gets or sets the versrion.
        /// </summary>
        /// <value>The versrion.</value>
        public Version Versrion { get; set; }



        /// <summary>
        /// Gets or sets the enviroment.
        /// </summary>
        /// <value>The enviroment.</value>
        public EnviromentInfo Enviroment{get; set; }



        #endregion

        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(string fileName)
        {
            TypeSerializer<ErrorReport>.ToXmlFile(fileName, this);
        }
        /// <summary>
        /// Saves the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Save(Stream stream)
        {
            TypeSerializer<ErrorReport>.ToXml(stream, this);
        }
        /// <summary>
        /// Gets the XML.
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            return TypeSerializer<ErrorReport>.ToXmlString(this);
        }
        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <returns></returns>
        public Stream GetStream()
        {
            var result = new MemoryStream();
            TypeSerializer<ErrorReport>.PutToXmlStream(result, this);
            return result;
        }
        /// <summary>
        /// Creates the report.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static ErrorReport CreateReport(Exception ex)
        {
            return CreateReport(ex, AppInfo.Product + " Unhandled exception", AppInfo.Version);
        }

        /// <summary>
        /// Creates the fill report.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="title">The title.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static ErrorReport CreateReport(Exception ex, string title,Version version)
        {
            var report = new ErrorReport
                             {
                                 Exeption = ex.ToString(),
                                 Title = title
                             };
            report.Enviroment= new EnviromentInfo();
            report.Versrion = version;
            return report;
        }




    }
}