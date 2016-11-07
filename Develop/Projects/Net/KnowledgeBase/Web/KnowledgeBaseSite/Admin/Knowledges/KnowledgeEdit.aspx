<%@ Page Language="C#" MasterPageFile="~/MasterPages/ScrollFormMasterPage.master"
    AutoEventWireup="true" CodeFile="KnowledgeEdit.aspx.cs" Inherits="KnowledgeEdit"
    Title="Untitled Page" %>
<%@ Import Namespace="KnowledgeBase.SiteCore"%>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="FormContentPlaceHolder" runat="Server">
<style>
.rade_toolbar.Sunset .HtmlImport
{
   background-image: url(<%=  AppSettings.ApplicationImagesUrl%>importHtml.gif);
}

.rade_toolbar.Sunset .DownloadImages
{
   background-image: url(<%=  AppSettings.ApplicationImagesUrl%>downloadImages.gif);
}
</style>

    <KB:HeaderPage runat="server" ID="header" Text="Knowledge Add" />
    <table class="f">
        <tr>
            <td class="fl">
                Title:
            </td>
            <td class="fv">
                <asp:TextBox ID="textBoxTitle" runat="server" SkinID="Full" Wrap="False"></asp:TextBox>
            </td>
            <td>
                <KB:AdvancedValidator runat="server" PropertyName="Title" ID="val1" ControlToValidate="textBoxTitle" />
            </td>
        </tr>
        <tr>
            <td class="fl">
                Category:
            </td>
            <td class="fv">
                <KB:CategoryDropDownList runat="server" ID="listCategory" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fl">
                Type:
            </td>
            <td class="fv">
                <KB:KnowledgeTypeDropDownList runat="server" ID="listKnowledgeType" Required="true" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fl">
                Status:
            </td>
            <td class="fv">
                <KB:KnowledgeStatusDropDownList runat="server" ID="listKnowledgeStatus" Required="true" />
            </td>
            <td>
            </td>
        </tr>        
        <tr>
            <td lass="fl">
                Keywords:
            </td>
            <td class="fv">
                <KB:KeywordListControl runat="server" ID="listKeywords" AllowCustomText="true">
                </KB:KeywordListControl>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td lass="fl">
                Summary:
            </td>
            <td class="fv">
            </td>
            <td>
                <asp:UpdatePanel runat="server" ID="keywords" RenderMode="Inline" UpdateMode="Always">
                    <ContentTemplate>
                        <KB:AdvancedValidator runat="server" PropertyName="Summary" ID="AdvancedValidator1"
                            ControlToValidate="textBoxSummary" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <telerik:radeditor id="textBoxSummary" runat="server" width="900" height="500" skin="Sunset"
        backcolor="White" ToolsFile="~/App_Data/ToolsData.xml">
        <Content></Content>
        <DocumentManager ViewPaths="D:/Develop,S:/Install" SearchPatterns="*.*" />
        <Languages >
            <telerik:SpellCheckerLanguage Code="en-US" Title="English" />
            <telerik:SpellCheckerLanguage Code="ru-RU" Title="Russian" />
        </Languages>
        <SpellCheckSettings  SpellCheckProvider="EditDistanceProvider" ></SpellCheckSettings>
    </telerik:radeditor>
    
     <script type="text/javascript">
Telerik.Web.UI.Editor.CommandList["HtmlImport"] = function(commandName, editor, args)
{

  
       var  myCallbackFunction = function(sender, args)
       {
           editor.pasteHtml(args);
       }
       
       editor.showExternalDialog(
            'DlgHtmlImport.aspx',
            null,
            270,
            300,
            myCallbackFunction,
            null,
            'Html Import',
            true,
            Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
            false,
            false);

};

Telerik.Web.UI.Editor.CommandList["DownloadImages"] = function(commandName, editor, args)
{

  
       var  myCallbackFunction = function(sender, args)
       {
           editor.set_html(args);
       }
       
       editor.showExternalDialog(
            'DownloadImages.aspx',
            editor.get_html(true),
            270,
            300,
            myCallbackFunction,
            null,
            'Html Import',
            true,
            Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
            false,
            false);

};
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td style="width: 400px;" align="left">
                <telerik:radspell id="RadSpell1" runat="server" controltocheck="" skin="Sunset" supportedlanguages="en-US,English,ru-RU,Russian"
                    isclientid="True" spellcheckprovider="EditDistanceProvider" />
            </td>
            <td>
                <KB:SaveButton ID="buttonSave" runat="server" OnClick="buttonSave_Click" /><KB:CancelButton
                    ID="buttonCancel" runat="server" OnClick="buttonCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
