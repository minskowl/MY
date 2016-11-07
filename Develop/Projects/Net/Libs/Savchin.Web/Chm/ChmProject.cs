using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Ini;

namespace Savchin.Web.Chm
{
    /// <summary>
    /// 
    /// </summary>
    public class ChmProject
    {
        #region Properties

        private string filePath;

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private string compatibility = "1.1 or later";
        /// <summary>
        /// Gets or sets the compatibility.
        /// </summary>
        /// <value>The compatibility.</value>
        public string Compatibility
        {
            get { return compatibility; }
            set { compatibility = value; }
        }
        private string compiledFile;
        /// <summary>
        /// Gets or sets the compiled file.
        /// </summary>
        /// <value>The compiled file.</value>
        public string CompiledFile
        {
            get { return compiledFile; }
            set { compiledFile = value; }
        }
        private string indexFile;
        /// <summary>
        /// Gets or sets the index file. hhk file
        /// </summary>
        /// <value>The index file.</value>
        public string IndexFile
        {
            get { return indexFile; }
            set { indexFile = value; }
        }
        private string contentsFile;
        /// <summary>
        /// Gets or sets the contents file. hhc file
        /// </summary>
        /// <value>The contents file.</value>
        public string ContentsFile
        {
            get { return contentsFile; }
            set { contentsFile = value; }
        }
        private string defaultTopicFile;
        /// <summary>
        /// Gets or sets the default topic file.
        /// </summary>
        /// <value>The default topic file.</value>
        public string DefaultTopicFile
        {
            get { return defaultTopicFile; }
            set { defaultTopicFile = value; }
        }
        private string language = "0x419 Russian";
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        private bool createFullTextSearch = true;
        /// <summary>
        /// Gets or sets a value indicating whether [create full text search].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [create full text search]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateFullTextSearch
        {
            get { return createFullTextSearch; }
            set { createFullTextSearch = value; }
        }

        private bool createIndexAutomatical = true;
        /// <summary>
        /// Gets or sets a value indicating whether create index automatical from html keywords .
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [create index automatical]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateIndexAutomatical
        {
            get { return createIndexAutomatical; }
            set { createIndexAutomatical = value; }
        }

        private bool displayCompileProgress = false;
        /// <summary>
        /// Gets or sets a value indicating whether [display compile progress].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [display compile progress]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayCompileProgress
        {
            get { return displayCompileProgress; }
            set { displayCompileProgress = value; }
        }
        private readonly List<string> files = new List<string>();
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <value>The files.</value>
        public List<string> Files
        {
            get { return files; }
        }



        #endregion
        public void Save()
        {
            var document = new IniDocument();
            var sectionOptions = new IniSection("OPTIONS");
            sectionOptions.Set("Compatibility", compatibility);
            sectionOptions.Set("Compiled file", compiledFile);
            sectionOptions.Set("Display compile progress", displayCompileProgress ? "Yes" : "No");
            sectionOptions.Set("Auto Index", createIndexAutomatical ? "Yes" : "No");
            sectionOptions.Set("Full-text search", createFullTextSearch ? "Yes" : "No");
            sectionOptions.Set("Language", language);
            sectionOptions.Set("Default topic", defaultTopicFile);

            if (!string.IsNullOrEmpty(contentsFile))//hhc file
                sectionOptions.Set("Contents file", contentsFile);

            if (!string.IsNullOrEmpty(indexFile))//hhk file
                sectionOptions.Set("Index file", indexFile);

            document.Sections.Add(sectionOptions);


            var sectionFiles = new IniSection("FILES");
            foreach (var file in files)
            {
                sectionFiles.Set(file, null);
            }
            document.Sections.Add(sectionFiles);

            document.Save(FilePath);
        }
    }
}
