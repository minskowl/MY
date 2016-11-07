//KeywordListControl.js

var KeywordListControl = function(obj, id, items, imagePath, comboId) {

    this.ObjectName = obj;
    this.ID = id;
    this.Items = items.evalJSON();
    this.Control = $(this.ID);
    this.ImagePath = imagePath;
    this.ComboId = comboId;
    this.NewID = 1;
    var f = this.ObjectName + ".Init();";
    Event.observe(window, 'load', function() { eval(f); });

}

KeywordListControl.prototype.Init = function() {

    for (var i = 0; i < this.Items.length; i++) {
        var item = this.Items[i];
        this.CreateItem(item.Name, item.Value);
    }
}
KeywordListControl.prototype.CreateItem = function(text, value) {
    value = parseInt(value);
    if (isNaN(value)) {
        value = -this.NewID;
        this.NewID = this.NewID + 1;
    }
    var id = this.ID + "_item" + value;
    if ($(id)) {
        alert("Keyword '" + text + "' already added.");
        return;
    }
    var d = document.createElement("SPAN");
    d.id = id;
    d.name = d.id.replace(/_/g, '$');
    var label = document.createElement("SPAN");
    label.innerHTML = text;

    var field = document.createElement("INPUT");
    field.type = "hidden";
    field.id = d.id + "_field";
    field.name = d.name + "$field";

    field.value = (value > 0) ? value : text;

    //var h = "<a href=\"javascript: " + this.ObjectName + ".RemoveItem('" + d.id + "');\" title=\"Remove keyword\"><img src=\"" + this.ImagePath + "buttonClear.gif\"/></a>";
    var button = document.createElement("A");
    button.href = "javascript: " + this.ObjectName + ".RemoveItem('" + d.id + "');";
    button.title = "Remove keyword";

    var img = document.createElement("IMG");
    img.src = this.ImagePath + "buttonClear.gif";
    img.border = 0;
    button.appendChild(img);

    d.appendChild(label);
    d.appendChild(field);
    d.appendChild(button);
    this.Control.appendChild(d);
}

KeywordListControl.prototype.RemoveItem = function(id) {
    $(id).remove();
}

KeywordListControl.prototype.AddItem = function() {

    var combo = $find(this.ComboId);
    this.CreateItem(combo.get_text(), combo.get_value());
}