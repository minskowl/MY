using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Core
{
    public class Identifier
    {
        /// <summary>
        /// Determines whether the specified id is valid.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(int id)
        {
            return id > 0;
        }
        /// <summary>
        /// Determines whether the specified id is valid.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(int? id)
        {
            return id.HasValue &&  id.Value > 0;
        }

        /// <summary>
        /// Determines whether the specified id is valid.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(long id)
        {
            return id > 0;
        }

        /// <summary>
        /// Determines whether the specified id is valid.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(Guid id)
        {
            return id != Guid.Empty;
        }
    }
}