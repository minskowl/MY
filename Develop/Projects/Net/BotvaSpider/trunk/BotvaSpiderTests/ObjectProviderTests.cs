using System;
using System.Linq;
using BotvaSpider.Automation.Mining;
using BotvaSpider.BookKeeping;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Gears;
using NUnit.Framework;
using Savchin.TimeManagment;

namespace BotvaSpider.Tests
{
    [TestFixture]
    public class ObjectProviderTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Assert.IsTrue(ObjectProvider.Instance.ConnectionIsValid());
        }

        /// <summary>
        /// Gets the fight log test.
        /// </summary>
        [Test]
        public void GetFightLogTest()
        {
            var result = ObjectProvider.Instance.GetFightLog(1);
            Assert.IsNotNull(result);
            Assert.Greater(result.Rows.Count, 0);
        }

        /// <summary>
        /// Gets the useres test.
        /// </summary>
        [Test]
        public void GetUseresTest()
        {
            var result = ObjectProvider.Instance.GetUsers();
            Assert.IsNotNull(result);
            var user = result[0];
            ObjectProvider.Instance.UpdateUser(user);
        }

        #region Cows

        /// <summary>
        /// Gets the get not cows.
        /// </summary>
        [Test]
        public void GetGetNotCows()
        {
            var result = ObjectProvider.Instance.GetNotCows();
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);
        }
        /// <summary>
        /// Gets the cristal owners test.
        /// </summary>
        [Test]
        public void GetCristalOwnersTest()
        {
            var result = ObjectProvider.Instance.GetCristalOwners(new LevelFilter(12, 100));
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);
        }

        /// <summary>
        /// Gets the guild users.
        /// </summary>
        [Test]
        public void GetGuildUsers()
        {
            var result = ObjectProvider.Instance.GetGuildUsers(GuildType.Miner, new LevelFilter(12, 100));
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);
        }
        [Test]
        public void GetPotentialCows()
        {
            var poterial = ObjectProvider.Instance.GetPotentialCows(new LevelFilter(19, 20), AppCore.AttackSettings.MinBenefit);
            Assert.IsNotNull(poterial);
            Assert.Greater(poterial.Count, 0);
        }
        [Test]
        public void GetCowByNameTest()
        {
            var result = ObjectProvider.Instance.GetCowsByName("HolyDoli");
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);

        }

        [Test]
        public void GetCowsTest()
        {
            var result = ObjectProvider.Instance.GetCows(new LevelFilter(9, 12));
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);
            foreach (var cow in result)
            {
                Assert.IsTrue(cow.Level > 8 && cow.Level < 13);
            }
        }

        [Test]
        public void GetCowsAllTest()
        {
            var result = ObjectProvider.Instance.GetCows();
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);

        }
        #endregion






        #region Accountant
        [Test]
        public void GetBalanceItemsTest()
        {
            var result = ObjectProvider.Instance.GetBalanceItems(DateRange.GetThisWeekRange());
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);

        }
        [Test]
        public void AddPurchaseTest()
        {

            var result = new BalanceItem();
            result.Item = "Кирка";
            result.Gold = 100;
            result.Category = BalanceCategory.Mine;
            result.BigTicket = true;
            result.IsProfit = false;

            ObjectProvider.Instance.AddBalanceItem(result);
            Assert.Greater(result.ID, 0);
        } 
        #endregion

        #region Mine
        /// <summary>
        /// Gets the top test.
        /// </summary>
        [Test]
        public void GetCrystalMapPositionsTopTest()
        {
            var result = ObjectProvider.Instance.GetCrystalMapPositionsTop(true);
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReadMineStatisticsTest()
        {
            var result = ObjectProvider.Instance.GetMineStatistics();
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);
        }

        [Test]
        public void GetCrystalMapLastAttemptMapTest()
        {
            var result = ObjectProvider.Instance.GetCrystalMapLastAttemptMap(true);
            Assert.IsNotNull(result);
            Assert.Greater(result.Count, 0);
        }
        [Test]
        public void GetCrystalMapLastAttemptTest()
        {
  
            var result = ObjectProvider.Instance.GetCrystalMapLastAttempt(true);

            Assert.Greater(result, 0);
        }

        [Test]
        public void AddMineStatisticsTest()
        {
            var result = new SearchCristalResult();
            result.Cristals = 5;
            result.Date = DateTime.Now;
            result.Coulomb = Coulomb.Unscrewer;
            result.BigTicket = true;
            result.MinerGear = MinerGear.Pick | MinerGear.Helmet;
            result.DoAttempt = true;
            result.Percentage = 80;
            result.Level = 38;
            result.SmallTicket = true;
            result.Spirit = SpiritType.AssiduousMiner;

            ObjectProvider.Instance.AddMineStatistics(result);
            Assert.Greater(result.StatisticsID, 0);
        } 
        #endregion


    }
}
