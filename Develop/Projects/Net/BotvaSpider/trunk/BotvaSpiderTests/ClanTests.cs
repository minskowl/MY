using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotvaSpider.Data;
using NUnit.Framework;

namespace BotvaSpider.Tests
{
    [TestFixture]
    public class ClanTests
    {
        [Test]
        public void GetClansTest()
        {
          var result=  ObjectProvider.Instance.GetClans();
          Assert.IsNotNull(result);
          Assert.Greater(result.Count,0);
        }

        [Test]
        public void GetClansByTag()
        {
            var result = ObjectProvider.Instance.GetClanByTag("676");
            Assert.IsNotNull(result);
            Assert.AreEqual("676",result.Tag);
        }
    }
}
