using System.Configuration;
using System.IO;
using System.Web;

namespace Savchin.Configuration
{
    public class DataFile : ConfigurationElement
    {
        private static ConfigurationPropertyCollection _properties;
        private static readonly ConfigurationProperty _propName;
        private static readonly ConfigurationProperty _propPath;



        static DataFile()
        {
            _propName = new ConfigurationProperty("name",
                                                  typeof(string),
                                                  string.Empty,
                                                  ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

            _propPath = new ConfigurationProperty("path",
                                                  typeof(string),
                                                  string.Empty,
                                                  ConfigurationPropertyOptions.IsRequired);
            
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_propName);
            _properties.Add(_propPath);            

        }
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base[_propName];
            }
        }
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        [ConfigurationProperty("path",  IsRequired = true)]
        public string Path
        {
            get
            {
                return (string)base[_propPath];
            }
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
           return File.ReadAllText(Path);
        }
    }
}
