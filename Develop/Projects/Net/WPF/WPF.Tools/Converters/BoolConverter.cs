using System;
using System.Globalization;
using System.Windows.Data;

namespace Savchin.Wpf.Converters
{
    /// <summary>
    /// Implements logic of conversion for boolean value
    /// </summary>
    public class BoolConverter : IValueConverter
    {
        #region Properties

        /// <summary>
        /// Gets or sets the true value which is returned by converted when bound value is equal to true
        /// </summary>
        /// <value>The true value.</value>
        public object TrueValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the false value which is returned by converter when bound value is equal to false
        /// </summary>
        /// <value>The false value.</value>
        public object FalseValue
        {
            get;
            set;
        }

        #endregion

        #region Implementation of IValueConverter

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool))
                return Binding.DoNothing;

            return (bool)value ? TrueValue : FalseValue;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
