using MarsApp.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsAppTest.Utilities
{
    [TestClass]
    public class EnumUtilitiesTest
    {
        [TestMethod]
        public void EnumUtilitiesTest_GetDirection()
        {
            var utilities = new EnumUtilities();
            Assert.AreEqual(EnumUtilities.Direction.L, utilities.GetDirection('L'));
            Assert.AreEqual(EnumUtilities.Direction.R, utilities.GetDirection('R'));
            Assert.AreEqual(EnumUtilities.Direction.E, utilities.GetDirection('E'));
            Assert.AreEqual(EnumUtilities.Direction.O, utilities.GetDirection('O'));
            Assert.AreEqual(EnumUtilities.Direction.S, utilities.GetDirection('S'));
            Assert.AreEqual(EnumUtilities.Direction.N, utilities.GetDirection('N'));
            Assert.AreEqual(EnumUtilities.Direction.WRONG, utilities.GetDirection('A'));
        }

        [TestMethod]
        public void EnumUtilitiesTest_GetDirectionString()
        {
            var utilities = new EnumUtilities();
            Assert.AreEqual("L", utilities.GetDirectionString(EnumUtilities.Direction.L));
            Assert.AreEqual("R", utilities.GetDirectionString(EnumUtilities.Direction.R));
            Assert.AreEqual("E", utilities.GetDirectionString(EnumUtilities.Direction.E));
            Assert.AreEqual("O", utilities.GetDirectionString(EnumUtilities.Direction.O));
            Assert.AreEqual("N", utilities.GetDirectionString(EnumUtilities.Direction.N));
            Assert.AreEqual("S", utilities.GetDirectionString(EnumUtilities.Direction.S));
            Assert.AreEqual("wrong", utilities.GetDirectionString(EnumUtilities.Direction.WRONG));
        }
    }
}
