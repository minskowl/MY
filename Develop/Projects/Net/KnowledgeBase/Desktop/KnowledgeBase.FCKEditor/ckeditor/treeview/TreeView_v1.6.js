/*
 * TreeView v1.5
 * Developer : Navnath R. Kale
 * Last Updated : 28, March 2010
*/

function TreeView()
{
    this.treeView = {
        _showLines          : false,
        _className          : 'treeview',
        _lineStyle          : 'default',
        _container          : null,
        _visible            : true,
        
        Type                : 'TREEVIEW',
        PathSeparator       : '\\',
        UpdateImageURL      : 'treeview/images/updating.gif',
        ToggleOnSelect      : false,
        SelectedNode        : null,
        EnableKeyNavigation : true,
        EditLabel           : false,
        Nodes               : [],

        _init: function(){
            this.Nodes._control = null;                       
            this.Nodes._this    = this;
            
            var ul              = this.Nodes._control;

            if(!ul && this.Nodes.length == 0){
                ul                  = document.createElement('table');                    
                ul.setAttribute('border','0');
                ul.setAttribute('cellspacing','0');
                ul.setAttribute('cellpadding','0');
                ul.appendChild(document.createElement('tbody'));
                this.Nodes._control = ul;
            };
            
            this.ShowLines(this._showLines);
            
            this.Nodes.AddAt = function(index, node){
                var index = index || 0;
                if(node == void 0 || node.Type == void 0 || node.Type != 'TREENODE'){
                    alert('Node cannot be null');
                    return null;
                };
                
                if(index.toString().indexOf('-') >= 0){
                    alert('Index cannot be negative.');
                    return null;
                };

                node.Parent     = null;
                node.Value      = node.Value || node.Text;
                node._treeView  = this._this;
                
                var refNode     = ul.childNodes[0].childNodes[index*2] || null;
                
                ul.childNodes[0].insertBefore(node.ChildNodes._control, refNode);
                ul.childNodes[0].insertBefore(node._control, node.ChildNodes._control);

                this.splice(index, 0, node);                    

                if(this.length > 0){
                    var index = 0;
                    
                    for(index = 0; index<= this.length-1; index++){
                        var _node                   = this[index];                        
                        _node._control.className    = '';
                        _node.ChildNodes._control.childNodes[0].className = 'vertical_line';
                    };
                    
                    this[this.length-1]._control.className = 'last';  
                    this[this.length-1].ChildNodes._control.childNodes[0].className = '';
                };
                
                if(node._selectedNode != void 0){
                    node._selectedNode.Select(false);
                    node._selectedNode = null;
                };
                
                this._control.style.display = (this.length > 0 && this._this._visible) ? '' : 'none';
                
                return node; 
            };
            
            this.Nodes.AddAt1 = function(index, text, value, navigationURL, target, collapsedImageURL, expandedImageURL){
                return this.AddAt(index, new TreeNode(text, value, navigationURL, target, collapsedImageURL, expandedImageURL));   
            };
            
            this.Nodes.Add = function(node){
                return this.AddAt(this.length, node );
            };

            this.Nodes.Add1 = function(text, value, navigationURL, target, collapsedImageURL, expandedImageURL){
                return this.Add(new TreeNode(text, value, navigationURL, target, collapsedImageURL, expandedImageURL));   
            };
            
            this.Nodes.RemoveAt = function(index){
                var node = this[index];               
                this.Remove(node);                
            };
            
            this.Nodes.Remove = function(node){
                if(node == void 0 || node.Type == void 0 || node.Type != 'TREENODE'){
                    alert('Node cannot be null');
                    return null;                    
                };
                
                if(node.TreeView() == void 0 || node.Parent != void 0){
                    alert('Invalid node object supplied');
                    return null;
                }

                this._control.childNodes[0].removeChild(node._control);
                this._control.childNodes[0].removeChild(node.ChildNodes._control);                

                if(this.length > 0){
                    var index = 0;
                    
                    for(index = 0; index<= this.length-1; index++){
                        var _node = this[index];
                        
                        if(node == _node){
                            this.splice(index,1);
                            break;
                        };
                    };
                };
                
                if(node.TreeView().SelectedNode == node)
                    node.TreeView().SelectedNode = null;
                
                node = null;
                
                if(this.length > 0){
                    for(index = 0; index<= this.length-1; index++){
                        var _node                   = this[index];                        
                        _node._control.className    = '';        
                        _node.ChildNodes._control.childNodes[0].className = 'vertical_line';             
                    };
                    
                    this[this.length-1]._control.className = 'last';                     
                    this[this.length-1].ChildNodes._control.childNodes[0].className = '';
                };
                
                this._control.style.display = (this.length > 0 && this._this._visible) ? '' : 'none';
            };
            
            this.Nodes.Clear = function(){
                while(this.length > 0)
                    this.RemoveAt(0);
            };
        },
        
        SetContainer : function(parent){                    
            if(this._container == void 0){
                var objParent = typeof(parent) == 'string' ? document.getElementById(parent) : parent;

                if(objParent){
                    this._container = objParent;
                        
                    if(this._container && this.Nodes._control)
                        this._container.appendChild(this.Nodes._control);
                }
                else
                    alert('Parent cannot be null.');                
            }
            else
                alert('This control is already parented.');                
        },
        
        Container : function(){
            return this._container;
        },
        
        CSSName : function(className){
            if(className != void 0){
                this._className = className;
                this.LineStyle(this._lineStyle);                
            };

            return this._className;
        },

	    SelectedValuePath : function(){
	        return this.SelectedNode == null ? null : this.SelectedNode.ValuePath();        
        },
        
        ExpandAll : function(){
            if(this.Nodes.length > 0){
                var index = 0;
                
                for(index = 0; index<= this.Nodes.length-1; index++){
                    var _node = this.Nodes[index];
                    _node.Toggle(true, true);
                };
            };
        },
        
        CollapseAll : function(){
            if(this.Nodes.length > 0){
                var index = 0;
                
                for(index = 0; index<= this.Nodes.length-1; index++){
                    var _node = this.Nodes[index];
                    _node.Toggle(false, true);
                };
            };
        },

        FindNode : function(valuePath){
            if(valuePath == void 0){
                alert('valuePath cannot be null');
                return null;
            };
            
            var index = 0;            
            var valuePathSegments   = valuePath.split(this.PathSeparator);            
            var node                = null;
            
            for(segment in valuePathSegments){            
                if(node == void 0){
                    for(index = 0; index<= this.Nodes.length-1; index++){
                        var _node = this.Nodes[index];
                        if(_node.Value == valuePathSegments[segment]){
                            node = _node;
                            break;
                        };                            
                    };
                }
                else{
                    node = node.Find(valuePathSegments[segment], false);
                };                                            

                if(node == void 0)                            
                    return null;                
            };
            
            return node;
        },

        ShowLines : function(value){
            if(value != void 0){
                this._showLines = value;
                
                if(this.Nodes._control != void 0){
                    this.Nodes._control.className = (value != void 0 && value ? this._className + ' ' + this._className + '_' + this._lineStyle + '_line' : this._className);
                };
            };
            
            return this._showLines;
        },

        LineStyle : function(value){
            if(value != void 0){
                this._lineStyle = value;
                this.ShowLines(this._showLines);
            };
            
            return this._lineStyle;                
        },

        Visible : function(value){
            if(value != void 0){
                if(this.Nodes._control != void 0){
                    this.Nodes._control.style.display = value ? '' : 'none';                       
                    this._visible = value;
                };
            };

            return this.Nodes._control.style.display != 'none' ;
        },
        
        ToJSON : function(){
            var __treeView = {
                CSSName           : this.CSSName() || "",
                EditLabel         : this.EditLabel.toString(),
                ShowLines         : this.ShowLines().toString(),
                LineStyle         : this.LineStyle() || "",
                Visible           : this.Visible().toString(),
                PathSeparator     : this.PathSeparator,
                ToggleOnSelect    : this.ToggleOnSelect.toString(),
                SelectedValuePath : this.SelectedValuePath() || "",
                Nodes             : null
            };
            
            if(this.Nodes.length > 0){
                __treeView.Nodes = [];
                
                for(var nodeIndex = 0 ; nodeIndex<= this.Nodes.length-1; nodeIndex++){
                     __treeView.Nodes.push(this.Nodes[nodeIndex].ToJSON());
                };
            }
            
            return __treeView;
        },
        
        ToXML : function(){

            var __treeView = '<Tree ';
                __treeView += 'CSSName = "' + (this.CSSName() || '') + '" ';
                __treeView += 'EditLabel = "' + this.EditLabel.toString() + '" ';
                __treeView += 'ShowLines = "' + this.ShowLines().toString() + '" ';
                __treeView += 'LineStyle = "' + (this.LineStyle() || '') + '" ';
                __treeView += 'Visible = "' + this.Visible().toString() + '" ';
                __treeView += 'PathSeparator = "' + this.PathSeparator + '" ';
                __treeView += 'ToggleOnSelect = "' + this.ToggleOnSelect.toString() + '" ';
                __treeView += 'SelectedValuePath = "' + (this.SelectedValuePath() || '') + '">';
            
            if(this.Nodes.length > 0){
                for(var nodeIndex = 0 ; nodeIndex<= this.Nodes.length-1; nodeIndex++){
                    __treeView += this.Nodes[nodeIndex].ToXML();
                };
            };
            
            __treeView += '</Tree>';
            
            return __treeView;
        },
        
        _endRequest : function(){
            if(this && this.readyState === 4){  

                try {
                    if (typeof(this.status) === "undefined") {
                        // its an aborted request in Safari, ignore it
                        return;
                    };
                }
                catch(ex) {
                    // its an aborted request in Firefox, ignore it
                    return;
                };
                
                var treeview = this._this;
                
                if(treeview.Type === 'TREENODE')
                    treeview = treeview.TreeView();
                
                if(treeview != void 0 && treeview.EndRequest != void 0)
                    treeview.EndRequest(this.responseText, this._context);
            };
        },
       
        BeginRequest : function(url, context, verb){
            
            function serialize(obj){
	            var returnVal;
	            if(obj != undefined){
	                switch(obj.constructor){
		                case Array:
			                var vArr="[";
			                
			                for(var i=0;i<obj.length;i++)
			                {
				                if(i>0) vArr += ",";
				                vArr += serialize(obj[i]);
			                };
			                
			                vArr += "]"
			                return vArr;
    			            
		                case String:
			                returnVal = escape("'" + obj + "'");
			                return returnVal;
    			            
		                case Number:
			                returnVal = isFinite(obj) ? obj.toString() : null;
			                return returnVal;
    			            				
		                case Date:
			                returnVal = "#" + obj + "#";
			                return returnVal;		
    			            
		                default:
			                if(typeof obj == "object"){
				                var vobj=[];
				                for(attr in obj)
				                {
					                if(typeof obj[attr] != "function")
					                {
						                vobj.push('"' + attr + '":' + serialize(obj[attr]));
					                };
				                };
				                
				                if(vobj.length >0)
					                return "{" + vobj.join(",") + "}";
				                else
					                return "{}";
			                }
			                else
			                {
				                return obj.toString();
			                };
	                };
	            };
	            
	            return null;
            };
        
            if(url != void 0){
                var xmlHttp = new XMLHttpRequest();

                if(xmlHttp != void 0){
                    
                    xmlHttp.open(verb || 'GET', url, true);
                    xmlHttp.onreadystatechange = this._endRequest;                    
                    xmlHttp._this = this;
                    xmlHttp._context = context;
                    
                    if(verb && verb.toLowerCase() === 'post')
                        xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
                    
                    var sendStr = null;                                        
                    xmlHttp.send(sendStr);
                }
                else
                    alert("Your browser doesn't seem to support XMLHttpRequests.");                   
            };
        },
        
        EndRequest : null,

        SelectedNodeChanging : null,
        SelectedNodeChanged : null,
        
        BeforeCollapse : null,
        BeforeExpand : null,        
        AfterCollapse : null,
        AfterExpand : null,
        
        BeforeLabelEdit : null,
        AfterLabelEdit : null
    };
    
    this.treeView._init();
    
    return this.treeView;
};

