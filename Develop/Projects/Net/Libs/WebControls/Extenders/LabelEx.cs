using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    public class LabelEx : Label, IBindable
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public virtual string PropertyName
        {
            get { return (string)ViewState["PropertyName"]; }
            set { ViewState["PropertyName"] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can get value.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can get value; otherwise, <c>false</c>.
        /// </value>
        public bool CanGetValue
        {
            get { return false; }
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue(object value)
        {
            Text = (value == null) ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
           throw new NotSupportedException();
        }
    }
}
