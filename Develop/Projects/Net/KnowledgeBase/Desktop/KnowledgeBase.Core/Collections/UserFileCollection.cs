using System.IO;
using System.Linq;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using FileInfo = KnowledgeBase.Core.FileInfo;

namespace KnowledgeBase.Desktop.Collections
{
    /// <summary>
    /// FileCollection
    /// </summary>
    public class UserFileCollection : FileCollectionBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFileCollection"/> class.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="context">The context.</param>
        public UserFileCollection(int userId, KbContext context)
            : base(userId, Path.Combine(AppCore.Settings.ContentPath, "user" + userId + "\\"), context)
        {

            CopyFrom(Context.ManagerUserFile.GetByUserID(userId)
                         .Select(e => new FileInfo(e,FilesDir)));

        }


        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns></returns>
        protected override IFile CreateNew(System.IO.FileInfo info)
        {
            return new UserFile
                       {
                           FileName = info.Name,
                           Size = (int)info.Length,
                           UserID = (int)OwnerId
                       };
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        protected override byte[] GetData(FileInfo file)
        {
            return Context.ManagerUserFile.GetData((int)file.ID);
        }

        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void InsertItem(int index, FileInfo item)
        {
            var file = (UserFile)item.Instance;
            Context.ManagerUserFile.Save(file);
            Context.ManagerUserFile.SetData(file.UserFileID, File.ReadAllBytes(item.Path));

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="index">The index.</param>
        protected override void RemoveItem(int index)
        {
            Context.ManagerUserFile.Delete((UserFile)base[index].Instance);
            base.RemoveItem(index);
        }

    }
}
