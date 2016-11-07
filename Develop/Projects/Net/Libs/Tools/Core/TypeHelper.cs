using System;
using System.Reflection;

namespace Savchin.Core
{
    /// <summary>
    /// TypeHelper
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetDefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// Gets the type reference.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetTypeReference(this Type type)
        {
            return string.Format("{0}, {1}", type.FullName, type.Assembly.GetName().Name);
        }
        /// <summary>
        /// Determines whether the specified value property type is numeric.
        /// </summary>
        /// <param name="type">Type of the value property.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value property type is numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumeric(this Type type)
        {
            return type == typeof(sbyte) || type == typeof(sbyte?) ||
                   type == typeof(byte) || type == typeof(byte?) ||
                   type == typeof(short) || type == typeof(short?) ||
                   type == typeof(ushort) || type == typeof(ushort?) ||
                   type == typeof(int) || type == typeof(int?) ||
                   type == typeof(uint) || type == typeof(uint?) ||
                   type == typeof(long) || type == typeof(long?) ||
                   type == typeof(float) || type == typeof(float?) ||
                   type == typeof(decimal) || type == typeof(decimal?) ||
                   type == typeof(float) || type == typeof(float?) ||
                   type == typeof(double) || type == typeof(double?);
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <param name="inherrits">if set to <c>true</c> [inherrits].</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider type, bool inherrits=false)
            where T : class
        {
            var attr = type.GetCustomAttributes(typeof(T), inherrits);
            if (attr == null || attr.Length == 0) return null;
            return (T)attr[0];
        }
 
        /// <summary>
        /// Determines whether the specified provider has attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider">The provider.</param>
        /// <param name="inherrits">if set to <c>true</c> [inherrits].</param>
        /// <returns>
        ///   <c>true</c> if the specified provider has attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute<T>(this ICustomAttributeProvider provider, bool inherrits)
            where T : class
        {
            return provider.GetCustomAttributes(typeof(T), inherrits).Length > 0;
        }

        /// <summary>
        /// Determines whether the specified provider has attribute.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="inherrits">if set to <c>true</c> [inherrits].</param>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified provider has attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute(this ICustomAttributeProvider provider, bool inherrits, Type type)
        {
            return provider.GetCustomAttributes(type, inherrits).Length > 0;
        }
        /// <summary>
        /// Converts the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object Convert(this Type type, object value)
        {
            if (value == null || value.GetType().Equals(type))
                return value;


            if (type.IsGenericType &&
                type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                return type.GetGenericArguments()[0].Convert(value);
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, value.ToString());
            }

            return global::System.Convert.ChangeType(value, type);
        }

    }
}