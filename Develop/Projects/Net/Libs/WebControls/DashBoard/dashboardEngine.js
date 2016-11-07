// JScript File
var DashboardEngine = function(id)
{   
	this.ID=id;
	this.ManagerPanelID = id + "AddDashboard";
	
	//Options
	this.transparencyWhenDragging = true;
	this.autoScrollEnable = true;   
	this.autoScrollSpeed = 4;	// Autoscroll speed	- Higher = faster	
	
	//Variables
	this.dragDropCounter = -1;
	this.mouse_x=0;
	this.mouse_y=0;

	this.el_x=0;
	this.el_y=0;    
	
	this.dragObject=false;  
	this.dragObjectParent=false;
	this.dragObjectNextSibling = false;
	this.destinationObj=false;
	
	this.documentHeight=0;
	this.documentScrollHeight = false;
	
	this.okToMove = true;
	
	this.rectangleDiv=false;
	
	this.dragObjectBorderWidth = 1

	this.autoScrollActive = false;
	
	this.dashboards = new Array();
	this.dashboardMap = new Array();
	this.dragableZonesArray = new Array();  
	
	this.DashboardClosed = new EventHandlersArray(); 

	addEvent(window,'load',initDashboardEngine);
}
// ============================= Interface


DashboardEngine.prototype.getInstance = function(controlId)
{
	eval("var result=" + controlId + "Obj;");
	return result;
}
DashboardEngine.prototype.addDashBoard = function(obj)
{
	var index=this.dashboards.length;
	this.dashboards[index] = obj;
	this.dashboardMap[obj.ID]=index;
}

DashboardEngine.prototype.addZone = function(id)
{
	var index=this.dragableZonesArray.length;
	this.dragableZonesArray[index]=id;
}
//======================= Common
DashboardEngine.prototype.stopDrag = function()
{
	this.dragDropCounter = -5;
}

DashboardEngine.prototype.isDrag = function()
{
	return(this.dragDropCounter==10)
}

DashboardEngine.prototype.startDragElement = function(e,dashboard)
{

	var obj =dashboard.control;
	this.dragObject = obj;
	this.dragObjectParent = dashboard.parentObj; 
	
	this.mouse_x = e.clientX;
	this.mouse_y = e.clientY;
	
	this.el_x = getLeftPos(obj)/1;
	this.el_y = getTopPos(obj)/1 - document.documentElement.scrollTop;  
	
	this.documentScrollHeight = document.documentElement.scrollHeight + 100 + obj.offsetHeight; 
	
	if (obj.nextSibling) {
		this.dragObjectNextSibling = obj.nextSibling;
		if (this.dragObjectNextSibling.tagName!='DIV')
			this.dragObjectNextSibling = this.dragObjectNextSibling.nextSibling;
	}

	this.dragDropCounter = 0;
}

DashboardEngine.prototype.stopDragElement = function()
{
	if (this.dragDropCounter<10) {
		this.dragDropCounter = -1
		return;
	}

	this.dragDropCounter = -1;
	if (this.transparencyWhenDragging) {
		this.dragObject.style.filter = null;
		this.dragObject.style.opacity = null;
	}

	this.dragObject.style.position = 'static';
	this.dragObject.style.width = null;

	if (this.destinationObj && this.destinationObj.id!=this.dragObject.id) {
		if (this.destinationObj.id.indexOf('dragableBoxesColumn')>=0)
			this.destinationObj.appendChild(this.dragObject);
		else
			this.destinationObj.parentNode.insertBefore(this.dragObject,this.destinationObj);
	} else {
		if (this.dragObjectNextSibling)
			this.dragObjectParent.insertBefore(this.dragObject,this.dragObjectNextSibling);
		else
			this.dragObjectParent.appendChild(this.dragObject);
	}
	this.rectangleDiv.style.display = 'none'; 

	eval("var dashboard=" + this.dragObject.getAttribute("instanceName") +";") ;
	if (dashboard) {
		var zone=this.dragObject.parentNode;
		dashboard.Settings.DragableBoxId=zone.id;
		dashboard.SettingsChanged=true;
		var order=0;
		for (var i=0; i< zone.childNodes.length; i++) {
			var child=zone.childNodes[i];
			if (child.nodeName.toLowerCase()=="div") {
				if (child==this.dragObject) {
					dashboard.Settings.Order=order;
					dashboard.SettingsChanged=true;
					break;
				}
				order++;
			}

		}
	}
	this.autoScrollActive = false;
	this.dragObject = false;
	this.dragObjectNextSibling = false;
	this.destinationObj = false;

	this.documentHeight = document.documentElement.clientHeight;    
}

