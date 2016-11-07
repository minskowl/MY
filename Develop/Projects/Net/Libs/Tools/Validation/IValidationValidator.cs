using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Validation
{
    public interface IValidationValidator
    {
        /// <summary>
        /// Validates the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void Validate(ValidationException ex);

        /// <summary>
        /// Initializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        void Initialize(Type type);
    }
}
