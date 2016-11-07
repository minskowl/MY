using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;

namespace Savchin.Web.UI
{
    [DefaultProperty("Text")]
    public class CaptchaControl : System.Web.UI.WebControls.WebControl, INamingContainer, IPostBackDataHandler, IValidator
    {
        #region Properties

        public enum Layout
        {
            Horizontal,
            Vertical
        }

        public enum CacheType
        {
            HttpRuntime,
            Session
        }

        private int _timeoutSecondsMax = 90;
        private int _timeoutSecondsMin = 3;
        private bool _userValidated = true;
        private string _text = "Enter the code shown";
        private string _font = "";
        private CaptchaImage _captcha = new CaptchaImage();
        private Layout _layoutStyle = Layout.Horizontal;
        private string _prevguid;
        private string _errorMessage = "";

        private CacheType _cacheStrategy = CacheType.HttpRuntime;

        #region   Public Properties

        /// <summary>
        /// are we in design mode?
        /// </summary>
        private bool IsDesignMode
        {
            get { return HttpContext.Current == null; }
        }

        [Browsable(false), Bindable(true),
        Category("Appearance"),
        DefaultValue("The text you typed does not match the text in the image."),
        Description("Message to display in a Validation Summary when the CAPTCHA fails to validate.")]
        public string ErrorMessage
        {
            get
            {
                if (!_userValidated)
                {
                    return _errorMessage;
                }
                else
                {
                    return "";
                }
            }
            set { _errorMessage = value; }
        }

        [Browsable(false), Category("Behavior"),
        DefaultValue(true), Description("Is Valid"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValid
        {
            get { return _userValidated; }
            set { }
        }

        public override bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                // When a validator is disabled, generally, the intent is not to
                // make the page invalid for that round trip.
                if (!value)
                {
                    _userValidated = true;
                }
            }
        }


