using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Web.UI;
using Site.Bl;

namespace Site.Cotrols
{
    /// <summary>
    /// ProductList
    /// </summary>
    public class ProductList : DropDownListEx
    {

        /// <summary>
        /// Handles the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (DesignMode || Page.IsPostBack) return;


            AddItems(new ProductManager().GetAll(), "Name", "ProductID");
        }
    }
}
