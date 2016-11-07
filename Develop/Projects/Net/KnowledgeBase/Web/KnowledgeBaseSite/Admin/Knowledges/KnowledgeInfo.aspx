<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="KnowledgeInfo.aspx.cs" Inherits="KnowledgeInfo" %>

<%@ Register src="~/Controls/KnowledgeView.ascx" tagname="KnowledgeView" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">

    <uc1:KnowledgeView ID="viewKnowledge" runat="server" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <KB:BackButton runat="server" ID="buttonBack" OnClick="buttonBack_OnClick" />
    <KB:EditButton runat="server" ID="buttonEdit" OnClick="buttonEdit_OnClick" Type="Button" Text="Edit" />
</asp:Content>
