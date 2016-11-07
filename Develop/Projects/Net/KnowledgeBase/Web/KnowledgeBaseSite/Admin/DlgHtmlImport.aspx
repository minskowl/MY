<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DlgHtmlImport.aspx.cs" Inherits="Admin_DlgHtmlImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Html Import</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div>
        <fieldset style="width: 214px; height: 192px">
            Import html from<br />
            URL:
            <input type="text" id="textBoxUrl"><br />
            <input type="button" onclick="javascript:importHtml();" value="Import" />
        </fieldset>

        <script type="text/javascript">
     function getRadWindow()
    {
           if (window.radWindow)
        {
            return window.radWindow;
        }
        if (window.frameElement && window.frameElement.radWindow)
        {
            return window.frameElement.radWindow;
        }
        return null;
    }
    
    function importHtml()
    {
        PageMethods.GetHtml($F('textBoxUrl'), OnGetHtmlComplete);
    }

  
    function OnGetHtmlComplete(result)
    {
        getRadWindow().close(result);  
    }
        </script>

    </div>
    </form>
</body>
</html>
