using System;
using System.Globalization;
using System.Windows.Data;

namespace Bashni.Controls
{
    public class ConvertLevelToIndent : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value * 16;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Not supported - ConvertBack should never be called in a OneWay Binding.");
        }

        #endregion
    }
}