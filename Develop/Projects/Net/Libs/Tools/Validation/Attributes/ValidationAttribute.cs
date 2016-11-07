using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Validation
{
    /// <summary>
    /// Validation Attribute Base Class
    /// </summary>
    public class ValidationAttribute : Attribute
    {
        protected virtual string DefaultMessage
        {
            get { return "Field validation error."; }
        }

        private string message;
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return string.IsNullOrEmpty(message) ? DefaultMessage : message; }
            set { message = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationAttribute(string message)
        {
            this.message = message;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class.
        /// </summary>
        public ValidationAttribute()
        {
        }
    }
}
