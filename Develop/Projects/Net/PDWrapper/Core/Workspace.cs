using System;
using System.Collections.Generic;
using System.Text;

namespace PDWrapper
{
    public class Workspace
    {
        private object instance;
        private Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workspace"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        internal Workspace(object instance)
        {
            this.instance = instance;
            type = instance.GetType();
        }
    }
}
