var Dashboard = function(id, imageUrl, settingsJSON, saveSettingsURL)
{
    this.ID=id;
    this.objId=id + "Obj";

    this.control=$(this.ID);

    this.headerId= id + '_dragableBoxHeader';
    this.headerControlsPlaceHolderId= id + '_headerControls';   
    this.contentId= id + '_Layout_Content';   
     
    this.buttonExpandId=  id + '_Layout_Header_ButtonExpand';
    this.buttonRefreshId= id + '_Layout_Header_ButtonRefresh'; 
    this.buttonCloseId=id + '_Layout_Header_ButtonClose'; 
    this.buttonEditId= id + '_dragableBoxEditLink'; 
    
    this.parentObj=this.control.parentNode  
    
    this.src_rightImage = imageUrl + 'arrow_right.gif';
	this.src_downImage = imageUrl + 'arrow_down.gif';    
	
    this.Settings =  settingsJSON.parseJSON();
	this.SettingsChanged= false;
	this.SaveSettingsURL =saveSettingsURL;
	this.OnSettingsChanged= new EventHandlersArray();
}

Dashboard.prototype.init = function()
{
    dbEngine.addDashBoard(this);
	if(this.Settings.Closed)
	  	this.control.style.display='none';
    this.Expand(this.Settings.Expanded);	
	this.SettingsChanged= false;
}


// =============================   Interface

Dashboard.prototype.GetContentFromURL = function(url)
{
    var container=$(this.contentId);
   	var a = new sack();
	a.onCompletion = function(){ container.innerHTML=a.response; };
	a.requestFile = url;	
	a.runAJAX();		
		
}

Dashboard.prototype.SaveSettings = function(e)
{
	if(!this.SettingsChanged) return;

   	var a = new sack();
   	a.setVar("dashboardId",this.ID);
   	a.setVar("action","saveSettings");
   	a.setVar("settings",this.Settings.toJSONString());
	a.requestFile = this.SaveSettingsURL;	
	a.runAJAX();		
		
}
Dashboard.prototype.Close = function()
{
	if(this.Settings.Closed) 
		return;
	this.control.style.display='none';	
	
	this.Settings.Closed=true;  
	this.SettingsChanged=true;
	this.OnSettingsChanged.FireEvent(this);
	
    dbEngine.DashboardClosed.FireEvent(this);
	this.stopDragEarly();
}

Dashboard.prototype.Show = function()
{
	if(this.Settings.Closed==false) 
		return;

	this.control.style.display='';
	this.Settings.Closed=false; 
	this.SettingsChanged=true;
	this.OnSettingsChanged.FireEvent(this);
}
	
Dashboard.prototype.Expand = function(expanded)
{
	$(this.contentId).style.display = expanded ? 'block' : 'none';
	$(this.buttonExpandId).src = expanded ?  this.src_rightImage : this.src_downImage;
	
	this.Settings.Expanded = expanded;
	this.SettingsChanged=true;
	this.OnSettingsChanged.FireEvent(this);
}
//  ==========================       header ===================================   
Dashboard.prototype.headerMouseOver = function(id)
{
	if(! dbEngine.isDrag())
	  this.setButtonVisibility('visible');
}

Dashboard.prototype.headerMouseOut = function(id)
{
    this.setButtonVisibility('hidden');
}

Dashboard.prototype.headerMouseDown = function(e)
{
   // debug("headerMouseDown");
	dbEngine.dragDropCounter = 1;

	
	if (e.target) 
	   source = e.target;
	else if (e.srcElement) source = e.srcElement;
	
	if (source.nodeType == 3) // defeat Safari bug
		source = source.parentNode;
	

    var header=	$(this.headerId);
    var dragObject= $(this.ID);
   

    dbEngine.startDragElement(e,this);
		

	this.initDragDropBoxTimer();	
	
	return false;
}

//  ==========================       button Expand ===================================  

Dashboard.prototype.buttonExpandClick = function()
{
    this.Expand(! this.Settings.Expanded);

	this.stopDragEarly();
}
	
//  ==========================       button Close ===================================  
Dashboard.prototype.buttonCloseMouseOver = function()
{
   $(this.buttonCloseId).className = 'closeButton_over';	
   	this.stopDragEarly();
}
Dashboard.prototype.buttonCloseMouseOut = function()
{
   $(this.buttonCloseId).className = 'closeButton';	
}
Dashboard.prototype.buttonCloseClick = function()
{
   this.Close();
}

//===========================       Common =======================
Dashboard.prototype.initDragDropBoxTimer = function()
{
	if(dbEngine.dragDropCounter>=0 && dbEngine.dragDropCounter<10)
	{
		dbEngine.dragDropCounter++;
		setTimeout(this.objId+".initDragDropBoxTimer()",10);
		return;
	}
	if(dbEngine.dragDropCounter==10)
		this.headerMouseOut();
	
}

Dashboard.prototype.stopDragEarly = function()
{
    setTimeout('dbEngine.stopDrag();',5);
}

Dashboard.prototype.setButtonVisibility = function(value)
{
	$(this.buttonExpandId).style.visibility = value;		
	$(this.buttonRefreshId).style.visibility = value;		
	$(this.buttonCloseId).style.visibility = value;
	if($(this.buttonEditId)) 
	  $(this.buttonEditId).style.visibility = value;
		
}