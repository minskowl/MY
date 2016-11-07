<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HeaderPage.aspx.cs" Inherits="HeaderPage"
    MasterPageFile="~/MasterPages/SimpleMasterPage.master" %>

<%@ Import Namespace="KnowledgeBase.SiteCore" %>
<asp:Content runat="server" ContentPlaceHolderID="toolbar" ID="C1">
    <td style="width: 60px;">
        <KB:ButtonEx runat="server" ID="buttonEditUser" Mode="Link" Text="User" UseSubmitBehavior="false"
            ForeColor="Black" ToolTip="Click to edit prifile" />
    </td>
    <td style="width: 60px;">
        <KB:ButtonEx runat="server" ID="buttonLogout" Mode="Link" Text="Logout" OnClick="buttonLogout_Click"
            ForeColor="Black" ToolTip="Click to logout from KnowledgeBase" />
    </td>
    <td align="right" style="width: 280px;">
        <KB:ButtonEx runat="server" ID="buttonSearch" Mode="Link" Text="Search" OnClick="buttonSearch_Click"
            ForeColor="Black" />
        <asp:TextBox ID="textBoxCategoryMatcher" runat="server"></asp:TextBox>
        <KB:ButtonEx runat="server" ID="buttonGotoCategory" Mode="Link" ImageUrl="/images/move.gif"
            ToolTip="Go to category" OnClick="buttonGotoCategory_Click" />
    </td>
</asp:Content>
