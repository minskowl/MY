using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Util;

namespace NAnt.Savchin.Tasks.IO
{
    /// <summary>
    /// FilePropertyTask
    /// </summary>
    [TaskName("fileproperty")]
    public class FilePropertyTask : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FilePropertyTask"/> is dynamic.
        /// </summary>
        /// <value><c>true</c> if dynamic; otherwise, <c>false</c>.</value>
        [BooleanValidator, TaskAttribute("dynamic", Required = false)]
        public bool Dynamic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FilePropertyTask"/> is overwrite.
        /// </summary>
        /// <value><c>true</c> if overwrite; otherwise, <c>false</c>.</value>
        [TaskAttribute("overwrite", Required = false), BooleanValidator]
        public bool Overwrite { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [TaskAttribute("name", Required = true), StringValidator(AllowEmpty = false)]
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value><c>true</c> if [read only]; otherwise, <c>false</c>.</value>
        [TaskAttribute("readonly", Required = false), BooleanValidator]
        public bool ReadOnly { get; set; }


        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        [TaskAttribute("file", Required = true)]
        public virtual FileInfo File { get; set; }


        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        [TaskAttribute("options", Required = false), StringValidator(AllowEmpty = true)]
        public RegexOptions Options { get; set; }


        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>The pattern.</value>
        [StringValidator(AllowEmpty = false), TaskAttribute("pattern", Required = true)]
        public string Pattern { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePropertyTask"/> class.
        /// </summary>
        public FilePropertyTask()
        {
            ReadOnly = false;
            Dynamic = false;
            Overwrite = true;
            Options = 0;
        }
        /// <summary>
        /// Executes the task.
        /// </summary>
        protected override void ExecuteTask()
        {
            string value = GetValue();

            if (!Project.Properties.Contains(PropertyName))
            {
                if (ReadOnly)
                {
                    Properties.AddReadOnly(PropertyName, value);
                }
                else
                {
                    Properties[PropertyName] = value;
                }
                if (Dynamic)
                {
                    Properties.MarkDynamic(PropertyName);
                }
            }
            else if (Overwrite)
            {
                if (Project.Properties.IsReadOnlyProperty(PropertyName))
                {
                    Log(Level.Warning, "Read-only property \"{0}\" cannot be overwritten.", new object[] { PropertyName });
                }
                else
                {
                    Properties[PropertyName] = value;
                    if (Dynamic)
                    {
                        Properties.MarkDynamic(PropertyName);
                    }
                }
            }
            else
            {
                Log(Level.Verbose, "Property \"{0}\" already exists, and \"overwrite\" is set to false.",
                    new object[] { PropertyName });
            }
        }

        private string GetValue()
        {
            Regex regex;
            try
            {
                regex = new Regex(Pattern, Options);
            }
            catch (ArgumentException exception)
            {
                throw new BuildException(
                    string.Format(CultureInfo.InvariantCulture, ResourceUtils.GetString("NA1145"),
                                  new object[] { Pattern }), Location, exception);
            }
            if (Verbose) Console.WriteLine("file process: " + File.FullName);
            string fileContent = System.IO.File.ReadAllText(File.FullName);

            var match = regex.Match(fileContent);
            return match.Value;
        }
    }
}
