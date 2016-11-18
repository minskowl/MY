using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Savchin.Core
{
    /// <summary>
    /// Randomizer
    /// </summary>
    public class Randomizer
    {
        private static readonly Random Rand = new Random();

        /// <summary>
        /// Coulds the get random value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool couldGetRandomValue(Type type)
        {
            try
            {
                GetRandomValueString(type);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetType(Type type)
        {

            //if (typeInfo.IsEnum)
            //{
            //    Array vals = Enum.GetValues(type);
            //    return Convert.ChangeType(vals.GetValue(Rand.Next(0, vals.Length - 1)), type);
            //}

            if (type.Name == "String")
                return GetString(10);
            if (type.Name == "Boolean")
                return GetBoolean();
            if (type.Name == "Int16")
                return GetInt16();
            if (type.Name == "Int32")
                return GetInt32();
            if (type.Name == "Integer")
                return GetInteger();
            if (type.Name == "Single")
                return GetInteger();
            if (type.Name == "Byte")
                return GetByte();
            if (type.Name == "DateTime")
                return GetDate();
            if (type.Name == "Long")
                return GetLong();

            //else if (type.IsClass)
            //{
            //    return getConstructorCall(type);
            //}
            //else
            //{
            throw new NotImplementedException("getRandomValue: not Implemented for " + type.Name);
            // }

        }
        /// <summary>
        /// Gets the random value string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetRandomValueString(Type type)
        {
            if (type.Name == "String")
            {
                return "\"" + GetString(10) + "\"";
            }
            if (type.Name == "Boolean")
            {
                return GetBoolean().ToString().ToLower();
            }
            if (type.Name == "Int16")
            {
                return GetInt16().ToString();
            }
            if (type.Name == "Int32")
            {
                return GetInt32().ToString();
            }
            if (type.Name == "Integer")
            {
                return GetInteger().ToString();
            }
            if (type.Name == "Single")
            {
                return GetInteger().ToString();
            }
            if (type.Name == "Byte")
            {
                return GetByte().ToString();
            }
            if (type.Name == "DateTime")
            {
                var d = GetDate();
                return "new DateTime( " + d.Year + "," + d.Month + "," + d.Day + " )";
            }
            if (type.Name == "Long")
            {
                return GetLong().ToString();
            }

            //if (type.IsEnum)
            //{
            //    string[] vals = Enum.GetNames(type);
            //    return type.FullName + "." + vals[Rand.Next(0, vals.Length - 1)];
            //}
            //else if (type.IsClass)
            //{
            //    return getConstructorCall(type);
            //}
            //else
            //{
            throw new NotImplementedException("getRandomValue: not Implemented for " + type.Name);
            //}

        }

        /// <summary>
        /// Gets the constructor call.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string getConstructorCall(TypeInfo type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new Exception("Type without constructor");
            }
            return " new " + type.FullName + GetParamList(constructors[0].GetParameters());
        }

        /// <summary>
        /// Gets the param list.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string GetParamList(ParameterInfo[] parameters)
        {
            string res = "";
            foreach (ParameterInfo para in parameters)
            {
                res += "," + GetType(para.ParameterType);
            }
            if (res.Length > 0)
            {
                res = res.Substring(1);
            }
            return "( " + res + " )";
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="yearFrom">The year from.</param>
        /// <param name="yearTo">The year to.</param>
        /// <returns></returns>
        public static DateTime GetDate(int yearFrom, int yearTo)
        {
            Again:
            try
            {
                var year = Rand.Next(yearFrom, yearTo);
                int month = Rand.Next(1, 12);
                int day = Rand.Next(1, DateTime.DaysInMonth(year, month));
                return new DateTime(year, month, day);
            }
            catch
            {
                goto Again;
            }
        }
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDate()
        {
            return GetDate(1910, 2056);
        }
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static DateTime GetDate(IList<DateTime> values)
        {
            return values[Rand.Next(0, values.Count - 1)];
        }

        /// <summary>
        /// Gets the long.
        /// </summary>
        /// <returns></returns>
        public static long GetLong()
        {
            long res;
            Again:
            try
            {
                res = ((long)((Rand.NextDouble() * 1000000000000000000)));
            }
            catch (Exception)
            {
                res = -1;
            }
            if (res == -1)
            {
                goto Again;
            }
            return res;
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <returns></returns>
        public static float GetSingle()
        {
            return Convert.ToSingle(GetInteger());
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <returns></returns>
        public static byte GetByte()
        {
            return (byte)Rand.Next(byte.MinValue, byte.MaxValue);
        }

        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <returns></returns>
        public static int GetInteger()
        {
            return Rand.Next(0, int.MaxValue);
        }

        /// <summary>
        /// Gets the integer between.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static int GetIntegerBetween(int start, int end)
        {
            return Rand.Next(start, end);
        }

        /// <summary>
        /// Gets the integer in range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static int GetInteger(Core.Range<int> range)
        {
            return Rand.Next(range.From, range.To);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <returns></returns>
        public static Int32 GetInt32()
        {
            return Convert.ToInt32(Rand.Next(0, Int32.MaxValue));
        }

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <returns></returns>
        public static Int16 GetInt16()
        {
            return Convert.ToInt16(Rand.Next(0, Int16.MaxValue));
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <returns></returns>
        public static bool GetBoolean()
        {
            return Rand.Next(0, 2) == 1;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static string GetString(IList<String> values)
        {
            return values[Rand.Next(0, values.Count - 1)];
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GetString(int length)
        {
            var res = new StringBuilder();
            var bA = Convert.ToByte('a');
            var bZ = Convert.ToByte('z');
            for (int i = 0; i <= length - 1; i++)
            {
                res.Append(Convert.ToChar(Rand.Next(bA, bZ)));
            }
            return res.ToString();
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static Stream GetStream(int length)
        {
            var res = new MemoryStream(length);
            for (var i = 0; i < length; i++)
                res.WriteByte(GetByte());
            res.Position = 0;
            return res;
        }



        #region GetFromArray
        /// <summary>
        /// Exctracts from array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public static T ExctractFromArray<T>(IList<T> ar)
        {
            if (ar == null || ar.Count == 0)
                return default(T);
            var index = GetIntegerBetween(0, ar.Count);
            var result = ar[index];
            ar.RemoveAt(index);
            return result;

        }

        /// <summary>
        /// Gets from array.
        /// </summary>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public static object GetFromArray(object[] ar)
        {
            return ar.Length == 0 ? null : ar[GetIntegerBetween(0, ar.Length)];
        }

        /// <summary>
        /// Gets the specified ar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public static T GetFromArray<T>(T[] ar)
        {
            return ar.Length == 0 ? default(T) : ar[GetIntegerBetween(0, ar.Length)];
        }

        /// <summary>
        /// Gets from array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ar">The ar.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static T[] GetFromArray<T>(T[] ar, int count)
        {
            if (count > ar.Length) throw new ArgumentException("Count sequence grater than size of array.", "count");
            var tmp = new List<T>(ar);
            var result = new T[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = GetFromArray<T>(tmp);
                tmp.Remove(result[i]);

            }
            return result;
        }


        /// <summary>
        /// Gets the specified ar.
        /// </summary>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public static object GetFromArray(Array ar)
        {
            return ar == null || ar.Length == 0 ? null :
                ar.GetValue(GetIntegerBetween(0, ar.Length));
        }
        /// <summary>
        /// Gets the specified ar.
        /// </summary>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public static object GetFromArray(IList ar)
        {
            return ar == null || ar.Count == 0 ? null :
                ar[GetIntegerBetween(0, ar.Count)];
        }

        /// <summary>
        /// Gets from array.
        /// </summary>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public static T GetFromArray<T>(IList<T> ar)
        {
            return ar == null || ar.Count == 0 ? default(T) :
                ar[GetIntegerBetween(0, ar.Count)];
        }
        #endregion
    }
}
