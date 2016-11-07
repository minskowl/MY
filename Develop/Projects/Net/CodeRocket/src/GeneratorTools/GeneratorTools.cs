using System;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Savchin.Core;
using Savchin.Data.Schema;
using System.Collections.Generic;
using Savchin.Text;


namespace Savchin.CodeGeneration
{
    /// <summary>
    /// Generator Tools
    /// </summary>
    public class GeneratorTools
    {

        /// <summary>
        /// Tests this instance.
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            return "1";
        }

        /// <summary>
        /// Gets the function param list.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <returns></returns>
        public string getFunctionParamList(IEnumerable<ColumnSchema> columns)
        {
            var builder = new StringBuilder();
            foreach (var column in columns)
            {
                builder.AppendFormat(" {0} {1},", column.NetType, column.Alias);
            }
            return builder.ToString().TrimEnd(',');
        }

        public string getSpParamList(IEnumerable<ColumnSchema> columns)
        {
            var builder = new StringBuilder();
            foreach (var column in columns)
            {
                builder.AppendFormat(" @{0} {1},", column.Name, column.DataTypeFull);
            }
            return builder.ToString().TrimEnd(',');
        }

        public string getWhereCriteria(IEnumerable<ColumnSchema> columns)
        {
            //  #set( $whereList="${whereList}[${column.Name}]=@${column.Name} AND ")
            var builder = new StringBuilder();
            foreach (ColumnSchema column in columns)
            {
                builder.AppendFormat(" [{0}]=@{0} AND", column.Name);
            }
            string result = builder.ToString();

            return (result.Length > 0) ? result.Substring(0, result.Length - 3) : string.Empty;
        }
        /// <summary>
        /// Gets the name of the singular.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string getSingularName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            if (name.EndsWith("ies"))
            {
                return name.Substring(0, name.Length - 3) + "y";
            }
            else if (name.EndsWith("s"))
            {
                return name.Substring(0, name.Length - 1);
            }
            else
                return name;
        }

        /// <summary>
        /// Determines whether the specified obj is NULL.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// 	<c>true</c> if the specified obj is NULL; otherwise, <c>false</c>.
        /// </returns>
        public bool isNULL(object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Gets the shortname.
        /// </summary>
        /// <param name="FullName">The full name.</param>
        /// <returns></returns>
        public string getShortname(string FullName)
        {
            string tmp = FullName.Substring(3);
            return tmp.Substring(0, tmp.Length - 6);
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public string getTypeName(object obj)
        {
            var o = ((MemberInfo)(obj));
            string typeString = "";
            if ((o.MemberType == MemberTypes.Property))
            {
                typeString = ((PropertyInfo)(obj)).PropertyType.Name;
            }
            else if ((o.MemberType == MemberTypes.Field))
            {
                typeString = ((FieldInfo)(obj)).FieldType.Name;
            }
            return typeString;
        }

        public static string getNameFromNode(TreeNode node)
        {
            string[] ar = node.FullPath.Split('\\');
            string name = "";
            for (int i = 2; i <= ar.Length - 1; i += 2)
            {
                name += ar[i];
            }
            return name;
        }

        public static string getNameFromNodeSeparate(TreeNode node)
        {
            string[] ar = node.FullPath.Split('\\');
            string name = "";
            for (int i = 2; i <= ar.Length - 1; i += 2)
            {
                name += "." + ar[i];
            }
            return name;
        }

        public static TreeNode getLastCheckedNode(TreeNode node)
        {
            foreach (TreeNode nodeSub in node.Nodes)
            {
                if ((nodeSub.Checked))
                {
                    return getLastCheckedNode(nodeSub);
                }
            }
            return node;
        }

        public static ArrayList getLastCheckedNodes(TreeNode node)
        {
            bool found = false;
            ArrayList res = new ArrayList();
            foreach (TreeNode subNode in node.Nodes)
            {
                if (subNode.Checked)
                {
                    res.AddRange(getLastCheckedNodes(subNode));
                    found = true;
                }
            }
            if (found)
            {
                return res;
            }
            res.Add(node);
            return res;
        }

        public static ArrayList getPropertiesFromInterface(Type inter)
        {
            ArrayList res = new ArrayList();
            if (!(inter.IsInterface))
            {
                return res;
            }
            res.AddRange(inter.GetProperties());
            foreach (Type subInter in inter.GetInterfaces())
            {
                res.AddRange(subInter.GetProperties());
            }
            return res;
        }

        public static ArrayList getMethodsFromInterface(Type inter)
        {
            ArrayList res = new ArrayList();
            if (!(inter.IsInterface))
            {
                return res;
            }
            res.AddRange(inter.GetMethods());
            foreach (Type subInter in inter.GetInterfaces())
            {
                res.AddRange(subInter.GetMethods());
            }
            return res;
        }
        public static string joinCollection(IEnumerable enumerable, string glue, string property)
        {
            return StringUtil.Join(enumerable, glue, property);
        }
        public static bool couldGetRandomValue(Type type)
        {
            return Randomizer.couldGetRandomValue(type);
        }

        #region Randomozer
        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string getRandomValue(Type type)
        {
            return Randomizer.GetRandomValueString(type);
        }

        public static string getRandomConstructorCall(Type type)
        {
            return Randomizer.getConstructorCall(type);
        }

        public static string getRandomParamList(ParameterInfo[] paramsList)
        {
            return Randomizer.GetParamList(paramsList);
        }

        public static DateTime getRandomDateTime()
        {
            return Randomizer.GetDate();
        }

        public static long getRandomLong()
        {
            return Randomizer.GetLong();
        }

        public static float getRandomSingle()
        {
            return Randomizer.GetSingle();
        }

        public static byte getRandomByte()
        {
            return Randomizer.GetByte();
        }

        public static int getRandomInteger()
        {
            return Randomizer.GetInteger();
        }

        public static Int32 getRandomInt32()
        {
            return Randomizer.GetInt32();
        }

        public static Int16 getRandomInt16()
        {
            return Randomizer.GetInt16();
        }

        public static bool getRandomBoolean()
        {
            return Randomizer.GetBoolean();
        }

        public static string getRandomString(int Length)
        {
            return Randomizer.GetString(Length);
        }

        #endregion

        public static string getInterfaceDefaultImp(Type ty, bool withSubType)
        {
            if (ty.IsInterface == false)
            {
                return "";
            }
            string tmp = Environment.NewLine + Environment.NewLine + "// Imp " + ty.Name + Environment.NewLine;
            foreach (MemberInfo mem in ty.GetMembers())
            {
                if (mem.MemberType == MemberTypes.Method)
                {
                    MethodInfo meth = ((MethodInfo)(mem));
                    if (meth.IsSpecialName == false)
                    {
                        tmp += getMethodDefaultImp(meth);
                    }
                }
                else if (mem.MemberType == MemberTypes.Property)
                {
                    PropertyInfo prop = ((PropertyInfo)(mem));
                    tmp += Environment.NewLine + Environment.NewLine + GetPropertyDefaultImp(prop, GeneratorTools.getRandomValue(prop.PropertyType));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            if (withSubType == true)
            {
                foreach (Type subType in ty.GetInterfaces())
                {
                    tmp += getInterfaceDefaultImp(subType, false);
                }
            }
            return tmp;
        }

        public static string GetPropertyDefaultImp(PropertyInfo prop, string DefaultValue)
        {
            string tmp = "private " + prop.PropertyType.FullName + " _" + prop.Name;
            if (!(DefaultValue == null))
            {
                tmp += " = " + DefaultValue;
            }
            tmp += ";" + Environment.NewLine + " public " + prop.PropertyType.FullName + " " + prop.Name + " { " + Environment.NewLine;
            if (prop.CanRead)
            {
                tmp += " get { return _" + prop.Name + "; }" + Environment.NewLine;
            }
            if (prop.CanWrite)
            {
                tmp += " set { _" + prop.Name + " = value; }" + Environment.NewLine;
            }
            return tmp + "}";
        }

        public static string getMethodDefaultImp(MethodInfo meth)
        {
            string paramsList = "";
            foreach (ParameterInfo param in meth.GetParameters())
            {
                paramsList += "," + param.ParameterType.FullName + " " + param.Name;
            }
            if (paramsList.Length > 0)
            {
                paramsList = paramsList.Substring(1);
            }
            return "    public " + meth.ReturnType.FullName + " " + meth.Name + "(" + paramsList + ")" + Environment.NewLine + "{" + Environment.NewLine + "      throw new NotImplementedException();" + Environment.NewLine + "}";
        }

        /// <summary>
        /// Joins the specified enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="glue">The glue.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public string join(IEnumerable enumerable, string glue, string property)
        {
            return enumerable.Join(glue, property);
        }
        /// <summary>
        /// Joins the ex.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="format">The format.</param>
        /// <param name="glue">The glue.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public string joinEx(IEnumerable enumerable, string format, string glue, string property)
        {
            return enumerable.JoinFormatEx(format, glue, property);
        }

        /// <summary>
        /// Cuts the string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string cutString(string input, int length)
        {
            return (string.IsNullOrEmpty(input)) ? string.Empty : input.Substring(0, input.Length - length);
        }


    }
}