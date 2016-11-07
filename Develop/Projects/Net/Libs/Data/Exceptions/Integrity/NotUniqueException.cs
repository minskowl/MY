using System;

namespace Savchin.Data
{

    /// <summary>
    /// Это исключение бросается при нарушении уникальности индекса или поля в БД.
    /// </summary>
    /// 

    [Serializable()]
    public class NotUniqueException : IntegrityException
    {
        /// <summary>
        /// Type of Exception 
        /// </summary>
        public enum Type : int
        {
            /// <summary>
            /// Exception raised for Index 
            /// </summary>
            Index = 0,

            /// <summary>
            /// Exception raised for  Field 
            /// </summary>
            Field = 1
        }
        private Type what;

        /// <summary>
        /// Gets the what Exception for.
        /// </summary>
        /// <value>The what.</value>
        public Type What
        {
            get { return what; }
        }

        private string name;
        /// <summary>
        /// Gets the name of field or index.
        /// </summary>
        /// <value>The name of Field or Index.</value>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NotUniqueException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public NotUniqueException(string name, Type type)
            : base()
        {
            this.name = name;
            this.what = type;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NotUniqueException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="innerException">The inner exception.</param>
        internal NotUniqueException(string name, Exception innerException) : base(string.Format("Not Unique {0}", name), innerException)
        {
            this.name = name;

        }




    }
}
