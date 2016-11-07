using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsCheckedListBox, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]

namespace Savchin.Web.UI
{
internal static partial class EmbeddedResources
{
    internal const string JsCheckedListBox = namespaceName + "Lists.CheckedListBox.js";
}


    /// <summary>
    /// CheckedListBox
    /// </summary>
    public class CheckedListBox : ListControl, INamingContainer, IPostBackDataHandler
    {

        private CheckBox controlToRepeat;
        private Boolean hasNotifiedOfChange;
        // this is a nice little utility function which lets me use my specified writer for only this control's tag
        private HtmlTextWriter tagWriter;

        #region Properties
        /// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button"></see> control's <see cref="E:System.Web.UI.WebControls.Button.Click"></see> event is raised.</summary>
        /// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button"></see> control's <see cref="E:System.Web.UI.WebControls.Button.Click"></see> event is raised.</returns>
        [Category("Behavior"),
         DefaultValue(""),
         Themeable(false),
         Description("On CheckBox ClientClick")]
        public virtual string OnCheckBoxClientClick
        {
            get
            {
                string text1 = (string)ViewState["OnCheckBoxClientClick"];
                if (text1 == null)
                {
                    return string.Empty;
                }
                return text1;
            }
            set
            {
                ViewState["OnCheckBoxClientClick"] = value;
            }
        }

        /// <summary>
        /// Gets the selected values.
        /// </summary>
        /// <value>The selected values.</value>
        public List<string> SelectedValues
        {
            get
            {
                List<string> result = new List<string>();
                foreach (ListItem item in Items)
                {
                    if (item.Selected)
                    {
                        result.Add(item.Value);
                    }
                }
                return result;
            }
            set
            {
                foreach (ListItem item in Items)
                {
                    item.Selected = value.Contains(item.Value);
                }
            }
        }
        /// <summary>
        /// Gets or sets the selected long values.
        /// </summary>
        /// <value>The selected long values.</value>
        public List<long> SelectedLongValues
        {
            get
            {
                List<long> result = new List<long>();
                foreach (ListItem item in Items)
                {
                    if (item.Selected)
                    {
                        result.Add(long.Parse(item.Value));
                    }
                }
                return result;
            }
            set
            {
                if (value == null)
                {
                    UnSelectAll();
                    return;
                }
                foreach (ListItem item in Items)
                {
                    item.Selected = value.Contains(long.Parse(item.Value));
                }
            }
        }
        /// <summary>
        /// Gets or sets the selected int values.
        /// </summary>
        /// <value>The selected int values.</value>
        public List<int> SelectedIntValues
        {
            get
            {
                List<int> result = new List<int>();
                foreach (ListItem item in Items)
                {
                    if (item.Selected)
                    {
                        result.Add(int.Parse(item.Value));
                    }
                }
                return result;
            }
            set
            {
                foreach (ListItem item in Items)
                {
                    item.Selected = value.Contains(int.Parse(item.Value));
                }
            }
        }
        #endregion


        protected override Control FindControl(string id, int pathOffset)
        {
            if (id.Contains(this.ID + "$"))
                return this;
            return base.FindControl(id, pathOffset);
        }


        /// <summary>
        /// Creates a new instance of the <see cref="CheckedListBox"/> class.
        /// </summary>
        public CheckedListBox()
        {
            InitControl();
        }
        private void InitControl()
        {
            controlToRepeat = new CheckBox();
            controlToRepeat.ID = "0";
            controlToRepeat.EnableViewState = false;
            Controls.Add(controlToRepeat);
            hasNotifiedOfChange = false;
            BorderWidth = Unit.Parse("2px");
            BorderStyle = BorderStyle.Inset;
        }

