using System;
using System.Collections.Generic;
using System.IO;

using System.Net;
using System.Text;
using System.Web;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using Savchin.IO;
using Savchin.Net;
using Savchin.Text;

using System.Text.RegularExpressions;
using Savchin.Web.Chm;

namespace KnowledgeBase.KbTools.Export
{
    public class BaseToHtmlBuilder
    {
        #region Constants
        private static readonly string[] scripts = new[] { 
                "scripts/highlight/languages/1c.js",
                "scripts/highlight/languages/axapta.js",
                "scripts/highlight/languages/bash.js",
                "scripts/highlight/languages/diff.js",
                "scripts/highlight/languages/dos.js",
                "scripts/highlight/languages/dynamic.js",
                "scripts/highlight/languages/ini.js",
                "scripts/highlight/languages/javascript.js",
                "scripts/highlight/languages/lisp.js",
                "scripts/highlight/languages/mel.js",
                "scripts/highlight/languages/profile.js",
                "scripts/highlight/languages/renderman.js",
                "scripts/highlight/languages/smalltalk.js",
                "scripts/highlight/languages/sql.js",
                "scripts/highlight/languages/static.js",
                "scripts/highlight/languages/vbscript.js",
                "scripts/highlight/languages/www.js",
            };

        private const string defaultHeader = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 3.2 Final//EN'>
<HTML>
<HEAD>
<meta content='text/html; charset=utf-8' http-equiv='Content-Type'/>
<TITLE>Knowledge Base</TITLE>
</head>

<BODY>
<table>
";

