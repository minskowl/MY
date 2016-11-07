using System.Reflection;

namespace Savchin.Validation.Validators
{
    /// <summary>
    /// RequiredFieldValidator
    /// </summary>
    class RequiredFieldValidator : ValidatorBase<RequiredFieldValidationAttribute>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFieldValidator"/> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="info">The info.</param>
        public RequiredFieldValidator(MemberInfo info, RequiredFieldValidationAttribute attribute)
            : base(info, attribute)
        {
        }

        /// <summary>
        /// Validates the specified errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="member">The member.</param>
        /// <param name="value">The value.</param>
        public override void Validate(ErrorCollection errors, MemberInfo member, object value)
        {
            if (member.Name != MemberName) return;

            if (value == null)
            {
                errors.Add(member, attribute);
                return;
            }

            if (value is string && string.IsNullOrEmpty((string)value))
                errors.Add(member, attribute);
        }

    }
}