        #region Interface

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="type">The type.</param>
        public void AddItems(Type type)
        {
            string[] names = Enum.GetNames(type);
            Array values = Enum.GetValues(type);
            for (int i = 0; i < names.Length; i++)
            {
                AddItem(names[i], Enum.Format(type,values.GetValue(i),"d"));
            }
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
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(string value)
        {
            foreach (ListItem item in Items)
            {
                if (item.Value == value)
                    item.Selected = true;
            }
        }
        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(int value)
        {
            Select(value.ToString());
        }
        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(long value)
        {
            Select(value.ToString());
        }

        /// <summary>
        /// Uns the select all.
        /// </summary>
        public void UnSelectAll()
        {
            foreach (ListItem item in Items)
            {
                item.Selected = false;
            }
        }
        /// <summary>
        /// Selects all.
        /// </summary>
        public void SelectAll()
        {
            foreach (ListItem item in Items)
            {
                item.Selected = true;
            }
        } 
        #endregion

        #region Implementation of IPostBackDataHandler

        /// <summary>
        /// When implemented by a class, signals the server control to notify the ASP.NET application that the state of the control has changed.
        /// </summary>
        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }

        private bool firstLoadPostData = true;
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
            string itemIndexString = postDataKey.Substring(UniqueID.Length + 1);
            if (firstLoadPostData)
            {
                foreach (ListItem item in Items)
                {
                    item.Selected = false;
                }
                firstLoadPostData = false;
            }

            if (UseValue)
            {
                foreach (ListItem item in Items)
                {
                    if (item.Value == itemIndexString)
                    {
                        bool itemSelected = (postCollection[postDataKey] != null);
                        if (item.Selected != itemSelected)
                        {
                            item.Selected = itemSelected;
                            if (!(hasNotifiedOfChange))
                            {
                                hasNotifiedOfChange = true;
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                int itemIndex = Int32.Parse(itemIndexString);
                if (itemIndex < 0 && itemIndex >= Items.Count)
                    return false;

                bool itemSelected = (postCollection[postDataKey] != null);
                if (Items[itemIndex].Selected != itemSelected)
                {
                    Items[itemIndex].Selected = itemSelected;
                    if (!(hasNotifiedOfChange))
                    {
                        hasNotifiedOfChange = true;
                        return true;
                    }
                }

            }

            return false;
        }

        #endregion

        #region Overridden Rendering Functions

        /// <summary>
        /// Overrides <see cref="System.Web.UI.Control.OnPreRender"/>
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            if (Page != null)
            {
                for (Int32 itemIndex = 0; itemIndex < Items.Count; itemIndex++)
                {
                    if (Items[itemIndex].Selected)
                    {
                        controlToRepeat.ID = itemIndex.ToString(NumberFormatInfo.InvariantInfo);
                        Page.RegisterRequiresPostBack(controlToRepeat);
                    }
                }
            }

            RegisterClientScript();
        }



        /// <summary>
        /// Register the client script for the control with the page.
        /// </summary>
        protected virtual void RegisterClientScript()
        {
            Page.ClientScript.RegisterClientScriptResource(typeof(CheckedListBox), EmbeddedResources.JsCheckedListBox);


            Page.ClientScript.RegisterArrayDeclaration("Page_CheckedListBoxes", "'" + ClientID + "'");

            if (!Page.ClientScript.IsStartupScriptRegistered("CheckedListBox Init"))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "CheckedListBox Init",
                                           "<script language='javascript'>\r\nCheckedListBox_Init();\r\n</script>");
            }
        }

        /// <summary>
        /// Overrides <see cref="System.Web.UI.Control.Render"/>
        /// </summary>
        protected override void Render(HtmlTextWriter writer)
        {
            Style["overflow"] = "auto";

            short originalTabIndex = TabIndex;
            bool tabIndexChanged = false;
            controlToRepeat.TabIndex = originalTabIndex;
            if (originalTabIndex != 0)
            {
                if (!(ViewState.IsItemDirty("TabIndex")))
                {
                    tabIndexChanged = true;
                }
                TabIndex = 0;
            }

            RenderBeginTag(writer);
            RenderContents(writer);
            RenderEndTag(writer);

            if (originalTabIndex != 0)
            {
                TabIndex = originalTabIndex;
            }
            if (tabIndexChanged)
            {
                ViewState.SetItemDirty("TabIndex", false);
            }
        }

        /// <summary>
        /// Overridden to give w3c dom compliant browsers a html4 writer
        /// </summary>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            tagWriter = null;
            base.RenderBeginTag(getCorrectTagWriter(writer));
        }

        /// <summary>
        /// Overrides <see cref="System.Web.UI.WebControls.WebControl.RenderContents"/>
        /// </summary>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("table");
            writer.WriteAttribute("id", ClientID + "_Container");
            writer.WriteAttribute("cellspacing", "0");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("border", "0");
            writer.Write(">");
            writer.Write(writer.NewLine);

