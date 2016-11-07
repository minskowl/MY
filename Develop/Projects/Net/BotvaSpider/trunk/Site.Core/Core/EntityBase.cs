using Savchin.Validation;

namespace Site.Core
{
    public class EntityBase<T> : IValidatable
        where T : class, new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public EntityBase()
        {
            objectValue = new T();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal EntityBase(T value)
        {
            objectValue = value;
        }

        protected T objectValue;
        /// <summary>
        /// Gets or sets the object value.
        /// </summary>
        /// <value>The object value.</value>
        protected internal T ObjectValue
        {
            get { return objectValue; }
            set { objectValue = value; }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public virtual void Validate()
        {
            var validator = new ObjectValidator();
            validator.ValidateFields(objectValue);
            validator.Validate(this);

        }


    }
}