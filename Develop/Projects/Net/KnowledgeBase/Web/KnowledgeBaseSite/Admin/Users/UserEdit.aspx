<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="Users_UserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage runat="server" ID="header" Text="Add User" />
    <table class="f">
        <tr>
            <td class="fl">
                Login:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxLogin" runat="server" SkinID="Full" Wrap="False" PropertyName="Login"></KB:TextBoxEx>
            </td>
            <td>
                <KB:AdvancedValidator runat="server" ID="val1" ControlToValidate="textBoxLogin" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                First Name:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxFirstName" runat="server" SkinID="Full" Wrap="False" PropertyName="FirstName"></KB:TextBoxEx>
            </td>
            <td>
                <KB:AdvancedValidator runat="server" ID="AdvancedValidator2" ControlToValidate="textBoxFirstName" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                Last Name:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxLastName" runat="server" SkinID="Full" Wrap="False" PropertyName="LastName"></KB:TextBoxEx>
            </td>
            <td>
                <KB:AdvancedValidator runat="server" ID="AdvancedValidator3" ControlToValidate="textBoxLastName" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                Email:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxEmail" runat="server" SkinID="Full" Wrap="False" PropertyName="Email" />
            </td>
            <td>
                <KB:AdvancedValidator runat="server" ID="AdvancedValidator1" ControlToValidate="textBoxEmail" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                Password:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxPassword" runat="server" SkinID="Full" Wrap="False" TextMode="Password" />
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please confirm password valid."
                    Text="*" ControlToValidate="textBoxPassword" ControlToCompare="textBoxConfirmPassword" />
                <KB:ProxyValidator runat="server" ID="valProxy" PropertyName="Password" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                Confirm Password:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxConfirmPassword" runat="server" SkinID="Full" Wrap="False"
                    TextMode="Password" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fl">
                Security Question:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxSecurityQuestion" runat="server" SkinID="Full" Wrap="False"
                    PropertyName="SecurityQuestion" />
            </td>
            <td>
                <KB:AdvancedValidator runat="server" ID="AdvancedValidator4" ControlToValidate="textBoxSecurityQuestion" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                Security Answer:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxSecurityAnswer" runat="server" SkinID="Full" Wrap="False"
                    PropertyName="SecurityAnswer" />
            </td>
            <td>
                <KB:AdvancedValidator runat="server" ID="AdvancedValidator5" ControlToValidate="textBoxSecurityAnswer" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <KB:SaveButton ID="buttonSave" runat="server" OnClick="ButtonSaveClick" /><KB:CancelButton
        ID="buttonCancel" runat="server" OnClick="ButtonCancelClick" />
</asp:Content>
