using System.Data.Common;

namespace Savchin.Data.Common
{
    /// <summary>
    /// IExceptionConverter
    /// </summary>
    public interface IExceptionConverter
    {
        /// <summary>
        /// Converts the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        DbException Convert(DbException exception);
    }
}
