using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace Savchin.CodeGeneration
{
    [Serializable]
    public class Generation
    {
        /// <summary>
        /// Gets or sets the template file.
        /// </summary>
        /// <value>The template file.</value>
        [XmlAttribute]
        public string TemplateFile { get; set; }


        /// <summary>
        /// Gets or sets the destination file.
        /// </summary>
        /// <value>The destination file.</value>
        [XmlAttribute()]
        public string DestinationFile { get; set; }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        [XmlAttribute]
        public string ObjectType { get; set; }


        /// <summary>
        /// Gets or sets the destination directory.
        /// </summary>
        /// <value>The destination directory.</value>
        [XmlAttribute, Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string DestinationDirectory { get; set; }


        /// <summary>
        /// Gets or sets the solution path.
        /// </summary>
        /// <value>The solution path.</value>
        [XmlAttribute]
        public string SolutionPath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Generation"/> class.
        /// </summary>
        public Generation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Generation"/> class.
        /// </summary>
        /// <param name="templateFile">The template file.</param>
        /// <param name="destinationFile">The destination file.</param>
        public Generation(String templateFile, String destinationFile)
        {
            TemplateFile = templateFile;
            DestinationFile = destinationFile;
        }










    }
}
