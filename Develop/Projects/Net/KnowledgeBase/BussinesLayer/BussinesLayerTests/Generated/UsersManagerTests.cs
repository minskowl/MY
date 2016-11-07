

/******************************************
* Auto-generated by CodeRocket
* 24.04.2008 22:20:18
******************************************/

using KnowledgeBase.BussinesLayer.Core;
using NUnit.Framework;

namespace KnowledgeBase.BussinesLayer.Tests
{

	/// <summary>
	/// User Manager class
	///
	//</summary>
    [TestFixture]
    public partial class UserManagerTests 
	{
        IUserManager manager = KbContext.CurrentKb.ManagerUser;

        public User CreateUser()
        {
            var entity = new User
                              {
                                  Email = Savchin.Core.Randomizer.GetString(10),
                                  FirstName = Savchin.Core.Randomizer.GetString(10),
                                  LastName = Savchin.Core.Randomizer.GetString(10),
                                  Login = Savchin.Core.Randomizer.GetString(10),
                                  Password = Savchin.Core.Randomizer.GetString(10)
                              };

            manager.Save(entity);
            return entity;
        }
        
        /// <summary>
        /// Adds User test.
        /// </summary>
        [Test]
        public void AddTest()
        { 
            int countBefore = manager.GetAll().Count;
            User entity = CreateUser();
            int countAfter = manager.GetAll().Count;
            Assert.AreEqual(countBefore + 1, countAfter);

            Assert.Greater(entity.UserID,0);

            manager.Delete(entity);
            Assert.AreEqual(countBefore , manager.GetAll().Count);
        }
    }
}