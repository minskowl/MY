#region Version & Copyright
/* 
 * $Id: DropDownListEx.cs 34700 2008-07-07 14:53:57Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Savchin.Core;

namespace Savchin.Web.UI
{
    /// </summary>
    public class DropDownListEx : DropDownList, IBindable
    {

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DropDownListEx"/> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool Required
        {
            get
            {
                var obj1 = ViewState["Required"];
                if (obj1 != null) return (bool)obj1;
                return false;
            }
            set
            {
                ViewState["Required"] = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [auto fill].
        /// </summary>
        /// <value><c>true</c> if [auto fill]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(true)]
        public virtual bool AutoFill
        {
            get
            {
                var obj1 = ViewState["AutoFill"];
                if (obj1 != null) return (bool)obj1;
                return true;
            }
            set
            {
                ViewState["AutoFill"] = value;
            }
        }
        /// <summary>
        /// Gets or sets the selected long value.
        /// </summary>
        /// <value>The selected long value.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long SelectedLongValue
        {
            get { return long.Parse(SelectedValue); }
            set { SelectedValue = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the selected int value.
        /// </summary>
        /// <value>The selected int value.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIntValue
        {
            get { return string.IsNullOrEmpty(SelectedValue) ? int.MinValue : int.Parse(SelectedValue); }
            set { SelectedValue = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the selected short value.
        /// </summary>
        /// <value>The selected short value.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public short SelectedShortValue
        {
            get { return short.Parse(SelectedValue); }
            set { SelectedValue = value.ToString(); }
        }
        /// <summary>
        /// Gets or sets the selected byte value.
        /// </summary>
        /// <value>The selected byte value.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte SelectedByteValue
        {
            get { return byte.Parse(SelectedValue); }
            set { SelectedValue = value.ToString(); }
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


        #endregion

        #region Interface
        /// <summary>
        /// Datas the bind.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="textColumnName">Name of the text column.</param>
        /// <param name="valueColumnName">Name of the value column.</param>
        public void DataBind(object data, string textColumnName, string valueColumnName)
        {
            DataTextField = textColumnName;
            DataValueField = valueColumnName;
            DataSource = data;
            DataBind();

        }
        /// <summary>
        /// Binds the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="textColumnName">Name of the text column.</param>
        /// <param name="valueColumnName">Name of the value column.</param>
        public void Bind(object data, string textColumnName, string valueColumnName)
        {
            DataTextField = textColumnName;
            DataValueField = valueColumnName;
            DataSource = data;
            DataBind();

        }
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        public void AddItem(string text, string value)
        {
            Items.Add(new ListItem(text, value));
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        public void AddItem(string text, int value)
        {
            Items.Add(new ListItem(text, value.ToString()));
        }
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        public void AddItem(Enum value, Type type)
        {
            Items.Add(new ListItem(value.GetDescription(), Enum.Format(type, value, "d")));
        }

        /// <summary>
        /// Adds the localize item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        public void AddLocalizeItem(Enum value, Type type)
        {
            Items.Add(new ListItem(Localizator.Get(value), Enum.Format(type, value, "d")));
        }
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="text">The text.</param>
        public void AddItem(string text)
        {
            Items.Add(text);
        }


        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="type">The type.</param>
        public void AddItems(Type type)
        {
            foreach (var value in Enum.GetValues(type))
            {
                AddItem((Enum)value, type);
            }
        }

        /// <summary>
        /// Adds the localize items.
        /// </summary>
        /// <param name="type">The type.</param>
        public void AddLocalizeItems(Type type)
        {
            foreach (var value in Enum.GetValues(type))
            {
                AddLocalizeItem((Enum)value, type);
            }
        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="textColumnName">Name of the text column.</param>
        /// <param name="valueColumnName">Name of the value column.</param>
        public void AddItems(DataTable table, string textColumnName, string valueColumnName)
        {

            int columnText = table.Columns.IndexOf(textColumnName);
            int columnValue = table.Columns.IndexOf(valueColumnName);
            foreach (DataRow row in table.Rows)
            {
                Items.Add(new ListItem(row[columnText].ToString(), row[columnValue].ToString()));
            }
        }
        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="textColumnName">Name of the text column.</param>
        /// <param name="valueColumnName">Name of the value column.</param>
        public void AddItems(IEnumerable data, string textColumnName, string valueColumnName)
        {
            Type dataType = null;
            PropertyInfo textPropertyInfo = null;
            PropertyInfo valuePropertyInfo = null;
            foreach (object o in data)
            {
                if (dataType == null)
                {
                    dataType = o.GetType();

                    textPropertyInfo = dataType.GetProperty(textColumnName);
                    valuePropertyInfo = dataType.GetProperty(valueColumnName);
                }
                Items.Add(new ListItem(textPropertyInfo.GetValue(o, null).ToString(),
                                       valuePropertyInfo.GetValue(o, null).ToString()));
            }
        }
        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="labels">The labels.</param>
        public void AddItems(IEnumerable<string> labels)
        {
            foreach (string s in labels)
            {
                Items.Add(s);
            }
        }
        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="labels">The labels.</param>
        public void AddItems(IEnumerable labels)
        {
            foreach (object s in labels)
            {
                Items.Add(s.ToString());
            }
        }
        /// <summary>
        /// Sets the selected value safe.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool SetSelectedValueSafe(string value)
        {
            try
            {
                SelectedValue = value;
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

        }

        #endregion

        #region Implementation of IBindable

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get;
            set;
        }

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
            if (value == null)
            {
                SelectedValue = string.Empty;
                return;
            }
            var type = value.GetType();

            if (type.IsEnum )
            {
                value=Convert.ChangeType(value, type.GetEnumUnderlyingType());
            }

            SelectedValue =  value.ToString();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        object IBindable.GetValue()
        {
            return SelectedValue;
        }

        #endregion

        /// <summary>
        /// Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.CheckBox"></see> control to be rendered to the specified output stream.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (!string.IsNullOrEmpty(OnClientClick))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onchange, Util.EnsureEndWithSemiColon(OnClientClick));
            }
        }
    }
}
