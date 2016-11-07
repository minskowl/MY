<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Keywords_Default" %>

<%@ Import Namespace="KnowledgeBase.BussinesLayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage runat="server" ID="header" Text="Manage Keywords" />
    <table style="width: 100%;">
        <tr>
            <td>
                <KB:AddButton runat="server" ID="buttonAddKeyword" Text="Add Keyword" OnClick="ButtonAddKeywordClick" />
            </td>
            <td align="right">
                <KB:DropPanelButton runat="server" ID="buttonFilter" Button-Type="Link" Button-ImageUrl="/images/filter_24.png"
                    Button-Text="Filter" Panel-Width="150px">
                    <table>
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
                    </table>
                    <KB:ButtonEx runat="server" ImageAlign="NotSet" Mode="Link" Text="Filter" UseSubmitBehavior="False"
                        NavigateUrl="" Target="" ID="buttonFilterButton" ImageUrl="/images/filter_24.png">
                    </KB:ButtonEx>
                    <asp:Panel runat="server" CssClass="DropPanelButton" Height="30px" Width="150px"
                        ID="buttonFilterPanel">
                    </asp:Panel>
                </KB:DropPanelButton>
            </td>
        </tr>
    </table>
    <KB:GridViewEx ID="griKeywords" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        PageSize="50" SkinID="view" DataSourceID="dsKeywords" DataKeyNames="KeywordID"
        EmptyDataText="No Keywords" AutoSortOrder="false">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="KeywordTypeName" HeaderText="Type" SortExpression="KeywordTypeID">
                <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="KeywordStatusName" HeaderText="Status" SortExpression="KeywordStatusID">
                <HeaderStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" SortExpression="CreationDate"
                DataFormatString="{0:d}" HtmlEncode="False">
                <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <KB:EditButton runat="server" ID="buttonEdit" OnClick="ButtonEditClick" />
                    <KB:ButtonEx runat="server" ID="buttonApprove" Mode="Link" ImageUrl='<%# ImagePathProvider.OkImage %>'
                        OnClick="ButtonApproveClick" Visible='<%# ((KeywordStatus) Eval("KeywordStatusID")) == KeywordStatus.New %>' />
                </ItemTemplate>
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </KB:GridViewEx>
    <KB:ControlDataSource ID="dsKeywords" runat="server" SelectMethod="GetData" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
</asp:Content>
