<%@ Page Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="CategoryEdit.aspx.cs" Inherits="CategoryEdit"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage runat="server" ID="header" Text="Category Add" />
    <table class="f">
        <tr>
            <td class="fl">
                Name:
                <KB:HelpTip ID="HelpTip1" runat="server">
                    This will be the headline of your news item.
                </KB:HelpTip>
            </td>
            <td class="fv">
                <asp:TextBox ID="textBoxName" runat="server" SkinID="Full" Wrap="False" MaxLength="200"></asp:TextBox>
            </td>
            <td>
                <KB:AdvancedValidator runat="server" PropertyName="Name" ID="val1" ControlToValidate="textBoxName" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <KB:SaveButton runat="server" ID="buttonSave" OnClick="buttonSave_Click" />
    <KB:CancelButton runat="server" ID="buttonCancel" OnClick="buttonCancel_Click" />
</asp:Content>
