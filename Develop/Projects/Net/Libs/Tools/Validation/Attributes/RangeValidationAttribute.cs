using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Validation
{
    public class RangeValidationAttribute : ValidationAttribute
    {


        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        /// <value>The min value.</value>
        public object MinValue { get; set; }

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value.</value>
        public object MaxValue { get; set; }

        /// <summary>
        /// Gets the default message.
        /// </summary>
        /// <value>The default message.</value>
        protected override string DefaultMessage
        {
            get
            {
                if (MaxValue != null && MinValue != null)
                    return
                        string.Format(Resources.Validation_SrtingInRange, MinValue, MaxValue);

                if (MinValue != null)
                    return
                       string.Format(Resources.Validation_StringMore, MinValue);

                if (MaxValue != null)
                    return
                        string.Format(Resources.Validation_StringLess, MaxValue);
                return base.DefaultMessage;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidationAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        public RangeValidationAttribute(object minValue, object maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidationAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The max value.</param>
        public RangeValidationAttribute(object maxValue)
        {
            MaxValue = maxValue;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidationAttribute"/> class.
        /// </summary>
        public RangeValidationAttribute()
        {

        }

    }
}
