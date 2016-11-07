using System.Collections.Generic;
using System.Linq;
using Advertiser.Entities;
using Savchin.Collection.Generic;
using WatiN.Core;

namespace Advertiser.Controllers
{
    class AvController : AdvControllerBase
    {
        public override string MainUrl
        {
            get { return "http://av.by/"; }
        }

        public override bool DoLogin(Login login)
        {
            base.DoLogin(login);

        login:

            var loginForm = Browser.Form(Find.ByName("user_login"));
            if (loginForm.Exists == false)
            {
          
                goto login;
            }
            loginForm.TextField(Find.ByName("login")).Value = login.User;
            loginForm.TextField(Find.ByName("pwd")).Value = login.Password;
            loginForm.Buttons[0].Click();
            return true;
        }

        public override void DoLogout()
        {
            var exitForm = Browser.GetFormByAction("/public/login.php");
            if(exitForm.Exists)
            {
                exitForm.Buttons[0].Click();
                base.DoLogout();
            }
            else
            {
                LogWriter.LogDebug("Ccылка выхода ненайдена");
            }
            
        }
        public override void Clear()
        {
            base.Clear();
            string[] deleteLinks;
            do
            {
                deleteLinks = GetDeleteLinks();
                deleteLinks.Foreach(e => Browser.GoTo(e));
            } while (deleteLinks.Length > 0);
            LogWriter.LogAction("Окончена");
        }

        private string[] GetDeleteLinks()
        {
            Browser.GoTo("http://av.by/advpublic/index.php?event=5");
   
            return Browser.Links.Where(
                   e => !string.IsNullOrWhiteSpace(e.Url) && e.Url.Contains("advpublic.php?event=Delete&public_delete[]="))
                   .Select(e => e.Url).ToArray();
        }


        public override void Post(Wheels adv)
        {
            base.Post(adv);

            var subject = adv.SeasonSubject;

            GoToTopic(adv);

            var form = Browser.Form(Find.ByName("AddForm"));
            form.SetText("public_topic", subject);
            form.SetText("public_text", string.Format("{2}\n{0} {1}", adv.Description, adv.PriceText, subject));

            form.SelectOption("public_period_id", "4");
            form.SelectOption("city_id", "1");

            if (adv.Images.Count > 0)
                form.RadioButton(e => e.GetAttributeValue("name") == "upload_image" && e.GetAttributeValue("value") == "1").Checked = true;

            PostPhones(form, GetPhones(adv));

            form.SetCaptcha("image_security_code");
  
            var button=form.Button(Find.ByName("send"));
            if(button.Exists)
                button.Click();

            PostImages(adv);
        }

        private void PostPhones(Form form,IList<Phone> phones)
        {
            for (int i = 0; i < phones.Count; i++)
            {
                var phone = phones[i];
                form.SelectOption(string.Format("phone{0}_operator_id",i+1), ((int)phone.Operator).ToString());
                form.SetText(string.Format("phone{0}_number", i + 1),phone.Number);
                form.SetText(string.Format("phone{0}_code", i + 1), phone.Code);
            }
           
        }

        private void PostImages(Wheels adv)
        {
            foreach (var img  in adv.Images)
            {
                var form = Browser.GetFormByAction("/advpublic/advpublic.php");
                SetUploadFile(img, form.FileUpload(Find.ByName("public_image")));
                form.Submit();
            }
            var link=Browser.Links.FirstOrDefault(e =>
                e.ClassName == "txtBlue" && 
                e.Url != null && e.Url.Contains("advpublic.php?event=View") &&
                e.Text == "Закончить загрузку изображений");

            if(link!=null && link.Exists)
                link.Click();
        }

        private void GoToTopic(Wheels adv)
        {
            Browser.GoTo("http://av.by/advpublic/advpublic.php?event=Pre_Form");

            var form = Browser.Form(Find.ByName("preAddForm"));

            form.SelectList(Find.ByName("category_parent")).SelectByValue(adv.Condition == WheelCondition.New ? "52" : "21");
            form.SelectList(Find.ByName("country_id")).SelectByValue("1");
            string catId;
            if (adv.Size.Radius < 14)
                catId = "35";
            else if (adv.Size.Radius < 18)
                catId = (adv.Size.Radius + 22).ToString();
            else
                catId = "87";

            form.SelectList(Find.ByName("category_id")).SelectByValue(catId);

            form.Buttons[0].Click();
        }
    }
}
