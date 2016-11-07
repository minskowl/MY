<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="UserArea_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    В данном разделе вы можете:<br />
    <ul>
        <li>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/UserArea/Licenses.aspx">Управлять лицензиями</asp:HyperLink></li></ul>
</asp:Content>
