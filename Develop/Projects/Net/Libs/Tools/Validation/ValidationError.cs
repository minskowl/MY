using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Validation
{
    public class ValidationError
    {
        private readonly string propertyName;
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="message">The message.</param>
        public ValidationError(string propertyName, string message)
        {
            this.propertyName = propertyName;
            this.message = message;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get { return propertyName; }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return message; }
        }
    }
}
