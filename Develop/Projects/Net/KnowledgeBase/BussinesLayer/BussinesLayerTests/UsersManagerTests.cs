

/******************************************
* Auto-generated by CodeRocket
* 24.04.2008 22:17:53
******************************************/
using System;
using System.Collections.Generic;
using KnowledgeBase.BussinesLayer;
using NUnit.Framework;

namespace KnowledgeBase.BussinesLayer.Tests
{

	/// <summary>
	/// User Manager class
	///
	//</summary>
    public partial class UserManagerTests 
	{
        /// <summary>
        /// Adds User test.
        /// </summary>
        [Test]
        public void AddTest1()
        { 
            int countBefore = manager.GetAll().Count;
            User entity = CreateUser();
            int countAfter = manager.GetAll().Count;
            Assert.AreEqual(countBefore + 1, countAfter);
            
 
        }
    }
}