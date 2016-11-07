﻿using System;
using System.Windows.Data;

namespace Savchin.Wpf.Controls.DataGrid.Filtering.Core
{
    public class ComboBoxToQueryStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && value.ToString() == String.Empty ? null : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
