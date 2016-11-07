<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs"
 Inherits="Login" MasterPageFile="~/MasterPages/SimpleMasterPage.master" Title="Knowledge Base Login" %>
<asp:Content runat="server" ContentPlaceHolderID="content" ID="c1">
    <table style="width: 100%; height: 100%">
        <tr>
            <td align="center" valign="middle">
                <asp:Login ID="Login1" runat="server" onloggedin="Login1_LoggedIn">
                </asp:Login>
            </td>
        </tr>
    </table>
</asp:Content>
