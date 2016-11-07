using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Savchin.Wpf.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that converts the casing of the input string.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>CaseConverter</c> class can be used to convert input strings into upper or lower case according to the <see cref="Casing"/>
    /// property.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how a <c>CaseConverter</c> can be used to convert a bound value to upper-case:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter Upper}}"/>
    /// ]]>
    /// </code>
    /// </example>
#if !SILVERLIGHT
    [ValueConversion(typeof(string), typeof(string))]
#endif
    public class CaseConverter : DependencyObject, IValueConverter
    {
        /// <summary>
        /// Identifies the <see cref="Casing"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CasingProperty = DependencyProperty.Register(
            "Casing",
            typeof(CharacterCasing),
            typeof(CaseConverter),
            new PropertyMetadata(CharacterCasing.Normal)
#if !SILVERLIGHT
            , ValidateCasing
#endif
            );

        /// <summary>
        /// Gets or sets the target casing for the converter.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("casing")]
#endif
        public CharacterCasing Casing
        {
            get { return (CharacterCasing)GetValue(CasingProperty); }
            set { SetValue(CasingProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverter class.
        /// </summary>
        public CaseConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverter class with the specified target casing.
        /// </summary>
        /// <param name="casing">
        /// The target casing for the converter (see <see cref="Casing"/>).
        /// </param>
        public CaseConverter(CharacterCasing casing)
        {
            this.Casing = casing;
        }

        /// <summary>
        /// Attempts to convert the specified value.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            if (str != null)
            {
                culture = culture ?? CultureInfo.CurrentCulture;

                switch (this.Casing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower(culture);
                    case CharacterCasing.Upper:
                        return str.ToUpper(culture);
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Attempts to convert the specified value back.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        private static bool ValidateCasing(object value)
        {
            Debug.Assert(value is CharacterCasing);

            try
            {
                ArgumentHelper.AssertEnumMember((CharacterCasing)value, "value");
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}
