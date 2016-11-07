using System.Collections;
using System.Collections.Generic;
namespace Savchin.Data.Common
{
    public interface IArrayParameter
    {
        /// <summary>
        /// Sends the specified _connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        void Send(DBConnection connection, IEnumerable parameters);

        /// <summary>
        /// Sends the specified connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        void Send<T>(DBConnection connection, IEnumerable<T> parameters);
    }
}