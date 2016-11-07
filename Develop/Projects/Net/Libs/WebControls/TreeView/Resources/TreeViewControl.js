/// <reference name="MicrosoftAjax.js"/>
/// <reference name="AjaxControlToolkit.js"/>
/// <reference name="TreeViewControl.js"/>
/// <reference name="TreeNode.js"/>

Type.registerNamespace("Odyssey.Web");

Odyssey.Web.TreeViewControl = function(element) {

    Odyssey.Web.TreeViewControl.initializeBase(this, [element]);
    this._cmkeyHandler = null;
    this._cmclickHandler = null;
    this._contextMenuHandler = null;
    this.dragStartHandler = null;
    this._clickHandler = null;
    this._dblClickHandler = null;
    this._keyHandler = null;
    this._hiddenFieldName = "";
    this._expandedNodes = new Array();
    this._multiSelect = false;
    this._autoPostBack = false;
    this._updating = true;
    this._allNodes = null;
    this._contextMenu = null;
    this._contextMenuVisible = false;
    this._selectedIndex = -1;
    this._bodyClickHandler = null;
    this._enableDragDrop = false;
    this._focusOutHandler = null;
    this._dblClickEnabled = false;
    this._allowNodeEditing = false;
    this._mouseMoveHandler = null;
    this._disableTextSelection = true;
    this._isOpera = false;
    this._editNodeIndex = -1;
    this._selectedNodePostBackOnly = false; //true, to update only the selected node for the hidden field.
    this._captureAllClicks = false;

}


