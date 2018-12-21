using MarsApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsAppTest.ViewModel
{
    [TestClass]
    public class EngineViewModelTest : BaseApp
    {
        [TestMethod]
        public void EngineViewModelTest_ParseData()
        {
            FileUtilities.Lines.Add("5 5");
            FileUtilities.Lines.Add("1 2 E");
            FileUtilities.Lines.Add("LMLMLMLMM");
            FileUtilities.Lines.Add("3 3 N");
            FileUtilities.Lines.Add("MMRMMRMRRM");

            var engine = new EngineViewModel(Container);

            // true
            Assert.IsTrue(engine.ParseData(new string[] { "file.txt" }));

            FileUtilities.Lines.Clear();

            // false
            Assert.IsFalse(engine.ParseData(new string[] { "" }));
            Assert.IsFalse(engine.ParseData(new string[] { }));
            Assert.IsFalse(engine.ParseData(null));

            engine.Dispose();
        }

        [TestMethod]
        public void EngineViewModelTest_MakeMoves()
        {
            FileUtilities.Lines.Add("5 5");
            FileUtilities.Lines.Add("1 2 N");
            FileUtilities.Lines.Add("LMLMLMLMM");
            FileUtilities.Lines.Add("3 3 E");
            FileUtilities.Lines.Add("MMRMMRMRRM");

            var engine = new EngineViewModel(Container);
            engine.ParseData(new string[] { "file.txt" });

            var lines = engine.MakeMoves();
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual("1 3 N", lines[0]);
            Assert.AreEqual("5 1 E", lines[1]);

            engine.Dispose();
        }
    }
}
