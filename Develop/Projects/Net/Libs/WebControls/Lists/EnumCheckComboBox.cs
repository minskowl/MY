using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Collection.Generic;

namespace Savchin.Web.UI
{
    public class EnumCheckComboBox<T> : CheckBoxCombo
    {
        /// <summary>
        /// Gets or sets the type of the selected knowledge.
        /// </summary>
        /// <value>The type of the selected knowledge.</value>
        public List<T> SelectedValues
        {
            get { return CollectionUtil.ConvertToEnum<T>(SelectedLongValues); }
            set { SelectedLongValues = CollectionUtil.Convert<long>(value); }
        }

        /// <summary>
        /// Handles the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            if (!DesignMode)
                AddItems(typeof(T));

        }
    }
}
