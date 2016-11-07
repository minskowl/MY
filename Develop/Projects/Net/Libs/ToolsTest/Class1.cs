using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Savchin.CodeParsing;

namespace ToolsTest
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test()
        {
            CodeParser parser = new CodeParser(CodeParser.ProgramLanguage.CSharp1);
            Assert.IsNotNull(parser);
            parser.ParseString(@"
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Savchin.CodeParsing;

namespace ToolsTest
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test()
        {
            CodeParser parser= new CodeParser(CodeParser.ProgramLanguage.CSharp1);
            Assert.IsNotNull(parser);
            parser.ParseString();
        }
    }
}
            ");

            Assert.IsNotNull(parser.GetRoot());
        }
    }
}
