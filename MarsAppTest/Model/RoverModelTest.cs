using MarsApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsAppTest.Model
{
    [TestClass]
    public class RoverModelTest : BaseApp
    {
        [TestMethod]
        public void RoverModelTest_StartRover()
        {
            var rover = new RoverModel(Container);

            // true
            Assert.IsTrue(rover.StartRover(5, 5, "1 1 N", "MMRM"));
            Assert.IsTrue(rover.StartRover(5, 5, "1 1 N", "aze"));

            // false
            Assert.IsFalse(rover.StartRover(5, 5, "2 3N", "MMRM"));
            Assert.IsFalse(rover.StartRover(5, 5, "11N", "aze"));
            Assert.IsFalse(rover.StartRover(5, 5, "1 1 N", ""));
            Assert.IsFalse(rover.StartRover(5, 5, "", ""));
            Assert.IsFalse(rover.StartRover(5, 5, "1 1", ""));
            Assert.IsFalse(rover.StartRover(5, 5, "1", ""));

            rover.Dispose();
        }

        [TestMethod]
        public void RoverModelTest_CalculateNewPositions()
        {
            var rover = new RoverModel(Container);

            rover.StartRover(5, 5, "1 1 N", "MMRM");
            Assert.AreEqual("2 3 E", rover.CalculateNewPosition());

            rover.StartRover(5, 5, "3 5 S", "M");
            Assert.AreEqual("3 4 S", rover.CalculateNewPosition());

            // keep the same direction as previous when error occurs
            // 35S is a wrong position
            rover.StartRover(5, 5, "35S", "M");
            Assert.AreEqual("3 4 S", rover.CalculateNewPosition());

            rover.Dispose();

            rover.StartRover(54444444, 123456445, "54443444 123456425 S", "MML");
            Assert.AreEqual("54443444 123456423 E", rover.CalculateNewPosition());

            rover.Dispose();

            rover.StartRover(5, 5, "35S", "M");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.Dispose();
        }

        [TestMethod]
        public void RoverModelTest_CalculateNewPositions_Errors()
        {
            var rover = new RoverModel(Container);

            rover.StartRover(5, 5, "", "");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover(5, 5, " - -", "-('");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover(5, 5, "1 1", "MMRM");
            Assert.AreEqual("", rover.CalculateNewPosition());
            
            rover.StartRover(5, 5, "1 1 W", "MMRM");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover(5, 5, "1   ", "M");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover(0, 0, "1 1 S", "M");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover(0, 0, "1 1 S", "M---");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover(0, 0, "1 1111111( S", "M---");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.StartRover('r', 'r', "1 1 W", "MMRM");
            Assert.AreEqual("", rover.CalculateNewPosition());

            rover.Dispose();
        }
    }
}
