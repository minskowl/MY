<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true"
    CodeFile="Licenses.aspx.cs" Inherits="UserArea_Licenses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="contentbox">
        <bot:HeaderPage runat="server" Text="Лицензии" /><br />
        <bot:AddButton runat="server" Text="Создать новую" />
        <bot:GridViewEx runat="server" ID="grid" EmptyDataText="У вас пока не лицензий" DataSourceID="source"
            SkinID="view" DataKeyNames="InstrumentID">
            <Columns>
                <asp:BoundField HeaderText="Продукт" SortExpression="Name" DataField="Name" />
                <asp:BoundField HeaderText="Версия" SortExpression="Version" DataField="Version" />
                <asp:BoundField HeaderText="Кол-во" SortExpression="Count" DataField="Count" />
                <asp:BoundField HeaderText="Создана" SortExpression="CreationDate" DataField="CreationDate"
                    DataFormatString="{0:d}" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </bot:GridViewEx>
        <bot:ControlDataSource runat="server" ID="source" SelectMethod="GetData" />
        <bot:Window runat="server" Height="250" Width="340" Title="Регистрация пожертвования">
            <table>
                <td>
                    Продукт
                </td>
                <td>
                    <bot:ProductList runat="server" ID="listProducts" />
                </td>
                <tr>
                    <td>
                        Номер кошелька
                    </td>
                    <td>
                        <bot:TextBoxEx runat="server" ID="boxPurse" />
                        <asp:RequiredFieldValidator runat="server" ID="val1" ControlToValidate="boxPurse"
                            Text="Введите номер кошелька с которого был сделан перевод." />
                    </td>
                </tr>
                <tr>
                    <td>
                        Дата пожервования
                    </td>
                    <td>
                        <bot:CalendarControl runat="server" Mode="EditBox" ID="boxDate" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="boxDate"
                            Text="Введите дату перевода." />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <bot:ButtonEx runat="server" ID="buttonSearchTransaction" Text="Искать" OnClick="buttonSearchTransactionOnClick" />
                    </td>
                </tr>
            </table>
        </bot:Window>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