DashboardEngine.prototype.moveDragableElement = function(e)
{
	if (document.all)e = event;
	if (this.dragDropCounter<10)
		return;

	if (document.all && e.button!=1 && !opera) {
		this.stopDragElement();
		return;
	}

	if (document.body!=this.dragObject.parentNode) {
		this.dragObject.style.width = (this.dragObject.offsetWidth - (this.dragObjectBorderWidth*2)) + 'px';
		this.dragObject.style.position = 'absolute';    
		this.dragObject.style.textAlign = 'left';
		if (this.transparencyWhenDragging) {
			this.dragObject.style.filter = 'alpha(opacity=70)';
			this.dragObject.style.opacity = '0.7';
		}
		this.dragObject.parentNode.insertBefore(this.rectangleDiv,this.dragObject);
		this.rectangleDiv.style.display='block';
		document.body.appendChild(this.dragObject);

		this.rectangleDiv.style.width = this.dragObject.style.width;
		this.rectangleDiv.style.height = (this.dragObject.offsetHeight - (this.dragObjectBorderWidth*2)) + 'px'; 
		
	}
	this.autoScrollCheck(e);
	
	this.dragObject.style.left = (e.clientX - this.mouse_x + this.el_x) + 'px';
	this.dragObject.style.top = (this.el_y - this.mouse_y + e.clientY + document.documentElement.scrollTop) + 'px';
	
	if (!this.okToMove)
		return;

	this.okToMove = false;
	
	this.markTarget(e);
	
	setTimeout('dbEngine.okToMove=true',5);
	//this.okToMove=true	
}

DashboardEngine.prototype.markTarget = function(e)
{
	var objFound = false;
	
	var leftPos = e.clientX;
	var topPos = e.clientY + document.documentElement.scrollTop;

	this.destinationObj = false;    
	this.rectangleDiv.style.display = 'none'; 

	for ( var no=0; no<this.dashboards.length; no++ ) {
		var obj=this.dashboards[no].control;
		
		if (obj==this.dragObject)
			continue;


		var tmpX = getLeftPos(obj);
		var tmpY = getTopPos(obj);

		if (leftPos>tmpX && leftPos<(tmpX + obj.offsetWidth)) {
			
			if (topPos>(tmpY-20) && topPos<(tmpY + (obj.offsetHeight/2))
			   ) {
				this.destinationObj = obj;
				this.destinationObj.parentNode.insertBefore(this.rectangleDiv,obj);
				this.rectangleDiv.style.display = 'block';
				objFound = true;
				break;
			} else if ( topPos>=(tmpY + (obj.offsetHeight/2)) && topPos<(tmpY + obj.offsetHeight)) {
				objFound = true;
				if (obj.nextSibling &&  obj.nextSibling.nodeName!="#text") {
					this.destinationObj = obj.nextSibling;
					if (!this.destinationObj.tagName)
						this.destinationObj = this.destinationObj.nextSibling;
					if (this.destinationObj!=this.rectangleDiv)
						this.destinationObj.parentNode.insertBefore(this.rectangleDiv,this.destinationObj);
				} else {
					this.destinationObj = obj.parentNode;
					obj.parentNode.appendChild(this.rectangleDiv);
				}
				this.rectangleDiv.style.display = 'block';
				break;                  
			} else if (!obj.nextSibling  && topPos>topPos>(tmpY + (obj.offsetHeight))) {
				this.destinationObj = obj.parentNode;
				obj.parentNode.appendChild(this.rectangleDiv);  
				this.rectangleDiv.style.display = 'block';  
				objFound = true;    
				break;          
			}
		}
	}
	
	if (!objFound) {
		for (var no=0;no<this.dragableZonesArray.length;no++) {
			if (!objFound) {
				var obj = $(this.dragableZonesArray[no]);           
				var left = getLeftPos(obj)/1;                       
				
				var width = obj.offsetWidth;
				if (leftPos>left && leftPos<(left+width)) {
					this.destinationObj = obj;
					obj.appendChild(this.rectangleDiv);
					this.rectangleDiv.style.display='block';
					objFound=true;      
				}
			}
		}       
	}
}

