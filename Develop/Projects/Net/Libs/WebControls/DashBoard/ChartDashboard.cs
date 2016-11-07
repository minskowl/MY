using System;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;


[assembly: WebResource("Savchin.Web.UI.DashBoard.ChartDashboard.js", "application/x-javascript", PerformSubstitution = true)]

namespace Savchin.Web.UI
{
    public class ChartDashboard : Dashboard
    {
        private const string actionKeyApplyGraphType = "applyGraphType";
        private const string actionKeyApplyTimeFrame = "applyTimeFrame";
        private const string actionKeyShow = "show";




        private readonly GraphTypeControl buttonGraphType = new GraphTypeControl();
        private readonly TymeFrameControl buttonTimeFrame = new TymeFrameControl();
        private readonly ButtonEx buttonZoom = new ButtonEx();

        #region Properties

    

        private GetUniversalData datasource;
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public GetUniversalData DataSource
        {
            get { return datasource; }
            set { datasource = value; }
        }
        /// <summary>
        /// Gets the name of the JS chart object.
        /// </summary>
        /// <value>The name of the JS chart object.</value>
        public string JSChartObjectName
        {
            get { return ClientID + "ChartObj"; }
        }
        private ChartDashboardSettings settings;
        /// <summary>
        /// Gets the chart settings.
        /// </summary>
        /// <value>The chart settings.</value>
        internal ChartDashboardSettings ChartSettings
        {
            get { return settings; }
        }

        /// <summary>
        /// Gets the time frame.
        /// </summary>
        /// <value>The time frame.</value>
        public TimeFrameType TimeFrame
        {
            get { return settings.TimeFrameType; }
        }

        readonly ChartControl chart = new ChartControl();
        /// <summary>
        /// Gets the chart.
        /// </summary>
        /// <value>The chart.</value>
        internal ChartControl Chart
        {
            get { return chart; }
        }
        private ISettingProvider<ChartDashboardSettings> chartDashboardSettingsProvider;
        /// <summary>
        /// Gets or sets the chart dashboard settings provider.
        /// </summary>
        /// <value>The chart dashboard settings provider.</value>
        public ISettingProvider<ChartDashboardSettings> ChartDashboardSettingsProvider
        {
            get { return chartDashboardSettingsProvider; }
            set { chartDashboardSettingsProvider = value; }
        }

        #endregion


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            MaxHeight = 300;
            ContentHeight = 270;

            ScrollBars = ScrollBars.None;

            buttonGraphType.ID = ID + "_GraphType";
            buttonTimeFrame.ID = ID + "_TimeFrame";

            buttonZoom.ID = ID + "_ButtonZoom";
            buttonZoom.Mode = ButtonEx.ButtonType.Link;
            buttonZoom.Text = "Zoom";
            buttonZoom.ImageUrl = ImagePathProvider.PreviewImage;
            buttonZoom.UseSubmitBehavior = false;
            buttonZoom.OnClientClick = JSChartObjectName + ".ShowZoomWindow();";



            settings = GetSettings(ClientID);
        }
        private ChartDashboardSettings GetSettings(string objectId)
        {
            ChartDashboardSettings result = null;

            if(chartDashboardSettingsProvider!=null)
             result = chartDashboardSettingsProvider.Load(objectId);

            if (result == null)
            {
                result = new ChartDashboardSettings();
            }
            return result;
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //if (!IsAJAXRequest)
            //    return;

            Layout.Header.AdditionalControls.Add(buttonTimeFrame);
            Layout.Header.AdditionalControls.Add(LiteralEx.CreateSpace(2));
            Layout.Header.AdditionalControls.Add(buttonGraphType);
            Layout.Header.AdditionalControls.Add(LiteralEx.CreateSpace(2));
            Layout.Header.AdditionalControls.Add(buttonZoom);
            Layout.Header.AdditionalControlsWidth = "160px";
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (settings.ChartType == ChartType.Pie)
            {
                buttonGraphType.Visible = false;
                buttonTimeFrame.Visible = false;
            }

            //Test
            //Page.ClientScript.RegisterClientScriptInclude(typeof(ChartDashboard), "ChartDashboard.js", AppSettings.ApplicationJsPath + "ChartDashboard.js");
            Page.ClientScript.RegisterClientScriptResource(typeof(ChartDashboard), "NewWayMedia.BusinessLayer.Controls.DashBoard.ChartDashboard.js");


            string initInstanceScript = string.Format("\n var {0} = new ChartDashboard('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}');  {0}.Init(); \n",
                                                      JSChartObjectName,
                                                      JSObjectName,
                                                      buttonGraphType.ButtonBarID,
                                                      buttonGraphType.ButtonLineID,
                                                      buttonTimeFrame.ButtonWeekID,
                                                      buttonTimeFrame.ButtonMonthID,
                                                      buttonTimeFrame.ButtonDayID,
                                                      handler.GetActionURL(string.Empty),
                                                      settings.toJSONString().Replace("'", "\'"));
            Page.ClientScript.RegisterStartupScript(typeof(ChartDashboard), "InitChartDashboard" + ClientID, initInstanceScript, true);

            buttonTimeFrame.ApplyScript = JSChartObjectName + ".ApplyTimeFrame(); " + buttonTimeFrame.JSObjectName + ".Hide();";
            buttonGraphType.ApplyScript = JSChartObjectName + ".ApplyGraphType(); " + buttonGraphType.JSObjectName + ".Hide();";
        }


        /// <summary>
        /// Raises the    AJAXRequest event.
        /// </summary>
        /// <param name="args">The <see cref="NewWayMedia.Common.Controls.DataRequestEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected override string OnAJAXRequest(DataRequestEventArgs args)
        {
            try
            {
                PageEx p = Page as PageEx;
                int? height = p.GetRequestInt("height");
                int? width = p.GetRequestInt("width");



                chart.Height = height ?? 250;
                chart.Width = width ?? 250;

                switch (handler.Action)
                {
                    case actionKeyApplyGraphType:
                        int? type = p.GetRequestInt("type");
                        settings.ChartType = (ChartType)(type ?? 0);
                       chartDashboardSettingsProvider.Save(settings,ClientID);
                        break;
                    case actionKeyApplyTimeFrame:
                        int? timeFrame = p.GetRequestInt("frame");
                        settings.TimeFrameType = (TimeFrameType)(timeFrame ?? 0);
                        chartDashboardSettingsProvider.Save(settings, ClientID);
                        break;
                    case actionKeyShow:
                        break;


                    default:
                        return base.OnAJAXRequest(args);
                }
                chart.Type = settings.ChartType;
                chart.TimeFrame = settings.TimeFrameType;
                if (datasource != null)
                    chart.DataSource = datasource();

                return ControlHelper.GetControlHtml(chart);
            }
            catch (Exception ex)
            {
                Util.Log.Error("OnAJAXRequest", ex);
            }
            return string.Empty;
        }




        /// <summary>
        /// Gets the cached data.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected object GetCachedData(GetUniversalData source, string key)
        {
            object result;

            if (Page.Cache[key] == null)
            {
                result = source();
                Page.Cache.Add(key,
                               result,
                               null,
                               DateTime.Now.AddSeconds(60),
                               TimeSpan.Zero,
                               CacheItemPriority.High, 
                               null);
            }
            else
                result = Page.Cache[key];

            return result;
        }
    }
}
