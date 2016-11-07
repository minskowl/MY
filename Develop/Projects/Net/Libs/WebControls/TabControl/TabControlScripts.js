// TabControlScripts.js


var TabControl = function(
    buttonACssClass,
    selButtonACssClass,
    hotButtonACssClass,

    buttonBCssClass,
    selButtonBCssClass,
    hotButtonBCssClass,

    buttonCCssClass,
    selButtonCCssClass,
    hotButtonCCssClass,
    
    buttonPrefix,
    selectionHiddenFieldId,
    contentPanelPrefix,
    contentPanelCount,
    hiddenButtonListFieldId,
    separatorPrefix
)
{
    this._buttonACssClass    = buttonACssClass;
    this._selButtonACssClass = selButtonACssClass;
    this._hotButtonACssClass = hotButtonACssClass;

    this._buttonBCssClass    = buttonBCssClass;
    this._selButtonBCssClass = selButtonBCssClass;
    this._hotButtonBCssClass = hotButtonBCssClass;

    this._buttonCCssClass    = buttonCCssClass;
    this._selButtonCCssClass = selButtonCCssClass;
    this._hotButtonCCssClass = hotButtonCCssClass;

    this._buttonPrefix = buttonPrefix;
    this._selectionHiddenFieldId = selectionHiddenFieldId;
    this._contentPanelPrefix = contentPanelPrefix;
    this._separatorPrefix = separatorPrefix;
    this._contentPanelCount = contentPanelCount;
    this._hiddenButtonListFieldId = hiddenButtonListFieldId;
    
    
    this.OnPanelClick = null;
}

/* Private methods */

TabControl.prototype._GetAButton = function(index)
{
    return document.getElementById(this._buttonPrefix + 'A' + index);
}

TabControl.prototype._GetBButton = function(index)
{
    return document.getElementById(this._buttonPrefix + 'B' + index);
}

TabControl.prototype._GetCButton = function(index)
{
    return document.getElementById(this._buttonPrefix + 'C' + index);
}

TabControl.prototype._UpdatePanelVisibility = function()
{
    for (var i = 0; i < this._contentPanelCount; i++)
    {
        var displayValue = this._IsHiddenPanel(i) ? 'none' : '';
        
        document.getElementById(this._separatorPrefix + i).style.display = displayValue;
        document.getElementById(this._buttonPrefix    + i).style.display = displayValue;
    }
}

TabControl.prototype._IsHiddenPanel = function(index)
{
    var indexValue = document.getElementById(this._hiddenButtonListFieldId).value;
    if (indexValue == '')
        return;
    var panelIndexes = indexValue.split(',');
    
    for (var i = 0; i < panelIndexes.length; i++)
    {
        if (panelIndexes[i] == index)
            return true;
    }
    return false;
}


/* Public methods */
TabControl.prototype.ShowHotSelection = function(index)
{
    this._GetAButton(index).className = this._hotButtonACssClass;
    this._GetBButton(index).className = this._hotButtonBCssClass;
    this._GetCButton(index).className = this._hotButtonCCssClass;
}

TabControl.prototype.HideHotSelection = function(index)
{
    var useSelection = index == document.getElementById(this._selectionHiddenFieldId).value;

    this._GetAButton(index).className = useSelection ? this._selButtonACssClass : this._buttonACssClass;
    this._GetBButton(index).className = useSelection ? this._selButtonBCssClass : this._buttonBCssClass;
    this._GetCButton(index).className = useSelection ? this._selButtonCCssClass : this._buttonCCssClass;
}

TabControl.prototype.OnClick = function(index)
{
    document.getElementById(this._selectionHiddenFieldId).value = index;
    for (var i = 0; i < this._contentPanelCount; i++)
    {
        var panel= document.getElementById(this._contentPanelPrefix + i);
        panel.style.display = i == index ? '' : 'none';

        if (i == index)
           {
              if(document.createEvent)
             {
               var evt = document.createEvent("Events"); 
               evt.initEvent("panelchanged", true, true); 
               panel.dispatchEvent(evt);  
              }
           }
           else
            {
                this._GetAButton(i).className = this._buttonACssClass;
                this._GetBButton(i).className = this._buttonBCssClass;
                this._GetCButton(i).className = this._buttonCCssClass;
            }
                

        

    }
    
    if (this.OnPanelClick)
        this.OnPanelClick(index);
        
    
}


TabControl.prototype.HidePanel = function(panelIndex)
{
    var hiddenFieldObj = document.getElementById(this._hiddenButtonListFieldId);
    var panelIndexes = hiddenFieldObj.value.split(',');
    hiddenFieldObj.value = '';

    for (var i = 0; i < panelIndexes.length; i++)
    {
        if (panelIndexes[i] == '')
            continue;
        if (panelIndex != panelIndexes[i])
            hiddenFieldObj.value += panelIndexes[i] + ','; 
    }
    hiddenFieldObj.value += panelIndex; 
    this._UpdatePanelVisibility();
}

TabControl.prototype.ShowPanel = function(panelIndex)
{
    var hiddenFieldObj = document.getElementById(this._hiddenButtonListFieldId);
    var panelIndexes = hiddenFieldObj.value.split(',');
    hiddenFieldObj.value = '';

    for (var i = 0; i < panelIndexes.length; i++)
    {
        if (panelIndexes[i] == '')
            continue;
        if (panelIndex == panelIndexes[i])
            continue;
        
        if (hiddenFieldObj.value == '')
            hiddenFieldObj.value += panelIndexes[i];
        else
            hiddenFieldObj.value += ',' + panelIndexes[i];
    }

    this._UpdatePanelVisibility();
}

TabControl.prototype.MoveToNextPanel = function()
{
    var curIndex = document.getElementById(this._selectionHiddenFieldId).value;
    if (curIndex == (this._contentPanelCount - 1))
        return;

    curIndex++;
    
    for (curIndex; curIndex < this._contentPanelCount; curIndex++)
    {
        if (!this._IsHiddenPanel(curIndex))
        {
            this.OnClick(curIndex);
            return;
        }
    }
}

TabControl.prototype.MoveToPrevPanel = function()
{
    var curIndex = document.getElementById(this._selectionHiddenFieldId).value;
    if (curIndex < 1)
        return;

    curIndex--;
    
    for (curIndex; curIndex > -1; curIndex--)
    {
        if (!this._IsHiddenPanel(curIndex))
        {
            this.OnClick(curIndex);
            return;
        }
    }
}