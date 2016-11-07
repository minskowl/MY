using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using Advertiser.Core;
using Advertiser.Entities;
using Savchin.Logging;
using Savchin.Wpf.Data;
using Savchin.Wpf.Imaging;

namespace Advertiser.Controls
{
    /// <summary>
    /// Interaction logic for WheelsControl.xaml
    /// </summary>
    public partial class WheelsControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WheelsControl"/> class.
        /// </summary>
        public WheelsControl()
        {
            InitializeComponent();
        }
    }


    public class PhonesSelectedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var p = (Phone) values[0];
                var phones = (List<int>) values[1];
                return phones.Contains(p.Id); 

            }
            catch (Exception ex)
            {
                AdvContext.Current.Log.AddMessage(Severity.Error, "Ошибка выбора телефона", ex);
                return Binding.DoNothing;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ImageConverter : SimpleConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var path = Path.GetFullPath((string)value);
                return path.ToImageSource();
            }
            catch (Exception ex)
            {
                AdvContext.Current.Log.AddMessage(Severity.Warning, "Ошибка просмотра картинки", ex);
                return Binding.DoNothing;
            }
        }
    }

}
