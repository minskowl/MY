using System;
using System.Threading;
using System.Windows.Forms;
using FlatSearcher.Core;
using MyCustomWebBrowser.Core;
using Savchin.Forms.Core;
using Savchin.Logging;
using WatiN.Core;

namespace FlatSearcher.Controls
{
    public class SearchControl : BrowserControl
    {
        private const string UrlResults = "http://realt.by/sale/flats/show/database/";
        private const string UrlResults1 = "http://realt.by/sale/flats/";
        private const string UrlSearch = "http://realt.by/sale/flats/show/search/";
        public SearchControl()
        {
            if (ControlHelper.DesignMode) return;

            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;

            Navigate(UrlSearch);
        }



        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url != webBrowser.Url) return;

            switch (e.Url.ToString())
            {
                case UrlSearch:
                    if (Properties.Settings.Default.RestoreQuery)
                        SetCriterias();
                    //HideBlocks();
                    break;
                case UrlResults1:
                case UrlResults:
                    ParseResults();
                    //HideBlocks();
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Parses the results.
        /// </summary>
        public void ParseResults()
        {
            var database = SearchContext.Current.Data;

            var table = WatinBrowser.Table("list");
            var i = 0;

            var settings = Properties.Settings.Default;

            while (i < table.TableRows.Count)
            {
                var firstRow = table.TableRows[i];
                i++;
                if (firstRow.ClassName == null || !firstRow.ClassName.StartsWith("text-row"))
                {
                    firstRow.Style.SetAttributeValue("display", "none");
                    continue;
                }

                var secondRow = table.TableRows[i];
                i++;

                var link = firstRow.TableCells[3].Links[0];

                var parts = link.Url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var id = parts[parts.Length - 1];
                var flat = database.Find(id);
                var visible = GetVisibility(settings, link, flat);

                if (visible == null) continue;

                if (visible.Value)
                {
                    foreach (var cell in firstRow.TableCells)
                    {
                        cell.Style.SetAttributeValue("background-color", "LightGreen");
                    }

                }
                else
                {
                    firstRow.Style.SetAttributeValue("display", "none");
                    secondRow.Style.SetAttributeValue("display", "none");
                }

            }
        }
        private bool? GetVisibility(Properties.Settings settings, Link link, Flat flat)
        {
            bool? result = null;
            if (settings.FilterByAddress)
            {
                var address = link.Text.Trim();
                result = SearchContext.Current.Data.GetVisbility(address);
                if (result.HasValue && !result.Value) return result;
            }
            if (settings.FilterByFlat && flat != null)
            {
                result = flat.Visible;
            }
            return result;
        }

        public void SaveQuery()
        {
            if (webBrowser.Url.ToString() != UrlSearch)
            {
                MessageBox.Show("Это не страница поиска");
                return;
            }
            var form = WatinBrowser.Form(Find.ByName("tx_uedbflat_pi2"));
            if (!form.Exists)
            {
                MessageBox.Show("Это не страница поиска");
                return;
            }
            var criteria = SearchContext.Current.Data.Criteria;
            criteria.Town = form.SelectList(Find.ByName("tx_uedbflat_pi2[DATA][town_id][e]")).GetAttributeValue("value");
            var tmp = form.SelectList(Find.ByName("tx_uedbflat_pi2[DATA][rooms][e][1]")).GetAttributeValue("value");
            criteria.RoomCount = int.Parse(tmp);

            criteria.IgnoreFirstFloor = form.CheckBox(Find.ByName("tx_uedbflat_pi2[DATA][storey][ne]")).Checked;
            criteria.IgnoreLastFloor = form.CheckBox(Find.ByName("tx_uedbflat_pi2[DATA][storey][fne]")).Checked;

            tmp = form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][area_kitchen][ge]")).Value;
            criteria.KitcherFrom = string.IsNullOrWhiteSpace(tmp) ? (decimal?)null : decimal.Parse(tmp);

            tmp = form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][area_living][ge]")).Value;
            criteria.LivingSpaceFrom = string.IsNullOrWhiteSpace(tmp) ? (decimal?)null : decimal.Parse(tmp);

            tmp = form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][building_year][le]")).Value;
            criteria.YearTo = string.IsNullOrWhiteSpace(tmp) ? (int?)null : int.Parse(tmp);

            tmp = form.SelectList(Find.ByName("tx_uedbflat_pi2[rec_per_page]")).GetAttributeValue("value");
            criteria.FlatsPerPage = int.Parse(tmp);

            tmp = form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][price][le]")).Value;
            criteria.PriceTo = string.IsNullOrWhiteSpace(tmp) ? (int?)null : int.Parse(tmp);
            tmp = form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][price][ge]")).Value;
            criteria.PriceFrom = string.IsNullOrWhiteSpace(tmp) ? (int?)null : int.Parse(tmp);

        }

        public void RestoreQuery()
        {
            if (webBrowser.Url.ToString() == UrlSearch)
                SetCriterias();
            else
                webBrowser.Navigate(UrlSearch);
        }

        private void SetCriterias()
        {
            try
            {
                var form = WatinBrowser.Form(Find.ByName("tx_uedbflat_pi2"));
                if (!form.Exists)
                {
                    WatinBrowser.WaitForComplete();
                }
                var criteria = SearchContext.Current.Data.Criteria;


                form.SelectOption("tx_uedbflat_pi2[DATA][rooms][e][1]", criteria.RoomCount > 0 ? criteria.RoomCount.ToString() : "3");

                // tmp.SelectByValue(criteria.RoomCount > 0 ? criteria.RoomCount.ToString() : "3");

                form.SetCheked("tx_uedbflat_pi2[DATA][storey][ne]", criteria.IgnoreFirstFloor);
                form.SetCheked("tx_uedbflat_pi2[DATA][storey][fne]", criteria.IgnoreLastFloor);
                form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][building_year][le]")).Value = criteria.YearTo.ToString();
                form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][area_kitchen][ge]")).Value = criteria.KitcherFrom.ToString();
                form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][area_living][ge]")).Value = criteria.LivingSpaceFrom.ToString();
                form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][price][ge]")).Value = criteria.PriceFrom.ToString();
                form.TextField(Find.ByName("tx_uedbflat_pi2[DATA][price][le]")).Value = criteria.PriceTo.ToString();

                if (criteria.FlatsPerPage > 0)
                    form.SelectOption("tx_uedbflat_pi2[rec_per_page]", criteria.FlatsPerPage.ToString());


                form.SelectOption("tx_uedbflat_pi2[DATA][town_id][e]", string.IsNullOrWhiteSpace(criteria.Town) ? "5102" : criteria.Town);
            }
            catch (Exception ex)
            {
                SearchContext.Current.Log.AddMessage(Severity.Warning, "Ошибка установки параметров по умролчанию", ex);
            }
        }



    }
}
