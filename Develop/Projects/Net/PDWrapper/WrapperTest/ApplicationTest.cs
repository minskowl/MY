using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using PDWrapper;
using NUnit.Framework;

namespace PDWrapperTest
{
    [TestFixture]
    public class ApplicationTest
    {

        [Test]
        public void CreatePDTest()
        {

            Application application = Application.CreatePD();
            Assert.IsNotNull(application);
            Assert.IsNotNull(application.ActiveWorkspace);
            Workspace workspace = application.ActiveWorkspace;
            Assert.IsNotNull(workspace);
            //foreach (object o in workspace.Children)
            //{
            //    Assert.IsNotNull(o);
            //}


        }

        [Test]
        public void CreatePD11Test()
        {

            Application application = Application.CreatePD11();
            Assert.IsNotNull(application);
            Assert.IsNotNull(application.ActiveWorkspace);
            Workspace workspace = application.ActiveWorkspace;
            Assert.IsNotNull(workspace);
            //foreach (object o in workspace.Children)
            //{
            //    Assert.IsNotNull(o);
            //}
        }
        [Test]
        public void CreatePD12Test()
        {

            Application application = Application.CreatePD12();
            Assert.IsNotNull(application);
            Assert.IsNotNull(application.ActiveWorkspace);
            Workspace workspace = application.ActiveWorkspace;
            Assert.IsNotNull(workspace);
            //foreach (object o in workspace.Children)
            //{
            //    Assert.IsNotNull(o);
            //}

        }
    }
}
