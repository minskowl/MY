using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Savchin.Web.UI;
using Savchin.Web.UI.Calendar;

[assembly: WebResource(EmbeddedResources.JsCalendar, EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(EmbeddedResources.CssCalendar, EmbeddedResources.Css, PerformSubstitution = true)]

namespace Savchin.Web.UI
{



    internal static partial class EmbeddedResources
    {
        internal const string JsCalendar = namespaceName + "Calendar.Calendar.js";
        internal const string CssCalendar = namespaceName + "Calendar.Calendar.css";
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CalendarMode
    {
        /// <summary>
        /// 
        /// </summary>
        EditBox,
        /// <summary>
        /// 
        /// </summary>
        SelectBox,
        /// <summary>
        /// 
        /// </summary>
        SelectBoxWithTime,
        /// <summary>
        /// 
        /// </summary>
        Label
    }

    /// <summary>
    /// CalendarControl
    /// </summary>
    [ValidationPropertyAttribute("Value")]
    public class CalendarControl : WebControl, IBindable
    {
        private readonly EditBoxLayout editBoxLayout = new EditBoxLayout();
        LayoutBase activeLayout = null;
        //private Label

        #region Properties

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public CalendarMode Mode
        {
            get
            {
                object value = ViewState["Type"];
                return ((value == null) ? CalendarMode.EditBox : (CalendarMode)value);
            }

            set
            {
                ViewState["Type"] = value;
                SetActiveLayout(value);
            }
        }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>The date format.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string DateFormat
        {
            get { return (String)ViewState["DateFormat"] ?? "yyyy/mm/dd"; }
            set { ViewState["DateFormat"] = value; }
        }


        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        [Category("Appearance")]
        [DefaultValue("")]
        public string ImageUrl
        {
            get { return (String)ViewState["ImageUrl"] ?? ImagePathProvider.CommonImagesUrl + "calendar/"; }
            set { ViewState["ImageUrl"] = value; }
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
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public DateTime? Value
        {
            get { return activeLayout.Value; }
            set { activeLayout.Value = value; }
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [Category("Behavior")]
        [DefaultValue("")]
        public string PropertyName
        {
            get { return (String)ViewState["PropertyName"]; }
            set { ViewState["PropertyName"] = value; }
        }
        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            editBoxLayout.ID = ID + "_EditBoxLayout";
            editBoxLayout.Visible = false;
            Controls.Add(editBoxLayout);

            if (activeLayout == null) SetActiveLayout(Mode);

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            //TODO: clear when all layouts
            if (activeLayout != null)
            {
                activeLayout.Visible = true;
                activeLayout.DateFormat = DateFormat;

            }

            base.OnPreRender(e);

            Page.ClientScript.RegisterStartupScript(typeof(CalendarControl), "Init", "var calendarPathToImages = '" + ImageUrl + "';", true);
            Page.ClientScript.RegisterClientScriptResource(typeof(CalendarControl), EmbeddedResources.JsCalendar);
            ControlHelper.AddCssInclude(Page, typeof(CalendarControl), EmbeddedResources.CssCalendar);



        }
        private void SetActiveLayout(CalendarMode mode)
        {
            switch (mode)
            {
                case CalendarMode.EditBox:
                    activeLayout = editBoxLayout;
                    break;
                case CalendarMode.SelectBox:
                    break;
                case CalendarMode.Label:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region Implementation of IBindable



        /// <summary>
        /// Gets a value indicating whether this instance can get value.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can get value; otherwise, <c>false</c>.
        /// </value>
        bool IBindable.CanGetValue
        {
            get { return true; }
        }



        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void IBindable.SetValue(object value)
        {
            Value = (DateTime)value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        object IBindable.GetValue()
        {
            return Value;
        }

        #endregion
    }
}