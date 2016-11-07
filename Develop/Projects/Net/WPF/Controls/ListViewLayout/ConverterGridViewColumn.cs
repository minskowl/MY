using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Savchin.Wpf.Controls.ListViewLayout
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ConverterGridViewColumn : GridViewColumn, IValueConverter
    {
        // ----------------------------------------------------------------------
        private readonly Type bindingType;

        protected ConverterGridViewColumn(Type bindingType)
        {
            if (bindingType == null)
            {
                throw new ArgumentNullException("bindingType");
            }

            this.bindingType = bindingType;

            // binding
            var binding = new Binding();
            binding.Mode = BindingMode.OneWay;
            binding.Converter = this;
            DisplayMemberBinding = binding;
        } // ConverterGridViewColumn

        // ----------------------------------------------------------------------
        public Type BindingType
        {
            get { return bindingType; }
        } // BindingType

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!bindingType.IsAssignableFrom(value.GetType()))
            {
                throw new InvalidOperationException();
            }
            return ConvertValue(value);
        } // IValueConverter.Convert

        // ----------------------------------------------------------------------
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected abstract object ConvertValue(object value);
        // IValueConverter.ConvertBack

        // ----------------------------------------------------------------------
        // members
    } // class ConverterGridViewColumn
} // namespace Itenso.Windows.Controls.ListViewLayout
// -- EOF -------------------------------------------------------------------