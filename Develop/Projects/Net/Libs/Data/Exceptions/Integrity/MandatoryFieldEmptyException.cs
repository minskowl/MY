using System;

namespace Savchin.Data
{
    /// <summary>
    /// MandatoryFieldEmptyException
    /// </summary>
    /// 

    [Serializable()]
    public class MandatoryFieldEmptyException : IntegrityException
    {
        private string entityName;
        /// <summary>
        /// Gets the name of the Entity.
        /// </summary>
        /// <value>The name of the object.</value>
        public string EntityName
        {
            get { return entityName; }
        }

        private string fieldName;
        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName
        {
            get { return fieldName; }
        }
       

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MandatoryFieldEmptyException"/> class.
        /// </summary>
        /// <param name="entityName">Name of the object.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="inner">The inner.</param>
        internal MandatoryFieldEmptyException(string entityName, string fieldName, Exception inner)
            : base("Exception Mandatory Field Empty for " + entityName + " field: " + fieldName,inner)
        {
            this.entityName = entityName;
            this.fieldName = fieldName;

        }

 
    }
}
