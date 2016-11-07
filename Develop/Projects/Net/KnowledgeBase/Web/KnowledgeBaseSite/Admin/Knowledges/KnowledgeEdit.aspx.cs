using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.SiteCore;
using Savchin.IO;
using Savchin.Validation;
using KnowledgeBase.SiteCore.Providers;

public partial class KnowledgeEdit : SitePage
{
    private int? id;
    private int? _parentId;
    readonly KnowledgeManager _manager = KbContext.CurrentKb.ManagerKnowledge;
    private readonly List<FileIncludeData> newFiles = new List<FileIncludeData>();

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        id = RequestIntId;
        KbContext.CurrentKb.CurrentKnowledgeID = id;
        _parentId = GetRequestInt(RedirectorAdmin.keyParentId);

        if (!Identifier.IsValid(id) && !Identifier.IsValid(_parentId))
        {
            GoToPreviousPage();
            return;
        }
        InitializeValidators(typeof(Knowledge));

        if (IsPostBack)
            return;

        InitWSYNGEditor();


        RadSpell1.ControlsToCheck = new string[] { textBoxTitle.ClientID };

        if (Identifier.IsValid(id))
        {
            Knowledge knowledge = _manager.GetByID(id.Value);
            if (knowledge == null || !knowledge.CanEdit)
            {
                GoToPreviousPage();
                return;
            }


            ShowKnowledge(knowledge);
        }
        else
        {
            if (!KbContext.CurrentKb.HasPermission(_parentId.Value, Permission.Publish))
            {
                GoToPreviousPage();
                return;
            }
            listCategory.SelectedIntValue = _parentId.Value;
        }
    }

    private void InitWSYNGEditor()
    {


        textBoxSummary.ImageManager.ContentProviderTypeName = typeof(KnowledgeContentProvider).AssemblyQualifiedName;
        textBoxSummary.ImageManager.ViewPaths = new string[] { "/" };
        textBoxSummary.ImageManager.MaxUploadFileSize = StorageSize.SizeMb * 1;
        textBoxSummary.DocumentManager.ContentProviderTypeName = typeof(StorageContentProvider).AssemblyQualifiedName;
        List<string> pathes = new List<string>();
        foreach (FileStorage storage in KbContext.CurrentKb.Storages)
        {
            pathes.Add(storage.Path);
        }
        textBoxSummary.DocumentManager.ViewPaths = pathes.ToArray();

        textBoxSummary.Snippets.Add("Code","<div class='code'><pre><code><br/></code></pre></div>");
    }

    private void ShowKnowledge(Knowledge knowledge)
    {
        header.Text = "Knowledge Edit";
        textBoxTitle.Text = knowledge.Title;
        textBoxSummary.Content = knowledge.Summary;
        listKnowledgeType.SelectedKnowledgeType = knowledge.KnowledgeType;
        listCategory.SelectedIntValue = knowledge.CategoryID;
        listKeywords.KeywordsID = knowledge.KewordsAssociations;
        listKnowledgeStatus.SelectedKnowledgeStatus = knowledge.KnowledgeStatus;

    }

    /// <summary>
    /// Handles the Click event of the buttonSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSave_Click(object sender, EventArgs e)
    {

        Knowledge knowledge;
        if (Identifier.IsValid(id))
        {
            knowledge = _manager.GetByID(id.Value);
            if (knowledge == null)
            {
                GoToPreviousPage();
                return;
            }
        }
        else
        {
            knowledge = new Knowledge();

        }
        knowledge.CategoryID = listCategory.SelectedIntValue;
        knowledge.Title = textBoxTitle.Text.Trim();
        knowledge.Summary = GetContent();
        knowledge.KnowledgeType = listKnowledgeType.SelectedKnowledgeType;
        knowledge.KnowledgeStatus = listKnowledgeStatus.SelectedKnowledgeStatus;
        FileIncludeManager fileIncludeManager = KbContext.CurrentKb.ManagerFileInclude;

        try
        {
            _manager.Save(knowledge, listKeywords.KeywordsID, listKeywords.NewKeywords);
            foreach (var file in newFiles)
            {
                fileIncludeManager.CreateFromUserFile(file.UserFileID, file.FileIncludeID, knowledge.KnowledgeID);
            }
            GoToPreviousPage();
        }
        catch (ValidationException ex)
        {
            ShowException(ex);
        }
    }
    private string GetContent()
    {
        string content = textBoxSummary.Content;
        Regex regexFiles = new Regex("(?<=(\"|')file:///)([^\"']*)(?=(\"|'))");


        MatchCollection matches = regexFiles.Matches(content);
        foreach (Match match in matches)
        {
            string path = HttpUtility.UrlDecode(match.Value);
            if (!File.Exists(path))
                continue;


            FileLink link = KbContext.CurrentKb.ManagerFileLink.GetByPath(path);
            if (link != null)
                content = content.Replace(
                    "file:///" + match.Value,
                    AppSettings.FileBaseUrl + link.PublicID.ToString().Replace("-", string.Empty));
        }
        string pattern = string.Format("(?<=(\"|'))((http://{0})?{1}\\?UserFileID=)(\\d+)(?=(\"|'))",
                                       Request.Url.Host,
                                       AppSettings.ImageProviderBaseUrl);

        Regex regexImages = new Regex(pattern);
        matches = regexImages.Matches(content);
        foreach (Match match in matches)
        {
            //0 4
            string url = match.Groups[0].Value;
            int id = int.Parse(match.Groups[4].Value);
            FileIncludeData newFile = new FileIncludeData(Guid.NewGuid(), id);
            newFiles.Add(newFile);

            string newUrl = string.Format("http://{0}{1}?ID={2}",
                                          Request.Url.Host,
                                          AppSettings.ImageProviderBaseUrl,
                                          newFile.FileIncludeID.ToString().Replace("-", string.Empty));
            content = content.Replace(url, newUrl);

        }
        return content;

    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        GoToPreviousPage();
    }

    private class FileIncludeData
    {
        private Guid fileIncludeID;
        private int userFileID;

        public FileIncludeData(Guid fileIncludeID, int userFileID)
        {
            this.FileIncludeID = fileIncludeID;
            this.UserFileID = userFileID;
        }

        public Guid FileIncludeID
        {
            get { return fileIncludeID; }
            set { fileIncludeID = value; }
        }

        public int UserFileID
        {
            get { return userFileID; }
            set { userFileID = value; }
        }
    }
}
