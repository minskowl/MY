using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;

namespace NAnt.Savchin.Tasks.XML
{
    [TaskName("xmlstrip")]
    public class XmlStrip : Task
    {
        /// <summary>
        /// Gets or sets the X path.
        /// </summary>
        /// <value>The X path.</value>
        [TaskAttribute("xpath", Required = true), StringValidator(AllowEmpty = false)]
        public string XPath { get; set; }

        /// <summary>
        /// Gets or sets the XML file.
        /// </summary>
        /// <value>The XML file.</value>
        [TaskAttribute("file")]
        public FileInfo XmlFile { get; set; }

        /// <summary>
        /// Gets or sets the file set.
        /// </summary>
        /// <value>The file set.</value>
        [BuildElement("fileset")]
        public virtual FileSet FileSet { get; set; }

        /// <summary>
        /// Executes the task.
        /// </summary>
        protected override void ExecuteTask()
        {

            if (XmlFile != null)
            {
                StripInFile(XmlFile.FullName);
            }
            else if (FileSet != null)
            {
                foreach (var fileName in FileSet.FileNames)
                {
                    StripInFile(fileName);
                }
            }
        }

        private void StripInFile(string filename)
        {
            if(Verbose) Log(Level.Info, "Attempting to load XML document in file '{0}'.", filename);
            var document = new XmlDocument();
            document.Load(filename);
            if (Verbose) Log(Level.Info, "XML document in file '{0}' loaded successfully.", filename);

            var nodes = document.SelectNodes(XPath);
            if (nodes == null || nodes.Count == 0)
            {
                Log(Level.Info, "Find nodes not found.");
                return;
            }
            if (Verbose) Log(Level.Info, "Find nodes '{0}'.", nodes.Count);
            foreach (XmlNode node in nodes)
            {
                node.ParentNode.RemoveChild(node);
            }


            if (Verbose) Log(Level.Info, "Attempting to save XML document to '{0}'.", filename);
            document.Save(filename);
            if (Verbose) Log(Level.Info, "XML document successfully saved to '{0}'.", filename);
        }
    }
}
