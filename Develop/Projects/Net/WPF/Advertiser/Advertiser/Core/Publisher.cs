using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using Advertiser.Controllers;
using Advertiser.Entities;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace Advertiser.Core
{
    class Publisher
    {
        private Wheels[] _items;
        private Login[] _accounts;
        private Dictionary<int, Phone> _phones;

        private readonly ILogWriter _logWriter;

        public Publisher(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        /// <summary>
        /// Publishes the specified accounts.
        /// </summary>
        /// <param name="accounts">The accounts.</param>
        /// <param name="items">The items.</param>
        /// <param name="phones">The phones.</param>
        public void Publish(Login[] accounts, Wheels[] items, Dictionary<int, Phone> phones)
        {
            _items = items;
            _accounts = accounts;
            _phones = phones;

            DoAsync(DoPublish);

        }

        /// <summary>
        /// Does the async.
        /// </summary>
        /// <param name="t">The t.</param>
        public static void DoAsync(ThreadStart t)
        {
            var thread = new Thread(t);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void DoPublish()
        {
           // var set = Properties.Settings.Default;
            //using (new BrowserOptions(set.LoadImages, set.LoadExtensions))
            using (var browser = new IE())
            {
                browser.DialogWatcher.CloseUnhandledDialogs = false;
               // browser.ClearCache();
                browser.AutoClose = false;


                foreach (var login in _accounts)
                {
                    var controller = AdvControllerBase.CreateController(login);
                    if (controller == null) continue;

                    using (controller)
                    {
                        controller.Browser = browser;
                        controller.LogWriter = _logWriter;
                        controller.Phones = _phones;

                        Publish(controller, login);
                    }
                }

            }



        }

        private void Publish(IAdvController controller, Login login)
        {


            try
            {
                if(!controller.DoLogin(login))
                    return;
            }
            catch (Exception ex)
            {
                _logWriter.LogAction("Ошибка входа на сайт " + Environment.NewLine + ex);
                return;
            }

            foreach (var adv in _items)
            {
                try
                {
                    controller.Post(adv);
                }
                catch (Exception ex)
                {
                    _logWriter.LogAction("Ошибка публикации " + Environment.NewLine + ex);
                }
            }
            _logWriter.LogAction(string.Format("Публикация на {0} закончена.", controller.MainUrl));
            SystemSounds.Asterisk.Play();
        }





    }
}
