using NUnit.Framework;
using Savchin.TimeManagment;

namespace ToolsTest.DateTimeManagment
{
    [TestFixture]
    public class TimeRangeTests
    {
        [Test]
        public void IsInRangeTest()
        {
            //Inner range
            var range = new TimeRange(8, 20);
            Assert.IsTrue(range.IsInRange(new Time(8, 0)));
            Assert.IsTrue(range.IsInRange(new Time(9, 0)));
            Assert.IsTrue(range.IsInRange(new Time(20, 0)));

            Assert.IsFalse(range.IsInRange(new Time(7, 59)));
            Assert.IsFalse(range.IsInRange(new Time(20, 1)));


            //Outter range
            range = new TimeRange(20,8);
            Assert.IsTrue(range.IsInRange(new Time(7, 0)));
            Assert.IsTrue(range.IsInRange(new Time(8, 0)));
            Assert.IsTrue(range.IsInRange(new Time(20, 0)));
            Assert.IsTrue(range.IsInRange(new Time(21, 0)));

            Assert.IsFalse(range.IsInRange(new Time(19, 59)));
            Assert.IsFalse(range.IsInRange(new Time(8, 1)));
        }

        /// <summary>
        /// Empties the rage test.
        /// </summary>
        [Test]
        public void EmptyRageTest()
        {
            var range = new TimeRange();
            Assert.IsTrue(range.IsEmpty);
            Assert.AreEqual(range.From, range.To);
            Assert.AreEqual(range.From, Time.MinValue);
        }

    }
}