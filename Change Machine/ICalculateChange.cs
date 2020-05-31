using System.Collections.Generic;

namespace Change_Machine
{
    internal interface ICalculateChange
    {
        internal string CurrencySymbol 
        {
            get;
        }

        internal List<Denomination> Denominations
        {
            get;
        }

        internal void CalculateChangeToGive(decimal totalCost, decimal totalAmountGiven);
    }
}