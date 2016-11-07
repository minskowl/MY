using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Savchin.Collection.Generic;
using Savchin.Core;

namespace Savchin.Text
{
    /// <summary>
    /// Summary description for TextHelper.
    /// </summary>
    public static class StringUtil
    {
        private static Dictionary<char, string> trans = new Dictionary<char, string>();
        /// <summary>
        /// Initializes the <see cref="StringUtil"/> class.
        /// </summary>
        static StringUtil()
        {
            trans.Add('à', "a");
            trans.Add('á', "b");
            trans.Add('â', "v");
            trans.Add('ã', "g");
            trans.Add('ä', "d");
            trans.Add('å', "e");
            trans.Add('¸', "e");
            trans.Add('æ', "zh");
            trans.Add('ç', "z");
            trans.Add('è', "i");
            trans.Add('é', "j");
            trans.Add('ê', "k");
            trans.Add('ë', "l");
            trans.Add('ì', "m");
            trans.Add('í', "n");
            trans.Add('î', "o");
            trans.Add('ï', "p");
            trans.Add('ð', "r");
            trans.Add('ñ', "s");
            trans.Add('ò', "t");
            trans.Add('ó', "u");
            trans.Add('ô', "f");
            trans.Add('õ', "h");
            trans.Add('ö', "c");
            trans.Add('÷', "ch");
            trans.Add('ø', "shh");
            trans.Add('ù', "sth");
            trans.Add('û', "y");
            trans.Add('þ', "yu");
            trans.Add('ÿ', "ya");
            trans.Add('ü', "'");
            trans.Add('ú', "y");
            trans.Add('ý', "E'");

            trans.Add('À', "A");
            trans.Add('Á', "B");
            trans.Add('Â', "V");
            trans.Add('Ã', "G");
            trans.Add('Ä', "D");
            trans.Add('Å', "E");
            trans.Add('¨', "E");
            trans.Add('Æ', "ZH");
            trans.Add('Ç', "Z");
            trans.Add('È', "I");
            trans.Add('É', "J");
            trans.Add('Ê', "K");
            trans.Add('Ë', "L");
            trans.Add('Ì', "M");
            trans.Add('Í', "N");
            trans.Add('Î', "O");
            trans.Add('Ï', "P");
            trans.Add('Ð', "R");
            trans.Add('Ñ', "S");
            trans.Add('Ò', "T");
            trans.Add('Ó', "U");
            trans.Add('Ô', "F");
            trans.Add('Õ', "H");
            trans.Add('Ö', "C");
            trans.Add('×', "CH");
            trans.Add('Ø', "SHH");
            trans.Add('Ù', "STH");
            trans.Add('Û', "Y");
            trans.Add('Þ', "YU");
            trans.Add('ß', "YA");
            trans.Add('Ü', "'");
            trans.Add('Ú', "Y");
            trans.Add('Ý', "E'");
        }

        /// <summary>
        /// Gets the string hash code.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        //public static unsafe int GetStringHashCode(string s)
        //{

        //    fixed (char* str = ((char*)s))
        //    {
        //        char* chPtr = str;
        //        int num = 0x15051505;
        //        int num2 = num;
        //       int* numPtr = (int*)chPtr;
        //        for (int i = s.Length; i > 0; i -= 4)
        //        {
        //            num = (((num << 5) + num) + (num >> 0x1b)) ^ numPtr[0];
        //            if (i <= 2)
        //            {
        //                break;
        //            }
        //            num2 = (((num2 << 5) + num2) + (num2 >> 0x1b)) ^ numPtr[1];
        //            numPtr += 2;
        //        }
        //        return (num + (num2 * 0x5d588b65));
        //    }
        //}

        public static int IndexOfIgnoreCase(this IEnumerable<string> array, string key)
        {
            return array.FindIndex(e => string.Equals(key, e, StringComparison.OrdinalIgnoreCase));
        }

        public static string Replace(string source, string matcher, string replaceText,
            bool useRegularExp, RegexOptions? options)
        {
            if (useRegularExp)
            {
                var exp = options.HasValue ? new Regex(matcher, options.Value) : new Regex(matcher);
                return exp.Replace(source, replaceText);
            }
            else
            {
                return source.Replace(matcher, replaceText);
            }
        }

        /// <summary>
        /// Clones the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static string Clone(string source, int count)
        {
            var result = new StringBuilder();
            for (int i = 0; i < count; i++)
                result.Append(source);
            return result.ToString();
        }

        public static int CountLines(this string s)
        {
            if (s == null) return 0;
            var count = 1;
            var start = 0;
            while ((start = s.IndexOf('\n', start)) != -1)
            {
                count++;
                start++;
            }
            return count;
        }
        /// <summary>
        /// Joins the specified enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="glue">The glue.</param>
        /// <returns></returns>
        public static string Join(this IEnumerable enumerable, string glue)
        {
            var result = new StringBuilder();
            if (enumerable == null)
                return result.ToString();

            foreach (object o in enumerable)
            {
                result = result.Append(o + glue);
            }

            if (result.Length > 0)
                result.Length = result.Length - glue.Length;
            return result.ToString();
        }

