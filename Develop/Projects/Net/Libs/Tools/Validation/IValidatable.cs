using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Validation
{
    public interface IValidatable
    {
        /// <summary>
        /// Validates this instance.
        /// </summary>
        void Validate();
    }
}
