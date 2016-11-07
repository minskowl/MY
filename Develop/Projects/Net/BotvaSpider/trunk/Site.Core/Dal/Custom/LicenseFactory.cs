using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Savchin.Data.Common;

namespace Site.Dal.Factories
{
    partial class LicenseFactory
    {
        /// <summary>
        /// Gets the info by user ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public DataTable GetInfoByUserID(System.Int32 UserID)
        {

            return database.ExecuteDataset("_License_GetInfoByUserID", "@UserID", UserID).Tables[0];

        }
    }
}
