using System.Collections.Generic;
using System.Linq;

namespace Change_Machine
{
    internal abstract class CalculateChange : ICalculateChange
    {
        private protected List<Denomination> _denominations;
        private protected string _currencySymbol;

        public CalculateChange()
        {
            _denominations = new List<Denomination>();
        }

        string ICalculateChange.CurrencySymbol => _currencySymbol;

        List<Denomination> ICalculateChange.Denominations => _denominations;

        void ICalculateChange.CalculateChangeToGive(decimal totalCost, decimal totalAmountGiven)
        {
            //Validate inputs
            if (totalCost <= 0)
            {
                throw new CalculateChangeException(string.Format("Total cost must be greater than zero."));
            }
            if (totalAmountGiven <= 0)
            {
                throw new CalculateChangeException(string.Format("Total amount given must be greater than zero."));
            }
            if (totalAmountGiven < totalCost)
            {
                throw new CalculateChangeException(string.Format("Total amount given must be greater than or equal to total cost."));
            }

            //Calculate total change
            decimal totalChangeToGive = (totalAmountGiven - totalCost);

            //Calculate minimum number of denominations required
            calculateDenominatios(totalChangeToGive);
        }

        private void calculateDenominatios(decimal totalChange)
        {
            //Using LINQ to ensure sorted by amount - probably overkill again using LINQ for this
            var denominations = _denominations.OrderByDescending(denomination => denomination.Amount);
            foreach (Denomination denomination in denominations)
            {
                if (totalChange >= denomination.Amount)
                {
                    denomination.Quantity = (int)(totalChange / denomination.Amount);
                    totalChange -= denomination.Total;
                }
            }
        }
    }
}