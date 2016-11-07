using System;
using System.Globalization;
using System.Windows.Data;

namespace Savchin.Wpf.Data
{
    /// <summary>
    /// Convert
    /// </summary>
    public delegate object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    /// <summary>
    /// DelegateConverter
    /// </summary>
    public class DelegateConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the forward converter.
        /// </summary>
        /// <value>The forward converter.</value>
        public Convert ForwardConverter { get; set; }
        /// <summary>
        /// Gets or sets the backward converter.
        /// </summary>
        /// <value>The backward converter.</value>
        public Convert BackwardConverter { get; set; }

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
            if (ForwardConverter == null)
                return Binding.DoNothing;
          return ForwardConverter(value, targetType, parameter, culture);
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
            if (BackwardConverter == null)
                return Binding.DoNothing;
            return BackwardConverter(value, targetType, parameter, culture);
        }
    }
}
