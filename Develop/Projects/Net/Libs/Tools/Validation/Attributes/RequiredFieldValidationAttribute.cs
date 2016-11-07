namespace Savchin.Validation
{
    /// <summary>
    /// Required Field Validator Attribute.
    /// </summary>
    public class RequiredFieldValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the default message.
        /// </summary>
        /// <value>The default message.</value>
        protected override string DefaultMessage
        {
            get { return Resources.Validation_RequiredField; }
        }
    }
}