        /// <summary>
        /// Joins the format.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static string JoinFormat(this IEnumerable enumerable, string format)
        {
            var result = new StringBuilder();
            if (enumerable == null)
                return result.ToString();

            foreach (object o in enumerable)
            {
                result = result.AppendFormat(format, o);
            }
            return result.ToString();
        }

        /// <summary>
        /// Joins the format ex.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="format">The format.</param>
        /// <param name="glue">The glue.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string JoinFormatEx(this IEnumerable enumerable, string format, string glue, params string[] args)
        {
            if (enumerable == null || args == null || args.Length == 0) return null;

            Type enumType = null;

            var result = new StringBuilder();
            var listProperties = new List<MemberInfo>();

            foreach (var obj in enumerable)
            {
                if (enumType == null)
                {
                    enumType = obj.GetType();
                    foreach (var property in args)
                    {
                        var propertyInfo = enumType.GetProperty(property);
                        if (propertyInfo != null)
                        {
                            listProperties.Add(propertyInfo);
                        }
                        else
                        {
                            var fieldInfo = enumType.GetField(property);
                            if (fieldInfo == null)
                                return null;
                            listProperties.Add(fieldInfo);
                        }
                    }


                }
                var values = listProperties.Select(e => e.GetValue(obj)).ToArray();
                result.Append(string.Format(format, values) + glue);

            }
            return result.Length == 0 ? string.Empty :
                result.ToString().Substring(0, result.Length - glue.Length);

        }
        /// <summary>
        /// Joins the specified enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="glue">The glue.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static string Join(this IEnumerable enumerable, string glue, string property)
        {

            if (enumerable == null) return null;

            Type enumType = null;
            PropertyInfo propertyInfo = null;
            FieldInfo fieldInfo = null;
            var result = new StringBuilder();
            foreach (object o in enumerable)
            {
                if (enumType == null)
                {
                    enumType = o.GetType();
                    propertyInfo = enumType.GetProperty(property);
                    if (propertyInfo == null)
                        fieldInfo = enumType.GetField(property);
                    if (propertyInfo == null && fieldInfo == null)
                        return null;
                }
                if (propertyInfo != null)
                    result.Append(propertyInfo.GetValue(o, null) + glue);
                else
                    result.Append(fieldInfo.GetValue(o) + glue);
            }
            return result.Length == 0 ? string.Empty :
                result.ToString().Substring(0, result.Length - glue.Length);

        }

