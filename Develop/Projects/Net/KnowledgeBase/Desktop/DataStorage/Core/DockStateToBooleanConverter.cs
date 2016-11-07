using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using AvalonDock;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// DockStateToBooleanConverter
    /// </summary>
    [Localizability(LocalizationCategory.NeverLocalize)]
    public sealed class DockStateToBooleanConverter : IValueConverter
    {
        // Methods
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

            if (value is DockableContentState)
            {
                var flag = (DockableContentState)value;
                return flag == DockableContentState.Hidden ? false : true;
            }

            return false;
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
            if (value is bool)
            {
                return (bool)value ? DockableContentState.Docked : DockableContentState.Hidden;
            }
            return DockableContentState.Hidden;
        }
    }

}
