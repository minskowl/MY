using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Savchin.Web.UI.PropertyGrid
{
    abstract class GridControl : Control
    {
        PropertyGridControl parentgrid;

        /// <summary>
        /// Gets the parent grid.
        /// </summary>
        /// <value>The parent grid.</value>
        public PropertyGridControl ParentGrid
        {
            get
            {
                if (parentgrid == null)
                {
                    Control p = Parent;
                    while (!(p is PropertyGridControl))
                    {
                        p = p.Parent;
                    }
                    parentgrid = (PropertyGridControl)p;
                }
                return parentgrid;
            }
        }
    }
}
