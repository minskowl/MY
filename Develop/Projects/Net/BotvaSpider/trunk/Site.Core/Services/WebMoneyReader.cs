using System;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Web;
using Savchin.Data.Common;
using Savchin.TimeManagment;
using Site.Bl;
using Site.Core;
using Site.WebMoney;
using WebMoney.XmlInterfaces.BasicObjects;

namespace Site.Services
{
    public class WebMoneyReader : IHttpModule
    {
        #region Fields

        private Timer _timer;
        private bool firstread = true;
        private int errorCount = 0;

        private int intervalRead;
        #endregion

        #region Implementation of IHttpModule

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidator.RemoteCertificateValidationCallback;

            Log.WebMoneyService.Debug("WebMoneyReader Starting");
            var enabled = ConfigurationManager.AppSettings["WebMoneyReaderEnabled"];
            if (string.IsNullOrEmpty(enabled) || enabled.ToLower() != "true") return;
            intervalRead = int.Parse(ConfigurationManager.AppSettings["WebMoneyReaderInterval"]);

          

            _timer = new Timer(60 * 1000 * intervalRead);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;

            Log.WebMoneyService.Debug("WebMoneyReader started");
        }


        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            //Log.WebMoneyService.Debug("WebMoneyReader Disposing");
            //if (_timer != null)
            //{
            //    _timer.Stop();
            //    _timer.Dispose();
            //    _timer = null;
            //}

            //if (_connection != null)
            //{
            //    _connection.Dispose();
            //    _connection = null;
            //}

            //_reader = null;
            //_manager = null;
            //Log.WebMoneyService.Debug("WebMoneyReader Disposed");
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Handles the Elapsed event of the _timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            var start = firstread ? DateTime.Now.AddDays(-1) : DateTime.Now.AddMinutes(-intervalRead - 1);
            var end = DateTime.Now;
            Log.WebMoneyService.Debug("WebMoneyReader Start read");

            try
            {
                using (DBConnection connection = SiteContext.Current.CreateConnection())
                {
                    new InvoiceReader(new TransferManager(connection)).Read(new DateRange(start, end));
                }
            }
            catch (Exception ex)
            {
                Log.WebMoneyService.Error("WebMoneyReader error", ex);
                errorCount++;
                if (errorCount > 50)
                {
                    Log.WebMoneyService.Fatal("WebMoneyReader stoped. To many errors");
                    return;
                }
            }
            finally
            {
                firstread = false;
                Log.WebMoneyService.Debug("WebMoneyReader End read");
            }
            _timer.Enabled = true;
        }

        #endregion
    }
}
