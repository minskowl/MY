using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Savchin.Validation.Validators
{
    interface IValidator
    {
        void Validate(ErrorCollection errors, MemberInfo member, Object value);
    }
}
