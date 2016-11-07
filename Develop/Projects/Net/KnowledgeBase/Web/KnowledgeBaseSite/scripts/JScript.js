function copyText(text) {

    if (window.clipboardData)
    { window.clipboardData.setData('Text', text); }
    else {
    /*
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        str.data = text;*/
        
       var h = Components.classes["@mozilla.org/widget/clipboardhelper;1"].getService(Components.interfaces.nsIClipboardHelper);
       h.copyString(text); 
    }

}