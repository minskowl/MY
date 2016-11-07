/******************************************
* Auto-generated by CodeRocket
* 5/14/2010 5:05:17 PM
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
    /// Category Factory class
    ///</summary>
    public partial class CategoryFactory : DbFactoryBase<CategoryValue>
    {
        private const string SelectQuery = "SELECT [CategoryID],[CreationDate],[Name],[ParentCategoryID] FROM [Categories]";
        private int _ordinalCategoryID;
        private int _ordinalCreationDate;
        private int _ordinalName;
        private int _ordinalParentCategoryID;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryFactory"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CategoryFactory(DalContext context)
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
            _ordinalName = reader.GetOrdinal("Name");
            _ordinalParentCategoryID = reader.GetOrdinal("ParentCategoryID");
        }

        /// <summary>
        /// Maps the IDataReader values to a Category object
        ///</summary>
        /// <param name="reader">The IDataReader to map</param>
        protected override CategoryValue MapObject(IDataReader reader)
        {
            var result = new CategoryValue();
            result.CategoryID = reader.GetInt32(_ordinalCategoryID);
            result.CreationDate = reader.GetDateTime(_ordinalCreationDate);
            result.Name = reader.GetString(_ordinalName);
            result.ParentCategoryID = reader.IsDBNull(_ordinalParentCategoryID) ? (System.Int32?)null : reader.GetInt32(_ordinalParentCategoryID);
            return result;
        }





        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public void Insert(CategoryValue value)
        {
            var command = Database.CreateSqlCommand(@"
INSERT INTO [Categories] ([Name],[ParentCategoryID])
VALUES (@Name,@ParentCategoryID);
SELECT last_insert_rowid();
			");
            command.AddInputParameter("@Name", DbType.String, value.Name);
            command.AddInputParameter("@ParentCategoryID", DbType.Int32, value.ParentCategoryID);
            value.CategoryID = (System.Int32)(long)Database.ExecuteScalar(command);

        }




        /// <summary>
        /// Updates the specified Category.
        /// </summary>
        /// <param name="value">The Category value.</param>
        public void Update(CategoryValue value)
        {
            var command = Database.CreateSqlCommand(@"
		UPDATE [Categories]
		SET [CreationDate]=@CreationDate,[Name]=@Name,[ParentCategoryID]=@ParentCategoryID
		WHERE  [CategoryID]=@CategoryID ;");
            command.AddInputParameter("@CategoryID", DbType.Int32, value.CategoryID);
            command.AddInputParameter("@CreationDate", DbType.DateTime, value.CreationDate);
            command.AddInputParameter("@Name", DbType.String, value.Name);
            command.AddInputParameter("@ParentCategoryID", DbType.Int32, value.ParentCategoryID);
            Database.ExecuteNonQuery(command);

        }
        /// <summary>
        /// Gets Category by ID.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <returns></returns>
        public CategoryValue SelectByID(System.Int32 CategoryID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + "	WHERE  [CategoryID]=@CategoryID ");
            command.AddInputParameter("@CategoryID", DbType.Int32, CategoryID);
            return SelectSingle(command);
        }

        /// <summary>
        /// Deletes the specified Category.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        public void Delete(System.Int32 CategoryID)
        {
            var command = Database.CreateSqlCommand(@"
		DELETE FROM [Categories]
		 WHERE  [CategoryID]=@CategoryID ;");
            command.AddInputParameter("@CategoryID", DbType.Int32, CategoryID);
            Database.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Deletes the specified Category.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Delete(CategoryValue value)
        {
            Delete(value.CategoryID);
        }
        /// <summary>
        /// Selects all Category values.
        /// </summary>
        /// <returns>List of all Category</returns>
        public IList<CategoryValue> SelectAll()
        {
            return Select(Database.CreateSqlCommand(SelectQuery));
        }


        /// <summary>
        /// Selects Category values ParentCategoryID .
        /// ForeignKey: FK_Categories_Categories
        /// </summary>
        /// <param name="ParentCategoryID">The ParentCategoryID.</param>
        /// <returns>List of Category</returns>   
        public IList<CategoryValue> SelectByParentCategoryID(System.Int32 ParentCategoryID)
        {
            var command = Database.CreateSqlCommand(SelectQuery + " WHERE  [ParentCategoryID]=@ParentCategoryID ;");
            command.AddInputParameter("@ParentCategoryID", DbType.Int32, ParentCategoryID);
            return Select(command);
        }
    }
}