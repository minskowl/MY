using System;
using System.Collections.Generic;
using System.Reflection;
using Savchin.Validation.Validators;

namespace Savchin.Validation
{
    /// <summary>
    /// ObjectValidator
    /// </summary>
    public class ObjectValidator
    {
        private static readonly Dictionary<Type, TypeValidator> _cache = new Dictionary<Type, TypeValidator>();

        #region Interface

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errors">The errors.</param>
        public void Validate(Object obj, ErrorCollection errors)
        {
            var type = obj.GetType();
            var members = type.GetProperties();

            var validator = GetTypeValidator(type, members);
            validator.Validate(errors, members, obj);
        }



        /// <summary>
        /// Validates the fields.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errors">The errors.</param>
        public void ValidateFields(Object obj, ErrorCollection errors)
        {
            var type = obj.GetType();
            var members = type.GetFields();
            var validator = GetTypeValidator(type, members);
            validator.Validate(errors, members, obj);
           
        }
        #endregion

        private static TypeValidator GetTypeValidator(Type type, IEnumerable<MemberInfo> members)
        {
            if (_cache.ContainsKey(type)) return _cache[type];


            var result = new TypeValidator(members);
            _cache.Add(type, result);
            return result;
        }





    }
}
