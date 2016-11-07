<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginControl.ascx.cs"
    Inherits="UserControls_LoginControl" %>
<asp:Login ID="Login1" runat="server" OnLoggedIn="Login1_LoggedIn" Width="100%" Height="100%">
    <LayoutTemplate>
        <table style="height: 100%; width: 100%;">
            <tr>
                <td>
                </td>
                <td class="loginPanelText">
                    <bot:HeaderPage ID="HeaderPage1" runat="server" Text="Войти" ForeColor="White" />
                </td>
                <td style="width: 10px;">
                </td>
            </tr>
            <tr class="loginPanelText">
                <td align="right">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" ForeColor="White">Вход</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="UserName" runat="server" SkinID="Full"></asp:TextBox>
                </td>
                <td style="width: 8px;">
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="loginPanelText">
                <td align="right">
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" ForeColor="White">Пароль</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" SkinID="Full"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <asp:HyperLink ID="HyperLinkForgotPassword" runat="server" ForeColor="White">Забыли пароль</asp:HyperLink>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:CheckBox ID="RememberMe" runat="server" Text="Запомнить меня" ForeColor="White" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Label ID="FailureText" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Войти" ValidationGroup="Login1"
                        Width="81px" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:Login>
