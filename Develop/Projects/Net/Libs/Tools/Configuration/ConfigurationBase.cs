using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Savchin.Configuration
{

    // Define the ConfigurationElement for the types contained by the 
    // UrlsSection.
    public class ConfigurationBase : ConfigurationElement
    {
        protected readonly ConfigurationProperty _propName;
        protected ConfigurationPropertyCollection _properties;

        public ConfigurationBase()
        {
            _propName = new ConfigurationProperty("name", 
                                                  typeof(string), 
                                                  string.Empty, 
                                                  ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey );

            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_propName);

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
        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base[_propName];
            }
        }



    }


}
