using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotvaSpider.Core;
using BotvaSpider.Logging;
using NUnit.Framework;
using Savchin.Utils;

namespace BotvaSpider.Tests
{
    [TestFixture]
    public class LogEntryTests
    {
        [Test]
        public void XmlSerializationTest()
        {
            var e = new LogEntry
                        {
                            Date = DateTime.Now,
                            Message = "Message",
                            Title = "Title",
                            Object = CreateException(),
                            Source = LoggerType.Mine
                        };

            var xml = TypeSerializer<LogEntry>.ToXmlString(e);

            Assert.IsNotEmpty(xml);
            Console.WriteLine(xml);

            var de = TypeSerializer<LogEntry>.FromXmlString(xml);

            Assert.AreEqual(e.Title,de.Title);
            Assert.AreEqual(e.Date.Date, de.Date.Date);
            Assert.AreEqual(e.Message, de.Message);
            Assert.AreEqual(e.Source, de.Source);
        }

        private Exception CreateException()
        {
            try
            {
                throw new ArgumentException("Test", "11");
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
