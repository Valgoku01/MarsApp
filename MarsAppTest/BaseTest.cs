using MarsApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsAppTest
{
    [TestClass]
    public class BaseTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var baseToTest = new Base();
            Assert.IsTrue(baseToTest.InitBase());
            baseToTest.Dispose();
        }
    }
}
