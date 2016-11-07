using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Savchin.IO;


namespace Savchin.Net
{
    public class PageDownloader
    {
        public delegate string HtmlProcessor(string input);

        private const RegexOptions defaultOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled;

        private static readonly Regex[] processingExp = new Regex[]
                                                            {
                                                                new Regex("(?<=src=(\"|'))([^\"']*)(?=(\"|'))",defaultOptions),
                                                                new Regex("(?<=<link\\s+href=(\"|'))([^\"']*)(?=(\"|'))", defaultOptions)
                                                            };

        private static readonly Regex linkExp = new Regex("(?<=<a\\s+[^<>]*href=(\"|'))([^\"']*)(?=(\"|'))", defaultOptions);


        private readonly string destinationFolder;
        private readonly Uri baseUrl;
        private readonly WebClient client = new WebClient();
        private readonly Dictionary<string, string> fileMap = new Dictionary<string, string>();



        private readonly List<Exception> errors = new List<Exception>();
        public List<Exception> Errors
        {
            get { return errors; }
        }
        private HtmlProcessor processor;
        /// <summary>
        /// Gets or sets the processor.
        /// </summary>
        /// <value>The processor.</value>
        public HtmlProcessor Processor
        {
            get { return processor; }
            set { processor = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageDownloader"/> class.
        /// </summary>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <param name="baseUrl">The base URL.</param>
        public PageDownloader(string destinationFolder, Uri baseUrl)
        {
            this.destinationFolder = destinationFolder;
            this.baseUrl = baseUrl;

            DirectoryHelper.CreateIfNotExists(destinationFolder);
        }


        /// <summary>
        /// Downloads the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="destinationName">Name of the destination.</param>
        public string Download(Uri uri, string destinationName)
        {
            return Download(uri, destinationName, true);
        }

        /// <summary>
        /// Downloads the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
        /// <returns>Result file path</returns>
        public string Download(Uri uri, string destinationName, bool withFiles)
        {
            string destionationPath = Path.Combine(destinationFolder, destinationName);
            DownloadFile(uri, destionationPath);
            if (withFiles)
                File.WriteAllText(destionationPath, RebuildLinks(uri, File.ReadAllText(destionationPath)));
            return destionationPath;
        }


        /// <summary>
        /// Rebuilds the links.
        /// </summary>
        /// <param name="contentFile">The content file.</param>
        private string RebuildLinks(Uri pageUrl, string contentFile)
        {

            var result = DownloadResources(contentFile, pageUrl);

            var linkMatches = linkExp.Matches(result);
            foreach (Match linkMatch in linkMatches)
            {
                var link = linkMatch.Value;
                if (Uri.IsWellFormedUriString(link, UriKind.Relative))
                {
                    result = result.Replace(link, new Uri(pageUrl, link).ToString());
                }
            }

            return (processor == null) ? result : processor(result);
        }

        private string DownloadResources(string input, Uri pageUrl)
        {
            var result = input;

            foreach (var regex in processingExp)
            {
                var matches = regex.Matches(result);
                foreach (Match match in matches)
                {
                    try
                    {
                        result = result.Replace(match.Value, ProcessLink(new Uri(pageUrl, match.Value)));
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            return result;
        }

        private string ProcessLink(Uri uri)
        {
            string url = uri.ToString();
            if (fileMap.ContainsKey(url))
                return fileMap[url];

            string filePath = GetPath(uri);
            DownloadFile(uri, filePath);
            string result = PathHelper.GetRealitive(destinationFolder, filePath).Substring(2).Replace('\\', '/');
            fileMap.Add(url, result);
            return result;

        }

        private void DownloadFile(Uri uri, string filePath)
        {
            DirectoryHelper.CreateIfNotExists(Path.GetDirectoryName(filePath));
            try
            {
                client.DownloadFile(uri, filePath);
            }
            catch (Exception ex)
            {
                throw new DownloadException(uri, ex);
            }
        }

        private string GetPath(Uri uri)
        {
            var i = 0;
            while (i < baseUrl.Segments.Length &&
                   i < uri.Segments.Length &&
                   uri.Segments[i] == baseUrl.Segments[i]
                )
            {
                i++;
            }
            var result = destinationFolder;
            for (; i < uri.Segments.Length; i++)
            {
                result = Path.Combine(result, uri.Segments[i]);
            }
            result = result.Replace('/', '\\');
            return PathHelper.CreateUniqueName(result);
        }


    }
}