        /// <summary>
        /// Strings the array equals.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static bool StringArrayEquals(string[] a, string[] b)
        {
            if ((a == null) != (b == null))
            {
                return false;
            }
            if (a != null)
            {
                int num1 = a.Length;
                if (num1 != b.Length)
                {
                    return false;
                }
                for (int num2 = 0; num2 < num1; num2++)
                {
                    if (a[num2] != b[num2])
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        /// <summary>
        /// Objects the array to string array.
        /// </summary>
        /// <param name="objectArray">The object array.</param>
        /// <returns></returns>
        public static string[] ObjectArrayToStringArray(object[] objectArray)
        {
            string[] textArray1 = new string[objectArray.Length];
            objectArray.CopyTo(textArray1, 0);
            return textArray1;
        }



        /// <summary>
        /// Removes the final char.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string RemoveFinalChar(this string source)
        {
            return source.Length > 1 ? source.Substring(0, source.Length - 1) : source;
        }

        /// <summary>
        /// Removes the final comma.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string RemoveFinalComma(string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            int c = source.LastIndexOf(",");
            if (c == -1)
                return source;

            return source.Substring(0, c);

        }

        /// <summary>
        /// Removes the spaces.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string RemoveSpaces(string source)
        {
            return source.Trim().Replace(" ", string.Empty);
        }

        /// <summary>
        /// Removes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="chars">The chars.</param>
        /// <returns></returns>
        public static string Remove(string source, char[] chars)
        {
            return Remove(source, new List<char>(chars));
        }

        /// <summary>
        /// Removes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="chars">The chars.</param>
        /// <returns></returns>
        public static string Remove(string source, List<char> chars)
        {
            char[] result = new char[source.Length];

            int i = 0;
            foreach (char ch in source)
            {
                if (!chars.Contains(ch))
                {
                    result[i] = ch;
                    i++;
                }
            }
            return new string(result, 0, i);

        }
        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static string ToString(Object o)
        {
            StringBuilder builder = new StringBuilder();
            Type t = o.GetType();



            builder.Append("Properties for: " + t.Name + Environment.NewLine);

            PropertyInfo[] propertyInfos = t.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {

                builder.Append("\t" + propertyInfo.Name + "(" + propertyInfo.PropertyType + "): ");
                if (propertyInfo.GetIndexParameters().Length == 0)
                {

                    try
                    {
                        object value = propertyInfo.GetValue(o, null);
                        if (null != value)
                            builder.Append(value.ToString());
                    }
                    catch { }


                }



                builder.Append(Environment.NewLine);

            }

            FieldInfo[] fieldInfos = t.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                builder.Append("\t" + fieldInfo.Name + "(" + fieldInfo.FieldType + "): ");
                try
                {
                    object value = fieldInfo.GetValue(o);
                    if (null != value)
                        builder.Append(value.ToString());
                }
                catch { }



                builder.Append(Environment.NewLine);

            }

            return builder.ToString();
        }

        /// <summary>
        /// Extracts the content of the inner.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static ArrayList ExtractInnerContent(string content, string start, string end)
        {
            int sindex, eindex, msindex, span;

            ArrayList list = new ArrayList();

            sindex = content.IndexOf(start);
            msindex = sindex + start.Length;

            eindex = content.IndexOf(end, msindex);

            span = eindex - msindex;

            if (sindex >= 0 && eindex > sindex)
            {
                list.Add(content.Substring(msindex, span));
            }

            while (sindex >= 0 && eindex > 0)
            {
                sindex = content.IndexOf(start, eindex);
                if (sindex > 0)
                {
                    eindex = content.IndexOf(end, sindex);
                    msindex = sindex + start.Length;

                    span = eindex - msindex;

                    if (msindex > 0 && eindex > 0)
                    {
                        list.Add(content.Substring(msindex, span));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Extracts the content of the outer.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static ArrayList ExtractOuterContent(string content, string start, string end)
        {
            int sindex, eindex;

            ArrayList al = new ArrayList();

            sindex = content.IndexOf(start);
            eindex = content.IndexOf(end);

            if (sindex >= 0 && eindex > sindex)
            {
                al.Add(content.Substring(sindex, eindex + end.Length - sindex));
            }

            while (sindex >= 0 && eindex > 0)
            {
                sindex = content.IndexOf(start, eindex);

                if (sindex > 0)
                {
                    eindex = content.IndexOf(end, sindex);

                    if (sindex > 0 && eindex > 0)
                    {
                        al.Add(content.Substring(sindex, eindex + end.Length - sindex));
                    }
                }
            }

            return al;
        }

        /// <summary>
        /// Ensures the end with.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lastChar">The last char.</param>
        /// <returns></returns>
        public static string EnsureEndWith(string value, char lastChar)
        {
            if (value == null)
                return value;

            int num1 = value.Length;
            if (num1 > 0 && value[num1 - 1] != lastChar)
            {
                return value + lastChar;
            }

            return value;
        }



        /// <summary>
        /// Convert String To  proper case.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ToProperCase(string s)
        {
            string revised = "";
            if (s.Length > 0)
            {
                if (s.IndexOf(" ") > 0)
                {
                    revised = Strings.StrConv(s, VbStrConv.ProperCase, 1033);
                }
                else
                {
                    string firstLetter = s.Substring(0, 1).ToUpper(new System.Globalization.CultureInfo("en-US"));
                    revised = firstLetter + s.Substring(1, s.Length - 1);
                }
            }
            return revised;
        }

        /// <summary>
        /// Toes the trimmed proper case.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ToTrimmedProperCase(string s)
        {
            return RemoveSpaces(ToProperCase(s));
        }

        /// <summary>
        /// Traslits the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string Traslit(string source)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                char key = source[i];
                if (trans.ContainsKey(key))
                    builder.Append(trans[key]);
                else
                    builder.Append(key);
            }

            return builder.ToString();
        }

        private static Regex regexRusChars = new Regex("[à-ÿ]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        /// <summary>
        /// Determines whether [has russian chars] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// 	<c>true</c> if [has russian chars] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasRussianChars(string source)
        {
            return regexRusChars.IsMatch(source);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static String GetString(Stream stream)
        {
            if (stream.CanSeek && stream.Position != 0)
                stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static Stream GetStream(string source)
        {

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(source);
            writer.Flush();

            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Limits the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Limit(string input, int length)
        {
            return input.Length <= length ? input : input.Substring(0, length);
        }

        /// <summary>
        /// Convers the encoding.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public static byte[] ConverEncoding(string input, Encoding to)
        {
            return ConverEncoding(Encoding.Default.GetBytes(input), Encoding.UTF8, to);
        }
        /// <summary>
        /// Convers the encoding.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public static byte[] ConverEncoding(string input, Encoding from, Encoding to)
        {
            return ConverEncoding(Encoding.Default.GetBytes(input), from, to);
        }
        /// <summary>
        /// Convers the encoding.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public static byte[] ConverEncoding(byte[] input, Encoding from, Encoding to)
        {
            return Encoding.Convert(from, to, input);
        }
    }


}
