using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Advertiser.Entities;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace Advertiser.Controllers
{
    public class OnlinerController : AdvControllerBase
    {
        public override string MainUrl
        {
            get { return "http://www.onliner.by/"; }
        }


        /// <summary>
        /// Does the login.
        /// </summary>
        /// <param name="login">The login.</param>
        public override bool DoLogin(Login login)
        {
            base.DoLogin(login);


            var logginedUser = Browser.ElementWithTag("big", Find.ByClass("onliner__user__name"));
            if (logginedUser.Exists)
            {
                if (logginedUser.Text.Trim() == login.User.Trim())
                    return true;

                DoLogout();
            }

            var form = Browser.GetFormByActionStart("https://profile.onliner.by/login/");

            if (!form.Exists)
            {
                LogWriter.LogAction("Логин форма не найдена");
                return false;
            }

            form.TextField(Find.ByName("username")).Value = login.User;
            form.TextField(Find.ByName("password")).Value = login.Password;
            form.Buttons[0].Click();

            return true;
        }
        public override void DoLogout()
        {
            var link = Browser.Link(Find.ByClass("onliner__user__exit"));
            if (link.Exists)
            {
                link.Click();
                base.DoLogout();
            }
            else
            {
                LogWriter.LogDebug("Ccылка выхода ненайдена");
            }

        }
        public override void DoUp()
        {
            base.DoUp();
            Browser.GoTo("http://baraholka.onliner.by/");
            var navi = Browser.FindElement("ul", Find.ByClass("onav__subnav"));
            var link = navi.Links.FirstOrDefault(
                e => !string.IsNullOrWhiteSpace(e.Url) && e.Url.Contains("/search.php?type=ufleamarket&id="));
            link.Click();

            Browser.Link("select-all-my-adverts").Click();
            Browser.Link(Find.ByClass("btn-up-2-orange")).Click();
        }

        public override void Post(Wheels adv)
        {
            base.Post(adv);

            Browser.GoTo("http://baraholka.onliner.by/posting.php?mode=post&f=213");

            var topic = Browser.Div(Find.ByClass("b-ba-newtopic"));

            var subject = adv.Subject;

            topic.TextField(Find.ByName("subject")).Value = subject;


            var shortDesc = string.Format("{0} {1}", subject, adv.PriceText);

            topic.TextField(Find.ByName("topic_desc")).Value = shortDesc;


            var fieldMessage = topic.TextField(Find.ByName("message"));

            fieldMessage.TypeTextAction.TypeText(BuildMessage(adv, shortDesc));

            if (adv.Price.HasValue)
                topic.TextField(Find.ByName("topic_price")).Value = adv.Price.Value.ToString();



            var elUpload = topic.FileUpload(Find.ByName("file"));
            foreach (var f in adv.Images)
            {
                SetUploadFile(f, elUpload);
                fieldMessage.TypeTextAction.AppendText(Environment.NewLine);
            }

            Browser.BringToFront();
            topic.SetCaptcha("captcha");
        }

        private string BuildMessage(Wheels adv, string shortDesc)
        {
            var builder = new StringBuilder();
            builder.AppendLine(shortDesc);
            if (!string.IsNullOrWhiteSpace(adv.Description))
                builder.AppendLine(adv.Description);

            foreach (var phone in adv.Phones.Select(e => Phones[e]))
            {
                builder.AppendLine(phone.ToString());
            }
            builder.AppendLine(string.Empty);
            return builder.ToString();
        }
    }
}
