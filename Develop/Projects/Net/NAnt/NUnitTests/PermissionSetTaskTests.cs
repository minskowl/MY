using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using NAnt.Savchin.Tasks.IO;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class PermissionSetTaskTests
    {
        [Test]
        public void Test()
        {
            //var task = new PermissionSetTask
            //               {
            //                   Rights = FileSystemRights.FullControl,
            //                   File = new FileInfo(@"U:\KnowledgeBase.mdf")
            //               };
            //task.Execute();

            var file = new FileInfo(@"U:\KnowledgeBase.mdf");
            var security = new FileSecurity();
            var identity = WindowsIdentity.GetCurrent();

            var rule = new FileSystemAccessRule(identity.User, FileSystemRights.FullControl, AccessControlType.Allow);
            security.SetAccessRule(rule);
            file.SetAccessControl(security);
        }
    }
}
