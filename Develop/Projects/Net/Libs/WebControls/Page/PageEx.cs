
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;

using Savchin.Collection.Generic;
using Savchin.Development;
using Savchin.TimeManagment;
using Savchin.Validation;

using Savchin.Web.Core;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsPrototype, Savchin.Web.UI.EmbeddedResources.JavaScript)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsJQuery, Savchin.Web.UI.EmbeddedResources.JavaScript)]

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsPageEx, Savchin.Web.UI.EmbeddedResources.JavaScript)]


namespace Savchin.Web.UI
{
    internal static partial class EmbeddedResources
    {
        public const string JsPageEx = namespaceName + "Page.PageEx.js";
        public const string JsPrototype = namespaceName + "Page.Prototype.js";
        public const string JsJQuery = namespaceName + "Page.jquery-1.4.1.js";
    }
    /// <summary>
    /// Page Extentions
    /// </summary>
    public partial class PageEx : Page, ICallbackEventHandler
    {
        #region Contants

        private const string _onPageNavigateFuncName = "onPageNavigate";
        private const string alertMessageKey = "PageEx.Alert";
        private const string _confirmNavigateFieldId = "confirmNavigateControlChanged";
        private const string _confirmQuestionText = "You are going to navigate from the current page.\n All unsaved changes will be lost.\n Click 'OK' to continue.";

        #endregion

        #region Properties

        private readonly JavaScriptBuilder onLoadScriptBuilder = new JavaScriptBuilder(false);
        public bool IsAjaxRequest
        {
            get
            {
                return (Request.Headers.Get("x-microsoftajax") ?? "")
          .Equals("delta=true", StringComparison.OrdinalIgnoreCase);
            }

        }

