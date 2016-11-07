using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using System.IO;
using Savchin.Core;
using Savchin.Validation;

namespace Savchin.CodeGeneration
{
    public enum ProjectType
    {
        Assembly,
        PD,
        Schema
    }
    [Serializable]
    public class GenerateProject : IValidatable
    {

        #region Properties
        public const string FileExtension = ".gp";

        [NonSerialized]
        private string _projectFileName = "default" + FileExtension;
        /// <summary>
        /// Gets the name of the project file.
        /// </summary>
        /// <value>The name of the project file.</value>
        [XmlIgnore]
        public string ProjectFileName
        {
            get { return _projectFileName; }
        }
        [NonSerialized]
        private string _projectDirectory = string.Empty;
        /// <summary>
        /// Gets the project directory.
        /// </summary>
        /// <value>The project directory.</value>
        [XmlIgnore]
        public string ProjectDirectory
        {
            get { return _projectDirectory; }
        }

        /// <summary>
        /// Gets or sets the schema file.
        /// </summary>
        /// <value>The schema file.</value>
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor)), RequiredFieldValidation()]
        public string SchemaFile { get; set; }

        /// <summary>
        /// Gets the schema file full path.
        /// </summary>
        /// <value>The schema file full path.</value>
        [XmlIgnore]
        public string SchemaFileFullPath
        {
            get { return GetFullPath(SchemaFile); }
        }

        /// <summary>
        /// Gets or sets the solution file.
        /// </summary>
        /// <value>The solution file.</value>
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string SolutionFile { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the template path.
        /// </summary>
        /// <value>The template path.</value>
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string TemplatePath { get; set; }

        /// <summary>
        /// Gets the template full path.
        /// </summary>
        /// <value>The template full path.</value>
        [XmlIgnore]
        public string TemplateFullPath
        {
            get { return GetFullPath(TemplatePath); }
        }

        /// <summary>
        /// Gets or sets the last view.
        /// </summary>
        /// <value>The last view.</value>
        public string LastView { get; set; }

        /// <summary>
        /// Gets or sets the type of the project.
        /// </summary>
        /// <value>The type of the project.</value>
        [XmlAttribute]
        public ProjectType ProjectType { get; set; }

        /// <summary>
        /// Gets or sets the visual studio version.
        /// </summary>
        /// <value>The visual studio version.</value>
        [XmlAttribute]
        public VisualStudio.Version VisualStudioVersion { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>The properties.</value>
        [XmlArrayItem(ElementName = "Property", Type = typeof(Property)), XmlArray()]
        public List<Property> Properties { get; set; }

        /// <summary>
        /// Gets or sets the generations.
        /// </summary>
        /// <value>The generations.</value>
        [XmlArrayItem(ElementName = "Generation", Type = typeof(Generation)), XmlArray]
        public List<Generation> Generations { get; set; }


        /// <summary>
        /// Gets or sets the book marks.
        /// </summary>
        /// <value>The book marks.</value>
        [XmlArrayItem(ElementName = "BookMark", Type = typeof(BookMark)), XmlArray()]
        public List<BookMark> BookMarks { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateProject"/> class.
        /// </summary>
        public GenerateProject()
        {
            Properties = new List<Property>();
            Generations = new List<Generation>();
            VisualStudioVersion = VisualStudio.Version.v80;
            ProjectType = ProjectType.Schema;
            LastView = string.Empty;
            TemplatePath = string.Empty;
            OutputPath = string.Empty;
            SolutionFile = string.Empty;
            SchemaFile = string.Empty;
            BookMarks = new List<BookMark>();
        }

        #region Methods
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Save(_projectFileName);
        }



        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(string fileName)
        {
            TypeSerializer<GenerateProject>.ToXmlFile(fileName, this);
        }


        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static GenerateProject Load(String fileName)
        {
            var result = TypeSerializer<GenerateProject>.FromXmlFile(fileName);
            result._projectFileName = fileName;
            result._projectDirectory = Path.GetDirectoryName(fileName);
            return result;
        }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string GetFullPath(string path)
        {
            return Path.IsPathRooted(path) ? path : Path.Combine(ProjectDirectory , path);
        }



        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <exception cref="ValidationException"></exception>
        public void Validate()
        {
            if (string.IsNullOrEmpty(SchemaFile))
            {
                throw new ValidationException("Schema File is empty", "SchemaFile");
            }
            if (File.Exists(SchemaFile))
            {
                throw new ValidationException("Schema File not found.", "SchemaFile");
            }
        }

        #endregion
    }
}
