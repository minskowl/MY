#region Version & Copyright
/* 
 * $Id: Dashboard.cs 21865 2007-09-20 16:44:02Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



namespace Savchin.Web.UI
{
    public class Dashboard : WebControl, IDashboard
    {
        private const string actionKeySaveSettings = "SaveSettings";
        private static readonly string imageUrl;


        protected AJAXHandler handler = new AJAXHandler();
        private readonly DashboardLayout layout = new DashboardLayout();


        #region Properties
        private ISettingProvider<DashboardSettings> dashboardSettingsProvider;

        /// <summary>
        /// Gets or sets the dashboard settings provider.
        /// </summary>
        /// <value>The dashboard settings provider.</value>
        public ISettingProvider<DashboardSettings> DashboardSettingsProvider
        {
            get { return dashboardSettingsProvider; }
            set { dashboardSettingsProvider = value; }
        }
        /// <summary>
        /// Gets the init instance J script.
        /// </summary>
        /// <value>The init instance J script.</value>
        protected string InitInstanceJScript
        {
            get
            {
                return string.Format("\n var {0} = new Dashboard('{1}','{2}','{3}','{4}'); \n {0}.init();\n",
                                                      JSObjectName,
                                                      ClientID,
                                                      imageUrl,
                                                      settings.toJSONString().Replace("'", "\'"),
                                                      handler.GetActionURL(actionKeySaveSettings)
                                                      );
            }
        }
        /// <summary>
        /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.</returns>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Title
        {
            get
            {
                String s = (String)ViewState["Title"];
                return (s ?? String.Empty);
            }

            set
            {
                ViewState["Title"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        [DefaultValue(typeof(Unit), "100"),
         Category("Layout"),
         Description("Height")]
        public virtual Unit ContentHeight
        {
            get
            {
                object o = ViewState["ContentHeight"];
                if (o == null) return 100;
                return (Unit)o;
            }
            set
            {
                ViewState["ContentHeight"] = value;
            }
        }

        /// <summary>
        /// Gets the height of the real.
        /// </summary>
        /// <value>The height of the real.</value>
        private Unit RealHeight
        {
            get
            {
                if (MaxHeight != Unit.Empty && ContentHeight.Value > MaxHeight.Value)
                    return MaxHeight;
                return ContentHeight;
            }
        }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        [Category("Layout"),
        Description("Max Height")]
        public virtual Unit MaxHeight
        {
            get
            {
                object o = ViewState["MaxHeight"];
                if (o == null) return Unit.Empty;
                return (Unit)o;
            }
            set
            {
                ViewState["MaxHeight"] = value;
            }
        }

        /// <summary>
        /// Gets the name of the JS object.
        /// </summary>
        /// <value>The name of the JS object.</value>
        public string JSObjectName
        {
            get { return ClientID + "Obj"; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [status bar visible].
        /// </summary>
        /// <value><c>true</c> if [status bar visible]; otherwise, <c>false</c>.</value>
        [Category("Layout")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool StatusBarVisible
        {
            get
            {
                object obj1 = ViewState["StatusBarVisible"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return false;
            }
            set
            {
                ViewState["StatusBarVisible"] = value;
            }
        }
        private DashboardSettings settings;
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public DashboardSettings Settings
        {
            get { return settings; }
        }


        /// <summary>
        /// Gets or sets the scroll bars.
        /// </summary>
        /// <value>The scroll bars.</value>
        [Category("Layout"), DefaultValue(0), Description("Panel_ScrollBars")]
        public virtual ScrollBars ScrollBars
        {
            get
            {
                object o = ViewState["ScrollBars"];
                if (o == null)
                    return ScrollBars.Vertical;
                return (ScrollBars)o;
            }
            set { ViewState["ScrollBars"] = value; }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public HtmlGenericControl Container
        {
            get { return Layout.Container; }
        }

        /// <summary>
        /// Gets the layout.
        /// </summary>
        /// <value>The layout.</value>
        public DashboardLayout Layout
        {
            get { return layout; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is AJAX request.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is AJAX request; otherwise, <c>false</c>.
        /// </value>
        protected bool IsAjaxRequest
        {
            get { return ((PageEx)Page).IsAjaxRequest; }
        }
        #endregion

        /// <summary>
        /// Initializes the <see cref="Dashboard"/> class.
        /// </summary>
        static Dashboard()
        {
            try
            {
                imageUrl = ImagePathProvider.CommonImagesUrl + "dashboard/";
            }
            catch
            {
                //Failed in design mode
            }
        }

        /// <summary>
        /// Gets the inner HTML.
        /// </summary>
        /// <returns></returns>
        protected string GetInnerHTML()
        {

            using (StringWriter stringWriter = new StringWriter())
            {
                HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
                Layout.Container.RenderControl(writer);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {


            Attributes.Add("instanceName", JSObjectName);
            Attributes.Add("class", "dragableBox");
            Attributes.CssStyle.Add("position", "static");

            Layout.ID = ID + "_Layout";
            Layout.JSObjectName = JSObjectName;
            Layout.ImageUrl = imageUrl;
            Controls.Add(Layout);



            handler.ID = ID + "AJAXHandler";
            handler.ContentProcessor = OnAJAXRequest;
            Controls.Add(handler);

            base.OnInit(e);

            settings = GetSetting(ClientID);


        }
        private DashboardSettings GetSetting(string objectId)
        {
            DashboardSettings result = null;
            if (dashboardSettingsProvider != null)
                result = dashboardSettingsProvider.Load(objectId);

            if (result == null)
            {
                result = new DashboardSettings();
                result.Closed = false;
                result.Expanded = true;
            }
            return result;
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {


            base.OnPreRender(e);

            //Note: For Test
            //Page.ClientScript.RegisterClientScriptInclude(typeof(Dashboard), "Dashboard", AppSettings.ApplicationJsPath + "Dashboard.js");

            Page.ClientScript.RegisterClientScriptResource(typeof(DashboardManager), EmbeddedResources.JsDashboard);


            Page.ClientScript.RegisterStartupScript(typeof(Dashboard), "InitDashBoard" + ClientID, InitInstanceJScript, true);

            if (settings.Closed)
                Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");

            Layout.Title = Title;
            Layout.StatusBarVisible = StatusBarVisible;
            Layout.ContentHeight = RealHeight;
            Layout.ContentScrollBars = ScrollBars;
        }




        /// <summary>
        /// Raises the AJAXRequest event.
        /// </summary>
        /// <param name="args">The <see cref="NewWayMedia.Common.Controls.DataRequestEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected virtual string OnAJAXRequest(DataRequestEventArgs args)
        {
            switch (handler.Action)
            {
                case actionKeySaveSettings:
                    SaveSettings();
                    break;
                default:
                    break;
            }

            return string.Empty;
        }

        private void SaveSettings()
        {
            try
            {
                if (Page.Request["settings"] == null)
                    return;
                settings = DashboardSettings.fromJSONString(Page.Request["settings"]);
                if (settings != null)
                    dashboardSettingsProvider.Save(settings, ClientID);
            }
            catch (Exception ex)
            {
                Util.Log.Error("Datsboard::SaveSettings", ex);
            }
        }
    }
}
