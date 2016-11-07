#region Version & Copyright
/* 
 * $Id: HeaderControl.cs 19126 2007-07-23 12:40:05Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Savchin.Web.UI
{

    public class HeaderControl : HtmlTable
    {
        #region Properties
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
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return cellTitle.InnerHtml; }
            set { cellTitle.InnerHtml = value; }
        }

        /// <summary>
        /// Gets the additional controls.
        /// </summary>
        /// <value>The additional controls.</value>
        public ControlCollection AdditionalControls
        {
            get { return cellControls.Controls; }
        }
        /// <summary>
        /// Gets or sets the width of the additional controls.
        /// </summary>
        /// <value>The width of the additional controls.</value>
        public string AdditionalControlsWidth
        {
            get { return cellControls.Attributes.CssStyle["width"]; }
            set { cellControls.Attributes.CssStyle.Add("width", value); }
        }
        #endregion


        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cellExpand = new HtmlTableCell();
        HtmlTableCell cellTitle = new HtmlTableCell();
        HtmlTableCell cellButtons = new HtmlTableCell();
        HtmlTableCell cellControls = new HtmlTableCell();

        HtmlGenericControl closeButton = new HtmlGenericControl("SPAN");
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderControl"/> class.
        /// </summary>
        public HeaderControl()
        {

        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CellPadding = 0;
            CellSpacing = 0;
            Border = 0;
            Width = "100%";

            Attributes.Add("onmouseover", JSObjectName + ".headerMouseOver();");
            Attributes.Add("onmouseout", JSObjectName + ".headerMouseOut();");

            row.Attributes.Add("class", "dragableBoxHeader");
            Rows.Add(row);

            CreateExpandButtonCell();
            CreatTitleCell();
            CreateControlsCell();
            CreateButtonsCell();

        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (AdditionalControls.Count == 0)
            {
                row.Cells.Remove(cellControls);
            }
        }

        #region Create Cells
        private void CreateExpandButtonCell()
        {
            cellExpand.Attributes.CssStyle.Add("width", "20px");


            HtmlImage image = new HtmlImage();
            image.ID = ID + "_ButtonExpand";
            image.Src = imageUrl + "arrow_right.gif";
            image.Attributes.Add("onmousedown", JSObjectName + ".buttonExpandClick();");
            image.Attributes.CssStyle.Add("visibility", "hidden");
            image.Attributes.CssStyle.Add("cursor", "pointer");
            cellExpand.Controls.Add(image);
            row.Cells.Add(cellExpand);
        }
        private void CreatTitleCell()
        {
            cellTitle.ID = ID + "_Title";
            //cellTitle.Attributes.CssStyle.Add("width", "90%");


            cellTitle.Attributes.Add("onmousedown", JSObjectName + ".headerMouseDown(event);");
            cellTitle.Attributes.CssStyle.Add("cursor", "move");
            row.Cells.Add(cellTitle);

        }
        private void CreateControlsCell()
        {
            cellControls.Align = "right";
            row.Cells.Add(cellControls);
        }

        private void CreateButtonsCell()
        {
            cellButtons.ID = ID + "_CellControls";
            cellButtons.Align = "right";
            cellExpand.Attributes.CssStyle.Add("width", "20px");

            CreateRefreshButton();
            CreateCloseButton();

            row.Cells.Add(cellButtons);
        }
        #endregion

        private void CreateRefreshButton()
        {
            HtmlImage image = new HtmlImage();
            image.Src = imageUrl + "refresh.gif";
            image.ID = ID + "_ButtonRefresh"; ;
            image.Attributes.CssStyle.Add("visibility", "hidden");
            image.Attributes.CssStyle.Add("cursor", "pointers");
            image.Attributes.CssStyle.Add("display", "none");

            cellButtons.Controls.Add(image);
        }

        private void CreateCloseButton()
        {
            closeButton.Attributes.Add("class", "closeButton");
            closeButton.Attributes.CssStyle.Add("cursor", "pointers");
            closeButton.Attributes.CssStyle.Add("visibility", "hidden");
            closeButton.ID = ID + "_ButtonClose"; ;
            closeButton.Attributes.Add("onmouseover", JSObjectName + ".buttonCloseMouseOver();");
            closeButton.Attributes.Add("onmouseout", JSObjectName + ".buttonCloseMouseOut();");
            closeButton.Attributes.Add("onmousedown", JSObjectName + ".buttonCloseClick();");
            closeButton.InnerText = "x";

            cellButtons.Controls.Add(closeButton);
        }





    }
}
