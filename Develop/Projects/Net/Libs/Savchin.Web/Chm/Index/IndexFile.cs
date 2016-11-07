using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Savchin.Web.Chm
{
    public class IndexFile
    {
        private readonly NodeCollection indexKeyws = new NodeCollection();

        /// <summary>
        /// Gets the index keys.
        /// </summary>
        /// <value>The index keys.</value>
        public NodeCollection IndexKeys
        {
            get { return indexKeyws; }
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
	<param name=""FrameName"" value=""current"">
</OBJECT>
");
            indexKeyws.Write(writer, basePath);
            writer.WriteLine("</BODY></HTML>");
        }

        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(string fileName, Encoding encoding)
        {
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            using (var writer = new StreamWriter(stream, encoding))
            {
                Write(writer, Path.GetDirectoryName(fileName));
            }
        }
    }
}
