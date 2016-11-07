using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Savchin.Web.UI.PropertyGrid
{
    class PropertyGridSubItem : PropertyGridItem
    {
        PropertyGridItem parentitem;

        public PropertyGridSubItem(PropertyDescriptor pd, PropertyGridItem parentitem)
            : base(pd)
        {
            this.parentitem = parentitem;
        }

        public PropertyDescriptor ParentDescriptor
        {
            get { return parentitem.Descriptor; }
        }

        public override object SelectedObject
        {
            get { return parentitem.Descriptor.GetValue(base.SelectedObject); }
        }

        public PropertyGridItem ParentItem
        {
            get { return parentitem; }
        }

        protected override bool IsSubItem
        {
            get { return true; }
        }

    }
}
