<%@ Import Namespace="KnowledgeBase.SiteCore" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="KnowledgeView.ascx.cs"
    Inherits="Controls_KnowledgeView" %>
<table>
    <tr>
        <td>
            <KB:HeaderPage ID="HeaderPage1" runat="server" Text="Category: "></KB:HeaderPage>
        </td>
        <td style="width: 32px;">
        </td>
        <td>
        </td>
    </tr>
</table>
<KB:HeaderParagraph runat="server" ID="labelType" />
<asp:Label runat="server" ID="labelSummary" />
<KB:HeaderItem runat="server" ID="labelKeywords" />
<KB:Window ID="WindowCode" runat="server" Height="350px" Width="450px" Hide="true"  Resizeble="true" EnableViewState="false" Left="10%" Top="10%">
    <div class="code" style="overflow: auto; overflow-y: auto;	overflow-x: auto; width: 95%; height: 90%; ">
        <pre><code id="fileViewer"></code>
        </pre>
    </div>
</KB:Window>

<script language="javascript" type="text/javascript">

    function InitLinks() {
        var summary = $('<%= labelSummary.ClientID %>');
        var links = summary.getElementsBySelector('a')
        for (var i = 0; i < links.length; i++) {

            var l = links[i];
            if (l.href.indexOf("<%= AppSettings.FileBaseUrl %>") != -1) 
            {
                l.title="Download file";
                
                var viewLink = $(document.createElement("A"));
                viewLink.href = "#";
                viewLink.innerHtml = "View";
                viewLink.title="View file content";

                var img = document.createElement("IMG");
                img.src = "<%= AppSettings.ApplicationImagesUrl %>search_16.gif";
                img.border=0;
                viewLink.appendChild(img);

                summary.insert(viewLink, { after: l });
                
                viewLink.observe("click", function(){showFile(l.href);});
            }
            
        }
    }


    function showFile(url) 
    {
        <%= WindowCode.JScriptShow %>
        new Ajax.Request(url, {   method: 'get',   onSuccess: function(transport) 
                {  
                  var viewer=$('fileViewer');
                  if(firefox)
                   viewer.textContent= transport.responseText;
                 else 
                   viewer.innerText= transport.responseText;   
                  
                  hljs.highlightBlock(viewer);
                    
                  }
          } ); 
    }

    Event.observe(window, 'load', InitLinks);
</script>

