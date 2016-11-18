using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;


namespace Savchin.Core
{

    /// <summary>
    /// Enum Helper Class
    /// </summary>
    /// <summary>
    /// Enum Helper Class
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        static Regex capitalLetterMatch = new Regex("\\B[A-Z]");

        ///// <summary>
        ///// Gets the description of.
        ///// </summary>
        ///// <param name="enumType">Type of the enum.</param>
        ///// <returns></returns>
        //public static string GetDescription(this Enum enumType)
        //{
        //    var memberInfo = enumType.GetType().GetMember(enumType.ToString());

        //    if (memberInfo == null || memberInfo.Length != 1)
        //        return enumType.ToString(true);

        //    var customAttributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        //    if (customAttributes == null || customAttributes.Length == 0)
        //        return enumType.ToString(true);


        //    return ((DescriptionAttribute)customAttributes[0]).Description;
        //}

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="splitWords">if set to <c>true</c> [split words].</param>
        /// <returns></returns>
        public static string ToString(this Enum enumType, bool splitWords)
        {
            return splitWords ? capitalLetterMatch.Replace(enumType.ToString(), " $&") : enumType.ToString();
        }

        ///// <summary>
        ///// Gets the data.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //public static NameValuePair<T>[] GetData<T>()
        //{
        //    return Enum.GetValues(typeof(T)).Cast<object>().Select(e => new NameValuePair<T>(((Enum)e).GetDescription(), (T)e)).ToArray();
        //}

        ///// <summary>
        ///// Gets the data.
        ///// </summary>
        ///// <param name="type">The type.</param>
        ///// <returns></returns>
        //public static NameValuePair[] GetData(Type type)
        //{

        //    return Enum.GetValues(type).Cast<Enum>()
        //        .Select(e => new NameValuePair(e.GetDescription(), e)).ToArray();
        //}

        /// <summary>
        /// Gets the values array.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static Enum[] GetValuesArray(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<Enum>().ToArray();
        }
        /// <summary>
        /// Gets the values array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetValuesArray<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetValues(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<Enum>();
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        /// <summary>
        /// Determines whether [is set flag] [the specified enum type].
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="flag">The flag.</param>
        /// <returns>
        /// 	<c>true</c> if [is set flag] [the specified enum type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSet(this Enum enumType, Enum flag)
        {
            var value = Convert.ToInt64(enumType);
            var flagValue = Convert.ToInt64(flag);
            return (value & flagValue) == flagValue;
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T Parse<T>(string value)
        {
            return Parse<T>(value, true);
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        public static T Parse<T>(string value, bool ignoreCase)
        {
            var type = typeof(T);
            try
            {
                return (T)Enum.Parse(type, value, ignoreCase);
            }
            catch
            {
                return (T)Activator.CreateInstance(type);
            }
        }
    }

}
