<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<%@ Register src="~/Controls/SearchFilter.ascx" tagname="SearchFilter" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
    <KB:HeaderPage runat="server" ID="h">Search</KB:HeaderPage><br/>
    <uc1:SearchFilter ID="filter" runat="server" />
    <br/>
    <asp:Panel runat="server" ID="panelItems" Visible="false">
        <KB:HeaderParagraph ID="headerItems" runat="server" Text="Items: " />
        <KB:GridViewEx ID="gridKnowledges" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="50" SkinID="view" DataSourceID="dataSourceItems" DataKeyNames="KnowledgeID"
            EmptyDataText="No items">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="KnowledgeTypeName" HeaderText="Type" SortExpression="KnowledgeTypeID">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="KnowledgeStatusName" HeaderText="Status" SortExpression="KnowledgeStatusID">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>                
                <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" SortExpression="CreationDate"
                    DataFormatString="{0:d}" HtmlEncode="False">
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <KB:ViewButton ID="buttonViewKnowledge" runat="server" OnClick="buttonViewKnowledge_Click" />
                        <KB:EditButton runat="server" ID="buttonEditKnowledge" OnClick="buttonEditKnowledge_Click" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </KB:GridViewEx>
        <KB:ControlDataSource ID="dataSourceItems" runat="server" SelectMethod="GetItemsData" />
   
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <KB:ButtonEx runat="server" ID="buttonSearch" Mode="Button" Text="Search" OnClick="buttonSearch_Click" />
</asp:Content>
