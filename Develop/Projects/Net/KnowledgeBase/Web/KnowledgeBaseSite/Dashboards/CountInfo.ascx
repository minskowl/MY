<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountInfo.ascx.cs" Inherits="Dashboards_CountInfo" %>
<table class="lstBody" cellpadding="0" cellspacing="0">
    <tr>
        <td class="lstHeaderCell">
            Status
        </td>
        <td class="lstHeaderCell">
            Count
        </td>
        <td class="lstHeaderCell">
            %
        </td>
    </tr>
    <tr class="lstRow">
        <td class="lstCell">
            <b>Total</b>
        </td>
        <td class="lstCell">
            <b>
                <asp:Label ID="TotalCountLabel" runat="server" Text="0"></asp:Label></b></td>
        <td class="lstCell">
            <b>100%</b></td>
    </tr>
    <tr class="lstAltRow">
        <td class="lstCell">
            Imported / Added
        </td>
        <td class="lstCell">
            <asp:Label ID="AddedCountLabel" runat="server" Text="0"></asp:Label></td>
        <td class="lstCell">
            <asp:Label ID="AddedPercentLabel" runat="server" Text="0"></asp:Label>%</td>
    </tr>
    <tr class="lstRow">
        <td class="lstCell">
            Confirmed (double-opted in)
        </td>
        <td class="lstCell">
            <asp:Label ID="ConfirmedCountLabel" runat="server" Text="0"></asp:Label></td>
        <td class="lstCell">
            <asp:Label ID="ConfirmedPerLabel" runat="server" Text="0"></asp:Label>%
        </td>
    </tr>
    <tr class="lstAltRow">
        <td class="lstCell">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>Subtotal</i>
        </td>
        <td class="lstCell">
            <i>
                <asp:Label ID="SubtotalCountLabel" runat="server" Text="0"></asp:Label></i></td>
        <td class="lstCell">
            <i>
                <asp:Label ID="SubtotalPerLabel" runat="server" Text="0"></asp:Label>%</i>
        </td>
    </tr>
    <tr class="lstRow">
        <td class="lstCell" colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr class="lstAltRow">
        <td class="lstCell">
            *Declined
        </td>
        <td class="lstCell">
            <asp:Label ID="DeclinedCountLabel" runat="server" Text="0"></asp:Label></td>
        <td class="lstCell">
            <asp:Label ID="DeclinedPerLabel" runat="server" Text="0"></asp:Label>%
        </td>
    </tr>
    <tr class="lstRow">
        <td class="lstCell">
            *Unsubscribed by Contact (permanent)
        </td>
        <td class="lstCell">
            <asp:Label ID="UnsubscribedCountLabel" runat="server" Text="0"></asp:Label></td>
        <td class="lstCell">
            <asp:Label ID="UnsubscribedPerLabel" runat="server" Text="0"></asp:Label>%
        </td>
    </tr>
    <tr class="lstAltRow">
        <td class="lstCell">
            *Deactivated (Can re-activate)
        </td>
        <td class="lstCell">
            <asp:Label ID="WAMUnsubscribedCountLabel" runat="server" Text="0"></asp:Label></td>
        <td class="lstCell">
            <asp:Label ID="WAMUnsubscribedPerLabel" runat="server" Text="0"></asp:Label>%
        </td>
    </tr>
    <tr class="lstRow">
        <td class="lstCell">
            *No Email
        </td>
        <td class="lstCell">
            <asp:Label ID="NoEmailCountLabel" runat="server" Text="0"></asp:Label></td>
        <td class="lstCell">
            <asp:Label ID="NoEmailPerLabel" runat="server" Text="0"></asp:Label>%
        </td>
    </tr>
</table>