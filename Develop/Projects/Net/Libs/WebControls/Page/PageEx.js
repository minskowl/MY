//PageEx.js
//$.noConflict();
/*                     Browser Detection                        */
var ie = false;
var opera = false;
var firefox = false;
var isIExplorer = false;

var browserDetect = {
    init: function() {
        this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
        this.version = this.searchVersion(navigator.userAgent) || this.searchVersion(navigator.appVersion) || "an unknown version";
        this.OS = this.searchString(this.dataOS) || "an unknown OS";
        switch (this.browser) {
            case "Opera":
                opera = true;
                break;
            case "Firefox":
                firefox = true;
                break;
            case "Explorer":
                ie = true;
                isIExplorer = true;
                break;
            default:
                isIExplorer = true;
                break;
        }
    },
    searchString: function(data) {
        for (var i = 0; i < data.length; i++) {
            var dataString = data[i].string;
            var dataProp = data[i].prop;
            this.versionSearchString = data[i].versionSearch || data[i].identity;
            if (dataString) {
                if (dataString.indexOf(data[i].subString) != -1)
                    return data[i].identity;
            } else if (dataProp)
                return data[i].identity;
        }
    },
    searchVersion: function(dataString) {
        var index = dataString.indexOf(this.versionSearchString);
        if (index == -1) return;
        return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
    },
    dataBrowser: [
				 { string: navigator.userAgent,
				     subString: "OmniWeb",
				     versionSearch: "OmniWeb/",
				     identity: "OmniWeb"
				 },
				 {
				     string: navigator.vendor,
				     subString: "Apple",
				     identity: "Safari"
				 },
				 {
				     prop: window.opera,
				     identity: "Opera"
				 },
				 {
				     string: navigator.vendor,
				     subString: "iCab",
				     identity: "iCab"
				 },
				 {
				     string: navigator.vendor,
				     subString: "KDE",
				     identity: "Konqueror"
				 },
				 {
				     string: navigator.userAgent,
				     subString: "Firefox",
				     identity: "Firefox"
				 },
				 {
				     string: navigator.vendor,
				     subString: "Camino",
				     identity: "Camino"
				 },
				 {		 // for newer Netscapes (6+)
				     string: navigator.userAgent,
				     subString: "Netscape",
				     identity: "Netscape"
				 },
				 {
				     string: navigator.userAgent,
				     subString: "MSIE",
				     identity: "Explorer",
				     versionSearch: "MSIE"
				 },
				 {
				     string: navigator.userAgent,
				     subString: "Gecko",
				     identity: "Mozilla",
				     versionSearch: "rv"
				 },
				 {		 // for older Netscapes (4-)
				     string: navigator.userAgent,
				     subString: "Mozilla",
				     identity: "Netscape",
				     versionSearch: "Mozilla"
				 }
				 ],
    dataOS: [
			 {
			     string: navigator.platform,
			     subString: "Win",
			     identity: "Windows"
			 },
			 {
			     string: navigator.platform,
			     subString: "Mac",
			     identity: "Mac"
			 },
			 {
			     string: navigator.platform,
			     subString: "Linux",
			     identity: "Linux"
			 }
			 ]

};
browserDetect.init();

var _defaultButtonID = null;
function fireDefaultButton(e) {
    if (!_defaultButtonID) return true;
    return WebForm_FireDefaultButton(e, _defaultButtonID);
}

function disableSelection(target)
{
    if (isIExplorer) //IE route
        target.attachEvent("onselectstart", DisableEvent);
    else if (firefox) //Firefox route
        target.style.MozUserSelect = "none";
    else //All other route (ie: Opera)
        target.attachEvent(target, "onmousedown", DisableEvent);

     target.style.cursor = "default";
 }

function enableSelection(target)
 {
     if (isIExplorer) //IE route
         target.detachEvent("onselectstart", DisableEvent);
     else if (firefox) //Firefox route
         target.style.MozUserSelect = null;
     else //All other route (ie: Opera)
         target.detachEven(target, "onmousedown", DisableEvent);

     target.style.cursor = "auto";
}
 function DisableEvent() {  return false; }
/*               Cookie Managment Functions                                  */


