using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Text;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class OtherTest
    {
        public void Test()
        {
            string URL = "http://localhost/WAMUser/";
 

            WebClient client = new WebClient();
            string res=client.DownloadString(URL);
            Assert.IsNotEmpty(res);
        }
    }
}
