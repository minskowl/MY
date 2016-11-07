using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms.PropertyGridInternal;

namespace Savchin.Forms.PropertyGrid
{
    /// <summary>
    /// 
    /// </summary>
    [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust"), PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    public class PropertiesTabEx : PropertyTab
    {
        /// <summary>
        /// extend everything
        /// </summary>
        public override bool CanExtend(object extendee)
        {
            return true;
        }
        // Methods
        /// <summary>
        /// Gets the default property.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public override PropertyDescriptor GetDefaultProperty(object obj)
        {
            PropertyDescriptor defaultProperty = base.GetDefaultProperty(obj);
            if (defaultProperty == null)
            {
                PropertyDescriptorCollection properties = this.GetProperties(obj);
                if (properties == null)
                {
                    return defaultProperty;
                }
                for (int i = 0; i < properties.Count; i++)
                {
                    if ("Name".Equals(properties[i].Name))
                    {
                        return properties[i];
                    }
                }
            }
            return defaultProperty;
        }

        /// <summary>
        /// Gets the properties of the specified component that match the specified attributes.
        /// </summary>
        /// <param name="component">The component to retrieve properties from.</param>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute"/> that indicates the attributes of the properties to retrieve.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that contains the properties.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
        {
            return this.GetProperties(null, component, attributes);
        }

        /// <summary>
        /// Gets the properties of the specified component that match the specified attributes and context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that indicates the context to retrieve properties from.</param>
        /// <param name="component">The component to retrieve properties from.</param>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute"/> that indicates the attributes of the properties to retrieve.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that contains the properties matching the specified context and attributes.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
        {
            if (attributes == null)
            {
                attributes = new Attribute[] { BrowsableAttribute.Yes };
            }
            if (context != null)
            {
                TypeConverter converter = (context.PropertyDescriptor == null) ? TypeDescriptor.GetConverter(component) : context.PropertyDescriptor.Converter;
                if ((converter != null) && converter.GetPropertiesSupported(context))
                {
                    return converter.GetProperties(context, component, attributes);
                }
            }
            return TypeDescriptor.GetProperties(component, attributes);
        }

        // Properties
        /// <summary>
        /// Gets the Help keyword that is to be associated with this tab.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The Help keyword to be associated with this tab.
        /// </returns>
        public override string HelpKeyword
        {
            get
            {
                return "vs.properties";
            }
        }

        /// <summary>
        /// Gets the name for the property tab.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The name for the property tab.
        /// </returns>
        public override string TabName
        {
            get
            {
                return "MyTab";
            }
        }
    }




}
