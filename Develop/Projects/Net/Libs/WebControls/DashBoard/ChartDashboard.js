// JScript File ChartDashboard.js

var ChartDashboard= function (objectName,dashboard, buttonBarID,buttonLineID,buttonWeekID,
                              buttonMonthID,buttonDayID,ajaxUrl,settings)
{
	this.ObjectName=objectName;

	this.ButtonBar=$(buttonBarID);
	this.ButtonLine=$(buttonLineID);

	this.ButtonWeek=$(buttonWeekID);
	this.ButtonMonth=$(buttonMonthID);
	this.ButtonDay=$(buttonDayID);	

	this.Dashboard=dashboard;
	this.AJAXUrl=ajaxUrl;
	this.Settings =  settings.parseJSON();
	this.Loaded=false;
}

ChartDashboard.prototype.Init=function()
{
	var f=this.ObjectName +".Load();";
	addEvent(window,"load",function(){eval(f);});
}
ChartDashboard.prototype.Load=function()
{
	if (this.ButtonBar) 
	{
		this.ButtonBar.checked=this.Settings.ChartType == 0;
		this.ButtonLine.checked=this.Settings.ChartType == 1;
	}

	if (this.ButtonWeek) 
	{
		this.ButtonWeek.checked=this.Settings.TimeFrameType == 0;
		this.ButtonMonth.checked=this.Settings.TimeFrameType == 1;
		this.ButtonDay.checked=this.Settings.TimeFrameType == 2;
	}
    
    if(!this.Dashboard.Settings.Closed && this.Dashboard.Settings.Expanded)
	    this.GetContent("show");
	    
	this.InitHandlers();   
}
ChartDashboard.prototype.InitHandlers=function()
{
    var chart=this;
    this.Dashboard.OnSettingsChanged.AddHandler(
    function(dash)
    {  
      if(!chart.Loaded  && !dash.Settings.Closed && dash.Settings.Expanded)
        chart.GetContent("show");
    }
    );
}
ChartDashboard.prototype.ApplyGraphType=function()
{
	var type=this.ButtonBar.checked ? 0 :1;

	var vars= new Array(1);
	vars["type"] =type;
	this.GetContent("applyGraphType",vars);
}
ChartDashboard.prototype.ApplyTimeFrame=function()
{
	var type=-1;
	
	if(this.ButtonWeek.checked)
	  type=0;
	else if(this.ButtonMonth.checked)  
	  type=1;
	else if(this.ButtonDay.checked) 
	  type=2;

	if(type<0)return;
	
	var vars= new Array(1);
	vars["frame"] =type;
	this.GetContent("applyTimeFrame",vars);
}
ChartDashboard.prototype.GetContent = function(action,vars)
{
	var a = new sack();
	var container =$(this.Dashboard.contentId)
	var obj =this;
	if (vars) a.setVars(vars);
	a.setVar("height",container.clientHeight);
	a.setVar("width",container.clientWidth);

	a.onCompletion = function()
	{
	  container.innerHTML=a.response;
	  obj.Loaded=true;
	};
	a.requestFile =  this.AJAXUrl + action; 
	a.runAJAX()
}
ChartDashboard.prototype.getTopWindow = function()
{
  var win=window;
  while(win.parent !=null && win.parent!=win)
  {
   win=win.parent;
  }
  return win;
}
ChartDashboard.prototype.ShowZoomWindow = function()
{
    var win=this.getTopWindow();
    
	var width=win.document.body.offsetWidth - 20;
	var height=win.document.body.offsetHeight;
	
	if (width<600) width=600;
	if (width<400) width=400;


	var l=(window.screen.availWidth / 2 - width / 2);
	var t = (window.screen.availHeight / 2 - height / 2)
	var features='status=no,width=' + width + ',height=' + height +  ',left=' + l  + ',top=' + t;

	var  w= window.open("/WAMUser/DashBoards/zoom.htm",'Zoom',features);


	var a = new sack();

	a.setVar("height",height - 20);
	a.setVar("width",width);
	a.onCompletion = function()
	{ 
		w.document.write(a.response); 
	};
	a.requestFile =  this.AJAXUrl + "show"; 
	a.runAJAX();        
	
}

