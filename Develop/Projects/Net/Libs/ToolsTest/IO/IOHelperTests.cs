using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using NUnit.Framework;
using Savchin.IO;
using Savchin.WinApi;

namespace ToolsTest.IO
{
    [TestFixture]
    public class IOHelperTests
    {
        private static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\test.txt";
        /// <summary>
        /// Determines whether [has current identity accces to file test].
        /// </summary>
        [Test]
        public void HasCurrentIdentityAcccesToFileTest()
        {   
            var file = new FileInfo(filePath);
            var permission = FileSystemRights.Delete;

            File.WriteAllText(filePath, "test");
       

            Assert.IsTrue(IOHelper.HasCurrentIdentityAcccesToFile(file, permission));

           // IOHelper.RemoveCurrentIdentityFileRights(file, permission);
           // Assert.IsFalse(IOHelper.HasCurrentIdentityAcccesToFile(file, permission));

           // IOHelper.SetCurrentIdentityFileRights(file, permission);
           // Assert.IsTrue(IOHelper.HasCurrentIdentityAcccesToFile(file, permission));
            file.Delete();

        }

        [Test]
        public void Kernel32tEST()
        {
            var size = Kernel32.GetFileSize("D:\\Install");
            if(size==-1)
            {
                var ex=Win32.GetLastWin32Error();
                Console.WriteLine(ex);
            }
            else
            {
                Console.WriteLine(size);
            }
        }
    }
}
