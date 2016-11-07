#region Version & Copyright
/* 
 * $Id: Error.aspx.cs 485 2008-11-05 10:35:14Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;

using Savchin.Web.UI;
using KnowledgeBase.SiteCore;
using KnowledgeBase.Core;


public partial class Error : ErrorPage
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (pageNotExists)
            {
                RedirectorAdmin.GoToDefaultPage();
                return;
            }

            BackLink.NavigateUrl = Request.Url.ToString();
            if (exception == null)
                return;

            if (AppSettings.ShowExceptions)
            {
                RealExceptionPanel.Visible = true;
                ExceptionTextBox.Text = exception.ToString();
                // Message.Text = GetErrorMessage();

                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
            }


        }
        catch (Exception ex)
        {
            Log.Site.Fatal("Error show Exception: ", ex);
        }

    }

    ///// <summary>
    ///// Logings the exception.
    ///// </summary>
    //protected override void LogingException()
    //{
    //    Log.Site.Fatal(//Log.GetFullTraceInfo() +
    //        Environment.NewLine + "Unhandled Exception: ", exception);
    //}



}
