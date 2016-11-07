using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Forms.Browsers
{
    public delegate void RegistryEventHandler(object sender, RegistryEventArgs e);

    public class RegistryEventArgs : EventArgs
    {

        private readonly string keyName;

        /// <summary>
        /// Gets the name of the key.
        /// </summary>
        /// <value>The name of the key.</value>
        public string KeyName
        {
            get { return keyName; }
        }

        private readonly string valueName;
        /// <summary>
        /// Gets the name of the value.
        /// </summary>
        /// <value>The name of the value.</value>
        public string ValueName
        {
            get { return valueName; }
        }

        private readonly object value;
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return this.value; }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryEventArgs"/> class.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="value">The value.</param>
        internal RegistryEventArgs(string keyName, string valueName, object value)
        {
            this.valueName = valueName;
            this.keyName = keyName;
            this.value = value;
        }
    }
}
