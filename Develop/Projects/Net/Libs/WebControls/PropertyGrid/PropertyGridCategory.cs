using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Savchin.Web.UI.PropertyGrid
{
    class PropertyGridCategory : GridControl
    {
        string catname;

        public string CategoryName
        {
            get { return catname; }
            set { catname = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(@"
<div class=""PGC PGC_{2}"">
<div class=""PGC_OPEN PGC_OPEN_{2}"" onclick=""PGCatToggle(this)""></div>
<div class=""PGC_HEAD PGC_HEAD_{2}""><span>{0}</span></div>
<div id=""{1}"" class=""PGC_WRAP"">", CategoryName, ClientID, ParentGrid.ClientID);

            RenderChildren(writer);

            writer.Write(@"</div></div>");
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ID = "cat" + ParentGrid.catcounter++;
        }


    }
}
