using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    public class HeaderParagraph : Label 
    {
        protected override System.Web.UI.HtmlTextWriterTag TagKey
        {
            get
            {
                return System.Web.UI.HtmlTextWriterTag.H2;
            }
        }
    }
}
