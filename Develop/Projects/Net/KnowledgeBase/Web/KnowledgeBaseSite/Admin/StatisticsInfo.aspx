<%@ Page Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="StatisticsInfo.aspx.cs" Inherits="StatisticsInfo" Title="Status" %>

<%@ Register src="~/Dashboards/CountInfo.ascx" tagname="CountInfo" tagprefix="dash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">

    <KB:DashboardManager runat="server" ID="manager" />
      <table width="100%" cellpadding="5" cellspacing="5">
        <tr>
            <td style="width: 50%;" valign="top">
                <kb:dragableboxcontrol runat="server" ID="dragableBoxesColumn1" Height="100%" 
                    Width="100%">
                       <dash:CountInfo ID="CountInfo1" runat="server" />
                </kb:dragableboxcontrol>
            </td>
            <td valign="top" style="width: 50%;">
                <kb:dragableboxcontrol runat="server" ID="dragableBoxesColumn3" Height="100%" 
                    Width="100%">
            
                </kb:dragableboxcontrol>
            </td>
        </tr>
    </table>
</asp:Content>

