using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer.Managers
{

    /// <summary>
    /// FileStorage Manager class
    ///</summary>
    public class FileStorageManager : ManagerBase<IFileStorageFactory, FileStorage, FileStorageValue>, IFileStorageManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileStorageManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FileStorageManager(KbContext context, IFactoryProvider provider)
            : base(context, provider.CreateFileStorageFactory())
        {

        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        public IList<FileStorage> GetAll()
        {
            return Wrap(Factory.SelectAll());
        }


        /// <summary>
        /// Ges the FileStorage by ID.
        /// </summary>
        /// <param name="FileStorageID">The FileStorageID.</param>
        /// <returns></returns>        
        public FileStorage GetByID(System.Int16 FileStorageID)
        {
            return Wrap(Factory.SelectByID(FileStorageID, Context.SettingsId));
        }

        /// <summary>
        /// Gets the by path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public FileStorage GetByPath(string path)
        {
            return Context.Storages
                .FirstOrDefault(storage => path.ToLower().StartsWith(storage.Path.ToLower()));
        }
    }
}