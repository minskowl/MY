using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Savchin.Text;
using Site.Bl;

namespace Site.Core.Tests
{
    [TestFixture]
    public class AdvancedStringClass
    {
        [Test]
        public void Test()
        {
            var str = new AdvancedString("Site.Bl.Resource, Site.Core", "pLastName");
            var firstVal = str.GetValue();

            var cul = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cul;
            Thread.CurrentThread.CurrentUICulture = cul;
            var secondVale = str.GetValue();
            Assert.IsTrue(string.Compare(firstVal, secondVale) != 0);
        }
        [Test]
        public void Test1()
        {
            var manager = new ResourceManager("Site.Bl.Resource", typeof(User).Assembly);

            Console.WriteLine(manager.GetString("pLastName"));
            var cul = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cul;
            Thread.CurrentThread.CurrentUICulture = cul;

            Console.WriteLine(manager.GetString("pLastName"));
            //"Site.Bl.Resource, Site.Core", "pLastName"
        }

    }
}
