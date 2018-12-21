using MarsApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MarsAppTest.ViewModel
{
    [TestClass]
    public class SystemAppViewModelTest : BaseApp
    {
        [TestMethod]
        public void SystemAppViewModelTest_Nominal()
        {
            var noError = true;

            try
            {
                var system = new SystemAppViewModel();
                system.Run(new string[] { "file.txt" }, Container);
            }
            catch (Exception e)
            {
                var exception = e;
                noError = false;
            }

            Assert.IsTrue(noError);
        }
    }
}
