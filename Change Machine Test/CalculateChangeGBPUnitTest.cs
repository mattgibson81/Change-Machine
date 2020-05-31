using Microsoft.VisualStudio.TestTools.UnitTesting;
using Change_Machine;
using System.Linq;

namespace Change_Machine_Test
{
    [TestClass]
    public class CalculateChangeGBPUnitTest
    {
        [TestMethod]
        public void Test_CurrencySymbol()
        {
            CalculateChangeGBP calculateChange = new CalculateChangeGBP();
            ICalculateChange calculateChange1 = calculateChange;
            Assert.IsTrue(calculateChange1.CurrencySymbol == "£", "Currency symbol is not correct.");
        }

        [DataTestMethod]
        [DataRow(0.01)]
        [DataRow(0.02)]
        [DataRow(0.05)]
        [DataRow(0.1)]
        [DataRow(0.2)]
        [DataRow(0.5)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(50)]
        public void Test_Denominations(double amount)
        {
            CalculateChangeGBP calculateChange = new CalculateChangeGBP();
            ICalculateChange calculateChange1 = calculateChange;
            
            var denominations = from denomination in calculateChange1.Denominations
                                where denomination.Amount == new decimal(amount)
                                select denomination;

            Assert.IsTrue(denominations.Count() == 1, string.Format("{0} denomination does not exist.", amount));
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total cost zero allowed.")]
        public void Test_TotalCostZero()
        {
            CalculateChangeGBP calculateChange = new CalculateChangeGBP();
            ICalculateChange calculateChange1 = calculateChange;
            calculateChange1.CalculateChangeToGive(new decimal(0), new decimal(1));
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total cost negative allowed.")]
        public void Test_TotalCostNegative()
        {
            CalculateChangeGBP calculateChange = new CalculateChangeGBP();
            ICalculateChange calculateChange1 = calculateChange;
            calculateChange1.CalculateChangeToGive(new decimal(-1), new decimal(1));
        }

        [TestMethod]
        [ExpectedException(typeof(CalculateChangeException), "Total amount given is less than total cost allowed.")]
        public void Test_TotalAmountGivenLessThanTotalCost()
        {
            CalculateChangeGBP calculateChange = new CalculateChangeGBP();
            ICalculateChange calculateChange1 = calculateChange;
            calculateChange1.CalculateChangeToGive(new decimal(10), new decimal(1));
        }

        [TestMethod]
        public void Test_DenominationsZeroNotCorrect()
        {
            CalculateChangeGBP calculateChange = new CalculateChangeGBP();
            ICalculateChange calculateChange1 = calculateChange;
            calculateChange1.CalculateChangeToGive(new decimal(10), new decimal(10));

            var denominations = from denomination in calculateChange1.Denominations
                                where denomination.Quantity > 0
                                select denomination;

            Assert.IsTrue(denominations.Count() == 0, "Number of denominations when no change is not correct.");
        }

        [TestMethod]
        public void Test_DenominationsNotCorrect()
        {
            CalculateChangeGBP calculateChange = new CalculateChangeGBP();
            ICalculateChange calculateChange1 = calculateChange;
            calculateChange1.CalculateChangeToGive(new decimal(1), new decimal(89.88));

            var denominations = from denomination in calculateChange1.Denominations
                                where denomination.Quantity > 0
                                select denomination;

            Assert.IsTrue(denominations.Count() == 12, "Number of denominations is not correct.");
        }
    }
}