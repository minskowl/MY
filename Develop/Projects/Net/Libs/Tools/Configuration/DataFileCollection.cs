using System;
using System.Configuration;

namespace Savchin.Configuration
{
    [ConfigurationCollection(typeof(DataFile), AddItemName = "dataFile",
                            CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class DataFileCollection : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection _properties;
        
        static DataFileCollection()
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
        protected override ConfigurationElement CreateNewElement()
        {
            return new DataFile();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((DataFile)element).Name;
        }
        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the collection; otherwise, an empty string. The default is an empty string.</returns>
        protected override string ElementName
        {
            get { return "dataFile"; }
        }
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        /// <summary>
        /// Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public DataFile Get(string name)
        {
            return (DataFile)BaseGet(name);
        }
        /// <summary>
        /// Gets the <see cref="NewWayMedia.Common.BusinessLayer.Core.Configuration.DataFile"/> at the specified index.
        /// </summary>
        /// <value></value>
        public DataFile this[int index]
        {
            get
            {
                return (DataFile)BaseGet(index);
            }
        }
        /// <summary>
        /// Gets the <see cref="NewWayMedia.Common.BusinessLayer.Core.Configuration.DataFile"/> with the specified name.
        /// </summary>
        /// <value></value>
        public new DataFile this[string name]
        {
            get
            {
                return (DataFile)BaseGet(name);
            }
        }
    }
}
