using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Savchin.Text;

namespace Prodigy.Converters
{
    public class IntArrayToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return ((int[])value).Join(",");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            if (string.IsNullOrWhiteSpace(text)) return null;

            var parts=text.Split(',');
            return parts.Select(e => int.Parse(e.Trim())).ToArray();
        }
    }
}
