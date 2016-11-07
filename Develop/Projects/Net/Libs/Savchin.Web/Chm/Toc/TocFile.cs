using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Savchin.Web.Chm
{
    /// <summary>
    /// TocFile class
    /// </summary>
    public class TocFile
    {
        private readonly NodeCollection childNodes = new NodeCollection();
        /// <summary>
        /// Gets the child nodes.
        /// </summary>
        /// <value>The child nodes.</value>
        public NodeCollection ChildNodes
        {
            get { return childNodes; }
        }


        /// <summary>
        /// Writes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="basePath">The base path.</param>
        public void Write(TextWriter writer, string basePath)
        {
            writer.Write(@"<!DOCTYPE HTML PUBLIC ""-//IETF//DTD HTML//EN"">
<HTML>
<HEAD>
<meta name=""GENERATOR"" content=""Microsoft&reg; HTML Help Workshop 4.1"">
<!-- Sitemap 1.0 -->
</HEAD><BODY>
<OBJECT type=""text/site properties"">
	<param name=""Window Styles"" value=""0x800225"">
</OBJECT>
");
            childNodes.Write(writer, basePath);
            writer.WriteLine("</BODY></HTML>");
        }

        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(string fileName,Encoding encoding)
        {
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            using (var writer = new StreamWriter(stream,encoding))
            {
                Write(writer, Path.GetDirectoryName(fileName));
            }
        }
    }
}
