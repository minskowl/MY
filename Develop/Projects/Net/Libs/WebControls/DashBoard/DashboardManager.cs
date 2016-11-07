#region Version & Copyright
/* 
 * $Id: DashboardManager.cs 21865 2007-09-20 16:44:02Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Savchin.Web.UI;


namespace Savchin.Web.UI
{
    public class DashboardManager : Control
    {
        
        public const string JSInstance = "dbEngine";

        private readonly DashboardManagerPanel buttonAddDashboard = new DashboardManagerPanel();

        private readonly List<DragableBoxControl> boxes = new List<DragableBoxControl>();
        private readonly List<IDashboard> dashboards = new List<IDashboard>();
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Add(buttonAddDashboard);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (DesignMode)
                return;

            RegisterIncludes();
            InitializeDasboardsAndDragableBoxes();
            RegisterInitializeJScript();
        }

        /// <summary>
        /// Initializes the dasboards and dragable boxes.
        /// </summary>
        private void InitializeDasboardsAndDragableBoxes()
        {
            FindControlsRecursive(Page.Controls, boxes);
            FindControlsRecursive(Page.Controls, dashboards);

            buttonAddDashboard.Dashboards = dashboards;

            foreach (IDashboard dash in dashboards)
            {
                if (!string.IsNullOrEmpty(dash.Settings.DragableBoxId))
                    MoveDashboard(dash);
            }
        }

        /// <summary>
        /// Moves the dashboard.
        /// </summary>
        /// <param name="dash">The dash.</param>
        private void MoveDashboard(IDashboard dash)
        {
            DragableBoxControl boxControl;

            try
            {
                boxControl = (DragableBoxControl)Page.FindControl(dash.Settings.DragableBoxId.Replace("_", "$"));
            }
            catch (InvalidCastException)
            {
                dash.Settings.DragableBoxId = null;
                return;
            }

            if (boxControl == null)
                return;

            Control controlDash = (Control)dash;
            controlDash.NamingContainer.Controls.Remove(controlDash);
            try
            {
                boxControl.Controls.AddAt(dash.Settings.Order, controlDash);
            }
            catch (ArgumentOutOfRangeException)
            {
                boxControl.Controls.Add(controlDash);
            }


        }

        /// <summary>
        /// Registers the includes.
        /// </summary>
        private void RegisterIncludes()
        {
            //TEst
            //Page.ClientScript.RegisterClientScriptInclude(typeof(DashboardManager), "dashboardEngine.js", AppSettings.ApplicationJsPath + "dashboardEngine.js");
            //Page.ClientScript.RegisterClientScriptInclude(typeof(DashboardManager), "DashboardManagerWindow.js", AppSettings.ApplicationJsPath + "DashboardManagerWindow.js");


            Page.ClientScript.RegisterClientScriptResource(typeof(DashboardManager), EmbeddedResources.JsDashboardEngine);
            Page.ClientScript.RegisterClientScriptResource(typeof(DashboardManager), EmbeddedResources.JsDashboardManagerWindow);

            EmbeddedResources.RegisterAjax(Page);
            EmbeddedResources.RegisterJson(Page);

            ControlHelper.AddCssInclude(Page, typeof(DashboardManager), EmbeddedResources.CssDashboard);
        }

        /// <summary>
        /// Registers the initialize J script.
        /// </summary>
        private void RegisterInitializeJScript()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("var " + JSInstance + "= new DashboardEngine('" + ClientID + @"'); ");

            foreach (DragableBoxControl box in boxes)
            {
                builder.AppendLine(JSInstance + ".addZone('" + box.ClientID + "');");
            }

            Page.ClientScript.RegisterStartupScript(typeof(DashboardManager),
                                                    "initDragableBoxesScript",
                                                    builder.ToString(),
                                                    true);
        }

        /// <summary>
        /// Finds the controls recursive.
        /// </summary>
        /// <typeparam name="T">The type of the control.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="result">The result.</param>
        public static void FindControlsRecursive<T>(ControlCollection collection, List<T> result)
            where T : class
        {
            foreach (Control control in collection)
            {
                if (control is T)
                {
                    T obj = control as T;
                    if (!result.Contains(obj))
                        result.Add(obj);
                }
                else
                    FindControlsRecursive(control.Controls, result);
            }

        }

    }
}
