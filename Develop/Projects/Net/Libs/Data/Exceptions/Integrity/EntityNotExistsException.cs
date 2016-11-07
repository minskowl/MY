using System;

namespace Savchin.Data
{
    /// <summary>
    /// EntityNotExistsException.
    /// </summary>
    /// 

    [Serializable()]
    public class EntityNotExistsException : IntegrityException
    {

        private string name;
        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string Name
        {
            get { return name; }
        }
        private string id;
        /// <summary>
        /// Gets and set the ID.
        /// </summary>
        /// <value>The ID.</value>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:EntityNotExistsException"/> class.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        public EntityNotExistsException(string name)
            : base("Entity Not Exists: " + name)
        {

            this.name = name;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EntityNotExistsException"/> class.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="id">The Id.</param>
        public EntityNotExistsException(string name, string id)
            : base("Entity Not Exists: " + name)
        {
            this.name = name;
            this.id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:EntityNotExistsException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The id.</param>
        public EntityNotExistsException(string name, int id)
            : base("Entity Not Exists: " + name)
        {
            this.name = name;
            this.id = id.ToString( );
        }
    }
}
