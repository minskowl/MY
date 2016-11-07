var OutlookPanelBar = function()
{
    this._buttonCount = 0; 
    this._splitterId = null;
    this._buttonSuffix = null;
    this._quickLinkSuffix = null;
    this._titleSuffix = null;
    this._buttonRowSuffix = null;
    
    this._buttonClass = null;
    this._buttonHightClass = null;
    this._quickLinkClass = null;
    this._quickLinkHighClass = null;
    this._contentRowSuffix = null;
    
    this._selectedPanelHF = null;
    this._visibleButtonHF = null;
    this._useCookieForPanelIndex = false;
    this._selectedPanelcookieName = null;
    this._useCookieForVisibleButtonCount = false;
    this._buttonCountCookieName = null;
    
    this.OnPanelClicking = null;
    this.OnPanelClicked  = null;
}


/* Private methods */

OutlookPanelBar.prototype._GetButtonByIndex = function(buttonIndex)
{
    return document.getElementById(this._buttonSuffix + buttonIndex);
}


OutlookPanelBar.prototype._GetQuickLinkByIndex = function(buttonIndex)
{
    return document.getElementById(this._quickLinkSuffix + buttonIndex);
}

OutlookPanelBar.prototype._setCookieValue = function(cookieName, value, expires, path, domain, secure) 
{
	// set time, it's in milliseconds
	var today = new Date();
	today.setTime( today.getTime() );
	// if the expires variable is set, make the correct expires time, the
	// current script below will set it for x number of days, to make it
	// for hours, delete * 24, for minutes, delete * 60 * 24
	if ( expires )
	{
		expires = expires * 1000 * 60 * 60 * 24;
	}
	//alert( 'today ' + today.toGMTString() );// this is for testing purpose only
	var expires_date = new Date( today.getTime() + (expires) );
	//alert('expires ' + expires_date.toGMTString());// this is for testing purposes only
    var cookieString = cookieName + "=" +escape( value ) +
		( ( expires ) ? ";expires=" + expires_date.toGMTString() : "" ) + //expires.toGMTString()
		( ( path ) ? ";path=" + path : "" ) + 
		( ( domain ) ? ";domain=" + domain : "" ) +
		( ( secure ) ? ";secure" : "" );
		
	document.cookie = cookieString;
}


/* end of private methods */

/* Selection displaying */
OutlookPanelBar.prototype.RemoveTextSelection = function()
{
     window.event.returnValue = false;
     window.event.cancelBubble = true;
     return false;
}


OutlookPanelBar.prototype.ShowButtonSelection = function(buttonIndex)
{
    var buttonObj = this._GetButtonByIndex(buttonIndex);
    buttonObj.className = this._buttonHightClass;
}

OutlookPanelBar.prototype.RemoveButtonSelection = function(buttonIndex)
{
    var buttonObj = this._GetButtonByIndex(buttonIndex);
    buttonObj.className = this._buttonClass;
}

OutlookPanelBar.prototype.ShowLinkSelection = function(buttonIndex)
{
    var linkObj = this._GetQuickLinkByIndex(buttonIndex);
    linkObj.className = this._quickLinkHighClass;
}

OutlookPanelBar.prototype.RemoveLinkSelection = function(buttonIndex)
{
    var linkObj = this._GetQuickLinkByIndex(buttonIndex);
    linkObj.className = this._quickLinkClass;
}

/* Spliter movement */
OutlookPanelBar.prototype.OnSplitterDown = function(onMoveHandler, onButtonUpHandler)
{
    this._onMoveHandler = onMoveHandler;

    if (document.attachEvent)
    {
	    document.attachEvent("onmousemove", onMoveHandler);
	    document.attachEvent("onmouseup", onButtonUpHandler); 
    }
    else
    {
	    document.addEventListener("mousemove", onMoveHandler, true);
	    document.addEventListener("mouseup", onButtonUpHandler, true); 
    }
}


OutlookPanelBar.prototype.OnSplitterUp = function(onMoveHandler, onButtonUpHandler)
{
    if (document.detachEvent)
    {
        document.detachEvent("onmousemove", onMoveHandler);
        document.detachEvent("onmouseup",onButtonUpHandler); 
    }
    else
    {
	    document.removeEventListener("mousemove", onMoveHandler, true);
	    document.removeEventListener("mouseup", onButtonUpHandler, true); 
    }
}

OutlookPanelBar.prototype.OnSplitterMove = function(evt)
{
    if (document.selection) 
        document.selection.empty();

    var cursorY = window.event ? window.event.clientY : evt.clientY;
    var splitterRow =  document.getElementById(this._splitterId);
    var buttonAreaHeight = splitterRow.parentNode.offsetHeight - cursorY;
    var visibleButtonCount = 0;
    
    for (var i = 0; i < this._buttonCount; i++)
    {
        var row = document.getElementById(this._buttonRowSuffix + i);
        var icon = this._GetQuickLinkByIndex(i);
    
        buttonAreaHeight -= 28;
    
        if (buttonAreaHeight > 0)
        {
            visibleButtonCount++;
            row.style.display = '';
            icon.style.display = 'none';   
        }
        if (buttonAreaHeight < 0)
        {
            row.style.display = 'none';
            icon.style.display = '';   
        }
    }
    
    document.getElementById(this._visibleButtonHF).value = visibleButtonCount;
    
    if (this._useCookieForVisibleButtonCount)
        this._setCookieValue(this._buttonCountCookieName, visibleButtonCount, 10, '/');
}

OutlookPanelBar.prototype.OnClick = function(panelIndex)
{
    if (this.OnPanelClicking)
    {
        if (!this.OnPanelClicking(panelIndex))
            return;
    }
    

    for (var i = 0; i < this._buttonCount; i++)
    {
        var titleObj = document.getElementById(this._titleSuffix + i);
        var contentDivObj = document.getElementById(this._contentRowSuffix + i);
        
        titleObj.style.display = i != panelIndex ? 'none' : '';
        contentDivObj.style.display = i != panelIndex ? 'none' : '';
    }
    
    document.getElementById(this._selectedPanelHF).value = panelIndex;
    
    if (this._useCookieForPanelIndex)
        this._setCookieValue(this._selectedPanelcookieName, panelIndex, 10, '/');
    
    if (this.OnPanelClicked)
        this.OnPanelClicked(panelIndex);
}

