using System;

namespace Savchin.Data
{
    /// <summary>
    /// ChildEntityExistsException.
    /// </summary>
    
    [Serializable()]
    public class ChildEntityExistsException : IntegrityException
    {

        private string name;
        /// <summary>
        /// Gets the name of the Entity.
        /// </summary>
        /// <value>The name of the object.</value>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ChildEntityExistsException"/> class.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        internal ChildEntityExistsException(string name)
            : base("Exception Child Entity Exists: " + name)
        {
            this.name = name;
        }
    }
}
