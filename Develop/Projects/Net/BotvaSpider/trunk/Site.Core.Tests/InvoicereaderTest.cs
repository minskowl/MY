using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Savchin.Development;
using Savchin.TimeManagment;
using Site.Bl;
using Site.WebMoney;

namespace Site.Core.Tests
{
    [TestFixture]
    public class InvoicereaderTest
    {
        [Test]
        public void Test()
        {
            SiteContext.Current.Provider = new SingleThreadProvider();


            var reader = new InvoiceReader(new TransferManager());
            reader.Read(new DateRange(DateTime.Today.AddMonths(-2), DateTime.Today));
        }
    }
}
