using System;
using System.Collections.Generic;

namespace Savchin.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        readonly ErrorCollection _errors = new ErrorCollection();
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IEnumerable<ValidationError> Errors
        {
            get { return _errors; }
        }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="field">The field.</param>
        public ValidationException(string message, string field)
        {
            _errors.Add(new ValidationError(field, message));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public ValidationException(ErrorCollection errors)
        {
            _errors = errors;
        } 
        #endregion

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public string GetMessage(string propertyName)
        {
            return _errors.GetMessage(propertyName);
        }
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            return _errors.GetMessage();
        }

    }
}
