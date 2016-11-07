using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Savchin.Core;

namespace ToolsTest.Core
{
    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void IntTest()
        {
            var r1 = new Range<int>(1, 3);

            Assert.AreEqual(r1.From,1);
            Assert.AreEqual(r1.To, 3);

            Assert.IsTrue(r1.IsInRange(2));
            Assert.IsTrue(r1.IsInRange(1));
            Assert.IsTrue(r1.IsInRange(3));

            Assert.IsFalse(r1.IsInRange(0));
            Assert.IsFalse(r1.IsInRange(4));

        }
        //[Test]
        //public void NullbleIntTest()
        //{
        //    var r1 = new Range<Nullable<int>>(1, 3);

        //    Assert.AreEqual(r1.From, 1);
        //    Assert.AreEqual(r1.To, 3);

        //    Assert.IsTrue(r1.IsInRange(2));
        //    Assert.IsTrue(r1.IsInRange(1));
        //    Assert.IsTrue(r1.IsInRange(3));

        //    Assert.IsFalse(r1.IsInRange(0));
        //    Assert.IsFalse(r1.IsInRange(4));

        //}
    }
}