Odyssey.Web.TreeViewControl.prototype = {
    initialize: function() {
        Odyssey.Web.TreeViewControl.callBaseMethod(this, 'initialize');
        this._clickHandler = Function.createDelegate(this, this._onClick);
        this._keyHandler = Function.createDelegate(this, this._dokeyDown);
        this._contextMenuHandler = Function.createDelegate(this, this._contextMenuEvent);
        this._focusOutHandler = Function.createDelegate(this, this._onFocusOut);
        this._updating = true;

        this._isOpera = Sys.Browser.agent == Sys.Browser.Opera;


        var e = this.get_element();

        var cm = $get("contextMenu", this.get_element());
        if (cm) {
            Sys.UI.DomElement.addCssClass(cm, "odcContextMenu");
            var s = cm.style;
            Sys.UI.DomElement.setVisibilityMode(cm, Sys.UI.VisibilityMode.collapse);
            Sys.UI.DomElement.setVisible(cm, false);
            s.position = "absolute";
            s.zIndex = 10000;

            this._cmclickHandler = Function.createDelegate(this, this._cmClickEvent);
            this._cmkeyHandler = Function.createDelegate(this, this._cmKeyEvent);


            $addHandler(cm, "mousedown", this._cmclickHandler);
            $addHandler(cm, "keydown", this._cmkeyHandler);

        }

        $addHandler(e, 'click', this._clickHandler);
        $addHandler(e, "keydown", this._keyHandler);


        if (this._isOpera) {
            $addHandler(e, "mousedown", this._contextMenuHandler);
        } else {
            $addHandler(e, "contextmenu", this._contextMenuHandler);
        }
        $addHandler(e, "focusout", this._focusOutHandler);

        if (this._disableTextSelection) {
            this._mouseMoveHandler = Function.createDelegate(this, this._onMouseMove);
            $addHandler(e, "mousemove", this._mouseMoveHandler);
        }

        if (this._dblClickEnabled) {
            this._dblClickHandler = Function.createDelegate(this, this._onDblClick);
            $addHandler(e, "dblclick", this._dblClickHandler);
        }

        if (this._enableDragDrop) {
            this._dragStartHandler = Function.createDelegate(this, this._onDragStart);
            $addHandler(e, "dragstart", this._dragStartHandler);
        }
    },

    dispose: function() {
        var cm = $get("contextMenu", this.get_element());
        if (cm) {
            $removeHandler(cm, "mousedown", this._cmclickHandler);
            $removeHandler(cm, "keydown", this._cmkeyHandler);
        }

        var e = this.get_element();
        if (this._mouseMoveHandler) $removeHandler(e, "mousemove", this._mouseMoveHandler);
        if (this._dblClickHandler) $removeHandler(e, "dblclick", this._dblClickHandler);
        $removeHandler(e, "focusout", this._focusOutHandler);
        $removeHandler(e, 'click', this._clickHandler);
        $removeHandler(e, "keydown", this._keyHandler);
        if (this._isOpera) {
            $removeHandler(e, "mousedown", this._contextMenuHandler);
        } else {
            $removeHandler(e, "contextmenu", this._contextMenuHandler);
        }
        if (this._dragStartHandler) $removeHandler(e, "dragstart", this._dragStartHandler);
        Odyssey.Web.TreeViewControl.callBaseMethod(this, 'dispose');
    },

    _onMouseMove: function(evt) {
        ///prevent selection:
        var dom = new Sys.UI.DomEvent(evt);
        if ((dom.target.tagName != "INPUT") || (dom.target.type != "text")) {
            dom.stopPropagation();
            dom.preventDefault();
        }
    },

    getPostBackId: function() {
        /// <summary>gets the id used for __doPostBack</summary>
        var id = this.get_element().id;
        id = id.replace(/_/g, '$');   // replaces all _ by $. this is necassary for master/content pages.
        return id;
    },


    _onDblClick: function(evt) {
        var dom = new Sys.UI.DomEvent(evt);
        var e = dom.target;
        var node = Odyssey.Web.NodeFromChildElement(e);
        if (node) {
            if (this.IsParentOf(e, node._getContent())) node.doDblClick();
        }
    },

    _endNodeEditMode: function() {
        var node = this.getSelectedNode();
        if (node) {
            node.setEditMode(false);
            //node._update();
        }
    },

    _onFocusOut: function(evt) {
        var dom = new Sys.UI.DomEvent(evt);
        var e = dom.target;
        if (e.id == "edit") {
            var node = Odyssey.Web.NodeFromChildElement(e);
            if (node) {
                node.setEditMode(false); // if allowNodeEditing
                node._update();  /// if not allowNodeEditing!
            }

        }
    },

    _onDragStart: function(evt) {
        var dom = new Sys.UI.DomEvent(evt);
        if (dom.button == Sys.UI.MouseButton.leftButton) {
            var e = dom.target;
            var node = Odyssey.Web.NodeFromChildElement(e);
            if (node) {
                var c = node._getContent();
            }
        }
    },

    IsParentOf: function(e, parent) {
        while (e && e.parentNode) {
            if (e == parent) return true;
            e = e.parentNode;
        }
        return false;
    },

    setSelectedNode: function(node) {
        this._selectedIndex = node.get_index();
        //    this.updateClientData();
    },

    _contextMenuEvent: function(evt) {
        var dom = new Sys.UI.DomEvent(evt);
        var e = dom.target;

        if (this._isOpera && (dom.button != Sys.UI.MouseButton.rightButton)) return;

        var node = Odyssey.Web.NodeFromChildElement(e);
        if (node && this.IsParentOf(e, node._getContent())) {
            dom.stopPropagation();
            dom.preventDefault();
            node.showContextMenu(dom);
            return;
        }
        this.hideContextMenu();
    },

    _bodyClickEvent: function(evt) {
        this.hideContextMenu();
    },

    _cmClickEvent: function(evt) {
        var ev = new Sys.UI.DomEvent(evt);
        ev.preventDefault();
        ev.stopPropagation();
    },

    _cmKeyEvent: function(evt) {
        var ev = new Sys.UI.DomEvent(evt);
        if (ev.keyCode == Sys.UI.Key.esc) this.hideContextMenu();
    },

    _updateHiddenFieldInfo: function() {
        var hf = this._hiddenFieldName;
        if (hf) {
            var sb = new Sys.StringBuilder();
            if (!this._autoPostBack && !this._selectedNodePostBackOnly) {
                // if a click event causes a postback, there is not need to save the state to the hidden field, except the selected node, 
                // since it may depend on a command:

                sb.append("exp:");
                var allnodes = this.allNodes();
                var max = allnodes.length;
                if (max > 0) {
                    var cnt = 0;
                    for (var i = 0; i < max; i++) {
                        var node = allnodes[i];
                        var exp = node.get_expanded();
                        if (exp != null) {
                            if (exp == true) {
                                if (cnt > 0) sb.append(",");
                                sb.append(node.get_index());
                                cnt = cnt + 1;
                            }
                        }
                    }
                    sb.append(";sel:");
                    cnt = 0;
                    for (var i = 0; i < max; i++) {
                        var node = allnodes[i];
                        var exp = node.get_selected();
                        if (exp != null) {
                            if (exp == true) {
                                if (cnt > 0) sb.append(",");
                                sb.append(node.get_index());
                                cnt = cnt + 1;
                            }
                        }
                    }
                    sb.append(";cb:");
                    cnt = 0;
                    for (var i = 0; i < max; i++) {
                        var node = allnodes[i];
                        var cb = node.get_checked();
                        if (cb == true) {
                            if (cnt > 0) sb.append(",");
                            sb.append(node.get_index());
                            cnt = cnt + 1;
                        }
                    }
                    sb.append(";txt:");
                    cnt = 0;
                    for (var i = 0; i < max; i++) {
                        var node = allnodes[i];
                        if ((!this._allowNodeEditing && node.isEditable()) || node.getModified()) {
                            if (cnt > 0) sb.append(",");
                            sb.append(node.get_index());
                            sb.append("=");
                            sb.append(node.getText());
                            cnt = cnt + 1;
                        }
                    }
                }
            }
            // the selected node is even necassary when autopostback is set to true, since event bubbling might occur before
            // a postback of the treeview:
            if (this._selectedIndex >= 0) {
                if (!sb.isEmpty()) {
                    sb.append(";");
                }
                sb.append("sn:");
                sb.append(this._selectedIndex);
            }

            var hidden = $get(hf);
            hidden.value = sb.toString();
        }
    },

    get_hiddenFieldName: function() {
        return this._hiddenFieldName;
    },

    set_hiddenFieldName: function(value) {
        this._hiddenFieldName = value;
    },



    _dokeyDown: function(evt) {
        var dom = new Sys.UI.DomEvent(evt);
        var e = dom.target;

        if (dom.keyCode == Sys.UI.Key.enter) this._endNodeEditMode();
        if (e.id != "edit") {
            if (!this._contextMenuVisible) {
                if (dom.ctrlKey || dom.altKey) {
                    if (dom.keyCode == Sys.UI.Key.down) {
                        dom.stopPropagation();
                        this._showContextMenu(null);
                        return;
                    }
                }
                switch (dom.keyCode) {
                    case 113: if (this._allowNodeEditing) {  //F2
                            var node = this.getSelectedNode();
                            if (node) {
                                dom.stopPropagation();
                                node.setEditMode(true);
                            }
                        }
                        break;
                    case Sys.UI.Key.down: this._down(e); dom.stopPropagation(); break;
                    case Sys.UI.Key.up: this._up(e); dom.stopPropagation(); break;
                    case Sys.UI.Key.left: this._toggleCurrent(e, false); dom.stopPropagation(); break;
                    case Sys.UI.Key.right: this._toggleCurrent(e, true); dom.stopPropagation(); break;
                    case Sys.UI.Key.space: if (this._toggleChecked()) dom.stopPropagation(); break;
                    case Sys.UI.Key.esc:
                        if (this._contextMenuVisible) { this.hideContextMenu(); dom.stopPropagation(); }
                        else {
                            var node = this.getSelectedNode();
                            dom.stopPropagation();
                            if (node && node.isEditable()) node.setEditMode(false);
                        }
                        break;
                }
            } else if (dom.keyCode == Sys.UI.Key.esc && this._contexMenuVisible) { this.hideContextMenu(); dom.stopPropagation(); }
        } else if (dom.keyCode == Sys.UI.Key.esc) this._endNodeEditMode();
    },

    _showContextMenu: function(dom) {
        var current = this.get_selectedNode();
        if (current) current.showContextMenu(dom);
    },

    _toggleChecked: function() {
        var current = this.get_selectedNode();
        if (current && current.hasCheckBox()) {
            current.toggleChecked();
            return true;
        } else return false;
    },

    _down: function(e) {
        this.select(1);
    },

    _up: function(e) {
        this.select(-1);
    },

    get_selectedNode: function() {
        var nodes = this.allNodes();
        var n = nodes.length;
        for (var i = 0; i < n; i++) {
            var node = nodes[i];
            if (node.get_selected()) return node;
        }
        return null;
    },

    _toggleCurrent: function(e, expand) {
        var current = this.get_selectedNode();
        if (current) current.set_expanded(expand);
    },

    select: function(offset) {
        var nodes = this.visibleNodes();
        var n = nodes.length;
        var index;
        for (var i = 0; i < n; i++) {
            var node = nodes[i];
            if (node.get_selected()) {
                index = i + offset;
                if (index >= 0 && index < n) {
                    node = nodes[index];
                    this.clearSelection();
                    node.set_selected(true);
                }
                break;
            }
        }
    },

    _onClick: function(evt) {
        var dom = new Sys.UI.DomEvent(evt);
        if (this._contextMenuVisible) {
            this.hideContextMenu();
            dom.stopPropagation();
        } else {
            var e = dom.target;
            if (dom.button != Sys.UI.MouseButton.leftButton) return;
            if (e && e.tagName) tagName = e.tagName;

            var type = e.getAttribute("event");

            if (!type) {
                // since the click was not directly caused by a registered element, it's important not to fire a postback, but update the hidden field if the element is of type input
                // since the click might be caused by a control to perform it's own postback, and thus the treeview must be aware of the assoicated node
                // that was the reason for the click when it causes a command:
                if (e.tagName != "INPUT") type = "click";
            }
            if (type) {
                var node = Odyssey.Web.NodeFromChildElement(e);
                if (node) {
                    var index = node.get_index();
                    var selected = this.getSelectedNode();
                    if (selected && selected.get_index() != index) {
                        if (this._allowNodeEditing) {
                            if (selected.setEditMode(false));
                        }
                    }
                    switch (type) {
                        case "check": node.submitChecked(); break;
                        case "collapse":
                        case "expand": node.toggleExpanded(); break;
                        case "click":
                            if (this._allowNodeEditing && node.get_selected()) {
                                node.setEditMode(true);
                                dom.stopPropagation();
                            } else {
                                if (this.get_multiSelect()) node.toggleSelected(); else node.set_selected(true);
                            }
                            break;
                        default: __doPostBack(this.getPostBackId(), type + ":" + index); break;
                    }
                    node.dispose();
                }
            } else {
                this.updateClientData();
            }
        }
    },

    ensureClearSelection: function() {
        if (!this.get_multiSelect()) {
            this.clearSelection();
        }
    },

    clearSelection: function() {
        var nodes = this.allNodes();
        var max = nodes.length;
        for (var i = 0; i < max; i++) nodes[i].set_selectedNoPb(false);
        this._selectedIndex = -1;
    },

    visibleNodes: function() {
        /// gets an array with all nodes and subnodes that are visible in the browser (not collapsed).

        var all = this.allNodes();
        var array = new Array();
        var n = all.length;
        for (var i = 0; i < n; i++) {
            var node = all[i];
            if (node.getVisible()) array.push(node);
        }
        return array;
    },

    allNodes: function() {
        /// gets an array with all nodes and subnodes.

        if (this._allNodes) return this._allNodes;

        var array = new Array();

        var nodes = this.get_element().getElementsByTagName("li");
        var max = nodes.length;
        for (var i = 0; i < max; i++) {
            var li = nodes[i];
            var index = li.getAttribute("key");
            if (index) {
                var node = new Odyssey.Web.TreeNode(li);
                array.push(node);
            }
        }
        this._allNodes = array;
        return array;
    },

    firstNode: function() {
        var e = this.get_element().firstChild.firstChild;
        if (Sys.Browser.agent != Sys.Browser.InternetExplorer) {
            e = this.get_element().firstChild.nextSibling.firstChild.nextSibling;
        }
        return new Odyssey.Web.TreeNode(e);
    },

    get_multiSelect: function() {
        return this._multiSelect;
    },
    set_multiSelect: function(value) {
        this._multiSelect = value;
    },

    get_autoPostBack: function() {
        return this._autoPostBack;
    },
    set_autoPostBack: function(value) {
        this._autoPostBack = value;
    },

    updateClientData: function() {
        if (this._updating) {
            this._updateHiddenFieldInfo();
        }
    },

    hideContextMenu: function() {
        if (this._contextMenuVisible == true) {
            $removeHandler(document.body, 'mousedown', this._bodyClickHandler);
            $removeHandler(document.body, 'contextmenu', this._bodyClickHandler);
            cm = $get("contextMenu", this.get_element());
            Sys.UI.DomElement.setVisible(cm, false);
            this._contextMenuVisible = false;
        }
    },

    showContextMenu: function(node, x, y) {
        this.hideContextMenu();
        var cm = $get("contextMenu", this.get_element());
        if (cm) {
            this._bodyClickHandler = Function.createDelegate(this, this._bodyClickEvent);
            $addHandler(document.body, 'mousedown', this._bodyClickHandler);
            $addHandler(document.body, 'contextmenu', this._bodyClickHandler);

            this._contextMenuVisible = true;
            var s = cm.style;
            s.left = x + "px";
            s.top = y + "px";

            var handler = this.get_events().getHandler("contextMenu");
            if (handler) {
                var e = new Odyssey.Web.TreeContextMenuEventArgs(node, cm);
                handler(this, e);
                if (e.get_cancel()) return;
            }

            Sys.UI.DomElement.setVisible(cm, true);

        }
    },

    getSelectedNode: function() {
        /// gets the TreeNode that is currently selected (via select or context menu opened), otherwise null.
        var idx = this._selectedIndex;
        var all = this.allNodes();
        var n = all.length;
        if (idx >= 0) {
            for (var i = 0; i < n; i++) {
                var node = all[i];
                if (node.get_index() == idx) return node;
            }
        }
        for (var i = 0; i < n; i++) {
            var node = all[i];
            if (node.get_selected()) return node;
        }
        return null;

    },

    get_enableDragDrop: function() {
        return this._enableDragDrop;
    },
    set_enableDragDrop: function(value) {
        this._enableDragDrop = value;
    },

    get_dblClickEnabled: function() {
        return this._dblClickEnabled;
    },
    set_dblClickEnabled: function(value) {
        this._dblClickEnabled = value;
    },

    get_allowNodeEditing: function() {
        return this._allowNodeEditing;
    },
    set_allowNodeEditing: function(value) {
        this._allowNodeEditing = value;
    },
    get_disableTextSelection: function() {
        return this._disableTextSelection;
    },
    set_disableTextSelection: function(value) {
        this._disableTextSelection = value;
    },

    get_editNodeIndex: function() {
        return this._editNodeIndex;
    },
    set_editNodeIndex: function(value) {
        this._editNodeIndex = value;
    },
    
    get_captureAllClicks: function() {
        return this._captureAllClicks;
    },
    set_captureAllClicks: function(value) {
        this._captureAllClicks = value;
    },    

    add_contextMenu: function(handler) {
        this.get_events().addHandler("contextMenu", handler);
    },
    remove_contextMenu: function(handler) {
        this.get_events().removeHandler("contextMenu", handler);
    },

    add_nodeSelectionChanged: function(handler) {
        this.get_events().addHandler("selectionChanged", handler);
    },
    remove_nodeSelectionChanged: function(handler) {
        this.get_events().removeHandler("selectionChanged", handler);
    },

    add_nodeExpanded: function(handler) {
        this.get_events().addHandler("nodeExpanded", handler);
    },
    remove_nodeExpanded: function(handler) {
        this.get_events().removeHandler("nodeExpanded", handler);
    },

    add_nodeCollapsed: function(handler) {
        this.get_events().addHandler("nodeCollapsed", handler);
    },
    remove_nodeCollapsed: function(handler) {
        this.get_events().removeHandler("nodeCollapsed", handler);
    },

    add_checkedChanged: function(handler) {
        this.get_events().addHandler("checkedChanged", handler);
    },
    remove_checkedChanged: function(handler) {
        this.get_events().removeHandler("checkedChanged", handler);
    },

    add_nodeTextChanged: function(handler) {
        this.get_events().addHandler("nodeTextChanged", handler);
    },
    remove_nodeTextChanged: function(handler) {
        this.get_events().removeHandler("nodeTextChanged", handler);
    },

    add_nodeEditModeChanged: function(handler) {
        this.get_events().addHandler("nodeEditModeChanged", handler);
    },
    remove_nodeEditModeChanged: function(handler) {
        this.get_events().removeHandler("nodeEditModeChanged", handler);
    }
}

Odyssey.Web.TreeViewControl.registerClass('Odyssey.Web.TreeViewControl', Sys.UI.Control);

// can cause exception in combination with nested update panels. :
// see http://www.codeplex.com/AjaxControlToolkit/WorkItem/View.aspx?WorkItemId=16582
//if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();