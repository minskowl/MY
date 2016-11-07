// JScript File DashboardManagerWindow.js

var DashboardManagerWindow = function(id,buttonObj)
{
    this.ID=id;
    this.control=$(id);
    this.ButtonObj=buttonObj;
	this.checkboxSuffix="_VisibleBox";
	
	Event.observe(window, 'load', dmInitEvents);
}

DashboardManagerWindow.prototype.dashboardClosed = function(dash)
{
    var chekBox=dbManagerWindow.getDashboardCheckBox(dash);
    chekBox.checked= false;
}

DashboardManagerWindow.prototype.getDashboardCheckBox = function(dashboard)
{
    var checkBoxes= this.control.getElementsBySelector('input[type="checkbox"]');
    for(var i=0; i< checkBoxes.length; i++)
    {
      if(checkBoxes[i].getAttribute("dashboardObjName")==dashboard.objId)
        return checkBoxes[i];
    }
}

DashboardManagerWindow.prototype.Show = function()
{
   this.UpdateCheckBoxState();
   this.ButtonObj.Show();
}

DashboardManagerWindow.prototype.Close = function()
{  
    this.ButtonObj.Hide();
    this.UpdateCheckBoxState();
}

DashboardManagerWindow.prototype.UpdateCheckBoxState = function()
{
    for(var i=0; i<dbEngine.dashboards.length; i++) 
   {
		var dash=dbEngine.dashboards[i];
		this.getDashboardCheckBox(dash).checked=! dash.Settings.Closed;
   }
}
DashboardManagerWindow.prototype.Apply = function()
{
   
   var checkBoxes=findElements(this.control.getElementsByTagName("input"),"input","checkbox",this.checkboxSuffix);
   for(var i=0; i<checkBoxes.length; i++) 
   {
	   var checkBox=checkBoxes[i];
	   eval("var dashboard= " + checkBox.getAttribute("dashboardObjName") + ";");
	   if(checkBox.checked) 
	   {
			dashboard.Show();
	   }
	   else
	   {
		   dashboard.Close();
	   }
   }
   this.ButtonObj.Hide();
}
function dmInitEvents()
{
    dbEngine.DashboardClosed.AddHandler(dbManagerWindow.dashboardClosed);

}