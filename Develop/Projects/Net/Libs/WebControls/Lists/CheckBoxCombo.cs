#region Version & Copyright
/* 
 * $Id: CheckBoxCombo.cs 30122 2008-03-28 14:14:09Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Savchin.Web;


[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsCheckeBoxCombo, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]


namespace Savchin.Web.UI
{
internal static partial class EmbeddedResources
{
    internal const string JsCheckeBoxCombo = namespaceName + "Lists.CheckeBoxCombo.js";
}
    [PersistChildren(false),
    ParseChildren(true),
    ValidationProperty("SelectedValues")]
    public class CheckBoxCombo : WebControlEx, INamingContainer
    {

        //private const int scrollHeight = 18;
        private const int rowHeight = 21;
        private const int rowHeightIE = 22;

        #region Controls

        private readonly CheckedListBox listBox = new CheckedListBox();
        private readonly FilterComboLayout layout = new FilterComboLayout();
        private readonly HtmlGenericControl label = new HtmlGenericControl("div");

        #endregion

        #region Properties



        /// <summary>
        /// Overrides base.TagKey
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        [Description("Gets the GridEXDropDown object associated to the control."),
         DefaultValue(null),
         NotifyParentProperty(true),
        TypeConverter(typeof(ExpandableObjectConverter)),
        DesignerSerializationVisibility(0)]
        public CheckedListBox ListBox
        {
            get { return listBox; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [with label].
        /// </summary>
        /// <value><c>true</c> if [with label]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool WithLabel
        {
            get
            {
                object obj1 = ViewState["WithLabel"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return false;
            }
            set { ViewState["WithLabel"] = value; }
        }

        [Description("Gets or set number of visible items of list box")]
        [DefaultValue(15)]
        [Category("Appearance")]
        public int VisibleItemCount
        {
            get
            {
                if (ViewState["VisibleItemCount"] == null)
                    ViewState["VisibleItemCount"] = 15;
                return (int)ViewState["VisibleItemCount"];
            }
            set { ViewState["VisibleItemCount"] = value; }
        }

        /// <summary>
        /// Gets or sets the drop down image URL.
        /// </summary>
        /// <value>The drop down image URL.</value>
        [Bindable(false)]
        [Category("Behaviour")]
        [DefaultValue("")]
        public String AfterClearJScript
        {
            get
            {
                object value = ViewState["AfterClearJScript"];
                return ((value == null) ? "" : (String)value);
            }

            set
            {
                ViewState["AfterClearJScript"] = value;
            }
        }
        /// <summary>
        /// Gets or sets the clear JS filter.
        /// </summary>
        /// <value>The clear JS filter.</value>
        [Bindable(false)]
        [Category("Behaviour")]
        [DefaultValue("")]
        public String ClearJSFilter
        {
            get
            {
                object value = ViewState["ClearJSFilter"];
                return ((value == null) ? "" : (String)value);
            }

            set
            {
                ViewState["ClearJSFilter"] = value;
            }
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
            get { return ((String)ViewState["Text"]) ?? String.Empty; }

            set { ViewState["Text"] = value; }
        }

        /// <summary>
        /// Gets or sets the drop down image URL.
        /// </summary>
        /// <value>The drop down image URL.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [EditorAttribute(typeof(UrlEditor), typeof(UITypeEditor))]
        public String DropDownImageUrl
        {
            get
            {
                return (string)ViewState["DropDownImageUrl"];
            }

            set
            {
                ViewState["DropDownImageUrl"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the clear image URL.
        /// </summary>
        /// <value>The clear image URL.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [EditorAttribute(typeof(UrlEditor), typeof(UITypeEditor))]
        public String ClearImageUrl
        {
            get
            {
                return (string) ViewState["ClearImageUrl"];
            }

            set
            {
                ViewState["ClearImageUrl"] = value;
            }
        }
        /// <summary>
        /// Gets or sets the button background image URL.
        /// </summary>
        /// <value>The button background image URL.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [EditorAttribute(typeof(UrlEditor), typeof(UITypeEditor))]
        public String ButtonBackgroundImageUrl
        {
            get
            {
                object value = ViewState["ButtonBackgroundImageUrl"];
                return ((value == null) ? "~/App_Themes/images/button.gif" : (String)value);
            }

            set
            {
                ViewState["ButtonBackgroundImageUrl"] = value;
            }
        }
        /// <summary>
        /// Gets the selected values.
        /// </summary>
        /// <value>The selected values.</value>
        public virtual List<string> SelectedStringValues
        {
            get { return ListBox.SelectedValues; }
            set { ListBox.SelectedValues = value; }
        }
        /// <summary>
        /// Gets or sets the selected long values.
        /// </summary>
        /// <value>The selected long values.</value>
        public virtual List<long> SelectedLongValues
        {
            get { return ListBox.SelectedLongValues; }
            set { ListBox.SelectedLongValues = value; }
        }
        /// <summary>
        /// Gets or sets the selected int values.
        /// </summary>
        /// <value>The selected int values.</value>
        public virtual List<int> SelectedIntValues
        {
            get { return ListBox.SelectedIntValues; }
            set { ListBox.SelectedIntValues = value; }
        }


        /// <summary>
        /// Gets or sets the data source for the dropdown list.
        /// </summary>
        [DefaultValue(null),
        DesignerSerializationVisibility(0),
        Description("Gets or sets the data source for the dropdown list.")]
        public object DataSource
        {
            get { return ListBox.DataSource; }
            set { ListBox.DataSource = value; }
        }
        /// <summary>
        /// Gets or sets the data text field.
        /// </summary>
        /// <value>The data text field.</value>
        [DefaultValue("")]
        public string DataTextField
        {
            get { return ListBox.DataTextField; }
            set { ListBox.DataTextField = value; }
        }
        /// <summary>
        /// Gets or sets the data value field.
        /// </summary>
        /// <value>The data value field.</value>
        [DefaultValue("")]
        public string DataValueField
        {
            get { return ListBox.DataValueField; }
            set { ListBox.DataValueField = value; }
        }

        /// <summary>
        /// Gets or sets function which will be called during
        /// change event
        /// </summary>
        public string OnChangeScript
        {
            get { return ViewState["OnChangeScript"] as string; }
            set { ViewState["OnChangeScript"] = value; }
        }

        /// <summary>
        /// Gets the js object id.
        /// </summary>
        /// <value>The js object id.</value>
        public string JsObjectId
        {
            get { return ClientID + "Obj"; }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(string value)
        {
            ListBox.Select(value);
        }
        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(int value)
        {
            ListBox.Select(value);
        }

        /// <summary>
        /// Selects the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void Select(IEnumerable<int> values)
        {
            foreach (int i in values)
            {
                ListBox.Select(i);
            }

        }
        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(long value)
        {
            ListBox.Select(value);
        }

        /// <summary>
        /// Uns the select all.
        /// </summary>
        public void UnSelectAll()
        {
            ListBox.UnSelectAll();
        }

        /// <summary>
        /// Selects all.
        /// </summary>
        public void SelectAll()
        {
            ListBox.SelectAll();
        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="type">The type.</param>
        public void AddItems(Type type)
        {
            listBox.AddItems(type);
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxCombo"/> class.
        /// </summary>
        public CheckBoxCombo()
        {
            InitializeControl();
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        private void InitializeControl()
        {
            Width = 150;
            Height = 23;
            ToolTip = "Filter:";
            BorderColor = Color.DarkBlue; //"#002D96";
            BorderStyle = BorderStyle.Solid;
            BorderWidth = Unit.Pixel(1);
            BackColor = Color.White;
        }

        #region Ovverides

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            listBox.Height = 50;

            layout.ID = ID + "table";
            layout.JsObjectId = JsObjectId;
            listBox.ID = ID + "listBox";
            label.ID = ID + "label";

            Controls.Add(layout);
            Controls.Add(label);
            Controls.Add(listBox);
        }


        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriterTag"></see>. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            //base.AddAttributesToRender(writer);
            writer.AddAttribute("id", ClientID);
            writer.AddStyleAttribute("width", Width.ToString());

        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            InitializeListBox();
            InitializeLayout();

            label.Visible = WithLabel;
            if (!DesignMode && Page != null)
            {
                Page.ClientScript.RegisterStartupScript(typeof(CheckBoxCombo), ClientID + "Init", GetGridInitializationScript(), true);

                Page.ClientScript.RegisterClientScriptResource(typeof(CheckBoxCombo), EmbeddedResources.JsCheckeBoxCombo);
                //Note: Use for test JSCript
                //Page.ClientScript.RegisterClientScriptInclude(typeof(FilterCombo), "init", AppSettings.ApplicationJsPath + "Test.js");
            }

        }
        
        #endregion


        #region Helpers

        private void InitializeLayout()
        {

            layout.ClearButtonVisible = (SelectedStringValues.Count != 0);
            layout.Text = Text;
            layout.ClearButtonImageUrl = ClearImageUrl;
            layout.DropDownImageUrl = DropDownImageUrl;
            layout.ButtonBackgroundImageUrl = ButtonBackgroundImageUrl;

            layout.BorderStyle = BorderStyle;
            layout.BorderWidth = BorderWidth;
            layout.BorderColor = BorderColor;

            layout.BackColor = BackColor;
            layout.TabIndex = TabIndex;
            layout.Height = Height;
        }

        private void InitializeListBox()
        {
            listBox.BackColor = BackColor;
            listBox.BorderStyle = BorderStyle;
            listBox.BorderWidth = BorderWidth;
            listBox.BorderColor = BorderColor;

            if (ControlHelper.BrowserType == BrowserType.FireFox &&
                Width.Type == UnitType.Pixel)
                listBox.Width = (int)Width.Value - 2;
            else
                listBox.Width = Width;

            SetListBoxHeight();

            listBox.OnCheckBoxClientClick = JsObjectId + ".CheckBoxClick(event);";
            if (ControlHelper.BrowserType == BrowserType.FireFox)
                listBox.Attributes.CssStyle.Add(HtmlTextWriterStyle.Top, "-1");

            listBox.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            listBox.Attributes.CssStyle.Add(HtmlTextWriterStyle.Position, "absolute");
            listBox.Attributes.CssStyle.Add(HtmlTextWriterStyle.ZIndex, "4001");
            //listBox.Attributes.CssStyle.Add(HtmlTextWriterStyle.Overflow, "hidden");
        }

        /// <summary>
        /// Sets the height of the list box.
        /// </summary>
        private void SetListBoxHeight()
        {
            int itemCount = (VisibleItemCount > 0 && listBox.Items.Count > VisibleItemCount)
                      ? VisibleItemCount
                      : listBox.Items.Count;

            int newHeight;
            if (ControlHelper.BrowserType == BrowserType.IE)
                newHeight = rowHeightIE * itemCount;
            else
                newHeight = rowHeight * itemCount;

            //newHeight += scrollHeight;

            if (newHeight > listBox.Height.Value)
                listBox.Height = newHeight;

        }

        /// <summary>
        /// Gets the grid initialization script.
        /// </summary>
        /// <returns></returns>
        private string GetGridInitializationScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("var {0} = new FilterCombo('{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}');",
                                 JsObjectId,
                                 ClientID,
                                 layout.ClientID,
                                 listBox.ClientID,
                                 ToolTip,
                                 JavaScriptBuilder.ConvertToJavaScriptLine(AfterClearJScript),
                                 JavaScriptBuilder.ConvertToJavaScriptLine(ClearJSFilter),
                                  layout.ClearButtonClientID,
                                  label.ClientID);

            builder.AppendLine(JsObjectId + ".InitControl();");

            if (!string.IsNullOrEmpty(OnChangeScript))
            {
                builder.AppendLine(JsObjectId + ".OnChangeFuncName = " + OnChangeScript + ";");
                builder.AppendLine(OnChangeScript + "(" + JsObjectId + ");");
            }

            return builder.ToString();
        }

        #endregion
    }
}