DashboardEngine.prototype.autoScrollCheck = function(e)
{
	if (!this.autoScrollEnable)
		return;

	if (e.clientY<50 || e.clientY>(this.documentHeight-50)) {
		if (e.clientY<50 && !this.autoScrollActive) {
			this.autoScrollActive = true;
			this.autoScroll((this.autoScrollSpeed*-1),e.clientY);
		}

		if (e.clientY>(this.documentHeight-50) && 
			document.documentElement.scrollHeight<=this.documentScrollHeight && 
			!this.autoScrollActive) {
			this.autoScrollActive = true;
			this.autoScroll(this.autoScrollSpeed,e.clientY);
		}
	} else {
		this.autoScrollActive = false;
	}         
}


DashboardEngine.prototype.autoScroll = function(direction,yPos)
{
	if (document.documentElement.scrollHeight>this.documentScrollHeight && direction>0)
		return;
	if (opera)
		return;

	window.scrollBy(0,direction);
	if (!this.dragObject)
		return;

	if (direction<0) {
		if (document.documentElement.scrollTop>0) {
			dragObject.style.top = (this.el_y - this.mouse_y + yPos + document.documentElement.scrollTop) + 'px';       
		} else {
			this.autoScrollActive = false;
		}
	} else {
		if (yPos>(this.documentHeight-50)) {
			this.dragObject.style.top = (this.el_y - this.mouse_y + yPos + document.documentElement.scrollTop) + 'px';          
		} else {
			this.autoScrollActive = false;
		}
	}
	if (this.autoScrollActive)
		setTimeout('dbEngine.autoScroll(' + direction + ',' + yPos + ')',5);
}





function dbCancelEvent()
{
	return false;
}

function dbMoveDragableElement(e)
{
	dbEngine.moveDragableElement(e);
}
function dbStopDragElement(e)
{
	dbEngine.stopDragElement(e);
}
function dbCancelSelectionEvent(e)
{
	if (document.all)e = event;

	if (e.target)
		source = e.target;
	else if (e.srcElement)
		source = e.srcElement;

	if (source.nodeType == 3) // defeat Safari bug
		source = source.parentNode;

	if (source.tagName.toLowerCase()=='input')
		return true;

	return dbEngine.dragDropCounter < 0;
}
function dbPageUload(e)
{
	
	for (var i=0; i<dbEngine.dashboards.length; i++) {
		var dash=dbEngine.dashboards[i];
		dash.SaveSettings();
	}
}

function initDashboardEngine()
{
	// Creating support div 
	var obj=document.createElement('DIV');
	dbEngine.rectangleDiv = obj;
	obj.id='rectangleDiv';
	obj.style.display='none';
	document.body.appendChild(obj);
	
	document.body.onmousemove = dbMoveDragableElement;
	document.body.onmouseup = dbStopDragElement;
	document.body.onselectstart = dbCancelSelectionEvent;
	document.body.ondragstart = dbCancelEvent;  
	dbEngine.documentHeight = document.documentElement.clientHeight;
	addEvent(window,'unload', dbPageUload); 

}


