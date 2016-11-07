
using System.Linq;
using System.Collections.Generic;
using Advertiser.Entities;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace Advertiser.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AdvControllerBase : IAdvController
    {


        public IE Browser { get; set; }
        public ILogWriter LogWriter { get; set; }
        public Dictionary<int, Phone> Phones { get; set; }

        public abstract string MainUrl { get; }

        protected bool? ShowImages { get; set; }

        /// <summary>
        /// Creates the controller.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        public static IAdvController CreateController(Login login)
        {
            switch (login.Site)
            {
                case Site.ABW:
                    return new AbwController();
                case Site.Onliner:
                    return new OnlinerController();
                case Site.AV:
                    return new AvController();
                case Site.IRR:
                    return new IrrController();
                case Site.TUTBY:
                    return new TutByController();
                default:
                    return null;
            }
        }

        public void Dispose()
        {
            Browser = null;
            LogWriter = null;
            Phones = null;
        }

        public virtual bool DoLogin(Login login)
        {
            LogWriter.LogAction("Логинимся на " + MainUrl);

            Settings.WaitForCompleteTimeOut = Properties.Settings.Default.WaitForCompleteTimeOut;
            Browser.GoTo(MainUrl);
            return true;
        }

        public virtual void DoLogout()
        {
            LogWriter.LogAction("Вышли из аккаунта");
        }

        public virtual void Clear()
        {
            LogWriter.LogAction("Очищаем аккаунт");
        }

        public virtual void DoUp()
        {
            LogWriter.LogAction("Поднимаем объявы");
        }


        public virtual void Post(Wheels adv)
        {
            Settings.WaitForCompleteTimeOut = Properties.Settings.Default.WaitForCompleteTimeOut;

            var subject = adv.Subject;
            LogWriter.LogAction(string.Format("Публикуем объявление #{0} {1}", adv.Id, subject));
        }

        protected IList<Phone> GetPhones(Advertisement adv)
        {
            return adv.Phones.Where(id => Phones.ContainsKey(id)).Select(id => Phones[id]).ToArray();
        }

        protected void SetUploadFile(string f, Element elUpload)
        {
            elUpload.SetUploadFile(System.IO.Path.GetFullPath(f), Browser.DialogWatcher);
        }

     
    }
}