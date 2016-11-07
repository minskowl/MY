using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Savchin.Wpf.Converters
{
    public static class ArgumentHelperExtensions
    {
        [DebuggerHidden]
        public static void AssertNotNull<T>(this T arg, string argName) where T : class
        {
            ArgumentHelper.AssertNotNull<T>(arg, argName);
        }

        [DebuggerHidden]
        public static void AssertNotNull<T>(this T? arg, string argName) where T : struct
        {
            ArgumentHelper.AssertNotNull<T>(arg, argName);
        }

        [DebuggerHidden]
        public static void AssertGenericArgumentNotNull<T>(this T arg, string argName)
        {
            ArgumentHelper.AssertGenericArgumentNotNull<T>(arg, argName);
        }

        [DebuggerHidden]
        public static void AssertNotNull<T>(this IEnumerable<T> arg, string argName, bool assertContentsNotNull)
        {
            ArgumentHelper.AssertNotNull<T>(arg, argName, assertContentsNotNull);
        }

        [DebuggerHidden]
        public static void AssertNotNullOrEmpty(this string arg, string argName)
        {
            ArgumentHelper.AssertNotNullOrEmpty(arg, argName);
        }

        [DebuggerHidden]
        public static void AssertNotNullOrEmpty(this string arg, string argName, bool trim)
        {
            ArgumentHelper.AssertNotNullOrEmpty(arg, argName, trim);
        }

        [DebuggerHidden]
        //[CLSCompliant(false)]
        public static void AssertEnumMember<TEnum>(this TEnum enumValue, string argName) where TEnum : struct, IConvertible
        {
            ArgumentHelper.AssertEnumMember<TEnum>(enumValue, argName);
        }

        [DebuggerHidden]
        //[CLSCompliant(false)]
        public static void AssertEnumMember<TEnum>(this TEnum enumValue, string argName, params TEnum[] validValues) where TEnum : struct, IConvertible
        {
            ArgumentHelper.AssertEnumMember<TEnum>(enumValue, argName, validValues);
        }
    }
}
