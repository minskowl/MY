<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="KeywordEdit.aspx.cs" Inherits="Keywords_KeywordEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage runat="server" ID="header" Text="Add User" />
    <table class="f">
        <tr>
            <td class="fl">
                Name:
            </td>
            <td class="fv">
                <KB:TextBoxEx ID="textBoxName" runat="server" SkinID="Full" Wrap="False" PropertyName="Name"></KB:TextBoxEx>
            </td>
            <td>
                <KB:AdvancedValidator runat="server" ID="val1" ControlToValidate="textBoxName" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                Status:
            </td>
            <td class="fv">
                <KB:KeywordStatusDropDownList ID="listStatus" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fl">
                Status:
            </td>
            <td class="fv">
                <KB:KeywordTypeDropDownList ID="listType" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fl">
                Created:
            </td>
            <td class="fv">
                <KB:LabelEx ID="label" runat="server" PropertyName="CreationDate" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <KB:SaveButton ID="buttonSave" runat="server" OnClick="buttonSave_Click" /><KB:CancelButton
        ID="buttonCancel" runat="server" OnClick="ButtonCancelClick" />
</asp:Content>
