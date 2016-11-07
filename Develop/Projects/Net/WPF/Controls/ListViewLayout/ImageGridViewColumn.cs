using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Savchin.Wpf.Controls.ListViewLayout
{
    // ------------------------------------------------------------------------
    public abstract class ImageGridViewColumn : GridViewColumn, IValueConverter
    {
        // ----------------------------------------------------------------------
        protected ImageGridViewColumn() :
            this(Stretch.None)
        {
        } // ImageGridViewColumn

        // ----------------------------------------------------------------------
        protected ImageGridViewColumn(Stretch imageStretch)
        {
            var imageElement = new FrameworkElementFactory(typeof (Image));

            // image source
            var imageSourceBinding = new Binding();
            imageSourceBinding.Converter = this;
            imageSourceBinding.Mode = BindingMode.OneWay;
            imageElement.SetBinding(Image.SourceProperty, imageSourceBinding);

            // image stretching
            var imageStretchBinding = new Binding();
            imageStretchBinding.Source = imageStretch;
            imageElement.SetBinding(Image.StretchProperty, imageStretchBinding);

            var template = new DataTemplate();
            template.VisualTree = imageElement;
            CellTemplate = template;
        } // ImageGridViewColumn

        // ----------------------------------------------------------------------

        // ----------------------------------------------------------------------

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetImageSource(value);
        } // Convert

        // ----------------------------------------------------------------------
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected abstract ImageSource GetImageSource(object value);
        // ConvertBack
    } // class ImageGridViewColumn
} // namespace Itenso.Windows.Controls.ListViewLayout
// -- EOF -------------------------------------------------------------------