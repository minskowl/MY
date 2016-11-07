#region Version & Copyright
/* 
 * $Id: DashboardLayout.cs 19126 2007-07-23 12:40:05Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    public class DashboardLayout : HtmlTable
    {
        private HtmlGenericControl divScrollArea = new HtmlGenericControl("DIV");
        private HtmlGenericControl divContainer = new HtmlGenericControl("DIV");
        private HtmlTableRow rowHeader = new HtmlTableRow();
        private HtmlTableRow rowContent = new HtmlTableRow();
        private HtmlTableRow rowStatus = new HtmlTableRow();
        private HtmlTableCell cellHeader = new HtmlTableCell();
        private HtmlTableCell cellContent = new HtmlTableCell();
        private HtmlTableCell cellStatus = new HtmlTableCell();

        private HeaderControl headerControl = new HeaderControl();


        #region Properties

        private ScrollBars contentScrollBars;

        /// <summary>
        /// Gets or sets the content scroll bars.
        /// </summary>
        /// <value>The content scroll bars.</value>
        public ScrollBars ContentScrollBars
        {
            get { return contentScrollBars; }
            set { contentScrollBars = value; }
        }



        private string jsObjectName;
        /// <summary>
        /// Gets or sets the name of the JS object.
        /// </summary>
        /// <value>The name of the JS object.</value>
        public string JSObjectName
        {
            get { return jsObjectName; }
            set { jsObjectName = value; }
        }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return Header.Title; }
            set { Header.Title = value; }
        }
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        private string imageUrl;
        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public HtmlGenericControl Container
        {
            get { return divContainer; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [status bar visible].
        /// </summary>
        /// <value><c>true</c> if [status bar visible]; otherwise, <c>false</c>.</value>
        public bool StatusBarVisible
        {
            get { return statusBarVisible; }
            set
            {
                statusBarVisible = value;
                if (statusBarVisible)
                    rowStatus.Attributes.CssStyle.Remove("display");
                else
                    rowStatus.Attributes.CssStyle.Add("display", "none");
            }
        }

        /// <summary>
        /// Gets or sets the height of the content.
        /// </summary>
        /// <value>The height of the content.</value>
        public Unit ContentHeight
        {
            get { return contentHeight; }
            set
            {
                contentHeight = value;
                divScrollArea.Attributes.CssStyle.Add("height", value.ToString());
            }
        }

        public HeaderControl Header
        {
            get { return headerControl; }
        }


        private bool statusBarVisible;

        private Unit contentHeight;

        #endregion




        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CellPadding = 0;
            CellPadding = 0;
            Border = 0;
            Attributes.Add("class", "dragableBoxInner");

            CreateHeaderRow();
            CreateContentRow();
            CreateStatusRow();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            switch (contentScrollBars)
            {
                case ScrollBars.None:
                    divScrollArea.Attributes.CssStyle.Add("overflow-x", "hidden");
                    divScrollArea.Attributes.CssStyle.Add("overflow-y", "hidden");
                    break;
                case ScrollBars.Horizontal:
                    divScrollArea.Attributes.CssStyle.Add("overflow-x", "scroll");
                    divScrollArea.Attributes.CssStyle.Add("overflow-y", "hidden");
                    break;
                case ScrollBars.Vertical:
                    divScrollArea.Attributes.CssStyle.Add("overflow-x", "hidden");
                    divScrollArea.Attributes.CssStyle.Add("overflow-y", "scroll");
                    break;
                case ScrollBars.Both:
                    divScrollArea.Attributes.CssStyle.Add("overflow", "scroll");
                    break;
                case ScrollBars.Auto:
                    divScrollArea.Attributes.CssStyle.Add("overflow", "auto");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void CreateStatusRow()
        {
            cellStatus.Attributes.Add("class", "dragableBoxStatusBar");
            cellStatus.ID =ID + "_StatusBar";

            Rows.Add(rowStatus);
            rowStatus.Cells.Add(cellStatus);
        }

        private void CreateHeaderRow()
        {
            Rows.Add(rowHeader);

            cellHeader.ID =ID + "_HeaderCell";
            cellHeader.Attributes.Add("class", "dragableBoxHeader");

            Header.ID = ID + "_Header";
            Header.JSObjectName = JSObjectName;
            Header.ImageUrl = imageUrl;

            cellHeader.Controls.Add(Header);
            rowHeader.Cells.Add(cellHeader);


        }

        private void CreateContentRow()
        {
            Rows.Add(rowContent);

            rowContent.Cells.Add(cellContent);
            divScrollArea.Attributes.Add("class", "dragableBoxContent");
            divScrollArea.ID = ID + "_Content";
            divScrollArea.Controls.Add(divContainer);
            cellContent.Controls.Add(divScrollArea);
        }
    }
}
