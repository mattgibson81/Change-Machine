using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Change_Machine
{
    internal class CalculateChangeFactory
    {
        private ICalculateChange _calculateChange;
        ServiceProvider _serviceProvider;

        internal CalculateChangeFactory()
        {
            _serviceProvider = new ServiceCollection()
                .AddTransient<ICalculateChange, CalculateChangeGBP>()
                .AddTransient<ICalculateChange, CalculateChangeUSD>()
                .AddTransient<ICalculateChange, CalculateChangeEUR>()
                .BuildServiceProvider();
        }

        //TODO - Could use reflection to get types that inherit CalculateChange instead, maybe implement a name property
        internal static string GetAvailableCurrencies() => "GBP, USD, EUR";

        internal void CalculateChangeToGive(string currencyEntered, string totalCostEntered, string totalAmountGivenEntered)
        {
            //Validate inputs
            decimal totalCost;
            if (!decimal.TryParse(totalCostEntered, out totalCost))
            {
                throw new CalculateChangeException(string.Format("Total cost is not valid."));
            }
            decimal totalAmountGiven;
            if (!decimal.TryParse(totalAmountGivenEntered, out totalAmountGiven))
            {
                throw new CalculateChangeException(string.Format("Total amount given is not valid."));
            }

            //TODO - Not the most elegant solution - potentially look to implement StructureMap (or another IoC) so that services can be retrieved by a name/key
            IEnumerable<ICalculateChange> calculateChangeServices = _serviceProvider.GetServices<ICalculateChange>();
            switch (currencyEntered.ToUpper())
            {
                case "GBP":
                    _calculateChange = calculateChangeServices.First(o => o.GetType() == typeof(CalculateChangeGBP));
                    break;
                case "USD":
                    _calculateChange = calculateChangeServices.First(o => o.GetType() == typeof(CalculateChangeUSD));
                    break;
                case "EUR":
                    _calculateChange = calculateChangeServices.First(o => o.GetType() == typeof(CalculateChangeEUR));
                    break;
                default:
                    throw new CalculateChangeException(string.Format("Unknown currency entered. {0} does not exist.", currencyEntered));
            }
            _calculateChange.CalculateChangeToGive(totalCost, totalAmountGiven);
        }

        internal List<Denomination> Denominations => _calculateChange.Denominations;

        internal string CurrencySymbol => _calculateChange.CurrencySymbol;

        internal decimal TotalChange
        {
            get
            {
                //TODO - Overkill probably (?) to use LINQ for this but demonstrates LINQ usage a bit more
                var total = from denomination in Denominations
                            where denomination.Quantity > 0
                            select new { GrandTotal = Denominations.Sum(x => x.Total) };
                if (total.Count() > 0)
                {
                    return total.First().GrandTotal;
                } else
                {
                    return 0;
                }
            }
        }
    }
}