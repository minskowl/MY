using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Savchin.Data.Common;

namespace Savchin.Data.MSSQL
{
    /// <summary>
    /// DBConvertor
    /// </summary>
    public class DBConvertor : IDbConvertor
    {
        
        /// <summary>
        /// Toes the name of the DB param.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string ToDbParamName(string name)
        {
            return "@" + name;
        }
        /// <summary>
        /// Convert Object Name to correct DB format.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        public string ToDbObjectName(string objectName)
        {
            return "[" + objectName + "]";
        }
        /// <summary>
        /// Dbs the command to string.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <returns></returns>
        public string DbCommandToString(IDbCommand cmd)
        {
            var stream = new StringBuilder(10000);

            //get the details of SQL

            if (cmd.CommandType == CommandType.StoredProcedure)
                SqlExecToString(cmd, stream);
            else
                SqlToString(cmd, stream);

            return stream.ToString();
        }

        /// <summary>
        /// Converts the type of the net type to db.
        /// </summary>
        /// <param name="netType">Type of the net.</param>
        /// <returns></returns>
        public DbType NetTypeToDbType(Type netType)
        {
            switch (netType.Name)
            {
                case "Byte":
                    return DbType.Byte;
                case "Int16":
                    return DbType.Int16;
                case "Int32":
                    return DbType.Int32;
                case "Int64":
                    return DbType.Int64;
                case "Decimal":
                    return DbType.Decimal;

                case "Boolean":
                    return DbType.Boolean;

                case "String":
                    return DbType.String;
                case "DateTime":
                    return DbType.DateTime;
                case "Guid":
                    return DbType.Guid;
                case "Byte[]":
                    return DbType.Binary;
                case "Object":
                    return DbType.Object;
                default:
                    throw new NotImplementedException("Type not converted: " + netType.Name);
            }

        }
        /// <summary>
        /// Datas the type of the base type to net.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Type DataBaseTypeToNetType(string type)
        {
            switch (type.ToLower())
            {
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "text":
                case "ntext":
                case "xml":
                    return typeof(String);

                case "tinyint":
                    return typeof(Byte);
                case "smallint":
                    return typeof(Int16);
                case "int":
                    return typeof(Int32);
                case "bigint":
                    return typeof(Int64);
                case "decimal":
                    return typeof(Decimal);

                case "bool":
                    return typeof(Boolean);
                case "datetime":
                    return typeof(DateTime);
                case "bit":
                    return typeof(Boolean);
                case "uniqueidentifier":
                    return typeof(Guid);

                case "varbinary":
                case "image":
                    return typeof (byte[]);
                default:
                    throw new NotImplementedException("Type not converted: " + type);
            }
        }
        /// <summary>
        /// Converts the type of the net type to db.
        /// </summary>
        /// <param name="netType">Type of the net.</param>
        /// <returns></returns>
        public string NetTypeToDataBaseType(Type netType)
        {
            switch (netType.Name)
            {
                     
                case "String":
                    return "nvarchar(4000)";
                case "Int64":
                    return "bigint";
                case "Int32":
                    return "int";
                case "Int16":
                    return "smallint";
                case "Byte":
                    return "tinyint";
                case "Boolean":
                    return "bit";
                default:
                    throw new NotImplementedException("Type not converted: " + netType.Name);
            }

        }

