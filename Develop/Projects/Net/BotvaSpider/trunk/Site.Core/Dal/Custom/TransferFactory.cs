using System.Collections.Generic;
using Savchin.TimeManagment;
using Savchin.Data.Common;
using System.Data;
using Site.Dal.Entities;

namespace Site.Dal.Factories
{
    partial class TransferFactory
    {
        /// <summary>
        /// Selects the by purse by creation date.
        /// </summary>
        /// <param name="purse">The purse.</param>
        /// <param name="creationDate">The creation date.</param>
        /// <returns></returns>
        public IList<TransferValue> SelectFreeByPurseByCreationDate(string purse, DateRange creationDate)
        {
            var sp = database.CreateSPCommand("_Transfer_GetFreeByPurseByCreationDate");
            sp.AddInputParameter("@Purse", DbType.String, purse);
            sp.AddInputParameter("@Date", creationDate);
            return Select(sp);
        }
    }
}
