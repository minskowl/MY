<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchFilter.ascx.cs"
    Inherits="Controls_SearchFilter" %>
<table>
    <tr>
        <td>
            <KB:HeaderItem runat="server" ID="HeaderControl1" Text="In:" />
        </td>
        <td>
            <KB:TreeViewCategory runat="server" ID="listCaregories" ShowCheckBoxes="All" />
        </td>
    </tr>
    <tr>
        <td>
            <KB:HeaderItem runat="server" ID="HeaderControl2" Text="Type:" />
        </td>
        <td>
            <KB:KnowledgeTypeCheckCombo runat="server" ID="listTypess" />
        </td>
    </tr>
    <tr>
        <td>
            <KB:HeaderItem runat="server" ID="HeaderItem3" Text="Status:" />
        </td>
        <td>
            <KB:KeywordStatusCheckCombo runat="server" ID="listStatuses" />
        </td>
    </tr>    
    <tr>
        <td>
            <KB:HeaderItem runat="server" ID="HeaderItem1" Text="Text:" />
        </td>
        <td>
            <asp:TextBox runat="server" ID="textBoxText" />
        </td>
    </tr>
    <tr>
        <td>
            <KB:HeaderItem runat="server" ID="HeaderItem2" Text="Keywords:" />
        </td>
        <td>
            <KB:KeywordListControl runat="server" ID="listKeywords" AllowCustomText="false" />
        </td>
    </tr>
</table>
