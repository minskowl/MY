using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using BotvaSpider.Core;
using NUnit.Framework;
using Savchin.Core;
using Savchin.SystemEnvironment;
using Savchin.Text;
using Savchin.Utils;

namespace BotvaSpider.Tests
{
    [TestFixture]
    public class OtherTests
    {
        [Test]
        public void FrameworksVersionTest()
        {
            //var type = typeof (InvestmentStrategy);
            //var array=Enum.GetValues(type);
            //var result = array.Cast<Enum>();

            //var res=result.Except(new Enum[] {InvestmentStrategy.Undefined});

            var collection = new FrameworksVersionCollection();
            foreach (var version in collection)
            {
                Console.WriteLine(version);
            }
        }
        [Test]
        public void Test()
        {
            var stri = "кристах";

            var result = HttpUtility.UrlEncode(Encoding.Default.GetBytes(stri));
            Console.WriteLine(result);


        }
        [Test]
        public void RandomTest()
        {
            DoTest(10);
            DoTest(100);
            DoTest(1000);
            DoTest(10000);
            DoTest(1000000);
            DoTest(10000000);
        }
        private void DoTest(int attempts)
        {
           //var r = new Random();
            var list = new List<int>();
            for (var i = 0; i < attempts; i++)
            {
                Thread.Sleep(1);
                list.Add(new Random().Next(0, 10));
            }
            var groups = list.GroupBy(e => e);

            var results = groups.Select(g => new Result { Integer = g.Key, Count = g.Count() }).OrderBy(e => e.Integer).ToArray();
            var builder = new StringBuilder(attempts + " попыток: ");
            foreach (var result in results)
            {
                builder.Append(string.Format("{0}={1},", result.Integer, result.Count));
            }
            Console.WriteLine(builder.ToString());
        }
    }

    public class Result
    {
        public int Integer { get; set; }
        public int Count { get; set; }
    }
}