// this function gets the cookie, if it exists
function Get_Cookie(name) {

    var start = document.cookie.indexOf(name + "=");
    var len = start + name.length + 1;
    if ((!start) && (name != document.cookie.substring(0, name.length))) {
        return null;
    }
    if (start == -1) return null;
    var end = document.cookie.indexOf(";", len);
    if (end == -1) end = document.cookie.length;
    return unescape(document.cookie.substring(len, end));
}

/*
only the first 2 parameters are required, the cookie name, the cookie
value. Cookie time is in milliseconds, so the below expires will make the 
number you pass in the Set_Cookie function call the number of days the cookie
lasts, if you want it to be hours or minutes, just get rid of 24 and 60.

Generally you don't need to worry about domain, path or secure for most applications
so unless you need that, leave those parameters blank in the function call.
*/
function Set_Cookie(name, value, expires, path, domain, secure) {
    // set time, it's in milliseconds
    var today = new Date();
    today.setTime(today.getTime());
    // if the expires variable is set, make the correct expires time, the
    // current script below will set it for x number of days, to make it
    // for hours, delete * 24, for minutes, delete * 60 * 24
    if (expires) {
        expires = expires * 1000 * 60 * 60 * 24;
    }
    //alert( 'today ' + today.toGMTString() );// this is for testing purpose only
    var expires_date = new Date(today.getTime() + (expires));
    //alert('expires ' + expires_date.toGMTString());// this is for testing purposes only
    var cookieString = name + "=" + escape(value) +
					 ((expires) ? ";expires=" + expires_date.toGMTString() : "") + //expires.toGMTString()
					 ((path) ? ";path=" + path : "") +
					 ((domain) ? ";domain=" + domain : "") +
					 ((secure) ? ";secure" : "");

    document.cookie = cookieString;
}

// this deletes the cookie when called
function Delete_Cookie(name, path, domain) {
    if (Get_Cookie(name)) document.cookie = name + "=" +
												((path) ? ";path=" + path : "") +
												((domain) ? ";domain=" + domain : "") +
												";expires=Thu, 01-Jan-1970 00:00:01 GMT";
}



/********************************   COMMON *******************************************/

function getScrollableAreas() {
    var result = $$('div[class="scrollable"]', 'div[class="pageScroll"]');
    result[result.length] = document;
    return result;
}


function AlphaRender() {
    var arVersion = navigator.appVersion.split("MSIE")
    var version = parseFloat(arVersion[1])

    if (!(version >= 5.5 && document.body.filters))
        return;

    for (var i = 0; i < document.images.length; i++) {
        var img = document.images[i]
        var imgName = img.src.toUpperCase()

        if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
            alphaFixPngImg(img);
            i = i - 1;
        }
    }

}

function alphaFixPngImg(img) {
    var arVersion = navigator.appVersion.split("MSIE")
    var version = parseFloat(arVersion[1])

    if (!(version >= 5.5 && document.body.filters))
        return;

    var imgID = (img.id) ? "id='" + img.id + "' " : ""
    var imgClass = (img.className) ? "class='" + img.className + "' " : ""
    var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' "
    var imgStyle = "display:inline-block;" + img.style.cssText
    if (img.align == "left") imgStyle = "float:left;" + imgStyle
    if (img.align == "right") imgStyle = "float:right;" + imgStyle
    if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle
    var strNewHTML = "<span " + imgID + imgClass + imgTitle
	+ " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
	+ "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
	+ "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>"
    img.outerHTML = strNewHTML
}

function findElements(elements, tagName, type, partId) {
    var result = new Array();
    for (var i = 0; i < elements.length; i++) {
        var obj = elements[i];

        if (tagName != null && obj.tagName.toLowerCase() != tagName.toLowerCase())
            continue;
        if (type != null && obj.type.toLowerCase() != type.toLowerCase())
            continue;
        if (partId != null && obj.id.indexOf(partId) == -1)
            continue;

        result[result.length] = obj;
    }
    return result;
}
/************************************ EVENTS ****************************************/
function addEvent(obj, eventName, fn, useCapture) {

    if (obj == null) return false;

    if (obj.addEventListener) {
        obj.addEventListener(eventName, fn, useCapture);
        return true;
    } else if (obj.attachEvent) {
        obj.attachEvent('on' + eventName, fn);
        return true;
    }
    return false;
}

