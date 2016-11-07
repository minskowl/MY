using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Savchin.Wpf.Converters
{
    public static class ArgumentHelper
    {
        [DebuggerHidden]
        public static void AssertNotNull<T>(T arg, string argName) where T : class
        {
            if ((object)arg == null)
                throw new ArgumentNullException(argName);
        }

        [DebuggerHidden]
        public static void AssertNotNull<T>(T? arg, string argName) where T : struct
        {
            if (!arg.HasValue)
                throw new ArgumentNullException(argName);
        }

        [DebuggerHidden]
        public static void AssertGenericArgumentNotNull<T>(T arg, string argName)
        {
            Type type = typeof(T);
            if (type.IsValueType && (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>)))
                return;
            ArgumentHelper.AssertNotNull<object>((object)arg, argName);
        }

        [DebuggerHidden]
        public static void AssertNotNull<T>(IEnumerable<T> arg, string argName, bool assertContentsNotNull)
        {
            ArgumentHelper.AssertNotNull<IEnumerable<T>>(arg, argName);
            if (!assertContentsNotNull || !typeof(T).IsClass)
                return;
            foreach (T obj in arg)
            {
                if ((object)obj == null)
                    throw new ArgumentException("An item inside the enumeration was null.", argName);
            }
        }

        [DebuggerHidden]
        public static void AssertNotNullOrEmpty(string arg, string argName)
        {
            ArgumentHelper.AssertNotNullOrEmpty(arg, argName, false);
        }

        [DebuggerHidden]
        public static void AssertNotNullOrEmpty(string arg, string argName, bool trim)
        {
            if (string.IsNullOrEmpty(arg) || trim && ArgumentHelper.IsOnlyWhitespace(arg))
                throw new ArgumentException("Cannot be null or empty.", argName);
        }

       // [CLSCompliant(false)]
        [DebuggerHidden]
        public static void AssertEnumMember<TEnum>(TEnum enumValue, string argName) where TEnum : struct, IConvertible
        {
            if (Attribute.IsDefined((MemberInfo)typeof(TEnum), typeof(FlagsAttribute), false))
            {
                long num = enumValue.ToInt64((IFormatProvider)CultureInfo.InvariantCulture);
                bool flag;
                if (num == 0L)
                {
                    flag = !Enum.IsDefined(typeof(TEnum), ((IConvertible)0).ToType(Enum.GetUnderlyingType(typeof(TEnum)), (IFormatProvider)CultureInfo.InvariantCulture));
                }
                else
                {
                    foreach (TEnum @enum in Enum.GetValues(typeof(TEnum)))
                        num &= ~@enum.ToInt64((IFormatProvider)CultureInfo.InvariantCulture);
                    flag = num != 0L;
                }
                if (!flag)
                    return;
                throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Enum value '{0}' is not valid for flags enumeration '{1}'.", new object[2]
        {
          (object) enumValue,
          (object) typeof (TEnum).FullName
        }), argName);
            }
            else
            {
                if (Enum.IsDefined(typeof(TEnum), (object)enumValue))
                    return;
                throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Enum value '{0}' is not defined for enumeration '{1}'.", new object[2]
        {
          (object) enumValue,
          (object) typeof (TEnum).FullName
        }), argName);
            }
        }

        [DebuggerHidden]
        [CLSCompliant(false)]
        public static void AssertEnumMember<TEnum>(TEnum enumValue, string argName, params TEnum[] validValues) where TEnum : struct, IConvertible
        {
            ArgumentHelper.AssertNotNull<TEnum[]>(validValues, "validValues");
            if (Attribute.IsDefined((MemberInfo)typeof(TEnum), typeof(FlagsAttribute), false))
            {
                long num = enumValue.ToInt64((IFormatProvider)CultureInfo.InvariantCulture);
                bool flag;
                if (num == 0L)
                {
                    flag = true;
                    foreach (TEnum @enum in validValues)
                    {
                        if (@enum.ToInt64((IFormatProvider)CultureInfo.InvariantCulture) == 0L)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (TEnum @enum in validValues)
                        num &= ~@enum.ToInt64((IFormatProvider)CultureInfo.InvariantCulture);
                    flag = num != 0L;
                }
                if (!flag)
                    return;
                throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Enum value '{0}' is not allowed for flags enumeration '{1}'.", new object[2]
        {
          (object) enumValue,
          (object) typeof (TEnum).FullName
        }), argName);
            }
            else
            {
                foreach (TEnum @enum in validValues)
                {
                    if (enumValue.Equals((object)@enum))
                        return;
                }
                if (!Enum.IsDefined(typeof(TEnum), (object)enumValue))
                    throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Enum value '{0}' is not defined for enumeration '{1}'.", new object[2]
          {
            (object) enumValue,
            (object) typeof (TEnum).FullName
          }), argName);
                else
                    throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Enum value '{0}' is defined for enumeration '{1}' but it is not permitted in this context.", new object[2]
          {
            (object) enumValue,
            (object) typeof (TEnum).FullName
          }), argName);
            }
        }

        private static bool IsOnlyWhitespace(string arg)
        {
            foreach (char c in arg)
            {
                if (!char.IsWhiteSpace(c))
                    return false;
            }
            return true;
        }
    }
}
