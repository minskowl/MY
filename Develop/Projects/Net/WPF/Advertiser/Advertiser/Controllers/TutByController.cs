using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advertiser.Entities;
using Savchin.Core;
using WatiN.Core;

namespace Advertiser.Controllers
{
    public class TutByController : AdvControllerBase
    {
        public override string MainUrl
        {
            get { return "http://www.tut.by/"; }
        }

        /// <summary>
        /// Does the login.
        /// </summary>
        /// <param name="login">The login.</param>
        public override bool DoLogin(Login login)
        {
            base.DoLogin(login);

            DoLogout();

            var form = Browser.Form("login");

            form.TextField(Find.ByName("login")).Value = login.User;
            form.TextField(Find.ByName("password")).Value = login.Password;
            var btn = form.Buttons[0];
            btn.Click();
            return true;
        }

        private void DoLogout()
        {
            var form = Browser.Form("authorize_form");
            if (!form.Exists) return;

            var exitLink = form.Links.FirstOrDefault(e => e.Url != null && e.ClassName == "ulR red" &&
                                                             e.Url.Contains("http://profile.tut.by/logout/?"));
            if (exitLink != null && exitLink.Exists)
            {
                exitLink.Click();
            }
        }

        public override void Post(Wheels adv)
        {
            base.Post(adv);

            Browser.GoTo("http://ay.tut.by/sell/");

            var form = Browser.Form("form");

            var categories = form.FindElement("ul", "lotadd-category__ul");
            var maiCat=categories.FindElement("li", "catalog_main");
            maiCat.Link("alink_1101563").Click();

            var subCat = categories.FindElement("li", "catalog_1101563");
            subCat.Link("alink_1103650").Click();

            var subCat1 = categories.FindElement("li", "catalog_1103650");

            switch (adv.Season)
            {
       
                case WheelSeason.Summer:
                    subCat1.Link("alink_1103655").Click();
                    break;
                case WheelSeason.Winter:
                case WheelSeason.SnowTyre:
                    subCat1.Link("alink_1103654").Click();
                    break;
                case WheelSeason.AllSeasons:
                    subCat1.Link("alink_1103653").Click();
                    break;
                default:
                  subCat1.Link("alink_1103656").Click();
                    break;
            }

            form.SetText("title", adv.SeasonSubject);
            form.SetText("count",adv.Count.ToString());
            form.SetTextById("auc_pricecost",adv.Price.ToString());

            var elUpload = form.FileUpload("fileupload_newinput");
            foreach (var f in adv.Images)
            {
                SetUploadFile(f, elUpload);
            }

            form.Button("img-btn").Click();
        }
    }
}
