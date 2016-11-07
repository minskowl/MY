

/******************************************
* Auto-generated by CodeRocket
* 07.11.2009 22:52:52
******************************************/
using System.Collections.Generic;
using System.Data;
using Site.Core;
using Savchin.Data.Common;
using Site.Dal.Entities;

namespace Site.Dal.Factories
{
    /// <summary>
    /// Transfer Factory class
    ///
    //</summary>
    public partial class TransferFactory : FactoryEntityBase<TransferValue>
    {

        int ordinalAmmount;
        int ordinalDate;
        int ordinalDescription;
        int ordinalID;
        int ordinalInvoiceId;
        int ordinalPurse;
        int ordinalTransferId;


        protected override void InitOrdinals(IDataReader reader)
        {
            ordinalAmmount = reader.GetOrdinal("Ammount");
            ordinalDate = reader.GetOrdinal("Date");
            ordinalDescription = reader.GetOrdinal("Description");
            ordinalID = reader.GetOrdinal("ID");
            ordinalInvoiceId = reader.GetOrdinal("InvoiceId");
            ordinalPurse = reader.GetOrdinal("Purse");
            ordinalTransferId = reader.GetOrdinal("TransferId");
        }
        /// <summary>
        /// Maps the IDataReader values to a Transfer object
        //</summary>
        /// <param name="reader">The IDataReader to map</param>
        protected override TransferValue MapObject(IDataReader reader)
        {
            TransferValue result = new TransferValue();
            result.Ammount = reader.GetDecimal(ordinalAmmount);
            result.Date = reader.GetDateTime(ordinalDate);
            result.Description = reader.IsDBNull(ordinalDescription) ? null : reader.GetString(ordinalDescription);
            result.ID = reader.GetInt32(ordinalID);
            result.InvoiceId = reader.IsDBNull(ordinalInvoiceId) ? (System.Int32?)null : reader.GetInt32(ordinalInvoiceId);
            result.Purse = reader.GetString(ordinalPurse);
            result.TransferId = reader.IsDBNull(ordinalTransferId) ? (System.Int32?)null : reader.GetInt32(ordinalTransferId);
            return result;
        }

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public void Insert(TransferValue value)
        {
            IDbCommand command = database.CreateSPCommand("Transfer_Insert");
            command.AddInputParameter("@Ammount", DbType.Decimal, value.Ammount);
            command.AddInputParameter("@Date", DbType.DateTime, value.Date);
            command.AddInputParameter("@Description", DbType.String, value.Description);
            IDbDataParameter IDParameter = command.AddParameter( "@ID", DbType.Int32, null, ParameterDirection.Output);
            command.AddInputParameter("@InvoiceId", DbType.Int32, value.InvoiceId);
            command.AddInputParameter("@Purse", DbType.String, value.Purse);
            command.AddInputParameter("@TransferId", DbType.Int32, value.TransferId);
            database.ExecuteNonQuery(command);

            value.ID = (System.Int32)IDParameter.Value;
        }

        /// <summary>
        /// Updates the specified Transfer.
        /// </summary>
        /// <param name="value">The Transfer value.</param>
        public void Update(TransferValue value)
        {
            IDbCommand command = database.CreateSPCommand("Transfer_Update");
            command.AddInputParameter("@Ammount", DbType.Decimal, value.Ammount);
            command.AddInputParameter("@Date", DbType.DateTime, value.Date);
            command.AddInputParameter("@Description", DbType.String, value.Description);
            command.AddInputParameter("@ID", DbType.Int32, value.ID);
            command.AddInputParameter("@InvoiceId", DbType.Int32, value.InvoiceId);
            command.AddInputParameter("@Purse", DbType.String, value.Purse);
            command.AddInputParameter("@TransferId", DbType.Int32, value.TransferId);
            database.ExecuteNonQuery(command);

        }
        /// <summary>
        /// Gets Transfer by ID.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public TransferValue SelectByID(System.Int32 ID)
        {
            IDbCommand command = database.CreateSPCommand("Transfer_GetByID");
            command.AddInputParameter("@ID", DbType.Int32, ID);
            return SelectSingle(command);
        }

        /// <summary>
        /// Deletes the specified Transfer.
        /// </summary>
        /// <param name="ID">The ID.</param>
        public void Delete(System.Int32 ID)
        {
            IDbCommand command = database.CreateSPCommand("Transfer_Delete");
            command.AddInputParameter("@ID", DbType.Int32, ID);
            database.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Deletes the specified Transfer.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Delete(TransferValue value)
        {
            IDbCommand command = database.CreateSPCommand("Transfer_Delete");
            command.AddInputParameter("@ID", DbType.Int32, value.ID);
            database.ExecuteNonQuery(command);

        }
        /// <summary>
        /// Selects all Transfer values.
        /// </summary>
        /// <returns>List of all Transfer</returns>
        public IList<TransferValue> SelectAll()
        {
            return Select(database.CreateSPCommand("Transfer_GetAll"));
        }

        /// <summary>
        /// Selects the last.
        /// </summary>
        /// <returns></returns>
        public IList<TransferValue> SelectLast()
        {
            return Select(database.CreateSPCommand("Transfer_GetLast"));
        }
    }
}