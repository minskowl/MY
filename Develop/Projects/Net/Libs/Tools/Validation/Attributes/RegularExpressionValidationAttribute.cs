namespace Savchin.Validation
{
    /// <summary>
    /// Regular Expression ValidationAttribute
    /// </summary>
    public class RegularExpressionValidationAttribute : ValidationAttribute
    {

        /// <summary>
        /// Gets the default message.
        /// </summary>
        /// <value>The default message.</value>
        protected override string DefaultMessage
        {
            get { return Resources.Validation_FieldIncorrectFormat; }
        }

        private string regularExpression;

        /// <summary>
        /// Gets or sets the regular expression.
        /// </summary>
        /// <value>The regular expression.</value>
        public string RegularExpression
        {
            get { return regularExpression; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionValidationAttribute"/> class.
        /// </summary>
        /// <param name="regularExpression">The regular expression.</param>
        /// <param name="message">The message.</param>
        public RegularExpressionValidationAttribute(string regularExpression, string message)
            : base(message)
        {
            this.regularExpression = regularExpression;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionValidationAttribute"/> class.
        /// </summary>
        /// <param name="regularExpression">The regular expression.</param>
        public RegularExpressionValidationAttribute(string regularExpression)
            : this(regularExpression, null)
        {

        }
    }
}
