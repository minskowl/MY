using System;
using System.Data;

namespace Savchin.Data.Common
{
    public interface IDbConvertor
    {

        /// <summary>
        /// Toes the name of the DB param.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string ToDbParamName(string name);
        
        /// <summary>
        /// Convert Object Name to correct DB format.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        string ToDbObjectName(string objectName);
        
        /// <summary>
        /// Dbs the command to string.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <returns></returns>
        string DbCommandToString(IDbCommand cmd);

        /// <summary>
        /// Converts the type of the net type to db.
        /// </summary>
        /// <param name="netType">Type of the net.</param>
        /// <returns></returns>
        DbType NetTypeToDbType(Type netType);

        /// <summary>
        /// Converts the type of the net type to db.
        /// </summary>
        /// <param name="netType">Type of the net.</param>
        /// <returns></returns>
        string NetTypeToDataBaseType(Type netType);

        /// <summary>
        /// Datas the type of the base type to net.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Type DataBaseTypeToNetType(string type);
    }
}