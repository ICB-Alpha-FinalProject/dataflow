using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataflowICB.UnitTests.DummyUnitTest
{
    [TestClass]
    public class DummyUnitTest
    {
        [TestMethod]
        public void DummyTest()
        {
            HttpClient client = new HttpClient();
            var mockedClient = new Mock<HttpClient>();
            

        }
    }
}
