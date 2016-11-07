using System.Collections.Generic;
using System.Data;

namespace Savchin.Data.Common
{
    /// <summary>
    /// Class ParameterCollection using for storing DBParametrs and usin in AbstracrDao methods
    /// </summary>
    /// 
    public  class ParameterCollection : List<IDbDataParameter>
    {
        #region Constructors 
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ParameterCollection"/> class.
        /// </summary>
        public ParameterCollection(): base()
        {
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="T:ParameterCollection"/> class.
        ///// </summary>
        ///// <param name="parameterName">Name of the parameter.</param>
        ///// <param name="parameterValue">The parameter value.</param>
        //public ParameterCollection(string parameterName, object parameterValue)
        //    : base()
        //{
        //    AddInput(parameterName, parameterValue);
        //}
        #endregion
        
       
        ///// <summary>
        ///// Adds the input Paramatr.
        ///// </summary>
        ///// <param name="parameterName">The name.</param>
        ///// <param name="parameterValue">The value.</param>
        //public void AddInput(string parameterName, object parameterValue)
        //{
        //    Add(ConnectionHolder.Instance.CreateParameterInput(parameterName, parameterValue));
        //}

        ///// <summary>
        ///// Adds the translated string.
        ///// </summary>
        ///// <param name="parameterName">Name of the parameter.</param>
        ///// <param name="parameterValue">The parameter value.</param>
        //public void AddInputTranslatedString(string parameterName, string parameterValue)
        //{
        //    AddInput("@i_s" + parameterName + "Name", parameterValue);
            
        //    if (parameterValue == null)
        //        AddInput("@i_n" + parameterName + "Flag", 0);
        //    else
        //        AddInput("@i_n" + parameterName + "Flag", 1);
        //}

        ///// <summary>
        ///// Adds the BLOB.
        ///// </summary>
        ///// <param name="parameterName">Name of the parameter.</param>
        ///// <param name="parameterValue">The parameter value.</param>
        //public void AddInputBlob(string parameterName,  byte[]  parameterValue) 
        //{
        //    DbParameter parameter = ConnectionHolder.Instance.CreateParameter();
        //    parameter.DbType = DbType.Binary;
        //    parameter.Direction = ParameterDirection.Input;
        //    parameter.ParameterName = parameterName;
        //    parameter.Value = parameterValue;
        //    parameter.Size = parameterValue.Length;
        //    Add(parameter);
        //}

        ///// <summary>
        ///// Adds the BLOB string.
        ///// </summary>
        ///// <param name="parameterName">Name of the parameter.</param>
        ///// <param name="parameterValue">The parameter value.</param>
        //public void AddInputBlobString(string parameterName, string parameterValue)
        //{
        //    DbParameter parameter = ConnectionHolder.Instance.CreateParameter();
        //    parameter.DbType = DbType.String;
        //    parameter.Direction = ParameterDirection.Input;
        //    parameter.ParameterName = parameterName;
        //    parameter.Value = parameterValue;
            
        //    Add(parameter);
        //}
        ///// <summary>
        ///// Adds the stream.
        ///// </summary>
        ///// <param name="parameterName">Name of the parameter.</param>
        ///// <param name="parameterValue">The parameter value.</param>
        //public void AddInputStream(string parameterName,  Stream parameterValue)
        //{
        //    byte[] buffer = new byte[parameterValue.Length];
        //    parameterValue.Position = 0;
        //    parameterValue.Read(buffer, 0, (int)parameterValue.Length);

        //    AddInputBlob(parameterName,buffer);
        //}        
    }
}
