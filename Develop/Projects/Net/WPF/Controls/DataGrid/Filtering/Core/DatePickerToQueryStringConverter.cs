﻿using System;
using System.Windows.Data;

namespace Savchin.Wpf.Controls.DataGrid.Filtering.Core
{
    public class DatePickerToQueryStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object convertedValue;

            if (value != null && value.ToString() == String.Empty)
            {
                convertedValue = null;
            }
            else
            {
                DateTime dateTime;

                if (DateTime.TryParse(
                    value.ToString(),
                    culture.DateTimeFormat,
                    System.Globalization.DateTimeStyles.None,
                    out dateTime))
                {
                    convertedValue = dateTime;
                }
                else
                {
                    convertedValue = null;
                }
            }

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
