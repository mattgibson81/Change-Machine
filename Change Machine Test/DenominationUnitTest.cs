using Microsoft.VisualStudio.TestTools.UnitTesting;
using Change_Machine;

namespace Change_Machine_Test
{
    [TestClass]
    public class DenominationUnitTest
    {
        [TestMethod]
        public void Test_Total()
        {
            Denomination denomination = new Denomination(100);
            denomination.Quantity = 2;
            Assert.IsTrue(denomination.Total == 200, "Total is not correct.");
        }
    }
}
