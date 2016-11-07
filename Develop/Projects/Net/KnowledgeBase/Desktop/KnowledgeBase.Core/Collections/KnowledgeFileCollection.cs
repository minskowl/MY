using System;
using System.IO;
using System.Linq;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Desktop.Collections;
using FileInfo = KnowledgeBase.Core.FileInfo;

namespace KnowledgeBase.Core.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public  class KnowledgeFileCollection: FileCollectionBase
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="UserFileCollection"/> class.
        /// </summary>
        /// <param name="knowledgeId">The knowledge id.</param>
        /// <param name="context">The context.</param>
        public KnowledgeFileCollection(int knowledgeId,KbContext context)
            : base(knowledgeId, Path.Combine(AppCore.Settings.ContentPath, "article" + knowledgeId + "\\"), context)
        {

            CopyFrom(Context.ManagerFileInclude.GetByKnowledgeID(knowledgeId)
                         .Select(e => new FileInfo(e,FilesDir)));

        }


        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns></returns>
        protected override IFile CreateNew(System.IO.FileInfo info)
        {
            return new FileInclude
                       {
                           FileName = info.Name,
                           Size = (int)info.Length,
                           KnowledgeID = (int)OwnerId
                       };
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        protected override byte[] GetData(FileInfo file)
        {
            return Context.ManagerFileInclude.GetData((Guid)file.ID);
        }

        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void InsertItem(int index, FileInfo item)
        {
            var file = (FileInclude)item.Instance;
            Context.ManagerFileInclude.Save(file);
            Context.ManagerFileInclude.SetData(file.FileIncludeID, File.ReadAllBytes(item.Path));

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="index">The index.</param>
        protected override void RemoveItem(int index)
        {
            Context.ManagerFileInclude.Delete((FileInclude)base[index].Instance);
            base.RemoveItem(index);
        }

    }
}
