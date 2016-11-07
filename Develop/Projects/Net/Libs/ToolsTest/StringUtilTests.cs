using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Savchin.Text;


namespace ToolsTest
{
    [TestFixture]
    public class StringUtilTests
    {

        [Test]
        public void TraslitTest()
        {
            string pat = "��s����������d�����������f���������";
            Assert.IsFalse(StringUtil.HasRussianChars(StringUtil.Traslit(pat))); 
        }

        [Test]
        public void HasRussioanCharsTest()
        {
            Assert.IsTrue(StringUtil.HasRussianChars("dasjj�klkj"));
            Assert.IsTrue(StringUtil.HasRussianChars("das�jjklkj"));
            Assert.IsFalse(StringUtil.HasRussianChars("dasjjklkj"));
        }

//        [Test]
//        public void Test()
//        {
//            CodeParser parser = new CodeParser(CodeParser.ProgramLanguage.CSharp1);
//            Assert.IsNotNull(parser);
//            parser.ParseString(@"
//using System;
//using System.Collections.Generic;
//using System.Text;
//using NUnit.Framework;
//using Savchin.CodeParsing;
//
//namespace ToolsTest
//{
//    [TestFixture]
//    public class StringUtilTests
//    {
//        [Test]
//        public void Test()
//        {
//            CodeParser parser= new CodeParser(CodeParser.ProgramLanguage.CSharp1);
//            Assert.IsNotNull(parser);
//            parser.ParseString();
//        }
//    }
//}
//            ");

//            Assert.IsNotNull(parser.GetRoot());
//        }
    }
}