TreeView.ParseHTML = function(object, treeview)
{
    function __converter(object, element){
        
        if(element != void 0 && element.tagName && element.tagName == 'UL'){
            if(object == void 0)
                object = new TreeView();

            for(var liIndex = 0; liIndex <= element.childNodes.length-1; liIndex++){
                var li = element.childNodes[liIndex];

                if(li != void 0 && li.tagName && li.tagName == 'LI'){
                    var treeNode = TreeNode(li.childNodes[0]);
                    
                    if(treeNode != void 0){
                        if(object.Type == 'TREEVIEW')
                            object.Nodes.Add(treeNode);
                        else if(object.Type == 'TREENODE')
                            object.ChildNodes.Add(treeNode);

                        __converter(treeNode, li.childNodes[1]);
                    };
                };
            };
            
            return object;
        };
        
        return null;
    };

    var obj = typeof(object) == 'string' ? document.getElementById(object) : object;
        
    if(obj == void 0)
        throw('Invalid object provided');

    return __converter(treeview, obj);
};

TreeView.ParseJSON = function(json, treeView){

    treeView = treeView || new TreeView();
    
    if(json.CSSName != void 0 && typeof(json.CSSName) == 'string' && json.CSSName.length > 0) 
        treeView.CSSName(json.CSSName);

    if(json.EditLabel != void 0 && typeof(json.EditLabel) == 'string' && json.EditLabel.length > 0)
        treeView.EditLabel = json.EditLabel.toLowerCase() == 'true' ? true  : false;
    
    if(json.ShowLines != void 0 && typeof(json.ShowLines) == 'string' && json.ShowLines.length > 0)
        treeView.ShowLines(json.ShowLines.toLowerCase() == 'true');
   
    if(json.PathSeparator != void 0 && typeof(json.PathSeparator) == 'string' && json.PathSeparator.length > 0) 
        treeView.PathSeparator = json.PathSeparator;
        
    if(json.ToggleOnSelect != void 0 && typeof(json.ToggleOnSelect) == 'string' && json.ToggleOnSelect.length > 0) 
        treeView.ToggleOnSelect = json.ToggleOnSelect.toLowerCase() == 'true' ? true  : false;

    if(json.Visible != void 0 && typeof(json.Visible) == 'string' && json.Visible.length > 0) 
        treeView.Visible(json.Visible.toLowerCase() == 'true');

    if(json.Nodes != void 0){
        for(var index = 0; index <= json.Nodes.length-1; index++){
            var _treeNode = TreeNode.ParseJSON(json.Nodes[index]);
            if(_treeNode != void 0) treeView.Nodes.Add(_treeNode);            
        };
    };

    return treeView;    
};

