<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SimpleMasterPage.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Test_Default" %>

<%@ Register Src="~/Controls/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc1" %>
<%@ Register Src="Controls/CategoryInfoView.ascx" TagName="CategoryInfoView" TagPrefix="uc2" %>
<%@ Register Src="Controls/SearchResultsView.ascx" TagName="SearchResultsView" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="toolbar" runat="Server">
    <td>
        <KB:ButtonEx runat="Server" ID="buttonLogin" Mode="Link" Text="Login To Admin" UseSubmitBehavior="false"
            ForeColor="Black" />
    </td>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:UpdatePanel runat="server" ID="panelUpdate">
        <ContentTemplate>
            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="600" Width="1100">
                <telerik:RadPane ID="navigationPane" runat="server" Width="200">
                    <KB:TreeViewCategory runat="server" ID="listCategories" OnTreeNodeCreate="listCategories_TreeNodeCreate"
                        BackColor="White">
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                    </KB:TreeViewCategory>
                </telerik:RadPane>
                <telerik:RadSplitBar ID="RadSplitbar1" runat="server" CollapseMode="Forward"></telerik:RadSplitBar>
                <telerik:RadPane ID="contentPane" runat="server" Scrolling="none">
                    <uc2:CategoryInfoView ID="categoryInfoView" runat="server" />
                    <uc3:SearchResultsView ID="searchResultsView" runat="server" Visible="false" />
                    <asp:HiddenField runat="server" ID="categoryIDField" EnableViewState="false" />
                </telerik:RadPane>
                <telerik:RadPane ID="Radpane1" runat="server" Width="22" Scrolling="None" MinWidth="22">
                    <telerik:RadSlidingZone ID="SlidingZone1" runat="server" Width="22" ClickToOpen="true"
                        SlideDirection="Left">
                        <telerik:RadSlidingPane ID="paneSearch" Title="Search" runat="server" Width="300">
                            Search
                            <uc1:SearchFilter ID="filter" runat="server" />
                            <KB:ButtonEx runat="server" ID="buttonSearch" Text="Search" OnClick="buttonSearch_Click" />
                        </telerik:RadSlidingPane>
                    </telerik:RadSlidingZone>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function showCategory(id) {
            $('<%= categoryIDField.ClientID %>').value = id;
            __doPostBack("<%= panelUpdate.ClientID %>", "");

        }
    </script>

</asp:Content>
