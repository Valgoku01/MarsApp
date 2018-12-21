using MarsApp.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsAppTest.Utilities
{
    [TestClass]
    public class ByteUtilitiesTest
    {
        [TestMethod]
        public void ByteUtilitiesTest_GetBytes()
        {
            var utilities = new ByteUtilities();

            var bytes = new byte[] { 97, 32, 90, 32, 51 };
            var newBytes = utilities.GetBytes("a Z 3");

            Assert.AreEqual(bytes[0], newBytes[0]);
            Assert.AreEqual(bytes[1], newBytes[1]);
            Assert.AreEqual(bytes[2], newBytes[2]);
            Assert.AreEqual(bytes[3], newBytes[3]);
            Assert.AreEqual(bytes[4], newBytes[4]);

            newBytes = utilities.GetBytes("");
            Assert.AreEqual(0, newBytes.Length);
        }

        [TestMethod]
        public void ByteUtilitiesTest_IsByteValidInAscii()
        {
            var utilities = new ByteUtilities();

            Assert.IsTrue(utilities.IsByteValidInAscii(97));
            Assert.IsFalse(utilities.IsByteValidInAscii(20));
        }
    }
}
