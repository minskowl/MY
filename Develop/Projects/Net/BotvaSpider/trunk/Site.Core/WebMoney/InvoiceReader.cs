using System;
using System.Linq;
using System.Net;
using System.Net.Security;
using Savchin.Data;
using Savchin.TimeManagment;
using Site.Bl;
using WebMoney.XmlInterfaces;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Responses;
using System.Configuration;

namespace Site.WebMoney
{
    class InvoiceReader
    {

        private Purse _purse;

        TransferManager _manager;
  

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceReader"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public InvoiceReader(TransferManager manager)
        {
            _purse = Purse.Parse(ConfigurationManager.AppSettings["WebMoneyReaderPurse"]);
            _manager = manager;
        }



        /// <summary>
        /// Reads the specified start.
        /// </summary>
        /// <param name="range">The range.</param>
        public void Read(DateRange range)
        {
            TransferFilter transferFilter = new TransferFilter(_purse, range.From, range.To);
            var transactions = transferFilter.Submit();

            if (transactions == null || transactions.Length == 0) return;
            //Incomment transfers
            transactions = transactions.Where(e => e.TargetPurse == _purse).OrderBy(e => e.CreateTime).ToArray();

            var last = _manager.GetLast();
            if (last.Count > 0)
                transactions = transactions.Where(e => e.CreateTime > last[0].Date).ToArray();

            foreach (var transfer in transactions)
            {
                Store(transfer);
            }
        }

        private void Store(ExistentTransfer transfer)
        {

            var res = new Transfer
                          {
                              Ammount = transfer.Amount,
                              Purse = transfer.SourcePurse.ToString(),
                              Date = transfer.CreateTime
                          };
            if (transfer.InvoiceId > 0)
                res.InvoiceId = ((int)transfer.InvoiceId);
            if (transfer.TransferId > 0)
                res.TransferId = ((int)transfer.TransferId);
            if (!string.IsNullOrEmpty(transfer.Description))
                res.Description = transfer.Description;

            try
            {
                _manager.Save(res);
            }
            catch (NotUniqueException)
            {
            }
        }
    }
}
