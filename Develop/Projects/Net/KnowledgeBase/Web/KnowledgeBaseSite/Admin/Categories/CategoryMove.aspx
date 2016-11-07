<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="CategoryMove.aspx.cs" Inherits="CategoryMove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage ID="headerPage1" runat="server" Text="Category: "></KB:HeaderPage>
    <KB:HeaderPage ID="headerPage2" runat="server" Text="Move to: "></KB:HeaderPage>
    <KB:TreeViewCategory runat="server" ID="listCategories" OnTreeNodeCreate="listCategories_TreeNodeCreate">
       
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
            VerticalPadding="0px" />
    </KB:TreeViewCategory>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <KB:OkButton ID="buttonOk" runat="server" OnClick="buttonOk_Click" /><KB:CancelButton
        ID="buttonCancel" runat="server" OnClick="buttonCancel_Click" />
</asp:Content>
