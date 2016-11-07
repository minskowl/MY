/******************************************
* Auto-generated by CodeRocket
* 5/17/2010 12:02:37 PM
******************************************/
using System;
using System.Collections.Generic;
using System.Data;

using KnowledgeBase.Dal;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.SqlLite.Dal.Factories
{

    /// <summary>
    /// Knowledge Factory class
    ///</summary>
    public partial class KnowledgeFactory : DbFactoryBase<KnowledgeValue>
    {
        private const string SelectQuery = "SELECT [CategoryID],[CreationDate],[CreatorID],[KnowledgeID],[KnowledgeStatusID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[PublicID],[Summary],[Title] FROM [Knowledges]";
        private int _ordinalCategoryID;
        private int _ordinalCreationDate;
        private int _ordinalCreatorID;
        private int _ordinalKnowledgeID;
        private int _ordinalKnowledgeStatusID;
        private int _ordinalKnowledgeTypeID;
        private int _ordinalModificationDate;
        private int _ordinalModificatorID;
        private int _ordinalPublicID;
        private int _ordinalSummary;
        private int _ordinalTitle;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeFactory"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public KnowledgeFactory(DalContext context)
            : base(context)
        {

        }
        /// <summary>
        /// Inits the ordinals.
        /// </summary>
        /// <param name="reader">The reader.</param>		    
        protected override void InitOrdinals(IDataReader reader)
        {
            _ordinalCategoryID = reader.GetOrdinal("CategoryID");
            _ordinalCreationDate = reader.GetOrdinal("CreationDate");
            _ordinalCreatorID = reader.GetOrdinal("CreatorID");
            _ordinalKnowledgeID = reader.GetOrdinal("KnowledgeID");
            _ordinalKnowledgeStatusID = reader.GetOrdinal("KnowledgeStatusID");
            _ordinalKnowledgeTypeID = reader.GetOrdinal("KnowledgeTypeID");
            _ordinalModificationDate = reader.GetOrdinal("ModificationDate");
            _ordinalModificatorID = reader.GetOrdinal("ModificatorID");
            _ordinalPublicID = reader.GetOrdinal("PublicID");
            _ordinalSummary = reader.GetOrdinal("Summary");
            _ordinalTitle = reader.GetOrdinal("Title");
        }

        /// <summary>
        /// Maps the IDataReader values to a Knowledge object
        ///</summary>
        /// <param name="reader">The IDataReader to map</param>
        protected override KnowledgeValue MapObject(IDataReader reader)
        {
            var result = new KnowledgeValue();
            result.CategoryID = reader.GetInt32(_ordinalCategoryID);
            result.CreationDate = reader.GetDateTime(_ordinalCreationDate);
            result.CreatorID = reader.GetInt32(_ordinalCreatorID);
            result.KnowledgeID = reader.GetInt32(_ordinalKnowledgeID);
            result.KnowledgeStatusID = reader.GetByte(_ordinalKnowledgeStatusID);
            result.KnowledgeTypeID = reader.GetInt16(_ordinalKnowledgeTypeID);
            result.ModificationDate = reader.IsDBNull(_ordinalModificationDate) ? (System.DateTime?)null : reader.GetDateTime(_ordinalModificationDate);
            result.ModificatorID = reader.IsDBNull(_ordinalModificatorID) ? (System.Int32?)null : reader.GetInt32(_ordinalModificatorID);
            result.PublicID = reader.GetGuid(_ordinalPublicID);
            result.Summary = reader.IsDBNull(_ordinalSummary) ? null : reader.GetString(_ordinalSummary);
            result.Title = reader.GetString(_ordinalTitle);
            return result;
        }



        partial void BeforeInser(KnowledgeValue value);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public void Insert(KnowledgeValue value)
        {
            BeforeInser(value);
            var command = Database.CreateSqlCommand(@"
INSERT INTO [Knowledges] ([CategoryID],[CreatorID],[KnowledgeStatusID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[PublicID],[Summary],[Title])
VALUES (@CategoryID,@CreatorID,@KnowledgeStatusID,@KnowledgeTypeID,@ModificationDate,@ModificatorID,@PublicID,@Summary,@Title);
SELECT last_insert_rowid();
			");
            command.AddInputParameter("@CategoryID", DbType.Int32, value.CategoryID);
            command.AddInputParameter("@CreatorID", DbType.Int32, value.CreatorID);
            command.AddInputParameter("@KnowledgeStatusID", DbType.Byte, value.KnowledgeStatusID);
            command.AddInputParameter("@KnowledgeTypeID", DbType.Int16, value.KnowledgeTypeID);
            command.AddInputParameter("@ModificationDate", DbType.DateTime, value.ModificationDate);
            command.AddInputParameter("@ModificatorID", DbType.Int32, value.ModificatorID);
            command.AddInputParameter("@PublicID", DbType.Guid, value.PublicID);
            command.AddInputParameter("@Summary", DbType.String, value.Summary);
            command.AddInputParameter("@Title", DbType.String, value.Title);
            value.KnowledgeID = (System.Int32)(long)Database.ExecuteScalar(command);

        }




        /// <summary>
        /// Updates the specified Knowledge.
        /// </summary>
        /// <param name="value">The Knowledge value.</param>
        public void Update(KnowledgeValue value)
        {
            var command = Database.CreateSqlCommand(@"
		UPDATE [Knowledges]
		SET [CategoryID]=@CategoryID,[CreationDate]=@CreationDate,[CreatorID]=@CreatorID,[KnowledgeStatusID]=@KnowledgeStatusID,[KnowledgeTypeID]=@KnowledgeTypeID,[ModificationDate]=@ModificationDate,[ModificatorID]=@ModificatorID,[PublicID]=@PublicID,[Summary]=@Summary,[Title]=@Title
		WHERE  [KnowledgeID]=@KnowledgeID ;");
            command.AddInputParameter("@CategoryID", DbType.Int32, value.CategoryID);
            command.AddInputParameter("@CreationDate", DbType.DateTime, value.CreationDate);
            command.AddInputParameter("@CreatorID", DbType.Int32, value.CreatorID);
            command.AddInputParameter("@KnowledgeID", DbType.Int32, value.KnowledgeID);
            command.AddInputParameter("@KnowledgeStatusID", DbType.Byte, value.KnowledgeStatusID);
            command.AddInputParameter("@KnowledgeTypeID", DbType.Int16, value.KnowledgeTypeID);
            command.AddInputParameter("@ModificationDate", DbType.DateTime, value.ModificationDate);
            command.AddInputParameter("@ModificatorID", DbType.Int32, value.ModificatorID);
            command.AddInputParameter("@PublicID", DbType.Guid, value.PublicID);
            command.AddInputParameter("@Summary", DbType.String, value.Summary);
            command.AddInputParameter("@Title", DbType.String, value.Title);
            Database.ExecuteNonQuery(command);

        }
        /// <summary>
        /// Gets Knowledge by ID.
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        /// <returns></returns>
        public KnowledgeValue SelectByID(System.Int32 KnowledgeID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + "	WHERE  [KnowledgeID]=@KnowledgeID ");
            command.AddInputParameter("@KnowledgeID", DbType.Int32, KnowledgeID);
            return SelectSingle(command);
        }

        /// <summary>
        /// Deletes the specified Knowledge.
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        public void Delete(System.Int32 KnowledgeID)
        {
            var command = Database.CreateSqlCommand(@"
		DELETE FROM [Knowledges]
		 WHERE  [KnowledgeID]=@KnowledgeID ;");
            command.AddInputParameter("@KnowledgeID", DbType.Int32, KnowledgeID);
            Database.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Deletes the specified Knowledge.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Delete(KnowledgeValue value)
        {
            Delete(value.KnowledgeID);
        }
        /// <summary>
        /// Selects all Knowledge values.
        /// </summary>
        /// <returns>List of all Knowledge</returns>
        public IList<KnowledgeValue> SelectAll()
        {
            return Select(Database.CreateSqlCommand(SelectQuery));
        }


        /// <summary>
        /// Selects Knowledge values CategoryID .
        /// ForeignKey: FK_Knowledges_Categories
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<KnowledgeValue> SelectByCategoryID(System.Int32 CategoryID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + " WHERE  [CategoryID]=@CategoryID ;");
            command.AddInputParameter("@CategoryID", DbType.Int32, CategoryID);
            return Select(command);
        }

        /// <summary>
        /// Selects Knowledge values CreatorID .
        /// ForeignKey: FK_Knowledges_Creator
        /// </summary>
        /// <param name="CreatorID">The CreatorID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<KnowledgeValue> SelectByCreatorID(System.Int32 CreatorID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + " WHERE  [CreatorID]=@CreatorID ;");
            command.AddInputParameter("@CreatorID", DbType.Int32, CreatorID);
            return Select(command);
        }

        /// <summary>
        /// Selects Knowledge values KnowledgeStatusID .
        /// ForeignKey: FK_Knowledges_KnowledgeStatuses
        /// </summary>
        /// <param name="KnowledgeStatusID">The KnowledgeStatusID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<KnowledgeValue> SelectByKnowledgeStatusID(System.Byte KnowledgeStatusID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + " WHERE  [KnowledgeStatusID]=@KnowledgeStatusID ;");
            command.AddInputParameter("@KnowledgeStatusID", DbType.Byte, KnowledgeStatusID);
            return Select(command);
        }

        /// <summary>
        /// Selects Knowledge values KnowledgeTypeID .
        /// ForeignKey: FK_Knowledges_KnowledgeTypes
        /// </summary>
        /// <param name="KnowledgeTypeID">The KnowledgeTypeID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<KnowledgeValue> SelectByKnowledgeTypeID(System.Int16 KnowledgeTypeID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + " WHERE  [KnowledgeTypeID]=@KnowledgeTypeID ;");
            command.AddInputParameter("@KnowledgeTypeID", DbType.Int16, KnowledgeTypeID);
            return Select(command);
        }

        /// <summary>
        /// Selects Knowledge values ModificatorID .
        /// ForeignKey: FK_Knowledges_Modificator
        /// </summary>
        /// <param name="ModificatorID">The ModificatorID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<KnowledgeValue> SelectByModificatorID(System.Int32 ModificatorID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + " WHERE  [ModificatorID]=@ModificatorID ;");
            command.AddInputParameter("@ModificatorID", DbType.Int32, ModificatorID);
            return Select(command);
        }
    }
}