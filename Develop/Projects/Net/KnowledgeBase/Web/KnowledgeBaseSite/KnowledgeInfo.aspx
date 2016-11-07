<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="KnowledgeInfo.aspx.cs"
    Inherits="Public_KnowledgeInfo" %>

<%@ Register Src="~/Controls/KnowledgeView.ascx" TagName="KnowledgeView" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
</head>
<body >
    <form id="form1" runat="server">
    <uc1:KnowledgeView ID="viewKnowledge" runat="server" />
    </form>
</body>
</html>
