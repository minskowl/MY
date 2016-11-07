<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownloadImages.aspx.cs" Inherits="Admin_DownloadImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download Images</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div>
        <fieldset style="width: 214px; height: 192px">
            Download all images from<br />
            URL:
            <input type="text" id="textBoxUrl"><br />
            <input type="button" onclick="javascript:downloadImages();" value="Download" />
            <div id="status">
            </div>
        </fieldset>
    </div>

    <script type="text/javascript">
        var content = null;
        var urls = null;
        var currentUrl = 0;
        var resultUrls = new Array();
        var baseUrl;

        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function downloadImages() {
            var re = new RegExp("(?:src=(?:\"|'))(?:[^'\"]+(?:\.gif|\.jpg|\.png))(?:'|\")", "ig");
            urls = content.match(re);
            baseUrl = $F('textBoxUrl').trimEndChars('/');

            if (urls && urls.length > 0)
                downloadUrl();
            else {
                alert("Not found external images.");
                getRadWindow().close(content);
            }

        }
        function downloadUrl() {
            var url = urls[currentUrl];
            var arr = (url.indexOf("'") == -1) ? url.split('"') : url.split("'");
            url = arr[1];

            var imageUrl = (url.startsWith("http://")) ? url : baseUrl + "/" + url.trimStartChars('/');
            $('status').update("Status: Start download image " + imageUrl);
            PageMethods.DownloadImage(imageUrl, OnGetHtmlComplete);
        }

        function OnGetHtmlComplete(result) {
            resultUrls[currentUrl] = result;
            currentUrl++;
            if (currentUrl < urls.length)
                downloadUrl();
            else {
                for (var i = 0; i < urls.length; i++) {
                    if (resultUrls[i] && resultUrls[i].length > 0)
                        content = content.replaceSimple(urls[i], ' src="'+resultUrls[i]+'" ');
                }

                getRadWindow().close(content);
            }
        }

        Event.observe(window, 'load', function() {
            content = getRadWindow().ClientParameters;
        }); 
    </script>

    </form>
</body>
</html>
