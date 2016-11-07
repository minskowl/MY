using System.Linq;
using System.Reflection;

namespace Savchin.CommandLine
{
    abstract class BaseSwitch
    {
        protected readonly SwitchAttribute Data;
        protected readonly PropertyInfo Property;
        protected readonly object Object;

        /// <summary>
        /// Gets or sets a value indicating whether [ingore case].
        /// </summary>
        /// <value><c>true</c> if [ingore case]; otherwise, <c>false</c>.</value>
        public static bool IngoreCase { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSwitch"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="property">The property.</param>
        /// <param name="o">The o.</param>
        protected BaseSwitch(SwitchAttribute data, PropertyInfo property, object o)
        {
            Data = data;
            Object = o;
            Property = property;
        }

        /// <summary>
        /// Processes the switch.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool ProcessSwitch(string name, string value)
        {
            if(Data.Names.Contains(name))
            {
                SetValue(value);
                return true;
            }
            return false;
        }

        protected abstract void SetValue(string value);

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public virtual void Validate()
        {
          
        }
    }
}
