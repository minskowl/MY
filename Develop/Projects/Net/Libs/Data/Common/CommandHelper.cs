using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Savchin.TimeManagment;

namespace Savchin.Data.Common
{
    public static class CommandHelper
    {
        /// <summary>
        /// Adds the input parameter.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="range">The range.</param>
        public static void AddInputParameter(this IDbCommand cmd, string paramName, DateRange range)
        {
            cmd.AddParameter( paramName + "From", DbType.DateTime, range.From, ParameterDirection.Input);
            cmd.AddParameter( paramName + "To", DbType.DateTime, range.To, ParameterDirection.Input);
        }

        /// <summary>
        /// Adds the command parameter.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns></returns>
        public static IDbDataParameter AddInputParameter(this IDbCommand cmd, string paramName, DbType dbType, object paramValue)
        {
            return cmd.AddParameter(paramName, dbType, paramValue, ParameterDirection.Input);
        }
        /// <summary>
        /// Adds the input parameter.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns></returns>
        public static IDbDataParameter AddInputParameter(this IDbCommand cmd, string paramName, object paramValue)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = (paramValue == null || paramValue is DBNull)
                                  ? DBNull.Value
                                  : paramValue;

            cmd.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Adds the parameter to command.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="paramValue">The param value.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static IDbDataParameter AddParameter(this IDbCommand cmd, string paramName,
     DbType dbType, object paramValue, ParameterDirection direction)
        {

            IDbDataParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Direction = direction;
            parameter.DbType = dbType;

            if (paramValue == null || paramValue is DBNull)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                switch (dbType)
                {

                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                        parameter.Value = paramValue;
                        break;
                    case DbType.Int16:
                        parameter.Value = (short)paramValue;
                        break;
                    case DbType.Int32:
                        parameter.Value =
                            (int)paramValue == int.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.Int64:
                        parameter.Value =
                            (long)paramValue == long.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.Boolean:
                        parameter.Value = (bool)paramValue;
                        break;
                    case DbType.Byte:
                        parameter.Value = (byte)paramValue;
                        break;
                    case DbType.Currency:
                        parameter.Value = (decimal)paramValue;
                        break;
                    case DbType.Date:
                        parameter.Value =
                            (DateTime)paramValue == DateTime.MinValue.Date ? DBNull.Value : paramValue;
                        break;
                    case DbType.DateTime:
                        DateTime date = (DateTime)paramValue;
                        if (date == DateTime.MinValue)
                            parameter.Value = DBNull.Value;
                        else if (date.Year < 1900)
                        {
                            parameter.Value = date.AddYears(1900 - date.Year);
                        }
                        else
                            parameter.Value = date;
                        break;
                    case DbType.Decimal:
                        parameter.Value = (decimal)paramValue;
                        break;
                    case DbType.Double:
                        parameter.Value =
                            (double)paramValue == double.MinValue ? DBNull.Value : paramValue;
                        break;

                    case DbType.Object:
                        parameter.Value = paramValue;
                        break;
                    case DbType.SByte:
                        parameter.Value =
                            (sbyte)paramValue == sbyte.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.Single:
                        parameter.Value =
                            (float)paramValue == float.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.Time:
                        parameter.Value =
                            (TimeSpan)paramValue == TimeSpan.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.UInt16:
                        parameter.Value =
                            (ushort)paramValue == ushort.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.UInt32:
                        parameter.Value =
                            (uint)paramValue == uint.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.UInt64:
                        parameter.Value =
                            (ulong)paramValue == ulong.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.VarNumeric:
                        parameter.Value =
                            (decimal)paramValue == decimal.MinValue ? DBNull.Value : paramValue;
                        break;
                    case DbType.Binary:
                        parameter.Size = ((byte[])paramValue).Length;
                        parameter.Value = paramValue;
                        break;
                    case DbType.Guid:
                        parameter.Value =
                            (Guid)paramValue == Guid.Empty ? DBNull.Value : paramValue;
                        break;
                    default:
                        parameter.Value = DBNull.Value;
                        break;
                }
            }
         
            cmd.Parameters.Add(parameter);
            return parameter;
        }
    }
}
