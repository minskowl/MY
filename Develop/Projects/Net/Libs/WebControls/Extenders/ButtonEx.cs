using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    [DefaultProperty("Text")]
    [DefaultEvent("Click")]
    [ToolboxData("<{0}:ButtonEx runat=server></{0}:ButtonEx>")]
    [SupportsEventValidation]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ButtonEx : WebControl, IButtonControl, IPostBackEventHandler, IPostBackDataHandler
    {
        private static readonly object EventCommand;
        private static readonly object EventClick;

        /// <summary>
        /// ButtonType
        /// </summary>
        public enum ButtonType
        {
            /// <summary>
            /// InputButton
            /// </summary>
            InputButton,
            /// <summary>
            /// Button
            /// </summary>
            Button,
            /// <summary>
            /// ImageButton
            /// </summary>
            ImageButton,
            /// <summary>
            /// Link
            /// </summary>
            Link
        }

        [Category("Action")]
        [DescriptionAttribute("Click Event")]
        public event EventHandler Click
        {
            add { Events.AddHandler(EventClick, value); }
            remove { Events.RemoveHandler(EventClick, value); }
        }

        /// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button"></see> control is clicked.</summary>
        [Category("Action")]
        public event CommandEventHandler Command
        {
            add { Events.AddHandler(EventCommand, value); }
            remove { Events.RemoveHandler(EventCommand, value); }
        }

        #region Properties

        #region Appearance

        /// <summary>
        /// Gets or sets the image align.
        /// </summary>
        /// <value>The image align.</value>
        [Description("ImageAlign"), DefaultValue(0), Category("Appearance")]
        public virtual ImageAlign ImageAlign
        {
            get
            {
                object obj2 = ViewState["ImageAlign"];
                if (obj2 != null)
                {
                    return (ImageAlign)obj2;
                }
                return ImageAlign.NotSet;
            }
            set
            {
                if ((value < ImageAlign.NotSet) || (value > ImageAlign.TextTop))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                ViewState["ImageAlign"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public ButtonType Mode
        {
            get
            {
                var value = ViewState["Mode"];
                return ((value == null) ? ButtonType.InputButton : (ButtonType)value);
            }

            set { ViewState["Mode"] = value; }
        }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [EditorAttribute(typeof(UrlEditor), typeof(UITypeEditor))]
        public String ImageUrl
        {
            get
            {
                var value = ViewState["ImageUrl"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set { ViewState["ImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get { return (String)ViewState["Text"] ?? String.Empty; }

            set { ViewState["Text"] = value; }
        }


        #endregion

        #region Behavior

        /// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Button"></see> control uses the client browser's submit mechanism or the ASP.NET postback mechanism.</summary>
        /// <returns>true if the control uses the client browser's submit mechanism; otherwise, false. The default is true.</returns>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(true)]
        public virtual bool UseSubmitBehavior
        {
            get
            {
                object obj1 = ViewState["UseSubmitBehavior"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }
            set { ViewState["UseSubmitBehavior"] = value; }
        }


        /// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button"></see> control's <see cref="E:System.Web.UI.WebControls.Button.Click"></see> event is raised.</summary>
        /// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button"></see> control's <see cref="E:System.Web.UI.WebControls.Button.Click"></see> event is raised.</returns>
        [Category("Behavior")]
        [DefaultValue("")]
        [Themeable(false)]
        public virtual string OnClientClick
        {
            get
            {
                string text1 = (string)ViewState["OnClientClick"];
                if (text1 == null)
                {
                    return string.Empty;
                }
                return text1;
            }
            set { ViewState["OnClientClick"] = value; }
        }


        /// <summary>
        /// Gets or sets the command argument.
        /// </summary>
        /// <value>The command argument.</value>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CommandArgument
        {
            get
            {
                return ((String)ViewState["CommandArgument"] ?? String.Empty);
            }

            set { ViewState["CommandArgument"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CommandName
        {
            get
            {
                return ((String)ViewState["CommandName"] ?? String.Empty);
            }

            set { ViewState["CommandName"] = value; }
        }

        /// <summary>
        /// Gets or sets the validation group.
        /// </summary>
        /// <value>The validation group.</value>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public virtual string ValidationGroup
        {
            get
            {
                return ((String)ViewState["ValidationGroup"] ?? String.Empty);
            }

            set { ViewState["ValidationGroup"] = value; }
        }

        /// <summary>
        /// Gets or sets the command argument.
        /// </summary>
        /// <value>The command argument.</value>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Localizable(true)]
        public bool CausesValidation
        {
            get
            {
                var s = ViewState["CausesValidation"];
                return ((s == null) ? true : (bool)s);
            }

            set { ViewState["CausesValidation"] = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [send argument].
        /// </summary>
        /// <value><c>true</c> if [send argument]; otherwise, <c>false</c>.</value>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool SendArgument
        {
            get
            {
                var s = ViewState["SendArgument"];
                return ((s == null) ? false : (bool)s);
            }

            set { ViewState["SendArgument"] = value; }
        }

        /// <summary>
        /// Gets or sets the post back URL.
        /// </summary>
        /// <value>The post back URL.</value>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        [EditorAttribute(typeof(UrlEditor), typeof(UITypeEditor))]
        public String PostBackUrl
        {
            get
            {
                object value = ViewState["PostBackUrl"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set { ViewState["PostBackUrl"] = value; }
        }

        /// <summary>
        /// Specifies url if button is rendered as link
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        [EditorAttribute(typeof(UrlEditor), typeof(UITypeEditor))]
        public String NavigateUrl
        {
            get { return ViewState["NavigateUrl"] as string; }
            set { ViewState["NavigateUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        public String Target
        {
            get { return ViewState["Target"] as string; }
            set { ViewState["Target"] = value; }
        }

        #endregion

        #endregion

        /// <summary>
        /// Initializes the <see cref="ButtonEx"/> class.
        /// </summary>
        static ButtonEx()
        {
            EventClick = new object();
            EventCommand = new object();
        }

 
        #region Render Overides

        /// <summary>
        /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"></see> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"></see> enumeration values.</returns>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                switch (Mode)
                {
                    case ButtonType.InputButton:
                        return HtmlTextWriterTag.Input;
                    case ButtonType.Button:
                         return HtmlTextWriterTag.Button;
                    case ButtonType.ImageButton:
                         return HtmlTextWriterTag.Input;
                    case ButtonType.Link:
                         return HtmlTextWriterTag.A;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriterTag"></see>. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            try
            {
                if (Page != null)
                {
                    Page.VerifyRenderingInServerForm(this);
                }

                var mode = Mode;
                if (mode == ButtonType.InputButton || mode == ButtonType.Button)
                {
                    AddAttributesToRenderForTypeButton(writer);
                }
                else if (mode == ButtonType.Link)
                {
                    AddAttributesToRenderForTypeLink(writer);
                }
                else
                {
                    AddAttributesToRenderForTypeImageButton(writer);
                }
            }
            catch (Exception ex)
            {
                Util.Log.Error("ButtonEx:: AddAttributesToRender", ex);
            }
        }


        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (Mode != ButtonType.Link)
            {
                return;
            }

            string imageUrl = ImageUrl;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                var image = new ImageEx
                                {
                                    ImageUrl = imageUrl,
                                    AlternateText = ToolTip,
                                    ImageAlign = ImageAlign,
                                    AutoDetectSize = true
                                };

                Controls.Add(image);
            }

            //string text = Text;
            //if (!string.IsNullOrEmpty(text))
            //{
            //    Literal literal = new Literal();
            //    if (hasImage)
            //        literal.Text = " " + text;
            //    else
            //        literal.Text = text;
            //    Controls.Add(literal);
            //}
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            //try
            //{
            base.RenderContents(writer);

            if (Mode != ButtonType.Link)
                return;

            string text = Text;
            if (string.IsNullOrEmpty(text))
                return;

            if (!string.IsNullOrEmpty(ImageUrl))
            {
                writer.Write("&nbsp;");
            }
            writer.Write(text);
            //}
            //catch (Exception ex)
            //{
            //    Log.BusinessLayer.Error("ButtonEx.RenderContents", ex);
            //}
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Mode == ButtonType.ImageButton && Page != null)
            {
                Page.RegisterRequiresPostBack(this);
                //if (base.IsEnabled && ((this.CausesValidation && (this.Page.GetValidators(this.ValidationGroup).Count > 0)) || !string.IsNullOrEmpty(this.PostBackUrl)))
                //{
                //    this.Page.RegisterWebFormsScript();
                //}
            }
        }

        #endregion

        /// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Button.Click"></see> event of the <see cref="T:System.Web.UI.WebControls.Button"></see> control.</summary>
        /// <param name="e">A <see cref="T:System.EventArgs"></see> that contains the event data. </param>
        protected virtual void OnClick(EventArgs e)
        {
            var handler1 = (EventHandler)Events[EventClick];
            if (handler1 != null)
            {
                handler1(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Command"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCommand(CommandEventArgs e)
        {
            var handler1 = (CommandEventHandler)Events[EventCommand];
            if (handler1 != null)
            {
                handler1(this, e);
            }
            RaiseBubbleEvent(this, e);
        }

        #region Helpers

        #region AddAttributesToRender

        private void AddAttributesToRenderForTypeButton(HtmlTextWriter writer)
        {
            bool useSubmitBehavior = UseSubmitBehavior;

            writer.AddAttribute(HtmlTextWriterAttribute.Type, useSubmitBehavior ? "submit" : "button");

            var postBackOptions = GetPostBackOptionsForTypeButton();

            string uniqueId = UniqueID;
            if (uniqueId != null && (postBackOptions == null || postBackOptions.TargetControl == this))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueId);
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Text);

            bool isEnabled = IsEnabled;
            string onClientClick = string.Empty;

            if (isEnabled)
            {
                onClientClick = Util.EnsureEndWithSemiColon(OnClientClick);
                if (HasAttributes)
                {
                    string onclickScript = Attributes["onclick"];
                    if (onclickScript != null)
                    {
                        onClientClick = onClientClick + Util.EnsureEndWithSemiColon(onclickScript);
                        Attributes.Remove("onclick");
                    }
                }
                if (Page != null)
                {
                    string text4 = Page.ClientScript.GetPostBackEventReference(postBackOptions, false);
                    if (text4 != null)
                    {
                        onClientClick = Util.MergeScript(onClientClick, text4);
                    }
                }
            }
            if (Page != null)
            {
                Page.ClientScript.RegisterForEventValidation(postBackOptions);
            }
            if (onClientClick.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClientClick);
                //if (base.EnableLegacyRendering)
                //{
                //    writer.AddAttribute("language", "javascript", false);
                //}
            }
            if (Enabled && !isEnabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            }

            base.AddAttributesToRender(writer);
        }


        /// <summary>
        /// Adds the attributes to render for type link.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void AddAttributesToRenderForTypeLink(HtmlTextWriter writer)
        {
            string onClientClick = Util.EnsureEndWithSemiColon(OnClientClick);
            if (HasAttributes)
            {
                string text2 = Attributes["onclick"];
                if (text2 != null)
                {
                    onClientClick = onClientClick + Util.EnsureEndWithSemiColon(text2);
                    Attributes.Remove("onclick");
                }
            }
            if (onClientClick.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClientClick);
            }
            bool isEnabled = IsEnabled;
            if (Enabled && !isEnabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            }


            if (isEnabled && (Page != null))
            {
                PostBackOptions postBackOptions = GetPostBackOptionsForTypeLink();
                string href = null;
                if (postBackOptions != null && UseSubmitBehavior)
                {
                    Page.ClientScript.RegisterForEventValidation(postBackOptions);
                    href = Page.ClientScript.GetPostBackEventReference(postBackOptions, true);

                }
                if (!UseSubmitBehavior && !(string.IsNullOrEmpty(NavigateUrl)))
                    href = NavigateUrl;

                if (string.IsNullOrEmpty(href))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0);");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, href);
                    if (!string.IsNullOrEmpty(Target))
                        writer.AddAttribute(HtmlTextWriterAttribute.Target, Target);
                }
            }
            base.AddAttributesToRender(writer);
        }


        private void AddAttributesToRenderForTypeImageButton(HtmlTextWriter writer)
        {
            Page page = Page;

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "image");

            string text1 = UniqueID;
            PostBackOptions postBackOptions = GetPostBackOptionsForTypeImageButton();
            if ((text1 != null) && ((postBackOptions == null) || (postBackOptions.TargetControl == this)))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, text1);
            }
            bool isEnabled = IsEnabled;
            string onClientClickScript = string.Empty;
            if (isEnabled)
            {
                onClientClickScript = Util.EnsureEndWithSemiColon(OnClientClick);
                if (HasAttributes)
                {
                    string text3 = Attributes["onclick"];
                    if (text3 != null)
                    {
                        onClientClickScript = onClientClickScript + Util.EnsureEndWithSemiColon(text3);
                        Attributes.Remove("onclick");
                    }
                }
            }
            if (Enabled && !isEnabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            }
            base.AddAttributesToRender(writer);
            if ((page != null) && (postBackOptions != null))
            {
                page.ClientScript.RegisterForEventValidation(postBackOptions);
                if (isEnabled)
                {
                    string text4 = page.ClientScript.GetPostBackEventReference(postBackOptions, false);
                    if (!string.IsNullOrEmpty(text4))
                    {
                        onClientClickScript = Util.MergeScript(onClientClickScript, text4);
                    }
                }
            }
            if (onClientClickScript.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClientClickScript);
                //if (base.EnableLegacyRendering)
                //{
                //    writer.AddAttribute("language", "javascript", false);
                //}
            }
            if (page != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src,
                                    ControlHelper.GetFullImageUrl(ImageUrl, page));
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, ImageUrl);
            }
        }

        #endregion

        private PostBackOptions GetPostBackOptionsForTypeButton()
        {
            var options = CreateOptions();
            options.ClientSubmit = false;
            if (Page == null)
                return options;


            if (CausesValidation && (Page.GetValidators(ValidationGroup).Count > 0))
            {
                options.PerformValidation = true;
                options.ValidationGroup = ValidationGroup;
            }
            if (!string.IsNullOrEmpty(PostBackUrl))
            {
                options.ActionUrl = HttpUtility.UrlPathEncode(ResolveClientUrl(PostBackUrl));
            }
            //options.ClientSubmit = !UseSubmitBehavior;

            return options;
        }

        private PostBackOptions GetPostBackOptionsForTypeImageButton()
        {
            var result = CreateOptions();
            result.ClientSubmit = false;
            if (!string.IsNullOrEmpty(PostBackUrl))
            {
                result.ActionUrl = HttpUtility.UrlPathEncode(ResolveClientUrl(PostBackUrl));
            }
            if ((CausesValidation && (Page != null)) && (Page.GetValidators(ValidationGroup).Count > 0))
            {
                result.PerformValidation = true;
                result.ValidationGroup = ValidationGroup;
            }
            return result;
        }

        private PostBackOptions GetPostBackOptionsForTypeLink()
        {
            var result = CreateOptions();

            result.RequiresJavaScriptProtocol = true;
            if (!string.IsNullOrEmpty(PostBackUrl))
            {
                result.ActionUrl = HttpUtility.UrlPathEncode(ResolveClientUrl(PostBackUrl));
                if ((!DesignMode && (Page != null)) &&
                    string.Equals(Page.Request.Browser.Browser, "IE", StringComparison.OrdinalIgnoreCase))
                {
                    result.ActionUrl = Util.QuoteJScriptString(result.ActionUrl, true);
                }
            }
            if (CausesValidation && (Page.GetValidators(ValidationGroup).Count > 0))
            {
                result.PerformValidation = true;
                result.ValidationGroup = ValidationGroup;
            }

            return result;
        }

        private PostBackOptions CreateOptions()
        {
            var result = new PostBackOptions(this);

            if (SendArgument &&
                (!string.IsNullOrEmpty(CommandName) || string.IsNullOrEmpty(CommandArgument)))
                result.Argument = CommandName + "::" + CommandArgument;

            return result;
        }

        #endregion

        #region IPostBackEventHandler

        /// <summary>
        /// When implemented by a class, enables a server control to process an event raised when a form is posted to the server.
        /// </summary>
        /// <param name="eventArgument">A <see cref="T:System.String"></see> that represents an optional event argument to be passed to the event handler.</param>
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            RaisePostBackEvent(eventArgument);
        }

        /// <summary>
        /// Enables a server control to process an event raised when a form is posted to the server.
        /// </summary>
        /// <param name="eventArgument">A <see cref="T:System.String"></see> that represents an optional event argument to be passed to the event handler.</param>
        protected virtual void RaisePostBackEvent(string eventArgument)
        {
            //ValidateEvent(this.UniqueID, eventArgument);
            Page.ClientScript.ValidateEvent(UniqueID, eventArgument);
            if (CausesValidation)
                Page.Validate(ValidationGroup);

            OnClick(EventArgs.Empty);
            if (string.IsNullOrEmpty(eventArgument))
            {
                OnCommand(new CommandEventArgs(CommandName, CommandArgument));
            }
            else
            {
                var parts = eventArgument.Split(new string[] { "::" }, StringSplitOptions.None);
                OnCommand(new CommandEventArgs(parts[0], parts[1]));
            }
        }

        #endregion

        #region IPostBackDataHandler

        /// <summary>
        /// When implemented by a class, processes postback data for an ASP.NET server control.
        /// </summary>
        /// <param name="postDataKey">The key identifier for the control.</param>
        /// <param name="postCollection">The collection of all incoming name values.</param>
        /// <returns>
        /// true if the server control's state changes as a result of the postback; otherwise, false.
        /// </returns>
        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return LoadPostData(postDataKey, postCollection);
        }

        /// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.ImageButton"></see> control.</summary>
        /// <returns>Returns false for all cases.</returns>
        /// <param name="postDataKey">The key value used to index an entry in the collection. </param>
        /// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection"></see> that contains post information.</param>
        protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            if (Page != null)
            {
                if (Mode == ButtonType.Button || Mode == ButtonType.InputButton)
                {
                    Page.RegisterRequiresRaiseEvent(this);
                }
                else if (Mode == ButtonType.ImageButton && postCollection[UniqueID + ".x"] != null)
                {
                    Page.RegisterRequiresRaiseEvent(this);
                }
            }
            return false;
        }


        /// <summary>
        /// Signals the server control to notify the ASP.NET application that the state of the control has changed.
        /// </summary>
        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            RaisePostDataChangedEvent();
        }

        /// <summary>
        /// Signals the server control to notify the ASP.NET application that the state of the control has changed.
        /// </summary>
        protected virtual void RaisePostDataChangedEvent()
        {
        }

        #endregion
    }
}
