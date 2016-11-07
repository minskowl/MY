using System;
using System.ComponentModel;
using Savchin.Forms.PropertyGrid;

namespace Savchin.Controls
{
    /// <summary>
    /// PropertyGridEx
    /// </summary>
    public class PropertyGridEx : System.Windows.Forms.PropertyGrid
    {
        /// <summary>
        /// Gets the type of the default tab.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.Type"/> representing the default tab.
        /// </returns>
        protected override Type DefaultTabType
        {
            get
            {
                return typeof(PropertiesTabEx);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGridEx"/> class.
        /// </summary>
        public PropertyGridEx()
        {
            //PropertyTabs.Clear(PropertyTabScope.Static);
            //PropertyTabs.RemoveTabType(typeof(PropertiesTab));
            this.PropertyTabs.AddTabType(typeof(PropertiesTabEx), PropertyTabScope.Static);
            //    ShowEventsButton(false);
        }
 
    }
}
