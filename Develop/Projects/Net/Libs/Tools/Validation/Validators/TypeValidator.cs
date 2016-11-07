using System.Collections.Generic;
using System.Reflection;
using Savchin.Core;

namespace Savchin.Validation.Validators
{
    class TypeValidator
    {
        private readonly ValidatorsCollection _validators = new ValidatorsCollection();

        /// <summary>
        /// Builds the type validator.
        /// </summary>
        /// <param name="members">The members.</param>
        public TypeValidator(IEnumerable<MemberInfo> members)
        {
 
            foreach (var member in members)
            {
                var attributes = member.GetCustomAttributes(typeof(ValidationAttribute), true);
                foreach (var attribute in attributes)
                {
                    if (attribute is RequiredFieldValidationAttribute)
                    {
                        _validators.Add(member.Name, new RequiredFieldValidator(member, (RequiredFieldValidationAttribute)attribute));
                    }
                    else if (attribute is RegularExpressionValidationAttribute)
                    {
                        _validators.Add(member.Name, new RegexValidator(member, (RegularExpressionValidationAttribute)attribute));
                    }
                    else if (attribute is RangeValidationAttribute)
                    {
                        _validators.Add(member.Name, new RangeValidator(member, (RangeValidationAttribute)attribute));
                    }
                }
            }
     
        }


        /// <summary>
        /// Validates the specified errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="members">The members.</param>
        /// <param name="obj">The obj.</param>
        public void Validate(ErrorCollection errors,IEnumerable<MemberInfo> members, object obj)
        {
            foreach (var member in members)
            {
                var memberValidators= _validators.GetValidators(member.Name);
                if(memberValidators==null) continue;
                var value = member.GetValue(obj);
                foreach (var memberValidator in memberValidators)
                {
                    memberValidator.Validate(errors, member, value);
                }
            }
        }
    }
}
