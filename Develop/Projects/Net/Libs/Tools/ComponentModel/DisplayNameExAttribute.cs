using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Savchin.Text;

namespace Savchin.ComponentModel
{
    /// <summary>
    /// Class possible show localizable display name
    /// </summary>
    public class DisplayNameExAttribute : DisplayNameAttribute
    {
        private AdvancedString _displayName;

        /// <summary>
        /// Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The display name.
        /// </returns>
        public override string DisplayName
        {
            get
            {
                return _displayName.GetValue();
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameExAttribute"/> class.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        public DisplayNameExAttribute(string displayname)
        {
            _displayName = new AdvancedString(displayname);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameExAttribute"/> class.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public DisplayNameExAttribute(string resourceType, string resourceName)
        {
            _displayName = new AdvancedString(resourceType, resourceName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameExAttribute"/> class.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        public DisplayNameExAttribute(AdvancedString displayname)
        {
            _displayName = displayname;
        }

    }
}