            controlToRepeat.Enabled = Enabled;
            controlToRepeat.AutoPostBack = AutoPostBack;

            string onCheckBoxClientClick = OnCheckBoxClientClick;
            for (int i = 0; i < Items.Count; i++)
            {
                writer.RenderBeginTag("tr");
                writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
                writer.RenderBeginTag("td");

                RenderItem(i, writer, onCheckBoxClientClick);

                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            writer.Write(writer.NewLine);
            writer.WriteEndTag("table");
        }

        /// <summary>
        /// Overridden to give w3c dom compliant browsers a html4 writer
        /// </summary>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(getCorrectTagWriter(writer));
        }

        /// <summary>
        /// Renders one ListItem
        /// </summary>
        /// <param name="repeatIndex">The index of the ListItem to render.</param>
        /// <param name="writer">The <see cref="System.Web.UI.HtmlTextWriter"/> to write to.</param>
        /// <param name="onCheckBoxClientClick">The on check box client click.</param>
        protected virtual void RenderItem(int repeatIndex, HtmlTextWriter writer, string onCheckBoxClientClick)
        {
            ListItem item = Items[repeatIndex];
            if (UseValue)
                controlToRepeat.ID = item.Value;
            else
                controlToRepeat.ID = repeatIndex.ToString(NumberFormatInfo.InvariantInfo);

            controlToRepeat.Text = item.Text;
            controlToRepeat.Checked = item.Selected;
            if (!string.IsNullOrEmpty(onCheckBoxClientClick))
                controlToRepeat.Attributes.Add("onclick", Util.EnsureEndWithSemiColon(onCheckBoxClientClick));
            controlToRepeat.Enabled = item.Enabled;

            controlToRepeat.RenderControl(writer);
        }

        #endregion

        #region Overridden simple members

        /// <summary>
        /// Overrides <see cref="System.Web.UI.WebControls.WebControl.TagKey"/>.
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        /// <summary>
        /// Overrides <see cref="System.Web.UI.WebControls.WebControl.TagName"/>.
        /// </summary>
        protected override string TagName
        {
            get { return "div"; }
        }


        /// <summary>
        /// Overrides <see cref="System.Web.UI.WebControls.WebControl.BorderStyle"/>.
        /// </summary>
        [DefaultValue(BorderStyle.Inset)]
        public override BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        /// <summary>
        /// Overrides <see cref="System.Web.UI.WebControls.WebControl.BorderWidth"/>.
        /// </summary>
        [DefaultValue(typeof(Unit), "2px")]
        public override Unit BorderWidth
        {
            get { return base.BorderWidth; }
            set { base.BorderWidth = value; }
        }
        private bool? useValue = null;
        /// <summary>
        /// Gets a value indicating whether [use value].
        /// </summary>
        /// <value><c>true</c> if [use value]; otherwise, <c>false</c>.</value>
        protected bool UseValue
        {
            get
            {
                if (!useValue.HasValue)
                {
                    useValue = true;
                    Dictionary<string, bool> dic = new Dictionary<string, bool>();
                    foreach (ListItem item in Items)
                    {
                        if (string.IsNullOrEmpty(item.Value))
                        {
                            useValue = false;
                            return false;
                        }
                        if (dic.ContainsKey(item.Value))
                        {
                            useValue = false;
                            return false;
                        }
                        dic.Add(item.Value, true);
                    }
                }
                return useValue.Value;
            }
        }

        #endregion


        private HtmlTextWriter getCorrectTagWriter(HtmlTextWriter writer)
        {
            if (tagWriter != null) return tagWriter;

            tagWriter = writer;

            if (writer is Html32TextWriter)
            {
                if (Page != null && Page.Request != null)
                {
                    HttpBrowserCapabilities browser = Page.Request.Browser;
                    if (browser.W3CDomVersion.Major > 0 || (browser.Browser == "Netscape" && browser.MajorVersion >= 5))
                    {
                        if (!(browser.Browser == "Opera" && browser.MajorVersion < 7))
                        {
                            tagWriter = new HtmlTextWriter(writer.InnerWriter);
                        }
                    }
                }
            }

            return tagWriter;
        }


    }
}
