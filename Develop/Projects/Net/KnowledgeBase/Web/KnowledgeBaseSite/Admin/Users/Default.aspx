<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Users_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage runat="server" ID="header" Text="Manage Users" />
    <KB:AddButton runat="server" ID="buttonAddUser" Text="Add User" OnClick="ButtonAddUserClick" />
    <KB:GridViewEx ID="gridUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        PageSize="50" SkinID="view" DataSourceID="dsUsers" DataKeyNames="UserID" EmptyDataText="No Users">
        <Columns>
            <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="IsSystem" HeaderText="System" SortExpression="IsSystem">
                <HeaderStyle Width="30px" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <KB:EditButton runat="server" ID="buttonEdit" OnClick="ButtonEditClick" />
                    <KB:ButtonEx runat="server" ID="buttonRights" Mode="Link" OnInit="ButtonRightsInit"
                        OnClick="ButtonRightsClick" />
                </ItemTemplate>
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </KB:GridViewEx>
    <KB:ControlDataSource ID="dsUsers" runat="server" SelectMethod="GetData" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
</asp:Content>
