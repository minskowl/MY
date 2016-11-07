using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    public enum LanguageDirection
    {
        LeftToRight,
        RightToLeft
    }

    [DefaultProperty("Value")]
    [ValidationProperty("Value")]
    [ToolboxData("<{0}:HtmlEditor runat=server></{0}:HtmlEditor>")]
    [Designer("Savchin.Web.UI.FCKeditorDesigner")]
    [ParseChildren(false)]
    public class HtmlEditorControl : Control, IPostBackDataHandler
    {
        private bool _IsCompatible;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlEditorControl"/> class.
        /// </summary>
        public HtmlEditorControl()
        {
        }

        #region Base Configurations Properties

        [Browsable(false)]
        public FCKeditorConfigurations Config
        {
            get
            {
                if (ViewState["Config"] == null)
                    ViewState["Config"] = new FCKeditorConfigurations();
                return (FCKeditorConfigurations) ViewState["Config"];
            }
        }

        [DefaultValue("")]
        public string Value
        {
            get
            {
                object o = ViewState["Value"];
                return (o == null ? "" : (string) o);
            }
            set { ViewState["Value"] = value; }
        }

        /// <summary>
        /// <p>
        ///		Sets or gets the virtual path to the editor's directory. It is
        ///		relative to the current page.
        /// </p>
        /// <p>
        ///		The default value is "/fckeditor/".
        /// </p>
        /// <p>
        ///		The base path can be also set in the Web.config file using the 
        ///		appSettings section. Just set the "FCKeditor:BasePath" for that. 
        ///		For example:
        ///		<code>
        ///		&lt;configuration&gt;
        ///			&lt;appSettings&gt;
        ///				&lt;add key="FCKeditor:BasePath" value="/scripts/fckeditor/" /&gt;
        ///			&lt;/appSettings&gt;
        ///		&lt;/configuration&gt;
        ///		</code>
        /// </p>
        /// </summary>
        [DefaultValue("/fckeditor/")]
        public string BasePath
        {
            get
            {
                object o = ViewState["BasePath"];

                if (o == null)
                    o = System.Configuration.ConfigurationSettings.AppSettings["FCKeditor:BasePath"];

                return (o == null ? "/fckeditor/" : (string) o);
            }
            set { ViewState["BasePath"] = value; }
        }

        [DefaultValue("Default")]
        public string ToolbarSet
        {
            get
            {
                object o = ViewState["ToolbarSet"];
                return (o == null ? "Default" : (string) o);
            }
            set { ViewState["ToolbarSet"] = value; }
        }

        #endregion

        #region Appearence Properties

        [Category("Appearence")]
        [DefaultValue("100%")]
        public Unit Width
        {
            get
            {
                object o = ViewState["Width"];
                return (o == null ? Unit.Percentage(100) : (Unit) o);
            }
            set { ViewState["Width"] = value; }
        }

        [Category("Appearence")]
        [DefaultValue("200px")]
        public Unit Height
        {
            get
            {
                object o = ViewState["Height"];
                return (o == null ? Unit.Pixel(200) : (Unit) o);
            }
            set { ViewState["Height"] = value; }
        }

        #endregion

        #region Configurations Properties

        [Category("Configurations")]
        public string CustomConfigurationsPath
        {
            set { Config["CustomConfigurationsPath"] = value; }
        }

        [Category("Configurations")]
        public string EditorAreaCSS
        {
            set { Config["EditorAreaCSS"] = value; }
        }

        [Category("Configurations")]
        public string BaseHref
        {
            set { Config["BaseHref"] = value; }
        }

        [Category("Configurations")]
        public string SkinPath
        {
            set { Config["SkinPath"] = value; }
        }

        [Category("Configurations")]
        public string PluginsPath
        {
            set { Config["PluginsPath"] = value; }
        }

        [Category("Configurations")]
        public bool FullPage
        {
            set { Config["FullPage"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool Debug
        {
            set { Config["Debug"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool AutoDetectLanguage
        {
            set { Config["AutoDetectLanguage"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public string DefaultLanguage
        {
            set { Config["DefaultLanguage"] = value; }
        }

        [Category("Configurations")]
        public LanguageDirection ContentLangDirection
        {
            set { Config["ContentLangDirection"] = (value == LanguageDirection.LeftToRight ? "ltr" : "rtl"); }
        }

        [Category("Configurations")]
        public bool EnableXHTML
        {
            set { Config["EnableXHTML"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool EnableSourceXHTML
        {
            set { Config["EnableSourceXHTML"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool FillEmptyBlocks
        {
            set { Config["FillEmptyBlocks"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool FormatSource
        {
            set { Config["FormatSource"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool FormatOutput
        {
            set { Config["FormatOutput"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public string FormatIndentator
        {
            set { Config["FormatIndentator"] = value; }
        }

        [Category("Configurations")]
        public bool GeckoUseSPAN
        {
            set { Config["GeckoUseSPAN"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool StartupFocus
        {
            set { Config["StartupFocus"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool ForcePasteAsPlainText
        {
            set { Config["ForcePasteAsPlainText"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool ForceSimpleAmpersand
        {
            set { Config["ForceSimpleAmpersand"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public int TabSpaces
        {
            set { Config["TabSpaces"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        [Category("Configurations")]
        public bool UseBROnCarriageReturn
        {
            set { Config["UseBROnCarriageReturn"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool ToolbarStartExpanded
        {
            set { Config["ToolbarStartExpanded"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool ToolbarCanCollapse
        {
            set { Config["ToolbarCanCollapse"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public string FontColors
        {
            set { Config["FontColors"] = value; }
        }

        [Category("Configurations")]
        public string FontNames
        {
            set { Config["FontNames"] = value; }
        }

        [Category("Configurations")]
        public string FontSizes
        {
            set { Config["FontSizes"] = value; }
        }

        [Category("Configurations")]
        public string FontFormats
        {
            set { Config["FontFormats"] = value; }
        }

        [Category("Configurations")]
        public string StylesXmlPath
        {
            set { Config["StylesXmlPath"] = value; }
        }

        [Category("Configurations")]
        public string LinkBrowserURL
        {
            set { Config["LinkBrowserURL"] = value; }
        }

        [Category("Configurations")]
        public string ImageBrowserURL
        {
            set { Config["ImageBrowserURL"] = value; }
        }

        [Category("Configurations")]
        public bool HtmlEncodeOutput
        {
            set { Config["HtmlEncodeOutput"] = (value ? "true" : "false"); }
        }

        //NOTE: SVD Width FIX
        /// <summary>
        /// Get id of container div
        /// </summary>
        private string ContainerDivId
        {
            get { return ClientID + "ContainerDiv"; }
        }
        /// <summary>
        /// Specifies if iframe should occupy area of container div
        /// </summary>
        public bool AdjustToContainerDiv
        {
            get
            {
                if (ViewState["AdjustToContainerDiv"] == null)
                    ViewState["AdjustToContainerDiv"] = false;
                return (bool)ViewState["AdjustToContainerDiv"];
            }
            set
            {
                ViewState["AdjustToContainerDiv"] = value;
            }
        }
        #endregion

        #region Rendering

        public string CreateHtml()
        {
            System.IO.StringWriter strWriter = new System.IO.StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(strWriter);
            Render(writer);
            return strWriter.ToString();
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            //NOTE: SVD Width FIX
            writer.Write("<div id=\"{0}\" width=\"{1}\" height=\"{2}\">",
                ContainerDivId,
                Width,
                Height);

            if (_IsCompatible)
            {
                string sLink = BasePath;
                if (sLink.StartsWith("~"))
                    sLink = ResolveUrl(sLink);

                string sFile =
                    System.Web.HttpContext.Current.Request.QueryString["fcksource"] == "true"
                        ?
                            "fckeditor.original.html"
                        :
                            "fckeditor.html";

                sLink += "editor/" + sFile + "?InstanceName=" + ClientID;
                if (ToolbarSet.Length > 0) sLink += "&amp;Toolbar=" + ToolbarSet;

                // Render the linked hidden field.
                writer.Write(
                    "<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />",
                    ClientID,
                    UniqueID,
                    System.Web.HttpUtility.HtmlEncode(Value));

                // Render the configurations hidden field.
                writer.Write(
                    "<input type=\"hidden\" id=\"{0}___Config\" value=\"{1}\" />",
                    ClientID,
                    Config.GetHiddenFieldString());

                // Render the editor IFRAME.
                writer.Write(
                    "<iframe id=\"{0}___Frame\" src=\"{1}\" width=\"{2}\" height=\"{3}\" frameborder=\"no\" scrolling=\"no\"></iframe>",
                    ClientID,
                    sLink,
                    Width,
                    Height);
            }
            else
            {
                writer.Write(
                    "<textarea name=\"{0}\" rows=\"4\" cols=\"40\" style=\"width: {1}; height: {2}\" wrap=\"virtual\">{3}</textarea>",
                    UniqueID,
                    Width,
                    Height,
                    System.Web.HttpUtility.HtmlEncode(Value));
            }

            writer.Write("</div>");
        }

        /// <summary>
        /// Registers resize java script
        /// </summary>
        private void RegisterResizeScript()
        {
            if (AdjustToContainerDiv)
                Page.ClientScript.RegisterStartupScript(typeof(HtmlEditorControl), "SUmmaryResizeScript",
@"
var iFrameObj = document.getElementById('" + ClientID + @"___Frame'); 
var containerDivObj=document.getElementById('" + ContainerDivId + @"');
if (iFrameObj!=null && containerDivObj !=null && containerDivObj.parentNode.offsetHeight>0) 
    iFrameObj.height = containerDivObj.parentNode.offsetHeight;
",
                    true);
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterResizeScript();
            _IsCompatible = CheckBrowserCompatibility();

            if (!_IsCompatible)
                return;

            object oScriptManager = null;

            // Search for the ScriptManager control in the page.
            Control oParent = Parent;
            while (oParent != null)
            {
                foreach (object control in oParent.Controls)
                {
                    // Match by type name.
                    if (control.GetType().FullName == "System.Web.UI.ScriptManager")
                    {
                        oScriptManager = control;
                        break;
                    }
                }

                if (oScriptManager != null)
                    break;

                oParent = oParent.Parent;
            }

            // If the ScriptManager control is available.
            if (oScriptManager != null)
            {
                try
                {
                    // Use reflection to check the SupportsPartialRendering
                    // property value.
                    bool bSupportsPartialRendering =
                        ((bool)
                         (oScriptManager.GetType().GetProperty("SupportsPartialRendering").GetValue(oScriptManager, null)));

                    if (bSupportsPartialRendering)
                    {
                        string sScript = "(function()\n{\n" +
                                         "\tvar editor = FCKeditorAPI.GetInstance('" + ClientID + "');\n" +
                                         "\tif (editor)\n" +
                                         "\t\teditor.UpdateLinkedField();\n" +
                                         "})();\n";

                        // Call the RegisterOnSubmitStatement method through
                        // reflection.
                        oScriptManager.GetType().GetMethod("RegisterOnSubmitStatement",
                                                           new Type[]
                                                               {
                                                                   typeof (Control), typeof (Type), typeof (String),
                                                                   typeof (String)
                                                               }).Invoke(oScriptManager, new object[]
                                                                                             {
                                                                                                 this,
                                                                                                 GetType
                                                                                                     (),
                                                                                                 "FCKeditorAjaxOnSubmit_" +
                                                                                                 ClientID
                                                                                                 ,
                                                                                                 sScript
                                                                                             });

                        // Tell the editor that we are handling the submit.
                        Config["PreventSubmitHandler"] = "true";
                    }
                }
                catch
                {
                }
            }
        }

        #endregion

        #region Compatibility Check

        public bool CheckBrowserCompatibility()
        {
            return IsCompatibleBrowser();
        }

        /// <summary>
        /// Checks if the current HTTP request comes from a browser compatible
        /// with FCKeditor.
        /// </summary>
        /// <returns>"true" if the browser is compatible.</returns>
        public static bool IsCompatibleBrowser()
        {
            return IsCompatibleBrowser(System.Web.HttpContext.Current.Request);
        }

        /// <summary>
        /// Checks if the provided HttpRequest object comes from a browser
        /// compatible with FCKeditor.
        /// </summary>
        /// <returns>"true" if the browser is compatible.</returns>
        public static bool IsCompatibleBrowser(System.Web.HttpRequest request)
        {
            System.Web.HttpBrowserCapabilities oBrowser = request.Browser;

            // Internet Explorer 5.5+ for Windows
            if (oBrowser.Browser == "IE" &&
                (oBrowser.MajorVersion >= 6 || (oBrowser.MajorVersion == 5 && oBrowser.MinorVersion >= 0.5)) &&
                oBrowser.Win32)
                return true;

            string sUserAgent = request.UserAgent;

            if (sUserAgent.IndexOf("Gecko/") >= 0)
            {
                Match oMatch = Regex.Match(request.UserAgent, @"(?<=Gecko/)\d{8}");
                return (oMatch.Success && int.Parse(oMatch.Value, CultureInfo.InvariantCulture) >= 20030210);
            }

            if (sUserAgent.IndexOf("Opera/") >= 0)
            {
                Match oMatch = Regex.Match(request.UserAgent, @"(?<=Opera/)[\d\.]+");
                return (oMatch.Success && float.Parse(oMatch.Value, CultureInfo.InvariantCulture) >= 9.5);
            }

            if (sUserAgent.IndexOf("AppleWebKit/") >= 0)
            {
                Match oMatch = Regex.Match(request.UserAgent, @"(?<=AppleWebKit/)\d+");
                return (oMatch.Success && int.Parse(oMatch.Value, CultureInfo.InvariantCulture) >= 522);
            }

            return false;
        }

        #endregion

        #region Postback Handling

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            string postedValue = postCollection[postDataKey];

            // Revert the HtmlEncodeOutput changes.
            if (Config["HtmlEncodeOutput"] != "false")
            {
                postedValue = postedValue.Replace("&lt;", "<");
                postedValue = postedValue.Replace("&gt;", ">");
                postedValue = postedValue.Replace("&amp;", "&");
            }

            if (postedValue != Value)
            {
                Value = postedValue;
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            // Do nothing
        }

        #endregion
    }
}