using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;
using NAnt.Core.Util;

namespace NAnt.Savchin.Tasks.IO
{
    [TaskName("replaceinfile")]
    public class ReplaceInFileTask : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        [TaskAttribute("options", Required = false), StringValidator(AllowEmpty = true)]
        public RegexOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }

        /// <summary>
        /// Gets or sets the replace.
        /// </summary>
        /// <value>The replace.</value>
        [StringValidator(AllowEmpty = true), TaskAttribute("replace", Required = true)]
        public string Replace
        {
            get { return _replace; }
            set { _replace = value; }
        }

        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>The pattern.</value>
        [StringValidator(AllowEmpty = false), TaskAttribute("pattern", Required = true)]
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        [TaskAttribute("file")]
        public virtual FileInfo File
        {
            get { return _file; }
            set { _file = value; }
        }

        /// <summary>
        /// Gets or sets the file set.
        /// </summary>
        /// <value>The file set.</value>
        [BuildElement("fileset")]
        public virtual FileSet FileSet
        {
            get { return _fileset; }
            set { _fileset = value; }
        }

        #endregion

        private RegexOptions _options = 0;
        private string _pattern;
        private string _replace;
        private FileInfo _file;
        private FileSet _fileset;


        // Methods
        protected override void ExecuteTask()
        {
            if (Verbose) Console.WriteLine("Start task replaceinfile: " + Pattern);
            Regex regex;
            try
            {
                regex = new Regex(Pattern, Options);
            }
            catch (ArgumentException exception)
            {
                throw new BuildException(
                    string.Format(CultureInfo.InvariantCulture, ResourceUtils.GetString("NA1145"),
                                  new object[] {Pattern}), Location, exception);
            }

            if(_replace==null)
                _replace = string.Empty;


            if (_file != null)
            {
                ReplaceFile(_file.FullName, regex);
            }
            else if (_fileset != null)
            {
                foreach (string fileName in _fileset.FileNames)
                {
                    ReplaceFile(fileName, regex);
                }
            }
            if (Verbose) 
                Console.WriteLine("End task replaceinfile: " + Pattern);
        }

        /// <summary>
        /// Replaces the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="regex">The regex.</param>
        private void ReplaceFile(string filePath, Regex regex)
        {
            if (Verbose) Console.WriteLine("file process: " + filePath);
            string fileContent = System.IO.File.ReadAllText(filePath);

            fileContent = regex.Replace(fileContent, _replace);
            System.IO.File.WriteAllText(filePath, fileContent);
        }
    }
}