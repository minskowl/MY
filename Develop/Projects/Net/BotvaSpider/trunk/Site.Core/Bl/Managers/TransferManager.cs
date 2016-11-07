using System;
using System.Linq;
using System.Collections.Generic;
using Savchin.Data.Common;
using Site.Core;
using Site.Dal.Entities;
using Site.Dal.Factories;
using Savchin.TimeManagment;
using Site.WebMoney;

namespace Site.Bl
{
    /// <summary>
    /// Transfer Manager class
    ///
    //</summary>
    public class TransferManager : ManagerBase<TransferFactory>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase&lt;T&gt;"/> class.
        /// </summary>
        public TransferManager()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase&lt;TFactory&gt;"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public TransferManager(DBConnection connection)
            : base(connection)
        {

        }
        #region Getters
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        public IList<Transfer> GetAll()
        {
            return Wrap(factory.SelectAll());
        }

        /// <summary>
        /// Gets the last.
        /// </summary>
        /// <returns></returns>
        public IList<Transfer> GetLast()
        {
            return Wrap(factory.SelectLast());
        }

        /// <summary>
        /// Ges the Transfer by ID.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>        
        public Transfer GetByID(System.Int32 ID)
        {
            return Wrap(factory.SelectByID(ID));
        }

        /// <summary>
        /// Selects the by purse by creation date.
        /// </summary>
        /// <param name="purse">The purse.</param>
        /// <param name="creationDate">The creation date.</param>
        /// <returns></returns>
        public IList<Transfer> GetFreeByPurseByCreationDate(string purse, DateRange creationDate)
        {
            return Wrap(factory.SelectFreeByPurseByCreationDate(purse, creationDate));
        }

        #endregion


        /// <summary>
        /// Searches the free trasfer.
        /// </summary>
        /// <param name="purse">The purse.</param>
        /// <param name="creationDate">The creation date.</param>
        /// <returns></returns>
        public Transfer SearchFreeTransfer(string purse, DateRange creationDate)
        {
            var transfers = GetFreeByPurseByCreationDate(purse, creationDate);
            if (transfers.Count > 0) return transfers[0];

            var reader = new InvoiceReader(this);
            reader.Read(creationDate);

            transfers = GetFreeByPurseByCreationDate(purse, creationDate);
            return transfers.Count > 0 ? transfers[0] : null;
        }

        /// <summary>
        /// Saves the specified Transfer.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Save(Transfer entity)
        {
            entity.Validate();
            if (Identifier.IsValid(entity.ID))
            {
                factory.Update(entity.ObjectValue);
            }
            else
            {
                factory.Insert(entity.ObjectValue);
            }
        }


        /// <summary>
        /// Deletes the specified Transfer.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(Transfer entity)
        {
            factory.Delete(entity.ObjectValue);
        }

        /// <summary>
        /// Wraps the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        private IList<Transfer> Wrap(ICollection<TransferValue> values)
        {
            List<Transfer> result = new List<Transfer>(values.Count);
            foreach (TransferValue value in values)
            {
                result.Add(new Transfer(value));
            }
            return result;
        }

        /// <summary>
        /// Wraps the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private Transfer Wrap(TransferValue value)
        {
            return (value == null) ? null : new Transfer(value);
        }
    }
}