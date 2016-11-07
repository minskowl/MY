using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Savchin.Tasks.Http
{
    [TaskName("httpproperty")]
    public class HttpPropertyTask : Task
    {
        #region Properties

        private bool _dynamic = false;
        [BooleanValidator, TaskAttribute("dynamic", Required = false)]
        public bool Dynamic
        {
            get { return _dynamic; }
            set { _dynamic = value; }
        }

        private bool _overwrite = true;
        [TaskAttribute("overwrite", Required = false), BooleanValidator]
        public bool Overwrite
        {
            get { return _overwrite; }
            set { _overwrite = value; }
        }

        private string _name;
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [TaskAttribute("name", Required = true),
         StringValidator(AllowEmpty = false)]
        public string PropertyName
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _readOnly = false;
        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value><c>true</c> if [read only]; otherwise, <c>false</c>.</value>
        [TaskAttribute("readonly", Required = false), BooleanValidator]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }



        private string _url;
        [TaskAttribute("url", Required = true),
         StringValidator(AllowEmpty = false)]
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _regex;
        [TaskAttribute("regex", Required = false),
         StringValidator(AllowEmpty = true)]
        public string Regex
        {
            get { return _regex; }
            set { _regex = value; }
        }
        #endregion

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
            //Debugger.Break();
            string file=string.Empty;
            try
            {
                file = new WebClient().DownloadString(URL);

            }
            catch (WebException ex)
            {
                throw new ApplicationException("Error load url: " + URL, ex);
            }
            
            //Console.WriteLine("Regex: " + Regex);
            //Console.WriteLine("Downloaded: " + file);
            if (string.IsNullOrEmpty(Regex))
                return file;

            Match match = new Regex(Regex).Match(file);
            if (!match.Success)
            {
                //Console.WriteLine("Not matched");
                return string.Empty;
            }
            if (match.Groups["value"] != null && match.Groups["value"].Success)
            {
                //Console.WriteLine("value=" + match.Groups["value"].Value);
                return match.Groups["value"].Value;
            }
            //Console.WriteLine("value=" + match.Value);
            return match.Value;
        }
    }
}