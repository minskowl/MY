using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace Advertiser.Controllers
{
    class IrrController : AdvControllerBase
    {
        #region Overrides of AdvControllerBase

        public override string MainUrl
        {
            get { return "http://irr.by/"; }
        }

        #endregion

        public override bool DoLogin(Entities.Login login)
        {
            base.DoLogin(login);

            //var link = Browser.Link(Find.ByUrl("/userarea/?detect=1"));

            Browser.GoTo("http://irr.by/login/");
            var form = Browser.Div(Find.ByClass("auth-form"));

            form.SetText("login", login.User);
            form.SetText("password", login.Password);
            form.Div(Find.ByClass("btn-a")).Buttons[0].Click();
            return true;
        }

        public override void Post(Entities.Wheels adv)
        {
            base.Post(adv);

            Browser.GoTo("http://irr.by/add/step2/?category=cars/parts/whell/");

            var form = Browser.Div(Find.ByClass("middle cfix"));

            if (adv.Price.HasValue)
                form.SetText("ad_price", adv.Price.Value.ToString());

            var subject = adv.Subject;
            form.SetText("ad_title", subject);

            var builder = new StringBuilder(subject);
            builder.AppendLine(adv.Description);

            foreach(var phone in GetPhones(adv))
            {
                builder.AppendLine(phone.ToString());
            }

            form.SetText("ad_text", builder.ToString());

            form.CheckBox("user_agree").Checked = true;

            var elUpload = form.FileUpload("input-file-upload");
            foreach (var f in adv.Images)
            {
                SetUploadFile(f, elUpload);
                form.Button("photoUploadButton").Click();
            }

            form.Button("modify-submit-button").Click();

        }
    }
}
