using System.Collections.Generic;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer
{

    /// <summary>
    /// UserRight Manager class
    ///</summary>
    public class UserRightManager : ManagerBase<IUserRightFactory, UserRight, UserRightValue>, IUserRightManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRightManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        public UserRightManager(KbContext context, IFactoryProvider provider)
            : base(context, provider.CreateUserRightFactory())
        {
         
        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        public IList<UserRight> GetAll()
        {
            return Wrap(Factory.SelectAll());
        }
        /// <summary>
        /// Ges the UserRight by ID.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <param name="UserID">The UserID.</param>
        /// <returns></returns>        
        public UserRight GetByID(System.Int32 CategoryID, System.Int32 UserID)
        {
            return Wrap(Factory.SelectByID(CategoryID, UserID));
        }



        /// <summary>
        /// Get UserRight values CategoryID .
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <returns>List of UserRight</returns>   
        public IList<UserRight> GetByCategoryID(System.Int32 CategoryID)
        {
            return Wrap(Factory.SelectByCategoryID(CategoryID));
        }

        /// <summary>
        /// Get UserRight values UserID .
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        /// <returns>List of UserRight</returns>   
        public IList<UserRight> GetByUserID(System.Int32 UserID)
        {
            return Wrap(Factory.SelectByUserID(UserID));
        }

        /// <summary>
        /// Saves the rights.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="rights">The rights.</param>
        public virtual void SaveRights(int userID, IEnumerable<CategoryPermission> rights)
        {
            Context.RequireUserAdminPermission();
            Factory.SaveRights(userID,  rights);
        }

        ///// <summary>
        ///// Saves the specified UserRight.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        //public void Save(UserRight entity)
        //{
        //    entity.Validate();
        //    if (Identifier.IsValid(entity.CategoryID) && Identifier.IsValid(entity.UserID))
        //    {
        //        factory.Update(entity.ObjectValue);
        //    }
        //    else
        //    {
        //        factory.Insert(entity.ObjectValue);
        //    }
        //}


        /// <summary>
        /// Deletes the specified UserRight.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(UserRight entity)
        {
            Factory.Delete(entity.ObjectValue);
        }

    }
}