#region Version & Copyright

/* 
 * $Id: FilterComboLayout.cs 24818 2007-11-30 15:56:43Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */

#endregion

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.ImgCheckeBoxComboArrow, Savchin.Web.UI.EmbeddedResources.Gif)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.ImgCheckeBoxComboClear, Savchin.Web.UI.EmbeddedResources.Gif)]

namespace Savchin.Web.UI
{

internal static partial class EmbeddedResources
{
    internal const string ImgCheckeBoxComboArrow = namespaceName + "Lists.arrow.gif";
    internal const string ImgCheckeBoxComboClear = namespaceName + "Lists.clear.gif";
}

    internal class FilterComboLayout : Table
    {
        private readonly ImageEx dropDownImage = new ImageEx();
        private readonly ImageEx clearImage = new ImageEx();
        private readonly TableRow rowFirst = new TableRow();


        private readonly TableCell cellButtonDropDown = new TableCell();
        private readonly TableCell cellButtonClear = new TableCell();
        private readonly TableCell cellTitle = new TableCell();

        #region Properties

        private string jsObjectId;

        /// <summary>
        /// Gets or sets the js object id.
        /// </summary>
        /// <value>The js object id.</value>
        public string JsObjectId
        {
            get { return jsObjectId; }
            set { jsObjectId = value; }
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            set { cellTitle.Text = "&nbsp;" + value; }
        }

        #region ClearButton

        /// <summary>
        /// Sets a value indicating whether [clear button visible].
        /// </summary>
        /// <value><c>true</c> if [clear button visible]; otherwise, <c>false</c>.</value>
        public bool ClearButtonVisible
        {
            set
            {
                if (value)
                    cellButtonClear.Attributes.CssStyle.Remove("display");
                else
                    cellButtonClear.Attributes.CssStyle.Add("display", "none");
            }
        }

        /// <summary>
        /// Gets the clear button client ID.
        /// </summary>
        /// <value>The clear button client ID.</value>
        public string ClearButtonClientID
        {
            get { return cellButtonClear.ClientID; }
        }

        /// <summary>
        /// Sets the clear image URL.
        /// </summary>
        /// <value>The clear image URL.</value>
        public string ClearButtonImageUrl
        {
            set
            {
                if (value == null)
                {
                    clearImage.ImageUrl = GetWebResourceUrl(EmbeddedResources.ImgCheckeBoxComboClear);
                    clearImage.AutoDetectSize = false;
                }
                else
                    clearImage.ImageUrl = value;
            }
        }

        #endregion

        /// <summary>
        /// Sets the drop down image URL.
        /// </summary>
        /// <value>The drop down image URL.</value>
        public string DropDownImageUrl
        {
            set
            {
                if (value == null)
                {
                    dropDownImage.ImageUrl = GetWebResourceUrl(EmbeddedResources.ImgCheckeBoxComboArrow);
                    dropDownImage.AutoDetectSize = false;
                }
                else
                    dropDownImage.ImageUrl = value;
            }
        }

        /// <summary>
        /// Sets the button background image URL.
        /// </summary>
        /// <value>The button background image URL.</value>
        public string ButtonBackgroundImageUrl
        {
            set
            {
                string url = ControlHelper.GetFullImageUrl(value, Page);
                SetBackground(cellButtonDropDown, url);
                SetBackground(cellButtonClear, url);
                //SetBackground(cellTitle, value);
            }
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            CellPadding = 0;
            CellSpacing = 0;

            clearImage.AutoDetectSize = true;
            dropDownImage.AutoDetectSize = true;

            cellButtonClear.ID = ID + "clearButtonCell";
            CreateFirstRow();
        }

        private void CreateFirstRow()
        {
            rowFirst.Cells.Add(cellTitle);


            cellButtonClear.Controls.Add(clearImage);
            rowFirst.Cells.Add(cellButtonClear);
            cellButtonDropDown.Controls.Add(dropDownImage);
            rowFirst.Cells.Add(cellButtonDropDown);

            Rows.Add(rowFirst);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);


            Attributes.CssStyle.Add("cursor", "pointer");
            Attributes.CssStyle.Add("width", "100%");


            cellButtonDropDown.Attributes.CssStyle.Add("width", Height.ToString());
            cellButtonDropDown.Attributes.Add("onclick", JsObjectId + ".Toggle();");
            InitButtonCell(cellButtonDropDown, dropDownImage.Width);

            cellButtonClear.Attributes.CssStyle.Add("width", Height.ToString());
            cellButtonClear.Attributes.Add("onclick", JsObjectId + ".ClearFilter(this);");
            InitButtonCell(cellButtonClear, clearImage.Width);


            cellTitle.HorizontalAlign = HorizontalAlign.Left;
        }

        private void SetBackground(WebControl cell, string bgImage)
        {
            cell.Attributes.CssStyle.Add("background-repeat", "repeat-x");
            cell.Attributes.CssStyle.Add("background-image", "'" + bgImage + "'");
        }

        private void InitButtonCell(TableCell cell, Unit width)
        {
            cell.Width = width;
            cell.BorderStyle = BorderStyle;
            cell.BorderWidth = BorderWidth;
            cell.BorderColor = BorderColor;
            cell.HorizontalAlign = HorizontalAlign.Center;
        }
        protected string GetWebResourceUrl(string resourceName)
        {
            if (Page == null)
                return null;
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), resourceName);
        }
    }
}