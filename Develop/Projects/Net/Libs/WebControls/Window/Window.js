//file Window.js
//////////////////////////        WindowEngine     \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

var WindowEngine = function() {
    this.moveDelay = 1;
    this.timeOut = 5;

    this.activeWindow = false;
    this.activeWindowContent = false;
    this.activeWindowIframe = false;

    this.divCounter = 0;
    this.moveCounter = -1;
    this.initResizeCounter = -1;

    this.currentZIndex = 900;

    this.startWindowSize = new Array();
    this.startEventPos = new Array();
    this.startPosWindow = new Array();

    // Mininum width and height of windows.
    this.windowMinSize = [80, 30];

    Event.observe(window, 'load', fwInitEngine);
}

WindowEngine.prototype.setWindow = function(window) {
    this.activeWindow = window;
    this.activeWindowContent = $("windowContent_" + window.id);

    this.currentZIndex = this.currentZIndex / 1 + 1;

    window.style.zIndex = this.currentZIndex;
}

WindowEngine.prototype.switchElement = function(e, inputElement) {
    if (!inputElement)
        inputElement = this;

    if (inputElement.className == "dhtmlgoodies_window")
        weInstance.setWindow(inputElement);
    else
        debug(inputElement.id);
}



WindowEngine.prototype.CancelEvent = function() {
    return (this.moveCounter == -1 && this.initResizeCounter == -1);
}

WindowEngine.prototype.initWindow = function(divObj) {

    if (!divObj) {
        var divs = document.getElementsByTagName('DIV');
        for (var no = 0; no < divs.length; no++) {
            if (divs[no].className)
                initWindows(divObj);
        }
        return;
    }


    divObj.style.zIndex = this.currentZIndex;
    this.currentZIndex = this.currentZIndex / 1 + 1;

    this.divCounter = this.divCounter + 1;

    if (this.divCounter == 1)
        this.activeWindow = divObj;
    divObj.onmousedown = weInstance.switchElement;

    return this.divCounter;
}

/*                        RESIZE WINDOW                                   */

WindowEngine.prototype.startResize = function() {
    if (this.initResizeCounter >= 0 && this.initResizeCounter <= this.moveDelay) {
        this.initResizeCounter++;
        setTimeout('weInstance.startResize()', this.timeOut);
    }
}

WindowEngine.prototype.initResize = function(evt, window) {

    this.setWindow(window);

    disableSelection(document.body);

    this.initResizeCounter = 0;

    this.startWindowSize = [this.activeWindowContent.offsetWidth, this.activeWindowContent.offsetHeight];
    this.startEventPos = [evt.clientX, evt.clientY];

    if (isIExplorer)
        this.activeWindowIframe = this.activeWindow.getElementsByTagName('IFRAME')[0];

    this.startResize();

}

/*                        MOVE WINDOW                                   */

WindowEngine.prototype.InitMove = function(evt, window) {

    if (this.moveCounter > 0) return;

    disableSelection(document.body);

    this.setWindow(window);

    this.moveCounter = 1;
    this.startEventPos = [evt.clientX, evt.clientY];
    this.startPosWindow = [window.offsetLeft, window.offsetTop];
    this.startMove();
}

WindowEngine.prototype.StopMove = function() {

    enableSelection(document.body);
    this.moveCounter = -1;
    this.initResizeCounter = -1;
}

WindowEngine.prototype.MoveWindow = function(e) {


    if (this.moveCounter >= this.moveDelay) {
        this.activeWindow.style.left = this.startPosWindow[0] + e.clientX - this.startEventPos[0] + 'px';
        this.activeWindow.style.top = this.startPosWindow[1] + e.clientY - this.startEventPos[1] + 'px';
    }

    if (this.initResizeCounter >= this.moveDelay) {
        var newWidth = Math.max(this.windowMinSize[0], this.startWindowSize[0] + e.clientX - this.startEventPos[0]);
        var newHeight = Math.max(this.windowMinSize[1], this.startWindowSize[1] + e.clientY - this.startEventPos[1]);
        this.activeWindow.style.width = newWidth + 'px';
        this.activeWindowContent.style.height = newHeight + 'px';

        if (isIExplorer && this.activeWindowIframe) {
            this.activeWindowIframe.style.width = (newWidth) + 'px';
            this.activeWindowIframe.style.height = (newHeight + 20) + 'px';
        }
    }

    if (!isIExplorer) return false;
}

WindowEngine.prototype.startMove = function() {


    if (this.moveCounter >= 0 && this.moveCounter <= this.moveDelay) {
        this.moveCounter++;
        setTimeout('weInstance.startMove()', this.timeOut);
    }
}


function fwInitEngine() {
    document.observe("mouseup", function() { weInstance.StopMove(); });
    document.observe("mousemove", function(e) { weInstance.MoveWindow(e); });

    Event.stopObserving(window, "load", fwInitEngine);
}

var weInstance = new WindowEngine();

//////////////////////////                     Window            \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

var Window = function(id) {
    this.ID = id;
    this.objName = id + "Obj";
    this.control = $(id);
    this.buttonMinimize = $("buttonMinimize_" + id);
    this.imgResize = $("imgResize_" + id);
    this.windowContent = $("windowContent_" + id);

    weInstance.initWindow(this.control);
}

Window.prototype.Hide = function() {
    weInstance.setWindow(this.control);
    this.control.style.display = 'none';
}
Window.prototype.Show = function() {
    weInstance.setWindow(this.control);
    this.control.style.display = '';
}
Window.prototype.Minimize = function(evt) {
    weInstance.setWindow(this.control);

    if (this.buttonMinimize.src.indexOf('minimize') >= 0) {
        this.windowContent.style.display = 'none';
        if (this.imgResize != null) this.imgResize.style.display = 'none';
        this.buttonMinimize.src = this.buttonMinimize.src.replace('minimize', 'maximize');
    }
    else {
        this.windowContent.style.display = 'block';
        if (this.imgResize != null) this.imgResize.style.display = '';
        this.buttonMinimize.src = this.buttonMinimize.src.replace('maximize', 'minimize');
    }

}

Window.prototype.initResize = function(evt) {
    weInstance.initResize(evt, this.control);

    return false;
}


Window.prototype.initMove = function(evt) {
    weInstance.InitMove(evt, this.control);
    if (!isIExplorer) return false;
}
