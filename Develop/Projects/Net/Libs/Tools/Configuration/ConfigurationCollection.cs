using System;
using System.Configuration;

namespace Savchin.Configuration
{

    public abstract class ConfigurationCollection<ConfigurationType> : ConfigurationElementCollection
        where ConfigurationType : ConfigurationBase
    {
        protected  ConfigurationPropertyCollection _properties;

        public ConfigurationCollection()
        {
            _properties = new ConfigurationPropertyCollection();

        }

        /// <summary>
        /// Gets the collection of properties.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection"></see> collection of properties for the element.</returns>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }


        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationType)element).Name;
        }

        
        /// <summary>
        /// Gets the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public ConfigurationType Get(int index)
        {
            return (ConfigurationType)BaseGet(index);
        }
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"></see>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"></see>.
        /// </returns>
        protected override ConfigurationElement  CreateNewElement()
        {
            return Activator.CreateInstance<ConfigurationType>();
        }
        /// <summary>
        /// Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ConfigurationType Get(string name)
        {
            return (ConfigurationType)BaseGet(name);
        }
        /// <summary>
        /// Gets the <see cref="ConfigurationType"/> at the specified index.
        /// </summary>
        /// <value></value>
        public ConfigurationType this[int index]
        {
            get
            {
                return (ConfigurationType)BaseGet(index);
            }
        }
        /// <summary>
        /// Gets the <see cref="ConfigurationType"/> with the specified name.
        /// </summary>
        /// <value></value>
        public new ConfigurationType this[string name]
        {
            get
            {
                return (ConfigurationType)BaseGet(name);
            }
        }
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetKey(int index)
        {
            return (string)BaseGetKey(index);
        }

        /// <summary>
        /// Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType"></see> of this collection.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        

 

    }
}
