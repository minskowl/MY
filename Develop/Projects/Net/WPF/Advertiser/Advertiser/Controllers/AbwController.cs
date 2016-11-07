using System;
using System.Linq;
using System.Windows;
using Advertiser.Entities;
using Savchin.Collection.Generic;
using WatiN.Core;

namespace Advertiser.Controllers
{
    class AbwController : AdvControllerBase
    {
        /// <summary>
        /// Gets the main URL.
        /// </summary>
        public override string MainUrl
        {
            get { return "http://www.abw.by/"; }
        }

        /// <summary>
        /// Does the login.
        /// </summary>
        /// <param name="login">The login.</param>
        public override bool DoLogin(Login login)
        {
            base.DoLogin(login);

            DoLogout();
            var form = Browser.Form(Find.ByClass("auth"));

            form.TextField(Find.ByName("login")).Value = login.User;
            form.TextField(Find.ByName("pass")).Value = login.Password;
            var btn = form.Buttons[0];
            btn.Click();
            return true;
        }

        public override void DoLogout()
        {
            var exitLink = Browser.Links.FirstOrDefault(e => e.Url != null && e.ClassName == "cat_red" &&
         e.Url.Contains("/index.php?act=exit"));
            if (exitLink != null && exitLink.Exists)
            {
                exitLink.Click();
                base.DoLogout();
            }
        }

        public override void Post(Wheels adv)
        {
            base.Post(adv);

            Browser.GoTo("http://www.abw.by/index.php?act=adv&act_adv=pre_add_adventure&id_cat=4&id_sub=15");

            var form = Browser.Form(Find.ByName("form1"));
            PostPhones(adv, form);

            var listRadius = form.SelectList("shina_size_id");
            listRadius.Select("R" + adv.Size.Radius);

            form.SelectList(Find.ByName("disk_num")).SelectByValue(adv.Count.ToString());
            form.TextField(Find.ByName("shina_size")).Value = adv.Size.Width.ToString();
            form.TextField(Find.ByName("shina_size_2")).Value = adv.Size.Height.ToString();
            form.SelectList(Find.ByName("shina_type")).SelectByValue("1");

            var parts = adv.Manufacturer.Split(new char[] { '/', '|' });
            if (parts.Length > 1)
            {
                form.SelectList(Find.ByName("shina_title")).Select(parts[1]);
            }
            else
            {
                form.TextField(Find.ByName("shina_model")).Value = parts[0];
            }
            form.SelectList(Find.ByName("shina_season")).SelectByValue(GetSeasonCode(adv));
            form.SelectList(Find.ByName("shina_condition_id")).SelectByValue(GetConditionCode(adv));
            form.TextField(Find.ByName("price_value")).Value = (adv.Price ?? 1).ToString();

            for (int i = 0; i < adv.Images.Count; i++)
            {
                var upl = form.FileUpload(Find.ByName("file" + (i + 1)));
                SetUploadFile(adv.Images[i], upl);
            }

            Browser.BringToFront();

            form.SetCaptcha("pam");


            form.Image("subbtn1").Click();

            var btn = Browser.Images.FirstOrDefault(e => !string.IsNullOrWhiteSpace(e.Src) && e.Src.EndsWith("/images/but_add.gif"));
            if (btn != null && btn.Exists)
            {
                btn.Click();
            }
            else
            {
                MessageBox.Show("Финальная кнопка ненайдена");
            }

        }

        private void PostPhones(Wheels adv, Form form)
        {
            var i = 1;
            foreach (var phone in GetPhones(adv))
            {
                form.TextField("code" + i).Value = phone.Code;
                form.TextField("phone" + i).Value = phone.Number;
                i++;
            }
        }

        public override void Clear()
        {
            base.Clear();
            string[] deleteLinks;
            do
            {
                deleteLinks = GetDeleteLinks();
                foreach (var deleteLink in deleteLinks)
                {
                    try
                    {
                        Browser.GoTo(deleteLink);
                    }
                    catch (Exception ex)
                    {

                        LogWriter.LogDebug("Ошибка удаления \n" + ex);
                    }
                }

            } while (deleteLinks.Length > 0);
            LogWriter.LogAction("Окончена");
        }
        private string[] GetDeleteLinks()
        {
            Browser.GoTo("http://www.abw.by/index.php?act=adv&act_adv=delete_advertisement");
            var table = Browser.Table("main_tb");
            var cell = table.TableCell("main_td02");
            return cell.Links.Where(
                   e => !string.IsNullOrWhiteSpace(e.Url) && e.Url.Contains("/index.php?act=adv&act_adv=1&public_delete="))
                   .Select(e => e.Url).ToArray();
        }

        private string GetConditionCode(Wheels w)
        {
            switch (w.Condition)
            {
                case WheelCondition.New:
                    return "1";
                case WheelCondition.Used:
                    return "2";
                case WheelCondition.Welded:
                    return "3";
                default:
                    return "2";
            }
        }

        private string GetSeasonCode(Wheels w)
        {
            switch (w.Season)
            {
                case WheelSeason.Summer:
                    return "1";
                case WheelSeason.Winter:
                    return "3";
                case WheelSeason.SnowTyre:
                    return "4";
                case WheelSeason.AllSeasons:
                    return "2";
                default:
                    return string.Empty;
            }
        }
    }
}