        private const string defaultFooter = "</table></body></html>";
        public const string DefaultPage = "index.html";
        #endregion
        private readonly static Regex regexViewstate = new Regex(@"<input\s+type=""hidden""\s+name=""__VIEWSTATE""\s+id=""__VIEWSTATE""\s+value=""[^<>]+""\s*/>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly KnowledgeManager knowledgeManager = KbContext.CurrentKb.ManagerKnowledge;
        private readonly CategoryManager categoryManager = KbContext.CurrentKb.ManagerCategory;
        private PageDownloader downloader;

        private string baseUrl;
        private string destinationFolder;
        private StringBuilder folderTree;

        #region Properties

        private readonly List<string> generatedFiles = new List<string>();
        /// <summary>
        /// Gets the HTML files.
        /// </summary>
        /// <value>The HTML files.</value>
        public List<string> GeneratedFiles
        {
            get { return generatedFiles; }
        }
        private string rootFilePath;
        /// <summary>
        /// Gets the root file path.
        /// </summary>
        /// <value>The root file path.</value>
        public string RootFilePath
        {
            get { return rootFilePath; }
        }
        private readonly IndexFile indexFile = new IndexFile();
        /// <summary>
        /// Gets the index file.
        /// </summary>
        /// <value>The index file.</value>
        public IndexFile IndexFile
        {
            get { return indexFile; }
        }
        private readonly TocFile tocFile = new TocFile();
        /// <summary>
        /// Gets the toc file.
        /// </summary>
        /// <value>The toc file.</value>
        public TocFile TocFile
        {
            get { return tocFile; }
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IList<Exception> Errors
        {
            get { return downloader.Errors; }
        }

        #endregion

        /// <summary>
        /// Builds the specified destination folder.
        /// </summary>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <param name="baseUrl">The base URL.</param>
        public void Build(string destinationFolder, string baseUrl)
        {
            generatedFiles.Clear();


            this.baseUrl = baseUrl;
            this.destinationFolder = destinationFolder;
            rootFilePath = Path.Combine(destinationFolder, DefaultPage);

            DirectoryHelper.Delete(destinationFolder);
            Directory.CreateDirectory(destinationFolder);

            downloader = new PageDownloader(destinationFolder, new Uri(baseUrl));
            downloader.Processor = ClearHtml;


            folderTree = new StringBuilder(defaultHeader);
            IList<Category> categories = categoryManager.GetRootLevel();
            foreach (var category in categories)
            {
                ExportCategory(tocFile.ChildNodes, category, 0);
            }

            folderTree.AppendLine(defaultFooter);
            File.WriteAllText(rootFilePath, folderTree.ToString());
            generatedFiles.Add(rootFilePath);

            CreateJavascripts();

        }
        private void CreateJavascripts()
        {
            var baseUri = new Uri(baseUrl);
            foreach (string script in scripts)
            {
                try
                {
                    generatedFiles.Add(downloader.Download(new Uri(baseUri, script), script, false));
                }
                catch (DownloadException ex)
                {
                    downloader.Errors.Add(ex);
                }
            }

            generatedFiles.Add(downloader.Download(new Uri(baseUri, "images/search_16.gif"),
                                                             "KnowledgeBase/images/search_16.gif",
                                                             false));
        }

        private string GetCategoryFileName(Category category)
        {
            return string.Format("category{0}.html", category.CategoryID);
        }
        private static string ClearHtml(string input)
        {
            var result = regexViewstate.Replace(input, string.Empty);
            result=result.Replace("/KnowledgeBase/images/search_16.gif", "KnowledgeBase/images/search_16.gif");
            return result;
        }

        /// <summary>
        /// Exports the category.
        /// </summary>
        /// <param name="toc">The toc.</param>
        /// <param name="category">The category.</param>
        /// <param name="level">The level.</param>
        private void ExportCategory(NodeCollection toc, Category category, int level)
        {
            var knowledges = knowledgeManager.GetByCategoryID(category.CategoryID);
            var subCategories = categoryManager.GetByParentCategoryID(category.CategoryID);
            if (knowledges.Count == 0 && subCategories.Count == 0)
                return;

            string categoryFileName = GetCategoryFileName(category);
            var categoryFilePath = Path.Combine(destinationFolder, categoryFileName);


            var node = new HeadingNode(category.Name, categoryFilePath);
            toc.Add(node);



            folderTree.AppendFormat(@"<tr><td>{0}<a href='{1}'>{2}</a></td></tr>{3}",
                 StringUtil.Clone("&nbsp;", level),
                categoryFileName,
                HttpUtility.HtmlAttributeEncode(category.Name),
                Environment.NewLine
               );

            var categoryTocBuilder = new StringBuilder(defaultHeader);

            categoryTocBuilder.AppendFormat(@"<tr><td><H1>{0}</H1></td></tr>", category.Name);




            foreach (var subCategory in subCategories)
            {
                categoryTocBuilder.AppendFormat("<tr><td>&nbsp;<a href='{1}'><b>{0}</b></a></td></tr>{2}",
                      HttpUtility.HtmlAttributeEncode(subCategory.Name),
                    GetCategoryFileName(subCategory),
                    Environment.NewLine
                    );
                ExportCategory(node.ChildNodes, subCategory, level + 1);
            }

            foreach (var knowledge in knowledges)
            {
                try
                {
                    ExportKnowledge(node.ChildNodes, knowledge, categoryTocBuilder);
                }

                catch (Exception ex)
                {
                    //TODO:Uncommetn
                    //Log.BusinessLayer.Error(String.Format("Error ExportKnowledge KnowledgeID={0}", knowledge.KnowledgeID), ex);
                    return;
                }
            }

            File.WriteAllText(categoryFilePath, categoryTocBuilder.ToString());
            generatedFiles.Add(categoryFilePath);

        }

        private void ExportKnowledge(NodeCollection toc, Knowledge knowledge, StringBuilder categoryToc)
        {
            string fileName = "knowledge" + knowledge.KnowledgeID + ".html";
            string filePath = Path.Combine(destinationFolder, fileName);

            Uri url = new UriBuilder(knowledge.GetPublicUrl(baseUrl)).Uri;

            downloader.Download(url, filePath);

            generatedFiles.Add(filePath);
            categoryToc.AppendFormat("<tr><td><a href=\"{0}\">{1}</a></td></tr>{2}",
                                       fileName,
                                       HttpUtility.HtmlAttributeEncode(knowledge.Title),
                                       Environment.NewLine
                );

            toc.Add(new PageNode(knowledge.Title, filePath));
            indexFile.IndexKeys.Add(new IndexKey(knowledge.Title, filePath));
        }
    }
}
