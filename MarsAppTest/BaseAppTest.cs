using MarsApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsAppTest
{
    [TestClass]
    public class BaseAppTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var baseToTest = new MarsApp.BaseApp();
            Assert.IsTrue(baseToTest.InitBase());
            baseToTest.Dispose();
        }
    }
}
