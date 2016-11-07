using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// GridViewEx
    /// </summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class GridViewEx : GridView
    {
        #region Properties

        private Dictionary<int, string> _helpTips = new Dictionary<int, string>();
        /// <summary>
        /// Gets dictionary of grid help tips
        /// </summary>
        public Dictionary<int, string> HelpTips
        {
            get { return _helpTips; }
        }



        /// <summary>
        /// Gets or sets the selections.
        /// </summary>
        /// <value>The selections.</value>
        [Category("Behavior")]
        [BindableAttribute(false)]
        [Description("Data Keys selected rows")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [Themeable(false)]
        [DefaultValueAttribute(null)]
        public ArrayList Selections
        {
            get
            {
                if (ViewState["Selections"] == null)
                    ViewState["Selections"] = new ArrayList();
                return ViewState["Selections"] as ArrayList;
            }

            set
            {
                ViewState["Selections"] = value;
            }
        }

        /// <summary>
        /// Gets the selections long.
        /// </summary>
        /// <value>The selections long.</value>
        [Themeable(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<long> SelectionsLong
        {
            get
            {
                var list = new List<long>();
                foreach (long id in Selections)
                {
                    list.Add(id);
                }
                return list;
            }
            set
            {
                Selections = new ArrayList(value);
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [auto sort order].
        /// </summary>
        /// <value><c>true</c> if [auto sort order]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [BindableAttribute(false)]
        [Description("Enable auto sort order")]
        [Themeable(false)]
        [DefaultValueAttribute(true)]
        public bool AutoSortOrder
        {
            get
            {
                if (ViewState["AutoSortOrder"] == null)
                    return true;
                return (bool)ViewState["AutoSortOrder"];
            }

            set
            {
                ViewState["AutoSortOrder"] = value;
            }
        }

        [Category("Behavior")]
        [BindableAttribute(false)]
        [Description("CAlwaysShowPager")]
        [DefaultValueAttribute(false)]
        public bool AlwaysShowPager
        {
            get
            {
                if (ViewState["AlwaysShowPager"] == null)
                    return false;
                return (bool)ViewState["AlwaysShowPager"];
            }

            set
            {
                ViewState["AlwaysShowPager"] = value;
            }
        }
        #region Properties

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [Description("Image to display for Ascending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue("")]
        public string SortAscImageUrl
        {
            get
            {
                object o = ViewState["SortImageAsc"];
                return (o != null ? (string)o : ImagePathProvider.ArrowDownImage);
            }
            set
            {
                ViewState["SortImageAsc"] = value;
            }
        }
        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [Description("Image to display for Descending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),]
        public string SortDescImageUrl
        {
            get
            {
                object o = ViewState["SortImageDesc"];
                return (o != null ? (string)o : ImagePathProvider.ArrowUpImage);
            }
            set
            {
                ViewState["SortImageDesc"] = value;
            }
        }
        #endregion

        #endregion


        #region Initialize

        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewEx"/> class.
        /// </summary>
        public GridViewEx()
        {
            SetDefaultSettings();
        }

        /// <summary>
        /// Sets the default settings.
        /// </summary>
        private void SetDefaultSettings()
        {
            SkinID = "view";
            AllowPaging = true;
            PageSize = 50;
        }
        #endregion

        /// <summary>
        /// Sets the data source.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        public void SetDataSource(DataTable data, GridViewSortEventArgs e)
        {
            SetDataSource(data, e.SortExpression + (e.SortDirection == SortDirection.Ascending ? " ASC" : " DESC"));
        }

        /// <summary>
        /// Sets the data source.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="sort">The sort.</param>
        public void SetDataSource(DataTable data, string sort)
        {
            if (data != null)
            {
                data.DefaultView.Sort = sort;
                DataSource = data.DefaultView;
            }
            else
            {
                DataSource = null;
            }
            DataBind();
        }

        /// <summary>
        /// Sets the data source.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetDataSource(object data)
        {
            DataSource = data;
            DataBind();
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns></returns>
        public int GetRowIndex(Control sender)
        {
            return ((GridViewRow)(sender.Parent.Parent)).RowIndex;
        }

        #region Data Keys

        /// <summary>
        /// Gets the data key.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns></returns>
        public long GetLongDataKey(Control sender)
        {
            return GetLongDataKey(GetRowIndex(sender));
        }
        /// <summary>
        /// Gets the data key.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public long GetLongDataKey(GridViewCommandEventArgs e)
        {
            return GetLongDataKey(Convert.ToInt32(e.CommandArgument));
        }
        /// <summary>
        /// Gets the data key.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns></returns>
        public long GetLongDataKey(int rowIndex)
        {
            if (DataKeys[rowIndex].Value is int)
                return (int)DataKeys[rowIndex].Value;
            return (long)DataKeys[rowIndex].Value;
        }

        /// <summary>
        /// Gets the data key.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns></returns>
        public int GetIntDataKey(Control sender)
        {
            return GetIntDataKey(GetRowIndex(sender));
        }
        /// <summary>
        /// Gets the data key.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public int GetIntDataKey(GridViewCommandEventArgs e)
        {
            return GetIntDataKey(Convert.ToInt32(e.CommandArgument));
        }
        /// <summary>
        /// Gets the data key.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns></returns>
        public int GetIntDataKey(int rowIndex)
        {
            return (int)DataKeys[rowIndex].Value;
        }
        /// <summary>
        /// Returns dictionary of data key for pscified row index
        /// </summary>
        /// <param name="contol">Control in row</param>
        /// <returns>List of objects</returns>
        public IOrderedDictionary GetDataKeyValues(Control contol)
        {
            return GetDataKeyValues(GetRowIndex(contol));
        }

        /// <summary>
        /// Gets the data key values.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns></returns>
        public IOrderedDictionary GetDataKeyValues(int rowIndex)
        {
            return DataKeys[rowIndex].Values;
        }

        /// <summary>
        /// Gets the data key object.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns></returns>
        public object GetDataKeyObject(Control sender)
        {
            return GetDataKeyObject(((GridViewRow)(sender.Parent.Parent)).RowIndex);
        }
        /// <summary>
        /// Gets the data key object.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns></returns>
        public object GetDataKeyObject(int rowIndex)
        {
            return DataKeys[rowIndex].Value;
        }
        #endregion

        #region get controls

        #region Control

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public Control GetControl(GridViewRowEventArgs eventArgs, int columnIndex)
        {
            return GetControl(eventArgs, columnIndex, 0);
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public Control GetControl(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {
            return eventArgs.Row.Cells[columnIndex].Controls[controlIndex];
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public Control GetControl(int rowIndex, int columnIndex, int controlIndex)
        {
            return Rows[rowIndex].Cells[columnIndex].Controls[controlIndex];
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public Control GetControl(GridViewRow row, int columnIndex, int controlIndex)
        {
            return row.Cells[columnIndex].Controls[controlIndex];
        }
        #endregion

        #region ImageButton

        /// <summary>
        /// Gets the image button.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public ImageButton GetImageButton(int rowIndex, int columnIndex)
        {
            return GetImageButton(rowIndex, columnIndex, 0);
        }
        /// <summary>
        /// Gets the image button.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public ImageButton GetImageButton(int rowIndex, int columnIndex, int controlIndex)
        {
            return (ImageButton)Rows[rowIndex].Cells[columnIndex].Controls[controlIndex];
        }
        /// <summary>
        /// Gets the image button.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public ImageButton GetImageButton(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {
            return (ImageButton)eventArgs.Row.Cells[columnIndex].Controls[controlIndex];
        }
        #endregion

        #region Button
        /// <summary>
        /// Gets the Button.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public Button GetButton(GridViewRowEventArgs eventArgs, int columnIndex)
        {
            return GetButton(eventArgs, columnIndex, 0);

        }
        /// <summary>
        /// Gets the Button.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public Button GetButton(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {
            return (Button)eventArgs.Row.Cells[columnIndex].Controls[controlIndex];
        }

        /// <summary>
        /// Gets the Button.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public Button GetButton(int rowIndex, int columnIndex)
        {
            return GetButton(rowIndex, columnIndex, 0);
        }

        /// <summary>
        /// Gets the Button.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public Button GetButton(int rowIndex, int columnIndex, int controlIndex)
        {
            return (Button)GetControl(rowIndex, columnIndex, controlIndex);
        }



        #endregion

        #region ButtonEx
        /// <summary>
        /// Gets the ButtonEx.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public ButtonEx GetButtonEx(GridViewRowEventArgs eventArgs, int columnIndex)
        {
            return GetButtonEx(eventArgs, columnIndex, 0);

        }
        /// <summary>
        /// Gets the ButtonEx.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public ButtonEx GetButtonEx(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {
            return (ButtonEx)GetUserControl(eventArgs, columnIndex, controlIndex);
        }

        /// Gets the ButtonEx.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public Control GetUserControl(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {
            return eventArgs.Row.Cells[columnIndex].Controls[controlIndex];
        }

        /// <summary>
        /// Gets the ButtonEx.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public ButtonEx GetButtonEx(int rowIndex, int columnIndex)
        {
            return GetButtonEx(rowIndex, columnIndex, 0);
        }

        /// <summary>
        /// Gets the ButtonEx.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public ButtonEx GetButtonEx(int rowIndex, int columnIndex, int controlIndex)
        {
            return (ButtonEx)GetControl(rowIndex, columnIndex, controlIndex);
        }

        /// <summary>
        /// Gets the button ex.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public ButtonEx GetButtonEx(GridViewRow row, int columnIndex, int controlIndex)
        {
            return (ButtonEx)GetControl(row, columnIndex, controlIndex);
        }

        #endregion

        #region Text Box
        /// <summary>
        /// Gets the Text Box.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public TextBox GetTextBox(GridViewRowEventArgs eventArgs, int columnIndex)
        {
            return GetTextBox(eventArgs, columnIndex, 0);

        }
        /// <summary>
        /// Gets the Text Box.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public TextBox GetTextBox(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {

            return (TextBox)eventArgs.Row.Cells[columnIndex].Controls[controlIndex];
        }

        /// <summary>
        /// Gets the Text Box.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public TextBox GetTextBox(int rowIndex, int columnIndex)
        {
            return GetTextBox(rowIndex, columnIndex, 0);
        }

        /// <summary>
        /// Gets the Text Box.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public TextBox GetTextBox(int rowIndex, int columnIndex, int controlIndex)
        {
            return GetTextBox(Rows[rowIndex], columnIndex, controlIndex);
        }
        /// <summary>
        /// Gets the text box.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public TextBox GetTextBox(GridViewRow row, int columnIndex)
        {
            return GetTextBox(row, columnIndex, 0);
        }
        /// <summary>
        /// Gets the text box.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public TextBox GetTextBox(GridViewRow row, int columnIndex, int controlIndex)
        {
            return (TextBox)row.Cells[columnIndex].Controls[controlIndex];
        }
        #endregion

        #region Hyper Link
        /// <summary>
        /// Gets the Hyper Link.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public HyperLink GetHyperLink(GridViewRowEventArgs eventArgs, int columnIndex)
        {
            return GetHyperLink(eventArgs, columnIndex, 0);

        }
        /// <summary>
        /// Gets the Hyper Link.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public HyperLink GetHyperLink(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {

            return (HyperLink)eventArgs.Row.Cells[columnIndex].Controls[controlIndex];
        }

        /// <summary>
        /// Gets the Hyper Link.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public HyperLink GetHyperLink(int rowIndex, int columnIndex)
        {
            return GetHyperLink(rowIndex, columnIndex, 0);
        }

        /// <summary>
        /// Gets the Hyper Link.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public HyperLink GetHyperLink(int rowIndex, int columnIndex, int controlIndex)
        {
            return GetHyperLink(Rows[rowIndex], columnIndex, controlIndex);
        }
        /// <summary>
        /// Gets the Hyper Link.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public HyperLink GetHyperLink(GridViewRow row, int columnIndex)
        {
            return GetHyperLink(row, columnIndex, 0);
        }
        /// <summary>
        /// Gets the Hyper Link.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public HyperLink GetHyperLink(GridViewRow row, int columnIndex, int controlIndex)
        {
            return (HyperLink)row.Cells[columnIndex].Controls[controlIndex];
        }
        #endregion

        #region Check Box


        /// <summary>
        /// Gets the check box.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public CheckBox GetCheckBox(GridViewRowEventArgs eventArgs, int columnIndex)
        {
            return GetCheckBox(eventArgs, columnIndex, 0);

        }
        /// <summary>
        /// Gets the check box.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public CheckBox GetCheckBox(GridViewRowEventArgs eventArgs, int columnIndex, int controlIndex)
        {
            return (CheckBox)eventArgs.Row.Cells[columnIndex].Controls[controlIndex];
        }

        /// <summary>
        /// Gets the check box.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public CheckBox GetCheckBox(GridViewRow row, int columnIndex)
        {
            return GetCheckBox(row, columnIndex, 0);
        }
        /// <summary>
        /// Gets the check box.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public CheckBox GetCheckBox(GridViewRow row, int columnIndex, int controlIndex)
        {
            return (CheckBox)row.Cells[columnIndex].Controls[controlIndex];
        }
        /// <summary>
        /// Gets the check box.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public CheckBox GetCheckBox(int rowIndex, int columnIndex)
        {
            return GetCheckBox(rowIndex, columnIndex, 0);
        }

        /// <summary>
        /// Gets the check box.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="controlIndex">Index of the control.</param>
        /// <returns></returns>
        public CheckBox GetCheckBox(int rowIndex, int columnIndex, int controlIndex)
        {
            return GetCheckBox(Rows[rowIndex], columnIndex, controlIndex);
        }


        #endregion

        #endregion

        #region Overides

        /// <summary>
        /// Creates the control hierarchy that is used to render a composite data-bound control based on the values that are stored in view state.
        /// </summary>
        protected override void CreateChildControls()
        {
            if (!DesignMode && !Page.IsPostBack && AutoSortOrder)
                SetDefaultSortOrder();

            base.CreateChildControls();
        }

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int returnValue = base.CreateChildControls(dataSource, dataBinding);
            if (this.TopPagerRow != null && this.AlwaysShowPager)
            {
                this.TopPagerRow.Visible = true;
            }
            if (this.BottomPagerRow != null && this.AlwaysShowPager)
            {
                this.BottomPagerRow.Visible = true;
            }
            return returnValue;
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            InitColumnApperance();


        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowCreated"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs"></see> that contains event data.</param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);

            if (e.Row.RowType == DataControlRowType.Header)
                SetupHeaderRow(e);
        }


        #endregion

        #region Protected Methods
        /// <summary>
        /// Setups the header row.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        private void SetupHeaderRow(GridViewRowEventArgs e)
        {
            if (AllowSorting)
            {
                ShowSortDirection(e.Row);
            }

            AddHelpTips(e.Row);
        }

        /// <summary>
        /// Adds tool tip controls to header
        /// </summary>
        /// <param name="headerRow">The header row.</param>
        private void AddHelpTips(GridViewRow headerRow)
        {
            if (_helpTips.Keys.Count == 0)
                return;
            int columnCnt = headerRow.Cells.Count;

            foreach (int index in _helpTips.Keys)
            {
                if (index < 0 && index >= columnCnt)
                    continue;

                HelpTip helpTipControl = new HelpTip();
                helpTipControl.HelpTipHtml = _helpTips[index];

                LiteralControl emptyControl = new LiteralControl();

                if (headerRow.Cells[index].Controls.Count == 0)
                    emptyControl.Text = headerRow.Cells[index].Text + "&nbsp;";
                else
                    emptyControl.Text = "&nbsp;";

                headerRow.Cells[index].Controls.Add(emptyControl);
                headerRow.Cells[index].Controls.Add(helpTipControl);
            }
        }

        #region Multi Sorting

        /// <summary>
        /// Shows the sort direction.
        /// </summary>
        /// <param name="row">The row.</param>
        private void ShowSortDirection(GridViewRow row)
        {
            if (!string.IsNullOrEmpty(SortExpression))
            {
                DisplaySortOrderImages(SortExpression, row);
            }
            else if (DataSource != null && DataSource is DataView)
            {
                string temp = ((DataView)DataSource).Sort;
                if (!string.IsNullOrEmpty(temp))
                    DisplaySortOrderImages(temp.Split(' ')[0], row);
            }
        }


        /// <summary>
        ///  Display a graphic image for the Sort Order along with the sort sequence no.
        /// </summary>
        protected void DisplaySortOrderImages(string dataSortExpr, GridViewRow viewRow)
        {


            for (int i = 0; i < viewRow.Cells.Count; i++)
            {
                if (Columns[i].SortExpression == SortExpression &&
                    viewRow.Cells[i].Controls.Count > 0 &&
                    viewRow.Cells[i].Controls[0] is LinkButton)
                {
                    var sortImgLoc = (SortDirection == SortDirection.Ascending ? SortAscImageUrl : SortDescImageUrl);

                    if (!string.IsNullOrEmpty(sortImgLoc))
                    {
                        var imgSortDirection = new ImageEx
                                                   {
                                                       AutoDetectSize = true, 
                                                       ImageUrl = sortImgLoc
                                                   };
                        viewRow.Cells[i].Controls.Add(imgSortDirection);

                    }
                    else
                    {

                        Label lblSortDirection = new Label();
                        lblSortDirection.Font.Size = FontUnit.XSmall;
                        lblSortDirection.Font.Name = "webdings";
                        lblSortDirection.EnableTheming = false;
                        lblSortDirection.Text = (SortDirection == SortDirection.Ascending ? "5" : "6");
                        viewRow.Cells[i].Controls.Add(lblSortDirection);
                    }

                    break;

                }
            }

        }
        #endregion

        #region Initalization


        private void SetDefaultSortOrder()
        {
            string expression = string.Empty;

            foreach (DataControlField field in Columns)
            {
                if (field is BoundField && !(field is CheckBoxField))
                {
                    BoundField boundField = (BoundField)field;
                    if ((boundField.DataFormatString == "{0:d}" || boundField.DataFormatString == "{0:D}") &&
                        !String.IsNullOrEmpty(boundField.SortExpression)
                        )
                    {
                        expression = boundField.SortExpression;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(expression))
                return;

            Sort(expression, SortDirection.Descending);
        }

        private void InitColumnApperance()
        {
            foreach (DataControlField field in Columns)
            {
                field.HeaderStyle.CssClass = "grdHeaderColumn";
                if (string.IsNullOrEmpty(field.ItemStyle.CssClass))
                    field.ItemStyle.CssClass = "grdCell";

                if (field is CommandField)
                {
                    var comField = (CommandField)field;
                    comField.ButtonType = ButtonType.Image;

                    comField.CancelImageUrl = ImagePathProvider.CancelImage;
                    comField.DeleteImageUrl = ImagePathProvider.DeleteImage;
                    comField.EditImageUrl = ImagePathProvider.EditImage;
                    comField.UpdateImageUrl = ImagePathProvider.UpdateImage;
                }

            }
        }
        #endregion

        #endregion

        #region Multy Selection

        public void SelectionCheckBox_Init(object sender, EventArgs e)
        {
            try
            {
                CheckBox rowCheckBox = (CheckBox)sender;
                rowCheckBox.Checked = Selections.Contains(GetDataKeyObject(rowCheckBox));
            }
            catch (Exception ex)
            {
                Util.Log.Error("DridViewEx::SelectionCheckBox_Init", ex);
            }
        }
        /// <summary>
        /// Handles the CheckedChanged event of the SelectionCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void SelectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var rowCheckBox = (CheckBox)sender;
                var key = GetDataKeyObject(rowCheckBox);
                var inSelection = Selections.Contains(key);
                if (rowCheckBox.Checked && !inSelection)
                {
                    Selections.Add(key);
                }
                else if (!rowCheckBox.Checked && inSelection)
                {
                    Selections.Remove(key);
                }
            }
            catch (Exception ex)
            {
                Util.Log.Error("DridViewEx::SelectionCheckBox_CheckedChanged", ex);
            }

        }
        #endregion
    }

}
