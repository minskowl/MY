/// <reference name="MicrosoftAjax.js"/>
/// <reference name="AjaxControlToolkit.js"/>
/// <reference name="TreeViewControl.js"/>

Type.registerNamespace("Odyssey.Web");

Odyssey.Web.NodeFromChildElement = function(embedded) {
    var parent = embedded;
    while (parent && parent.tagName) {
        var index = parent.getAttribute("key");
        if (index) {
            return new Odyssey.Web.TreeNode(parent);
        }
        parent = parent.parentNode;
    }
    return null;
}

Odyssey.Web.TreeContextMenuEventArgs = function(node, menuElement) {
    Odyssey.Web.TreeContextMenuEventArgs.initializeBase(this);
    this.node = node;
    this.menuElement = menuElement;
}

Odyssey.Web.TreeContextMenuEventArgs.prototype =
{
}

Odyssey.Web.TreeContextMenuEventArgs.registerClass("Odyssey.Web.TreeContextMenuEventArgs", Sys.CancelEventArgs);

Odyssey.Web.TreeNode = function(element) {
    this._index = parseInt(element.getAttribute("key"));
    this._expanded = null;
    this._node = element;
    this._treeView = null;
    this._cbElement = null;

};



Odyssey.Web.TreeNode.prototype = {
    initialize: function() {
    },
    dispose: function() {
    },

    get_element: function() {
        return this._node;
    },

    get_index: function() {
        /// <summary> gets the index of this node given from the server.</summary>
        return this._index;
    },

    get_childNodes: function() {
        return this._node.firstChild.nextSibling;
    },

    get_toggleElement: function() {
        var e = this._node.firstChild.firstChild;
        while (!e.className && e.className != "Collapse" && e.className != "Expand") e = e.nextSibling;
        return e;
    },

    get_populate: function() {
        return this._node.getAttribute("populate") == "true";
    },

    _raiseNodeEvent: function(name, param) {
        var tv = this.getTreeView();
        var handler = tv.get_events().getHandler(name);
        if (handler) {
            var e = Sys.EventArgs.Empty;
            handler(this, e);
        }
    },

    get_expanded: function() {
        //if (this._expanded) return this._expanded;
        var toggle = this.get_toggleElement();
        if (toggle) {
            var event = toggle.getAttribute("event");
            switch (event) {
                case "collapse": this._expanded = true; break;
                case "expand": this._expanded = false; break;
                default: this._expanded = null;
            }
            return this._expanded;
        } else return false;
    },

    set_expanded: function(value) {
        if (this.get_expanded() != value) {
            if (this.get_populate() || this.getTreeView().get_autoPostBack()) {
                var newType = !value ? "collapse" : "expand";
                var id = this.getTreeView().getPostBackId();
                __doPostBack(id, newType + ":" + this.get_index())
            } else {
                var newType = value ? "collapse" : "expand";
                this._expanded = value;
                var nodes = this.get_childNodes();
                if (nodes) {
                    Sys.UI.DomElement.setVisible(nodes, value);

                    var cssStyle = value ? "Collapse" : "Expand";
                    var e = this.get_toggleElement();
                    e.setAttribute("event", newType);
                    //Sys.UI.DomElement.toggleCssClass(e, cssStyle);  // note: this does not work properly!
                    e.className = cssStyle;
                    this._update();
                    this._raiseNodeEvent(value ? "nodeExpanded" : "nodeCollapsed", null);
                }
            }
        }
    },

    toggleExpanded: function() {
        /// <summary>toggles the expanded state of the node.</summary>

        var expanded = !this.get_expanded();
        this.set_expanded(expanded);
    },

    _getContent: function() {
        var c = this._node.firstChild.childNodes;
        var max = c.length;
        for (var i = 0; i < max; i++) {
            var e = c[i];
            if (e.className && (Sys.UI.DomElement.containsCssClass(e, "span") || Sys.UI.DomElement.containsCssClass(e, "spanedit"))) return e;
        }
        return null;
    },

    get_selected: function() {
        var e = this._getContent();

        return Sys.UI.DomElement.containsCssClass(e, "Sel");
    },

    set_selected: function(value) {
        var tv = this.getTreeView();
        var match = this.get_selected() != value;
        if (match == true) {
            if (value) tv.ensureClearSelection();
            this._set_selected(value);

            // note that _update is also necassary when AutoUpdate is set to true, since the server must be informed about the selected node
            // on event bubbling:
            if (value) tv.setSelectedNode(this);
            this._update();
        }
        if ((match == true) || (tv.get_captureAllClicks())) {
            if (tv.get_autoPostBack()) {
                __doPostBack(tv.getPostBackId(), "click:" + this.get_index());
            }
            this._raiseNodeEvent("selectionChanged", null);
        }
    },

    _set_selected: function(value) {
        // internally, do NOT USE!!
        if (this.get_selected() != value) {
            var e = this._getContent();
            if (value == true) {
                Sys.UI.DomElement.addCssClass(e, "Sel");
            } else {
                Sys.UI.DomElement.removeCssClass(e, "Sel");
            }
        }
    },

    set_selectedNoPb: function(value) {
        var tv = this.getTreeView();
        if (this.get_selected() != value) {
            if (value) tv.ensureClearSelection();
            this._set_selected(value);
            if (value) tv.setSelectedNode(this);
            this._update();
        }
    },

    toggleSelected: function() {
        var selected = !this.get_selected();
        this.set_selected(selected);
    },

    doDblClick: function() {
        var tv = this.getTreeView();
        __doPostBack(tv.getPostBackId(), "dblclk:" + this.get_index())
    },

    _getTextElement: function() {
        var c = this._getContent();
        var all = c.childNodes;
        var max = all.length;
        for (var i = 0; i < max; i++) {
            var e = all[i];
            if ((e.isText) && (e.isText == "true")) {
                //if (e.value) return e.value;
                return e.lastChild;
            }
        }
        return null;
    },

    getText: function() {
        /// <summary>gets the text of the node</summary>
        /// <returns>the text of the node</summary>
        var e = $get("edit");
        if (e) return e.value;
        var c = this._getContent();
        var t = this._getTextElement();
        if (!t) t = c.lastChild;
        if (t && t.nodeValue) return t.nodeValue; else return "";
    },

    setText: function(value) {
        /// <summary>sets the text of the node</summary>
        var e = $get("edit");
        if (e) {
            e.value = value;
        } else {
            var c = this._getContent();
            var t = this._getTextElement();
            if (!t) t = c.lastChild;
            if (t && t.nodeValue) return t.nodeValue = value;
            this._raiseNodeEvent("nodeTextChanged", null);
        }
    },

    setEditMode: function(mode) {
        if (!this.getTreeView().get_allowNodeEditing()) return;
        if (mode != this.isEditable()) {
            var tv = this.getTreeView();
            var c = this._getContent();
            if (mode) {
                var t = this._getTextElement();
                if (!t) t = c.lastChild;
                Sys.Debug.trace(t);
                Sys.Debug.traceDump(c);
                var edit = document.createElement("input");
                edit.id = "edit";
                edit.className = "edit";
                edit.type = "text";
                this._focusOutHandler = Function.createDelegate(this, this._onFocusOut);
                $addHandler(edit, "blur", this._focusOutHandler);
                edit.value = this.getText();
                if (t.nodeValue) t.parentNode.replaceChild(edit, t); else t.parentNode.appendChild(edit);
                Sys.UI.DomElement.removeCssClass(c, "span");
                Sys.UI.DomElement.addCssClass(c, "spanedit");
                //c.className = "spanedit";
                edit.focus();
                edit.select();
            } else {
                var e = $get("edit", c);
                if (e) {
                    $clearHandlers(e);
                    var value = e.value;
                    var v = document.createTextNode(e.value);
                    e.value = "";
                    e.parentNode.replaceChild(v, e);
                    this._node.setAttribute("text", v);
                    Sys.UI.DomElement.removeCssClass(c, "spanedit");
                    Sys.UI.DomElement.addCssClass(c, "span");
                    Sys.UI.DomElement.addCssClass(c, "Sel");

                    //                    c.className = "span Sel";
                    this.setModified();
                    this._update();

                    this._raiseNodeEvent("nodeTextChanged", null);
                }
                this._raiseNodeEvent("nodeEditModeChanged", null);
            }
        }
        return false;
    },

    _onFocusOut: function(evt) {
        this.setEditMode(false);
    },

    _get_cbElement: function() {
        /// <summary>gets the checkbox element if available.</summary>
        if (!this._cbElement) {
            var div = this._node.firstChild;
            if (Sys.Browser.agent != Sys.Browser.InternetExplorer) {
                var all = this._node.getElementsByTagName("div");
                div = all[0];
            }
            var inputs = div.getElementsByTagName("input");
            var n = inputs.length;
            for (var i = 0; i < n; i++) {
                var inp = inputs[i];
                var event = inp.getAttribute("event");
                if (event && event == "check") {
                    this._cbElement = inp;
                    break;
                }
            }
        }
        return this._cbElement;
    },

    get_checked: function() {
        var cb = this._get_cbElement();
        if (cb) {
            var c = cb.checked;
            if (c == "checked") return true;
            if (c == null) return false;
            if (c == true) return true;
            if (c == false) return false;
            return false;
        }
        return false;
    },

    set_checked: function(value) {
        if (this.get_checked() != value) {
            if (this.getTreeView().get_autoPostBack()) {
                __doPostBack(this.getTreeView().getPostBackId(), "check:" + this.get_index() + ":" + value);
            } else {
                var cb = this._get_cbElement();
                if (cb) {
                    cb.setAttribute("checked", value ? "checked" : null);
                    this._update();
                    this._raiseNodeEvent("checkedChanged", null);
                }
            }
        }
    },

    hasCheckBox: function() {
        var cb = this._get_cbElement();
        if (cb) return true; else return false;
    },

    toggleChecked: function() {
        if (this.hasCheckBox()) {
            this.set_checked(!this.get_checked());
        }
    },

    submitChecked: function() {
        /// <summary>required to possibly submit the check change back to the server.</summary>

        this.set_checked(this.get_checked());
        this._update();
    },

    _update: function() {
        /// <summary>notify the treeview that properties where changed to update the hidden field for client/server interchange.</summary>
        var tv = this.getTreeView();
        if (tv) {
            tv.updateClientData();
        }
    },

    getNextNode: function() {
        // gets the next node in the same level, otherwhise null.
        var next = this._node.nextSibling;
        if (next) {
            return new Odyssey.Web.TreeNode(next);
        } else return next;
    },

    getPreviousNode: function() {
        // gets the previous node in the same level, otherwhise null.
        var prev = this._node.previousSibling;
        if (prev) {
            return new Odyssey.Web.TreeNode(prev);
        } else return prev;
    },

    getVisible: function() {
        var e = this._node;
        while (e) {
            if (Sys.UI.DomElement.getVisible(e) == false) return false;
            e = e.parentNode;
        }
        return true;
    },

    isFirst: function() {
        // gets whehter this node is the first in a level.
        var prev = this.getPreviousNode();
        return prev == null;
    },

    isLast: function() {
        // gets whehter this node is the last in a level.
        var next = this.getNextNode();
        return next == null;
    },


    getFirstChildNode: function() {
        // gets the first child node otherwise null.
        var nodes = this.get_childNodes();
        if (nodes) {
            var e = nodes.firstChild;
            if (Sys.Browser.agent != Sys.Browser.InternetExplorer) e = e.nextSibling;
            return new Odyssey.Web.TreeNode(e);
        } else return nodes;
    },

    getTreeView: function() {
        // gets the treeview to which this node belongs to.
        if (this._treeView) return this._treeView;
        var e = this._node;
        while (e && !e.id) e = e.parentNode;
        var c = $find(e.id);
        this._treeView = c;
        return c;
    },

    showContextMenu: function(dom) {
        /// dom: SYS.UI.DomEvent
        var x, y;

        var c = this._getContent();
        var b = Sys.UI.DomElement.getBounds(c);
        if (!dom || !dom.clientX || !dom.clientY) {
            x = b.x;
            y = b.y + b.height;
        } else {
            x = dom.offsetX + b.x;
            y = dom.offsetY + b.y;
        }
        var tv = this.getTreeView();
        tv.setSelectedNode(this);
        tv.clearSelection();
        this.set_selectedNoPb(true);
        var tv = this.getTreeView();
        tv.showContextMenu(this, x, y);
    },

    setModified: function() {
        // internally used.
        var e = this._node;
        e.setAttribute("modified", "true");
        this._update();
    },
    getModified: function() {
        // internally used.
        var e = this._node;
        var a = e.getAttribute("modified");
        return a == "true";
    },

    isEditable: function() {
        var e = $get("edit", this._node);
        return e != null;
    }
}

Odyssey.Web.TreeNode.registerClass('Odyssey.Web.TreeNode');

// can cause exception in combination with nested update panels. :
// see http://www.codeplex.com/AjaxControlToolkit/WorkItem/View.aspx?WorkItemId=16582
//if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();  
