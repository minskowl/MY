using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    public class WindowControl : UserControl
    {
        private readonly Window instance = new Window();


        #region Properties

        /// <summary>
        /// Gets the J script close.
        /// </summary>
        /// <value>The J script close.</value>
        public string JScriptShow
        {
            get { return instance.JScriptShow; }
        }

        /// <summary>
        /// Gets the J script hide.
        /// </summary>
        /// <value>The J script hide.</value>
        public string JScriptHide
        {
            get { return instance.JScriptHide; }
        }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Title
        {
            get { return instance.Title; }
            set { instance.Title = value; }
        }

        /// <summary>
        /// Gets or sets the close button ID.
        /// </summary>
        /// <value>The close button ID.</value>
        [Category("Behavior")]
        [Themeable(false)]
        public virtual string CloseButtonID
        {
            get { return instance.CloseButtonID; }
            set { instance.CloseButtonID = value; }
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
            get
            {
                return ((String)ViewState["Text"] ?? String.Empty);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Window"/> is hide.
        /// </summary>
        /// <value><c>true</c> if hide; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool Hide
        {
            get { return instance.Hide; }
            set { instance.Hide = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WindowControl"/> is resizeble.
        /// </summary>
        /// <value><c>true</c> if resizeble; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool Resizeble
        {
            get { return instance.Resizeble; }
            set { instance.Resizeble = value; }
        }

        [Category("Layout"), DefaultValue(typeof(Unit), ""), Description("Left postion")]
        public virtual Unit Left
        {
            get { return instance.Left; }
            set { instance.Left = value; }
        }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>The left.</value>
        [Category("Layout"), DefaultValue(typeof(Unit), ""), Description("Top postion")]
        public virtual Unit Top
        {
            get { return instance.Top; }
            set { instance.Top = value; }
        }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        [Category("Layout"), DefaultValue(typeof(Unit), ""), Description("Height")]
        public virtual Unit Height
        {
            get { return instance.Height; }
            set { instance.Height = value; }
        }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        [Category("Layout"), DefaultValue(typeof(Unit), ""), Description("Width")]
        public virtual Unit Width
        {
            get { return instance.Width; }
            set { instance.Width = value; }
        }

        #endregion


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            instance.ID = ID + "DashBoard";
            Controls.Add(instance);
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!string.IsNullOrEmpty(Text))
            {
                Literal literal = new Literal();
                literal.ID = "Literal" + ID;
                literal.Text = Text;
                Controls.Add(literal);
            }
        }
        /// <summary>
        /// Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection"/> object.
        /// </summary>
        /// <param name="obj">An <see cref="T:System.Object"/> that represents the parsed element.</param>
        protected override void AddParsedSubObject(object obj)
        {
            if (obj != null && obj is Control)
                instance.Controls.Add((Control)obj);
        }


    }
}