function fireCustomEvent(fireOnThis, eventName) {
    if (document.createEvent) {
        var evt = document.createEvent("Events");
        evt.initEvent(eventName, true, false);
        fireOnThis.dispatchEvent(evt);
    } else if (document.createEventObject) {
        var e = document.createEventObject();
        e.srcElement = fireOnThis;
        eval(eventName + "Obj.fireEvent(eventName,e );");
    }
}

var EventHandlersArray = function() {
    this.handlers = new Array();
}
EventHandlersArray.prototype.AddHandler = function(handler) {
    this.handlers[this.handlers.length] = handler;
}
EventHandlersArray.prototype.Dispose = function(handler) {
    this.handlers = null;
}
EventHandlersArray.prototype.FireEvent = function(obj) {
    for (var i = 0; i < this.handlers.length; i++)
        if (this.handlers[i] && typeof (this.handlers[i]) == "function")
        this.handlers[i](obj);
}


/*************************  POSITION *********************************/

function getposOffset(what, offsettype) {
    var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
    var parentEl = what.offsetParent;
    while (parentEl != null) {
        totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return totaloffset;
}
// Return real top position	
function getTopPos(inputObj, parent) {
    var returnValue = inputObj.offsetTop;
    var parentEl = inputObj.offsetParent;
    while (parentEl != null && parent != parentEl) {
        if (parentEl.tagName != 'HTML')
            returnValue += parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return returnValue;
}

// Return real left position	
function getLeftPos(inputObj, parent) {
    var returnValue = inputObj.offsetLeft;
    var parentEl = inputObj.offsetParent;
    while (parentEl != null && parent != parentEl) {
        if (parentEl.tagName != 'HTML')
            returnValue += parentEl.offsetLeft;
        parentEl = parentEl.offsetParent;
    }
    return returnValue;
}

function eventInBound(e, obj) {
    var top = getTopPos(obj);
    var left = getLeftPos(obj);
    return (e.clientX >= left && e.clientX <= (left + obj.offsetWidth) &&
		   e.clientY >= top && e.clientY <= (top + obj.offsetHeight));
}

// Class Querystring
function Querystring(qs) {
    this.params = new Object()
    this.get = Querystring_get

    if (qs == null)
        qs = location.search.substring(1, location.search.length)

    if (qs.length == 0) return

    // Turn <plus> back to <space>
    // See: http://www.w3.org/TR/REC-html40/interact/forms.html#h-17.13.4.1
    qs = qs.replace(/\+/g, ' ')
    var args = qs.split('&') // parse out name/value pairs separated via &

    // split out each name=value pair
    for (var i = 0; i < args.length; i++) {
        var value;
        var pair = args[i].split('=')
        var name = unescape(pair[0])

        if (pair.length == 2)
            value = unescape(pair[1])
        else
            value = name

        this.params[name] = value
    }
}

function Querystring_get(key, default_) {
    // This silly looking line changes UNDEFINED to NULL
    if (default_ == null) default_ = null;

    var value = this.params[key]
    if (value == null) value = default_;

    return value
}


function InsertText(input, insertText) {
    if (input.createTextRange) {
        input.focus(input.caretPos);
        input.caretPos = document.selection.createRange().duplicate();
        if (input.caretPos.text.length > 0)
            input.caretPos.text = input.caretPos.text;
        else
            input.caretPos.text = insertText;
    }
    else {
        var start = input.selectionStart;
        var end = input.selectionEnd;
        input.value = input.value.substring(0, start) + insertText + input.value.substring(end, input.textLength);
        input.selectionStart = input.selectionEnd = start + insertText.length;
        input.focus();
    }
}


// Class WAMUser Settings

var Settings = function(domain) {
    this.keyCurrentURL = "CurrentURL";
    this.keyPanelSelected = "PanelSelected";
    this.keyPanelWidth = "PanelWidth";
    this.keyPanelHeight = "PanelHeight";
    this.keyPageScrollAreaHeight = "PageScrollAreaHeight";
    this.keyPageScrollAreaWidth = "PageScrollAreaWidth";
    this.keyFormScrollAreaHeight = "FormScrollAreaHeight";
    this.panelManagerID = "";
    this.pageLoadRequired = true;
    this.domain = (domain == null) ? "/" : domain;
}

// Public Function
Settings.prototype.GetPageScrollAreaHeight = function()
{ return Get_Cookie(this.keyPageScrollAreaHeight); }

Settings.prototype.GetPanelSelected = function()
{ return Get_Cookie(this.keyPanelSelected); }

Settings.prototype.GetPanelWidth = function()
{ return Get_Cookie(this.keyPanelWidth); }

Settings.prototype.GetPanelHeight = function()
{ return Get_Cookie(this.keyPanelHeight); }

Settings.prototype.GetCurrentURL = function()
{ return Get_Cookie(this.keyCurrentURL); }

Settings.prototype.SetCurrentURL = function(value)
{ return Set_Cookie(this.keyCurrentURL, value, 10, this.domain); }

Settings.prototype.SetFormScrollAreaHeight = function(value)
{ return Set_Cookie(this.keyFormScrollAreaHeight, value, 10, this.domain); }

Settings.prototype.SetPageScrollAreaHeight = function(value)
{ return Set_Cookie(this.keyPageScrollAreaHeight, value, 10, this.domain); }

Settings.prototype.SetPageScrollAreaWidth = function(value)
{ return Set_Cookie(this.keyPageScrollAreaWidth, value, 10, this.domain); }

Settings.prototype.SetPanelWidth = function(value)
{ return Set_Cookie(this.keyPanelWidth, value, 10, this.domain); }

Settings.prototype.SetPanelHeight = function(value)
{ return Set_Cookie(this.keyPanelHeight, value, 32, this.domain); }

/*                 END CLASS SETTINGS */

// String extenshion

Object.extend(String.prototype, {
    lastIndexOfDelimeter: function(delim) {
        var text = this;
        var pos = -1;
        for (var i = 0; i < delim.length; i++) {
            var n = text.lastIndexOf(delim[i]);
            if (n > pos) pos = n;
        }
        return pos;
    }
   ,
    IndexOfDelimeter: function(delim) {
        var pos = -1;
        var text = this;
        for (var i = 0; i < delim.length; i++) {
            var n = text.indexOf(delim[i]);
            if (n != -1 && (pos == -1 || pos > n))
                pos = n;
        }
        return pos;
    }
    ,
    trimEndChars: function(chars) {
        var res = this;
        if (Object.isArray(chars)) {
            while (chars.indexOf(res[res.length - 1]) != -1)
            { res = res.substr(0, res.length - 1); }
        }
        else {
            while (chars == res[res.length - 1])
            { res = res.substr(0, res.length - 1); }

        }
        return res;
    },

    trimStartChars: function(chars) {
        var res = this;
        if (Object.isArray(chars)) {
            while (chars.indexOf(res[0]) != -1)
            { res = res.substr(1, res.length - 1); }
        }
        else {
            while (chars == res[0])
            { res = res.substr(1, res.length - 1); }

        }
        return res;
    }
    ,
    trimChars: function(chars) {
        return this.trimEndChars(chars).trimStartChars(chars);
    }
    ,
    replaceSimple: function(searchText, replaceText) {
     return this.split(searchText).join(replaceText);
    }

});


function removeElement(id) {
    var elem = document.getElementById(id);
    while (elem != null) {
        if (isIExplorer)
            elem.removeNode(true);
        else
            elem.parentNode.removeChild(elem);
        elem = document.getElementById(id);
    }
}
function protectDiv(objDiv, id) {
    if (!isIExplorer)
        return;
    var ifr = document.createElement("iframe");
    ifr.id = id;
    ifr.style.position = "absolute";
    ifr.style.width = objDiv.offsetWidth;
    ifr.style.height = objDiv.offsetHeight;
    ifr.style.top = objDiv.style.top;
    ifr.style.left = objDiv.style.left;
    ifr.style.zIndex = objDiv.style.zIndex - 1;
    ifr.style.display = "block";
    objDiv.parentElement.appendChild(ifr);

}