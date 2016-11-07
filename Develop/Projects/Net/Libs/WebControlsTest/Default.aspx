<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

   <sav:ButtonEx runat="server" Mode="InputButton" Text="Test" ID="button" OnClick="buttonOnClick" 
   OnClientClick="alert('hi');" ValidationGroup="TestGroup" />
    <br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBox1" ValidationGroup="TestGroup"></asp:RequiredFieldValidator>
</asp:Content>
