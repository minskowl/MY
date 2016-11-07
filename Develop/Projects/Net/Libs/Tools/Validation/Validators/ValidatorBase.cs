using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Savchin.Validation.Validators
{
    abstract class ValidatorBase<T> : IValidator
        where T : Attribute
    {
        protected T attribute;
        /// <summary>
        /// MemberName
        /// </summary>
        protected string MemberName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="info">The info.</param>
        protected ValidatorBase(MemberInfo info, T attribute)
        {
            this.attribute = attribute;
            MemberName = info.Name;
        }

        #region Implementation of IValidator

        public abstract void Validate(ErrorCollection errors, MemberInfo member, object value);

        #endregion
    }
}
