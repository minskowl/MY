using System;

namespace Savchin.WinApi.Shell
{
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Delegate | AttributeTargets.Parameter | AttributeTargets.Interface | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly, AllowMultiple = false)]
    public class RegistryKeyNameAttribute : Attribute
    {
        // Fields
        internal string xaf6ccd5c85247969;

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyNameAttribute"/> class.
        /// </summary>
        /// <param name="registryKeyName">Name of the registry key.</param>
        public RegistryKeyNameAttribute(string registryKeyName)
        {
            if (registryKeyName == null)
            {
                throw new ArgumentNullException();
            }
            this.xaf6ccd5c85247969 = registryKeyName;
        }

        internal static string x0ba0e0095169fb2c(Type x3201d6d15a947682)
        {
            object[] customAttributes = x3201d6d15a947682.GetCustomAttributes(typeof(RegistryKeyNameAttribute), false);
            if ((customAttributes == null) || (customAttributes.Length <= 0))
            {
                return x3201d6d15a947682.Name;
            }
            RegistryKeyNameAttribute attribute = (RegistryKeyNameAttribute)customAttributes[0];
            return attribute.xaf6ccd5c85247969;
        }

        // Properties
        /// <summary>
        /// Gets or sets the name of the registry key.
        /// </summary>
        /// <value>The name of the registry key.</value>
        public string RegistryKeyName
        {
            get
            {
                return this.xaf6ccd5c85247969;
            }
            set
            {
                if (this.xaf6ccd5c85247969 == null)
                {
                    throw new ArgumentNullException();
                }
                this.xaf6ccd5c85247969 = value;
            }
        }
    }

 

}
