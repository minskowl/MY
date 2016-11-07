// JScript File DropPanelButton.js

var DropPanelButton = function (id, panelId, buttonId) {
    this.ID = id;
    this.objId = id + "Obj";
    this.PanelId = panelId;
    this.ButtonId = buttonId;
    this.button = $(buttonId);
    this.panel = $(panelId);

    Event.observe(document, "mousedown", DropPanelButtonHidePanel);


    var mweel = "DropPanelButtonHide('" + panelId + "');";
    var areas = getScrollableAreas();
    for (var i = 0; i < areas.length; i++) {
        if (Prototype.Browser.IE)
            Event.observe(areas[i], 'mousewheel', function () { eval(mweel); });
        else
            Event.observe(areas[i], 'scroll', function () { eval(mweel); });
    }

}



DropPanelButton.prototype.Show = function () {
    if (this.panel.visible()) return;

    var pos = this.getOffset();

    this.panel.style.left = pos[0];
    this.panel.style.top = pos[1] + this.button.offsetHeight; ;

    this.panel.show();
    if (Prototype.Browser.IE) protectDiv(this.panel, "showDropPanelIfr");


}

DropPanelButton.prototype.getOffset = function () {
    var res = new Array();
    if (Prototype.Browser.IE) {
        res[0] = getLeftPos(this.button, this.panel.offsetParent);
        res[1] = getTopPos(this.button, this.panel.offsetParent);
    }
    else {
        var scrollPos = Position.realOffset(this.button);

        res[0] = getLeftPos(this.button) - scrollPos[0];
        res[1] = getTopPos(this.button) - scrollPos[1];
    }
    return res;
}
DropPanelButton.prototype.Hide = function () {
    if (!this.panel.visible()) return;

    this.panel.hide();
    if (Prototype.Browser.IE) removeElement("showDropPanelIfr");
}


function DropPanelButtonHide(id) {
    $(id).hide();
    if(Prototype.Browser.IE) removeElement("showDropPanelIfr");
}
DropPanelButtonHidePanel = function (e) {
    if (Prototype.Browser.IE) e = event;

    var elements = $$('div[single="true"]');
    for (var i = 0; i < elements.length; i++) {
        var el = elements[i]
        if (el.visible() && !DropPanelButtonHideEventInBound(e, el)) {
            var button = $(el.getAttribute("buttonId"));
            if (button != null && !DropPanelButtonHideEventInBound(e, button)) {
                el.hide();
                if (isIE) removeElement("showDropPanelIfr");
            }

        }
    }

}


DropPanelButtonHideEventInBound = function (e, obj) {
    var top = getTopPos(obj);
    var left = getLeftPos(obj);
    if (isIE) {
        var scrollPos = Position.realOffset(obj);

        left = left - scrollPos[0];
        top = top - scrollPos[1];
    }
    return (e.clientX >= left && e.clientX <= (left + obj.offsetWidth) &&
		   e.clientY >= top && e.clientY <= (top + obj.offsetHeight));
}