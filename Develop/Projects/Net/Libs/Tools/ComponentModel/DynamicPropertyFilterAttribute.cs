using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.ComponentModel
{
    /// <summary>
    /// DynamicPropertyFilterAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DynamicPropertyFilterAttribute : Attribute
    {
        string _propertyName;


        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get { return _propertyName; }
        }

        string _showOn;


        /// <summary>
        /// Gets the show on.
        /// </summary>
        /// <value>The show on.</value>
        public string ShowOn
        {
            get { return _showOn; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicPropertyFilterAttribute"/> class.
        /// </summary>
        /// <param name="propName">Name of the prop.</param>
        /// <param name="value">The value.</param>
        public DynamicPropertyFilterAttribute(string propName, string value)
        {
            _propertyName = propName;
            _showOn = value;
        }
    }
}