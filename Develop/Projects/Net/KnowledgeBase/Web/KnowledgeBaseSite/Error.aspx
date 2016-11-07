<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopupMasterPage.master"
    AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" Title="Untitled Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ViewDevelop" runat="server">
            <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                <tr>
                    <td valign="top">
                        <br />
                        <KB:HeaderPage ID="ErrorPageHeader" runat="server">Error on page</KB:HeaderPage>
                        <br />
                        <asp:Label ID="Message" runat="server" Text="Label"></asp:Label>
                        <br />
                        <asp:Panel ID="RealExceptionPanel" runat="server" Width="100%" Visible="False">
                            <br />
                            <br />
                            <KB:HeaderParagraph ID="HeaderParagraph1" runat="server">Real Exception</KB:HeaderParagraph>
                            <asp:TextBox ID="ExceptionTextBox" runat="server" Rows="25" TextMode="MultiLine"
                                SkinID="Custom" Width="100%"></asp:TextBox></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td valign="bottom">
                        <table class="frm">
                            <tr>
                                <td class="frmButtons" colspan="2">
                                    <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="~/Default.aspx">Back</asp:HyperLink>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewProduction" runat="server">
            <KB:HeaderPage ID="ErrorPageHeader1" runat="server">Error on page</KB:HeaderPage></br>
            We’re very sorry, the system has experienced an unexpected issue. This can happy
            for many reasons, including the software itself, proxy/firewalls servers, Internet
            connections and many other reasons in or out of our control. </br>We do sincerely
            apologize for any inconvenience that may have occurred and our Customer Experience
            Team has automatically been notified of this issue so we can trouble shoot and correct
            any problems.</br> Please
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Click here</asp:HyperLink>
            to return to the Today Main Page.
        </asp:View>
    </asp:MultiView>
</asp:Content>
