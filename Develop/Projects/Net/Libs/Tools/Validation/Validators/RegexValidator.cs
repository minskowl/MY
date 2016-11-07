using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Savchin.Validation.Validators
{
    class RegexValidator : ValidatorBase<RegularExpressionValidationAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegexValidator"/> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="info">The info.</param>
        public RegexValidator(MemberInfo info, RegularExpressionValidationAttribute attribute)
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

            Type valueType = value.GetType();
            if (valueType.Equals(typeof(string)))
            {
                string valueString = (string)value;
                if (valueString != null && !Regex.IsMatch((string)value, attribute.RegularExpression))
                {
                    errors.Add(member, attribute);
                }
            }
        }
    }
}
