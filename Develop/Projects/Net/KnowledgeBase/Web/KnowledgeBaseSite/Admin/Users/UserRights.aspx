<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="UserRights.aspx.cs" Inherits="Users_UserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage runat="server" ID="header" Text="Add User" />
    <KB:TabControl runat="server" ID="tabs" Width="400" Height="400">
        <KB:TabPanel runat="server" ID="tabAdmin" PanelTitle="Admin">
            <div class="pageScroll" style="height: 380px;">
                <KB:TreeViewCategory runat="server" ID="treeAdmin" ShowCheckBoxes="All" LazyLoad="false" />
            </div>
        </KB:TabPanel>
        <KB:TabPanel runat="server" ID="tabModerate" PanelTitle="Moderate">
            <div class="pageScroll" style="height: 380px;">
                <KB:TreeViewCategory runat="server" ID="treeModerate" ShowCheckBoxes="All" LazyLoad="false" />
            </div>
        </KB:TabPanel>
        <KB:TabPanel runat="server" ID="tabPublish" PanelTitle="Publish">
            <div class="pageScroll" style="height: 380px;">
                <KB:TreeViewCategory runat="server" ID="treePublish" ShowCheckBoxes="All" LazyLoad="false" />
            </div>
        </KB:TabPanel>
        <KB:TabPanel runat="server" ID="tabView" PanelTitle="View">
            <div class="pageScroll" style="height: 380px;">
                <KB:TreeViewCategory runat="server" ID="treeView" ShowCheckBoxes="All" LazyLoad="false" />
            </div>
        </KB:TabPanel>
    </KB:TabControl>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <KB:SaveButton ID="buttonSave" runat="server" OnClick="buttonSave_Click" /><KB:CancelButton
        ID="buttonCancel" runat="server" OnClick="ButtonCancelClick" />
</asp:Content>
