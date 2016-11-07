using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Savchin.TimeManagment;
using Site.Bl;
using Site.Core;
using Site.Cotrols;

public partial class UserArea_Licenses : SitePage
{
    LicenseManager _licenseManager = new LicenseManager();
    TransferManager _transferManager = new TransferManager();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public DataTable GetData()
    {
        return _licenseManager.GetInfoByUserID(SiteContext.Current.CurrenUserID.Value);
    }

    /// <summary>
    /// Buttons the search transaction on click.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSearchTransactionOnClick(object sender, EventArgs e)
    {
        try
        {
            var transfer = _transferManager.SearchFreeTransfer(boxPurse.Text.Trim(), DateRange.GetDayRange(boxDate.Value.Value));
            if (transfer == null)
            {
                ShowAlert("Перевода не найдено. \n Проверьте правильность ввода кошелька и даты перевода.");
                return;
            }

            _licenseManager.Create(transfer);
            ShowAlert("Лицензия создана");
            grid.DataBind();
        }
        catch (Exception ex)
        {
            ShowAlert("Ошибка сервиса оплаты. \n Обратитесь в службу поддержки и Вам помогут с лицензией.");
            Log.Site.Error("Error Licenses.aspx buttonSearchTransactionOnClick", ex);
        }

    }
}
