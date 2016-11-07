using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Savchin.ComponentModel
{
    /// <summary>
    /// EnumTypeConverter
    /// </summary>
    public class EnumTypeConverter : EnumConverter
    {
        private readonly Type _enumType;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTypeConverter"/> class.
        /// </summary>
        /// <param name="type">A <see cref="T:System.Type"/> that represents the type of enumeration to associate with this enumeration converter.</param>
        public EnumTypeConverter(Type type)
            : base(type)
        {
            _enumType = type;
        }

        /// <summary>
        /// Determines whether this instance [can convert to] the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destType">Type of the dest.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can convert to] the specified context; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            return destType == typeof(string);
        }

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destType">Type of the dest.</param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
                                         object value, Type destType)
        {
            FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, value));
            DescriptionAttribute dna =
                (DescriptionAttribute)Attribute.GetCustomAttribute(
                                          fi, typeof(DescriptionAttribute));

            return dna != null ? dna.Description : value.ToString();
        }

        /// <summary>
        /// Determines whether this instance [can convert from] the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="srcType">Type of the SRC.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can convert from] the specified context; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
        {
            return srcType == typeof(string);
        }

        /// <summary>
        /// Converts the specified value object to an enumeration object.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo"/>. If not supplied, the current culture is assumed.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that represents the converted <paramref name="value"/>.
        /// </returns>
        /// <exception cref="T:System.FormatException">
        /// 	<paramref name="value"/> is not a valid value for the target type.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            foreach (FieldInfo fi in _enumType.GetFields())
            {
                DescriptionAttribute dna =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(
                                              fi, typeof(DescriptionAttribute));

                if ((dna != null) && ((string)value == dna.Description))
                    return Enum.Parse(_enumType, fi.Name);
            }

            return Enum.Parse(_enumType, (string)value);
        }

        /// <summary>
        /// Gets a value indicating whether this object supports a standard set of values that can be picked from a list using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <returns>
        /// true because <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"/> should be called to find a common set of values the object supports. This method never returns false.
        /// </returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        /// <summary>
        /// Gets a value indicating whether the list of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"/> is an exclusive list using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <returns>
        /// true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"/> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"/> is an exhaustive list of possible values; false if other values are possible.
        /// </returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// А вот и список
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
        /// </returns>
        public override StandardValuesCollection GetStandardValues(
            ITypeDescriptorContext context)
        {

            return new StandardValuesCollection(Enum.GetValues(_enumType));
        }
    }
}