        /// <summary>
        /// Datas the parameter type to string.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public string DataParameterTypeToString(IDbDataParameter param)
        {
            int size = param.Size;

            if (size == 0)
                if (param.DbType == DbType.AnsiString ||
                    param.DbType == DbType.String ||
                    param.DbType == DbType.AnsiStringFixedLength ||
                    param.DbType == DbType.StringFixedLength
                    )
                {
                    size = ((string)param.Value).Length;

                    if (size == 0)
                        size = 1;
                }
            switch (param.DbType)
            {
                case DbType.AnsiString:
                    return "varchar(" + size.ToString() + ")";
                case DbType.String:

                    return "nvarchar(" + size.ToString() + ")";

                case DbType.AnsiStringFixedLength:
                    return "char(" + size.ToString() + ")";

                case DbType.StringFixedLength:
                    return "nchar(" + size.ToString() + ")";

                case DbType.Time:
                case DbType.DateTime:
                case DbType.Date:
                    return "datetime";


                case DbType.Binary:
                    return "binary(" + param.Size.ToString() + ")";


                case DbType.Boolean:
                    return "bit";

                case DbType.Byte:
                    return "tinyint";
                case DbType.Int16:
                    return "smallint";
                case DbType.Int32:
                    return "int";
                case DbType.Int64:
                    return "bigint";

                case DbType.Currency:
                    return "money";

                case DbType.Decimal:
                    return "decimal(" + param.Precision.ToString() + ","
                           + param.Scale.ToString() + ")";

                case DbType.Double:
                    return "float";
                case DbType.Single:
                    return "real";

                default:
                    throw new NotImplementedException("Type: " + param.DbType.ToString());
                //case DbType.Guid:
                //    break;

                //case DbType.Object:
                //    break;
                //case DbType.SByte:
                //    break;

                //case DbType.UInt16:
                //    break;
                //case DbType.UInt32:
                //    break;
                //case DbType.UInt64:
                //    break;
                //case DbType.VarNumeric:
                //    break;

                //case DbType.Xml:
                //    break;
            }
        }
        /// <summary>
        /// Datas the parameter value to string.
        /// </summary>
        /// <param name="param">The param.</param>
        public string DataParameterValueToString(IDbDataParameter param)
        {
            if (param.Value == DBNull.Value)
            {
                return "NULL,";
            }

            if (param.DbType == DbType.String || param.DbType == DbType.StringFixedLength ||
        param.DbType == DbType.AnsiString || param.DbType == DbType.AnsiStringFixedLength)
            {
                return "'" + Convert.ToString(param.Value).Replace("\r\n", " ").Replace(";", ",").Replace("'", "''") + "'";
            }

            if (param.DbType == DbType.DateTime || param.DbType == DbType.Date)
            {
                DateTime value = (DateTime)param.Value;
                return "'" + value.Year + "/" + value.Month + "/" + value.Day + "'";
            }
            if (param.DbType == DbType.Boolean)
            {
                return ((Boolean)param.Value) ? "1" : "0";
            }

            //else if (param.DbType == DbType.Int16 || param.DbType == DbType.Int32)
            //{
            //    return param.Value.ToString();
            //}
            return param.Value.ToString();
        }


        #region DbCommant To string

        private void SqlToString(IDbCommand cmd, StringBuilder stream)
        {
            if (cmd.Parameters.Count > 0)
            {
                foreach (IDbDataParameter param in cmd.Parameters)
                {

                    stream.AppendLine("DECLARE " + param.ParameterName + " " + DataParameterTypeToString(param) + ";");
                    stream.AppendLine("SET " + param.ParameterName + "=" + DataParameterValueToString(param) + ";");
                }
            }

            stream.AppendLine(cmd.CommandText);
            stream.AppendLine("go");
        }
        private void SqlExecToString(IDbCommand cmd, StringBuilder stream)
        {
            stream.Append("exec ");
            stream.Append(cmd.CommandText);
            stream.Append(" ");

            if (cmd.Parameters.Count > 0)
            {


                bool hasInputParameters = false;
                foreach (IDbDataParameter param in cmd.Parameters)
                    if (param.Direction != ParameterDirection.ReturnValue)
                    {
                        hasInputParameters = true;

                        stream.Append(param.ParameterName);
                        stream.Append("=" + DataParameterValueToString(param) + ",");
                    }

                if (hasInputParameters)
                    stream = stream.Remove(stream.Length - 1, 1);

                stream.Append(";");
            }
            else
            {
                stream.Append(";");
            }

        }

        #endregion

    }
}