        [DefaultValue("Enter the code shown above"), Description("Instructional text displayed next to CAPTCHA image."), Category("Appearance")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        [DefaultValue(typeof(Layout), "Horizontal"),
        Description("Determines if image and input area are displayed horizontally, or vertically."), Category("Captcha")]
        public Layout LayoutStyle
        {
            get { return _layoutStyle; }
            set { _layoutStyle = value; }
        }

        [DefaultValue(typeof(CacheType), "HttpRuntime"), Description("Determines if CAPTCHA codes are stored in HttpRuntime (fast, but local to current server) or Session (more portable across web farms)."), Category("Captcha")]
        public CacheType CacheStrategy
        {
            get { return _cacheStrategy; }
            set { _cacheStrategy = value; }
        }

        [Description("Returns True if the user was CAPTCHA validated after a postback."), Category("Captcha")]
        public bool UserValidated
        {
            get { return _userValidated; }
        }


        [DefaultValue(""), Description("Font used to render CAPTCHA text. If font name is blank, a random font will be chosen."), Category("Captcha")]
        public string CaptchaFont
        {
            get { return _font; }
            set
            {
                _font = value;
                _captcha.Font = _font;
            }
        }

        [DefaultValue(""), Description("Characters used to render CAPTCHA text. A character will be picked randomly from the string."), Category("Captcha")]
        public string CaptchaChars
        {
            get { return _captcha.TextChars; }
            set { _captcha.TextChars = value; }
        }

        [DefaultValue(5),
        Description("Number of CaptchaChars used in the CAPTCHA text"),
        Category("Captcha")]
        public int CaptchaLength
        {
            get { return _captcha.TextLength; }
            set { _captcha.TextLength = value; }
        }

        [DefaultValue(2), Description("Minimum number of seconds CAPTCHA must be displayed before it is valid. If you're too fast, you must be a robot. Set to zero to disable."), Category("Captcha")]
        public int CaptchaMinTimeout
        {
            get { return _timeoutSecondsMin; }
            set
            {
                if (value > 15)
                {
                    throw new ArgumentOutOfRangeException("CaptchaTimeout", "Timeout must be less than 15 seconds. Humans aren't that slow!");
                }
                _timeoutSecondsMin = value;
            }
        }

        [DefaultValue(90), Description("Maximum number of seconds CAPTCHA will be cached and valid. If you're too slow, you may be a CAPTCHA hack attempt. Set to zero to disable."), Category("Captcha")]
        public int CaptchaMaxTimeout
        {
            get { return _timeoutSecondsMax; }
            set
            {
                if (value < 15 & value != 0)
                {
                    throw new ArgumentOutOfRangeException("CaptchaTimeout", "Timeout must be greater than 15 seconds. Humans can't type that fast!");
                }
                _timeoutSecondsMax = value;
            }
        }

        [DefaultValue(50), Description("Height of generated CAPTCHA image."), Category("Captcha")]
        public int CaptchaHeight
        {
            get { return _captcha.Height; }
            set { _captcha.Height = value; }
        }

        [DefaultValue(180), Description("Width of generated CAPTCHA image."), Category("Captcha")]
        public int CaptchaWidth
        {
            get { return _captcha.Width; }
            set { _captcha.Width = value; }
        }

        [DefaultValue(typeof(CaptchaImage.FontWarpFactor), "Low"), Description("Amount of random font warping used on the CAPTCHA text"), Category("Captcha")]
        public CaptchaImage.FontWarpFactor CaptchaFontWarping
        {
            get { return _captcha.FontWarp; }
            set { _captcha.FontWarp = value; }
        }

        [DefaultValue(typeof(CaptchaImage.BackgroundNoiseLevel), "Low"), Description("Amount of background noise to generate in the CAPTCHA image"), Category("Captcha")]
        public CaptchaImage.BackgroundNoiseLevel CaptchaBackgroundNoise
        {
            get { return _captcha.BackgroundNoise; }
            set { _captcha.BackgroundNoise = value; }
        }

        [DefaultValue(typeof(CaptchaImage.LineNoiseLevel), "None"), Description("Add line noise to the CAPTCHA image"), Category("Captcha")]
        public CaptchaImage.LineNoiseLevel CaptchaLineNoise
        {
            get { return _captcha.LineNoise; }
            set { _captcha.LineNoise = value; }
        }
        #endregion

        #endregion

        #region Interface
        void IValidator.Validate()
        {
            //-- a no-op, since we validate in LoadPostData
        }


        /// <summary>
        /// Retrieve the user's CAPTCHA input from the posted data
        /// </summary>
        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection values)
        {
            ValidateCaptcha(Convert.ToString(values[UniqueID]));
            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {

        }
        #endregion

        #region ovverides
        /// <summary>
        /// render raw control HTML to the page
        /// </summary>
        protected override void Render(HtmlTextWriter writer)
        {
            //-- master DIV
            writer.Write("<div");
            if (!string.IsNullOrEmpty(CssClass))
            {
                writer.Write(" class='" + CssClass + "'");
            }
            writer.Write(CssStyle());
            writer.Write(">");

            //-- image DIV/SPAN
            writer.Write(LayoutStyle == Layout.Vertical
                             ? "<span style='text-align:center;margin:5px;'>"
                             : "<span style='margin:5px;float:left;'>");

            //-- this is the URL that triggers the CaptchaImageHandler
            writer.Write("<img src=\"CaptchaImage.axd");
            if (!IsDesignMode)
            {
                writer.Write("?guid=" + Convert.ToString(_captcha.UniqueId));
            }
            if (CacheStrategy == CacheType.Session)
            {
                writer.Write("&s=1");
            }
            writer.Write("\" border='0'");
            if (ToolTip.Length > 0)
            {
                writer.Write(" alt='" + ToolTip + "'");
            }
            writer.Write(" width=" + _captcha.Width);
            writer.Write(" height=" + _captcha.Height);
            writer.Write(">");
            writer.Write("</span>");

            //-- text input and submit button DIV/SPAN
            writer.Write(LayoutStyle == Layout.Vertical
                             ? "<br/><span style='text-align:center;margin:5px;'>"
                             : "<span style='margin:5px;float:left;'>");
            if (_text.Length > 0)
            {
                writer.Write(_text);
                if (LayoutStyle == Layout.Horizontal)
                    writer.Write("<br>");
            }
            writer.Write("<input name=" + UniqueID + " type=text size=");
            writer.Write(_captcha.TextLength.ToString());
            writer.Write(" maxlength=");
            writer.Write(_captcha.TextLength.ToString());
            if (AccessKey.Length > 0)
            {
                writer.Write(" accesskey=" + AccessKey);
            }
            if (!Enabled)
            {
                writer.Write(" disabled=\"disabled\"");
            }
            if (TabIndex > 0)
            {
                writer.Write(" tabindex=" + TabIndex);
            }
            writer.Write(" value=''>");
            if (LayoutStyle == Layout.Vertical)
            {
                writer.Write("</span>");
            }
            else
            {
                writer.Write("</span>");
                writer.Write("<br clear='all'>");
            }

            //-- closing tag for master DIV
            writer.Write("</div>");
        }
        protected override object SaveControlState()
        {
            return _captcha.UniqueId;
        }

        protected override void LoadControlState(object state)
        {
            if (state != null)
            {
                _prevguid = Convert.ToString(state);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
            Page.Validators.Add(this);

        }

        protected override void OnUnload(EventArgs e)
        {
            if ((Page != null))
            {
                Page.Validators.Remove(this);
            }
            base.OnUnload(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Visible && !IsDesignMode)
            {
                GenerateNewCaptcha();
            }
            base.OnPreRender(e);

        }
        #endregion

        /// <summary>
        /// generate a new captcha and store it in the ASP.NET Cache by unique GUID
        /// </summary>
        private void GenerateNewCaptcha()
        {
            if (_cacheStrategy == CacheType.HttpRuntime)
            {
                HttpRuntime.Cache.Add(_captcha.UniqueId, _captcha, null, DateTime.Now.AddSeconds(Convert.ToDouble((CaptchaMaxTimeout == 0 ? 90 : CaptchaMaxTimeout))), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }
            else
            {
                HttpContext.Current.Session.Add(_captcha.UniqueId, _captcha);
            }
        }

        private CaptchaImage GetCachedCaptcha(string guid)
        {
            return _cacheStrategy == CacheType.HttpRuntime
                       ? (CaptchaImage)HttpRuntime.Cache.Get(guid)
                       : (CaptchaImage)HttpContext.Current.Session[guid];
        }

        private void RemoveCachedCaptcha(string guid)
        {
            if (_cacheStrategy == CacheType.HttpRuntime)
            {
                HttpRuntime.Cache.Remove(guid);
            }
            else
            {
                HttpContext.Current.Session.Remove(guid);
            }
        }



        /// <summary>
        /// Validate the user's text against the CAPTCHA text
        /// </summary>

        private void ValidateCaptcha(string userEntry)
        {
            if (!Visible | !Enabled)
            {
                _userValidated = true;
                return;
            }

            //-- retrieve the previous captcha from the cache to inspect its properties
            var ci = GetCachedCaptcha(_prevguid);
            if (ci == null)
            {
                ErrorMessage = "The code you typed has expired after " + CaptchaMaxTimeout + " seconds.";
                _userValidated = false;
                return;
            }

            //--  was it entered too quickly?
            if (CaptchaMinTimeout > 0)
            {
                if ((ci.RenderedAt.AddSeconds(CaptchaMinTimeout) > DateTime.Now))
                {
                    _userValidated = false;
                    ErrorMessage = "Code was typed too quickly. Wait at least " + CaptchaMinTimeout + " seconds.";
                    RemoveCachedCaptcha(_prevguid);
                    return;
                }
            }

            if (string.Compare(userEntry, ci.Text, true) != 0)
            {
                ErrorMessage = "The code you typed does not match the code in the image.";
                _userValidated = false;
                RemoveCachedCaptcha(_prevguid);
                return;
            }

            _userValidated = true;
            RemoveCachedCaptcha(_prevguid);
        }

        /// <summary>
        /// returns HTML-ized color strings
        /// </summary>
        private static string HtmlColor(System.Drawing.Color color)
        {
            if (color.IsEmpty)
                return "";
            if (color.IsNamedColor)
            {
                return color.ToKnownColor().ToString();
            }
            if (color.IsSystemColor)
            {
                return color.ToString();
            }
            return "#" + color.ToArgb().ToString("x").Substring(2);
        }

        /// <summary>
        /// returns css "style=" tag for this control
        /// based on standard control visual properties
        /// </summary>
        private string CssStyle()
        {

            var sb = new System.Text.StringBuilder();
            sb.Append(" style='");

            if (!BorderWidth.IsEmpty)
            {
                sb.Append("border-width:");
                sb.Append(BorderWidth.ToString());
                sb.Append(";");
            }
            if (BorderStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
            {
                sb.Append("border-style:");
                sb.Append(BorderStyle.ToString());
                sb.Append(";");
            }
            if (!BorderColor.IsEmpty)
            {
                sb.Append("border-color:");
                sb.Append(HtmlColor(BorderColor));
                sb.Append(";");
            }

            if (!BackColor.IsEmpty)
            {
                sb.Append("background-color:" + HtmlColor(BackColor) + ";");
            }

            if (!ForeColor.IsEmpty)
            {
                sb.Append("color:" + HtmlColor(ForeColor) + ";");
            }

            if (Font.Bold)
            {
                sb.Append("font-weight:bold;");
            }

            if (Font.Italic)
            {
                sb.Append("font-style:italic;");
            }

            if (Font.Underline)
            {
                sb.Append("text-decoration:underline;");
            }

            if (Font.Strikeout)
            {
                sb.Append("text-decoration:line-through;");
            }

            if (Font.Overline)
            {
                sb.Append("text-decoration:overline;");
            }

            if (!Font.Size.IsEmpty)
            {
                sb.Append("font-size:" + Font.Size + ";");
            }

            if (Font.Names.Length > 0)
            {
                sb.Append("font-family:");
                foreach (var fontFamily in Font.Names)
                {
                    sb.Append(fontFamily);
                    sb.Append(",");
                }
                sb.Length = sb.Length - 1;
                sb.Append(";");
            }

            if (!string.IsNullOrEmpty(Height.ToString()))
            {
                sb.Append("height:" + Height + ";");
            }
            if (!string.IsNullOrEmpty(Width.ToString()))
            {
                sb.Append("width:" + Width + ";");
            }

            sb.Append("'");
            return sb.ToString();

        }

    }
}
