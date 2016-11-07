using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Savchin.Web.UI.PropertyGrid
{
    class PropertyGridItem : GridControl
    {
        readonly PropertyDescriptor propdesc;

        internal string controlid;

        public PropertyGridItem(PropertyDescriptor propdesc)
        {
            this.propdesc = propdesc;
        }

        protected bool HasSubItems
        {
            get { return subitems.Count > 0; }
        }

        protected bool IsParentItem
        {
            get { return !IsSubItem; }
        }

        protected virtual bool IsSubItem
        {
            get { return false; }
        }

        internal readonly ArrayList subitems = new ArrayList();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsParentItem)
            {
                if (HasSubItems)
                {
                    ID = "sub" + ParentGrid.subcounter++;
                }
            }
        }

        void RenderEditor(HtmlTextWriter writer)
        {
            if (propdesc.IsReadOnly || ParentGrid.ReadOnly)
            {
                writer.Write(@"<span title=""{1}""><span id=""{0}"" style=""color:gray"">{1}</span></span>",
                  controlid,
                  PropertyValue);
            }
            else
            {
                TypeConverter tc = propdesc.Converter;
                if (tc.GetStandardValuesSupported())
                {
                    string pv = PropertyValue;
                    writer.Write(@"<a onclick=""{2}.BeginEdit(this); return false;"" href=""#""" +
                      @" title=""Click to edit""><span id=""{0}"">{1}</span></a>",
                      controlid,
                      pv,
                      ParentGrid.ClientID);

                    writer.Write(@"<select style=""display:none"" onblur=""{0}.CancelEdit(this)"" onchange=""{0}.EndEdit(this)"">",
                      ParentGrid.ClientID);

                    foreach (object si in tc.GetStandardValues())
                    {
                        string val = tc.ConvertToString(si);
                        if (val == pv)
                        {
                            writer.Write(@"<option selected=""selected"">{0}</option>", val);
                        }
                        else
                        {
                            writer.Write(@"<option>{0}</option>", val);
                        }
                    }

                    writer.Write("</select>");
                }
                else
                {
                    if (tc.CanConvertFrom(typeof(string)))
                    {
                        writer.Write(@"<a onclick=""{2}.BeginEdit(this);return false"" href=""#""" +
                          @" title=""Click to edit""><span id=""{0}"">{1}</span></a>",
                          controlid,
                          PropertyValue,
                          ParentGrid.ClientID);

                        writer.Write(@"<input onkeydown=""return {0}.HandleKey(this,event)"" onblur=""{0}.CancelEdit(this)""" +
                          @" style=""display:none"" type=""text"" onchange=""{0}.EndEdit(this)"" />",
                          ParentGrid.ClientID);
                    }
                    else
                    {
                        writer.Write(@"<span title=""{1}""><span id=""{0}"" style=""color:gray"">{1}</span></span>",
                          controlid,
                          PropertyValue);
                    }
                }
            }
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(@"
<div class=""PGI PGI_{4}"">
<div class=""PGI_{1} PGI_{1}_{4}""{3}></div>
<div onclick=""{4}.ShowHelp(this);return false"" class=""PGI_NAME{2} PGI_NAME{2}_{4}""" +
    @" title=""Click for help""><span>{0}</span></div><div class=""PGI_VALUE PGI_VALUE_{4}"">",
              propdesc.DisplayName,
              !HasSubItems ? "NONE" : "CLOSED",
              IsSubItem ? "_SUB" : string.Empty,
              !HasSubItems ? string.Empty : string.Format(@" onclick=""PGSubToggle(this)""", ClientID),
              ParentGrid.ClientID
              );

            try
            {
                RenderEditor(writer);
            }
            catch (Exception ex)
            {
               Util.Log.Error("Render", ex);
            }

            writer.Write("</div></div>");

            if (IsParentItem)
            {
                RenderChildren(writer);
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return propdesc.Name; }
        }

        /// <summary>
        /// Gets the descriptor.
        /// </summary>
        /// <value>The descriptor.</value>
        public PropertyDescriptor Descriptor
        {
            get { return propdesc; }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /// <value>The property value.</value>
        public string PropertyValue
        {
            get
            {
                if (propdesc.Converter.CanConvertTo(typeof(string)))
                {
                    return propdesc.Converter.ConvertToString(propdesc.GetValue(SelectedObject));
                }
                else
                {
                    return propdesc.GetValue(SelectedObject).ToString();
                }
            }
            set
            {
                object so = SelectedObject;
                object val = propdesc.Converter.ConvertFromString(value);
                propdesc.SetValue(so, val);

                if (IsSubItem)
                {
                    PropertyGridItem parent = ((PropertyGridSubItem)this).ParentItem;
                    parent.Descriptor.SetValue(parent.SelectedObject, so);
                }
                else
                {
                    ParentGrid.CreateGrid();
                }
            }
        }

        public virtual object SelectedObject
        {
            get { return ParentGrid.SelectedObject; }
        }
    }
}
