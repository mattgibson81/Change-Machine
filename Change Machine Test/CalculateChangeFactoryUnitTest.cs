using Microsoft.VisualStudio.TestTools.UnitTesting;
using Change_Machine;
using System.Linq;

namespace Change_Machine_Test
{
    [TestClass]
    public class CalculateChangeFactoryUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Unknown currency allowed.")]
        public void Test_UnknownCurrency()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("unknown", "1", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total cost blank allowed.")]
        public void Test_TotalCostNotEntered()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total cost not numeric allowed.")]
        public void Test_TotalCostNotADecimal()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "notadecimal", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total cost zero allowed.")]
        public void Test_TotalCostZero()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "0", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total cost negative allowed.")]
        public void Test_TotalCostNegative()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "-1", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total amount given blank allowed.")]
        public void Test_TotalAmountGivenNotEntered()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "1", "");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total amount given not numeric allowed.")]
        public void Test_TotalAmountGivenNotADecimal()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "1", "notadecimal");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total amount given zero allowed.")]
        public void Test_TotalAmountGivenZero()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "1", "0");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total amount given negative allowed.")]
        public void Test_TotalAmountGivenNegative()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "1", "-1");
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total amount given is less than total cost allowed.")]
        public void Test_TotalAmountGivenLessThanTotalCost()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "10", "1");
        }

        [TestMethod]
        public void Test_CurrencySymbolNotCorrect()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "10", "10");
            Assert.IsTrue(factory.CurrencySymbol == "£", "Currency symbol is not correct.");
        }

        [TestMethod]
        public void Test_DenominationsZeroNotCorrect()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "10", "10");

            var denominations = from denomination in factory.Denominations
                                where denomination.Quantity > 0
                                select denomination; 

            Assert.IsTrue(denominations.Count() == 0, "Number of denominations when no change is not correct.");
        }

        [TestMethod]
        public void Test_DenominationsNotCorrect()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "1", "89.88");

            var denominations = from denomination in factory.Denominations
                                where denomination.Quantity > 0
                                select denomination;

            Assert.IsTrue(denominations.Count() == 12, "Number of denominations is not correct.");
        }

        [TestMethod]
        public void Test_TotalChangeNotCorrect()
        {
            CalculateChangeFactory factory = new CalculateChangeFactory();
            factory.CalculateChangeToGive("GBP", "1", "10");
            Assert.IsTrue(factory.TotalChange == 9, "Total change is not correct.");
        }
    }
}