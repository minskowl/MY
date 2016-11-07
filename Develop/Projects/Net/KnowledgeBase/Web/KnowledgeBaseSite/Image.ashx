<%@ WebHandler Language="C#" Class="Image" %>

using System;
using System.Web;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.IO;
using Savchin.Web;

public class Image : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string id = context.Request.QueryString["UserFileID"];
        if (!string.IsNullOrEmpty(id))
        {
            ShowUserFile(context, id);

            return;
        }
        id = context.Request.QueryString["ID"];
        if (!string.IsNullOrEmpty(id))
        {
            ShowFileInclude(context, id);

            return;
        }
         
        ShowMessage(context, "Invalid url format");

    }

    private void ShowFileInclude(HttpContext context, string id)
    {
        Guid fileInclude;
        try
        {
            fileInclude = new Guid(id);
        }
        catch
        {
            ShowMessage(context, "Invalid url format");
            return;
        }
        FileInclude file = KbContext.CurrentKb.ManagerFileInclude.GetByID(fileInclude);
        if (file == null)
        {
            ShowFileNotExists(context.Response);
            return;
        }
        byte[] data = KbContext.CurrentKb.ManagerFileInclude.GetData(fileInclude);

        if (data == null)
        {
            ShowFileNotExists(context.Response);
            return;
        }
        context.SendFile(ResponseTransfer.SendMode.Inline, file.FileName, data);
    }

    private void ShowUserFile(HttpContext context, string id)
    {
        int userFileId;
        try
        {
            userFileId = int.Parse(id);
        }
        catch
        {
            ShowMessage(context, "Invalid url format");
            return;
        }

        UserFile file = KbContext.CurrentKb.ManagerUserFile.GetByID(userFileId);
        if (file == null)
        {
            ShowFileNotExists(context.Response);
            return;
        }
        byte[] data = KbContext.CurrentKb.ManagerUserFile.GetData(userFileId);

        if (data == null)
        {
            ShowFileNotExists(context.Response);
            return;
        }
        context.SendFile(ResponseTransfer.SendMode.Inline, file.FileName, data);

    }

    public bool IsReusable
    {
        get { return false; }
    }

    private void ShowFileNotExists(HttpResponse response)
    {
        response.StatusCode = 404;
        response.StatusDescription = "File not exists";
        response.End();
    }

    private void ShowMessage(HttpContext context, string message)
    {
        context.Response.ContentType = Mime.MimeTypeText;
        context.Response.Write(message);
    }
    

}