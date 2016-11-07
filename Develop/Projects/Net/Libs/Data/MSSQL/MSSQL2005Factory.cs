using Savchin.Data.Common;

namespace Savchin.Data.MSSQL
{
    public class MSSQL2005Factory : MssqlFactory
    {
        /// <summary>
        /// Creates the exception convertor.
        /// </summary>
        /// <returns></returns>
        public override IExceptionConverter CreateExceptionConvertor()
        {
            return  new ExceptionConverter(new MSSQL2005ErrorMessageParser());
        }
    }
}
