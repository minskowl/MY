<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SimpleMasterPage.master"
    AutoEventWireup="true" CodeFile="Test2.aspx.cs" Inherits="Test_Test2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="toolbar" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <telerik:radeditor id="textBoxSummary" runat="server" width="900" height="500" skin="Sunset"
        backcolor="White">
        <Content>

</Content>
        <DocumentManager ViewPaths="D:/Projects" SearchPatterns="*.*" />
        <Languages >
            <telerik:SpellCheckerLanguage Code="en-US" Title="English" />
            <telerik:SpellCheckerLanguage Code="ru-RU" Title="Russian" />
        </Languages>
        <SpellCheckSettings  SpellCheckProvider="EditDistanceProvider" ></SpellCheckSettings>
    </telerik:radeditor>
</asp:Content>
