using System;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using Savchin.TimeManagment;

namespace ToolsTest.DateTimeManagment
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class TimeTests
    {
        [Test]
        public void CompareTest()
        {
            var time = new Time(8, 8, 8, 8);
            var timeGreater = new Time(8, 8, 8, 9);
            var timeEq = new Time(8, 8, 8, 8);
            Assert.IsTrue(time < timeGreater);
            Assert.IsTrue(time <= timeGreater);
            Assert.IsFalse(time > timeGreater);
            Assert.IsFalse(time >= timeGreater);
            Assert.IsTrue(time != timeGreater);
            Assert.IsFalse(time == timeGreater);

            Assert.IsFalse(timeGreater < time);
            Assert.IsFalse(timeGreater <= time);
            Assert.IsTrue(timeGreater > time);
            Assert.IsTrue(timeGreater >= time);
            Assert.IsFalse(time >= timeGreater);
            Assert.IsTrue(time != timeGreater);
            Assert.IsFalse(time == timeGreater);

            Assert.IsFalse(time < timeEq);
            Assert.IsTrue(time <= timeEq);
            Assert.IsFalse(time > timeEq);
            Assert.IsTrue(time >= timeEq);
            Assert.IsFalse(time != timeEq);
            Assert.IsTrue(time == timeEq);
        }

        /// <summary>
        /// Toes the string test.
        /// </summary>
        [Test]
        public void ToStringTest()
        {
            var time = DateTime.Now;


            Assert.AreEqual(
                time.ToShortTimeString(),
                new Time(time, TimePrecision.WithMinutes).ToString());
            Assert.AreEqual(
                time.ToLongTimeString(),
                new Time(time, TimePrecision.WithSeconds).ToString());

            Console.WriteLine(new Time(time, TimePrecision.WithMilliseconds));


        }

        /// <summary>
        /// Serializes the test.
        /// </summary>
        [Test]
        public void SerializeTest()
        {
            var time = new Time(DateTime.Now);
            var ser = new XmlSerializer(typeof(Time));
            using (var stream = new MemoryStream())
            {
                ser.Serialize(stream,time);
                stream.Position = 0;
                var deserializedTime=(Time) ser.Deserialize(stream);
                Assert.AreEqual(time, deserializedTime);
            }


        }
    }
}