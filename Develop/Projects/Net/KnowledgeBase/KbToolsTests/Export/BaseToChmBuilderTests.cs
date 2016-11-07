
using System;
using KnowledgeBase.Dal;
using KnowledgeBase.KbTools.Export;
using KnowledgeBase.Mssql.Dal;
using NUnit.Framework;
using Savchin.Text;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.BussinesLayer.Tests.Export
{
    [TestFixture]
    public class BaseToChmBuilderTests
    {
        [Test]
        public void Test()
        {
            KbContext.CurrentKb = new KbContext(new DalSingleThreadProvider(new MssqlFactoryProvider("")));
            var builder=  new BaseToChmBuilder();
            builder.Build(@"D:\Tmp\help.chm", "http://192.168.1.154/KnowledgeBase/");

            foreach (var error in builder.Errors)
            {
                Console.WriteLine(StringUtil.ToString(error));
            }

         
        }
    }
}
