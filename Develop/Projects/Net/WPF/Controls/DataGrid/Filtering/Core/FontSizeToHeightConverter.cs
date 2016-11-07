﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Savchin.Wpf.Controls.DataGrid.Filtering.Core
{
    public class FontSizeToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double height;

            if (value != null)
            {
                if(Double.TryParse(value.ToString(), out height))
                {
                    return height * 2;
                }
                else
                {
                    return Double.NaN;
                }
                
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
