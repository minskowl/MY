using System;
using System.Globalization;
using System.Windows.Data;

namespace TVSeriesTracker.Converters
{
    class SummaryWidthConverter : IValueConverter
    {
        public double Delta { get; set; }

        public SummaryWidthConverter()
        {
            Delta = 140;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - Delta;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
