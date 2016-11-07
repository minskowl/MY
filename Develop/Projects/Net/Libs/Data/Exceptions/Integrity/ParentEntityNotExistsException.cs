using System;

namespace Savchin.Data
{
    /// <summary>
    /// ParentObjectNotExistsException.
    /// </summary>
    /// 
    [Serializable()]
    public class ParentEntityNotExistsException : IntegrityException 
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



        /// <summary>
        /// Initializes a new instance of the <see cref="T:ParentEntityNotExistsException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ParentEntityNotExistsException(string name): base("Parent Entity Not Exists: " + name)
		{
            this.name = name;
		}

	}


}
