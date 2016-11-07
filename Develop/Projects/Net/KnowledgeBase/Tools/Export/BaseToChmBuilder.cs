using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Savchin.Web.Chm;


namespace KnowledgeBase.KbTools.Export
{
    public class BaseToChmBuilder
    {
        readonly BaseToHtmlBuilder htmlBuilder = new BaseToHtmlBuilder();
        readonly ChmBuilder chmBuilder = new ChmBuilder();

        private static readonly Encoding destinationEncoding = Encoding.GetEncoding("Windows-1251");
        private const string folderNameHtml = "Html";
        private string filePath;
        private string projectFilePath;
        private string htmlPath;
        private readonly List<Exception> errors= new List<Exception>();

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<Exception> Errors
        {
            get { return errors; }
        }


        /// <summary>
        /// Builds the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="baseUrl">The base URL.</param>
        public void Build(string fileName, string baseUrl)
        {
            filePath = (Path.IsPathRooted(fileName)) ? fileName : Path.GetPathRoot(fileName);
            projectFilePath = Path.ChangeExtension(filePath, ".hhp");
            string tocFilePath = Path.ChangeExtension(filePath, ".hhc");
            string indexFilePath = Path.ChangeExtension(filePath, ".hhk");

            string basePath = Path.GetDirectoryName(fileName);
            htmlPath = Path.Combine(basePath, folderNameHtml);

            htmlBuilder.Build(htmlPath, baseUrl);
            errors.AddRange(htmlBuilder.Errors);

            htmlBuilder.TocFile.Save(tocFilePath, destinationEncoding);
            htmlBuilder.IndexFile.Save(indexFilePath, destinationEncoding);


            int basePathLength = (basePath.EndsWith("\\")) ? basePath.Length : basePath.Length + 1;


            var project = new ChmProject
                              {
                                  FilePath = projectFilePath,
                                  CompiledFile = filePath,
                                  DefaultTopicFile = Path.Combine(folderNameHtml, BaseToHtmlBuilder.DefaultPage),
                                  ContentsFile = tocFilePath.Substring(basePathLength),
                                  IndexFile = indexFilePath.Substring(basePathLength)
                              };

            foreach (var htmlFile in htmlBuilder.GeneratedFiles)
            {
                project.Files.Add(htmlFile.Substring(basePathLength));
            }

            chmBuilder.Build(project);


        }

    }
}

