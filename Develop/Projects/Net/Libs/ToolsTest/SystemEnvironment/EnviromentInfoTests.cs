using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using NUnit.Framework;
using Savchin.Core;
using Savchin.SystemEnvironment;

namespace ToolsTest.SystemEnvironment
{
    [TestFixture]
    public class EnviromentInfoTests
    {
        [Test]
        public void Test()
        {
            var info = EnviromentInfo.GetCurrent();
            var text = TypeSerializer<EnviromentInfo>.ToXmlString(info);
            Assert.IsNotEmpty(text);
            Console.WriteLine(text);
           // Console.WriteLine(Environment.WorkingSet);
            
            //Console.WriteLine(WindowsIdentity.GetCurrent().Name);
            //Console.WriteLine(System.Threading.Thread.CurrentPrincipal.Identity.Name);

            //foreach (var gr in WindowsIdentity.GetCurrent().Groups)
            //{
                
            //    Console.WriteLine(gr.Translate(typeof(NTAccount)).Value);
                
            //}
        }
    }
}
