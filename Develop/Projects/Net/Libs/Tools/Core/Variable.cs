using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Core
{
    public class Variable
    {
        private string name;
        private string value;

        public Variable(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}