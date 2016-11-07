using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsTextBoxEx, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]


namespace Savchin.Web.UI
{
    internal static partial class EmbeddedResources
    {
        internal const string JsTextBoxEx = namespaceName + "TextBoxEx.js";
    }

    public class TextBoxEx : TextBox, IBindable
    {

        private bool usingInitialValue = false;

        #region Properties
        /// <summary>
        /// Gets or sets the initial value.
        /// </summary>
        /// <value>The initial value.</value>
        public virtual string InitialValue
        {
            get { return (string)ViewState["InitialValue"]; }
            set { ViewState["InitialValue"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
       [Themeable(false), 
        DefaultValue(false), 
        Bindable(true), Category("Behavior"),
        Description("PropertyName")]
        public virtual string PropertyName
        {
            get { return (string)ViewState["PropertyName"]; }
            set { ViewState["PropertyName"] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can get value.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can get value; otherwise, <c>false</c>.
        /// </value>
        public bool CanGetValue
        {
            get { return true; }
        }
        #endregion

        #region IBindable


        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue(object value)
        {
            Text = (value == null) ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            return Text;
        }

        #endregion

        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter"/> instance.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            string initialValue = InitialValue;

            if (usingInitialValue)
            {
                Text = initialValue;
                //restoreText = true;
            }

            base.AddAttributesToRender(writer);

            if (usingInitialValue)
            {
                writer.AddAttribute("initialValue", InitialValue);
                Text = string.Empty;
            }
        }

        /// <summary>
        /// Registers client script for generating postback events prior to rendering on the client, if <see cref="P:System.Web.UI.WebControls.TextBox.AutoPostBack"/> is true.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);

            if (!string.IsNullOrEmpty(InitialValue) && string.IsNullOrEmpty(Text))
            {
                usingInitialValue = true;
            }

            if (usingInitialValue)
                Page.ClientScript.RegisterClientScriptResource(typeof(TextBoxEx), EmbeddedResources.JsTextBoxEx);

        }
    }
}
