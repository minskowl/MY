<%@ WebHandler Language="C#" Class="File" %>

using System;
using System.Web;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.IO;
using Savchin.Web;

public class File : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        string id = context.Request.QueryString["ID"];
        if (string.IsNullOrEmpty(id))
        {
            ShowMessage(context, "Invalid url format");
            return;
        }
        Guid PublicID;
        try
        {
            PublicID = new Guid(id);
        }
        catch
        {
            ShowMessage(context, "Invalid url format");
            return;
        }

        FileLink fileLink = KbContext.CurrentKb.ManagerFileLink.GetByPublicID(PublicID);
        if (fileLink == null)
        {
            ShowMessage(context, "File not exists");
            return;
        }

        string filePath = fileLink.FullPath;
        if (!System.IO.File.Exists(filePath))
        {
            ShowMessage(context, "File not exists");
            return;
        }

        context.SendFile(ResponseTransfer.SendMode.Inline ,filePath);
    }

    public bool IsReusable
    {
        get { return false; }
    }

    private void ShowMessage(HttpContext context, string message)
    {
        context.Response.ContentType = Mime.MimeTypeText;
        context.Response.Write(message);
    }
}