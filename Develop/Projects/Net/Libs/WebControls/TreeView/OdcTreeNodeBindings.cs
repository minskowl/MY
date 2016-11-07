using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// A collection of OdcTreeNodeBindings.
    /// </summary>
    public class OdcTreeNodeBindings:List<OdcTreeNodeBinding>
    {
        /// <summary>
        /// Gets the first TreeNodeBinding with a specific name.
        /// </summary>
        /// <param name="name">The name of the OdcTreeNodeBinding.</param>
        /// <returns>An OdcTreeNodeBinding with the name, otherwise null.</returns>
        public OdcTreeNodeBinding GetNamedBinding(string name)
        {
            foreach (OdcTreeNodeBinding b in this)
            {
                if (b.Name == name) return b;
            }
            return null;
        }
    }
}
