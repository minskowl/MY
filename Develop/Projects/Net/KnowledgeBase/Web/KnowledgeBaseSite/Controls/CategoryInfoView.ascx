<%@ Import Namespace="KnowledgeBase.SiteCore"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryInfoView.ascx.cs"
    Inherits="Controls_CategoryInfoView" %>
<KB:HeaderPage ID="header" runat="server" Text="" />
<KB:HeaderParagraph ID="HeaderParagraph1" runat="server" Text="SubCategories: " />
<KB:GridViewEx ID="gridSubCategories" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    PageSize="50" SkinID="view" DataSourceID="dsSubCategories" DataKeyNames="CategoryID"
    EmptyDataText="No SubCategories" EnableViewState="false">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
        <asp:BoundField DataField="cntSubCategories" HeaderText="Sub Categories" SortExpression="cntSubCategories">
            <HeaderStyle Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="cntKnowledges" HeaderText="Knowledges" SortExpression="cntKnowledges">
            <HeaderStyle Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField>
            <ItemTemplate>
                <KB:ViewButton ID="buttonViewCategory" runat="server" CausesValidation="false" UseSubmitBehavior="false"
                    OnClientClick='<%# "showCategory(\""+Eval("CategoryID")+"\");" %>' />
            </ItemTemplate>
            <HeaderStyle Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
</KB:GridViewEx>
<KB:ControlDataSource ID="dsSubCategories" runat="server" SelectMethod="GetData" />
<KB:HeaderParagraph ID="HeaderPage2" runat="server" Text="Items: " />
<KB:GridViewEx ID="gridKnowledges" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    PageSize="50" SkinID="view" DataSourceID="dataSourceItems" DataKeyNames="KnowledgeID"
    EmptyDataText="No items" EnableViewState="false">
    <Columns>
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
<KB:ControlDataSource ID="dataSourceItems" runat="server" SelectMethod="GetItemsData" />
