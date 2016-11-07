<%@ Import Namespace="KnowledgeBase.SiteCore"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchResultsView.ascx.cs"
    Inherits="Controls_SearchResultsView" %>
<%@ Reference Control="~/Controls/SearchFilter.ascx" %>
<KB:GridViewEx ID="gridKnowledges" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    PageSize="50" SkinID="view" DataSourceID="dataSourceItems" DataKeyNames="KnowledgeID"
    EmptyDataText="No items" EnableViewState="false">
    <Columns>
        <asp:BoundField DataField="CategoryName" HeaderText="Category" SortExpression="CategoryName" />   
        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
        <asp:BoundField DataField="KnowledgeTypeName" HeaderText="Type" SortExpression="KnowledgeTypeID">
            <HeaderStyle Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" SortExpression="CreationDate"
            DataFormatString="{0:d}" HtmlEncode="False">
            <HeaderStyle Width="90px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="KeywordRank" HeaderText="Rank" SortExpression="KeywordRank">
            <HeaderStyle Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>        
        
        <asp:TemplateField>
            <ItemTemplate>
                <KB:ViewButton ID="buttonViewKnowledge" runat="server" UseSubmitBehavior="false"
                    NavigateUrl='<%#  Redirector.GetKnowledgeInfoPublicPage((Guid)Eval("PublicID")) %>'
                    Target="_blank" />
            </ItemTemplate>
            <HeaderStyle Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
</KB:GridViewEx>
<KB:ControlDataSource ID="dataSourceItems" runat="server" SelectMethod="SearchItemsData" />
