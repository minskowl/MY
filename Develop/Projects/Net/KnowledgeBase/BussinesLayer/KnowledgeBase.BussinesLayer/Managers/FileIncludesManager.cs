using System;
using System.Collections.Generic;
using System.IO;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using KnowledgeBase.DAL;
using Savchin.Data;

namespace KnowledgeBase.BussinesLayer.Managers
{

    /// <summary>
    /// FileInclude Manager class
    ///</summary>
    public class FileIncludeManager : ManagerBase<IFileIncludeFactory, FileInclude, FileIncludeValue>, IFileIncludeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileIncludeManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        public FileIncludeManager(KbContext context, IFactoryProvider provider)
            : base(context,provider.CreateFileIncludeFactory())
        {
     
        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        public IList<FileInclude> GetAll()
        {
            return Wrap(Factory.SelectAll());
        }
        /// <summary>
        /// Ges the FileInclude by ID.
        /// </summary>
        /// <param name="FileIncludeID">The FileIncludeID.</param>
        /// <returns></returns>        
        public FileInclude GetByID(System.Guid FileIncludeID)
        {
            return Wrap(Factory.SelectByID(FileIncludeID));
        }



        /// <summary>
        /// Get FileInclude values KnowledgeID .
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        /// <returns>List of FileInclude</returns>   
        public IList<FileInclude> GetByKnowledgeID(System.Int32 KnowledgeID)
        {
            return Wrap(Factory.SelectByKnowledgeID(KnowledgeID));
        }


        /// <summary>
        /// Saves the specified FileInclude.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Save(FileInclude entity)
        {
            if(entity.Context==null) entity.Context = Context;

            entity.Validate();
            if (Identifier.IsValid(entity.FileIncludeID))
            {
                Factory.Update(entity.ObjectValue);
            }
            else
            {
                entity.FileIncludeID = Guid.NewGuid();
                try
                {
                    Factory.Insert(entity.ObjectValue);
                }
                catch
                {
                    entity.FileIncludeID = Guid.Empty;
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public byte[] GetData(Guid id)
        {
            return Factory.GetData(id);
        }
        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="data">The data.</param>
        public void SetData(Guid id, byte[] data)
        {
            Factory.SetData(id, data);
        }


        /// <summary>
        /// Creates from user file.
        /// </summary>
        /// <param name="userFileId">The user file id.</param>
        /// <param name="fileIncludeID">The file include ID.</param>
        /// <param name="knowledgeID">The knowledge ID.</param>
        public void CreateFromUserFile(int userFileId, Guid fileIncludeID, int knowledgeID)
        {
            Factory.CreateFromUserFile(userFileId, fileIncludeID, knowledgeID);
        }

        /// <summary>
        /// Creates the file include.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public FileInclude Create(int knowledgeID, string fileName, byte[] content)
        {
            FileInclude include = new FileInclude();
            include.KnowledgeID = knowledgeID;
            include.FileName = fileName;
            include.Size = content.Length;
            int attempt = 0;
        tryagain:
            attempt++;
            try
            {
                Save(include);
            }
            catch (NotUniqueException)
            {
                if (attempt > 10)
                    throw;


                include.FileName = Path.GetFileNameWithoutExtension(fileName) + attempt + Path.GetExtension(fileName);
                goto tryagain;

            }


            SetData(include.FileIncludeID, content);

            return include;

        }
        /// <summary>
        /// Deletes the specified FileInclude.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(FileInclude entity)
        {
            Factory.Delete(entity.ObjectValue);
        }
        /// <summary>
        /// Deletes the name of the by knowledge ID by file.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="fileName">Name of the file.</param>
        public void DeleteByKnowledgeIDByFileName(System.Int32 knowledgeID, string fileName)
        {
            Factory.DeleteByKnowledgeIDByFileName(knowledgeID, fileName);
        }

    }
}