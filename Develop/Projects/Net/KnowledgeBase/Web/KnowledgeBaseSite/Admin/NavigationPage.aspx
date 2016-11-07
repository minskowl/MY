<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NavigationPage.aspx.cs" Inherits="NavigationPage"
    MasterPageFile="~/MasterPages/MainMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td align="right">
                <KB:ButtonEx runat="server" ID="buttonManageKeywords" Mode="Link" ToolTip="Manage Keywords"
                    UseSubmitBehavior="false" />
                <KB:ButtonEx runat="server" ID="buttonManageUsers" Mode="Link" ToolTip="Manage Users"
                    UseSubmitBehavior="false" />
                <KB:RefreshButton runat="server" ID="buttonRefresh" ToolTip="Refresh" UseSubmitBehavior="false" />
            </td>
        </tr>
        <tr>
            <td>
                <KB:TreeViewCategory runat="server" ID="listCategories" OnTreeNodeCreate="ListCategoriesTreeNodeCreate"
                    BackColor="White">
                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                </KB:TreeViewCategory>
            </td>
        </tr>
    </table>
</asp:Content>
