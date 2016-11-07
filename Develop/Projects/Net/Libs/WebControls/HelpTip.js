// HelpTip.js

var horizontal_offset = "9px" //horizontal offset of hint box from anchor link

/////No further editting needed

var vertical_offset = "0" //horizontal offset of hint box from anchor link. No need to change.

var ns6 = document.getElementById && !document.all

function getposOffset(what, offsettype) {
    var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
    var parentEl = what.offsetParent;
    while (parentEl != null) {
        totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return totaloffset;
}

function iecompattest() {
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
}

function clearbrowseredge(obj, whichedge) {
    var edgeoffset = (whichedge == "rightedge") ? parseInt(horizontal_offset) * -1 : parseInt(vertical_offset) * -1
    if (whichedge == "rightedge") {
        var windowedge = isIExplorer && !window.opera ? iecompattest().scrollLeft + iecompattest().clientWidth - 30 :

window.pageXOffset + window.innerWidth - 40
        dropmenuobj.contentmeasure = dropmenuobj.offsetWidth
        if (windowedge - dropmenuobj.x < dropmenuobj.contentmeasure)
            edgeoffset = dropmenuobj.contentmeasure + obj.offsetWidth + parseInt(horizontal_offset)
    }
    else {
        var windowedge = isIExplorer && !window.opera ? iecompattest().scrollTop + iecompattest().clientHeight - 15 :

window.pageYOffset + window.innerHeight - 18
        dropmenuobj.contentmeasure = dropmenuobj.offsetHeight
        if (windowedge - dropmenuobj.y < dropmenuobj.contentmeasure)
            edgeoffset = dropmenuobj.contentmeasure - obj.offsetHeight
    }
    return edgeoffset
}

function showhint(menucontents, obj, e, tipwidth, timeoutmilliseconds) {
    if ($("hintbox") == null)
        return;

    var left = getposOffset(obj, "left");
    var top = getposOffset(obj, "top");
    var backgroundColor = "lightyellow";

    dropmenuobj = $("hintbox");
    dropmenuobj.innerHTML = menucontents;
    dropmenuobj.style.left = dropmenuobj.style.top = -500;
    if (tipwidth != "") {
        dropmenuobj.widthobj = dropmenuobj.style;
        dropmenuobj.widthobj.width = tipwidth;
    }
    dropmenuobj.x = left;
    dropmenuobj.y = top;
    dropmenuobj.style.left = left - clearbrowseredge(obj, "rightedge") + obj.offsetWidth + "px"
    dropmenuobj.style.top = top - clearbrowseredge(obj, "bottomedge") + "px"
    dropmenuobj.style.backgroundColor = backgroundColor;
    dropmenuobj.style.zIndex = 101;

    dropmenuobj.style.visibility = "visible";
    if (isIExplorer) {
        protectDiv(dropmenuobj, "ifrHint");
    }

    document.hintTimeoutMilliseconds = timeoutmilliseconds; // unellegant way of passing parameter to checkHideHint
    document.hintLastMouseOver = -1;
    obj.onmouseout = divMouseOut;

}

function checkHideHint() {
    d = new Date();
    if (document.hintLastMouseOver > 0 && d - document.hintLastMouseOver > document.hintTimeoutMilliseconds)
        hidetip(null);
}

function divMouseOver(e) {
    document.hintLastMouseOver = -1;
}

function divMouseOut(e) {
    d = new Date();
    document.hintLastMouseOver = d.getTime();
}

function hidetip(e) {
    dropmenuobj.style.visibility = "hidden"
    dropmenuobj.style.left = "-500px"

    var ifr = document.getElementById("ifrHint");
    if (ifr != null) {
        ifr.removeNode(true);
    }
}

function createhintbox() {
    var divblock = document.createElement("div")
    divblock.setAttribute("id", "hintbox")
    divblock.onmouseover = divMouseOver;
    divblock.onmouseout = divMouseOut;
    document.body.appendChild(divblock)
}
Event.observe(window, "load", createhintbox);
document.hintLastMouseOver = 0;
setInterval("checkHideHint()", 500);

