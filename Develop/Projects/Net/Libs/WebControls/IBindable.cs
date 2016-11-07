using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Web.UI
{
    public interface IBindable
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        string PropertyName { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance can get value.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can get value; otherwise, <c>false</c>.
        /// </value>
        bool CanGetValue { get; }
        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetValue(object value);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        object GetValue();
    }
}
