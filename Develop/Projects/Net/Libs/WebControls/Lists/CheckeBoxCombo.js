// CheckeBoxCombo.js

var FilterCombo = function (id, tableId, listBoxId, toolTip, afterClearJScript, clearJSFilter,
clearButtonCellId, labelID) {
    this.OnChangeFuncName = null;
    this.ID = id;
    this.Combo = $(this.ID);

    this.ToolTip = toolTip;
    this.TableId = tableId;
    this.Table = $(tableId);

    this.ListBoxId = listBoxId;
    this.List = $(this.ListBoxId);

    this.AfterClearJScript = afterClearJScript;
    this.ClearJSFilter = clearJSFilter;

    this.ClearButton = $(clearButtonCellId);
    this.Label = $(labelID);


    var mweel = "$('" + listBoxId + "').hide();";
    var areas = getScrollableAreas();
    for (var i = 0; i < areas.length; i++) {
        if (Prototype.Browser.IE)
            Event.observe(areas[i], 'mousewheel', function () { eval(mweel); removeElement("ifrFilterCombo"); });
        else
            Event.observe(areas[i], 'scroll', function () { eval(mweel); removeElement("ifrFilterCombo"); });
    }
}

FilterCombo.prototype.Toggle = function () {
    var l = this.List;

    if (l == null)
        return;
    if (l.visible()) {
        l.hide();
        removeElement("ifrFilterCombo");
    }
    else {
        var pos = this.getOffset();

        l.style.left = pos[0] + "px";
        l.style.top = (pos[1] + this.Table.offsetHeight) + "px";
        l.show();
        if (l.scrollWidth > l.offsetWidth) {
            if (l.scrollHeight > l.offsetHeight) {
                if (ie)
                    l.style.width = l.scrollWidth + 19;
                else
                    l.style.width = l.scrollWidth + 17;
            }
            else {
                if (ie)
                    l.style.width = l.scrollWidth + 10;
                else
                    l.style.width = l.scrollWidth;
            }
        }
        protectDiv(l, "ifrFilterCombo");
    }

}
FilterCombo.prototype.getOffset = function () {
    var res = new Array();
    var scrollPos = Position.realOffset(this.Table);
    var pos = Position.cumulativeOffset(this.Table);
    res[0] = pos[0] - scrollPos[0];
    res[1] = pos[1] - scrollPos[1];

    return res;
}
FilterCombo.prototype.InitControl = function () {
    this.UpdateTableTitle();

    var mdown = " FilterComboOnBlur( $('" + this.ID + "'),$('" + this.ListBoxId + "'),evt)";
    Event.observe(document, "mousedown", function (evt) { eval(mdown); });

    var mweel = "$('" + this.ListBoxId + "').hide();";
    var areas = getScrollableAreas();
    for (var i = 0; i < areas.length; i++) {
        if (Prototype.Browser.IE)
            Event.observe(areas[i], 'mousewheel', function () { eval(mweel); });
        else
            Event.observe(areas[i], 'scroll', function () { eval(mweel); });
    }


    window.setTimeout("FilterComboMoveList($('" + this.ListBoxId + "'));", 800);
}



function FilterComboOnBlur(combo, list, evt) {
    if (list == null || combo == null || !list.visible() || eventInBound(evt, list) || eventInBound(evt, combo))
        return true;

    list.hide();
    removeElement("ifrFilterCombo");
    return true;
}

FilterCombo.prototype.AddRow = function (text, value, checked) {
    var table = $(this.ListBoxId + "_Container");

    var index;
    var labels = table.getElementsByTagName("label");
    for (var index = 0; index < labels.length; index++) {
        if (labels[index].innerHTML.localeCompare(text) == 1)
            break;
    }

    var row = table.insertRow(index);
    var cell = row.insertCell(-1);

    var check = document.createElement("input");
    check.type = "checkbox";
    check.id = this.ListBoxId + "_" + value;
    check.name = check.id.replace(new RegExp("_", "g"), "$");
    check.setAttribute("obj", this.ID + "Obj");

    Event.observe(check, 'click', function (evt) { eval(Event.element(evt).getAttribute("obj") + ".CheckBoxClick(evt);"); });
    cell.appendChild(check);



    var label = document.createElement("label");
    label.htmlFor = check.id;
    label.innerHTML = text;
    cell.appendChild(label);

    if (checked != null && checked == true) {
        check.checked = true;
        check.checked = true;
        this.ClearButton.show();
        this.UpdateTableTitle();
    }

    if (this.OnChangeFuncName)
        this.OnChangeFuncName(this);
}


FilterCombo.prototype.GetSelectedIds = function () {
    var elements = this.GetAllCheckBoxes();
    var result = new Array()
    var index = 0;
    var id;
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].checked) {
            id = elements[i].id;
            result[index] = id.substr(id.lastIndexOf("_") + 1);
            index++;
        }
    }
    return result;
}

FilterCombo.prototype.GetAllCheckBoxes = function () {
    var elements = this.List.getElementsByTagName("input");
    elements = findElements(elements, "input", "checkbox", this.ListBoxId);
    return elements;
}

FilterCombo.prototype.HasChecked = function () {
    var elements = this.GetAllCheckBoxes();
    for (var i = 0; i < elements.length; i++)
        if (elements[i].checked)
            return true;

    return false;
}

FilterCombo.prototype.ClearFilter = function (button) {
    var check = (this.ClearJSFilter != null && this.ClearJSFilter.length != 0);
    var elements = this.GetAllCheckBoxes();

    for (var i = 0; i < elements.length; i++) {
        if (check == false || eval(this.ClearJSFilter + "('" + elements[i].id + "');"))
            elements[i].checked = false;
    }

    if (this.HasChecked() == false)
        button.hide();

    this.UpdateTableTitle();

    if (this.AfterClearJScript != null && this.AfterClearJScript.length != 0) {
        eval(this.AfterClearJScript);
    }
}

FilterCombo.prototype.CheckBoxClick = function (evt) {
    this.UpdateTableTitle();

    var checkBox = Event.element(evt); ;

    if (checkBox.checked || this.HasChecked())
        this.ClearButton.show();
    else
        this.ClearButton.hide();
}



FilterCombo.prototype.UpdateTableTitle = function () {
    var table = document.getElementById(this.TableId);
    if (table == null) return;

    var text = this.GetCheckedLabelNames();
    if (this.Label) this.Label.innerHTML = text;
    table.title = this.ToolTip + text;

    if (this.OnChangeFuncName)
        this.OnChangeFuncName(this);
}

FilterCombo.prototype.GetCheckedLabelNames = function () {
    var tooltip = "";
    var elements = this.GetAllCheckBoxes();
    for (var i = 0; i < elements.length; i++) {
        var checkBox = elements[i];
        if (checkBox.checked) {
            tooltip += checkBox.nextSibling.innerHTML + ", ";
        }
    }

    if (tooltip.length > 0)
        tooltip = " " + tooltip.substr(0, tooltip.length - 2);
    return tooltip;
}

function FilterComboMoveList(list) {
    if (list == null)
        return;

    if (list.parentNode != null)
        list.parentNode.removeChild(list);
    else
        list.parentElement.removeChild(list);
    document.forms[0].appendChild(list);
}