        /// <summary>
        /// Gets a value indicating whether this instance is json request.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is json request; otherwise, <c>false</c>.
        /// </value>
        public bool IsJsonRequest
        {
            get
            {
                return Request.ContentType.StartsWith("application/json",
                                                      StringComparison.OrdinalIgnoreCase);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is refresh.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is refresh; otherwise, <c>false</c>.
        /// </value>
        public bool IsRefresh
        {
            get { return _isRefresh; }
        }
        /// <summary>
        /// Gets the java script on load.
        /// </summary>
        /// <value>The java script on load.</value>
        public JavaScriptBuilder JavaScriptOnLoad
        {
            get { return onLoadScriptBuilder; }
        }

        private readonly JavaScriptBuilder jScript = new JavaScriptBuilder();
        /// <summary>
        /// Gets the java script.
        /// </summary>
        /// <value>The java script.</value>
        public JavaScriptBuilder JavaScript
        {
            get { return jScript; }
        }

        /// <summary>
        /// Gets the page includes.
        /// </summary>
        /// <value>The page includes.</value>
        public PageIncludeCollection PageIncludes
        {
            get { return includes; }
        }

        private readonly PageIncludeCollection includes = new PageIncludeCollection();

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public BrowserType Browser
        {
            get { return Request.GetBrowserType(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [SSL required].
        /// </summary>
        /// <value><c>true</c> if [SSL required]; otherwise, <c>false</c>.</value>
        public bool SSLRequired { get; set; }

        /// <summary>
        /// Gets or sets navigation action script
        /// </summary>
        public string OnNaviageScript
        {
            get { return ViewState["OnNaviageScript"] as string; }
            set { ViewState["OnNaviageScript"] = value; }
        }

        /// <summary>
        /// Setup form default button. 
        /// Make to possible change form default button on client side via varrible _defaultButtonID
        /// Form.DefaultButton don't use because you can't change button on client side
        /// </summary>
        /// <value>The default button.</value>
        public string DefaultButton { get; set; }

        /// <summary>
        /// Gets a value indicating whether [save previous page URL].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [save previous page URL]; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool SavePreviousPageUrl
        {
            get { return true; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [include default CSS].
        /// </summary>
        /// <value><c>true</c> if [include default CSS]; otherwise, <c>false</c>.</value>
        protected bool IncludeDefaultCSS
        {
            get
            {
                object o = ViewState["IncludeDefaultCSS"];
                return o == null ? true : (bool)o;
            }
            set { ViewState["IncludeDefaultCSS"] = value; }
        }

        /// <summary>
        /// Gets or sets the previous page URL.
        /// </summary>
        /// <value>The previous page URL.</value>
        protected string PreviousPageUrl
        {
            get { return ViewState["PreviousPageUrl"] as string; }
            set { ViewState["PreviousPageUrl"] = value; }
        }


        #region Request

        /// <summary>
        /// Gets the request mode.
        /// </summary>
        /// <value>The request mode.</value>
        public string RequestMode
        {
            get { return GetRequestString(RedirectorBase.keyMode); }
        }

        private long? requestId;
        /// <summary>
        /// Gets the request id.
        /// </summary>
        /// <value>The request id.</value>
        public long? RequestId
        {
            get
            {
                if (!requestId.HasValue)
                    requestId = GetRequestLong(RedirectorBase.keyId);
                return requestId;
            }
        }

        private List<long> requestIds;
        /// <summary>
        /// Gets the request ids.
        /// </summary>
        /// <value>The request ids.</value>
        public List<long> RequestIds
        {
            get
            {
                if (requestIds == null)
                {
                    requestIds = GetRequestLongs(RedirectorBase.keyId);
                }
                return new List<long>(requestIds);
            }
        }

        /// <summary>
        /// Gets the request int id.
        /// </summary>
        /// <value>The request int id.</value>
        public int? RequestIntId
        {
            get { return GetRequestInt(RedirectorBase.keyId); }
        }


        /// <summary>
        /// Gets current request string id
        /// </summary>
        public string RequestStringId
        {
            get { return GetRequestString(RedirectorBase.keyId); }
        }

        /// <summary>
        /// Gets the request GUID id.
        /// </summary>
        /// <value>The request GUID id.</value>
        public Guid? RequestGuidId
        {
            get { return GetRequestGuid(RedirectorBase.keyId); }
        }
        /// <summary>
        /// Gets the request long variable.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public long? GetRequestLong(string variableName)
        {
            try
            {
                if (String.IsNullOrEmpty(Request.QueryString[variableName]))
                    return null;
                return Convert.ToInt64(Request.QueryString[variableName]);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the request date range.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public DateRange? GetRequestDateRange(string variableName)
        {
            DateTime? to = GetRequestDateTime(variableName + "To");
            DateTime? from = GetRequestDateTime(variableName + "From");
            if (to.HasValue && from.HasValue)
            {
                try
                {
                    return new DateRange(from.Value, to.Value);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the request longs.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public List<long> GetRequestLongs(string variableName)
        {
            List<long> result = new List<long>();
            try
            {
                string value = Request.QueryString[variableName];
                if (String.IsNullOrEmpty(value))
                    return result;

                string[] parts = value.Split('-');
                foreach (string s in parts)
                {
                    result.Add(long.Parse(s));
                }

                return result;
            }
            catch
            {
                return result;
            }
        }
        /// <summary>
        /// Gets the request bool.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public bool? GetRequestBool(string variableName)
        {
            try
            {
                if (String.IsNullOrEmpty(Request[variableName]))
                    return null;
                return Convert.ToBoolean(Request[variableName]);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the request int.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public int? GetRequestInt(string variableName)
        {
            try
            {
                if (String.IsNullOrEmpty(Request[variableName]))
                    return null;
                return Convert.ToInt32(Request[variableName]);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the request date time.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public DateTime? GetRequestDateTime(string variableName)
        {
            try
            {
                if (String.IsNullOrEmpty(Request.QueryString[variableName]))
                    return null;
                return Convert.ToDateTime(Request.QueryString[variableName]);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the request GUID.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public Guid? GetRequestGuid(string variableName)
        {
            try
            {
                if (String.IsNullOrEmpty(Request.QueryString[variableName]))
                    return null;
                return new Guid(Request.QueryString[variableName]);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the request string.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public string GetRequestString(string variableName)
        {
            try
            {
                return Request.QueryString[variableName];
            }
            catch
            {
                return null;
            }
        }
        #endregion


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PageEx"/> class.
        /// </summary>
        public PageEx()
        {
            includes.AddJavaScript(typeof(PageEx), EmbeddedResources.JsPrototype);
            //includes.AddJavaScript(typeof(PageEx), EmbeddedResources.JsJQuery);
            includes.AddJavaScript(typeof(PageEx), EmbeddedResources.JsPageEx);
            
        }

        #region Public methods

        #region Java script messages

        /// <summary>
        /// Shows the alert after redirect.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowAlertAfterRedirect(string message)
        {
            Session[alertMessageKey] = message;
        }

        /// <summary>
        /// Shows the alert.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(PageEx),
                                              message.GetHashCode().ToString(),
                                              "\n alert('" + JavaScriptBuilder.ApplyEscapeSequences(message) +
                                              "'); \n",
                                              true);

        }
        /// <summary>
        /// Opens the new window.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void OpenNewWindow(string url)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(PageEx),
                                              url.GetHashCode().ToString(),
                                              "\n window.open('" + url + "'); \n",
                                              true);

        }
        /// <summary>
        /// Opens the new window.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="options">The options.</param>
        public void OpenNewWindow(string url, WindowOpenOptions options)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(PageEx),
                                              url.GetHashCode().ToString(),
                                              "\n window.open('" + url + "',null,'" + options + "'); \n",
                                              true);

        }
        #endregion

        #region Validation
        /// <summary>
        /// Gets the validation validators.
        /// </summary>
        /// <returns></returns>
        public List<IValidationValidator> GetValidationValidators()
        {
            return CollectionUtil.Find<IValidationValidator>(Validators);
        }
        /// <summary>
        /// Initializes the validators.
        /// </summary>
        /// <param name="type">The type.</param>
        public void InitializeValidators(Type type)
        {
            foreach (IValidationValidator validator in GetValidationValidators())
            {
                validator.Initialize(type);
            }
        }
        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void ShowException(ValidationException ex)
        {

            foreach (IValidationValidator validator in GetValidationValidators())
            {
                validator.Validate(ex);
            }
        }
        #endregion

        #region Binding
        /// <summary>
        /// Shows the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        public void Show(Object o)
        {
            if (o == null) throw new ArgumentNullException("o");

            var controls = new List<IBindable>();
            ControlHelper.FindControlsRecursive<IBindable>(Controls, controls);

            var type = o.GetType();
            foreach (var control in controls)
            {
                var propertyName = control.PropertyName;
                if (string.IsNullOrEmpty(propertyName))
                    continue;

                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    throw new InvalidOperationException("Error databinding. Property not exists " + propertyName);
                }
                control.SetValue(property.GetValue(o, null));
            }
        }

        /// <summary>
        /// Fills the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        public void Fill(Object o)
        {
            if (o == null) throw new ArgumentNullException("o");

            var controls = new List<IBindable>();
            ControlHelper.FindControlsRecursive<IBindable>(Controls, controls);

            var type = o.GetType();
            foreach (IBindable control in controls)
            {
                var propertyName = control.PropertyName;
                if (!control.CanGetValue || string.IsNullOrEmpty(propertyName))
                    continue;


                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    throw new InvalidOperationException(string.Format("Error bind property {0}. Property not found.",
                                                                      propertyName));
                }
                if (!property.CanRead)
                {
                    throw new InvalidOperationException(string.Format("Error bind property {0}. Property write only.",
                                                                      propertyName));
                }
                var value = control.GetValue();
                try
                {
                    property.SetValue(o, Convert.ChangeType(value, property.PropertyType), null);

                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format("Error bind property '{0}'. Can't set value '{1}'.",
                                                                      propertyName, value), ex);
                }
            }
        }
        #endregion

        /// <summary>
        /// Saves to session.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SaveToSession(Type type, string key, object value)
        {
            Session[type.FullName + "_" + key] = value;
        }
        /// <summary>
        /// Restores from session.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T RestoreFromSession<T>(Type type, string key)
        {
            try
            {
                return (T)Session[type.FullName + "_" + key];
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Redirects to previous page
        /// </summary>
        protected virtual void GoToPreviousPage()
        {
            if (PreviousPageUrl == null)
                return;

            Response.Redirect(PreviousPageUrl);
        }
        /// <summary>
        /// Called when register java scripts.
        /// </summary>
        protected virtual void OnRegisterJavaScripts()
        {

        }
        #endregion

        #region Overriden methods
        

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(IsPostBack) return;

            if (SSLRequired && Request.Url.Scheme == "http")
            {
                ChangeProtocol("https");
            }
            else if (!SSLRequired && Request.Url.Scheme == "https")
            {
                ChangeProtocol("http");
            }

        }

        private void ChangeProtocol(string protocol)
        {
            var builder = new UriBuilder(Request.Url)
            {
                Scheme = protocol,
                Port = -1
            };
            Response.Redirect(builder.Uri.ToString(), true);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.InitComplete"></see> event after page initialization.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (SavePreviousPageUrl && Request.UrlReferrer != null &&
                !IsPostBack && !IsAjaxRequest && !IsAjaxDataRequest)    // Insert url of previous page
            {
                PreviousPageUrl = Request.UrlReferrer.ToString();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.PreRenderComplete"/> event after the <see cref="M:System.Web.UI.Page.OnPreRenderComplete(System.EventArgs)"/> event and before the page is rendered.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            ShowStartUpAlert();

            if (IncludeDefaultCSS)
                includes.AddCss(typeof(PageEx), "Commons Css", ImagePathProvider.CommonImagesUrl + "Style.css");

            RegisterJavaScripts();

            base.OnPreRenderComplete(e);
        }

        #region F5 check
        private bool _refreshState;
        private bool _isRefresh;

        /// <summary>
        /// Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState"/> method.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Object"/> that represents the control state to be restored.</param>
        protected override void LoadViewState(object savedState)
        {
            var allStates = (Pair)savedState;
            base.LoadViewState(allStates.First);
            _refreshState = (bool)allStates.Second;
            var value = AppContext.CurrentApp.GetFromSession(typeof(PageEx), "IsRefresh"); 
            _isRefresh = _refreshState == (value == null ? false : (bool)value);
        }

        /// <summary>
        /// Saves any server control view-state changes that have occurred since the time the page was posted back to the server.
        /// </summary>
        /// <returns>
        /// Returns the server control's current view state. If there is no view state associated with the control, this method returns null.
        /// </returns>
        protected override object SaveViewState()
        {
            AppContext.CurrentApp.SaveToSession(typeof(PageEx), "IsRefresh", _refreshState);
            return new Pair(base.SaveViewState(), !_refreshState);
        }
        #endregion


        #endregion


        /// <summary>
        /// Displays startup scritp block
        /// </summary>
        private void ShowStartUpAlert()
        {
            if (HttpContext.Current.Session == null)
                return;
            if (string.IsNullOrEmpty(Session[alertMessageKey] as string))
                return;

            ShowAlert((string)Session[alertMessageKey]);
            Session.Remove(alertMessageKey);
        }


        /// <summary>
        /// Registers the java scripts.
        /// </summary>
        private void RegisterJavaScripts()
        {
            OnRegisterJavaScripts();

            PageIncludes.InitPage(this);


            //ClientScript.RegisterClientScriptInclude("JScript.js", AppSettings.ApplicationJsPath + "JScript.js");

            if(!string.IsNullOrEmpty(DefaultButton))
            {
                onLoadScriptBuilder.AppendLine(string.Format("_defaultButtonID='{0}';", DefaultButton));
            }

            var loadScript = onLoadScriptBuilder.GetScript();
            if (!string.IsNullOrEmpty(loadScript))
            {
                RegisterStartupScript(loadScript);
            }

            ScriptManager.RegisterClientScriptBlock(Page, typeof(PageEx), "PageScript", jScript.ToString(), true);
        }

        /// <summary>
        /// Registers the startup script.
        /// </summary>
        /// <param name="loadScript">The load script.</param>
        protected virtual void RegisterStartupScript(string loadScript)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(PageEx), "PageStartUp", loadScript, true);
        }
    }
}
