using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Text
{
    public struct AdvancedString
    {
        private string _value;
        private string _resourceTypeName;
        private string _resourceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedString"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public AdvancedString(string value)
        {
            _value = value;
            _resourceTypeName = null;
            _resourceName = null;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedString"/> class.
        /// </summary>
        /// <param name="resourceTypeName">Name of the resource type.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public AdvancedString(string resourceTypeName, string resourceName)
        {
            _value = null;
            _resourceTypeName = resourceTypeName;
            _resourceName = resourceName;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            if (!string.IsNullOrEmpty(_value)) return _value;

            if (string.IsNullOrEmpty(_resourceTypeName)) return null;

            Type resourceType = Type.GetType(_resourceTypeName);
            if (null == resourceType) return null;



            return ResourceStringLoader.LoadString(resourceType.FullName, _resourceName, resourceType.Assembly);
        }
    }
}
