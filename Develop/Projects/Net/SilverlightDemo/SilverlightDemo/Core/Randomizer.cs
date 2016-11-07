using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace EffectiveSoft.SilverlightDemo.Core
{
    /// <summary>
    /// Randomizer
    /// </summary>
    public class Randomizer
    {
        private static Random rand = new Random();

        /// <summary>
        /// Coulds the get random value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool couldGetRandomValue(Type type)
        {
            try
            {
                getRandomValueString(type);
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
            if (type.Name == "String")
                return GetString(10);
            else if (type.Name == "Boolean")
                return getBoolean();
            else if (type.Name == "Int16")
                return getInt16();
            else if (type.Name == "Int32")
                return getInt32();
            else if (type.Name == "Integer")
                return getInteger();
            else if (type.Name == "Single")
                return getInteger();
            else if (type.Name == "Byte")
                return getByte();
            else if (type.Name == "DateTime")
                return getDate();
            else if (type.Name == "Long")
                return getLong();
            else
            {
                //if (type.IsEnum)
                //{
                //    Array vals = Enum.GetValues(type);
                //    return Convert.ChangeType(vals.GetValue(rand.Next(0, vals.Length - 1)), type);
                //}
                ////else if (type.IsClass)
                ////{
                ////    return getConstructorCall(type);
                ////}
                //else
                //{
                    throw new NotImplementedException("getRandomValue: not Implemented for " + type.Name);
                //}
            }
        }
        /// <summary>
        /// Gets the random value string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string getRandomValueString(Type type)
        {
            if (type.Name == "String")
            {
                return "\"" + GetString(10) + "\"";
            }
            else if (type.Name == "Boolean")
            {
                return getBoolean().ToString().ToLower();
            }
            else if (type.Name == "Int16")
            {
                return getInt16().ToString();
            }
            else if (type.Name == "Int32")
            {
                return getInt32().ToString();
            }
            else if (type.Name == "Integer")
            {
                return getInteger().ToString();
            }
            else if (type.Name == "Single")
            {
                return getInteger().ToString();
            }
            else if (type.Name == "Byte")
            {
                return getByte().ToString();
            }
            else if (type.Name == "DateTime")
            {
                DateTime d = getDate();
                return "new DateTime( " + d.Year.ToString() + "," + d.Month.ToString() + "," + d.Day.ToString() + " )";
            }
            else if (type.Name == "Long")
            {
                return getLong().ToString();
            }
            else
            {
                //if (type.IsEnum)
                //{
                //    string[] vals = Enum.GetNames(type);
                //    return type.FullName + "." + vals[rand.Next(0, vals.Length - 1)];
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
        }

        /// <summary>
        /// Gets the constructor call.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string getConstructorCall(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
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
            int year;
            int month;
            int day;
            DateTime res = new DateTime();
            bool success = false;
        Again:
            try
            {
                year = rand.Next(yearFrom, yearTo);
                month = rand.Next(1, 12);
                day = rand.Next(1, DateTime.DaysInMonth(year, month));
                res = new DateTime(year, month, day);
                success = true;
            }
            catch (Exception)
            {
            }
            if (success == false)
            {
                goto Again;
            }
            return res;
        }
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <returns></returns>
        public static DateTime getDate()
        {
            return GetDate(1910, 2056);
        }
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static DateTime getDate(IList<DateTime> values)
        {
            return values[rand.Next(0, values.Count - 1)];
        }

        /// <summary>
        /// Gets the long.
        /// </summary>
        /// <returns></returns>
        public static long getLong()
        {
            long res;
        Again:
            try
            {
                res = ((long)((rand.NextDouble() * 1000000000000000000)));
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
        public static float getSingle()
        {
            return Convert.ToSingle(getInteger());
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <returns></returns>
        public static byte getByte()
        {

            return (byte)rand.Next(byte.MinValue, byte.MaxValue);
        }

        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <returns></returns>
        public static int getInteger()
        {
            return rand.Next(0, int.MaxValue);
        }

        /// <summary>
        /// Gets the integer between.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static int GetIntegerBetween(int start, int end)
        {
            return rand.Next(start, end);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <returns></returns>
        public static Int32 getInt32()
        {
            return Convert.ToInt32(rand.Next(0, Int32.MaxValue));
        }

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <returns></returns>
        public static Int16 getInt16()
        {
            return Convert.ToInt16(rand.Next(0, Int16.MaxValue));
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <returns></returns>
        public static bool getBoolean()
        {
            return Convert.ToBoolean(rand.Next(0, 1));
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static string getString(IList<String> values)
        {
            return values[rand.Next(0, values.Count - 1)];
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="Length">The length.</param>
        /// <returns></returns>
        public static string GetString(int Length)
        {
            string res = "";
            byte bA = Convert.ToByte('a');
            byte bZ = Convert.ToByte('z');
            for (int i = 0; i <= Length - 1; i++)
            {
                res += Convert.ToChar(rand.Next(bA, bZ));
            }
            return res;
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static Stream GetStream(int length)
        {
            MemoryStream res = new MemoryStream(length);
            res.Position = 0;
            for (int i = 0; i < length; i++)
                res.WriteByte(getByte());
            res.Position = 0;
            return res;
        }

        ///// <summary>
        ///// Gets the data table.
        ///// </summary>
        ///// <param name="table">The table.</param>
        ///// <param name="rowCount">The row count.</param>
        //public static void GetDataTable(DataTable table, int rowCount)
        //{
        //    for(int i=0; i<rowCount; i++)
        //    {
        //        DataRow row = table.Rows.Add();
                
        //        for(int columnIndex =0; columnIndex<table.Columns.Count; columnIndex++ )
        //        {
        //            row[columnIndex] = GetType(table.Columns[columnIndex].DataType);
        //        }
        //    }
        //}
    }
}
