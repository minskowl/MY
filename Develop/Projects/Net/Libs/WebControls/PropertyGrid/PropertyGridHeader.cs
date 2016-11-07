using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Savchin.Web.UI.PropertyGrid
{

    class PropertyGridHeader : GridControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            ID = "active";
            writer.Write(@"
<div class=""PGH PGH_{1}"">
  <img class=""PGH_L"" style=""margin-left:2px"" src=""{3}off.gif"" onclick=""{1}.LiveMode(this)"" title=""Live mode"" alt=""LIVE""/>
  <img class=""PGH_L"" src=""{3}refresh.gif"" onclick=""{1}.GetValues(this)"" title=""Refresh"" alt=""REFRESH""/>
  <img class=""PGH_R"" style=""margin-right:2px"" src=""{3}expand.gif"" onclick=""{1}.ExpandAll(this)"" ondblclick=""{1}.ExpandAll(this)"" title=""Expand all"" alt=""UP""/>
  <img class=""PGH_R"" src=""{3}collapse.gif"" onclick=""{1}.CollapseAll(this)"" ondblclick=""{1}.CollapseAll(this)"" title=""Collapse all"" alt=""DOWN""/>
  <img class=""PGH_R"" src=""{3}help{2}.gif"" onclick=""{1}.ToggleHelp(this)"" title=""Toggle help"" alt=""HELP""/>  
</div>", ClientID, Parent.ClientID,
              ParentGrid.ShowHelp ? "" : "off", ParentGrid.ResPath);
        }
    }
}
