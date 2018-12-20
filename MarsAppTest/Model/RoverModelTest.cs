using MarsApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsAppTest.Model
{
    [TestClass]
    public class RoverModelTest : BaseClass
    {
        [TestMethod]
        public void RoverModelTest_StartRover()
        {
            var rover = new RoverModel(Container, 5, 5);

            // true
            Assert.IsTrue(rover.StartRover("1 1 N", "MMRM"));
            Assert.IsTrue(rover.StartRover("1 1 N", "aze"));

            // false
            Assert.IsFalse(rover.StartRover("2 3N", "MMRM"));
            Assert.IsFalse(rover.StartRover("11N", "aze"));
            Assert.IsFalse(rover.StartRover("1 1 N", ""));
            Assert.IsFalse(rover.StartRover("", ""));
            Assert.IsFalse(rover.StartRover("1 1", ""));
            Assert.IsFalse(rover.StartRover("1", ""));

            rover.Dispose();
        }

        [TestMethod]
        public void RoverModelTest_CalculateNewPositions()
        {
            var rover = new RoverModel(Container, 5, 5);

            rover.StartRover("1 1 N", "MMRM");
            Assert.AreEqual("2 3 E", rover.CalculateNewPosition());

            rover.StartRover("3 5 S", "M");
            Assert.AreEqual("3 4 S", rover.CalculateNewPosition());

            rover.Dispose();
        }

        [TestMethod]
        public void RoverModelTest_CalculateNewPositions_Errors()
        {
            var rover = new RoverModel(Container, 5, 5);

            rover.StartRover("1 1", "MMRM");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover("1 1 W", "MMRM");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.Dispose();
        }
    }
}
