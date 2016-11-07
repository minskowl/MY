using System;
using System.Reflection;

namespace Savchin.Validation.Validators
{
    class RangeValidator : ValidatorBase<RangeValidationAttribute>
    {



        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidator"/> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="info">The info.</param>
        public RangeValidator(MemberInfo info, RangeValidationAttribute attribute)
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
            if (attribute.MinValue == null && attribute.MaxValue == null)return;

            var compValue = value is String ? ((string) value).Length : value as IComparable;

            if (compValue == null) return;
            if (attribute.MinValue != null && compValue.CompareTo(attribute.MinValue) == -1)
                errors.Add(member, attribute);

            if (attribute.MaxValue != null && compValue.CompareTo(attribute.MaxValue) == 1)
                errors.Add(member, attribute);

        }
     

    }
}
