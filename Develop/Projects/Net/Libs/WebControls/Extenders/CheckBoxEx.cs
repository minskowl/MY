#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// 
    /// </summary>
    public class CheckBoxEx : CheckBox, IBindable
    {
        #region Property
        /// <summary>
        /// Gets or sets the object id.
        /// </summary>
        /// <value>The object id.</value>
        [Bindable(false)]
        [Description("Data Keys selected rows")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [Themeable(false)]
        [DefaultValueAttribute(null)]
        public long ObjectId
        {
            get
            {
                if (ViewState["ObjectId"] == null)
                    return 0;
                return (long)ViewState["ObjectId"];
            }

            set
            {
                ViewState["ObjectId"] = value;
            }
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
            set
            {
                ViewState["OnClientClick"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public virtual string PropertyName
        {
            get { return (string)ViewState["PropertyName"]; }
            set { ViewState["PropertyName"] = value; }
        }
        #endregion

        /// <summary>
        /// Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.CheckBox"></see> control to be rendered to the specified output stream.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute("objectId", ObjectId.ToString());
            base.AddAttributesToRender(writer);
            if (!string.IsNullOrEmpty(OnClientClick))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Util.EnsureEndWithSemiColon(OnClientClick));
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
            if (value is bool)
            {
                Checked = (bool)value;
            }
            else 
            {
                Checked = (bool?)value ?? false;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        object IBindable.GetValue()
        {
            return Checked;
        }

        #endregion
    }
}