TreeView.ParseXML = function(xmlDocument, treeView){
    var xmlDoc = null;
    
    if(typeof xmlDocument === 'string')
    {        
        if (window.DOMParser)
        {
            parser=new DOMParser();
            xmlDoc=parser.parseFromString(xmlDocument,"text/xml");
        }
        else // Internet Explorer
        {
            xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async="false";
            xmlDoc.loadXML(xmlDocument);
        };
    }
    else if(typeof xmlDocument === 'object' && xmlDocument.getElementsByTagName != void 0){
        xmlDoc = xmlDocument;
    };

    treeView = treeView || new TreeView();

    if(xmlDoc != void 0){
        var treeElements = xmlDoc.getElementsByTagName('Tree');
        
        if(treeElements != void 0 && treeElements.length > 0){
            var treeElement = treeElements[0];
            
            var cssName         = treeElement.getAttribute('CSSName');
            var editLabel       = treeElement.getAttribute('EditLabel');
            var showLines       = treeElement.getAttribute('ShowLines');
            var pathSeparator   = treeElement.getAttribute('PathSeparator');
            var toggleOnSelect  = treeElement.getAttribute('ToggleOnSelect');
            var visible         = treeElement.getAttribute('Visible');                                  
            
            if( cssName != void 0 && typeof(cssName) == 'string' && cssName.length > 0) 
                treeView.CSSName(cssName);
               
            if(editLabel != void 0 && typeof(editLabel) == 'string' && editLabel.length > 0)
                treeView.EditLabel = editLabel.toLowerCase() == 'true' ? true  : false;
            
            if(showLines != void 0 && typeof(showLines) == 'string' && showLines.length > 0)
                treeView.ShowLines(showLines.toLowerCase() == 'true');
           
            if(pathSeparator != void 0 && typeof(pathSeparator) == 'string' && pathSeparator.length > 0) 
                treeView.PathSeparator = pathSeparator;
                
            if(toggleOnSelect != void 0 && typeof(toggleOnSelect) == 'string' && toggleOnSelect.length > 0) 
                treeView.ToggleOnSelect = toggleOnSelect.toLowerCase() == 'true' ? true  : false;

            if(visible != void 0 && typeof(visible) == 'string' && visible.length > 0) 
                treeView.Visible(visible.toLowerCase() == 'true');
            
            if(treeElement.childNodes.length > 0){
                for(var index = 0; index <= treeElement.childNodes.length-1; index++){
                    var _treeNode = TreeNode.ParseXML(treeElement.childNodes[index]);                    
                    if(_treeNode != void 0) treeView.Nodes.Add(_treeNode);
                };
            };
        };
    };

    return treeView;    
};

