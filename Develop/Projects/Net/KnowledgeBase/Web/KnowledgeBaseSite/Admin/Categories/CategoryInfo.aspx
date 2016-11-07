<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryInfo.aspx.cs" Inherits="CategoryInfo"
    MasterPageFile="~/MasterPages/ScrollFormMasterPage.master" %>
<%@ Import Namespace="KnowledgeBase.SiteCore"%>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder">
    <table>
        <tr>
            <td>
                <KB:HeaderPage ID="HeaderPage1" runat="server" Text="Category: "></KB:HeaderPage>
            </td>
            <td style="width: 32px;">
                <KB:ButtonEx runat="server" ID="buttonUp" Mode="Link" ImageUrl="/images/upLevel.gif"
                    UseSubmitBehavior="false" />
            </td>
            <td>
                <KB:ButtonEx runat="server" ID="buttonMove" Mode="Link" ImageUrl="/images/move.gif"
                    UseSubmitBehavior="false" />
            </td>
        </tr>
    </table>
    <KB:GridViewEx ID="gridSubCategories" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        PageSize="50" SkinID="view" DataSourceID="dsSubCategories" DataKeyNames="CategoryID"
        EmptyDataText="No SubCategories">
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
                    <KB:ViewButton ID="buttonViewCategory" runat="server" OnClick="ButtonViewCategory_Click"
                        OnDataBinding="buttonViewCategory_DataBinding" />
                    <KB:EditButton runat="server" ID="buttonEdit" OnClick="buttonEdit_Click" OnDataBinding="buttonEdit_DataBinding" />
                </ItemTemplate>
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </KB:GridViewEx>
    <KB:AddButton runat="server" ID="buttonAdd" Text="Add sub category" OnClick="ButtonAddClick" />
    <KB:ControlDataSource ID="dsSubCategories" runat="server" SelectMethod="GetData" />
    <asp:Panel runat="server" ID="panelItems">
        <KB:HeaderParagraph ID="HeaderPage2" runat="server" Text="Items: " />
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
                        <KB:ButtonEx runat="server" ID="buttonUrl" Mode="Link" ToolTip="Clcick to copy Public URL"
                            UseSubmitBehavior="false" ImageUrl='<%# ImagePathProvider.UrlImage %>' OnClientClick='<%# "showUrl(\""+ Redirector.GetKnowledgeInfoPublicPage((Guid)Eval("PublicID")) +"\");" %>' />
                        <KB:DeleteButton runat="server" ID="buttonDeleteKnowledge" OnClick="buttonDeleteKnowledge_Click" />
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </KB:GridViewEx>
        <KB:ControlDataSource ID="dataSourceItems" runat="server" SelectMethod="GetItemsData" />
        <KB:AddButton runat="server" ID="buttonAddItem" Text="Add item" OnClick="buttonAddItem_Click" />
        <KB:Window runat="server" ID="windowUrl" Title="Public Knowledge URL" Width="300px"
            Height="80px" HorizontalAlign="Center" Hide="true" Left="30%" Top="30%">
            <br />
            <input type="text" id="textBoxUrl" style="width: 280px;" /><br />
            <KB:ButtonEx runat="server" ID="buttonWindowUrlClose" CausesValidation="false" UseSubmitBehavior="false"
                Text="Close" />
        </KB:Window>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function showUrl(url) { 
        $('textBoxUrl').value=url;
        <%= windowUrl.JScriptShow %>
        }
    </script>

</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder">
    <KB:DeleteButton runat="server" ID="buttonDelete" OnClick="buttonDelete_Click" />
</asp:Content>
