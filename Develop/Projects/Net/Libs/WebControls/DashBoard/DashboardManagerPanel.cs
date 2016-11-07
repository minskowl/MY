#region Version & Copyright
/* 
 * $Id: DashboardManagerPanel.cs 21865 2007-09-20 16:44:02Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;


namespace Savchin.Web.UI
{
    internal class DashboardManagerPanel : DropPanelButton
    {
        private const string JSWindowInstance = "dbManagerWindow";
        private List<IDashboard> dashboards;

        /// <summary>
        /// Gets or sets the dashboards.
        /// </summary>
        /// <value>The dashboards.</value>
        public List<IDashboard> Dashboards
        {
            get { return dashboards; }
            set { dashboards = value; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Panel.Width = 400;
            Panel.Height = 200;
            Button.Mode = ButtonEx.ButtonType.Link;
            Button.Text = "Add dashboard";
            Button.ImageUrl = ImagePathProvider.AddImage;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            CreatePanelDashboards();

            int newHeight = dashboards.Count * 19 + 100;
            if (Panel.Height.Value < newHeight)
                Panel.Height = newHeight;

            StringBuilder builder= new StringBuilder();
            RegisterInitializeJScript(builder);
            Page.ClientScript.RegisterStartupScript(typeof(DashboardManagerPanel),
                                                  "initDashboardManagerPanel",
                                                  builder.ToString(),
                                                  true);
        }

        private void CreatePanelDashboards()
        {
            HeaderParagraph header = new HeaderParagraph();
            header.Text = "Please, choose the dashboards to display:";
            Panel.Controls.Add(header);
            foreach (IDashboard dashboard in Dashboards)
            {
                CheckBoxEx box = new CheckBoxEx();
                box.Text = dashboard.Title;
                box.ID = dashboard.ID + "_VisibleBox";
                box.Checked = !dashboard.Settings.Closed;
                box.InputAttributes.Add("dashboardObjName", dashboard.JSObjectName);

                Panel.Controls.Add(box);
                Panel.Controls.Add(LiteralEx.CreateNewLine());
            }


            ButtonEx buttonApply = new ButtonEx();
            buttonApply.Text = "Apply";
            buttonApply.UseSubmitBehavior = false;
            buttonApply.OnClientClick = JSWindowInstance + ".Apply();";
            Panel.Controls.Add(buttonApply);

            ButtonEx buttonCancel = new ButtonEx();
            buttonCancel.Text = "Cancel";
            buttonCancel.OnClientClick = JSWindowInstance + ".Close();";
            buttonCancel.UseSubmitBehavior = false;
            Panel.Controls.Add(buttonCancel);

        }
        /// <summary>
        /// Registers the initialize J script.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private void RegisterInitializeJScript(StringBuilder builder)
        {
            builder.AppendLine("var " + JSWindowInstance + " = new DashboardManagerWindow('" + Panel.ClientID + "'," + JSObjectName + ");");
        }
    }
}