function TreeNode(text, value, navigationURL, target, collapsedImageURL, expandedImageURL)
{
    this.treeNode = {
        _navigateUrl        : navigationURL,
        _target             : target || null,
        _collapsedImageURL  : collapsedImageURL,
        _expandedImageURL   : expandedImageURL,
        _control            : null,
        _isUpdating         : false,
        _treeView           : null,
        _expanded           : false,
        _selectedNode       : null,
        
        Type                : 'TREENODE',
        Text                : text,
        Value               : value,
        ChildNodes          : [],
        Parent              : null,
        Image               : null,        
        AllowKeyNavigation  : true,
        
        _onTaggleClick : function(evt){
            
            var e       = evt || window.event;
            var trgt    = e.target || e.srcElement;

            if(trgt && (e.nodeType == 3 || e.nodeType == 4))
                trgt = trgt.parentNode;

            if(trgt.treeNode)
                trgt.treeNode.Toggle();
            
            e.returnValue   = false;
            e.cancelBubble  = true;
                
            if( e.stopPropagation ) { e.stopPropagation(); }
            if( e.preventDefault ) { e.preventDefault(); }

            return false;
        },
        
        _onNodeClick : function(evt){
            var e       = evt || window.event;
            var trgt    = e.target || e.srcElement;

            if(trgt && (e.nodeType == 3 || e.nodeType == 4))
                trgt = trgt.parentNode;                       

            var clickedTag = trgt.tagName || '';
                                                            
            while (true){
                if(trgt.treeNode == void 0){
                    trgt = trgt.parentNode || trgt.offsetParent;
                   
                    if(trgt != void 0)
                        continue;
                };
                
                break;
            }

            var action  = true;

            if(trgt && trgt.treeNode != void 0){
                if(document.all /*IE*/ && clickedTag != 'A' && trgt.tagName == 'A' && trgt.click != void 0){
                    action = false;
                    
                    //Found some weird behaviour with IE7. Need to call ancher click explicitly 
                    //as actual click was not on A, so its navigation url was not getting into effect.
                    trgt.click();
                }
                else{
                    var treeView = trgt.treeNode.TreeView();

                    if(!trgt.treeNode.IsSelected() || (treeView && !treeView.EditLabel))
                        action = trgt.treeNode.Select();
                    else if(!trgt.treeNode.IsEditing())
                        action = trgt.treeNode.BeginEdit();
                };
            };

            if(action != void 0 && !action){                
                e.returnValue   = false;
                e.cancelBubble  = true;
                
                if( e.stopPropagation ) { e.stopPropagation(); }
                if( e.preventDefault ) { e.preventDefault(); }
            };

            return action != void 0 ? action : true;
        },
        
        _onNodeNavigate : function(evt){
            function KeyLeftPressed(node){
                if(node.ChildNodes.length > 0 && node.IsExpanded())
                    return node.Collapsed();
                else
                    return KeyUPPressed(node);
            };
            
            function KeyRightPressed(node){
                if(node.ChildNodes.length > 0 && !node.IsExpanded())
                    return node.Expanded();
                else
                    return KeyDownPressed(node);                    
            };

            function KeyUPPressed(node){
            
                function __selectLastNode(node){
                    var retValue = false;
                
                    if(node != void 0){
                        if(node.ChildNodes.length > 0 && node.IsExpanded())
                            __selectLastNode(node.ChildNodes[node.ChildNodes.length-1]);
                        else
                            retValue = node.Select(false);
                    };
                    
                    return retValue;
                };

                var nodeIndex = node.Index();                               
                var treeNodeCollection = null;

                try{
                    treeNodeCollection = node.Parent != void 0 ? node.Parent.ChildNodes : node.TreeView().Nodes;
                } catch(e){}

                if(treeNodeCollection){
                    if(nodeIndex > 0){
                        var treeNode = treeNodeCollection[nodeIndex-1];
                        return __selectLastNode(treeNode);
                    }
                    else if(nodeIndex == 0 && node.Parent != void 0){
                        return node.Parent.Select(false);
                    };
                };
                
                return true;
            };  
            
            function __selectNextNode(node){   
                var index = node.Index();
                
                if(index > -1 && node != void 0){
                    try{
                        treeNodeCollection = node.Parent != void 0 ? node.Parent.ChildNodes : node.TreeView().Nodes;
                    } catch(e){}
                    
                    if(treeNodeCollection != void 0 && treeNodeCollection.length > 0){
                        if(treeNodeCollection.length-1 > index)
                            return treeNodeCollection[index+1].Select(false);
                        else if(node.Parent != void 0)
                            return __selectNextNode(node.Parent);
                    };
                };

                return false;
            }

            function KeyDownPressed(node){
                if(node.ChildNodes.length > 0 && node.IsExpanded())
                    node.ChildNodes[0].Select(false);
                else
                    __selectNextNode(node);
            };
            
            var e       = evt || window.event;            
            var keyCode = e.keyCode;
            
            if(keyCode == 13 || keyCode == 27 || keyCode == 37 || keyCode == 38 || keyCode == 39 || keyCode == 40 || keyCode == 113){
                var trgt = e.target || e.srcElement;

                if(trgt && (e.nodeType == 3 || e.nodeType == 4))
                    trgt = trgt.parentNode;
                
                while (true){
                    if(trgt.treeNode == void 0){
                        trgt = trgt.parentNode;
                       
                        if(trgt != void 0 && trgt.tagName != 'LI')
                            continue;
                    };
                    
                    break;
                }
                                            
                var action  = true;

                if(trgt != void 0 && trgt.treeNode != void 0){
                    
                    
                    switch (keyCode){
                        case 113:
                            if(trgt.treeNode.IsSelected())
                                action = trgt.treeNode.BeginEdit();
                            break;

                        case 13:
                            if(trgt.treeNode.IsEditing())
                                action = trgt.treeNode.EndEdit();
                            break;
                            
                        case 27:
                            if(trgt.treeNode.IsEditing())
                                action = trgt.treeNode.CancelEdit();
                            
                            break;
                        case 37 :
                        case 38 :
                        case 39 :
                        case 40 :
                        
                            if(trgt.treeNode.AllowKeyNavigation == true && !trgt.treeNode.IsEditing()){
                                var treeView = trgt.treeNode.TreeView();
                                
                                if(treeView != void 0 && treeView.EnableKeyNavigation == true) {
                                    if(keyCode == 37)
                                        action = KeyLeftPressed(trgt.treeNode);
                                    else if(keyCode == 39)
                                        action = KeyRightPressed(trgt.treeNode);
                                    else if(keyCode == 40)
                                        KeyDownPressed(trgt.treeNode);
                                    else if(keyCode == 38)                        
                                        KeyUPPressed(trgt.treeNode);
                                };                            
                            };
                            
                            break;                            
                    };                    
                };
                
                if(action != void 0 && !action){
                    e.returnValue   = false;
                    e.cancelBubble  = true;
                    
                    if( e.stopPropagation ) { e.stopPropagation(); }
                    if( e.preventDefault ) { e.preventDefault(); }
                };

                return action != void 0 ? action : true;
            };
            
            return true;                
        },        

        _onNodeLableBlur : function(evt){
            var e       = evt || window.event;
            var trgt    = e.target || e.srcElement;

            if(trgt && (e.nodeType == 3 || e.nodeType == 4))
                trgt = trgt.parentNode;
            
            var trgt    = trgt.tagName == 'INPUT' && trgt.type.toLowerCase() == 'text' ? trgt : null;
            
            if(trgt){
                var action  = true;
                
                if(trgt && trgt.treeNode){
                    action = trgt.treeNode.EndEdit();
                };
            };
            
            if(action != void 0 && !action){
                e.returnValue   = false;
                e.cancelBubble  = true;
                
                if( e.stopPropagation ) { e.stopPropagation(); }
                if( e.preventDefault ) { e.preventDefault(); }
            };
            
            return action != void 0 ? action : true;            
        },
            
        _init : function(){           
            if(this.Text != void 0 && typeof(this.Text) != 'string'){
                if(this.Text.nodeType){
                    if(this.Text.nodeType == 3 || this.Text.nodeType == 4){
                        this.Text = this.Text.data;
                    }
                    else if(this.Text.tagName && this.Text.tagName == 'A'){
                        this._navigateUrl   = this.Text.getAttribute('href');
                        this._target        = this.Text.getAttribute('target');   
                        this.Text           = this.Text.innerText || this.Text.childNodes[0].data;                                     
                    }
                    else
                        throw("This object is not supported for parsing.");
                }
            }
            
            var _hiddenElementStyle = 'style="float:left; height:0px; display:block; line-height:0; overflow:hidden;"';
            
            var _li = document.createElement('tr');
            _li.appendChild(document.createElement('td'))
            _li.appendChild(document.createElement('td'))
            _li.appendChild(document.createElement('td'))
            
            _li.childNodes[0].className = 'vertical_line';
            _li.childNodes[0].setAttribute('align','center');
            _li.childNodes[1].className = 'indent';

            _li.childNodes[0].innerHTML = '<img class="vertical_line" src="treeview/images/whitespace.png" ' + _hiddenElementStyle + '/><div class="hitarea" />';

            var _hitarea            = _li.childNodes[0].childNodes[1];

            if(_hitarea.addEventListener)
                _hitarea.addEventListener('click',this._onTaggleClick, false);
            else if(_hitarea.attachEvent)
                _hitarea.attachEvent('onclick',this._onTaggleClick);

            _hitarea.treeNode = this;
            
            var _hiddenElement = '<img class="border" src="treeview/images/whitespace.png" ' + _hiddenElementStyle + '/>';
            
            _li.childNodes[1].innerHTML = '<img class="indent" src="treeview/images/whitespace.png" ' + _hiddenElementStyle + '>';
            _li.childNodes[2].innerHTML = '<table border=0 cellpadding=0 cellspacing=0 class="node"><tbody><tr><td class="node_upper_left">' + _hiddenElement + '</td><td class="node_upper_middle">' + _hiddenElement + '</td><td class="node_upper_right">' + _hiddenElement + '</td></tr><tr><td class="node_middle_left">' + _hiddenElement + '</td><td class="node_middle_middle"><a class="dataarea"><table border=0 cellpadding=0 cellspacing=0><tbody><tr><td><img class="icon" border="0"/></td><td style="white-space:nowrap;">' + this.Text + '</td></tr></tbody></table></a></td><td class="node_middle_right">' + _hiddenElement + '</td></tr><tr><td class="node_bottom_left">' + _hiddenElement + '</td><td class="node_bottom_middle">' + _hiddenElement + '</td><td class="node_bottom_right">' + _hiddenElement + '</td></tr></tbody></table>';

            var _data = _li.childNodes[2].childNodes[0].childNodes[0].childNodes[1].childNodes[1].childNodes[0];
            _data.title = this.Text;
            
            if(_data.addEventListener){
                _data.addEventListener('click',this._onNodeClick, false);
                _data.addEventListener('keydown',this._onNodeNavigate, false);
            }
            else if(_data.attachEvent){
                _data.attachEvent('onclick',this._onNodeClick);
                _data.attachEvent('onkeydown',this._onNodeNavigate);
            };
            
            this.Image = _data.childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[0];

            if(!this._collapsedImageURL)
                this.Image.style.display = 'none';

            _data.label = _data.childNodes[0].childNodes[0].childNodes[0].childNodes[1];
            _data.label.className = 'label';
            
            _data.treeNode = this;
            
            _hiddenElementStyle = null;
            _hiddenElement = null;
            
            this._control = _li;               
            this._control._hitarea = _hitarea;
            this._control._data = _data;

            this.ChildNodes._control = document.createElement('tr');
            this.ChildNodes._control.appendChild(document.createElement('td'))
            this.ChildNodes._control.appendChild(document.createElement('td'))
            this.ChildNodes._control.appendChild(document.createElement('td'))
            this.ChildNodes._control.childNodes[0].className = 'vertical_line';
            this.ChildNodes._control.childNodes[2].innerHTML = '<table border=0 cellpadding=0 cellspacing=0><tbody></tbody></table>';

            this.ChildNodes._control.style.display  = 'none';
            this.ChildNodes._this = this;

            this.SetNavigationURL(this._navigateUrl, this._target);            
            this.SetImageURL(this._collapsedImageURL, this._expandedImageURL);                        
            
            this.ChildNodes.AddAt = function(index, node){
                var index = index || 0;
                
                if(node == void 0 || node.Type == void 0 || node.Type != 'TREENODE'){
                    alert('Node cannot be null');
                    return null;
                };
            
                if(index.toString().indexOf('-') >= 0){
                    alert('Index cannot be negative.');
                    return null;
                };

                node.Parent = this._this;
                node.Value = node.Value || node.Text;
                
                var ul = this._control.childNodes[2].childNodes[0].childNodes[0];

                var refNode = ul.childNodes[index*2] || null;
                
                ul.insertBefore(node.ChildNodes._control, refNode);
                ul.insertBefore(node._control, node.ChildNodes._control);

                this.splice(index, 0, node);
                
                if(this.length > 0){
                    var index = 0;
                    
                    for(index = 0; index<= this.length-1; index++){
                        var _node = this[index];
                        _node._control.className = '';
                        _node.ChildNodes._control.childNodes[0].className = 'vertical_line';                        
                    };

                    this[this.length-1]._control.className = 'last';                    
                    this[this.length-1].ChildNodes._control.childNodes[0].className = '';
                };

                this._control.style.display = ( this.length > 0 &&  this._this.IsExpanded()) ? '' : 'none';
                this._this._control._hitarea.className = this.length == 0 ? 'hitarea' : (this._this.IsExpanded() ?  'hitarea collapsable-hitarea' : 'hitarea expandable-hitarea');        
                this._this._control._hitarea.title = this.length == 0 ? '' : (!this._this.IsExpanded() ? 'Expand ' : 'Collapse ') + this._this.Text;
                
                if(node._selectedNode != void 0){
                    node._selectedNode.Select(false);
                    node._selectedNode = null;
                };
                
                this._control.style.display = (this._this.IsExpanded() ? (this.length > 0 ? '' : 'none') : 'none');
                
                return node;
            };
            
            this.ChildNodes.AddAt1 = function(index, text, value, navigationURL, target, collapsedImageURL, expandedImageURL){
                return this.AddAt(index, new TreeNode(text, value, navigationURL, target, collapsedImageURL, expandedImageURL));   
            };            
            
            this.ChildNodes.Add = function(node){
                return this.AddAt(this.length, node);
            };
            
            this.ChildNodes.Add1 = function(text, value, navigationURL, target, collapsedImageURL, expandedImageURL){
                return this.Add(new TreeNode(text, value, navigationURL, target, collapsedImageURL, expandedImageURL));
            };
            
            this.ChildNodes.RemoveAt = function(index){ 
                var node = this[index];               
                this.Remove(node);
            };
            
            this.ChildNodes.Remove = function(node){     
                if(node == void 0 || node.Type == void 0 || node.Type != 'TREENODE'){
                    alert('Node cannot be null');
                    return null;
                };

                this._control.childNodes[2].childNodes[0].childNodes[0].removeChild(node._control);
                this._control.childNodes[2].childNodes[0].childNodes[0].removeChild(node.ChildNodes._control);

                if(this.length > 0){
                    var index = 0;
                    
                    for(index = 0; index<= this.length-1; index++){
                        var _node = this[index];
                        
                        if(node == _node){
                            this.splice(index,1);
                            break;
                        };
                    };
                };
                
                if(node.TreeView() != void 0 && 
                    node.TreeView().SelectedNode == node)
                        node.TreeView().SelectedNode = null;
                
                node = null;
                
                if(this.length > 0){
                    for(index = 0; index<= this.length-1; index++){
                        var _node = this[index];                        
                        _node._control.className = '';    
                        _node.ChildNodes._control.childNodes[0].className = 'vertical_line';                 
                    };
                    
                    this[this.length-1]._control.className = 'last';                     
                    this[this.length-1].ChildNodes._control.childNodes[0].className = '';
                };
                
                this._control.style.display = this.length > 0 && this._this.IsExpanded() ? '' : 'none';
                this._this._control._hitarea.className = this.length == 0 ? 'hitarea' : (this._this.IsExpanded() ?  'hitarea collapsable-hitarea' : 'hitarea expandable-hitarea');        
                this._this._control._hitarea.title = this.length == 0 ? '' : (!this._this.IsExpanded() ? 'Expand ' : 'Collapse ') + this._this.Text;    
                this._this.SetImageURL(this._this._collapsedImageURL, this._this._expandedImageURL); 
            };
            
            this.ChildNodes.Clear = function(){
                while(this.length > 0)
                    this.RemoveAt(0);
            };
        },
        
        TreeView : function(fn){
        
            var _parent = this;
            
            while(true){
                if(_parent.Parent == void 0)
                    return _parent._treeView;
                else{
                    _parent = _parent.Parent;
                    
                    if(fn) fn(_parent);
                };
            };
            
            return null;
        },

        IsExpanded: function(){               
            return this._expanded;
        },
        
        GetNavigationURL : function(){
            return (this._navigateUrl == 'javascript:void(0)' || this._navigateUrl == '#') ? null : this._navigateUrl;
        },

        GetTarget : function(){
            return this._target;
        },
        
        SetNavigationURL : function(url, target){
            this._navigateUrl = url || 'javascript:void(0)';           
            this._control._data.href = this._navigateUrl;
            
            this.SetTarget(target);
        },
        
        SetTarget : function(target){
            this._target = target || null;
            
            if(this._target)
                this._control._data.target = this._target;
            else
                this._control._data.removeAttribute('target');
        },
        
        GetImageURL : function(){
            return {CollapsedImageURL: this._collapsedImageURL, ExpandedImageURL: this._expandedImageURL};
        },
        
        SetImageURL : function(collapsedImageURL, expandedImageURL){
            this._collapsedImageURL     = collapsedImageURL || null
            this._expandedImageURL      = expandedImageURL || null

            if(this.Image && !this._isUpdating){
                
                if(this.ChildNodes.length == 0 || !this.IsExpanded()){
                    if(!this._collapsedImageURL)
                        this.Image.style.display = 'none';
                    else{
                        this.Image.style.display = '';
                        this.Image.src = this._collapsedImageURL;
                    };
                }
                else{
                    if(!this._expandedImageURL && !this._collapsedImageURL)
                        this.Image.style.display = 'none';
                    else{
                        this.Image.style.display = '';
                        this.Image.src = this._expandedImageURL || this._collapsedImageURL;
                    };
                };
            };
        },

        Select : function(toggle){ 
            this._selectedNode = null;
                       
            var _parent = this.TreeView(function(parent){parent.Expanded()});                      

            if(_parent != void 0){
                
                var action = true;
                
                if(_parent.SelectedNodeChanging)
                    action = _parent.SelectedNodeChanging(_parent, this);

                if(action != void 0 && !action) return false;
                
                if(this._control && this._control._data  != void 0 && this._control._data.focus)
                    this._control._data.focus();
                
                if(_parent.SelectedNode != void 0){
                    _parent.SelectedNode._control.childNodes[2].childNodes[0].className = 'node';
                };

                _parent.SelectedNode = this;
                _parent.SelectedNode._control.childNodes[2].childNodes[0].className = 'node selected';
                
                if(toggle == void 0 ? _parent.ToggleOnSelect : toggle && _parent.ToggleOnSelect)
                    this.Toggle();

                if(_parent.SelectedNodeChanged)
                    return _parent.SelectedNodeChanged(_parent, _parent.SelectedNode);

                return true;
            }
            else{                
                _parent = this;

                while(true){
                    if(_parent.Parent != void 0)
                        _parent = _parent.Parent;
                    else
                        break;                        
                };
                
                if(_parent != void 0){
                    
                    if(_parent._selectedNode != void 0){
                        _parent._selectedNode._control.childNodes[2].childNodes[0].className = 'node';
                    };
                    
                    _parent._selectedNode = this;
                    _parent._selectedNode._control.childNodes[2].childNodes[0].className = 'node selected';                    
                    
                    return true;
                };                    
            };
            
            return false;
        },
        
        ValuePath : function(){
            var _valuePath = this.Value;
            var _parent = this.Parent;

            while(_parent != void 0){
                _valuePath = _parent.Value + '#PathSeparator#' + _valuePath;
                _parent = _parent.Parent;
            };
            
            _parent = this.TreeView();
            
            var PathSeparator = '\\';
            if(_parent) PathSeparator = _parent.PathSeparator || '\\';
                
            return _valuePath.replace(/#PathSeparator#/g,PathSeparator);
        },
        
        Toggle : function(value, recursive){
            if(this.ChildNodes._control){
                var isExpanded = this.IsExpanded();
                var isExpanding = ((value != void 0 ? 
                                      (value  ? '' : 'none') :
                                      (this.ChildNodes._control.style.display == 'none' ? '' : 'none')) != 'none');
                
                
                var _parent = this.TreeView();

                var action = true;

                if(_parent != void 0)
                   if(!isExpanded && isExpanding && _parent.BeforeExpand)
                        action = _parent.BeforeExpand(_parent, this);
                    else if(isExpanded && !isExpanding && _parent.BeforeCollapse) 
                        action = _parent.BeforeCollapse(_parent,this);       

                if(action != void 0 && !action) return false;                               
                
                this._expanded = isExpanding; 

                this.ChildNodes._control.style.display = (isExpanding  && this.ChildNodes.length > 0) ? '' : 'none';

                this._control._hitarea.className = this.ChildNodes.length == 0 ? 'hitarea' : (isExpanding ?  'hitarea collapsable-hitarea' : 'hitarea expandable-hitarea');        
                document.title = this._control._hitarea.style['background'];
                this._control._hitarea.title = this.ChildNodes.length == 0 ? '' : (!isExpanding ? 'Expand ' : 'Collapse ') + this.Text;               
                
                this.SetImageURL(this._collapsedImageURL, this._expandedImageURL);
                
                if(recursive && this.ChildNodes.length > 0){
                    var index = 0;
                    
                    for(index = 0; index<= this.ChildNodes.length-1; index++){
                        var _node = this.ChildNodes[index];
                        
                        _node.Toggle(value, true);
                    };
                    
                    this.ChildNodes[this.ChildNodes.length-1]._control.className = 'last';
                    this.ChildNodes[this.ChildNodes.length-1].ChildNodes._control.childNodes[0].className = '';
                };
                               
                if(_parent != void 0)                
                   if(isExpanding && _parent.AfterExpand)
                        _parent.AfterExpand(_parent, this);
                    else if(_parent.AfterCollapse) 
                        _parent.AfterCollapse(_parent,this);                        

                return true;
            };

            return false;
        },
        
        Expanded : function(recursive){
            this.Toggle(true, recursive);
        },
        
        Collapsed : function(recursive){
            this.Toggle(false, recursive); 
        },
        
        Find : function(value, recursive){
            var index = 0;

            for(index = 0; index<= this.ChildNodes.length-1; index++){
                var _node = this.ChildNodes[index];
                if(_node.Value == value)
                    return _node;
                else if(recursive)
                    return _node.Find(value, recursive);
            };
            
            return null;
        },
        
        BeginUpdate : function(){
            var treeView = this.TreeView();
        
            if(this.Image && treeView){
                this.Image.style.display = '';
                this.Image.src = treeView.UpdateImageURL;
                this._isUpdating = true;
            };
        },
        
        EndUpdate: function(){
            this._isUpdating = false;
            this.SetImageURL(this._collapsedImageURL, this._expandedImageURL);
        },
                
        Level : function(){
            var level = 0;
            var _parent = this.Parent;

            while(_parent != void 0){
                level++;
                _parent = _parent.Parent;
            };
            
            return level;
        },
        
        IsSelected : function(){
            var treeView = this.TreeView();
            
            return (treeView && treeView.SelectedNode == this);
        },
        
        Index : function(){
            
            var treeNodeCollection = null;
            
            try{
                treeNodeCollection = this.Parent != void 0 ? this.Parent.ChildNodes : this.TreeView().Nodes;
            } catch(e){}
            
            if(treeNodeCollection != void 0){
                for(var index = 0; index<= treeNodeCollection.length-1; index++){                    
                    if(treeNodeCollection[index] == this)
                        return index;
                };
            };
            
            return -1;
        },
        
        BeginEdit : function(){
            var treeView = this.TreeView();
            
            if(treeView == void 0 || !treeView.EditLabel) return false;
            
            var action = true;
            
            this._control._data.label.innerHTML = '';   
            this._control._data.removeAttribute('href');
            
            if(this._control._data._edit == void 0){
                
                if(treeView.BeforeLabelEdit)
                    action = treeView.BeforeLabelEdit(treeView, this);
                
                if(action){
                    var edit        = document.createElement('input');
                    edit.className  = 'editlabel';
                    edit.type       = 'text';
                    edit.value      = this.Text;
                    edit.treeNode   = this;
                    
                    if(edit.addEventListener)
                        edit.addEventListener('blur',this._onNodeLableBlur, false);
                    else if(edit.attachEvent)
                        edit.attachEvent('onblur',this._onNodeLableBlur);
                    
                    this._control._data._edit = edit;
                    this._control._data.label.appendChild(edit);
                };
            };
                                     
            if(this._control._data._edit) this._control._data._edit.focus();

            return action != void 0 ? action : true;
        },

        CancelEdit : function(){
            if(this._control._data._edit != void 0){                
                this._control._data.label.removeChild(this._control._data._edit);    
                this._control._data.label.innerHTML = this.Text;                           
                this._control._data._edit = null;     
                
                this.SetNavigationURL(this._navigateUrl, this._target);   
                
                if(this.IsSelected()){      
                    if(this._control && this._control._data  != void 0 && this._control._data.focus)
                        this._control._data.focus();
                };
            };
            
            return true;
        },
        
        EndEdit : function(){
            if(this._control._data._edit != void 0){      
                if(this._control._data._edit.value.replace(/^\s+|\s+$/,'').length > 0)
                    this.Text                       = this._control._data._edit.value;

                this._control._data.label.removeChild(this._control._data._edit);   
                this._control._data.label.innerHTML = this.Text;            
                this._control._data.title           = this.Text;            
                this._control._data._edit           = null;                

                if(this.IsSelected()){
                    if(this._control && this._control._data  != void 0 && this._control._data.focus)
                        this._control._data.focus();                                                
                };

                this.SetNavigationURL(this._navigateUrl, this._target);                        
                
                var treeView = this.TreeView();            
                if(treeView == void 0) return false;
                
                if(treeView.AfterLabelEdit)
                    return treeView.AfterLabelEdit(treeView, this);
            };
            
            return true;
        },
        
        IsEditing : function(){              
            return (this._control._data._edit != void 0);
        },
        
        WordWrap : function(value){
            try{
                if(this._control && this._control._data  != void 0){
                                    
                    var _td = this._control._data.childNodes[0].childNodes[0].childNodes[0].childNodes[1];
                                                            
                    if(_td != void 0){
                        if(value != void 0){
                            if(value) 
                                if(_td.style.setAttribute){ 
                                    _td.style.setAttribute('whiteSpace','nowrap') ;
                                }
                                else _td.style.whiteSpace = 'nowrap';
                            else 
                                if(_td.style.removeAttribute) {
                                    _td.style.removeAttribute('whiteSpace');
                                }
                                else _td.style.whiteSpace = null;

                                if (this._control.render)  this._control.render();                                
                        };
                            
                        return (_td.style.getAttribute ? _td.style.getAttribute('whiteSpace') : _td.style.whiteSpace) == 'nowrap';
                    };
                };
            }
            catch(e){}
            
            return false;
        },
        
        SetText : function(value){
            if(value != void 0){
                this._control._data.label.innerHTML = value;
                this._control._data.title = value;
                this.Text = value;
            };

            return this._control._data.label.innerHTML;
        },

        SetValue : function(value){
            if(value != void 0){
                this.Value = value;
            };
            
            return this.Value;
        },       
        
        ToJSON : function(){
            var __treeNode = {
                Text                  : this.Text || "",
                Value                 : this.Value || "",
                ValuePath             : this.ValuePath() || "",
                AllowKeyNavigation    : this.AllowKeyNavigation.toString(),
                NavigationURL         : this.GetNavigationURL() || "",
                Target                : this.GetTarget() || "",
                CollapsedImageURL     : this.GetImageURL().CollapsedImageURL || "",
                ExpandedImageURL      : this.GetImageURL().ExpandedImageURL || "",
                IsSelected            : this.IsSelected().toString(),
                IsExpanded            : this.IsExpanded().toString(),
                Level                 : this.Level().toString(),
                Index                 : this.Index().toString(),
                WordWrap              : this.WordWrap().toString(),
                ChildNodes            : null
            };
            
            if(this.ChildNodes.length > 0){
                __treeNode.ChildNodes = [];
                
                for(var nodeIndex = 0 ; nodeIndex<= this.ChildNodes.length-1; nodeIndex++){
                     __treeNode.ChildNodes.push(this.ChildNodes[nodeIndex].ToJSON());
                };
            }
            
            return __treeNode;
        },
        
        ToXML : function(){

            var __treeNode = '<TreeNode ';
                __treeNode += 'Text = "' + (this.Text || '') + '" ';
                __treeNode += 'Value = "' + (this.Value || (this.Text || '')) + '" ';
                __treeNode += 'ValuePath = "' + (this.ValuePath() || '') + '" ';
                __treeNode += 'AllowKeyNavigation = "' + this.AllowKeyNavigation.toString() + '" ';
                __treeNode += 'NavigationURL = "' + (this.GetNavigationURL() || '') + '" ';
                __treeNode += 'Target = "' + (this.GetTarget() || '') + '" ';
                __treeNode += 'CollapsedImageURL = "' + (this.GetImageURL().CollapsedImageURL || '') + '" ';
                __treeNode += 'ExpandedImageURL = "' + (this.GetImageURL().ExpandedImageURL || '') + '" ';
                __treeNode += 'IsSelected = "' + this.IsSelected().toString() + '" ';
                __treeNode += 'IsExpanded = "' + this.IsExpanded().toString() + '" ';       
                __treeNode += 'Level = "' + this.Level().toString() + '" ';
                __treeNode += 'Index = "' + this.Index().toString() + '" ';
                __treeNode += 'WordWrap = "' + this.WordWrap().toString() + '">';
            
            if(this.ChildNodes.length > 0){               
                for(var nodeIndex = 0 ; nodeIndex<= this.ChildNodes.length-1; nodeIndex++){
                     __treeNode += this.ChildNodes[nodeIndex].ToXML();
                };
            };
            
            __treeNode += '</TreeNode>';
            
            return __treeNode;
        },

        CopyTo : function(parent, afterindex){                                                
            if(parent != void 0){
                var nodeJSON = this.ToJSON(); 
                
                if(parent.Type == 'TREEVIEW')
                    parent.Nodes.AddAt(afterindex,TreeNode.ParseJSON(nodeJSON));            
                else if(parent.Type == 'TREENODE')                     
                    parent.ChildNodes.AddAt(afterindex,TreeNode.ParseJSON(nodeJSON));                            
            };
        }
    };
    
    this.treeNode._init();
    
    return this.treeNode;
};

TreeNode.ParseJSON = function(json, treeNode){        
    if(json.Text != void 0){
        treeNode = treeNode || new TreeNode();
        
        if(json.Text != void 0 && typeof(json.Text) == 'string' && json.Text.length > 0) 
            treeNode.SetText(json.Text);
        
        if(json.Value != void 0 && typeof(json.Value) == 'string' && json.Value.length > 0) 
            treeNode.SetValue(json.Value);
        
        if(json.AllowKeyNavigation != void 0 && typeof(json.AllowKeyNavigation) == 'string' && json.AllowKeyNavigation.length > 0)
            treeNode.AllowKeyNavigation = json.AllowKeyNavigation.toLowerCase() == 'true' ? true  : false;
        
        if(json.NavigationURL != void 0 && typeof(json.NavigationURL) == 'string' && json.NavigationURL.length > 0) 
            treeNode.SetNavigationURL(json.NavigationURL);
        
        if(json.Target != void 0 && typeof(json.Target) == 'string' && json.Target.length > 0) 
            treeNode.SetTarget(json.Target);
        
        var _collapsedImageURL = null;
        var _expandedImageURL = null;
        
        if(json.CollapsedImageURL != void 0 && typeof(json.CollapsedImageURL) == 'string' && json.CollapsedImageURL.length > 0) 
            _collapsedImageURL = json.CollapsedImageURL;
            
        if(json.ExpandedImageURL != void 0 && typeof(json.ExpandedImageURL) == 'string' && json.ExpandedImageURL.length > 0) 
            _expandedImageURL = json.ExpandedImageURL;
            
        treeNode.SetImageURL(_collapsedImageURL, _expandedImageURL);        

        if(json.WordWrap != void 0 && typeof(json.WordWrap) == 'string' && json.WordWrap.length > 0) 
            treeNode.WordWrap(json.WordWrap.toLowerCase() == 'true');               

        if(json.IsSelected != void 0 && typeof(json.IsSelected) == 'string' && json.IsSelected.toLowerCase() == 'true') 
            treeNode.Select(false);

        if(json.IsExpanded != void 0 && typeof(json.IsExpanded) == 'string' && json.IsExpanded.toLowerCase() == 'true') 
            treeNode.Expanded();

        if(json.ChildNodes != void 0){
            for(var index = 0; index <= json.ChildNodes.length-1; index++){
                var _treeNode = TreeNode.ParseJSON(json.ChildNodes[index]);
                if(_treeNode != void 0) treeNode.ChildNodes.Add(_treeNode);
            };
        };
        
        return treeNode;
    };

    return null;
};

TreeNode.ParseXML = function(xmlData, treeNode){           
   
    var nodeElement = null;
    
    if(typeof xmlData === 'string')
    {
        if (window.DOMParser)
        {
            parser=new DOMParser();
            nodeElement=parser.parseFromString(xmlData,"text/xml");
        }
        else // Internet Explorer
        {
            nodeElement=new ActiveXObject("Microsoft.XMLDOM");
            nodeElement.async="false";
            nodeElement.loadXML(xmlData);
        };
    }
    else if(typeof xmlData === 'object'){
        nodeElement = xmlData;
    };

    if(nodeElement != void 0 && nodeElement.nodeType === 1 && nodeElement.tagName == 'TreeNode'){
        treeNode = treeNode || new TreeNode();
        
        var text                = nodeElement.getAttribute('Text') || nodeElement.getAttribute('Title');
        var value               = nodeElement.getAttribute('Value') || nodeElement.getAttribute('NodeId');
        var href                = nodeElement.getAttribute('NavigationURL') || nodeElement.getAttribute('Href');
        var allowKeyNavigation  = nodeElement.getAttribute('AllowKeyNavigation');
        var target              = nodeElement.getAttribute('Target');
        var collapsedImageURL   = nodeElement.getAttribute('CollapsedImageURL');
        var expandedImageURL    = nodeElement.getAttribute('ExpandedImageURL');
        var isExpanded          = nodeElement.getAttribute('IsExpanded');
        var wordWrap            = nodeElement.getAttribute('WordWrap');        
        var isSelected          = nodeElement.getAttribute('IsSelected');

        if(text != void 0 && typeof(text) == 'string' && text.length > 0) 
            treeNode.SetText(text);
        
        if(value != void 0 && typeof(value) == 'string' && value.length > 0) 
            treeNode.SetValue(value);
        
        if(allowKeyNavigation != void 0 && typeof(allowKeyNavigation) == 'string' && allowKeyNavigation.length > 0)
            treeNode.AllowKeyNavigation = allowKeyNavigation.toLowerCase() == 'true' ? true  : false;
        
        if(href != void 0 && typeof(href) == 'string' && href.length > 0) 
            treeNode.SetNavigationURL(href);
       
        if(target != void 0 && typeof(target) == 'string' && target.length > 0) 
            treeNode.SetTarget(target);
                   
        treeNode.SetImageURL(collapsedImageURL, expandedImageURL);

        if(wordWrap != void 0 && typeof(wordWrap) == 'string' && wordWrap.length > 0) 
            treeNode.WordWrap(wordWrap.toLowerCase() == 'true');               

        if(isSelected != void 0 && typeof(isSelected) == 'string' && isSelected.toLowerCase() == 'true') 
            treeNode.Select(false);
        
        if(isExpanded != void 0 && typeof(isExpanded) == 'string' && isExpanded.toLowerCase() == 'true') 
            treeNode.Expanded();

        if(nodeElement.childNodes.length > 0){
            for(var index = 0; index <= nodeElement.childNodes.length-1; index++){
                var _treeNode = TreeNode.ParseXML(nodeElement.childNodes[index]);
                if(_treeNode != void 0) treeNode.ChildNodes.Add(_treeNode);
            };
        };
        
        return treeNode;
    };

    return null;
};

if(!window.XMLHttpRequest){
    window.XMLHttpRequest = function(){

        var xmlHttp = null;   
        
        var clsids = ["Msxml2.XMLHTTP.4.0","MSXML2.XMLHTTP","Microsoft.XMLHTTP"];
        for(var i=0;i<clsids.length && xmlHttp == null;i++)
        {
            try
            {
                xmlHttp = new ActiveXObject(clsids[i]);
            }
            catch(e)
            {
            };
        };        
        
        if(xmlHttp == void 0){
            if(window.createRequest != void 0)
                xmlHttp = createRequest();
        };

        return xmlHttp;
    };
};

