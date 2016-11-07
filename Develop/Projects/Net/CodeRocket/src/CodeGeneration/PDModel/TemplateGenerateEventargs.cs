using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.CodeGeneration
{
    public delegate void TemplateGeneratedDelegate(object Sender, TemplateGenerateEventArgs e);
    
    public class TemplateGenerateEventArgs 
    {
        private string filePath;

        /// <summary>
        /// Gets the fil path.
        /// </summary>
        /// <value>The fil path.</value>
        public string FilePath
        {
            get { return filePath; }
        }
        

        private string fileContent;
        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        /// <value>The content of the file.</value>
        public string FileContent
        {
            get { return fileContent; }
            set { fileContent = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateGenerateEventArgs"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileContent">Content of the file.</param>
        public TemplateGenerateEventArgs(string filePath, string fileContent)
        {
            this.filePath = filePath;
            this.fileContent = fileContent;
        }
    }
}
